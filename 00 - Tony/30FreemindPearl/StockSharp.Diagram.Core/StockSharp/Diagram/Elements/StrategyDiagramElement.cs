// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.StrategyDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// The element which is using compiled strategy, based on S#.API.
  /// </summary>
  [DisplayNameLoc("Dll")]
  [CategoryLoc("Common")]
  [Doc("topics/Designer_DLL_Strategy.html")]
  [DescriptionLoc("DllDesc", false)]
  public class StrategyDiagramElement : BaseStrategyDiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260195108).To<Guid>();
    
    private readonly string _iconName = nameof(-1260195149);
    
    private readonly DiagramElementParam<StrategyType> \u0023\u003Dzhp250HO\u0024wWQn;
    
    private readonly SimpleResettableTimer \u0023\u003DzbW0gObU\u003D;
    
    private readonly ObservableCollectionEx<StrategyType> \u0023\u003DzPPq4ZZw\u003D;
    
    private readonly DispatcherObservableCollection<StrategyType> \u0023\u003Dz9fKgVNh1tmyu;
    
    private FileSystemWatcher \u0023\u003DziC3XLo0\u003D;
    
    private bool \u0023\u003Dz7jiJtePQYbum;
    
    private bool \u0023\u003DzxOsBInyhKgUH;
    
    private int \u0023\u003DzegMhDO\u0024npevo31CLRw\u003D\u003D;
    
    private readonly DiagramElementParam<string> \u0023\u003DzreONo8bwjKPV;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.StrategyDiagramElement" />.
    /// </summary>
    public StrategyDiagramElement()
    {
      this.\u0023\u003DzPPq4ZZw\u003D = new ObservableCollectionEx<StrategyType>();
      this.\u0023\u003Dz9fKgVNh1tmyu = new DispatcherObservableCollection<StrategyType>(DiagramElement.Dispatcher, (IListEx<StrategyType>) this.\u0023\u003DzPPq4ZZw\u003D);
      this.\u0023\u003DzreONo8bwjKPV = this.AddParam<string>(nameof(-1260195191), (string) null).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Strategy, LocalizedStrings.Str3182, LocalizedStrings.Str3191, 10).SetEditor<DiagramElementParam<string>>((Attribute) new EditorAttribute(typeof (IFileBrowserEditor), typeof (IFileBrowserEditor))).SetOnValueChangingHandler<string>(StrategyDiagramElement.LamdaShit.\u0023\u003DzCo7T0pqBBQWSyR3wKg\u003D\u003D ?? (StrategyDiagramElement.LamdaShit.\u0023\u003DzCo7T0pqBBQWSyR3wKg\u003D\u003D = new Action<string, string>(StrategyDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzGlg_PudavRgju3sdKgGrsn8\u003D))).SetOnValueChangedHandler<string>(new Action<string>(this.\u0023\u003DzqjtxII8ZBHzdKnwgS6dqevw\u003D));
      this.\u0023\u003Dzhp250HO\u0024wWQn = this.AddParam<StrategyType>(nameof(-1260195176), (StrategyType) null).SetDisplay<DiagramElementParam<StrategyType>>(LocalizedStrings.Strategy, LocalizedStrings.Str3188, LocalizedStrings.Str3188, 20).SetOnValueChangedHandler<StrategyType>(new Action<StrategyType>(this.\u0023\u003Dzuq2o2VB_6LyxGymo9OEu6xE\u003D)).SetSaveLoadHandlers<StrategyType>(new Func<StrategyType, SettingsStorage>(StrategyDiagramElement.\u0023\u003DzzcVdP8E\u003D), new Func<SettingsStorage, StrategyType>(this.\u0023\u003Dz\u00244x87xg\u003D)).SetNotifyOnChange<DiagramElementParam<StrategyType>>(false);
      this.\u0023\u003DzbW0gObU\u003D = new SimpleResettableTimer(TimeSpan.FromSeconds(2.0));
      this.\u0023\u003DzbW0gObU\u003D.Elapsed += new Action(this.\u0023\u003DzBrvlRFs\u003D);
    }

    /// <inheritdoc />
    public override Guid TypeId
    {
      get
      {
        return this._typeId;
      }
    }

    /// <inheritdoc />
    public override string IconName
    {
      get
      {
        return this._iconName;
      }
    }

    /// <summary>Path to assembly, where strategy executable code is.</summary>
    public string FileName
    {
      get
      {
        return this.\u0023\u003DzreONo8bwjKPV.Value;
      }
      set
      {
        this.\u0023\u003DzreONo8bwjKPV.Value = value;
      }
    }

    /// <summary>Available types.</summary>
    public IEnumerable<StrategyType> Types
    {
      get
      {
        return (IEnumerable<StrategyType>) this.\u0023\u003DzPPq4ZZw\u003D;
      }
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      if (this.\u0023\u003Dzhp250HO\u0024wWQn.Value == null)
        throw new InvalidOperationException(LocalizedStrings.StrategyNotSelected);
      if (this.Instance == null)
        throw new InvalidOperationException(LocalizedStrings.StrategyNotInitialized);
      this.\u0023\u003Dz7jiJtePQYbum = true;
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnStop()
    {
      base.OnStop();
      this.\u0023\u003Dz7jiJtePQYbum = false;
      if (!this.\u0023\u003DzxOsBInyhKgUH)
        return;
      this.\u0023\u003DzxOsBInyhKgUH = false;
      this.\u0023\u003DzW7E0bORMNucq();
    }

    private void \u0023\u003DzW7E0bORMNucq()
    {
      if (this.\u0023\u003Dz7jiJtePQYbum)
      {
        this.\u0023\u003DzxOsBInyhKgUH = true;
      }
      else
      {
        this.\u0023\u003DzegMhDO\u0024npevo31CLRw\u003D\u003D = 3;
        this.\u0023\u003DzbW0gObU\u003D.Reset();
      }
    }

    private void \u0023\u003DzBrvlRFs\u003D()
    {
      try
      {
        string fileName = this.FileName;
        byte[] rawAssembly;
        try
        {
          rawAssembly = File.ReadAllBytes(fileName);
        }
        catch (IOException ex)
        {
          --this.\u0023\u003DzegMhDO\u0024npevo31CLRw\u003D\u003D;
          if (this.\u0023\u003DzegMhDO\u0024npevo31CLRw\u003D\u003D <= 0)
            return;
          this.\u0023\u003DzbW0gObU\u003D.Reset();
          return;
        }
        StrategyType[] array = ((IEnumerable<Type>) Assembly.Load(rawAssembly).GetTypes()).Where<Type>(new Func<Type, bool>(BaseStrategyDiagramElement.IsTypeCompatible)).Select<Type, StrategyType>(StrategyDiagramElement.LamdaShit.\u0023\u003Dz7CH6dZRVQK_nQhlPWw\u003D\u003D ?? (StrategyDiagramElement.LamdaShit.\u0023\u003Dz7CH6dZRVQK_nQhlPWw\u003D\u003D = new Func<Type, StrategyType>(StrategyDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dzcz\u00241KfvpLPdTugspoQSU9vM\u003D))).ToArray<StrategyType>();
        this.\u0023\u003Dz9fKgVNh1tmyu.Clear();
        this.\u0023\u003Dz9fKgVNh1tmyu.AddRange((IEnumerable<StrategyType>) array);
        if (this.\u0023\u003Dzhp250HO\u0024wWQn.Value != null)
          this.\u0023\u003Dzhp250HO\u0024wWQn.Value = ((IEnumerable<StrategyType>) array).FirstOrDefault<StrategyType>(new Func<StrategyType, bool>(this.\u0023\u003DzLj0zCYcWFem5Iyminv9ZQHU\u003D));
        this.RaisePropertiesChanged();
      }
      catch (Exception ex)
      {
        this.AddErrorLog(nameof(-1260192409), (object[]) new object[2]
        {
          (object) this.FileName,
          (object) ex
        });
      }
    }

    private void \u0023\u003DzTMh05eBsUTk\u0024()
    {
      this.\u0023\u003Dz9fKgVNh1tmyu.Clear();
      this.\u0023\u003Dzhp250HO\u0024wWQn.Value = (StrategyType) null;
    }

    private static SettingsStorage \u0023\u003DzzcVdP8E\u003D(StrategyType _param0)
    {
      SettingsStorage settingsStorage = new SettingsStorage();
      string name = nameof(-1260192417);
      string str;
      if (_param0 == null)
      {
        str = (string) null;
      }
      else
      {
        Type type = _param0.Type;
        str = (object) type != null ? type.Name : (string) null;
      }
      if (str == null)
        str = _param0?.TypeName;
      settingsStorage.SetValue<string>(name, str);
      return settingsStorage;
    }

    private StrategyType \u0023\u003Dz\u00244x87xg\u003D(SettingsStorage _param1)
    {
      StrategyDiagramElement.\u0023\u003Dz0Omlz75hk26zqPkrpaKtJ_w\u003D omlz75hk26zqPkrpaKtJW = new StrategyDiagramElement.\u0023\u003Dz0Omlz75hk26zqPkrpaKtJ_w\u003D();
      omlz75hk26zqPkrpaKtJW.\u0023\u003DzivM7i2Q\u003D = _param1.GetValue<string>(nameof(-1260192417), (string) null);
      StrategyType strategyType = this.\u0023\u003DzPPq4ZZw\u003D.FirstOrDefault<StrategyType>(new Func<StrategyType, bool>(omlz75hk26zqPkrpaKtJW.\u0023\u003Dzc2fzXliT_ZgIwswuLg\u003D\u003D));
      if (strategyType == null && !omlz75hk26zqPkrpaKtJW.\u0023\u003DzivM7i2Q\u003D.IsEmpty())
        strategyType = new StrategyType()
        {
          TypeName = omlz75hk26zqPkrpaKtJW.\u0023\u003DzivM7i2Q\u003D
        };
      return strategyType;
    }

    private void \u0023\u003DzqjtxII8ZBHzdKnwgS6dqevw\u003D(string _param1)
    {
      if (this.\u0023\u003DziC3XLo0\u003D != null)
      {
        this.\u0023\u003DziC3XLo0\u003D.EnableRaisingEvents = false;
        this.\u0023\u003DziC3XLo0\u003D.Dispose();
        this.\u0023\u003DziC3XLo0\u003D = (FileSystemWatcher) null;
      }
      if (_param1.IsEmpty())
      {
        this.\u0023\u003DzTMh05eBsUTk\u0024();
      }
      else
      {
        this.\u0023\u003DzW7E0bORMNucq();
        this.\u0023\u003DziC3XLo0\u003D = new FileSystemWatcher(Path.GetDirectoryName(_param1), Path.GetFileName(_param1));
        this.\u0023\u003DziC3XLo0\u003D.Changed += new FileSystemEventHandler(this.\u0023\u003DzeO5gFQiRHBZf_D8gIxV3SSo\u003D);
        this.\u0023\u003DziC3XLo0\u003D.EnableRaisingEvents = true;
      }
    }

    private void \u0023\u003DzeO5gFQiRHBZf_D8gIxV3SSo\u003D(
      object _param1,
      FileSystemEventArgs _param2)
    {
      if (_param2.ChangeType != WatcherChangeTypes.Changed)
        return;
      this.\u0023\u003DzW7E0bORMNucq();
    }

    private void \u0023\u003Dzuq2o2VB_6LyxGymo9OEu6xE\u003D(StrategyType _param1)
    {
      try
      {
        IPersistable persistable;
        if (_param1 == null)
        {
          persistable = (IPersistable) null;
        }
        else
        {
          Type type = _param1.Type;
          persistable = (object) type != null ? type.CreateInstance<IPersistable>() : (IPersistable) null;
        }
        this.Instance = persistable;
        this.SetElementName(_param1?.Name);
      }
      catch (Exception ex)
      {
        this.Instance = (IPersistable) null;
        throw;
      }
    }

    private bool \u0023\u003DzLj0zCYcWFem5Iyminv9ZQHU\u003D(StrategyType _param1)
    {
      return _param1.TypeName.EqualsIgnoreCase(this.\u0023\u003Dzhp250HO\u0024wWQn.Value.TypeName);
    }

    private sealed class \u0023\u003Dz0Omlz75hk26zqPkrpaKtJ_w\u003D
    {
      public string \u0023\u003DzivM7i2Q\u003D;

      internal bool \u0023\u003Dzc2fzXliT_ZgIwswuLg\u003D\u003D(StrategyType _param1)
      {
        return _param1.TypeName.EqualsIgnoreCase(this.\u0023\u003DzivM7i2Q\u003D);
      }
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly StrategyDiagramElement.LamdaShit _lamdaShit = new StrategyDiagramElement.LamdaShit();
      public static Action<string, string> \u0023\u003DzCo7T0pqBBQWSyR3wKg\u003D\u003D;
      public static Func<Type, StrategyType> \u0023\u003Dz7CH6dZRVQK_nQhlPWw\u003D\u003D;

      internal void \u0023\u003DzGlg_PudavRgju3sdKgGrsn8\u003D(string _param1, string _param2)
      {
        if (_param2.IsEmpty())
          return;
        try
        {
          Assembly.Load(File.ReadAllBytes(_param2));
        }
        catch (BadImageFormatException ex)
        {
          throw new InvalidOperationException(LocalizedStrings.Str2897Params.Put((object[]) new object[1]{ (object) _param2 }));
        }
      }

      internal StrategyType \u0023\u003Dzcz\u00241KfvpLPdTugspoQSU9vM\u003D(Type _param1)
      {
        return new StrategyType() { Name = _param1.GetDisplayName((string) null), Description = _param1.GetDescription((string) null), Type = _param1, TypeName = (object) _param1 != null ? _param1.Name : (string) null };
      }
    }
  }
}
