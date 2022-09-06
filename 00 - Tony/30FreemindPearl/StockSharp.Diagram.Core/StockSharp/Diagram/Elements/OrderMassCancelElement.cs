// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OrderMassCancelElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Order mass cancelling element.</summary>
  [Doc("topics/Designer_Mass_Cancellations.html")]
  [DisplayNameLoc("OrderMassCancelling")]
  [CategoryLoc("Str3599")]
  [DescriptionLoc("OrderMassCancelling", true)]
  public class OrderMassCancelElement : OrderBaseDiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260194257).To<Guid>();
    
    private readonly string _iconName = nameof(-1260194234);
    
    private readonly DiagramSocket \u0023\u003DzROizxQxrHZ2G_FvBE_Raa8E\u003D;
    
    private readonly DiagramSocket \u0023\u003DzTal4U24QVng9;
    
    private readonly DiagramSocket \u0023\u003DzzA0hQ0E1W1am;
    
    private readonly DiagramSocket \u0023\u003DzKFCC0qs1i4jW;
    
    private long? \u0023\u003DzhKZoy3Qg0j3B;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OrderMassCancelElement" />.
    /// </summary>
    public OrderMassCancelElement()
    {
      this.\u0023\u003DzROizxQxrHZ2G_FvBE_Raa8E\u003D = this.AddInput(StaticSocketIds.Portfolio, LocalizedStrings.Portfolio, DiagramSocketType.Portfolio, (Action<DiagramSocketValue>) null, 1, int.MaxValue, false, new bool?());
      this.\u0023\u003DzTal4U24QVng9 = this.AddInput(StaticSocketIds.Security, LocalizedStrings.Security, DiagramSocketType.Security, (Action<DiagramSocketValue>) null, 1, int.MaxValue, false, new bool?());
      this.\u0023\u003DzzA0hQ0E1W1am = this.AddInput(StaticSocketIds.Direction, LocalizedStrings.Str129, DiagramSocketType.Side, (Action<DiagramSocketValue>) null, 1, int.MaxValue, false, new bool?());
      this.\u0023\u003DzKFCC0qs1i4jW = this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str1738, DiagramSocketType.Bool, int.MaxValue, int.MaxValue, new bool?());
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
      this.\u0023\u003DzhKZoy3Qg0j3B = new long?();
      DiagramStrategy strategy = this.Strategy;
      ((ITransactionProvider) strategy).MassOrderCanceled2 += new Action<long, DateTimeOffset>(this.\u0023\u003Dz_sqGlfDNJeEyT03cCA\u003D\u003D);
      ((ITransactionProvider) strategy).MassOrderCancelFailed2 += new Action<long, Exception, DateTimeOffset>(this.\u0023\u003DzTfJGV\u0024UgE30TLZfOcA\u003D\u003D);
      base.OnStart();
    }

    /// <inheritdoc />
    protected override void OnStop()
    {
      this.\u0023\u003DzhKZoy3Qg0j3B = new long?();
      DiagramStrategy strategy = this.Strategy;
      ((ITransactionProvider) strategy).MassOrderCanceled2 -= new Action<long, DateTimeOffset>(this.\u0023\u003Dz_sqGlfDNJeEyT03cCA\u003D\u003D);
      ((ITransactionProvider) strategy).MassOrderCancelFailed2 -= new Action<long, Exception, DateTimeOffset>(this.\u0023\u003DzTfJGV\u0024UgE30TLZfOcA\u003D\u003D);
      base.OnStop();
    }

    /// <inheritdoc />
    protected override void OnProcess(
      DateTimeOffset time,
      IDictionary<DiagramSocket, DiagramSocketValue> values,
      DiagramSocketValue source)
    {
      if (!this.CanProcess(values))
        return;
      DiagramSocketValue diagramSocketValue1;
      if (!values.TryGetValue(this.\u0023\u003DzROizxQxrHZ2G_FvBE_Raa8E\u003D, out diagramSocketValue1))
        throw new InvalidOperationException(LocalizedStrings.Str1381);
      DiagramSocketValue diagramSocketValue2;
      if (!values.TryGetValue(this.\u0023\u003DzTal4U24QVng9, out diagramSocketValue2))
        throw new InvalidOperationException(LocalizedStrings.Str1380);
      DiagramSocketValue diagramSocketValue3;
      if (!values.TryGetValue(this.\u0023\u003DzzA0hQ0E1W1am, out diagramSocketValue3))
        throw new InvalidOperationException(LocalizedStrings.Str1713);
      ITransactionProvider strategy = (ITransactionProvider) this.Strategy;
      ITransactionProvider transactionProvider = strategy;
      Portfolio portfolio1 = diagramSocketValue1.GetValue<Portfolio>();
      Sides? nullable1 = diagramSocketValue3.GetValue<Sides?>();
      Security security1 = diagramSocketValue2.GetValue<Security>();
      long? nullable2 = this.\u0023\u003DzhKZoy3Qg0j3B = new long?(strategy.TransactionIdGenerator.GetNextId());
      bool? isStopOrder = new bool?();
      Portfolio portfolio2 = portfolio1;
      Sides? direction = nullable1;
      Security security2 = security1;
      SecurityTypes? securityType = new SecurityTypes?();
      long? transactionId = nullable2;
      transactionProvider.CancelOrders(isStopOrder, portfolio2, direction, (ExchangeBoard) null, security2, securityType, transactionId);
    }

    private void \u0023\u003DzTfJGV\u0024UgE30TLZfOcA\u003D\u003D(
      long _param1,
      Exception _param2,
      DateTimeOffset _param3)
    {
      this.\u0023\u003DzrHcUZ\u0024\u00245fIcN(_param1, false, _param3);
    }

    private void \u0023\u003Dz_sqGlfDNJeEyT03cCA\u003D\u003D(long _param1, DateTimeOffset _param2)
    {
      this.\u0023\u003DzrHcUZ\u0024\u00245fIcN(_param1, true, _param2);
    }

    private void \u0023\u003DzrHcUZ\u0024\u00245fIcN(
      long _param1,
      bool _param2,
      DateTimeOffset _param3)
    {
      long num = _param1;
      long? zhKzoy3Qg0j3B = this.\u0023\u003DzhKZoy3Qg0j3B;
      long valueOrDefault = zhKzoy3Qg0j3B.GetValueOrDefault();
      if (!(num == valueOrDefault & zhKzoy3Qg0j3B.HasValue))
        return;
      this.RaiseProcessOutput(this.\u0023\u003DzKFCC0qs1i4jW, _param3, (object) _param2, (DiagramSocketValue) null, (Subscription) null);
      this.\u0023\u003DzhKZoy3Qg0j3B = new long?();
      this.Strategy.Flush();
    }
  }
}
