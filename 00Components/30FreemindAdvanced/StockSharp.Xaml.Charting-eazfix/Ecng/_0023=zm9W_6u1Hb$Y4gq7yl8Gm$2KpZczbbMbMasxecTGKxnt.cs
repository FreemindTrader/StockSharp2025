// Decompiled with JetBrains decompiler
// Type: #=zm9W_6u1Hb$Y4gq7yl8Gm$2KpZczbbMbMasxecTGKxnt5bT2l7s_zjbEU3l3ty8PLFOQY2AYvJuIy2kooCOncjwSFLeruKsraew==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
internal sealed class \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00242KpZczbbMbMasxecTGKxnt5bT2l7s_zjbEU3l3ty8PLFOQY2AYvJuIy2kooCOncjwSFLeruKsraew\u003D\u003D : 
  \u0023\u003DzQN2Zes8h9tElvYmX48o49LUqCmhVH6715qLutxVwykbkhQQD\u0024H5YaimuStgFrV95Cs0brS0\u003D
{
  private int \u0023\u003Dz_TCtD0vLATv8;
  private int \u0023\u003DzFfSb8y0\u003D;
  private byte[] \u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D;
  private int \u0023\u003DzOWGC0ICdmJEADlDalg\u003D\u003D;
  private \u0023\u003Dza9eQbgAsftIGbI_4wdfcZFY6vYc_HYGEdLlgutNQnK5Pj3kj4v\u0024AHgO299qs2MDumslM5o0\u003D[] \u0023\u003DzDfv8Db0RWqZH;
  private int \u0023\u003DzJZMDU99kHHNc;
  private int \u0023\u003DzlYYZvacF9UC9Kl5xTSuYyyE\u003D;

  public \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00242KpZczbbMbMasxecTGKxnt5bT2l7s_zjbEU3l3ty8PLFOQY2AYvJuIy2kooCOncjwSFLeruKsraew\u003D\u003D()
  {
    this.\u0023\u003Dz_TCtD0vLATv8 = 2147483632;
    this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D = new byte[1000];
    this.\u0023\u003DzDfv8Db0RWqZH = new \u0023\u003Dza9eQbgAsftIGbI_4wdfcZFY6vYc_HYGEdLlgutNQnK5Pj3kj4v\u0024AHgO299qs2MDumslM5o0\u003D[1000];
  }

  public \u0023\u003Dza9eQbgAsftIGbI_4wdfcZFY6vYc_HYGEdLlgutNQnK5Pj3kj4v\u0024AHgO299qs2MDumslM5o0\u003D \u0023\u003DzYkrxbPUh8YlkHw5nkgeSuH8\u003D()
  {
    ++this.\u0023\u003DzlYYZvacF9UC9Kl5xTSuYyyE\u003D;
    return this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzlYYZvacF9UC9Kl5xTSuYyyE\u003D - 1];
  }

  public void \u0023\u003Dzp_DWHgc\u003D(int _param1, int _param2)
  {
    int length = _param2 - _param1 + 3;
    if (length > this.\u0023\u003DzDfv8Db0RWqZH.Length)
    {
      this.\u0023\u003DzDfv8Db0RWqZH = new \u0023\u003Dza9eQbgAsftIGbI_4wdfcZFY6vYc_HYGEdLlgutNQnK5Pj3kj4v\u0024AHgO299qs2MDumslM5o0\u003D[length];
      this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D = new byte[length];
    }
    this.\u0023\u003Dz_TCtD0vLATv8 = 2147483632;
    this.\u0023\u003DzOWGC0ICdmJEADlDalg\u003D\u003D = 0;
    this.\u0023\u003DzJZMDU99kHHNc = 0;
    this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D = 0;
  }

  public void \u0023\u003Dzzb9PhvYi_sP8(int _param1, int _param2)
  {
    this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[this.\u0023\u003DzOWGC0ICdmJEADlDalg\u003D\u003D] = (byte) _param2;
    if (_param1 == this.\u0023\u003Dz_TCtD0vLATv8 + 1 && this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D > 0)
    {
      ++this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D;
    }
    else
    {
      ++this.\u0023\u003DzJZMDU99kHHNc;
      this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003DzSP2vcyDTa4xx = this.\u0023\u003DzOWGC0ICdmJEADlDalg\u003D\u003D;
      this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003DzwP120vA\u003D = (int) (short) _param1;
      this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D = 1;
    }
    this.\u0023\u003Dz_TCtD0vLATv8 = _param1;
    ++this.\u0023\u003DzOWGC0ICdmJEADlDalg\u003D\u003D;
  }

  public void \u0023\u003DzwKUVH04G6uXX(int _param1, int _param2, byte[] _param3, int _param4)
  {
    for (int index = 0; index < _param2; ++index)
      this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[this.\u0023\u003DzOWGC0ICdmJEADlDalg\u003D\u003D + index] = _param3[index];
    if (_param1 == this.\u0023\u003Dz_TCtD0vLATv8 + 1 && this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D > 0)
    {
      this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D += (int) (short) _param2;
    }
    else
    {
      ++this.\u0023\u003DzJZMDU99kHHNc;
      this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003DzSP2vcyDTa4xx = this.\u0023\u003DzOWGC0ICdmJEADlDalg\u003D\u003D;
      this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003DzwP120vA\u003D = (int) (short) _param1;
      this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D = (int) (short) _param2;
    }
    this.\u0023\u003DzOWGC0ICdmJEADlDalg\u003D\u003D += _param2;
    this.\u0023\u003Dz_TCtD0vLATv8 = _param1 + _param2 - 1;
  }

  public void \u0023\u003DzqJOc77USXqlC(int _param1, int _param2, int _param3)
  {
    if (_param1 == this.\u0023\u003Dz_TCtD0vLATv8 + 1 && this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D < 0 && _param3 == this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003DzSP2vcyDTa4xx)
    {
      this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D -= (int) (short) _param2;
    }
    else
    {
      this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D[this.\u0023\u003DzOWGC0ICdmJEADlDalg\u003D\u003D] = (byte) _param3;
      ++this.\u0023\u003DzJZMDU99kHHNc;
      this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003DzSP2vcyDTa4xx = this.\u0023\u003DzOWGC0ICdmJEADlDalg\u003D\u003D++;
      this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003DzwP120vA\u003D = (int) (short) _param1;
      this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D = (int) (short) -_param2;
    }
    this.\u0023\u003Dz_TCtD0vLATv8 = _param1 + _param2 - 1;
  }

  public void \u0023\u003DzDoZMoNygQSg8PGaYqw\u003D\u003D(int _param1)
  {
    this.\u0023\u003DzFfSb8y0\u003D = _param1;
  }

  public void \u0023\u003Dz_qg8im_\u0024N_mX()
  {
    this.\u0023\u003Dz_TCtD0vLATv8 = 2147483632;
    this.\u0023\u003DzOWGC0ICdmJEADlDalg\u003D\u003D = 0;
    this.\u0023\u003DzJZMDU99kHHNc = 0;
    this.\u0023\u003DzDfv8Db0RWqZH[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D = 0;
  }

  public int \u0023\u003Dzi8jDI4I\u003D() => this.\u0023\u003DzFfSb8y0\u003D;

  public int \u0023\u003DzAK83aWWCVpNB() => this.\u0023\u003DzJZMDU99kHHNc;

  public \u0023\u003Dza9eQbgAsftIGbI_4wdfcZFY6vYc_HYGEdLlgutNQnK5Pj3kj4v\u0024AHgO299qs2MDumslM5o0\u003D \u0023\u003DzoLjFgpI\u003D()
  {
    this.\u0023\u003DzlYYZvacF9UC9Kl5xTSuYyyE\u003D = 1;
    return this.\u0023\u003DzYkrxbPUh8YlkHw5nkgeSuH8\u003D();
  }

  public byte[] \u0023\u003Dz9PP6ydFYlNHt() => this.\u0023\u003Dzsr2UJaJdwnjcHzBzMw\u003D\u003D;
}
