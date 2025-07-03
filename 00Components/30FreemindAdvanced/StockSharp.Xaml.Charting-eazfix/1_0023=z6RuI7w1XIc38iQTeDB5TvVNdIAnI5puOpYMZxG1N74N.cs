// Decompiled with JetBrains decompiler
// Type: #=z6RuI7w1XIc38iQTeDB5TvVNdIAnI5puOpYMZxG1N74NUdS0_TeUB1xau$CDC
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
internal static class \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvVNdIAnI5puOpYMZxG1N74NUdS0_TeUB1xau\u0024CDC
{
  public static bool \u0023\u003DzLkkeFXBPneBY(
    \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4tiESttatY\u0024KE2k2uyGV0g01vOs3xfyzYzSge7gXJQq7CWw\u003D\u003D _param0,
    int[] _param1,
    int _param2,
    int _param3,
    out double _param4,
    out double _param5,
    out double _param6,
    out double _param7)
  {
    double num1 = 0.0;
    double num2 = 0.0;
    bool flag = true;
    _param4 = 1.0;
    _param5 = 1.0;
    _param6 = 0.0;
    _param7 = 0.0;
    for (int index = 0; index < _param3; ++index)
    {
      _param0.\u0023\u003DzVawdK5C5Lyf_(_param1[_param2 + index]);
      \u0023\u003DzXTZ2b\u0024bZtVqaBDtznjklQzroW5BoBmJmtevt_T1hEos_Jrh53_wPS4I\u003D.\u0023\u003Dz9kUnn38\u003D z9kUnn38;
      while (!\u0023\u003DzXTZ2b\u0024bZtVqaBDtznjklQzroW5BoBmJmtevt_T1hEos_Jrh53_wPS4I\u003D.\u0023\u003DzVHztYKNVoUMf(z9kUnn38 = _param0.\u0023\u003DzxfekdAs1X3YM(out num1, out num2)))
      {
        if (\u0023\u003DzXTZ2b\u0024bZtVqaBDtznjklQzroW5BoBmJmtevt_T1hEos_Jrh53_wPS4I\u003D.\u0023\u003DzepfxPD_ghBSfgm\u0024Sfw\u003D\u003D(z9kUnn38))
        {
          if (flag)
          {
            _param4 = num1;
            _param5 = num2;
            _param6 = num1;
            _param7 = num2;
            flag = false;
          }
          else
          {
            if (num1 < _param4)
              _param4 = num1;
            if (num2 < _param5)
              _param5 = num2;
            if (num1 > _param6)
              _param6 = num1;
            if (num2 > _param7)
              _param7 = num2;
          }
        }
      }
    }
    return _param4 <= _param6 && _param5 <= _param7;
  }

  public static bool \u0023\u003DzLkkeFXBPneBY(
    \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4tiESttatY\u0024KE2k2uyGV0g01vOs3xfyzYzSge7gXJQq7CWw\u003D\u003D _param0,
    int[] _param1,
    int _param2,
    int _param3,
    out \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEuHQdGn\u0024BlVr_f8qOhYtPerHvaqyP1QAjNMMJBAJ _param4)
  {
    return \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvVNdIAnI5puOpYMZxG1N74NUdS0_TeUB1xau\u0024CDC.\u0023\u003DzLkkeFXBPneBY(_param0, _param1, _param2, _param3, out _param4.\u0023\u003DzP4R7yU0\u003D, out _param4.\u0023\u003DzRNV_Dpk\u003D, out _param4.\u0023\u003Dzp55dtus\u003D, out _param4.\u0023\u003DzSzOWcj8\u003D);
  }

  public static bool \u0023\u003DzKePJMpXBgY4XmPgRHw\u003D\u003D(
    \u0023\u003Dz3uoqT9PJZU9sx1O75XaUu4Mi0dxanfUdCYT_jJrqlP2iRpVei5Um4fSD89q302QL9g\u003D\u003D _param0,
    int _param1,
    ref \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEuHQdGn\u0024BlVr_f8qOhYtPerHvaqyP1QAjNMMJBAJ _param2)
  {
    double num1;
    double num2;
    double num3;
    double num4;
    int num5 = \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvVNdIAnI5puOpYMZxG1N74NUdS0_TeUB1xau\u0024CDC.\u0023\u003DzKePJMpXBgY4XmPgRHw\u003D\u003D(_param0, _param1, out num1, out num2, out num3, out num4) ? 1 : 0;
    _param2.\u0023\u003DzP4R7yU0\u003D = num1;
    _param2.\u0023\u003DzRNV_Dpk\u003D = num2;
    _param2.\u0023\u003Dzp55dtus\u003D = num3;
    _param2.\u0023\u003DzSzOWcj8\u003D = num4;
    return num5 != 0;
  }

  public static bool \u0023\u003DzKePJMpXBgY4XmPgRHw\u003D\u003D(
    \u0023\u003Dz3uoqT9PJZU9sx1O75XaUu4Mi0dxanfUdCYT_jJrqlP2iRpVei5Um4fSD89q302QL9g\u003D\u003D _param0,
    int _param1,
    out double _param2,
    out double _param3,
    out double _param4,
    out double _param5)
  {
    double num1 = 0.0;
    double num2 = 0.0;
    bool flag = true;
    _param2 = 1.0;
    _param3 = 1.0;
    _param4 = 0.0;
    _param5 = 0.0;
    _param0.\u0023\u003DzVawdK5C5Lyf_(_param1);
    \u0023\u003DzXTZ2b\u0024bZtVqaBDtznjklQzroW5BoBmJmtevt_T1hEos_Jrh53_wPS4I\u003D.\u0023\u003Dz9kUnn38\u003D z9kUnn38;
    while (!\u0023\u003DzXTZ2b\u0024bZtVqaBDtznjklQzroW5BoBmJmtevt_T1hEos_Jrh53_wPS4I\u003D.\u0023\u003DzVHztYKNVoUMf(z9kUnn38 = _param0.\u0023\u003DzxfekdAs1X3YM(out num1, out num2)))
    {
      if (\u0023\u003DzXTZ2b\u0024bZtVqaBDtznjklQzroW5BoBmJmtevt_T1hEos_Jrh53_wPS4I\u003D.\u0023\u003DzepfxPD_ghBSfgm\u0024Sfw\u003D\u003D(z9kUnn38))
      {
        if (flag)
        {
          _param2 = num1;
          _param3 = num2;
          _param4 = num1;
          _param5 = num2;
          flag = false;
        }
        else
        {
          if (num1 < _param2)
            _param2 = num1;
          if (num2 < _param3)
            _param3 = num2;
          if (num1 > _param4)
            _param4 = num1;
          if (num2 > _param5)
            _param5 = num2;
        }
      }
    }
    return _param2 <= _param4 && _param3 <= _param5;
  }
}
