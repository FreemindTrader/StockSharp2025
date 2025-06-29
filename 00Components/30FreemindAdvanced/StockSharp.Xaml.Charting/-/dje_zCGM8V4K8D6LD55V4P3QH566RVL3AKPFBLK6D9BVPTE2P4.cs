// Decompiled with JetBrains decompiler
// Type: -.dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd : 
  ItemsControl
{
  
  public static readonly DependencyProperty \u0023\u003DzfMw6oHlwmSrk = DependencyProperty.Register(XXX.SSS(-539428268), typeof (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D), typeof (dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd.\u0023\u003DzlsjS\u0024q6k1db3)));
  
  public static readonly DependencyProperty \u0023\u003DzR_CKkzERxHn9SGnaiYrSxEE\u003D = DependencyProperty.Register(XXX.SSS(-539434381), typeof (bool), typeof (dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd.\u0023\u003Dz4yJK3ICfHaOEZ3fKD9DbViIosNdN)));
  
  public static readonly DependencyProperty \u0023\u003DzGEpRUSytcG_B = DependencyProperty.Register(XXX.SSS(-539428145), typeof (bool), typeof (dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003DzXMV_skc\u003D = DependencyProperty.Register(XXX.SSS(-539428099), typeof (Orientation), typeof (dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd), new PropertyMetadata((object) Orientation.Vertical));

  public dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd);
  }

  public \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D LegendData
  {
    get
    {
      return (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D) this.GetValue(dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd.\u0023\u003DzfMw6oHlwmSrk);
    }
    set
    {
      this.SetValue(dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd.\u0023\u003DzfMw6oHlwmSrk, (object) value);
    }
  }

  public bool ShowVisibilityCheckboxes
  {
    get
    {
      return (bool) this.GetValue(dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd.\u0023\u003DzR_CKkzERxHn9SGnaiYrSxEE\u003D);
    }
    set
    {
      this.SetValue(dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd.\u0023\u003DzR_CKkzERxHn9SGnaiYrSxEE\u003D, (object) value);
    }
  }

  public bool ShowSeriesMarkers
  {
    get
    {
      return (bool) this.GetValue(dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd.\u0023\u003DzGEpRUSytcG_B);
    }
    set
    {
      this.SetValue(dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd.\u0023\u003DzGEpRUSytcG_B, (object) value);
    }
  }

  public Orientation Orientation
  {
    get
    {
      return (Orientation) this.GetValue(dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd.\u0023\u003DzXMV_skc\u003D);
    }
    set
    {
      this.SetValue(dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd.\u0023\u003DzXMV_skc\u003D, (object) value);
    }
  }

  private static void \u0023\u003Dz4yJK3ICfHaOEZ3fKD9DbViIosNdN(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd r886FtpjvsuvaEjd = (dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd) _param0;
    if (r886FtpjvsuvaEjd.LegendData == null)
      return;
    r886FtpjvsuvaEjd.LegendData.ShowVisibilityCheckboxes = (bool) _param1.NewValue;
  }

  private static void \u0023\u003DzlsjS\u0024q6k1db3(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd r886FtpjvsuvaEjd = (dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd) _param0;
    if (!(_param1.NewValue is \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D newValue))
      return;
    newValue.ShowVisibilityCheckboxes = r886FtpjvsuvaEjd.ShowVisibilityCheckboxes;
  }
}
