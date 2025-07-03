// Decompiled with JetBrains decompiler
// Type: #=za5uC6EI3X0HH3HGpwdgoZmAstoZQw80oWSTVCoASbg8rfhDcyz1DFougHzA13v933QCCVK4=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
internal sealed class \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZmAstoZQw80oWSTVCoASbg8rfhDcyz1DFougHzA13v933QCCVK4\u003D
{
  public void \u0023\u003DzSJ0vPIg\u003D(
    \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb[] _param1)
  {
    this.\u0023\u003DzSJ0vPIg\u003D(_param1, 0, _param1.Length - 1);
  }

  public void \u0023\u003DzSJ0vPIg\u003D(
    \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb[] _param1,
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
    \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb[] _param1,
    int _param2,
    int _param3)
  {
    int index1 = _param2;
    int index2 = _param2 + 1;
    int index3 = _param3;
    while (index2 < _param3 && _param1[index1].\u0023\u003DzwP120vA\u003D >= _param1[index2].\u0023\u003DzwP120vA\u003D)
      ++index2;
    while (index3 > _param2 && _param1[index1].\u0023\u003DzwP120vA\u003D <= _param1[index3].\u0023\u003DzwP120vA\u003D)
      --index3;
label_10:
    while (index2 < index3)
    {
      \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb quLbcdO8tFdwl0Nmb = _param1[index2];
      _param1[index2] = _param1[index3];
      _param1[index3] = quLbcdO8tFdwl0Nmb;
      while (index2 < _param3 && _param1[index1].\u0023\u003DzwP120vA\u003D >= _param1[index2].\u0023\u003DzwP120vA\u003D)
        ++index2;
      while (true)
      {
        if (index3 > _param2 && _param1[index1].\u0023\u003DzwP120vA\u003D <= _param1[index3].\u0023\u003DzwP120vA\u003D)
          --index3;
        else
          goto label_10;
      }
    }
    if (index1 != index3)
    {
      \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb quLbcdO8tFdwl0Nmb = _param1[index3];
      _param1[index3] = _param1[index1];
      _param1[index1] = quLbcdO8tFdwl0Nmb;
    }
    return index3;
  }
}
