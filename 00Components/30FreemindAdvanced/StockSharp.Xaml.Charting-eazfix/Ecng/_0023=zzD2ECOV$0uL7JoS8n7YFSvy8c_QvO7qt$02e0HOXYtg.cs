// Decompiled with JetBrains decompiler
// Type: #=zzD2ECOV$0uL7JoS8n7YFSvy8c_QvO7qt$02e0HOXYtg5UkjUccmmBVwlVFwpO$EC4O_OLVZeFPIM
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;

#nullable disable
public class \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSvy8c_QvO7qt\u002402e0HOXYtg5UkjUccmmBVwlVFwpO\u0024EC4O_OLVZeFPIM : 
  IVertexSource
{
  private \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSvy8c_QvO7qt\u002402e0HOXYtg5UkjUccmmBVwlVFwpO\u0024EC4O_OLVZeFPIM.\u0023\u003DzafZj3dT3iE_e[] \u0023\u003DzAomBaIYGIk3J = new \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSvy8c_QvO7qt\u002402e0HOXYtg5UkjUccmmBVwlVFwpO\u0024EC4O_OLVZeFPIM.\u0023\u003DzafZj3dT3iE_e[3];
  private double[] \u0023\u003DzI6P8IpE\u003D = new double[8];
  private double[] \u0023\u003DzFfSb8y0\u003D = new double[8];
  private Path.\u0023\u003Dz9kUnn38\u003D[] \u0023\u003DzA25ZMxDrKaxP = new Path.\u0023\u003Dz9kUnn38\u003D[8];
  private int \u0023\u003Dz7y_N6Co2XMR5y5SMXw\u003D\u003D;

  public \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSvy8c_QvO7qt\u002402e0HOXYtg5UkjUccmmBVwlVFwpO\u0024EC4O_OLVZeFPIM()
  {
    this.\u0023\u003Dz7y_N6Co2XMR5y5SMXw\u003D\u003D = 0;
    this.\u0023\u003DzA25ZMxDrKaxP[0] = (Path.\u0023\u003Dz9kUnn38\u003D) 0;
  }

  public \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSvy8c_QvO7qt\u002402e0HOXYtg5UkjUccmmBVwlVFwpO\u0024EC4O_OLVZeFPIM(
    RGBA_Bytes _param1,
    RGBA_Bytes _param2,
    RGBA_Bytes _param3,
    double _param4,
    double _param5,
    double _param6,
    double _param7,
    double _param8,
    double _param9,
    double _param10)
  {
    this.\u0023\u003Dz7y_N6Co2XMR5y5SMXw\u003D\u003D = 0;
    this.\u0023\u003DzP7DRUPjNkOt\u0024((IColorType) _param1, (IColorType) _param2, (IColorType) _param3);
    this.\u0023\u003DzjA9IeU7sH3AGfXsnKA\u003D\u003D(_param4, _param5, _param6, _param7, _param8, _param9, _param10);
  }

  public void \u0023\u003DzP7DRUPjNkOt\u0024(
    IColorType _param1,
    IColorType _param2,
    IColorType _param3)
  {
    this.\u0023\u003DzAomBaIYGIk3J[0].\u0023\u003Dz6NUrJ5Q\u003D = _param1.\u0023\u003DzTBzq3CHoFG5sZ9taiA\u003D\u003D();
    this.\u0023\u003DzAomBaIYGIk3J[1].\u0023\u003Dz6NUrJ5Q\u003D = _param2.\u0023\u003DzTBzq3CHoFG5sZ9taiA\u003D\u003D();
    this.\u0023\u003DzAomBaIYGIk3J[2].\u0023\u003Dz6NUrJ5Q\u003D = _param3.\u0023\u003DzTBzq3CHoFG5sZ9taiA\u003D\u003D();
  }

  public void \u0023\u003DzjA9IeU7sH3AGfXsnKA\u003D\u003D(
    double _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6,
    double _param7)
  {
    this.\u0023\u003DzAomBaIYGIk3J[0].\u0023\u003DzwP120vA\u003D = this.\u0023\u003DzI6P8IpE\u003D[0] = _param1;
    this.\u0023\u003DzAomBaIYGIk3J[0].\u0023\u003Dzi8jDI4I\u003D = this.\u0023\u003DzFfSb8y0\u003D[0] = _param2;
    this.\u0023\u003DzAomBaIYGIk3J[1].\u0023\u003DzwP120vA\u003D = this.\u0023\u003DzI6P8IpE\u003D[1] = _param3;
    this.\u0023\u003DzAomBaIYGIk3J[1].\u0023\u003Dzi8jDI4I\u003D = this.\u0023\u003DzFfSb8y0\u003D[1] = _param4;
    this.\u0023\u003DzAomBaIYGIk3J[2].\u0023\u003DzwP120vA\u003D = this.\u0023\u003DzI6P8IpE\u003D[2] = _param5;
    this.\u0023\u003DzAomBaIYGIk3J[2].\u0023\u003Dzi8jDI4I\u003D = this.\u0023\u003DzFfSb8y0\u003D[2] = _param6;
    this.\u0023\u003DzA25ZMxDrKaxP[0] = (Path.\u0023\u003Dz9kUnn38\u003D) 1;
    this.\u0023\u003DzA25ZMxDrKaxP[1] = (Path.\u0023\u003Dz9kUnn38\u003D) 2;
    this.\u0023\u003DzA25ZMxDrKaxP[2] = (Path.\u0023\u003Dz9kUnn38\u003D) 2;
    this.\u0023\u003DzA25ZMxDrKaxP[3] = (Path.\u0023\u003Dz9kUnn38\u003D) 0;
    if (_param7 == 0.0)
      return;
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcTh97Xh9lnW7va3j9jm7azLp\u0024XO11LbiRMMEHGmBEYuhHw\u003D\u003D.\u0023\u003DzrhSOGwpl0ZrAlqaD7pIXX08\u003D(this.\u0023\u003DzAomBaIYGIk3J[0].\u0023\u003DzwP120vA\u003D, this.\u0023\u003DzAomBaIYGIk3J[0].\u0023\u003Dzi8jDI4I\u003D, this.\u0023\u003DzAomBaIYGIk3J[1].\u0023\u003DzwP120vA\u003D, this.\u0023\u003DzAomBaIYGIk3J[1].\u0023\u003Dzi8jDI4I\u003D, this.\u0023\u003DzAomBaIYGIk3J[2].\u0023\u003DzwP120vA\u003D, this.\u0023\u003DzAomBaIYGIk3J[2].\u0023\u003Dzi8jDI4I\u003D, this.\u0023\u003DzI6P8IpE\u003D, this.\u0023\u003DzFfSb8y0\u003D, _param7);
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcTh97Xh9lnW7va3j9jm7azLp\u0024XO11LbiRMMEHGmBEYuhHw\u003D\u003D.\u0023\u003Dz2OY3nZbjK2oUN9vYsL48jiFCTicz(this.\u0023\u003DzI6P8IpE\u003D[4], this.\u0023\u003DzFfSb8y0\u003D[4], this.\u0023\u003DzI6P8IpE\u003D[5], this.\u0023\u003DzFfSb8y0\u003D[5], this.\u0023\u003DzI6P8IpE\u003D[0], this.\u0023\u003DzFfSb8y0\u003D[0], this.\u0023\u003DzI6P8IpE\u003D[1], this.\u0023\u003DzFfSb8y0\u003D[1], out this.\u0023\u003DzAomBaIYGIk3J[0].\u0023\u003DzwP120vA\u003D, out this.\u0023\u003DzAomBaIYGIk3J[0].\u0023\u003Dzi8jDI4I\u003D);
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcTh97Xh9lnW7va3j9jm7azLp\u0024XO11LbiRMMEHGmBEYuhHw\u003D\u003D.\u0023\u003Dz2OY3nZbjK2oUN9vYsL48jiFCTicz(this.\u0023\u003DzI6P8IpE\u003D[0], this.\u0023\u003DzFfSb8y0\u003D[0], this.\u0023\u003DzI6P8IpE\u003D[1], this.\u0023\u003DzFfSb8y0\u003D[1], this.\u0023\u003DzI6P8IpE\u003D[2], this.\u0023\u003DzFfSb8y0\u003D[2], this.\u0023\u003DzI6P8IpE\u003D[3], this.\u0023\u003DzFfSb8y0\u003D[3], out this.\u0023\u003DzAomBaIYGIk3J[1].\u0023\u003DzwP120vA\u003D, out this.\u0023\u003DzAomBaIYGIk3J[1].\u0023\u003Dzi8jDI4I\u003D);
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcTh97Xh9lnW7va3j9jm7azLp\u0024XO11LbiRMMEHGmBEYuhHw\u003D\u003D.\u0023\u003Dz2OY3nZbjK2oUN9vYsL48jiFCTicz(this.\u0023\u003DzI6P8IpE\u003D[2], this.\u0023\u003DzFfSb8y0\u003D[2], this.\u0023\u003DzI6P8IpE\u003D[3], this.\u0023\u003DzFfSb8y0\u003D[3], this.\u0023\u003DzI6P8IpE\u003D[4], this.\u0023\u003DzFfSb8y0\u003D[4], this.\u0023\u003DzI6P8IpE\u003D[5], this.\u0023\u003DzFfSb8y0\u003D[5], out this.\u0023\u003DzAomBaIYGIk3J[2].\u0023\u003DzwP120vA\u003D, out this.\u0023\u003DzAomBaIYGIk3J[2].\u0023\u003Dzi8jDI4I\u003D);
    this.\u0023\u003DzA25ZMxDrKaxP[3] = (Path.\u0023\u003Dz9kUnn38\u003D) 2;
    this.\u0023\u003DzA25ZMxDrKaxP[4] = (Path.\u0023\u003Dz9kUnn38\u003D) 2;
    this.\u0023\u003DzA25ZMxDrKaxP[5] = (Path.\u0023\u003Dz9kUnn38\u003D) 2;
    this.\u0023\u003DzA25ZMxDrKaxP[6] = (Path.\u0023\u003Dz9kUnn38\u003D) 0;
  }

  public void \u0023\u003DzVawdK5C5Lyf_(int _param1)
  {
    this.\u0023\u003Dz7y_N6Co2XMR5y5SMXw\u003D\u003D = 0;
  }

  public Path.\u0023\u003Dz9kUnn38\u003D \u0023\u003DzxfekdAs1X3YM(
    out double _param1,
    out double _param2)
  {
    _param1 = this.\u0023\u003DzI6P8IpE\u003D[this.\u0023\u003Dz7y_N6Co2XMR5y5SMXw\u003D\u003D];
    _param2 = this.\u0023\u003DzFfSb8y0\u003D[this.\u0023\u003Dz7y_N6Co2XMR5y5SMXw\u003D\u003D];
    return this.\u0023\u003DzA25ZMxDrKaxP[this.\u0023\u003Dz7y_N6Co2XMR5y5SMXw\u003D\u003D++];
  }

  protected void \u0023\u003Dz9_iTpN8u7is\u0024KwW8kN9oXVBQ2TUq(
    \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSvy8c_QvO7qt\u002402e0HOXYtg5UkjUccmmBVwlVFwpO\u0024EC4O_OLVZeFPIM.\u0023\u003DzafZj3dT3iE_e[] _param1)
  {
    _param1[0] = this.\u0023\u003DzAomBaIYGIk3J[0];
    _param1[1] = this.\u0023\u003DzAomBaIYGIk3J[1];
    _param1[2] = this.\u0023\u003DzAomBaIYGIk3J[2];
    if (this.\u0023\u003DzAomBaIYGIk3J[0].\u0023\u003Dzi8jDI4I\u003D > this.\u0023\u003DzAomBaIYGIk3J[2].\u0023\u003Dzi8jDI4I\u003D)
    {
      _param1[0] = this.\u0023\u003DzAomBaIYGIk3J[2];
      _param1[2] = this.\u0023\u003DzAomBaIYGIk3J[0];
    }
    if (_param1[0].\u0023\u003Dzi8jDI4I\u003D > _param1[1].\u0023\u003Dzi8jDI4I\u003D)
    {
      \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSvy8c_QvO7qt\u002402e0HOXYtg5UkjUccmmBVwlVFwpO\u0024EC4O_OLVZeFPIM.\u0023\u003DzafZj3dT3iE_e zafZj3dT3iEE = _param1[1];
      _param1[1] = _param1[0];
      _param1[0] = zafZj3dT3iEE;
    }
    if (_param1[1].\u0023\u003Dzi8jDI4I\u003D <= _param1[2].\u0023\u003Dzi8jDI4I\u003D)
      return;
    \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSvy8c_QvO7qt\u002402e0HOXYtg5UkjUccmmBVwlVFwpO\u0024EC4O_OLVZeFPIM.\u0023\u003DzafZj3dT3iE_e zafZj3dT3iEE1 = _param1[2];
    _param1[2] = _param1[1];
    _param1[1] = zafZj3dT3iEE1;
  }

  public struct \u0023\u003DzafZj3dT3iE_e
  {
    
    public double \u0023\u003DzwP120vA\u003D;
    
    public double \u0023\u003Dzi8jDI4I\u003D;
    
    public RGBA_Bytes \u0023\u003Dz6NUrJ5Q\u003D;
  }
}
