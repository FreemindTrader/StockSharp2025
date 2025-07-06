// Decompiled with JetBrains decompiler
// Type: #=zUqahKaP3EIK$L1yMVA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable enable
internal sealed class \u0023\u003DzUqahKaP3EIK\u0024L1yMVA\u003D\u003D<T> : 
  IList<
  #nullable disable
  T>,
  ICollection<T>,
  IEnumerable<T>,
  IList,
  IEnumerable,
  ICollection,
  IReadOnlyCollection<T>,
  IReadOnlyList<T>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly List<T> \u0023\u003Dzg0gWX4E\u003D;

  public \u0023\u003DzUqahKaP3EIK\u0024L1yMVA\u003D\u003D(List<T> _param1)
  {
    this.\u0023\u003Dzg0gWX4E\u003D = _param1;
  }

  #nullable enable
  IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
  {
    return ((IEnumerable) this.\u0023\u003Dzg0gWX4E\u003D).GetEnumerator();
  }

  int ICollection.\u0023\u003Dzl6H\u0024MU\u0024Y9eNBxHLmAI_uHK8\u003D()
  {
    return this.\u0023\u003Dzg0gWX4E\u003D.Count;
  }

  bool ICollection.\u0023\u003DzU\u0024MaLspaVIlPZAquKstGPb4\u003D() => false;

  object ICollection.\u0023\u003DzxSE4OX0h3nsK6ev9wi6AL7I\u003D() => (object) this;

  void ICollection.\u0023\u003DzFAvqYrLd8QaQMLuvPOiGwFQ\u003D(Array _param1, int _param2)
  {
    ((ICollection) this.\u0023\u003Dzg0gWX4E\u003D).Clone(_param1, _param2);
  }

  object? IList.\u0023\u003Dzsw6uZQAY38X4SXUVUM6sxbU\u003D(int _param1)
  {
    return (object) this.\u0023\u003Dzg0gWX4E\u003D[_param1];
  }

  void IList.\u0023\u003DzPS8zWbReapv0MVOGfNSMdFU\u003D(int _param1, object? _param2)
  {
    throw new NotSupportedException();
  }

  bool IList.\u0023\u003DzVtB9Td1HClEhKFH\u0024dUP8Pw0\u003D() => true;

  bool IList.\u0023\u003DzTy9YbO8PCIjJtzE8Uz_BYx8\u003D() => true;

  int IList.\u0023\u003Dz9l8NFg1zgw0y\u0024EjyXw\u003D\u003D(object? _param1)
  {
    throw new NotSupportedException();
  }

  void IList.\u0023\u003DznLxu\u0024P7HzqmLFvcaeQ\u003D\u003D()
  {
    throw new NotSupportedException();
  }

  bool IList.\u0023\u003Dz07_U1xKJVCxa7bIf\u0024A\u003D\u003D(object? _param1)
  {
    return ((IList) this.\u0023\u003Dzg0gWX4E\u003D).Contains(_param1);
  }

  int IList.\u0023\u003DzRqsurumTDWAgVqHVtg\u003D\u003D(object? _param1)
  {
    return ((IList) this.\u0023\u003Dzg0gWX4E\u003D).IndexOf(_param1);
  }

  void IList.\u0023\u003DzuxPIbwR6tOrj8Wpe9w\u003D\u003D(int _param1, object? _param2)
  {
    throw new NotSupportedException();
  }

  void IList.\u0023\u003Dzx\u0024PvHCNWSNwq55LIBQ\u003D\u003D(object? _param1)
  {
    throw new NotSupportedException();
  }

  void IList.\u0023\u003DzatuMnTN2qfDvxicnPfohBOc\u003D(int _param1)
  {
    throw new NotSupportedException();
  }

  IEnumerator<
  #nullable disable
  T> IEnumerable<T>.\u0023\u003DzeZA2Ff9OcH6BD7PGVCI6viR78aQH()
  {
    return ((IEnumerable<T>) this.\u0023\u003Dzg0gWX4E\u003D).GetEnumerator();
  }

  int IReadOnlyCollection<T>.\u0023\u003DzgFtIrs4ENSc_WWxv3LSeKDD\u0024hCiHOapNIw\u003D\u003D()
  {
    return this.\u0023\u003Dzg0gWX4E\u003D.Count;
  }

  T IReadOnlyList<T>.\u0023\u003DzE6RKfhg7MZKfCZg03PVrQHF47QBOymO5IQ\u003D\u003D(
    int _param1)
  {
    return this.\u0023\u003Dzg0gWX4E\u003D[_param1];
  }

  int ICollection<T>.\u0023\u003DzYUole4\u0024EZuetsIl9ck_WMrZyYkc9()
  {
    return this.\u0023\u003Dzg0gWX4E\u003D.Count;
  }

  bool ICollection<T>.\u0023\u003DzUq2KM6jl3UCUrxQEdcWSy1EsGjzi\u0024m061w\u003D\u003D()
  {
    return true;
  }

  void ICollection<T>.\u0023\u003DzGBPAZl7kD4Y_019f_JdDMabqLLf_(
    T _param1)
  {
    throw new NotSupportedException();
  }

  void ICollection<T>.\u0023\u003DzDHU7m5zeb3RDfJiTgdiSGk4T_lc2()
  {
    throw new NotSupportedException();
  }

  bool ICollection<T>.\u0023\u003Dz\u0024bxRf6j_TZkMx5OhZQOQ5LXVqjFT(
    T _param1)
  {
    return this.\u0023\u003Dzg0gWX4E\u003D.Contains(_param1);
  }

  void ICollection<T>.\u0023\u003DzB1ZS\u002489m\u0024lCkmdGveUer9MPNc9vT(
    #nullable enable
    #nullable disable
    T[] _param1,
    int _param2)
  {
    this.\u0023\u003Dzg0gWX4E\u003D.Clone(_param1, _param2);
  }

  bool ICollection<T>.\u0023\u003DzSNMbA4NBIHO6wT8LM2lfHp2KTKyC(
    T _param1)
  {
    throw new NotSupportedException();
  }

  T IList<T>.\u0023\u003Dz0AHuzVTfq\u0024d4zWGWl4gMSpIlkWpf(
    int _param1)
  {
    return this.\u0023\u003Dzg0gWX4E\u003D[_param1];
  }

  void IList<T>.\u0023\u003Dz_vb79EtlArV3ZdNVeByl4tQWV3O8(
    int _param1,
    T _param2)
  {
    throw new NotSupportedException();
  }

  int IList<T>.\u0023\u003DzQSvgbNgfbIbolYFpnQTahdZzoVuR(
    T _param1)
  {
    return this.\u0023\u003Dzg0gWX4E\u003D.IndexOf(_param1);
  }

  void IList<T>.\u0023\u003DzZ4BzDdC48hLOGQW0MXBt3l3kXadY(
    int _param1,
    T _param2)
  {
    throw new NotSupportedException();
  }

  void IList<T>.\u0023\u003DzO_7_iKfNhQVcisMnfax5R9QC0XjJ(int _param1)
  {
    throw new NotSupportedException();
  }
}
