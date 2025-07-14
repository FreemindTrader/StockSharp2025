// Decompiled with JetBrains decompiler
// Type: #=zFDK4fEILkMRswIjIg1$y3MUjKjUKbI3AKh3tzAx6mv5sY8awUAMrmsk=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

#nullable disable
internal sealed class \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MUjKjUKbI3AKh3tzAx6mv5sY8awUAMrmsk\u003D : 
  dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd
{
  
  private readonly DispatcherTimer \u0023\u003DzO6iOtf4\u003D;
  
  private double \u0023\u003DzCvE85jZgqySy;
  
  private double \u0023\u003Dz9OR_fOFczTfr;
  
  private double \u0023\u003Dz6kKPJacULkFJ;
  
  private double \u0023\u003Dzt9nF\u0024JMVAHGK;

  public \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MUjKjUKbI3AKh3tzAx6mv5sY8awUAMrmsk\u003D()
  {
    this.\u0023\u003DzO6iOtf4\u003D = new DispatcherTimer()
    {
      Interval = TimeSpan.FromMilliseconds(40.0)
    };
    this.\u0023\u003DzO6iOtf4\u003D.Tick += new EventHandler(this.\u0023\u003Dz6fc78SIV6E\u0024a);
  }

  public override void \u0023\u003DzHcrX_TM\u003D(Point _param1, Point _param2, Point _param3)
  {
    this.\u0023\u003Dztd6gHB\u0024IuqHuDrJ4hobbxKs\u003D(_param1, _param2, _param3);
  }

  private void \u0023\u003Dztd6gHB\u0024IuqHuDrJ4hobbxKs\u003D(
    Point _param1,
    Point _param2,
    Point _param3)
  {
    this.\u0023\u003DzCvE85jZgqySy += _param1.X - _param2.X;
    this.\u0023\u003Dz9OR_fOFczTfr += _param2.Y - _param1.Y;
    this.\u0023\u003Dz6kKPJacULkFJ += _param1.X - _param3.X;
    this.\u0023\u003Dzt9nF\u0024JMVAHGK += _param3.Y - _param1.Y;
    this.\u0023\u003DzCvE85jZgqySy = Math.Max(Math.Min(this.\u0023\u003DzCvE85jZgqySy, 250.0), -250.0);
    this.\u0023\u003Dz9OR_fOFczTfr = Math.Max(Math.Min(this.\u0023\u003Dz9OR_fOFczTfr, 250.0), -250.0);
    this.\u0023\u003Dz6kKPJacULkFJ = Math.Max(Math.Min(this.\u0023\u003Dz6kKPJacULkFJ, 250.0), -250.0);
    this.\u0023\u003Dzt9nF\u0024JMVAHGK = Math.Max(Math.Min(this.\u0023\u003Dzt9nF\u0024JMVAHGK, 250.0), -250.0);
    if (this.\u0023\u003DzO6iOtf4\u003D.IsEnabled)
      return;
    this.\u0023\u003DzO6iOtf4\u003D.Start();
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (!this.IsDragging)
      return;
    base.\u0023\u003DzU3pYs4rYVmOS(_param1);
  }

  public override void \u0023\u003DzIc5ifvVNpnRzFbQNeA\u003D\u003D()
  {
    this.\u0023\u003DzO6iOtf4\u003D.Stop();
    this.\u0023\u003DzCvE85jZgqySy = this.\u0023\u003Dz9OR_fOFczTfr = this.\u0023\u003Dz6kKPJacULkFJ = this.\u0023\u003Dzt9nF\u0024JMVAHGK = 0.0;
  }

  private void \u0023\u003Dz6fc78SIV6E\u0024a(object _param1, EventArgs _param2)
  {
    double zCvE85jZgqySy = this.\u0023\u003DzCvE85jZgqySy;
    double z9OrFOfczTfr = this.\u0023\u003Dz9OR_fOFczTfr;
    double z6kKpJacUlkFj = this.\u0023\u003Dz6kKPJacULkFJ;
    double zt9nFJmvahgk = this.\u0023\u003Dzt9nF\u0024JMVAHGK;
    this.\u0023\u003DzCvE85jZgqySy = this.\u0023\u003DzCvE85jZgqySy > 0.0 ? Math.Max(0.0, this.\u0023\u003DzCvE85jZgqySy - 10.0) : Math.Min(0.0, this.\u0023\u003DzCvE85jZgqySy + 10.0);
    this.\u0023\u003Dz9OR_fOFczTfr = this.\u0023\u003Dz9OR_fOFczTfr > 0.0 ? Math.Max(0.0, this.\u0023\u003Dz9OR_fOFczTfr - 10.0) : Math.Min(0.0, this.\u0023\u003Dz9OR_fOFczTfr + 10.0);
    this.\u0023\u003Dz6kKPJacULkFJ = this.\u0023\u003Dz6kKPJacULkFJ > 0.0 ? Math.Max(0.0, this.\u0023\u003Dz6kKPJacULkFJ - 10.0) : Math.Min(0.0, this.\u0023\u003Dz6kKPJacULkFJ + 10.0);
    this.\u0023\u003Dzt9nF\u0024JMVAHGK = this.\u0023\u003Dzt9nF\u0024JMVAHGK > 0.0 ? Math.Max(0.0, this.\u0023\u003Dzt9nF\u0024JMVAHGK - 10.0) : Math.Min(0.0, this.\u0023\u003Dzt9nF\u0024JMVAHGK + 10.0);
    if (Math.Abs(this.\u0023\u003DzCvE85jZgqySy) <= 0.0 && Math.Abs(this.\u0023\u003Dz9OR_fOFczTfr) <= 0.0 && Math.Abs(this.\u0023\u003Dz6kKPJacULkFJ) <= 0.0 && Math.Abs(this.\u0023\u003Dzt9nF\u0024JMVAHGK) <= 0.0)
      this.\u0023\u003DzO6iOtf4\u003D.Stop();
    if (this.ParentSurface == null)
    {
      this.\u0023\u003DzO6iOtf4\u003D.Stop();
    }
    else
    {
      using (this.ParentSurface.SuspendUpdates())
      {
        if (this.XyDirection != dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection)
        {
          foreach (IAxis xax in this.XAxes)
          {
            int num1 = xax.IsHorizontalAxis ? 1 : 0;
            bool? isHorizontalAxis = this.XAxis?.IsHorizontalAxis;
            int num2 = isHorizontalAxis.GetValueOrDefault() ? 1 : 0;
            if (num1 == num2 & isHorizontalAxis.HasValue)
            {
              using (IUpdateSuspender fq05jnDg3bOrIrgCjote = xax.SuspendUpdates())
              {
                fq05jnDg3bOrIrgCjote.ResumeTargetOnDispose=false;
                double num3 = zCvE85jZgqySy;
                double num4 = z9OrFOfczTfr;
                if (xax.get_IsCategoryAxis())
                {
                  num3 = z6kKpJacUlkFj;
                  num4 = zt9nFJmvahgk;
                }
                double num5 = num3 * 0.5;
                double num6 = num4 * 0.5;
                xax.\u0023\u003DzquLnA5Y\u003D(xax.IsHorizontalAxis ? num5 : -num6, this.ClipModeX);
              }
            }
            else
              break;
          }
        }
        if (this.XyDirection == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection)
        {
          if (!this.ZoomExtentsY)
            return;
          this.ParentSurface.ZoomExtentsY();
        }
        else
        {
          foreach (IAxis yax in this.YAxes)
            yax.\u0023\u003DzquLnA5Y\u003D(yax.IsHorizontalAxis ? -zCvE85jZgqySy : z9OrFOfczTfr, ClipMode.None);
        }
      }
    }
  }
}
