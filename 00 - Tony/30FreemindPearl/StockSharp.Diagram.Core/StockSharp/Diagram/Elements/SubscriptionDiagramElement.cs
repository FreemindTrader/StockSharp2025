// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.SubscriptionDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// The diagram element which supports subscription to market data.
  /// </summary>
  public abstract class SubscriptionDiagramElement : DiagramElement
  {
    
    private Subscription \u0023\u003DzVYrji2E\u003D;
    
    private DiagramSocket \u0023\u003Dz9kGyo7_JjDhJ;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzUduL4etquQhH;
    
    private Security \u0023\u003Dz\u0024OFuvMkEA5Zgta\u0024Hlw\u003D\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.SubscriptionDiagramElement" />.
    /// </summary>
    protected SubscriptionDiagramElement()
    {
      this.AddInput(StaticSocketIds.Security, LocalizedStrings.Security, DiagramSocketType.Security, new Action<DiagramSocketValue>(this.\u0023\u003DzECIEXk\u0024A5Mms), 1, int.MaxValue, false, new bool?());
      this.\u0023\u003DzUduL4etquQhH = this.AddParam<bool>(nameof(-1260192308), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Common, LocalizedStrings.SubscribeOnSignal, LocalizedStrings.SubscribeOnSignal, 50).SetOnValueChangedHandler<bool>(new Action<bool>(this.\u0023\u003DzoJ2_obkV8LapK2wTh2g_qJI\u003D)).SetIsParam<DiagramElementParam<bool>>();
      this.ProcessNullValues = false;
    }

    /// <summary>Market order type.</summary>
    public bool IsManuallySubscription
    {
      get
      {
        return this.\u0023\u003DzUduL4etquQhH.Value;
      }
      set
      {
        this.\u0023\u003DzUduL4etquQhH.Value = value;
      }
    }

    /// <summary>Security.</summary>
    public Security Security
    {
      get
      {
        return this.\u0023\u003Dz\u0024OFuvMkEA5Zgta\u0024Hlw\u003D\u003D;
      }
    }

    private void \u0023\u003Dzfmr_CKXbXLd4(Security _param1)
    {
      this.\u0023\u003Dz\u0024OFuvMkEA5Zgta\u0024Hlw\u003D\u003D = _param1;
    }

    /// <inheritdoc />
    protected override void OnStop()
    {
      this.\u0023\u003DzCcUpQAQ\u003D();
    }

    /// <summary>
    /// The method is called at the subscribing to market data.
    /// </summary>
    /// <returns>Subscription.</returns>
    protected abstract Subscription OnCreateSubscription();

    /// <inheritdoc />
    public override void Load(SettingsStorage storage)
    {
      base.Load(storage);
      if (this.\u0023\u003Dz9kGyo7_JjDhJ == null)
        return;
      this.\u0023\u003Dz_YWjAwfVwGIE();
    }

    private void \u0023\u003DzECIEXk\u0024A5Mms(DiagramSocketValue _param1)
    {
      Security security = _param1.GetValue<Security>();
      if (this.Security == security)
        return;
      if (!this.IsManuallySubscription)
        this.\u0023\u003DzCcUpQAQ\u003D();
      this.\u0023\u003Dzfmr_CKXbXLd4(security);
      if (this.IsManuallySubscription)
        return;
      this.\u0023\u003DzGgg9Fas\u003D();
    }

    private void \u0023\u003DzCTMy1UJzEKMc(DiagramSocketValue _param1)
    {
      if (_param1.GetValue<bool>())
        this.\u0023\u003DzGgg9Fas\u003D();
      else
        this.\u0023\u003DzCcUpQAQ\u003D();
    }

    private void \u0023\u003DzGgg9Fas\u003D()
    {
      if (this.Security == null)
        return;
      this.Strategy.Subscribe(this.\u0023\u003DzVYrji2E\u003D = this.OnCreateSubscription());
    }

    private void \u0023\u003DzCcUpQAQ\u003D()
    {
      if (this.\u0023\u003DzVYrji2E\u003D == null)
        return;
      if (this.\u0023\u003DzVYrji2E\u003D.State.IsActive())
        this.Strategy.UnSubscribe(this.\u0023\u003DzVYrji2E\u003D);
      this.\u0023\u003DzVYrji2E\u003D = (Subscription) null;
      this.\u0023\u003Dzfmr_CKXbXLd4((Security) null);
    }

    private void \u0023\u003Dzi0TtfZ6E9XiW()
    {
      if (this.\u0023\u003Dz9kGyo7_JjDhJ == null)
        return;
      this.RemoveSocket(this.\u0023\u003Dz9kGyo7_JjDhJ);
      this.\u0023\u003Dz9kGyo7_JjDhJ = (DiagramSocket) null;
    }

    private void \u0023\u003Dz_YWjAwfVwGIE()
    {
      this.\u0023\u003Dzi0TtfZ6E9XiW();
      this.\u0023\u003Dz9kGyo7_JjDhJ = this.AddInput(DiagramElement.GenerateSocketId(nameof(-1260192303)), LocalizedStrings.Trigger, DiagramSocketType.Bool, new Action<DiagramSocketValue>(this.\u0023\u003DzCTMy1UJzEKMc), 1, 2, false, new bool?());
    }

    private void \u0023\u003DzoJ2_obkV8LapK2wTh2g_qJI\u003D(bool _param1)
    {
      if (_param1)
        this.\u0023\u003Dz_YWjAwfVwGIE();
      else
        this.\u0023\u003Dzi0TtfZ6E9XiW();
    }
  }
}
