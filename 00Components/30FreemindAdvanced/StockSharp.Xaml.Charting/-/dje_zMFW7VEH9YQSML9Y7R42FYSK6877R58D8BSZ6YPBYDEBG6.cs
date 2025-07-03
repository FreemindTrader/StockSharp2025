// Decompiled with JetBrains decompiler
// Type: -.dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable enable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd : 
  dje_zJTTBFK72S5XZHAEG5FKM6MZCVK2PFDPGX35PL6ZWJKC2L5HE3LYF5_ejd
{
  
  private 
  #nullable disable
  Dictionary<string, IRange> \u0023\u003DzlmAnsLYRHYNHzCBTWw\u003D\u003D;

  public override void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzsXEfcKpqchyX(_param1);
    this.\u0023\u003DzlmAnsLYRHYNHzCBTWw\u003D\u003D = this.XAxes.Where<IAxis>(dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzxLUmJTOQFZstXBxKkg\u003D\u003D ?? (dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzxLUmJTOQFZstXBxKkg\u003D\u003D = new Func<IAxis, bool>(dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DztdZxHXQvl0jLk18eWYDLqRE\u003D))).ToDictionary<IAxis, string, IRange>(dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D ?? (dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D = new Func<IAxis, string>(dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzDdBD4\u0024viao9m95yVI0EJ1GE\u003D)), dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzrvebN45bGKztwoYTiA\u003D\u003D ?? (dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzrvebN45bGKztwoYTiA\u003D\u003D = new Func<IAxis, IRange>(dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzzdOSOlF3qFksNQn1piaenHc\u003D)));
  }

  public override void \u0023\u003DzHcrX_TM\u003D(Point _param1, Point _param2, Point _param3)
  {
    this.\u0023\u003Dz6fc78SIV6E\u0024a(_param1, _param2, _param3);
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (!this.IsDragging)
      return;
    base.\u0023\u003DzU3pYs4rYVmOS(_param1);
    this.\u0023\u003DzlmAnsLYRHYNHzCBTWw\u003D\u003D = new Dictionary<string, IRange>();
  }

  private void \u0023\u003Dz6fc78SIV6E\u0024a(Point _param1, Point _param2, Point _param3)
  {
    double num1 = _param1.X - _param2.X;
    double num2 = _param2.Y - _param1.Y;
    using (this.ParentSurface.SuspendUpdates())
    {
      if (this.XyDirection != dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection)
      {
        foreach (IAxis xax in this.XAxes)
        {
          int num3 = xax.IsHorizontalAxis ? 1 : 0;
          bool? isHorizontalAxis = this.XAxis?.IsHorizontalAxis;
          int num4 = isHorizontalAxis.GetValueOrDefault() ? 1 : 0;
          if (num3 == num4 & isHorizontalAxis.HasValue)
          {
            using (IUpdateSuspender fq05jnDg3bOrIrgCjote = xax.SuspendUpdates())
            {
              fq05jnDg3bOrIrgCjote.ResumeTargetOnDispose=false;
              double num5 = num1;
              double num6 = num2;
              if (xax.get_IsCategoryAxis())
              {
                xax.VisibleRange = this.\u0023\u003DzlmAnsLYRHYNHzCBTWw\u003D\u003D[xax.Id];
                num5 = _param1.X - _param3.X;
                num6 = _param3.Y - _param1.Y;
              }
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
        this.ParentSurface.\u0023\u003Dz7mFu4O\u0024TokaR();
      }
      else
      {
        foreach (IAxis yax in this.YAxes)
          yax.\u0023\u003DzquLnA5Y\u003D(yax.IsHorizontalAxis ? -num1 : num2, dje_z2ZZ2J3MC6TQVDLKAL45CSJLJJGW9K8Z7DFRDFNMP_ejd.None);
      }
    }
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zMFW7VEH9YQSML9Y7R42FYSK6877R58D8BSZ6YPBYDEBG6VFFGCPZE_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<IAxis, bool> \u0023\u003DzxLUmJTOQFZstXBxKkg\u003D\u003D;
    public static Func<IAxis, 
    #nullable enable
    string> \u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D;
    public static 
    #nullable disable
    Func<IAxis, IRange> \u0023\u003DzrvebN45bGKztwoYTiA\u003D\u003D;

    internal bool \u0023\u003DztdZxHXQvl0jLk18eWYDLqRE\u003D(
      IAxis _param1)
    {
      return _param1.get_IsCategoryAxis();
    }

    internal 
    #nullable enable
    string \u0023\u003DzDdBD4\u0024viao9m95yVI0EJ1GE\u003D(
      #nullable disable
      IAxis _param1)
    {
      return _param1.Id;
    }

    internal IRange \u0023\u003DzzdOSOlF3qFksNQn1piaenHc\u003D(
      IAxis _param1)
    {
      return _param1.VisibleRange;
    }
  }
}
