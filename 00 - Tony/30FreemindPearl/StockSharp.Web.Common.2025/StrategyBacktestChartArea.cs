// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.StrategyBacktestChartArea
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Web.Common
{
    public class StrategyBacktestChartArea : IPersistable
    {
        public string GroupId { get; set; }

        public StrategyBacktestChartItem [ ] Items { get; set; }

        public string OrdersTitle { get; set; }

        public string TradesTitle { get; set; }

        void IPersistable.Load( SettingsStorage storage )
        {
            this.GroupId = ( string ) storage.GetValue<string>( "GroupId",  null );
            this.Items = ( ( IEnumerable<SettingsStorage> ) storage.GetValue<IEnumerable<SettingsStorage>>( "Items",  null ) ).Select<SettingsStorage, StrategyBacktestChartItem>( ( Func<SettingsStorage, StrategyBacktestChartItem> ) ( s => ( StrategyBacktestChartItem ) PersistableHelper.Load<StrategyBacktestChartItem>( s ) ) ).ToArray<StrategyBacktestChartItem>();
            this.OrdersTitle = ( string ) storage.GetValue<string>( "OrdersTitle",  null );
            this.TradesTitle = ( string ) storage.GetValue<string>( "TradesTitle",  null );
        }

        void IPersistable.Save( SettingsStorage storage )
        {
            storage.Set<string>( "GroupId",  this.GroupId ).Set<SettingsStorage [ ]>( "Items",  ( ( IEnumerable<StrategyBacktestChartItem> ) this.Items ).Select<StrategyBacktestChartItem, SettingsStorage>( ( Func<StrategyBacktestChartItem, SettingsStorage> ) ( e => PersistableHelper.Save( ( IPersistable ) e ) ) ).ToArray<SettingsStorage>() ).Set<string>( "OrdersTitle",  this.OrdersTitle ).Set<string>( "TradesTitle",  this.TradesTitle );
        }
    }
}
