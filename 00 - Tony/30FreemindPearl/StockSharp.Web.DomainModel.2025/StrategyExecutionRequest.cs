﻿// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyExecutionRequest
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyExecutionRequest : BaseRequest
    {
        public StrategyExecutionRequest()
          : base( SubscriptionTypes.StrategyExecution )
        {
        }

        public StrategyExecutionModes Mode { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Mode = ( StrategyExecutionModes ) storage.GetValue<StrategyExecutionModes>( "Mode", 0 );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<StrategyExecutionModes>( "Mode", this.Mode );
        }
    }
}
