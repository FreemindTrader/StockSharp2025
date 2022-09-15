// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.OperatorRegistry
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Collections;
using Ecng.Common;
using System;

namespace Ecng.ComponentModel
{
  public static class OperatorRegistry
  {
    private static readonly SynchronizedDictionary<Type, IOperator> _operators = new SynchronizedDictionary<Type, IOperator>();

    static OperatorRegistry()
    {
      OperatorRegistry.AddOperator<byte>((IOperator<byte>) new ByteOperator());
      OperatorRegistry.AddOperator<short>((IOperator<short>) new ShortOperator());
      OperatorRegistry.AddOperator<int>((IOperator<int>) new IntOperator());
      OperatorRegistry.AddOperator<long>((IOperator<long>) new LongOperator());
      OperatorRegistry.AddOperator<float>((IOperator<float>) new FloatOperator());
      OperatorRegistry.AddOperator<double>((IOperator<double>) new DoubleOperator());
      OperatorRegistry.AddOperator<Decimal>((IOperator<Decimal>) new DecimalOperator());
      OperatorRegistry.AddOperator<TimeSpan>((IOperator<TimeSpan>) new TimeSpanOperator());
      OperatorRegistry.AddOperator<DateTime>((IOperator<DateTime>) new DateTimeOperator());
      OperatorRegistry.AddOperator<DateTimeOffset>((IOperator<DateTimeOffset>) new DateTimeOffsetOperator());
      OperatorRegistry.AddOperator<sbyte>((IOperator<sbyte>) new SByteOperator());
      OperatorRegistry.AddOperator<ushort>((IOperator<ushort>) new UShortOperator());
      OperatorRegistry.AddOperator<uint>((IOperator<uint>) new UIntOperator());
      OperatorRegistry.AddOperator<ulong>((IOperator<ulong>) new ULongOperator());
    }

    public static void AddOperator<T>(IOperator<T> @operator)
    {
      if (@operator == null)
        throw new ArgumentNullException(nameof (@operator));
      OperatorRegistry._operators.Add(typeof (T), (IOperator) @operator);
    }

    public static IOperator GetOperator(this Type type)
    {
      IOperator instance;
      if (!OperatorRegistry._operators.TryGetValue(type, out instance))
      {
        if (!typeof (IOperable<>).Make(type).IsAssignableFrom(type))
          throw new InvalidOperationException(string.Format("Operator for type {0} doesn't exist.", (object) type));
        instance = typeof (OperatorRegistry.OperableOperator<>).Make(type).CreateInstance<IOperator>();
        OperatorRegistry._operators.Add(type, instance);
      }
      return instance;
    }

    public static IOperator<T> GetOperator<T>()
    {
      return (IOperator<T>) typeof (T).GetOperator();
    }

    public static bool IsRegistered<T>()
    {
      return OperatorRegistry.IsRegistered(typeof (T));
    }

    public static bool IsRegistered(Type type)
    {
      return OperatorRegistry._operators.ContainsKey(type);
    }

    public static void RemoveOperator<T>(IOperator<T> @operator)
    {
      if (@operator == null)
        throw new ArgumentNullException(nameof (@operator));
      OperatorRegistry._operators.Remove(typeof (T));
    }

    public static long? ThrowIfNegative(this long? value, string name)
    {
      if (!value.HasValue)
        return new long?();
      return new long?(value.Value.ThrowIfNegative(name));
    }

    public static long ThrowIfNegative(this long value, string name)
    {
      if (value < 0L)
        throw new ArgumentOutOfRangeException(name, (object) value, "Invalid value.");
      return value;
    }

    private sealed class OperableOperator<T> : BaseOperator<T> where T : IOperable<T>
    {
      public override int Compare(T x, T y)
      {
        return x.CompareTo(y);
      }

      public override T Add(T first, T second)
      {
        return first.Add(second);
      }

      public override T Subtract(T first, T second)
      {
        return first.Subtract(second);
      }

      public override T Multiply(T first, T second)
      {
        return first.Multiply(second);
      }

      public override T Divide(T first, T second)
      {
        return first.Divide(second);
      }
    }
  }
}
