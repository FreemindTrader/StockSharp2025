// Decompiled with JetBrains decompiler
// Type: #=z5VLaAZX2bctAcuSoajSAXhczba5zYMYvDVCey2E5itTCWQDIVo182ob46FA5I1aZ6JCZOQg=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;

#nullable disable
internal sealed class \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhczba5zYMYvDVCey2E5itTCWQDIVo182ob46FA5I1aZ6JCZOQg\u003D : 
  \u0023\u003DzRxKCQfwuO1Ym7C1efUUjvwsh_Yip\u00243_SfLJCROuCo7J59ZG9moAjHGXdMI6r60c7Fw\u003D\u003D
{
  private readonly double \u0023\u003DzWAnvsEc\u003D;
  private readonly double \u0023\u003DzjV9cs\u0024I\u003D;
  private readonly double \u0023\u003DzUKOfIBTUfhak;

  public \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhczba5zYMYvDVCey2E5itTCWQDIVo182ob46FA5I1aZ6JCZOQg\u003D(
    double _param1,
    double _param2,
    bool _param3,
    bool _param4,
    bool _param5)
  {
    this.\u0023\u003DzWAnvsEc\u003D = _param1;
    this.\u0023\u003DzjV9cs\u0024I\u003D = _param2;
    this.\u0023\u003DzUKOfIBTUfhak = (this.\u0023\u003DzjV9cs\u0024I\u003D - this.\u0023\u003DzWAnvsEc\u003D) / 360.0;
    this.\u0023\u003DzeuXgfasUDyUfGmCF\u0024EtXjOjpTjP2(_param3);
    this.\u0023\u003Dz83sA3hbUFtmcF0NtN3F3NVYGbngE(_param4);
    this.\u0023\u003DziaxW5h6Fhau4h9lgdx67D0k\u003D(_param5);
  }

  public override double \u0023\u003DzhL6gsJw\u003D(DateTime _param1)
  {
    return this.\u0023\u003DzhL6gsJw\u003D((double) _param1.Ticks);
  }

  public override double \u0023\u003DzhL6gsJw\u003D(double _param1)
  {
    _param1 = (_param1 - this.\u0023\u003DzWAnvsEc\u003D) / this.\u0023\u003DzUKOfIBTUfhak;
    return \u0023\u003DzRxKCQfwuO1Ym7C1efUUjvwsh_Yip\u00243_SfLJCROuCo7J59ZG9moAjHGXdMI6r60c7Fw\u003D\u003D.\u0023\u003Dzq1wgKfc\u003D(this.\u0023\u003DzTlJPop1Rus3dbxPf\u0024iJeyAQ\u003D(), _param1, 361.0);
  }

  public override double \u0023\u003DzACwLhyc\u003D(double _param1)
  {
    _param1 = \u0023\u003DzRxKCQfwuO1Ym7C1efUUjvwsh_Yip\u00243_SfLJCROuCo7J59ZG9moAjHGXdMI6r60c7Fw\u003D\u003D.\u0023\u003Dzq1wgKfc\u003D(this.\u0023\u003DzTlJPop1Rus3dbxPf\u0024iJeyAQ\u003D(), _param1, 361.0);
    return this.\u0023\u003DzWAnvsEc\u003D + _param1 * this.\u0023\u003DzUKOfIBTUfhak;
  }
}
