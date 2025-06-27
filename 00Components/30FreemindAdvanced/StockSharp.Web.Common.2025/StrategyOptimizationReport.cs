// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyOptimizationReport
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Web.Common
{
    public class StrategyOptimizationReport : StrategyBaseReport, IPersistable
    {
        public string [ ] Types { get; set; }

        public StrategyOptimizationReportItem [ ] Items { get; set; }

        void IPersistable.Load( SettingsStorage storage )
        {
            this.Types = ( string [ ] ) storage.GetValue<string [ ]>( "Types",  null );
            var m0_1 = storage.GetValue<IEnumerable<SettingsStorage>>("Items",  null);
            this.Items = m0_1 != null ? ( ( IEnumerable<SettingsStorage> ) m0_1 ).Select<SettingsStorage, StrategyOptimizationReportItem>( ( Func<SettingsStorage, StrategyOptimizationReportItem> ) ( s => ( StrategyOptimizationReportItem ) PersistableHelper.Load<StrategyOptimizationReportItem>( s ) ) ).ToArray<StrategyOptimizationReportItem>() : ( StrategyOptimizationReportItem [ ] ) null;
            var m0_2 = storage.GetValue<IEnumerable<SettingsStorage>>("Logs",  null);
            this.Logs = m0_2 != null ? ( ( IEnumerable<SettingsStorage> ) m0_2 ).Select<SettingsStorage, StrategyLog>( ( Func<SettingsStorage, StrategyLog> ) ( s =>
            {
                return new StrategyLog()
                {
                    CreationDate = TimeHelper.UtcKind( ( DateTime ) s.GetValue<DateTime>( "CreationDate",  new DateTime() ) ),
                    Level = ( LogLevels ) s.GetValue<LogLevels>( "Level",  0 ),
                    Text = ( string ) s.GetValue<string>( "Text",  null )
                };
            } ) ).ToArray<StrategyLog>() : ( StrategyLog [ ] ) null;
            this.Error = ( StrategyErrorInfo ) storage.GetValue<StrategyErrorInfo>( "Error",  null );
        }

        void IPersistable.Save( SettingsStorage storage )
        {
            SettingsStorage settingsStorage1 = storage.Set<string[]>("Types",  this.Types);
            StrategyOptimizationReportItem[] items = this.Items;
            SettingsStorage[] settingsStorageArray1 = items != null ? ((IEnumerable<StrategyOptimizationReportItem>) items).Select<StrategyOptimizationReportItem, SettingsStorage>((Func<StrategyOptimizationReportItem, SettingsStorage>) (e => PersistableHelper.Save((IPersistable) e))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage2 = settingsStorage1.Set<SettingsStorage[]>("Items",  settingsStorageArray1);
            StrategyLog[] logs = this.Logs;
            SettingsStorage[] settingsStorageArray2 = logs != null ? ((IEnumerable<StrategyLog>) logs).Select<StrategyLog, SettingsStorage>((Func<StrategyLog, SettingsStorage>) (e => new SettingsStorage().Set<DateTime>("CreationDate",  e.CreationDate).Set<LogLevels>("Level",  e.Level).Set<string>("Text",  e.Text))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            settingsStorage2.Set<SettingsStorage [ ]>( "Logs",  settingsStorageArray2 ).Set<StrategyErrorInfo>( "Error",  this.Error );
        }
    }
}
