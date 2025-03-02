// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.StrategyInputDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Serialization;
using StockSharp.Algo.Strategies;
using StockSharp.Localization;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// The diagram element which supports subscription to strategy.
  /// </summary>
  public abstract class StrategyInputDiagramElement : DiagramElement
  {
    
    private Strategy _diagramStrategy;
    
    private DiagramSocket _diagramSocket;
    
    private readonly DiagramElementParam<bool> _bDiagramElementParam;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.StrategyInputDiagramElement" />.
    /// </summary>
    /// <param name="groupName">The category of the diagram element parameter.</param>
    protected StrategyInputDiagramElement(string groupName)
    {
      this._bDiagramElementParam = this.AddParam<bool>(nameof(-1260196612), false).SetDisplay<DiagramElementParam<bool>>(groupName, LocalizedStrings.ShowStrategySocket, LocalizedStrings.ShowStrategySocket, 50).SetOnValueChangedHandler<bool>(new Action<bool>(this.\u0023\u003DzNstrlWaCxREys6HZvA\u003D\u003D)).SetIsParam<DiagramElementParam<bool>>();
      this.ProcessNullValues = false;
      this.\u0023\u003DzY1l5_p1RqIA5();
    }

    /// <summary>Show strategy socket.</summary>
    public bool ShowStrategySocket
    {
      get
      {
        return this._bDiagramElementParam.Value;
      }
      set
      {
        this._bDiagramElementParam.Value = value;
      }
    }

    /// <inheritdoc />
    protected override void OnStart()
    {
      if (!this.ShowStrategySocket)
        this.OnSubscribe((Strategy) this.Strategy, (DiagramSocketValue) null);
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnStop()
    {
      if (!this.ShowStrategySocket)
        this.OnUnSubscribe((Strategy) this.Strategy, (DiagramSocketValue) null);
      else if (this._diagramStrategy != null)
      {
        this.OnUnSubscribe(this._diagramStrategy, (DiagramSocketValue) null);
        this._diagramStrategy = (Strategy) null;
      }
      base.OnStop();
    }

    /// <summary>The method is called at the subscribing to strategy.</summary>
    /// <param name="strategy">Strategy.</param>
    /// <param name="source">Source value.</param>
    protected abstract void OnSubscribe(Strategy strategy, DiagramSocketValue source);

    /// <summary>
    /// The method is called at the unsubscribing to market data.
    /// </summary>
    /// <param name="strategy">Strategy.</param>
    /// <param name="source">Source value.</param>
    protected abstract void OnUnSubscribe(Strategy strategy, DiagramSocketValue source);

    /// <inheritdoc />
    public override void Load(SettingsStorage storage)
    {
      base.Load(storage);
      this.RemoveSockets(true);
      this.\u0023\u003DzY1l5_p1RqIA5();
    }

    private void \u0023\u003DzY1l5_p1RqIA5()
    {
      if (this._diagramSocket != null)
      {
        this.RemoveSocket(this._diagramSocket);
        this._diagramSocket = (DiagramSocket) null;
      }
      if (!this._bDiagramElementParam.Value)
        return;
      this._diagramSocket = this.AddInput(DiagramElement.GenerateSocketId(nameof(-1260192466)), LocalizedStrings.Strategy, DiagramSocketType.Strategy, new Action<DiagramSocketValue>(this.\u0023\u003DzbEk6WdfnA3Ub), 1, 2, false, new bool?());
    }

    private void \u0023\u003DzbEk6WdfnA3Ub(DiagramSocketValue _param1)
    {
      if (this._diagramStrategy != null)
        this.OnUnSubscribe(this._diagramStrategy, _param1);
      this._diagramStrategy = _param1.GetValue<Strategy>();
      if (this._diagramStrategy == null)
        return;
      this.OnSubscribe(this._diagramStrategy, _param1);
    }

    private void \u0023\u003DzNstrlWaCxREys6HZvA\u003D\u003D(bool _param1)
    {
      this.\u0023\u003DzY1l5_p1RqIA5();
    }
  }
}
