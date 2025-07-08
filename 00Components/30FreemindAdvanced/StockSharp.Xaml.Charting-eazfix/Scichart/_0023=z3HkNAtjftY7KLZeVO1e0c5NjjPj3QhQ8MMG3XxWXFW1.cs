// Decompiled with JetBrains decompiler
// Type: #=z3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

#nullable disable
internal sealed class \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a : 
  dje_zRZN2N3AMLJBXJD5QUJNGUET4WSTZAVXWDYQQFKDCKHYXDHP8L7XC4_ejd
{
  
  private bool \u0023\u003DzqGwbHdeZ8yMA;
  
  private Point \u0023\u003DzvQbXnoA\u003D;
  
  private readonly Dictionary<int, Point> \u0023\u003DzYw05nwk\u003D;
  
  private readonly Dictionary<int, Point> \u0023\u003DzdXHzI2qgXcUL;
  
  private double \u0023\u003Dzqx1eAeyQiSaJ;
  
  private double \u0023\u003DzvwtbDVFk\u0024M3n;
  
  private double \u0023\u003DzvfpGK0ec3b38;
  
  private readonly DispatcherTimer \u0023\u003DzEM3AHVhw9XWS;
  
  private bool \u0023\u003DzOsLBTKo4JmwE;
  
  private bool \u0023\u003Dzf1Hn4XroTm02;
  
  private Point \u0023\u003DzemTaNjeDUoih;
  
  private double \u0023\u003DzodsqFZFZbLUe;
  
  private double \u0023\u003Dz7EQIewpOll_c;
  
  private bool \u0023\u003DzoaFDpW7A_iqDVWvw5Q\u003D\u003D;

  public \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a()
  {
    this.GrowFactor = 0.01;
    this.\u0023\u003DzYw05nwk\u003D = new Dictionary<int, Point>();
    this.\u0023\u003DzdXHzI2qgXcUL = new Dictionary<int, Point>();
    this.\u0023\u003DzEM3AHVhw9XWS = new DispatcherTimer()
    {
      Interval = TimeSpan.FromMilliseconds(40.0)
    };
    this.\u0023\u003DzEM3AHVhw9XWS.Tick += new EventHandler(this.\u0023\u003DzWGQTnobNlSA4);
  }

  public bool IsDragging => this.\u0023\u003DzqGwbHdeZ8yMA;

  public bool IsUniform
  {
    get => this.\u0023\u003DzoaFDpW7A_iqDVWvw5Q\u003D\u003D;
    set => this.\u0023\u003DzoaFDpW7A_iqDVWvw5Q\u003D\u003D = value;
  }

  private void \u0023\u003DzWGQTnobNlSA4(object _param1, EventArgs _param2)
  {
    this.\u0023\u003DzEM3AHVhw9XWS.Stop();
    this.\u0023\u003DzOsLBTKo4JmwE = false;
    if (this.\u0023\u003Dzf1Hn4XroTm02)
      this.\u0023\u003DzIjNc90j5mMD8(this.\u0023\u003DzemTaNjeDUoih, this.\u0023\u003DzodsqFZFZbLUe, this.\u0023\u003Dz7EQIewpOll_c);
    this.\u0023\u003Dzf1Hn4XroTm02 = false;
    this.\u0023\u003DzodsqFZFZbLUe = this.\u0023\u003Dz7EQIewpOll_c = 0.0;
    this.\u0023\u003DzemTaNjeDUoih = new Point();
  }

  protected override void \u0023\u003DzIjNc90j5mMD8(Point _param1, double _param2, double _param3)
  {
    this.\u0023\u003DzOsLBTKo4JmwE = true;
    this.\u0023\u003Dzf1Hn4XroTm02 = false;
    this.\u0023\u003DzEM3AHVhw9XWS.Start();
    base.\u0023\u003DzIjNc90j5mMD8(_param1, _param2, _param3);
  }

  public override void \u0023\u003Dz0yya794Z8OaI(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    foreach (TouchPoint touchPoint in _param1.\u0023\u003DzeKSkpjwaiSdieql2hyn60Uw\u003D())
    {
      Point point = this.\u0023\u003DzOaYrn8YGTeR7(touchPoint.Position, (IHitTestable) this.ModifierSurface);
      if (point.X >= 0.0 && point.X <= this.ModifierSurface.ActualWidth && point.Y >= 0.0 && point.Y <= this.ModifierSurface.ActualHeight && !this.\u0023\u003DzYw05nwk\u003D.ContainsKey(touchPoint.TouchDevice.Id))
      {
        this.\u0023\u003DzYw05nwk\u003D.Add(touchPoint.TouchDevice.Id, point);
        this.\u0023\u003DzdXHzI2qgXcUL.Add(touchPoint.TouchDevice.Id, point);
      }
    }
    if (this.\u0023\u003DzYw05nwk\u003D.Count >= 2)
    {
      this.\u0023\u003DzqGwbHdeZ8yMA = true;
      this.\u0023\u003DzvwtbDVFk\u0024M3n = this.\u0023\u003DzYw05nwk\u003D.Max<KeyValuePair<int, Point>>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D ?? (\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D = new Func<KeyValuePair<int, Point>, double>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.SomeMethond0343.\u0023\u003DzqcNouvrU0rNE8lSQc4iox31rT88n))) - this.\u0023\u003DzYw05nwk\u003D.Min<KeyValuePair<int, Point>>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003Dzn3Ai2aKG_dFUAAfoTA\u003D\u003D ?? (\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003Dzn3Ai2aKG_dFUAAfoTA\u003D\u003D = new Func<KeyValuePair<int, Point>, double>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.SomeMethond0343.\u0023\u003Dz3ChMpBd_JR\u0024N6HzWsq2YTOPbtXVI)));
      this.\u0023\u003DzvfpGK0ec3b38 = this.\u0023\u003DzYw05nwk\u003D.Max<KeyValuePair<int, Point>>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzZP097hnyq4s7yMJTOQ\u003D\u003D ?? (\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzZP097hnyq4s7yMJTOQ\u003D\u003D = new Func<KeyValuePair<int, Point>, double>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.SomeMethond0343.\u0023\u003Dz6K97g7WdwUvNgOQFjTclH7nc2WEF))) - this.\u0023\u003DzYw05nwk\u003D.Min<KeyValuePair<int, Point>>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003Dzn3Ai2aKG_dHtFPpUoQ\u003D\u003D ?? (\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003Dzn3Ai2aKG_dHtFPpUoQ\u003D\u003D = new Func<KeyValuePair<int, Point>, double>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.SomeMethond0343.\u0023\u003Dze1wjt0BHA\u00246YkLoAv3w2IUygSs45)));
      this.\u0023\u003Dzqx1eAeyQiSaJ = Math.Sqrt(this.\u0023\u003DzvwtbDVFk\u0024M3n * this.\u0023\u003DzvwtbDVFk\u0024M3n + this.\u0023\u003DzvfpGK0ec3b38 * this.\u0023\u003DzvfpGK0ec3b38);
      this.\u0023\u003DzvQbXnoA\u003D = this.\u0023\u003Dz6Fh6_ls\u003D();
    }
    base.\u0023\u003Dz0yya794Z8OaI(_param1);
  }

  public override void \u0023\u003DzpmQpuKvOtHIk(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    if (!this.IsDragging)
      return;
    foreach (TouchPoint touchPoint in _param1.\u0023\u003DzeKSkpjwaiSdieql2hyn60Uw\u003D())
    {
      if (this.\u0023\u003DzYw05nwk\u003D.ContainsKey(touchPoint.TouchDevice.Id))
        this.\u0023\u003DzYw05nwk\u003D[touchPoint.TouchDevice.Id] = this.\u0023\u003DzOaYrn8YGTeR7(touchPoint.Position, (IHitTestable) this.ModifierSurface);
    }
    double num1 = this.\u0023\u003DzYw05nwk\u003D.Max<KeyValuePair<int, Point>>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzNCbtu8hqqUunVlh3ow\u003D\u003D ?? (\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzNCbtu8hqqUunVlh3ow\u003D\u003D = new Func<KeyValuePair<int, Point>, double>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.SomeMethond0343.\u0023\u003DzcxCfUXBY83pn0enPPVtErpE\u003D))) - this.\u0023\u003DzYw05nwk\u003D.Min<KeyValuePair<int, Point>>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzMRTIPs6OoCcj5pN\u0024xQ\u003D\u003D ?? (\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzMRTIPs6OoCcj5pN\u0024xQ\u003D\u003D = new Func<KeyValuePair<int, Point>, double>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.SomeMethond0343.\u0023\u003Dzye0OQ99rUllFk16awFxF7ks\u003D)));
    double num2 = this.\u0023\u003DzYw05nwk\u003D.Max<KeyValuePair<int, Point>>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzNMIp7invDKUnL7ASHg\u003D\u003D ?? (\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzNMIp7invDKUnL7ASHg\u003D\u003D = new Func<KeyValuePair<int, Point>, double>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.SomeMethond0343.\u0023\u003Dzi7NUn2JZ6fxgLRJ61qPMyLI\u003D))) - this.\u0023\u003DzYw05nwk\u003D.Min<KeyValuePair<int, Point>>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzJ4our6IdSBQJOV6WDQ\u003D\u003D ?? (\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzJ4our6IdSBQJOV6WDQ\u003D\u003D = new Func<KeyValuePair<int, Point>, double>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.SomeMethond0343.\u0023\u003DzmT57mKJOp6SNOj_6DrkfWfc\u003D)));
    double num3 = Math.Sqrt(num1 * num1 + this.\u0023\u003DzvfpGK0ec3b38 * this.\u0023\u003DzvfpGK0ec3b38);
    double num4 = num1 - this.\u0023\u003DzvwtbDVFk\u0024M3n;
    double num5 = num2 - this.\u0023\u003DzvfpGK0ec3b38;
    double num6 = num3 - this.\u0023\u003Dzqx1eAeyQiSaJ;
    if (this.\u0023\u003DzdXHzI2qgXcUL.Count >= 2)
    {
      this.\u0023\u003DzvQbXnoA\u003D = this.\u0023\u003Dz6Fh6_ls\u003D();
      double num7 = -this.\u0023\u003DzC520uIs\u003D(num6);
      this.\u0023\u003DzNNd36v7PvsH0uXqMGg\u003D\u003D(this.\u0023\u003DzvQbXnoA\u003D, this.IsUniform ? num7 : -this.\u0023\u003DzC520uIs\u003D(num5), this.IsUniform ? num7 : -this.\u0023\u003DzC520uIs\u003D(num4));
      this.\u0023\u003Dzqx1eAeyQiSaJ = num3;
      this.\u0023\u003DzvwtbDVFk\u0024M3n = num1;
      this.\u0023\u003DzvfpGK0ec3b38 = num2;
    }
    base.\u0023\u003DzpmQpuKvOtHIk(_param1);
  }

  private Point \u0023\u003Dz6Fh6_ls\u003D()
  {
    return new Point(this.\u0023\u003DzdXHzI2qgXcUL.Average<KeyValuePair<int, Point>>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D ?? (\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D = new Func<KeyValuePair<int, Point>, double>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.SomeMethond0343.\u0023\u003DzRMoXG\u0024QaQ7qrkIPxk3zAzyc\u003D))), this.\u0023\u003DzdXHzI2qgXcUL.Average<KeyValuePair<int, Point>>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzFrKZ2idkhLzP6ElHlg\u003D\u003D ?? (\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.\u0023\u003DzFrKZ2idkhLzP6ElHlg\u003D\u003D = new Func<KeyValuePair<int, Point>, double>(\u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383.SomeMethond0343.\u0023\u003DzQb1UTA78853CAcRtIcxw1T4\u003D))));
  }

  private double \u0023\u003DzC520uIs\u003D(double _param1)
  {
    return Math.Min(1.0, Math.Max(-1.0, _param1));
  }

  private void \u0023\u003DzNNd36v7PvsH0uXqMGg\u003D\u003D(
    Point _param1,
    double _param2,
    double _param3)
  {
    if (this.\u0023\u003DzOsLBTKo4JmwE)
    {
      this.\u0023\u003DzemTaNjeDUoih = _param1;
      this.\u0023\u003DzodsqFZFZbLUe += _param2;
      this.\u0023\u003Dz7EQIewpOll_c += _param3;
      this.\u0023\u003Dzf1Hn4XroTm02 = true;
    }
    else
      this.\u0023\u003DzIjNc90j5mMD8(_param1, _param2, _param3);
  }

  public override void \u0023\u003DzsSwjrBzrsGPJ(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1)
  {
    foreach (TouchPoint touchPoint in _param1.\u0023\u003DzeKSkpjwaiSdieql2hyn60Uw\u003D())
    {
      int id = touchPoint.TouchDevice.Id;
      if (this.\u0023\u003DzYw05nwk\u003D.ContainsKey(id))
      {
        this.\u0023\u003DzYw05nwk\u003D.Remove(id);
        this.\u0023\u003DzdXHzI2qgXcUL.Remove(id);
      }
    }
    if (this.\u0023\u003DzdXHzI2qgXcUL.Any<KeyValuePair<int, Point>>())
      return;
    this.\u0023\u003DzqGwbHdeZ8yMA = false;
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383 SomeMethond0343 = new \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c5NjjPj3QhQ8MMG3XxWXFW1a.SomeClass34343383();
    public static Func<KeyValuePair<int, Point>, double> \u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D;
    public static Func<KeyValuePair<int, Point>, double> \u0023\u003Dzn3Ai2aKG_dFUAAfoTA\u003D\u003D;
    public static Func<KeyValuePair<int, Point>, double> \u0023\u003DzZP097hnyq4s7yMJTOQ\u003D\u003D;
    public static Func<KeyValuePair<int, Point>, double> \u0023\u003Dzn3Ai2aKG_dHtFPpUoQ\u003D\u003D;
    public static Func<KeyValuePair<int, Point>, double> \u0023\u003DzNCbtu8hqqUunVlh3ow\u003D\u003D;
    public static Func<KeyValuePair<int, Point>, double> \u0023\u003DzMRTIPs6OoCcj5pN\u0024xQ\u003D\u003D;
    public static Func<KeyValuePair<int, Point>, double> \u0023\u003DzNMIp7invDKUnL7ASHg\u003D\u003D;
    public static Func<KeyValuePair<int, Point>, double> \u0023\u003DzJ4our6IdSBQJOV6WDQ\u003D\u003D;
    public static Func<KeyValuePair<int, Point>, double> \u0023\u003DzZ1TIJti3dLv69swWwQ\u003D\u003D;
    public static Func<KeyValuePair<int, Point>, double> \u0023\u003DzFrKZ2idkhLzP6ElHlg\u003D\u003D;

    internal double \u0023\u003DzqcNouvrU0rNE8lSQc4iox31rT88n(KeyValuePair<int, Point> _param1)
    {
      return _param1.Value.X;
    }

    internal double \u0023\u003Dz3ChMpBd_JR\u0024N6HzWsq2YTOPbtXVI(KeyValuePair<int, Point> _param1)
    {
      return _param1.Value.X;
    }

    internal double \u0023\u003Dz6K97g7WdwUvNgOQFjTclH7nc2WEF(KeyValuePair<int, Point> _param1)
    {
      return _param1.Value.Y;
    }

    internal double \u0023\u003Dze1wjt0BHA\u00246YkLoAv3w2IUygSs45(KeyValuePair<int, Point> _param1)
    {
      return _param1.Value.Y;
    }

    internal double \u0023\u003DzcxCfUXBY83pn0enPPVtErpE\u003D(KeyValuePair<int, Point> _param1)
    {
      return _param1.Value.X;
    }

    internal double \u0023\u003Dzye0OQ99rUllFk16awFxF7ks\u003D(KeyValuePair<int, Point> _param1)
    {
      return _param1.Value.X;
    }

    internal double \u0023\u003Dzi7NUn2JZ6fxgLRJ61qPMyLI\u003D(KeyValuePair<int, Point> _param1)
    {
      return _param1.Value.Y;
    }

    internal double \u0023\u003DzmT57mKJOp6SNOj_6DrkfWfc\u003D(KeyValuePair<int, Point> _param1)
    {
      return _param1.Value.Y;
    }

    internal double \u0023\u003DzRMoXG\u0024QaQ7qrkIPxk3zAzyc\u003D(KeyValuePair<int, Point> _param1)
    {
      return _param1.Value.X;
    }

    internal double \u0023\u003DzQb1UTA78853CAcRtIcxw1T4\u003D(KeyValuePair<int, Point> _param1)
    {
      return _param1.Value.Y;
    }
  }
}
