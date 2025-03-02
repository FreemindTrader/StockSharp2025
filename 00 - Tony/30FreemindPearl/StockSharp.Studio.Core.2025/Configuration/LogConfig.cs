// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Configuration.LogConfig
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

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

namespace StockSharp.Studio.Core.Configuration
{
    public class LogConfig
    {
        private readonly string _logSettingsFile;
        private readonly Lazy<LogManager> _manager;

        public static string OverrideLogsDirectory { get; set; }

        public void Save()
        {
            PersistableHelper.Save( ( IPersistable ) this.Manager ).Serialize<SettingsStorage>( this._logSettingsFile, true );
        }

        public LogConfig()
        {
            this._logSettingsFile = Path.Combine( Paths.AppDataPath, "logManager.json" );
            this.LogsDir = Paths.LogsDir;
            this._manager = new Lazy<LogManager>( ( Func<LogManager> ) ( () =>
            {
                LogManager logManager = ServicesRegistry.LogManager ?? new LogManager();
                if ( this._logSettingsFile.IsConfigExists() )
                {
                    string str = File.ReadAllText(this._logSettingsFile);
                    string contents = str.Replace("StockSharp.Logging", "Ecng.Logging");
                    if ( str != contents )
                        File.WriteAllText( this._logSettingsFile, contents );
                    if ( !PersistableHelper.LoadIfNotNull( ( IPersistable ) logManager, this._logSettingsFile.Deserialize<SettingsStorage>() ) )
                    {
                        InitLogsDefault();
                    }
                    else
                    {
                        FileLogListener fileLogListener = ((IEnumerable) logManager.Listeners ).OfType<FileLogListener>().FirstOrDefault<FileLogListener>((Func<FileLogListener, bool>) (fl => !StringHelper.IsEmpty(fl.LogDirectory)));
                        if ( fileLogListener != null )
                            this.LogsDir = fileLogListener.LogDirectory;
                        if ( !StringHelper.IsEmpty( LogConfig.OverrideLogsDirectory ) )
                        {
                            this.LogsDir = LogConfig.OverrideLogsDirectory;
                            CollectionHelper.ForEach<FileLogListener>(  ( ( IEnumerable ) logManager.Listeners ).OfType<FileLogListener>(), ( fl => fl.LogDirectory = ( this.LogsDir ) ) );
                        }
                    }
                }
                else
                    InitLogsDefault();
                return logManager;

                void InitLogsDefault()
                {
                    IList<ILogListener> listeners = logManager.Listeners;
                    FileLogListener fileLogListener = new FileLogListener();
                    fileLogListener.Append = ( true );
                    fileLogListener.LogDirectory = ( this.LogsDir );
                    fileLogListener.MaxLength = ( 104857600L );
                    fileLogListener.MaxCount = ( 10 );
                    fileLogListener.SeparateByDates = ( ( SeparateByDateModes ) 2 );
                    fileLogListener.HistoryPolicy = ( ( FileLogHistoryPolicies ) 1 );
                    ( ( ICollection<ILogListener> ) listeners ).Add( ( ILogListener ) fileLogListener );
                    PersistableHelper.Save( ( IPersistable ) logManager ).Serialize<SettingsStorage>( this._logSettingsFile, true );
                }
            } ) );
        }

        public string LogsDir { get; private set; }

        public LogManager Manager
        {
            get
            {
                return this._manager.Value;
            }
        }

        public void DeleteFiles()
        {
            File.Delete( this._logSettingsFile );
        }
    }
}
