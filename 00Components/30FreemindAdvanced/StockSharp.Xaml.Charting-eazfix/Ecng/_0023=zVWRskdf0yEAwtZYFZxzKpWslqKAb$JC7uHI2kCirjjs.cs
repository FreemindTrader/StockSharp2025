// Decompiled with JetBrains decompiler
// Type: #=zVWRskdf0yEAwtZYFZxzKpWslqKAb$JC7uHI2kCirjjsD
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public abstract class \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD
{
  private bool \u0023\u003DzW8xTrvhMhhoJiSvCyQ\u003D\u003D;
  private bool \u0023\u003Dzfng7Rt8gvrt\u0024\u0024cPAXg\u003D\u003D;
  private IReceiveMouseEvents  \u0023\u003DzyrR0fzsoBnc_6n4gMw\u003D\u003D;

  protected \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD()
  {
  }

  protected \u0023\u003DzVWRskdf0yEAwtZYFZxzKpWslqKAb\u0024JC7uHI2kCirjjsD(
    IReceiveMouseEvents  _param1,
    bool _param2)
  {
    this.\u0023\u003DzL_zaDEc\u003D(_param1);
    this.\u0023\u003Dz3P2dgUJ\u0024csBH(_param2);
  }

  public bool IsMaster() => this.\u0023\u003DzW8xTrvhMhhoJiSvCyQ\u003D\u003D;

  public void \u0023\u003Dz3P2dgUJ\u0024csBH(bool _param1)
  {
    this.\u0023\u003DzW8xTrvhMhhoJiSvCyQ\u003D\u003D = _param1;
  }

  public bool \u0023\u003Dz882B0y3Ue8fl()
  {
    return this.\u0023\u003Dzfng7Rt8gvrt\u0024\u0024cPAXg\u003D\u003D;
  }

  public void Handled(bool _param1)
  {
    this.\u0023\u003Dzfng7Rt8gvrt\u0024\u0024cPAXg\u003D\u003D = _param1;
  }

  public IReceiveMouseEvents  \u0023\u003DzRo7rSFU\u003D()
  {
    return this.\u0023\u003DzyrR0fzsoBnc_6n4gMw\u003D\u003D;
  }

  public void \u0023\u003DzL_zaDEc\u003D(
    IReceiveMouseEvents  _param1)
  {
    this.\u0023\u003DzyrR0fzsoBnc_6n4gMw\u003D\u003D = _param1;
  }
}
