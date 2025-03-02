// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.StrategyPnLDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// Position element (for security and money) for the specified portfolio.
  /// </summary>
  [CategoryLoc("Common")]
  [Doc("topics/Designer_StrategyPnLDiagramElement.html")]
  [DisplayNameLoc("PnLChange")]
  [DescriptionLoc("PnLChange", false)]
  public class StrategyPnLDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260192478).To<Guid>();
    
    private readonly string _iconName = nameof(-1260195712);
    
    private readonly DiagramSocket \u0023\u003Dz91xKVMYWRGs9cEpJ669yOFtAzXv_;
    
    private readonly DiagramSocket \u0023\u003DzlqwdU1z0U1ZK\u00241q\u0024wQ\u003D\u003D;
    
    private readonly DiagramSocket \u0023\u003DzUnhiWOWQ4UCY6lMAbzjID1w\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.PositionDiagramElement" />.
    /// </summary>
    public StrategyPnLDiagramElement()
    {
      this.\u0023\u003Dz91xKVMYWRGs9cEpJ669yOFtAzXv_ = this.AddOutput(StaticSocketIds.PnLUnreal, LocalizedStrings.PnLUnreal, DiagramSocketType.Unit, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzlqwdU1z0U1ZK\u00241q\u0024wQ\u003D\u003D = this.AddOutput(StaticSocketIds.PnLRealized, LocalizedStrings.PnLRealized, DiagramSocketType.Unit, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzUnhiWOWQ4UCY6lMAbzjID1w\u003D = this.AddOutput(StaticSocketIds.Commission, LocalizedStrings.Str159, DiagramSocketType.Unit, int.MaxValue, int.MaxValue, new bool?());
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

    /// <inheritdoc />
    protected override void OnStart()
    {
      this.Strategy.PnLReceived += new Action<Subscription>(this.\u0023\u003DzMlwNJnB6RrYjhyPOMw\u003D\u003D);
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnStop()
    {
      this.Strategy.PnLReceived -= new Action<Subscription>(this.\u0023\u003DzMlwNJnB6RrYjhyPOMw\u003D\u003D);
      base.OnStop();
    }

    private void \u0023\u003DzMlwNJnB6RrYjhyPOMw\u003D\u003D(Subscription _param1)
    {
      this.RaiseProcessOutput(this.\u0023\u003Dz91xKVMYWRGs9cEpJ669yOFtAzXv_, (object) new Unit(this.Strategy.PnLManager.UnrealizedPnL.GetValueOrDefault()), (DiagramSocketValue) null, _param1);
      this.RaiseProcessOutput(this.\u0023\u003DzlqwdU1z0U1ZK\u00241q\u0024wQ\u003D\u003D, (object) new Unit(this.Strategy.PnLManager.RealizedPnL), (DiagramSocketValue) null, _param1);
      this.RaiseProcessOutput(this.\u0023\u003DzUnhiWOWQ4UCY6lMAbzjID1w\u003D, (object) new Unit(this.Strategy.Commission.GetValueOrDefault()), (DiagramSocketValue) null, _param1);
    }
  }
}
