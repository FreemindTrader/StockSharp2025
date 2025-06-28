using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#nullable disable
namespace StockSharp.Studio.Core.Configuration;

public class LogConfig
{
    private readonly string _logSettingsFile;
    private readonly Lazy<LogManager> _manager;

    public static string OverrideLogsDirectory { get; set; }

    public void Save()
    {
        Paths.Serialize<SettingsStorage>(PersistableHelper.Save((IPersistable)this.Manager), this._logSettingsFile, true);
    }

    public LogConfig()
    {
        this._logSettingsFile = Path.Combine(Paths.AppDataPath, "logManager.json");
        this.LogsDir = Paths.LogsDir;
        this._manager = new Lazy<LogManager>((Func<LogManager>)(() =>
        {
            LogManager logManager = ServicesRegistry.LogManager ?? new LogManager();
            if (Paths.IsConfigExists(this._logSettingsFile))
            {
                string str = File.ReadAllText(this._logSettingsFile);
                string contents = str.Replace("StockSharp.Logging", "Ecng.Logging");
                if (str != contents)
                    File.WriteAllText(this._logSettingsFile, contents);
                if (!PersistableHelper.LoadIfNotNull((IPersistable)logManager, Paths.Deserialize<SettingsStorage>(this._logSettingsFile)))
                {
                    InitLogsDefault();
                }
                else
                {
                    FileLogListener fileLogListener = logManager.Listeners.OfType<FileLogListener>().FirstOrDefault<FileLogListener>((Func<FileLogListener, bool>)(fl => !StringHelper.IsEmpty(fl.LogDirectory)));
                    if (fileLogListener != null)
                        this.LogsDir = fileLogListener.LogDirectory;
                    if (!StringHelper.IsEmpty(LogConfig.OverrideLogsDirectory))
                    {
                        this.LogsDir = LogConfig.OverrideLogsDirectory;
                        CollectionHelper.ForEach<FileLogListener>(logManager.Listeners.OfType<FileLogListener>(), (Action<FileLogListener>)(fl => fl.LogDirectory = this.LogsDir));
                    }
                }
            }
            else
                InitLogsDefault();
            return logManager;

            void InitLogsDefault()
            {
                logManager.Listeners.Add((ILogListener)new FileLogListener()
                {
                    Append = true,
                    LogDirectory = this.LogsDir,
                    MaxLength = 104857600L /*0x06400000*/,
                    MaxCount = 10,
                    SeparateByDates = (SeparateByDateModes)2,
                    HistoryPolicy = (FileLogHistoryPolicies)1
                });
                Paths.Serialize<SettingsStorage>(PersistableHelper.Save((IPersistable)logManager), this._logSettingsFile, true);
            }
        }));
    }

    public string LogsDir { get; private set; }

    public LogManager Manager => this._manager.Value;

    public void DeleteFiles() => File.Delete(this._logSettingsFile);
}
