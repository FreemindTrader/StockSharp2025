// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Configuration.LogConfig
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Common;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Configuration;
using StockSharp.Logging;
using System;
using System.IO;
using System.Linq;

namespace StockSharp.Studio.Core.Configuration
{
    public class LogConfig
    {
        private readonly string _logSettingsFile;
        private readonly Lazy<LogManager> _manager;

        public static string OverrideLogsDirectory { get; set; }

        public LogConfig()
        {
            this._logSettingsFile = Path.Combine( Paths.AppDataPath, "logManager.json" );
            this.LogsDir = Path.Combine( Paths.AppDataPath, "Logs" );
            this._manager = new Lazy<LogManager>( ( Func<LogManager> )( () =>
               {
                   LogManager logManager = ServicesRegistry.LogManager ?? new LogManager();
                   if ( this._logSettingsFile.IsConfigExists() )
                   {
                       if ( !logManager.LoadIfNotNull( this._logSettingsFile.Deserialize<SettingsStorage>() ) )
                       {
                           InitLogsDefault();
                       }
                       else
                       {
                           FileLogListener fileLogListener = logManager.Listeners.OfType<FileLogListener>().FirstOrDefault<FileLogListener>( ( Func<FileLogListener, bool> )( fl => !fl.LogDirectory.IsEmpty() ) );
                           if ( fileLogListener != null )
                               this.LogsDir = fileLogListener.LogDirectory;
                           if ( !LogConfig.OverrideLogsDirectory.IsEmpty() )
                           {
                               this.LogsDir = LogConfig.OverrideLogsDirectory;
                               logManager.Listeners.OfType<FileLogListener>().ForEach<FileLogListener>( ( Action<FileLogListener> )( fl => fl.LogDirectory = this.LogsDir ) );
                           }
                       }
                   }
                   else
                       InitLogsDefault();
                   return logManager;

                   void InitLogsDefault()
                   {
                       logManager.Listeners.Add( ( ILogListener )new FileLogListener()
                       {
                           Append = true,
                           LogDirectory = this.LogsDir,
                           MaxLength = 104857600L,
                           MaxCount = 10,
                           SeparateByDates = SeparateByDateModes.SubDirectories
                       } );
                       logManager.Save().Serialize<SettingsStorage>( this._logSettingsFile );
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
