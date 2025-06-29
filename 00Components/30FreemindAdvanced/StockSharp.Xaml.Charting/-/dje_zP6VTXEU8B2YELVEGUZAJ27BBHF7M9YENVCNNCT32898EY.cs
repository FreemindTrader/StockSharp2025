// Decompiled with JetBrains decompiler
// Type: -.dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
namespace StockSharp.Xaml.Charting;

[System.Windows.Markup.ContentProperty("PointMarker")]
internal abstract class dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd : 
  ContentControl,
  IRenderableSeries,
  \u0023\u003Dz5VLaAZX2bctAcuSoajSAXvZYOg6JAbLCIgQvZp9odw6FSOKg1daH3vPLNHtT2ZG4iQ\u003D\u003D,
  \u0023\u003Dz03BSxVLolBnG92GmtCJpdmgFT25iN3r_1AVHKJDMXa95,
  IXmlSerializable
{
  
  public static readonly DependencyProperty \u0023\u003DzTe_gV3cWjEp7 = DependencyProperty.Register(XXX.SSS(-539434641), typeof (int), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) 1, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzw\u0024dhNXt0Rw_W)));
  
  public static readonly DependencyProperty \u0023\u003DzHttRjYlEOUXJ = DependencyProperty.Register(XXX.SSS(-539439396), typeof (bool), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzBSmu2cMVk0Wk)));
  
  public static readonly DependencyProperty \u0023\u003DzxWzRxjX3V8Eh = DependencyProperty.Register(XXX.SSS(-539324279), typeof (int), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) -1, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzGTMdFbtr59Hr = DependencyProperty.Register(XXX.SSS(-539430144), typeof (\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzM7Y4F17SPdO43L2F59a\u0024muWj0PcD)));
  
  public static readonly DependencyProperty \u0023\u003Dz8b6MQAIE8UZn = DependencyProperty.Register(XXX.SSS(-539433813), typeof (bool), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzNhd_wqx65hB\u0024)));
  
  public static readonly DependencyProperty \u0023\u003DzIcVMwZBBZ1n3 = DependencyProperty.Register(XXX.SSS(-539440969), typeof (Color), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) Colors.White, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzCBwgUnYQspPj)));
  
  public static readonly DependencyProperty \u0023\u003DzigzndanwPIFY = DependencyProperty.Register(XXX.SSS(-539429796), typeof (Style), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzBLNrrTpkTSKCvEflkQ\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzZgWT7YttYHbwyP3zHCVW0zI\u003D = DependencyProperty.Register(XXX.SSS(-539440984), typeof (\u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.Auto, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz865pKWj8QZ5WemB7jgEn5T0\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzdr5RTntdbeN7 = DependencyProperty.Register(XXX.SSS(-539434635), typeof (bool), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzNGe3htdX6rpV = DependencyProperty.Register(XXX.SSS(-539339419), typeof (ControlTemplate), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzakQhJ14btXMjqycMlg\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzYuatdiKD80mj = DependencyProperty.Register(XXX.SSS(-539339325), typeof (\u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzCKR0IuEkziJL)));
  
  public static readonly DependencyProperty \u0023\u003Dz13qAkT\u0024eFXTPuGCUqjA\u0024Svw\u003D = DependencyProperty.Register(XXX.SSS(-539339347), typeof (ControlTemplate), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzJ71ZJC5ar9kknXOdfdQ0wMk\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzcmph5bg8kibh = DependencyProperty.Register(XXX.SSS(-539339344), typeof (DataTemplate), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DziAqnE8_\u0024SBDB = DependencyProperty.Register(XXX.SSS(-539427833), typeof (string), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) XXX.SSS(-539431487), new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzSEAakZbtZKgY = DependencyProperty.Register(XXX.SSS(-539427791), typeof (string), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) XXX.SSS(-539431487), new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003Dz2NzoEVHQO1PF = DependencyProperty.Register(XXX.SSS(-539339371), typeof (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzyfKHGV64pf8n = DependencyProperty.Register(XXX.SSS(-539339677), typeof (double), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzFGPPff\u0024vovp27QyBcA\u003D\u003D = DependencyProperty.Register(XXX.SSS(-539441023), typeof (\u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D), typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd), new PropertyMetadata((object) \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.Gaps, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  private EventHandler \u0023\u003DzDpzMqzE\u003D;
  
  private FrameworkElement \u0023\u003Dzt_Bagxe7PabOJPRSBlq7e1s\u003D;
  
  private \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT \u0023\u003DzSUYbHOVc96jukZ8sQA\u003D\u003D;
  
  private Size \u0023\u003Dz3L36rdwwPqC0 = Size.Empty;
  
  private \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D \u0023\u003DzXfO9DgaVRj7B;
  
  private bool \u0023\u003DzcqLltIfVVWk84rM\u0024DsHo97I\u003D;
  
  private \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB \u0023\u003Dz__vU_DsUk3BN;
  
  private \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB \u0023\u003DzOul945NkfDSk;
  
  private \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D \u0023\u003Dzi7jlO4\u0024jhl_0oUowGg\u003D\u003D;
  
  private int \u0023\u003DzA2CWfbM2026Lwfy5zat1jBh7qfwb5Ci\u0024\u0024A\u003D\u003D;
  
  private \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D \u0023\u003Dzq_BOEKz6TP1hJ5Vkn77QRCE\u003D;
  
  private bool \u0023\u003DzxZnVF9e_Olw9LuFVWAjGc3e_XlJ2yyzlwQ\u003D\u003D;

  protected dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzhBqSd5Scc0Hy(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003DzDpzMqzE\u003D;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003DzDpzMqzE\u003D, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003Dz_bNSX12Vpev3(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003DzDpzMqzE\u003D;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003DzDpzMqzE\u003D, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D \u0023\u003Dzu\u0024P3XgkcE7BC()
  {
    return this.\u0023\u003Dzi7jlO4\u0024jhl_0oUowGg\u003D\u003D;
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzrEoWi5uPS0Yz(
    \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D _param1)
  {
    this.\u0023\u003Dzi7jlO4\u0024jhl_0oUowGg\u003D\u003D = _param1;
  }

  public double ZeroLineY
  {
    get
    {
      return (double) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzyfKHGV64pf8n);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzyfKHGV64pf8n, (object) value);
    }
  }

  public new bool IsVisible
  {
    get
    {
      return (bool) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz8b6MQAIE8UZn);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz8b6MQAIE8UZn, (object) value);
    }
  }

  public int StrokeThickness
  {
    get
    {
      return (int) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzTe_gV3cWjEp7);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzTe_gV3cWjEp7, (object) value);
    }
  }

  public int \u0023\u003Dz_X2CHgLveOobAVErJRBV6bU\u003D()
  {
    return this.\u0023\u003DzA2CWfbM2026Lwfy5zat1jBh7qfwb5Ci\u0024\u0024A\u003D\u003D;
  }

  public void \u0023\u003Dz\u0024J\u0024cgFIdCdziztWgsC9Ijq8\u003D(int _param1)
  {
    this.\u0023\u003DzA2CWfbM2026Lwfy5zat1jBh7qfwb5Ci\u0024\u0024A\u003D\u003D = _param1;
  }

  public \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D PaletteProvider
  {
    get
    {
      return (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz2NzoEVHQO1PF);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz2NzoEVHQO1PF, (object) value);
    }
  }

  [SpecialName]
  public FrameworkElement \u0023\u003Dz4VQla1xp7uAzX0hWwB5XAZw\u003D()
  {
    return this.\u0023\u003Dzt_Bagxe7PabOJPRSBlq7e1s\u003D;
  }

  public ControlTemplate PointMarkerTemplate
  {
    get
    {
      return (ControlTemplate) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzNGe3htdX6rpV);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzNGe3htdX6rpV, (object) value);
    }
  }

  public \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT PointMarker
  {
    get
    {
      return (\u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzYuatdiKD80mj);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzYuatdiKD80mj, (object) value);
    }
  }

  public ControlTemplate RolloverMarkerTemplate
  {
    get
    {
      return (ControlTemplate) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz13qAkT\u0024eFXTPuGCUqjA\u0024Svw\u003D);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz13qAkT\u0024eFXTPuGCUqjA\u0024Svw\u003D, (object) value);
    }
  }

  public DataTemplate LegendMarkerTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzcmph5bg8kibh);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzcmph5bg8kibh, (object) value);
    }
  }

  public string YAxisId
  {
    get
    {
      return (string) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DziAqnE8_\u0024SBDB);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DziAqnE8_\u0024SBDB, (object) value);
    }
  }

  public string XAxisId
  {
    get
    {
      return (string) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzSEAakZbtZKgY);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzSEAakZbtZKgY, (object) value);
    }
  }

  public bool AntiAliasing
  {
    get
    {
      return (bool) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzdr5RTntdbeN7);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzdr5RTntdbeN7, (object) value);
    }
  }

  public \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D ResamplingMode
  {
    get
    {
      return (\u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzZgWT7YttYHbwyP3zHCVW0zI\u003D);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzZgWT7YttYHbwyP3zHCVW0zI\u003D, (object) value);
    }
  }

  [SpecialName]
  public virtual object \u0023\u003DzQavr9eonlwL7DeqLQA\u003D\u003D() => (object) null;

  public Color SeriesColor
  {
    get
    {
      return (Color) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIcVMwZBBZ1n3);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIcVMwZBBZ1n3, (object) value);
    }
  }

  public Style SelectedSeriesStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzigzndanwPIFY);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzigzndanwPIFY, (object) value);
    }
  }

  public bool IsSelected
  {
    get
    {
      return (bool) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzHttRjYlEOUXJ);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzHttRjYlEOUXJ, (object) value);
    }
  }

  public \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D DataSeries
  {
    get
    {
      return (\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzGTMdFbtr59Hr);
    }
    set
    {
      this.\u0023\u003DzEYsWXVUJ8mGX(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzGTMdFbtr59Hr, (object) value);
    }
  }

  public \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D DrawNaNAs
  {
    get
    {
      return (\u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzFGPPff\u0024vovp27QyBcA\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzFGPPff\u0024vovp27QyBcA\u003D\u003D, (object) value);
    }
  }

  [CompilerGenerated]
  [SpecialName]
  public \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D \u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D()
  {
    return this.\u0023\u003Dzq_BOEKz6TP1hJ5Vkn77QRCE\u003D;
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzHZiyrsua6EwHR04JCw\u003D\u003D(
    \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D _param1)
  {
    this.\u0023\u003Dzq_BOEKz6TP1hJ5Vkn77QRCE\u003D = _param1;
  }

  public \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB XAxis
  {
    get => this.\u0023\u003Dz__vU_DsUk3BN;
    set
    {
      this.\u0023\u003Dz__vU_DsUk3BN = dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIVa59tEZ66sJPlZMkQ\u003D\u003D(this.\u0023\u003Dz__vU_DsUk3BN, value);
    }
  }

  public \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB YAxis
  {
    get => this.\u0023\u003DzOul945NkfDSk;
    set
    {
      this.\u0023\u003DzOul945NkfDSk = dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIVa59tEZ66sJPlZMkQ\u003D\u003D(this.\u0023\u003DzOul945NkfDSk, value);
    }
  }

  private static \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB \u0023\u003DzIVa59tEZ66sJPlZMkQ\u003D\u003D(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param0,
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
  {
    if (_param0 != _param1)
    {
      \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB = _param0;
      _param0 = _param1;
      dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz2KWD3lC7hdm1(dynWmoFzgH4RlWB0lB);
      dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz2KWD3lC7hdm1(_param0);
    }
    return _param0;
  }

  [CompilerGenerated]
  [SpecialName]
  public virtual bool \u0023\u003DztPaciKMZWysZOtqEskMFjk8\u003D()
  {
    return this.\u0023\u003DzxZnVF9e_Olw9LuFVWAjGc3e_XlJ2yyzlwQ\u003D\u003D;
  }

  private void \u0023\u003DzBjZmwQKXw9Dlg8q\u0024I_nYy\u0024A\u003D(bool _param1)
  {
    this.\u0023\u003DzxZnVF9e_Olw9LuFVWAjGc3e_XlJ2yyzlwQ\u003D\u003D = _param1;
  }

  internal virtual bool \u0023\u003Dz_2ANtA3ZTojx\u00243R38A\u003D\u003D()
  {
    return this.\u0023\u003DzWcglUt8A7ABL();
  }

  protected virtual bool \u0023\u003DzWcglUt8A7ABL()
  {
    return this.DataSeries != null && this.DataSeries.get_HasValues() && this.IsVisible && this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D() != null && this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI() != null;
  }

  protected \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT \u0023\u003Dz_Y6pODRV4VXF()
  {
    return this.PointMarker ?? this.\u0023\u003DzSUYbHOVc96jukZ8sQA\u003D\u003D;
  }

  public virtual void \u0023\u003Dzmf\u0024vfR3OJQU9()
  {
    if (this.\u0023\u003Dzu\u0024P3XgkcE7BC() == null)
      return;
    this.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D>().\u0023\u003Dz5q8i9C4\u003D();
  }

  protected virtual void \u0023\u003Dzd3otebQ_ivQa()
  {
  }

  protected virtual void \u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D()
  {
  }

  protected void \u0023\u003Dzz7UraMUVt1cf<TSeriesPoint>(string _param1) where TSeriesPoint : \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSesUJ7CmHq1ptV2iVGi9XvX4<double>
  {
    if (this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI() != null && this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI().\u0023\u003DzlpVGw6E\u003D() != 0 && !(this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI().\u0023\u003Dz\u0024CeUvME\u003D(0) is \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlFgNgyQmIWzGYVO4YAqDKpiI<TSeriesPoint>))
      throw new InvalidOperationException(string.Format(XXX.SSS(-539339661), (object) ((object) this).GetType(), (object) typeof (TSeriesPoint), (object) _param1));
  }

  void \u0023\u003Dz03BSxVLolBnG92GmtCJpdmgFT25iN3r_1AVHKJDMXa95.\u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRXZtMRvnJEmQ_GxehqHfz1YVfrT4VQ\u003D\u003D(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1,
    \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D _param2)
  {
    this.\u0023\u003DzHZiyrsua6EwHR04JCw\u003D\u003D(_param2);
    if (!this.\u0023\u003Dz_2ANtA3ZTojx\u00243R38A\u003D\u003D())
      return;
    if (Size.op_Inequality(this.\u0023\u003Dz3L36rdwwPqC0, _param1.\u0023\u003Dz8DEW4l1E337F()))
    {
      this.\u0023\u003Dz_YOJQqiJZV14ntmBJA\u003D\u003D();
      this.\u0023\u003Dz3L36rdwwPqC0 = _param1.\u0023\u003Dz8DEW4l1E337F();
    }
    if (_param1.\u0023\u003Dz8DEW4l1E337F().IsEmpty || Size.op_Equality(_param1.\u0023\u003Dz8DEW4l1E337F(), new Size(1.0, 1.0)))
    {
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(XXX.SSS(-539339771), new object[1]
      {
        (object) ((object) this).GetType().Name
      });
    }
    else
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      this.\u0023\u003Dz_mrkCOu7iZTY(_param1, _param2);
      stopwatch.Stop();
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(XXX.SSS(-539339530), new object[2]
      {
        (object) ((object) this).GetType().Name,
        (object) stopwatch.ElapsedMilliseconds
      });
    }
  }

  protected abstract void \u0023\u003Dz_mrkCOu7iZTY(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1,
    \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D _param2);

  public int \u0023\u003Dz6BuO4fnhj6SX(
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> _param1,
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param2,
    double _param3)
  {
    return this.\u0023\u003Dz6BuO4fnhj6SX(_param1, _param2, (double) _param2.\u0023\u003DzlpVGw6E\u003D(), _param3);
  }

  public int \u0023\u003Dz6BuO4fnhj6SX(
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> _param1,
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param2,
    double _param3,
    double _param4)
  {
    if (_param4 < 0.0 || _param4 > 1.0)
      throw new ArgumentException(XXX.SSS(-539339560), XXX.SSS(-539339620));
    double num = _param1.\u0023\u003Dz23Oi_5A6gjXaau8ZzBLLsFfzG2_K() ? this.\u0023\u003Dz3L36rdwwPqC0.Width : this.\u0023\u003Dz3L36rdwwPqC0.Height;
    if (_param3 > 1.0)
      num = Math.Abs(_param1.\u0023\u003DzhL6gsJw\u003D(_param2.\u0023\u003Dz\u0024CeUvME\u003D(_param2.\u0023\u003DzlpVGw6E\u003D() - 1).\u0023\u003Dz2_4KSTY\u003D()) - _param1.\u0023\u003DzhL6gsJw\u003D(_param2.\u0023\u003Dz\u0024CeUvME\u003D(0).\u0023\u003Dz2_4KSTY\u003D())) / (_param3 - 1.0);
    return (int) (num * _param4);
  }

  protected virtual double \u0023\u003DzySDi0_ve2vLaE3cXlA\u003D\u003D()
  {
    double num = (double) this.GetValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzyfKHGV64pf8n);
    return !this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV() ? Math.Min(this.\u0023\u003Dz3L36rdwwPqC0.Height + 1.0, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(num)) : Math.Min(this.\u0023\u003Dz3L36rdwwPqC0.Width + 1.0, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(num));
  }

  public \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzjuB\u0024Pa8\u003D(
    Point _param1,
    bool _param2 = false)
  {
    return this.\u0023\u003DzjuB\u0024Pa8\u003D(_param1, 7.07, _param2);
  }

  public \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DznVLFa68vHPHy(
    Point _param1,
    bool _param2 = false)
  {
    this.\u0023\u003DzcqLltIfVVWk84rM\u0024DsHo97I\u003D = true;
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy = this.\u0023\u003DzjuB\u0024Pa8\u003D(_param1, 0.0, _param2);
    this.\u0023\u003DzcqLltIfVVWk84rM\u0024DsHo97I\u003D = false;
    return zldchDrVsrVyHh6WyiGy;
  }

  public virtual \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzjuB\u0024Pa8\u003D(
    Point _param1,
    double _param2,
    bool _param3 = false)
  {
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy = \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
    if (this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D() != null && this.DataSeries != null && this.DataSeries.get_HasValues())
    {
      double num = _param2 + (double) this.StrokeThickness / 2.0;
      \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D b99xo8DgCb3haWTig = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003Dzy9phceyLTfoo();
      _param1 = b99xo8DgCb3haWTig.\u0023\u003Dz8miGAzg\u003D(_param1);
      zldchDrVsrVyHh6WyiGy = this.\u0023\u003Dz__R3\u0024ryThR5H(_param1, num, _param3);
      zldchDrVsrVyHh6WyiGy.\u0023\u003Dzo2ftAfxjqC04(b99xo8DgCb3haWTig.\u0023\u003DzsTReN_n58EEf(zldchDrVsrVyHh6WyiGy.\u0023\u003DzxZfJER0dbHuS()));
      zldchDrVsrVyHh6WyiGy.\u0023\u003Dz8RUGHczgdGF57F9Tiw\u003D\u003D(b99xo8DgCb3haWTig.\u0023\u003DzsTReN_n58EEf(zldchDrVsrVyHh6WyiGy.\u0023\u003DzsTZhf2NJangnoun2zQ\u003D\u003D()));
    }
    return zldchDrVsrVyHh6WyiGy;
  }

  protected virtual \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz__R3\u0024ryThR5H(
    Point _param1,
    double _param2,
    bool _param3)
  {
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D vworRoy7o1mkbGdjE = _param3 ? (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2 : (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1;
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy = this.\u0023\u003Dzr7PRxQcLL3EF(_param1, _param2, vworRoy7o1mkbGdjE, !this.\u0023\u003DzcqLltIfVVWk84rM\u0024DsHo97I\u003D);
    if (_param3)
      zldchDrVsrVyHh6WyiGy = this.\u0023\u003DzM23In7fqsY8pIBaQVOMv1JE\u003D(_param1, zldchDrVsrVyHh6WyiGy, _param2);
    return zldchDrVsrVyHh6WyiGy;
  }

  protected double \u0023\u003Dz1runmyhnjbZYf6YRbnCukUGsf9D0YvUs2A\u003D\u003D(double _param1)
  {
    \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT hw1ki13vvK4WxOgoljkHyInT = this.\u0023\u003Dz_Y6pODRV4VXF();
    return (hw1ki13vvK4WxOgoljkHyInT == null || hw1ki13vvK4WxOgoljkHyInT.Height.\u0023\u003DzeNpB9guo_tur() ? 0 : (!hw1ki13vvK4WxOgoljkHyInT.Width.\u0023\u003DzeNpB9guo_tur() ? 1 : 0)) == 0 ? _param1 : Math.Max(hw1ki13vvK4WxOgoljkHyInT.Width, hw1ki13vvK4WxOgoljkHyInT.Height) / 2.0 + _param1;
  }

  protected virtual \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dzr7PRxQcLL3EF(
    Point _param1,
    double _param2,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param3,
    bool _param4)
  {
    if (double.IsNaN(_param2))
      throw new ArgumentException(XXX.SSS(-539339631));
    Tuple<IComparable, IComparable> tuple = this.\u0023\u003Dzs0Y0\u0024lrpmkkQ(_param1);
    Point point = this.\u0023\u003Dzop6vn0GowyiR(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV());
    double num1 = Math.Abs(this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzACwLhyc\u003D(point.X + 1.0) - this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzACwLhyc\u003D(point.X));
    double num2 = Math.Abs(this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzACwLhyc\u003D(point.Y + 1.0) - this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzACwLhyc\u003D(point.Y));
    double num3 = _param4 ? num1 / num2 : 0.0;
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D dataSeries = this.DataSeries;
    if (dataSeries.get_Count() < 2)
      _param3 = (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1;
    int index;
    if (_param3 != (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 1)
    {
      if (_param3 != (\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D) 2)
        throw new NotImplementedException();
      index = dataSeries.FindClosestLine(tuple.Item1, tuple.Item2, num3, num1 * _param2, this.DrawNaNAs);
    }
    else
    {
      if (_param2.CompareTo(0.0) == 0 && !this.DataSeries.get_IsSorted())
        _param2 = 7.07;
      index = dataSeries.FindClosestPoint(tuple.Item1, tuple.Item2, num3, num1 * _param2);
    }
    return index == -1 || !((IComparable) dataSeries.\u0023\u003DzPqsSI6C5MOOb()[index]).\u0023\u003DzutrFxOU\u003D() ? \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D : this.\u0023\u003Dz1i1kPH8eGFmc(index, _param1, _param2, tuple.Item1);
  }

  protected Tuple<IComparable, IComparable> \u0023\u003Dzs0Y0\u0024lrpmkkQ(Point _param1)
  {
    _param1 = this.\u0023\u003Dzop6vn0GowyiR(_param1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV());
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
    double num1 = xkzemsMs5tGkouk5w1.\u0023\u003DzACwLhyc\u003D(_param1.X);
    IComparable comparable1 = \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003DzsQE9ClE\u003D(num1, this.DataSeries.\u0023\u003DzwQnyySN6xaVC()[0].GetType());
    if (xkzemsMs5tGkouk5w1 is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D q9i0MXI7Qb9c1V6c0)
    {
      int num2 = (int) num1;
      comparable1 = (IComparable) q9i0MXI7Qb9c1V6c0.\u0023\u003DzWZQlXHuDrnKc(num2);
      double val1_1 = q9i0MXI7Qb9c1V6c0.\u0023\u003DzhL6gsJw\u003D((double) num2);
      int num3 = val1_1 <= _param1.X ? Math.Min(num2 + 1, this.DataSeries.\u0023\u003DzwQnyySN6xaVC().Count - 1) : Math.Max(num2 - 1, 0);
      if (num2 != num3)
      {
        double val1_2 = q9i0MXI7Qb9c1V6c0.\u0023\u003DzWZQlXHuDrnKc(num3).\u0023\u003Dzb9UCYbo\u003D();
        double val2 = q9i0MXI7Qb9c1V6c0.\u0023\u003DzhL6gsJw\u003D((double) num3);
        double num4 = Math.Abs((_param1.X - Math.Min(val1_1, val2)) / (val2 - val1_1));
        comparable1 = (IComparable) new DateTime((long) (Math.Min(val1_2, comparable1.\u0023\u003Dzb9UCYbo\u003D()) + Math.Abs(val1_2 - comparable1.\u0023\u003Dzb9UCYbo\u003D()) * num4));
      }
    }
    IComparable comparable2 = \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003DzsQE9ClE\u003D(xkzemsMs5tGkouk5w2.\u0023\u003DzACwLhyc\u003D(_param1.Y), this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[0].GetType());
    return new Tuple<IComparable, IComparable>(comparable1, comparable2);
  }

  protected virtual \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz47Cmf38KMhH_(
    int _param1)
  {
    return this.DataSeries.\u0023\u003DzDKPxuEruV71w(_param1);
  }

  protected \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz1i1kPH8eGFmc(
    int _param1,
    Point _param2,
    double _param3,
    IComparable _param4)
  {
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy = this.\u0023\u003Dz47Cmf38KMhH_(_param1);
    lock (this.DataSeries.get_SyncRoot())
      zldchDrVsrVyHh6WyiGy.\u0023\u003DzZjtwJshPYJrbgaR43Q\u003D\u003D(!this.DataSeries.get_IsSorted() || this.DataSeries.get_HasValues() && _param4.CompareTo(this.DataSeries.\u0023\u003DzwQnyySN6xaVC()[0]) >= 0 && _param4.CompareTo(this.DataSeries.\u0023\u003DzwQnyySN6xaVC()[this.DataSeries.\u0023\u003DzwQnyySN6xaVC().Count - 1]) <= 0);
    double num1 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D() is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D q9i0MXI7Qb9c1V6c0 ? q9i0MXI7Qb9c1V6c0.\u0023\u003DzhL6gsJw\u003D((double) _param1) : this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzhL6gsJw\u003D(zldchDrVsrVyHh6WyiGy.\u0023\u003DztryT5H42SVj8().\u0023\u003Dzb9UCYbo\u003D());
    double num2 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(zldchDrVsrVyHh6WyiGy.\u0023\u003Dzd9IAScWutAfJ().\u0023\u003Dzb9UCYbo\u003D());
    Point point1 = new Point(num1, num2);
    _param2 = this.\u0023\u003Dzop6vn0GowyiR(_param2, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV());
    ref \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D local1 = ref zldchDrVsrVyHh6WyiGy;
    Point point2;
    zldchDrVsrVyHh6WyiGy.\u0023\u003Dz8RUGHczgdGF57F9Tiw\u003D\u003D(point2 = this.\u0023\u003Dzop6vn0GowyiR(point1, this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV()));
    Point point3 = point2;
    local1.\u0023\u003Dzo2ftAfxjqC04(point3);
    ref \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D local2 = ref zldchDrVsrVyHh6WyiGy;
    ref \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D local3 = ref zldchDrVsrVyHh6WyiGy;
    int num3;
    bool flag = (num3 = local3.\u0023\u003DzxIOIxNIOU4djmPFSiA\u003D\u003D() | Math.Abs(num1 - _param2.X) < _param3 ? 1 : 0) != 0;
    local3.\u0023\u003DzZjtwJshPYJrbgaR43Q\u003D\u003D(num3 != 0);
    int num4 = flag ? 1 : 0;
    local2.\u0023\u003DzkNMVgQ88lfxP(num4 != 0);
    double num5 = this.XAxis == null || !this.XAxis.get_IsPolarAxis() ? \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzwAiTZQA\u003D(point1, _param2) : \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzYJ5BoptbjSzj(point1, _param2);
    zldchDrVsrVyHh6WyiGy.\u0023\u003Dzn3o1RS9wuET8(num5 < _param3);
    return zldchDrVsrVyHh6WyiGy;
  }

  protected virtual \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzM23In7fqsY8pIBaQVOMv1JE\u003D(
    Point _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2,
    double _param3)
  {
    if (!_param2.\u0023\u003DzMeGSfVE\u003D())
    {
      int num1 = _param2.\u0023\u003DzSkvCFWUKQ7Fw();
      int num2 = _param2.\u0023\u003DzSkvCFWUKQ7Fw() + 1;
      if (num2 >= 0 && num2 < this.DataSeries.get_Count())
      {
        Tuple<double, double> tuple = this.\u0023\u003Dz0iTacSsYe_3qOn01Ow\u003D\u003D(num1, new Func<int, double>(this.\u0023\u003DzsQnYC1Zzsuu1XtglKb\u0024CvGExdj4jADVSvA\u003D\u003D));
        _param2 = this.\u0023\u003DzM23In7fqsY8pIBaQVOMv1JE\u003D(_param1, _param2, _param3, tuple, (Tuple<double, double>) null);
      }
    }
    return _param2;
  }

  protected \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003DzM23In7fqsY8pIBaQVOMv1JE\u003D(
    Point _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2,
    double _param3,
    Tuple<double, double> _param4,
    Tuple<double, double> _param5)
  {
    Tuple<IComparable, IComparable> tuple = this.\u0023\u003Dzs0Y0\u0024lrpmkkQ(_param1);
    Point point1 = new Point(tuple.Item1.\u0023\u003Dzb9UCYbo\u003D(), tuple.Item2.\u0023\u003Dzb9UCYbo\u003D());
    int index = _param2.\u0023\u003DzSkvCFWUKQ7Fw() + 1;
    Point point2 = new Point(_param2.\u0023\u003DztryT5H42SVj8().\u0023\u003Dzb9UCYbo\u003D(), _param4.Item1);
    Point point3 = point2;
    Point point4 = new Point(((IComparable) this.DataSeries.\u0023\u003DzwQnyySN6xaVC()[index]).\u0023\u003Dzb9UCYbo\u003D(), _param4.Item2);
    Point point5 = new Point();
    Point point6 = new Point();
    if (_param5 != null)
    {
      point5 = new Point(point2.X, _param5.Item1);
      point6 = new Point(point4.X, _param5.Item2);
    }
    if ((this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV() ? Math.Abs(_param1.Y - _param2.\u0023\u003DzxZfJER0dbHuS().Y) : Math.Abs(_param1.X - _param2.\u0023\u003DzxZfJER0dbHuS().X)) >= 2.0)
    {
      point2 = this.\u0023\u003DzCb_RLOvRccDNEzscA1idGz0\u003D(point1, point2, point4);
      if (_param5 != null)
        point5 = this.\u0023\u003DzCb_RLOvRccDNEzscA1idGz0\u003D(point1, point5, point6);
      _param2.\u0023\u003Dzo2ftAfxjqC04(this.\u0023\u003Dz1TAi3SadJ4Un(point2.X, point2.Y));
    }
    if (!_param2.\u0023\u003Dzmh1LiTa467ce())
      _param2.\u0023\u003Dzn3o1RS9wuET8(this.\u0023\u003DzRf_Fn6mPWZva(_param1, _param2, _param3, point3, point4));
    _param2.\u0023\u003Dz2Iv\u0024sxQuGDBR(\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003DzsQE9ClE\u003D(point2.X, this.DataSeries.\u0023\u003DzwQnyySN6xaVC()[0].GetType()));
    _param2.\u0023\u003DzBswzhzuQHrrX(\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003DzsQE9ClE\u003D(point2.Y, this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[0].GetType()));
    if (_param5 != null)
      _param2.\u0023\u003Dz3JT1kQLA9WwW(\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003DzsQE9ClE\u003D(point5.Y, this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[0].GetType()));
    return _param2;
  }

  protected Tuple<double, double> \u0023\u003Dz0iTacSsYe_3qOn01Ow\u003D\u003D(
    int _param1,
    Func<int, double> _param2)
  {
    double num = _param2(_param1);
    ++_param1;
    double d = _param2(_param1);
    if (this.DrawNaNAs == \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.ClosedLines)
    {
      for (; double.IsNaN(d) && _param1 < this.DataSeries.get_Count() - 1; d = _param2(_param1))
        ++_param1;
    }
    return new Tuple<double, double>(num, d);
  }

  private Point \u0023\u003DzCb_RLOvRccDNEzscA1idGz0\u003D(
    Point _param1,
    Point _param2,
    Point _param3)
  {
    double x1 = _param2.X;
    double y1 = _param2.Y;
    double x2 = _param3.X;
    double y2 = _param3.Y;
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003Dz9tfGzYXjAfsx(ref x1, ref x2, ref y1, ref y2);
    double num1 = (_param1.X - x1) / (x2 - x1);
    if (num1 > 1.0)
      num1 = 1.0;
    else if (num1 < 0.0)
      num1 = 0.0;
    double num2 = x1 + (x2 - x1) * num1;
    if (!this.\u0023\u003DzLDwF88FLhD9n2pkW3Q\u003D\u003D())
      y1 += (y2 - y1) * num1;
    return new Point(num2, y1);
  }

  protected Point \u0023\u003Dz1TAi3SadJ4Un(double _param1, double _param2)
  {
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
    if (xkzemsMs5tGkouk5w1 is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D q9i0MXI7Qb9c1V6c0)
      _param1 = (double) q9i0MXI7Qb9c1V6c0.\u0023\u003DzFk6sufr\u0024co4e((IComparable) _param1);
    return this.\u0023\u003Dzop6vn0GowyiR(new Point(xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(_param1), xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(_param2)), this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV());
  }

  protected virtual bool \u0023\u003DzRf_Fn6mPWZva(
    Point _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2,
    double _param3,
    Point _param4,
    Point _param5)
  {
    Point point1 = this.\u0023\u003Dz1TAi3SadJ4Un(_param4.X, _param4.Y);
    Point point2 = this.\u0023\u003Dz1TAi3SadJ4Un(_param5.X, _param5.Y);
    bool flag1 = false;
    bool flag2;
    if (this.\u0023\u003DzLDwF88FLhD9n2pkW3Q\u003D\u003D())
    {
      int index = _param2.\u0023\u003DzSkvCFWUKQ7Fw() - 1;
      if (index >= 0 && index < this.DataSeries.get_Count())
      {
        Point point3 = new Point(((IComparable) this.DataSeries.\u0023\u003DzwQnyySN6xaVC()[index]).\u0023\u003Dzb9UCYbo\u003D(), _param4.Y);
        Point point4 = this.\u0023\u003Dz1TAi3SadJ4Un(point3.X, point3.Y);
        Point point5 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV() ? new Point(point4.X, point1.Y) : new Point(point1.X, point4.Y);
        flag1 = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzAX\u0024lol1aDgYQ(_param1, point5, point1, true) < _param3;
      }
      Point point6 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV() ? new Point(point1.X, point2.Y) : new Point(point2.X, point1.Y);
      flag2 = ((flag1 ? 1 : 0) | (\u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzAX\u0024lol1aDgYQ(_param1, point1, point6, true) < _param3 ? 1 : (\u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzAX\u0024lol1aDgYQ(_param1, point6, point2, true) < _param3 ? 1 : 0))) != 0;
    }
    else
      flag2 = \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkzhZW4NvR\u00246A4_TU938\u003D.\u0023\u003DzAX\u0024lol1aDgYQ(_param1, point1, point2, true) < _param3;
    return flag2;
  }

  protected virtual \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz1SLEyANHenbwANn\u0024\u0024w\u003D\u003D(
    Point _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2,
    double _param3)
  {
    if (this.DataSeries != null && this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D() != null)
    {
      bool flag = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzDoU1CJhSUWFV();
      double num1 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(this.\u0023\u003DzPADldLd\u0024JydfjzvZWw\u003D\u003D(_param2));
      double num2 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzhL6gsJw\u003D(this.\u0023\u003DzWRZyMoPrv0mW7TClKA\u003D\u003D(_param2));
      if (num2 < num1)
        \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref num2, ref num1);
      double num3 = (double) this.StrokeThickness * 0.5 + _param3;
      double num4 = num1 - num3;
      double num5 = num2 + num3;
      double num6 = this.\u0023\u003DzcaynwI5AMDdY(_param2) * 0.5 + num3;
      double num7 = flag ? _param2.\u0023\u003DzxZfJER0dbHuS().Y : _param2.\u0023\u003DzxZfJER0dbHuS().X;
      Point point1 = new Point(num7 - num6, num5);
      Point point2 = new Point(num7 + num6, num4);
      Rect rect = new Rect(this.\u0023\u003Dzop6vn0GowyiR(point1, flag), this.\u0023\u003Dzop6vn0GowyiR(point2, flag));
      _param2.\u0023\u003Dzn3o1RS9wuET8(this.\u0023\u003DzO6mZ0OGu\u0024l6W(_param1, rect, _param2));
    }
    return _param2;
  }

  protected virtual double \u0023\u003DzcaynwI5AMDdY(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    return 0.0;
  }

  protected virtual double \u0023\u003DzPADldLd\u0024JydfjzvZWw\u003D\u003D(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    return 0.0;
  }

  protected virtual double \u0023\u003DzWRZyMoPrv0mW7TClKA\u003D\u003D(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    return 0.0;
  }

  protected virtual bool \u0023\u003DzO6mZ0OGu\u0024l6W(
    Point _param1,
    Rect _param2,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param3)
  {
    return _param2.Contains(_param1);
  }

  protected static bool \u0023\u003Dz99srMqSdWO6y(double _param0, double _param1, double _param2)
  {
    if (_param1 > _param2)
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzMv8ALVs\u003D(ref _param1, ref _param2);
    return _param0 >= _param1 && _param0 <= _param2;
  }

  internal \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D \u0023\u003DzZZbJdAS6fDJ\u0024(
    Point _param1)
  {
    return this.\u0023\u003DzZZbJdAS6fDJ\u0024(this.\u0023\u003DzjuB\u0024Pa8\u003D(_param1));
  }

  public virtual \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D \u0023\u003DzZZbJdAS6fDJ\u0024(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    return \u0023\u003Dzgg5QOmcWitJriAsXqwM_mmKL7LRAQHeU0CkDEWjOUd11EjbkZ3YobQ0\u003D.\u0023\u003DzZZbJdAS6fDJ\u0024(this, _param1);
  }

  public virtual \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003Dzq3MgExWxza1L()
  {
    return this.DataSeries.get_XRange();
  }

  public \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzxNQHuqrEvxH2(
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param1)
  {
    return this.\u0023\u003DzxNQHuqrEvxH2(_param1, false);
  }

  public virtual \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzxNQHuqrEvxH2(
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param1,
    bool _param2)
  {
    return this.DataSeries.\u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(_param1, _param2);
  }

  public virtual \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D \u0023\u003DzVAnbwOJn98Ya(
    \u0023\u003DzR2x48Sho4AxfV9DSAxG8OQ2rGx6KyfAREQ\u003D\u003D _param1)
  {
    return _param1;
  }

  public bool \u0023\u003DzVxrZQ3k9ZBGJ(
    \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy7z1KtGY\u0024N_H_U3tz7I\u003D _param1)
  {
    bool flag = true;
    switch (_param1)
    {
      case (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy7z1KtGY\u0024N_H_U3tz7I\u003D) 0:
        flag = dje_zWLGYTTT5DUFM55EFRPBBAZMZXPXLXXK6XF479D6U5R7JHZFQWRSVK3MYA6SQ_ejd.GetIncludeSeries((DependencyObject) this);
        break;
      case (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy7z1KtGY\u0024N_H_U3tz7I\u003D) 1:
        flag = dje_zN29EKU2DQXHTNKQ8X5JHMLEL2WYLBY8R3YWYA24CH8SZCCZ_ejd.GetIncludeSeries((DependencyObject) this);
        break;
      case (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy7z1KtGY\u0024N_H_U3tz7I\u003D) 2:
        flag = dje_zHR7NJDL95STD4SWVG26WR43JGQLXY8NHRZ7Q26829WGD2MZ_ejd.GetIncludeSeries((DependencyObject) this);
        break;
      case (\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy7z1KtGY\u0024N_H_U3tz7I\u003D) 3:
        flag = dje_zW53QLLBXPN8WXR6G6DUDNUP6W2EZEB9SXV35FLJUAR85WHV6RM45V_ejd.GetIncludeSeries((DependencyObject) this);
        break;
    }
    return flag;
  }

  protected virtual void \u0023\u003DznYxDSPc3ewIVoqnDfn0cVVg\u003D()
  {
    this.\u0023\u003Dzmf\u0024vfR3OJQU9();
  }

  protected virtual void \u0023\u003Dz_YOJQqiJZV14ntmBJA\u003D\u003D()
  {
  }

  private void \u0023\u003DzAxm3XXgpqYph(EventArgs _param1)
  {
    EventHandler zDpzMqzE = this.\u0023\u003DzDpzMqzE\u003D;
    if (zDpzMqzE == null)
      return;
    zDpzMqzE((object) this, _param1);
  }

  protected double \u0023\u003DzNfVFwxaLW3jC(
    \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZaScol6ORWm4rPwbPViMZ4rNexJsSmCJpOM\u003D _param1)
  {
    int num = _param1.\u0023\u003DzDoU1CJhSUWFV() ? 1 : 0;
    bool flag = _param1.\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzTlJPop1Rus3dbxPf\u0024iJeyAQ\u003D();
    return (num != 0 ? Math.PI / 2.0 : 0.0) + (flag ? Math.PI : 0.0);
  }

  protected Point \u0023\u003Dzop6vn0GowyiR(float _param1, float _param2, bool _param3)
  {
    return \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26h0hvlV4LfsI5sS6QMD39it_JzJq2Auui18\u003D.\u0023\u003Dzop6vn0GowyiR(_param1, _param2, _param3);
  }

  protected Point \u0023\u003Dzop6vn0GowyiR(Point _param1, bool _param2)
  {
    return \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26h0hvlV4LfsI5sS6QMD39it_JzJq2Auui18\u003D.\u0023\u003Dzop6vn0GowyiR(_param1, _param2);
  }

  protected static void \u0023\u003Dzmf\u0024vfR3OJQU9(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd ls4St64EqzfbaEjd))
      return;
    ls4St64EqzfbaEjd.\u0023\u003Dzmf\u0024vfR3OJQU9();
  }

  protected virtual void \u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1,
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param2)
  {
    \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D ii8u0KoV3jCaMdYpfetQ = this.\u0023\u003DzoXzc48\u0024TAMxP();
    if (ii8u0KoV3jCaMdYpfetQ != null)
    {
      ii8u0KoV3jCaMdYpfetQ.\u0023\u003Dzf72QDPKj6m\u0024z(_param1);
      ii8u0KoV3jCaMdYpfetQ.\u0023\u003DzbsWonVbyfEPS(_param2);
    }
    this.\u0023\u003DzXfO9DgaVRj7B = _param2;
    if (this.IsVisible)
      this.\u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D();
    this.\u0023\u003Dzmf\u0024vfR3OJQU9();
  }

  protected internal \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D \u0023\u003DzoXzc48\u0024TAMxP()
  {
    return this.\u0023\u003Dzu\u0024P3XgkcE7BC() == null ? (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) null : this.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D>();
  }

  protected virtual void \u0023\u003Dz2KqJz\u00247bE2vLRKrYBA\u003D\u003D()
  {
    this.\u0023\u003Dzt_Bagxe7PabOJPRSBlq7e1s\u003D = (FrameworkElement) dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd.\u0023\u003DzBv1vB\u0024LEKSF4(this.RolloverMarkerTemplate, (object) this);
  }

  public XmlSchema GetSchema() => (XmlSchema) null;

  public virtual void ReadXml(XmlReader _param1)
  {
    if (_param1.MoveToContent() != XmlNodeType.Element)
      return;
    \u0023\u003DzD8wDhZ3givcSnsIhbrLbuMG1x9yc5uc0Gequ88XBvceJ8sddSTrltYg\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz4EJs3pc\u003D((IRenderableSeries) this, _param1);
  }

  public virtual void WriteXml(XmlWriter _param1)
  {
    \u0023\u003DzD8wDhZ3givcSnsIhbrLbuMG1x9yc5uc0Gequ88XBvceJ8sddSTrltYg\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz7SZ\u0024Lrw\u003D((IRenderableSeries) this, _param1);
  }

  private static void \u0023\u003Dzw\u0024dhNXt0Rw_W(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzlTskcr4\u003D((IComparable) (int) _param1.NewValue, XXX.SSS(-539434641)).\u0023\u003DzIR3Z_Ken7pfcXCwNTw\u003D\u003D((IComparable) 0);
    dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9(_param0, _param1);
  }

  private static void \u0023\u003DzCBwgUnYQspPj(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd ls4St64EqzfbaEjd))
      return;
    ls4St64EqzfbaEjd.\u0023\u003Dzd3otebQ_ivQa();
    ls4St64EqzfbaEjd.\u0023\u003Dzmf\u0024vfR3OJQU9();
  }

  private static void \u0023\u003DzBLNrrTpkTSKCvEflkQ\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd ls4St64EqzfbaEjd) || !ls4St64EqzfbaEjd.IsSelected || ls4St64EqzfbaEjd.SelectedSeriesStyle == null)
      return;
    dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzxKKlOa0jEDmy(ls4St64EqzfbaEjd);
  }

  private static void \u0023\u003DzBSmu2cMVk0Wk(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd ls4St64EqzfbaEjd = _param0 as dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd;
    bool newValue = (bool) _param1.NewValue;
    if (ls4St64EqzfbaEjd == null || (bool) _param1.OldValue == newValue)
      return;
    dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzxKKlOa0jEDmy(ls4St64EqzfbaEjd);
    ls4St64EqzfbaEjd.\u0023\u003DzAxm3XXgpqYph(EventArgs.Empty);
  }

  private static void \u0023\u003DzNhd_wqx65hB\u0024(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd ls4St64EqzfbaEjd = _param0 as dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd;
    bool newValue = (bool) _param1.NewValue;
    if (ls4St64EqzfbaEjd == null || (bool) _param1.OldValue == newValue)
      return;
    dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9(_param0, _param1);
  }

  private static void \u0023\u003DzxKKlOa0jEDmy(
    dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd _param0)
  {
    _param0.\u0023\u003Dzjqa\u00243wA\u003D(_param0.IsSelected ? _param0.SelectedSeriesStyle : (Style) _param0.GetValue(\u0023\u003Dzgg5QOmcWitJriAsXqwM_mmKL7LRAQHeU0CkDEWjOUd11EjbkZ3YobQ0\u003D.\u0023\u003Dz7aV0h3kX1zh\u0024));
    _param0.\u0023\u003Dzd3otebQ_ivQa();
    _param0.\u0023\u003Dzmf\u0024vfR3OJQU9();
  }

  private static void \u0023\u003Dz865pKWj8QZ5WemB7jgEn5T0\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd ls4St64EqzfbaEjd))
      return;
    ls4St64EqzfbaEjd.\u0023\u003DznYxDSPc3ewIVoqnDfn0cVVg\u003D();
  }

  private static void \u0023\u003DzJ71ZJC5ar9kknXOdfdQ0wMk\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd ls4St64EqzfbaEjd))
      return;
    ls4St64EqzfbaEjd.\u0023\u003Dz2KqJz\u00247bE2vLRKrYBA\u003D\u003D();
    ls4St64EqzfbaEjd.\u0023\u003Dzmf\u0024vfR3OJQU9();
  }

  private static void \u0023\u003DzakQhJ14btXMjqycMlg\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd ls4St64EqzfbaEjd = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) _param0;
    ls4St64EqzfbaEjd.\u0023\u003DzSUYbHOVc96jukZ8sQA\u003D\u003D = ls4St64EqzfbaEjd.\u0023\u003DztCDogY9Bv4Ig((ControlTemplate) _param1.NewValue, (object) ls4St64EqzfbaEjd);
    ls4St64EqzfbaEjd.\u0023\u003Dzmf\u0024vfR3OJQU9();
  }

  private \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT \u0023\u003DztCDogY9Bv4Ig(
    ControlTemplate _param1,
    object _param2)
  {
    \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT hw1ki13vvK4WxOgoljkHyInT = (\u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT) null;
    if (_param1 != null)
    {
      dje_z89FJ3UY6BDUNEYRAHTXB4XHX3L65Z9F7D67T9VT5YAN2QMSJAGHFX_ejd yaN2QmsjaghfxEjd = dje_zCMGSMRQ8TGDWNMJYDMRJLTYQP85CBX8PUB3PLKHRY3KMMPUZVUKR4ZNETUTJMMVVAS_ejd.\u0023\u003DzBv1vB\u0024LEKSF4(_param1, _param2).\u0023\u003DzD_LPZWnSn3m1<dje_z89FJ3UY6BDUNEYRAHTXB4XHX3L65Z9F7D67T9VT5YAN2QMSJAGHFX_ejd>();
      if (yaN2QmsjaghfxEjd == null)
      {
        \u0023\u003DzAuXtmwo_UFdzWVVSiImlM\u002467cQAcrK8Ri9x59UQHE4_ZyklbLQ\u003D\u003D ri9x59UqhE4ZyklbLq = new \u0023\u003DzAuXtmwo_UFdzWVVSiImlM\u002467cQAcrK8Ri9x59UQHE4_ZyklbLQ\u003D\u003D();
        ri9x59UqhE4ZyklbLq.PointMarkerTemplate = _param1;
        ri9x59UqhE4ZyklbLq.DataContext = _param2;
        yaN2QmsjaghfxEjd = (dje_z89FJ3UY6BDUNEYRAHTXB4XHX3L65Z9F7D67T9VT5YAN2QMSJAGHFX_ejd) ri9x59UqhE4ZyklbLq;
      }
      hw1ki13vvK4WxOgoljkHyInT = (\u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT) yaN2QmsjaghfxEjd;
    }
    return hw1ki13vvK4WxOgoljkHyInT;
  }

  private static void \u0023\u003DzCKR0IuEkziJL(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd ls4St64EqzfbaEjd = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) _param0;
    if (_param1.NewValue is dje_z89FJ3UY6BDUNEYRAHTXB4XHX3L65Z9F7D67T9VT5YAN2QMSJAGHFX_ejd newValue)
    {
      newValue.DataContext = (object) ls4St64EqzfbaEjd;
      if (newValue.Parent is dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd parent)
        parent.Content = (object) null;
      ls4St64EqzfbaEjd.Content = (object) newValue;
    }
    ls4St64EqzfbaEjd.\u0023\u003Dzmf\u0024vfR3OJQU9();
  }

  internal double \u0023\u003Dz7qQnbobKkea3() => 7.07;

  Style IRenderableSeries.\u0023\u003DzUJpBz2W8IzAtBIqVtQXHBxp6vbyZAM3jtrSiCk74BV1DuKLm1V1cPvuYOQQ3vfm3CE6f2xA\u003D()
  {
    return this.Style;
  }

  void IRenderableSeries.\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEewBN21r93mO2zsz2dRPeFYWJbEJRkUuJj8DgmKkp4Eq5v4nmubs\u003D(
    Style _param1)
  {
    this.Style = _param1;
  }

  object IRenderableSeries.\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEewBN21r93mO2zsz2dRPeFYUUfzfIMBKYp9sEsF_gP\u0024Pd_00evAM\u003D()
  {
    return this.DataContext;
  }

  void IRenderableSeries.\u0023\u003DzupHrUO0UFO07vWyNRguf_0VZ\u0024qSpKkKiNQBNd6W0uxIwj8NLZZkxSgEI5esBhFlrGGFudoE\u003D(
    object _param1)
  {
    this.DataContext = _param1;
  }

  double \u0023\u003Dz03BSxVLolBnG92GmtCJpdmgFT25iN3r_1AVHKJDMXa95.\u0023\u003DzEa5ACpOap4rFIaHj5p9yfH70ARbSZe0FxQ0q\u00240QfMpnPN_04zQ\u003D\u003D()
  {
    return this.Width;
  }

  void \u0023\u003Dz03BSxVLolBnG92GmtCJpdmgFT25iN3r_1AVHKJDMXa95.\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI4gQW2p5XnENj22E0ug7VJ0RyC3hMw\u003D\u003D(
    double _param1)
  {
    this.Width = _param1;
  }

  double \u0023\u003Dz03BSxVLolBnG92GmtCJpdmgFT25iN3r_1AVHKJDMXa95.\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ZE9YOd5sMhl\u0024Z\u0024xSADAZlqXzWzlvA\u003D\u003D()
  {
    return this.Height;
  }

  void \u0023\u003Dz03BSxVLolBnG92GmtCJpdmgFT25iN3r_1AVHKJDMXa95.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntaAE_X2h6PHbsMnRBK9cYE8yLrOBvg\u003D\u003D(
    double _param1)
  {
    this.Height = _param1;
  }

  private double \u0023\u003DzsQnYC1Zzsuu1XtglKb\u0024CvGExdj4jADVSvA\u003D\u003D(int _param1)
  {
    return ((IComparable) this.DataSeries.\u0023\u003DzPqsSI6C5MOOb()[_param1]).\u0023\u003Dzb9UCYbo\u003D();
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz7qOdpi4\u003D();

    internal void \u0023\u003DzM7Y4F17SPdO43L2F59a\u0024muWj0PcD(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) _param1).\u0023\u003DzAVP20qah0DlKrctPXw\u003D\u003D((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) _param2.OldValue, (\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) _param2.NewValue);
    }
  }
}
