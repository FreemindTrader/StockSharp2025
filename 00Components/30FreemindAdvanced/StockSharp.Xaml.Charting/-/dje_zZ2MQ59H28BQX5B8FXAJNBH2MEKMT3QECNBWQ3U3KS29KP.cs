// Decompiled with JetBrains decompiler
// Type: -.dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd : 
  Control,
  INotifyPropertyChanged
{
  
  public static readonly DependencyProperty \u0023\u003DzaKv9NrOuOcapF0LrKNnfOo9b1\u0024C2bSjheA\u003D\u003D = DependencyProperty.Register(XXX.SSS(-539338101), typeof (dje_zYGCX6K4J87LQZ9RSX9K3KJFMDZGJ7QYG4UPCA49TR6FZFYAEDRSETKLDJTKTT2AG3C5RABZZAVPGTCHC3TQQYAYANMZQ_ejd), typeof (dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd.\u0023\u003DzPWvFs4bMVSvB)));
  
  public static readonly DependencyProperty \u0023\u003DzXMV_skc\u003D = DependencyProperty.Register(XXX.SSS(-539428099), typeof (Orientation), typeof (dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd), new PropertyMetadata((object) Orientation.Horizontal, new PropertyChangedCallback(dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd.\u0023\u003DzcZqyvQl0j0My)));

  public dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd);
  }

  public dje_zYGCX6K4J87LQZ9RSX9K3KJFMDZGJ7QYG4UPCA49TR6FZFYAEDRSETKLDJTKTT2AG3C5RABZZAVPGTCHC3TQQYAYANMZQ_ejd FastHeatMapRenderableSeries
  {
    get
    {
      return (dje_zYGCX6K4J87LQZ9RSX9K3KJFMDZGJ7QYG4UPCA49TR6FZFYAEDRSETKLDJTKTT2AG3C5RABZZAVPGTCHC3TQQYAYANMZQ_ejd) this.GetValue(dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd.\u0023\u003DzaKv9NrOuOcapF0LrKNnfOo9b1\u0024C2bSjheA\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd.\u0023\u003DzaKv9NrOuOcapF0LrKNnfOo9b1\u0024C2bSjheA\u003D\u003D, (object) value);
    }
  }

  public Orientation Orientation
  {
    get
    {
      return (Orientation) this.GetValue(dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd.\u0023\u003DzXMV_skc\u003D);
    }
    set
    {
      this.SetValue(dje_zZ2MQ59H28BQX5B8FXAJNBH2MEKMT3QECNBWQ3U3KS29KPK8C8DHPT9FEBA2Q_ejd.\u0023\u003DzXMV_skc\u003D, (object) value);
    }
  }

  private static void \u0023\u003DzPWvFs4bMVSvB(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
  }

  private static void \u0023\u003DzcZqyvQl0j0My(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
  }

  public event PropertyChangedEventHandler PropertyChanged;
}
