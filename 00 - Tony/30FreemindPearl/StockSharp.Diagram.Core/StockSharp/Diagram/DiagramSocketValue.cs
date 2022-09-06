// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.DiagramSocketValue
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram
{
  /// <summary>The value for the connection.</summary>
  public class DiagramSocketValue
  {
    
    private readonly DiagramSocket _sender;
    
    private readonly DiagramSocket _socket;
    
    private readonly DateTimeOffset _time;
    
    private readonly Subscription _subscription;
    
    private readonly object _value;
    /// <summary>
    /// </summary>
    public System.Collections.Generic.Stack<DiagramSocketValue> Stack;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.DiagramSocketValue" />.
    /// </summary>
    /// <param name="socket">Connection.</param>
    /// <param name="time">Time.</param>
    /// <param name="value">Value.</param>
    /// <param name="sender">The element sender of the value.</param>
    /// <param name="subscription">Subscription.</param>
    public DiagramSocketValue(
      DiagramSocket socket,
      DateTimeOffset time,
      object value,
      DiagramSocket sender,
      Subscription subscription)
    {
      DiagramSocket diagramSocket = socket;
      if (diagramSocket == null)
        throw new ArgumentNullException(nameof(-1260196559));
      this._socket = diagramSocket;
      this._time = time;
      this._value = value;
      this._sender = sender;
      this._subscription = subscription;
    }

    /// <summary>The element sender of the value.</summary>
    public DiagramSocket Sender
    {
      get
      {
        return this._sender;
      }
    }

    /// <summary>Connection.</summary>
    public DiagramSocket Socket
    {
      get
      {
        return this._socket;
      }
    }

    /// <summary>Time.</summary>
    public DateTimeOffset Time
    {
      get
      {
        return this._time;
      }
    }

    /// <summary>Subscription.</summary>
    public Subscription Subscription
    {
      get
      {
        return this._subscription;
      }
    }

    /// <summary>Value.</summary>
    public object Value
    {
      get
      {
        return this._value;
      }
    }

    /// <summary>To get the value for the connection.</summary>
    /// <typeparam name="T">Value type.</typeparam>
    /// <returns>Value.</returns>
    public T GetValue<T>()
    {
      return this.\u0023\u003DzIAIXri4\u003D(typeof (T)).To<T>();
    }

    private object \u0023\u003DzIAIXri4\u003D(Type _param1)
    {
      if (this.Value == null)
        throw new InvalidOperationException(LocalizedStrings.SocketNoValue);
      Type type = ((object) this.Value).GetType();
      if (_param1.IsAssignableFrom(type))
        return this.Value;
      if (_param1 == typeof (Decimal))
        return (object) DiagramSocketValue.\u0023\u003Dzt1\u00241Pa0\u003D(type, this.Value);
      if (!(_param1 == typeof (Unit)))
        return this.Value;
      string str = (string) (this.Value as string);
      if (str != null)
        return (object) str.ToUnit(false, (Func<UnitTypes, Decimal?>) null);
      return (object) (Unit) DiagramSocketValue.\u0023\u003Dzt1\u00241Pa0\u003D(type, this.Value);
    }

    private static Decimal \u0023\u003Dzt1\u00241Pa0\u003D(Type _param0, object _param1)
    {
      if (_param0.IsNumeric())
        return _param1.To<Decimal>();
      Unit unit = _param1 as Unit;
      if ((object) unit != null)
        return unit.To<Decimal>();
      string str = (string) (_param1 as string);
      if (str != null)
        return str.To<Decimal>();
      IIndicatorValue indicatorValue = _param1 as IIndicatorValue;
      if (indicatorValue != null)
        return indicatorValue.GetValue<Decimal>();
      if (_param1 is QuoteChange)
        return ((QuoteChange) _param1).Price;
      Quote quote = _param1 as Quote;
      if (quote != null)
        return quote.Price;
      Candle candle = _param1 as Candle;
      if (candle != null)
        return candle.ClosePrice;
      if (_param1 is DateTime)
        return (Decimal) ((DateTime) _param1).ToUniversalTime().Ticks;
      if (_param1 is DateTimeOffset)
        return (Decimal) ((DateTimeOffset) _param1).UtcTicks;
      if (_param1 is TimeSpan)
        return (Decimal) ((TimeSpan) _param1).Ticks;
      if (_param1 is CandleStates)
        return (Decimal) ((int) _param1);
      if (_param1 is OrderStates)
        return (Decimal) ((int) _param1);
      throw new ArgumentException(string.Format(nameof(-1260196604), _param1));
    }

    /// <inheritdoc />
    public override string ToString()
    {
      if (this.Sender == null)
        return nameof(-1260199384).Put((object[]) new object[2]{ (object) this.Socket, (object) this.Value });
      return nameof(-1260196354).Put((object[]) new object[3]{ (object) this.Sender, (object) this.Socket, (object) this.Value });
    }
  }
}
