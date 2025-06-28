// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Configuration.StudioUserConfig
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79FA112F-39E9-4D2F-8DA4-EB9B4E826551
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Core.dll

using StockSharp.Studio.Core.Services;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Configuration;
using StockSharp.Messages;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
#nullable disable
namespace StockSharp.Studio.Core.Configuration;

public class StudioUserConfig : Disposable, IPersistableService
{
    private static readonly Lazy<StudioUserConfig> _instance = new Lazy<StudioUserConfig>((Func<StudioUserConfig>)(() => new StudioUserConfig()));
    private readonly CachedSynchronizedDictionary<string, StudioUserConfig.FileSettingsInfo> _fileServices = new CachedSynchronizedDictionary<string, StudioUserConfig.FileSettingsInfo>();
    private readonly CachedSynchronizedDictionary<string, StudioUserConfig.DirectorySettingsInfo> _directoryServices = new CachedSynchronizedDictionary<string, StudioUserConfig.DirectorySettingsInfo>();
    private readonly TimeSpan _period = TimeSpan.FromSeconds(5.0);
    private readonly SyncObject _flushingSync = new SyncObject();
    private readonly StudioUserConfig.FileSettingsInfo _fileSettings;
    private Timer _flushTimer;
    private bool _isFlushing;
    private bool _isDisposing;
    private readonly SyncObject _refCountSync = new SyncObject();
    private int _refCount;

    public static StudioUserConfig Instance => StudioUserConfig._instance.Value;

    public IBasketSecurityProcessorProvider ProcessorProvider { get; } = (IBasketSecurityProcessorProvider)new BasketSecurityProcessorProvider();

    public bool IsReseting { get; private set; }

    public LogConfig LogConfig { get; }

    protected StudioUserConfig()
    {
        Directory.CreateDirectory(Paths.AppDataPath);
        this._fileSettings = new StudioUserConfig.FileSettingsInfo(Path.Combine(Paths.AppDataPath, "settings.json"));
        this.LogConfig = new LogConfig();
        this._flushTimer = new Timer(new TimerCallback(this.OnFlush), (object)null, this._period, this._period);
    }

    public bool IsChangesSuspended
    {
        get
        {
            lock (this._refCountSync)
                return this._refCount > 0;
        }
    }

    public void SuspendChangesMonitor()
    {
        lock (this._refCountSync)
            ++this._refCount;
    }

    public void ResumeChangesMonitor()
    {
        lock (this._refCountSync)
            --this._refCount;
    }

    public void ResetSettings()
    {
        this.IsReseting = true;
        ((Disposable)ServicesRegistry.LogManager).Dispose();
        for (int index = 0; index < 5; ++index)
        {
            try
            {
                IOHelper.BlockDeleteDir(Paths.AppDataPath, true, 1000, 0);
                break;
            }
            catch
            {
                ThreadingHelper.Sleep(TimeSpan.FromSeconds(2.0));
            }
        }
    }

    protected override void DisposeManaged()
    {
        this._isDisposing = true;
        if (!this.IsReseting)
        {
            this.DisposeTimer();
            this.OnSave(true);
        }
        base.DisposeManaged();
    }

    private void OnFlush(object state)
    {
        lock (this._flushingSync)
        {
            if (this._isFlushing)
                return;
            this._isFlushing = true;
        }
        if (this._isDisposing)
            return;
        try
        {
            this.OnSave(false);
        }
        finally
        {
            lock (this._flushingSync)
                this._isFlushing = false;
        }
    }

    private void OnSave(bool force)
    {
        this._fileSettings.Save(force);
        foreach (StudioUserConfig.FileSettingsInfo cachedValue in this._fileServices.CachedValues)
            cachedValue.Save(force);
        foreach (StudioUserConfig.DirectorySettingsInfo cachedValue in this._directoryServices.CachedValues)
            cachedValue.Save(force);
    }

    private void DisposeTimer()
    {
        if (this._flushTimer == null)
            return;
        this._flushTimer.Dispose();
        this._flushTimer = (Timer)null;
    }

    public INamedPersistableService GetService(string group, string key)
    {
        if (group == null)
            throw new ArgumentNullException(nameof(group));
        if (key == null)
            throw new ArgumentNullException(nameof(key));
        return CollectionHelper.SafeAdd<string, StudioUserConfig.DirectorySettingsInfo>((IDictionary<string, StudioUserConfig.DirectorySettingsInfo>)this._directoryServices, group, (Func<string, StudioUserConfig.DirectorySettingsInfo>)(k => new StudioUserConfig.DirectorySettingsInfo(Path.Combine(Paths.AppDataPath, k)))).GetService(key);
    }

    public IEnumerable<INamedPersistableService> GetServices(string group)
    {
        if (group == null)
            throw new ArgumentNullException(nameof(group));
        return CollectionHelper.SafeAdd<string, StudioUserConfig.DirectorySettingsInfo>((IDictionary<string, StudioUserConfig.DirectorySettingsInfo>)this._directoryServices, group, (Func<string, StudioUserConfig.DirectorySettingsInfo>)(k => new StudioUserConfig.DirectorySettingsInfo(Path.Combine(Paths.AppDataPath, k)))).GetServices();
    }

    public void RemoveService(string group, string key)
    {
        if (group == null)
            throw new ArgumentNullException(nameof(group));
        if (key == null)
            throw new ArgumentNullException(nameof(key));
        ((SynchronizedDictionary<string, StudioUserConfig.DirectorySettingsInfo>)this._directoryServices)[group].RemoveService(key);
    }

    public bool ContainsKey(string key) => this._fileSettings.ContainsKey(key);

    public TValue GetValue<TValue>(string key, TValue defaultValue = default)
    {
        return this._fileSettings.GetValue<TValue>(key, defaultValue);
    }

    public void SetValue(string key, object value)
    {
        if (this.IsChangesSuspended)
            return;
        this._fileSettings.SetValue(key, value);
    }

    public void SetDelayValue(string key, Func<object> value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));
        if (this.IsChangesSuspended)
            return;
        this._fileSettings.SetDelayValue(key, value);
    }

    private class FileSettingsInfo(string settingsFile) : INamedPersistableService, IPersistableService
    {
        private readonly IDictionary<string, Func<object>> _delayValues;
        private readonly SyncObject _syncRoot;
        private readonly string _settingsFile = settingsFile ?? throw new ArgumentNullException(nameof(settingsFile));
        private SettingsStorage _values;
        private bool _isChanged;

        private SettingsStorage Values
        {
            get
            {
                if (this._values != null)
                    return this._values;
                LoggingHelper.DoWithLog((Action)(() =>
                {
                    if (!File.Exists(this._settingsFile))
                        return;
                    this._values = InvariantCultureSerializer.DeserializeInvariant(this._settingsFile);
                }));
                return this._values ?? (this._values = new SettingsStorage());
            }
        }

        public void Save(bool force)
        {
            LoggingHelper.DoWithLog((Action)(() =>
            {
                lock (this._syncRoot)
                {
                    if (!this._isChanged && !force)
                        return;
                    this._isChanged = false;
                }
                IDictionary<string, Func<object>> dictionary;
                lock (this._syncRoot)
                    dictionary = this._delayValues.Count == 0 ? (IDictionary<string, Func<object>>)null : CollectionHelper.ToDictionary<string, Func<object>>((IEnumerable<KeyValuePair<string, Func<object>>>)this._delayValues);
                if (dictionary != null)
                {
                    foreach (KeyValuePair<string, Func<object>> keyValuePair in (IEnumerable<KeyValuePair<string, Func<object>>>)dictionary)
                    {
                        try
                        {
                            object obj = keyValuePair.Value();
                            lock (this._syncRoot)
                                this.Values.Set<object>(keyValuePair.Key, obj);
                        }
                        catch (Exception ex)
                        {
                            LoggingHelper.LogError(ex, (string)null);
                        }
                    }
                }
                SettingsStorage clone = new SettingsStorage();
                lock (this._syncRoot)
                {
                    ICollection<KeyValuePair<string, object>> keyValuePairs = (ICollection<KeyValuePair<string, object>>)clone;
                    SettingsStorage values = this.Values;
                    int index = 0;
                    KeyValuePair<string, object>[] items = new KeyValuePair<string, object>[((SynchronizedDictionary<string, object>)values).Count];
                    foreach (KeyValuePair<string, object> keyValuePair in (SynchronizedDictionary<string, object>)values)
                    {
                        items[index] = keyValuePair;
                        ++index;
                    }
                    
                    keyValuePairs.AddRange(items);
                }
                LoggingHelper.DoWithLog((Action)(() =>
          {
              byte[] bytes = InvariantCultureSerializer.SerializeInvariant(clone, true);
              lock (this._syncRoot)
                  File.WriteAllBytes(this._settingsFile, bytes);
          }));
            }));
        }

        public string Name => Path.GetFileName(this._settingsFile);

        public bool ContainsKey(string key)
        {
            lock (this._syncRoot)
                return ((SynchronizedDictionary<string, object>)this.Values).ContainsKey(key);
        }

        public TValue GetValue<TValue>(string key, TValue defaultValue = default)
        {
            lock (this._syncRoot)
                return this.Values.GetValue<TValue>(key, defaultValue);
        }

        public void SetValue(string key, object value)
        {
            lock (this._syncRoot)
            {
                this.Values.Set<object>(key, value);
                this._isChanged = true;
            }
        }

        

        

        public void SetDelayValue(string key, Func<object> value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            lock (this._syncRoot)
            {
                this._delayValues[key] = value;
                this._isChanged = true;
            }
        }
    }

    private class DirectorySettingsInfo
    {
        private readonly CachedSynchronizedDictionary<string, StudioUserConfig.FileSettingsInfo> _persistableServices = new CachedSynchronizedDictionary<string, StudioUserConfig.FileSettingsInfo>();
        private readonly string _path;

        public DirectorySettingsInfo(string path)
        {
            this._path = path ?? throw new ArgumentNullException(nameof(path));
            Directory.CreateDirectory(this._path);
            foreach (string file in Directory.GetFiles(this._path, "*.json"))
                ((SynchronizedDictionary<string, StudioUserConfig.FileSettingsInfo>)this._persistableServices).Add(Path.GetFileName(file), new StudioUserConfig.FileSettingsInfo(file));
        }

        public INamedPersistableService GetService(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return (INamedPersistableService)CollectionHelper.SafeAdd<string, StudioUserConfig.FileSettingsInfo>((IDictionary<string, StudioUserConfig.FileSettingsInfo>)this._persistableServices, key, (Func<string, StudioUserConfig.FileSettingsInfo>)(k => new StudioUserConfig.FileSettingsInfo(this.GetPath(k))));
        }

        public IEnumerable<INamedPersistableService> GetServices()
        {
            return (IEnumerable<INamedPersistableService>)this._persistableServices.CachedValues;
        }

        public void RemoveService(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            ((SynchronizedDictionary<string, StudioUserConfig.FileSettingsInfo>)this._persistableServices).Remove(key);
            File.Delete(this.GetPath(key));
        }

        public void Save(bool force)
        {
            foreach (StudioUserConfig.FileSettingsInfo cachedValue in this._persistableServices.CachedValues)
                cachedValue.Save(force);
        }

        private string GetPath(string key) => Path.Combine(this._path, key);
    }
}
