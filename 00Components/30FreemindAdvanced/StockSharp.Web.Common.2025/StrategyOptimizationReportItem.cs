// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyOptimizationReportItem
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Serialization;
using Newtonsoft.Json.Linq;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockSharp.Web.Common
{
    public class StrategyOptimizationReportItem : IPersistable
    {
        public int [ ] ParamTypeIdxs { get; set; }

        public StrategyParam [ ] Params { get; set; }

        
        public (DateTime time, StrategyPnL value) [ ] PnL { get; set; }

        public StrategyStatParameter [ ] StatParameters { get; set; }

        void IPersistable.Load( SettingsStorage storage )
        {
            this.ParamTypeIdxs = ( int [ ] ) storage.GetValue<int [ ]>( "ParamTypeIdxs",  null );
            var m0_1 = storage.GetValue<IEnumerable<SettingsStorage>>("Params",  null);
            this.Params = m0_1 != null ? ( ( IEnumerable<SettingsStorage> ) m0_1 ).Select<SettingsStorage, StrategyParam>( ( Func<SettingsStorage, StrategyParam> ) ( s => new StrategyParam()
            {
                Name = ( string ) s.GetValue<string>( "Name",  null ),
                UserId = ( string ) s.GetValue<string>( "UserId",  null ),
                Value = ( string ) s.GetValue<string>( "Value",  null )
            } ) ).ToArray<StrategyParam>() : ( StrategyParam [ ] ) null;
            var m0_2 = storage.GetValue<IEnumerable<SettingsStorage>>("PnL",  null);
            this.PnL = m0_2 != null ? ( ( IEnumerable<SettingsStorage> ) m0_2 ).Select<SettingsStorage, ValueTuple<DateTime, StrategyPnL>>( ( Func<SettingsStorage, ValueTuple<DateTime, StrategyPnL>> ) ( s => new ValueTuple<DateTime, StrategyPnL>( ( DateTime ) s.GetValue<DateTime>( "time",  new DateTime() ), ( StrategyPnL ) s.GetValue<StrategyPnL>( "value",  null ) ) ) ).ToArray<ValueTuple<DateTime, StrategyPnL>>() : ( ValueTuple<DateTime, StrategyPnL> [ ] ) null;
            var m0_3 = storage.GetValue<IEnumerable<SettingsStorage>>("StatParameters",  null);
            this.StatParameters = m0_3 != null ? ( ( IEnumerable<SettingsStorage> ) m0_3 ).Select<SettingsStorage, StrategyStatParameter>( ( Func<SettingsStorage, StrategyStatParameter> ) ( s => ( StrategyStatParameter ) PersistableHelper.Load<StrategyStatParameter>( s ) ) ).ToArray<StrategyStatParameter>() : ( StrategyStatParameter [ ] ) null;
        }

        void IPersistable.Save( SettingsStorage storage )
        {
            SettingsStorage settingsStorage1 = storage.Set<int[]>("ParamTypeIdxs",  this.ParamTypeIdxs);
            StrategyParam[] strategyParamArray = this.Params;
            SettingsStorage[] settingsStorageArray1 = strategyParamArray != null ? ((IEnumerable<StrategyParam>) strategyParamArray).Select<StrategyParam, SettingsStorage>((Func<StrategyParam, SettingsStorage>) (p => new SettingsStorage().Set<string>("Name",  p.Name).Set<string>("UserId",  p.UserId).Set<string>("Value",  p.Value))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            SettingsStorage settingsStorage2 = settingsStorage1.Set<SettingsStorage[]>("Params",  settingsStorageArray1).Set<SettingsStorage[]>("PnL",  ((IEnumerable<ValueTuple<DateTime, StrategyPnL>>) this.PnL).Select<ValueTuple<DateTime, StrategyPnL>, SettingsStorage>((Func<ValueTuple<DateTime, StrategyPnL>, SettingsStorage>) (p => new SettingsStorage().Set<DateTime>("time",  p.Item1).Set<StrategyPnL>("value",  p.Item2))).ToArray<SettingsStorage>());
            StrategyStatParameter[] statParameters = this.StatParameters;
            SettingsStorage[] settingsStorageArray2 = statParameters != null ? ((IEnumerable<StrategyStatParameter>) statParameters).Select<StrategyStatParameter, SettingsStorage>((Func<StrategyStatParameter, SettingsStorage>) (e => PersistableHelper.Save((IPersistable) e))).ToArray<SettingsStorage>() : (SettingsStorage[]) null;
            settingsStorage2.Set<SettingsStorage [ ]>( "StatParameters",  settingsStorageArray2 );
        }
    }
}
