// Decompiled with JetBrains decompiler
// Type: #=zAtYWtSRxk8WC$EcJQ7b1L7aLdD0UYentUKC7Ct5IeKJc9rSTNMo$$niOEu7tzTkpfi3UFcQoi7feZ0sMWA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7aLdD0UYentUKC7Ct5IeKJc9rSTNMo\u0024\u0024niOEu7tzTkpfi3UFcQoi7feZ0sMWA\u003D\u003D
{
  public void \u0023\u003DzSJ0vPIg\u003D(
    \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv3O1vVUi3Pf5LXL8sd0rJM6z2sXOfdVyBoG70yPzUmVGknO_tSQ\u003D _param1)
  {
    this.\u0023\u003DzSJ0vPIg\u003D(_param1, 0, _param1.\u0023\u003DzG2qqjnQ\u003D() - 1);
  }

  public void \u0023\u003DzSJ0vPIg\u003D(
    \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv3O1vVUi3Pf5LXL8sd0rJM6z2sXOfdVyBoG70yPzUmVGknO_tSQ\u003D _param1,
    int _param2,
    int _param3)
  {
    if (_param3 == _param2)
      return;
    int num = this.\u0023\u003Dzze5H2wKP8u1b(_param1, _param2, _param3);
    if (num > _param2)
      this.\u0023\u003DzSJ0vPIg\u003D(_param1, _param2, num - 1);
    if (num >= _param3)
      return;
    this.\u0023\u003DzSJ0vPIg\u003D(_param1, num + 1, _param3);
  }

  private int \u0023\u003Dzze5H2wKP8u1b(
    \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv3O1vVUi3Pf5LXL8sd0rJM6z2sXOfdVyBoG70yPzUmVGknO_tSQ\u003D _param1,
    int _param2,
    int _param3)
  {
    int num1 = _param2;
    int num2 = _param2 + 1;
    int num3 = _param3;
    while (num2 < _param3 && _param1.\u0023\u003Dz\u0024CeUvME\u003D(num1) >= _param1.\u0023\u003Dz\u0024CeUvME\u003D(num2))
      ++num2;
    while (num3 > _param2 && _param1.\u0023\u003Dz\u0024CeUvME\u003D(num1) <= _param1.\u0023\u003Dz\u0024CeUvME\u003D(num3))
      --num3;
label_10:
    while (num2 < num3)
    {
      int num4 = _param1.\u0023\u003Dz\u0024CeUvME\u003D(num2);
      _param1.\u0023\u003DzS9gpfR4\u003D(num2, _param1.\u0023\u003Dz\u0024CeUvME\u003D(num3));
      _param1.\u0023\u003DzS9gpfR4\u003D(num3, num4);
      while (num2 < _param3 && _param1.\u0023\u003Dz\u0024CeUvME\u003D(num1) >= _param1.\u0023\u003Dz\u0024CeUvME\u003D(num2))
        ++num2;
      while (true)
      {
        if (num3 > _param2 && _param1.\u0023\u003Dz\u0024CeUvME\u003D(num1) <= _param1.\u0023\u003Dz\u0024CeUvME\u003D(num3))
          --num3;
        else
          goto label_10;
      }
    }
    if (num1 != num3)
    {
      int num5 = _param1.\u0023\u003Dz\u0024CeUvME\u003D(num3);
      _param1.\u0023\u003DzS9gpfR4\u003D(num3, _param1.\u0023\u003Dz\u0024CeUvME\u003D(num1));
      _param1.\u0023\u003DzS9gpfR4\u003D(num1, num5);
    }
    return num3;
  }
}
