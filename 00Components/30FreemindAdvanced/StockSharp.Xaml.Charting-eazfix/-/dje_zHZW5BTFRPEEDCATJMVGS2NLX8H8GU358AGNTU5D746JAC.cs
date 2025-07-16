// Decompiled with JetBrains decompiler
// Type: -.dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Xaml;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal sealed class dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd : 
  \u0023\u003DzAuXtmwo_UFdzWVVSiImlM\u002467cQAcrK8Ri9x59UQHE4_ZyklbLQ\u003D\u003D
{
  
  private static readonly Lazy<ControlTemplate> \u0023\u003Dzvoot7Cph1AG8 = new Lazy<ControlTemplate>(new Func<ControlTemplate>(dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003Dzm27O\u0024m4PgoEaaJOSFwKn\u0024So\u003D));
  
  private readonly Point \u0023\u003DzNJ8EGFEgy8EH;
  
  private readonly Lazy<ImageSource> \u0023\u003DzXwID4m_q_aiB;

  public dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd(
    string _param1,
    Point _param2,
    Color _param3,
    Color _param4,
    double _param5)
  {
    dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd.SomeClass34343 zPKCmcad6Nxc5A8A = new dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd.SomeClass34343();
    zPKCmcad6Nxc5A8A.\u0023\u003Dz7Hkd0ns\u003D = _param1;
    zPKCmcad6Nxc5A8A.\u0023\u003DzRh5qfIg\u003D = _param4;
    zPKCmcad6Nxc5A8A.\u0023\u003Dz7eGAqyQ\u003D = _param3;
    // ISSUE: explicit constructor call
    base.\u002Ector();
    this.\u0023\u003DzXwID4m_q_aiB = new Lazy<ImageSource>(new Func<ImageSource>(zPKCmcad6Nxc5A8A.\u0023\u003DzPc3AK\u0024sNtgO1kq4Bew\u003D\u003D));
    this.\u0023\u003DzNJ8EGFEgy8EH = _param2;
    this.Width = _param5;
    this.Height = _param5;
    this.PointMarkerTemplate = dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd.\u0023\u003Dzvoot7Cph1AG8.Value;
  }

  
  public static ComponentResourceKey dje_zXWPHZG4CSS2GQXZJSD75W_ejd
  {
    get
    {
      return new ComponentResourceKey(typeof (dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd), (object) "SvgMarkerTemplate");
    }
  }

  public static ComponentResourceKey \u0023\u003Dz7UtwOMpZAQNPO3pYAw\u003D\u003D()
  {
    return new ComponentResourceKey(typeof (dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd), (object) "SvgMarkerTemplate");
  }

  public ImageSource MarkerImageSrc => this.\u0023\u003DzXwID4m_q_aiB.Value;

  [SpecialName]
  protected override Point \u0023\u003DzBM5aTGA3wdWI()
  {
    return new Point(((\u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT) this).Width * this.\u0023\u003DzNJ8EGFEgy8EH.X, ((\u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT) this).Height * this.\u0023\u003DzNJ8EGFEgy8EH.Y);
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd.SomeClass34343383 SomeMethond0343 = new dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd.SomeClass34343383();

    internal ControlTemplate \u0023\u003Dzm27O\u0024m4PgoEaaJOSFwKn\u0024So\u003D()
    {
      return (ControlTemplate) Application.Current.FindResource((object) dje_zHZW5BTFRPEEDCATJMVGS2NLX8H8GU358AGNTU5D746JACHADU4L5YHB24CAQ_ejd.\u0023\u003Dz7UtwOMpZAQNPO3pYAw\u003D\u003D());
    }
  }

  private sealed class SomeClass34343
  {
    public string \u0023\u003Dz7Hkd0ns\u003D;
    public Color \u0023\u003DzRh5qfIg\u003D;
    public Color \u0023\u003Dz7eGAqyQ\u003D;

    internal ImageSource \u0023\u003DzPc3AK\u0024sNtgO1kq4Bew\u003D\u003D()
    {
      return this.\u0023\u003Dz7Hkd0ns\u003D.GetImage(new SolidColorBrush(this.\u0023\u003DzRh5qfIg\u003D), new SolidColorBrush(this.\u0023\u003Dz7eGAqyQ\u003D));
    }
  }
}
