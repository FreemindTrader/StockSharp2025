// Decompiled with JetBrains decompiler
// Type: #=zXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D
{
  private readonly Brush \u0023\u003DzPbF2kpY\u003D;
  private readonly FrameworkElement \u0023\u003DzKz9ivOvkpx2X;
  private readonly Size \u0023\u003DzJpbCbio\u003D;

  public \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D(Size _param1, Brush _param2)
  {
    this.\u0023\u003DzJpbCbio\u003D = _param1;
    this.\u0023\u003DzPbF2kpY\u003D = _param2;
  }

  public \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D(FrameworkElement _param1)
  {
    this.\u0023\u003DzKz9ivOvkpx2X = _param1;
  }

  public override int GetHashCode()
  {
    if (this.\u0023\u003DzKz9ivOvkpx2X != null)
      return ((object) this.\u0023\u003DzKz9ivOvkpx2X).GetHashCode();
    double num1 = this.\u0023\u003DzJpbCbio\u003D.Width;
    int hashCode1 = num1.GetHashCode();
    num1 = this.\u0023\u003DzJpbCbio\u003D.Height;
    int hashCode2 = num1.GetHashCode();
    int num2 = hashCode1 ^ hashCode2;
    int hashCode3;
    if (this.\u0023\u003DzPbF2kpY\u003D is LinearGradientBrush)
    {
      foreach (GradientStop gradientStop in ((GradientBrush) this.\u0023\u003DzPbF2kpY\u003D).GradientStops)
      {
        num2 ^= gradientStop.Color.GetHashCode();
        int num3 = num2;
        num1 = gradientStop.Offset;
        int hashCode4 = num1.GetHashCode();
        num2 = num3 ^ hashCode4;
      }
      int num4 = num2;
      Point point = ((LinearGradientBrush) this.\u0023\u003DzPbF2kpY\u003D).StartPoint;
      int hashCode5 = point.GetHashCode();
      int num5 = num4 ^ hashCode5;
      point = ((LinearGradientBrush) this.\u0023\u003DzPbF2kpY\u003D).EndPoint;
      int hashCode6 = point.GetHashCode();
      hashCode3 = num5 ^ hashCode6;
    }
    else
      hashCode3 = num2 ^ ((object) this.\u0023\u003DzPbF2kpY\u003D).GetHashCode();
    return hashCode3;
  }

  public override bool Equals(object _param1)
  {
    if (!(_param1 is \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D mqG4NeX6nMgPydnA))
      return false;
    if (this.\u0023\u003DzKz9ivOvkpx2X != null)
      return ((object) this.\u0023\u003DzKz9ivOvkpx2X).Equals((object) mqG4NeX6nMgPydnA.\u0023\u003DzKz9ivOvkpx2X);
    if (mqG4NeX6nMgPydnA.\u0023\u003DzJpbCbio\u003D.Width != this.\u0023\u003DzJpbCbio\u003D.Width || mqG4NeX6nMgPydnA.\u0023\u003DzJpbCbio\u003D.Height != this.\u0023\u003DzJpbCbio\u003D.Height)
      return false;
    if (!(this.\u0023\u003DzPbF2kpY\u003D is LinearGradientBrush))
      return ((object) this.\u0023\u003DzPbF2kpY\u003D).Equals((object) mqG4NeX6nMgPydnA.\u0023\u003DzPbF2kpY\u003D);
    if (!(mqG4NeX6nMgPydnA.\u0023\u003DzPbF2kpY\u003D is LinearGradientBrush) || Point.op_Inequality(((LinearGradientBrush) this.\u0023\u003DzPbF2kpY\u003D).StartPoint, ((LinearGradientBrush) mqG4NeX6nMgPydnA.\u0023\u003DzPbF2kpY\u003D).StartPoint) || Point.op_Inequality(((LinearGradientBrush) this.\u0023\u003DzPbF2kpY\u003D).EndPoint, ((LinearGradientBrush) mqG4NeX6nMgPydnA.\u0023\u003DzPbF2kpY\u003D).EndPoint))
      return false;
    GradientStopCollection gradientStops1 = ((GradientBrush) this.\u0023\u003DzPbF2kpY\u003D).GradientStops;
    GradientStopCollection gradientStops2 = ((GradientBrush) mqG4NeX6nMgPydnA.\u0023\u003DzPbF2kpY\u003D).GradientStops;
    if (gradientStops1.Count != gradientStops2.Count)
      return false;
    for (int index = 0; index < gradientStops1.Count; ++index)
    {
      GradientStop gradientStop1 = gradientStops1[index];
      GradientStop gradientStop2 = gradientStops2[index];
      if (gradientStop1.Color != gradientStop2.Color || gradientStop1.Offset != gradientStop2.Offset)
        return false;
    }
    return true;
  }
}
