// Decompiled with JetBrains decompiler
// Type: -.dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Xaml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable enable
namespace StockSharp.Xaml.Charting;

[TemplatePart(Name = "PART_GridLinesArea", Type = typeof (dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLTCNAKZHF2LCBZNSWLDVPYTWWEKD4_ejd))]
[TemplatePart(Name = "PART_LeftAxisArea", Type = typeof (dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd))]
[TemplatePart(Name = "PART_TopAxisArea", Type = typeof (dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd))]
[TemplatePart(Name = "PART_RightAxisArea", Type = typeof (dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd))]
[TemplatePart(Name = "PART_BottomAxisArea", Type = typeof (dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd))]
[TemplatePart(Name = "PART_AnnotationsOverlaySurface", Type = typeof (dje_zL3F4JFPFYA57TDFR6E5RGMBJ4VYTG4UGR3GC8GM6BQ3B9PA_ejd))]
[TemplatePart(Name = "PART_AnnotationsUnderlaySurface", Type = typeof (dje_zL3F4JFPFYA57TDFR6E5RGMBJ4VYTG4UGR3GC8GM6BQ3B9PA_ejd))]
[TemplatePart(Name = "PART_ChartAdornerLayer", Type = typeof (Canvas))]
internal class dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd : 
  dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd,
  \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D,
  \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv1ei5a44WdHi6c16UXGWhmY1mMHOZA\u003D\u003D,
  \u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D,
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe06Do2pQ7ReqT8Ks0apzs3KdsLXgXg\u003D\u003D,
  \u0023\u003DzUib3SzczDtLU7txM4YiSeNZjP0NRThUE6PRgmDMkI3UwPa6FIQ\u003D\u003D,
  IDisposable,
  IXmlSerializable
{
  
  public static readonly 
  #nullable disable
  DependencyProperty \u0023\u003DzOEM9rkU52IaaaYckycathxI\u003D = DependencyProperty.Register("", typeof (bool), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003Dz8254dnHLdRCy = DependencyProperty.Register("", typeof (bool), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DzTX0a9G8VrfoD = DependencyProperty.Register("", typeof (ICommand), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DzzGCMRya8OoUTRsROAEXfFVg\u003D = DependencyProperty.Register("", typeof (ICommand), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DztnODn3Lbw9gQ = DependencyProperty.Register("", typeof (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz2xH_KOTJk1FH)));
  
  public static readonly DependencyProperty \u0023\u003Dz1Eyckd4j0Nvq = DependencyProperty.Register("", typeof (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzUYXUmgjXHZra)));
  
  public static readonly DependencyProperty \u0023\u003Dzp4pbuj2W1yJe = DependencyProperty.Register("", typeof (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzw83elPE6IaNqQIYgfw\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzDqajrUCXt2L8 = DependencyProperty.Register("", typeof (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz6cV0sTdoa0aXB7oEvg\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzCQBWGrk\u003D = DependencyProperty.Register("", typeof (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzTwHOANN3ITOI)));
  
  public static readonly DependencyProperty \u0023\u003Dzrq3jmpc2lTWmgU1xYQ\u003D\u003D = DependencyProperty.Register("", typeof (bool), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003Dzj87lYSja1JrB = DependencyProperty.Register("", typeof (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzD3CxC6LQYAec)));
  
  public static readonly DependencyProperty \u0023\u003Dz9lYVM5L3vWhgRCkiGg\u003D\u003D = DependencyProperty.Register("", typeof (ItemsPanelTemplate), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DzC8eGbYU38ZOhQsYmkg\u003D\u003D = DependencyProperty.Register("", typeof (ItemsPanelTemplate), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DzUKDFpV7tPYmw\u0024i2jGg\u003D\u003D = DependencyProperty.Register("", typeof (ItemsPanelTemplate), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DzkPeQninQ1wHrCZ6TRw\u003D\u003D = DependencyProperty.Register("", typeof (ItemsPanelTemplate), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DzhAlxcjWzGbAtV5gIOEJX7LQ\u003D = DependencyProperty.Register("", typeof (ItemsPanelTemplate), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null));
  
  public static readonly DependencyProperty \u0023\u003Dz3Igx8jH2JowbGxnbosZfY9g\u003D = DependencyProperty.Register("", typeof (ItemsPanelTemplate), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null));
  
  public static readonly DependencyProperty \u0023\u003DzKIR3fEiw\u0024nx0X6LDLQ\u003D\u003D = DependencyProperty.Register("", typeof (Style), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzYXVDCElLYMeX)));
  
  public static readonly DependencyProperty \u0023\u003Dz9HyEohEm8\u0024tx = DependencyProperty.Register("", typeof (Style), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzYXVDCElLYMeX)));
  
  public static readonly DependencyProperty \u0023\u003Dzda5ZTgpF7nPj_QX8WWDrVmQ\u003D = DependencyProperty.Register("", typeof (ObservableCollection<IRenderableSeries>), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzqAA3vS5ME5QQOVXt1JQV8cO54LGA)));
  
  public static readonly DependencyProperty \u0023\u003DzQFsR0tDPWyoiatb3P7odRD8\u003D = DependencyProperty.Register("", typeof (ObservableCollection<IRenderableSeries>), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003Dz8o0yiN0I55Gm = DependencyProperty.Register("", typeof (\u0023\u003Dz3RRntx4pzkd854dIVpLK6Ww8ODIV2zPrRw\u003D\u003D), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz2I6m3INvNdYt)));
  
  public static readonly DependencyProperty \u0023\u003Dzhi6ewyq0WHcz = DependencyProperty.Register("", typeof (ObservableCollection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzURlSYmINpP\u0024HWseZpg\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzgbpAybKiYbKbKG238g\u003D\u003D = DependencyProperty.Register("", typeof (bool), typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzEXdV0ADnLt07AffGX3qcEdc\u003D)));
  
  private dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLTCNAKZHF2LCBZNSWLDVPYTWWEKD4_ejd \u0023\u003Dz21FrxnEmUmBR;
  
  private dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003Dz0YK5mfh0G_wP;
  
  private dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003DzGFGsUr_xxE4H;
  
  private dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003DzK\u0024t4oL\u0024CAaKU;
  
  private dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003DztKgn6GxCcL\u0024B;
  
  private dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003Dz1mGpFN8tTa5PB1b5eA\u003D\u003D;
  
  private dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003DzkW\u0024qJVeNKX1VtemTog\u003D\u003D;
  
  private \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D \u0023\u003DzlisSOG7yPz_K;
  
  private \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyNMn5sYJZKzXDppYgrIuwPB \u0023\u003DzKPDuUZmOc92KysMGPQ\u003D\u003D;
  
  private bool \u0023\u003DzwRf5PdbT802y;
  
  private dje_zL3F4JFPFYA57TDFR6E5RGMBJ4VYTG4UGR3GC8GM6BQ3B9PA_ejd \u0023\u003DzJCSLrMbWu3xb\u00240pkXO0DnxY\u003D;
  
  private dje_zL3F4JFPFYA57TDFR6E5RGMBJ4VYTG4UGR3GC8GM6BQ3B9PA_ejd \u0023\u003Dzm\u0024RjME9m9HW8iNSk3pzjUqs\u003D;
  
  private Canvas \u0023\u003Dzn_X_okoYEhwx;
  
  private \u0023\u003DzYB09msiytIDFpDsyaHpANNcjrPNql900yccNyuZHBUW6 \u0023\u003DzMZpCUE8eL2X8;
  
  private readonly object \u0023\u003DzxztcSMfDuTst = new object();
  
  private readonly HashSet<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D> \u0023\u003DzL79ssDGYe7GX = new HashSet<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D>();
  
  private dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzym7l7vrt6xywpseFzgnpRX8\u003D \u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D;
  
  private EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D> \u0023\u003Dzpet4TiFkmcjB;
  
  private EventHandler \u0023\u003Dz7YsCgRH0e3NJ7eTEsg\u003D\u003D;
  
  private EventHandler \u0023\u003DzEA6vcLlqH_FFzckuMg\u003D\u003D;
  
  private EventHandler \u0023\u003DzqsL83n7r4oZ4;
  
  private static int \u0023\u003DziUTIXAQ\u003D;
  
  private static char \u0023\u003DzndAvyl4\u003D = 'a';
  
  private string \u0023\u003Dz\u0024RzuU0A\u003D = dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzndAvyl4\u003D++.ToString();
  
  private \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUtzzzd5rSNXl95sF5MghysRDMZyklVKg61SC2QL8 \u0023\u003DzBF9EFrE8iOBKBpnp2YeZjCs\u003D;
  
  private \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKg1uYp9C5FyfAbaz1_Vr5UELTst58YqUlllvt2EY65UXJw\u003D\u003D \u0023\u003DzUpDNoiuyFkQgKnG1rLeDiyFufOOLPECfpQ\u003D\u003D;

  public dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd);
    this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D = new dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzym7l7vrt6xywpseFzgnpRX8\u003D(this);
    this.SelectedRenderableSeries = new ObservableCollection<IRenderableSeries>();
    this.SetCurrentValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzda5ZTgpF7nPj_QX8WWDrVmQ\u003D, (object) new ObservableCollection<IRenderableSeries>());
    this.SetCurrentValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzp4pbuj2W1yJe, (object) new \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D());
    this.SetCurrentValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzDqajrUCXt2L8, (object) new \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D());
    this.SetCurrentValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzCQBWGrk\u003D, (object) new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D());
    this.SetCurrentValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz8o0yiN0I55Gm, (object) new \u0023\u003DzmAi_JN5raoSBYo9w2IEI_2WWX\u0024OoiqaMIQ\u003D\u003D());
    this.SetCurrentValue(dje_z45LS63Y8UMJ9EYX47PT4TZDDE9QEW5TMVSKL8UT3BP29L4PX7GMR67GCPGCQ_ejd.\u0023\u003DzZlWtNzRQ4OYQ, (object) new dje_zFYCV8LXGWR39M2R5WW2FCVKF529R9Q6JAUMT5WGC7MM4FFDF2V4HQQMUEFPET6GT7RXT7XFY_ejd());
    this.ZoomExtentsCommand = (ICommand) new DelegateCommand(new Action(this.\u0023\u003Dzn72LMZ0738BY));
    this.AnimateZoomExtentsCommand = (ICommand) new DelegateCommand(new Action<object>(this.\u0023\u003DzS2ufUgCmj4iLhzxXcG9wL_0\u003D));
  }

  internal dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd(
    \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D _param1)
    : this()
  {
    this.\u0023\u003DzrEoWi5uPS0Yz(_param1);
    this.\u0023\u003DzKPDuUZmOc92KysMGPQ\u003D\u003D = _param1.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyNMn5sYJZKzXDppYgrIuwPB>();
    this.\u0023\u003DzlisSOG7yPz_K = _param1.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>();
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzexJGsaAi6rVI(
    EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D> _param1)
  {
    EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D> eventHandler = this.\u0023\u003Dzpet4TiFkmcjB;
    EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D>>(ref this.\u0023\u003Dzpet4TiFkmcjB, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003Dz38JNnebwqLph(
    EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D> _param1)
  {
    EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D> eventHandler = this.\u0023\u003Dzpet4TiFkmcjB;
    EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D>>(ref this.\u0023\u003Dzpet4TiFkmcjB, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzgJ2fVeQPjKRri1qXw3qPlis\u003D(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003Dz7YsCgRH0e3NJ7eTEsg\u003D\u003D;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003Dz7YsCgRH0e3NJ7eTEsg\u003D\u003D, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzxnGwvDMxcoluh8vrt7fwxBo\u003D(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003Dz7YsCgRH0e3NJ7eTEsg\u003D\u003D;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003Dz7YsCgRH0e3NJ7eTEsg\u003D\u003D, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzLOJcvYqnP9Y4L3YbD2yw_yg\u003D(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003DzEA6vcLlqH_FFzckuMg\u003D\u003D;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003DzEA6vcLlqH_FFzckuMg\u003D\u003D, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzYo\u0024r0wypzk52wwMTFQ\u0024KNCQ\u003D(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003DzEA6vcLlqH_FFzckuMg\u003D\u003D;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003DzEA6vcLlqH_FFzckuMg\u003D\u003D, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzVms\u0024rml2A7zL1pwmGg\u003D\u003D(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003DzqsL83n7r4oZ4;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003DzqsL83n7r4oZ4, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003Dz6gcEAkTU68jnRIWXQQ\u003D\u003D(EventHandler _param1)
  {
    EventHandler eventHandler = this.\u0023\u003DzqsL83n7r4oZ4;
    EventHandler comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.\u0023\u003DzqsL83n7r4oZ4, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  internal \u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D \u0023\u003Dz\u00245MUffYz41yl()
  {
    return this.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D>();
  }

  public ItemsPanelTemplate LeftAxesPanelTemplate
  {
    get
    {
      return (ItemsPanelTemplate) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz9lYVM5L3vWhgRCkiGg\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz9lYVM5L3vWhgRCkiGg\u003D\u003D, (object) value);
    }
  }

  public ItemsPanelTemplate RightAxesPanelTemplate
  {
    get
    {
      return (ItemsPanelTemplate) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzC8eGbYU38ZOhQsYmkg\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzC8eGbYU38ZOhQsYmkg\u003D\u003D, (object) value);
    }
  }

  public ItemsPanelTemplate BottomAxesPanelTemplate
  {
    get
    {
      return (ItemsPanelTemplate) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzkPeQninQ1wHrCZ6TRw\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzkPeQninQ1wHrCZ6TRw\u003D\u003D, (object) value);
    }
  }

  public ItemsPanelTemplate TopAxesPanelTemplate
  {
    get
    {
      return (ItemsPanelTemplate) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzUKDFpV7tPYmw\u0024i2jGg\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzUKDFpV7tPYmw\u0024i2jGg\u003D\u003D, (object) value);
    }
  }

  public ItemsPanelTemplate CenterXAxesPanelTemplate
  {
    get
    {
      return (ItemsPanelTemplate) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzhAlxcjWzGbAtV5gIOEJX7LQ\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzhAlxcjWzGbAtV5gIOEJX7LQ\u003D, (object) value);
    }
  }

  public ItemsPanelTemplate CenterYAxesPanelTemplate
  {
    get
    {
      return (ItemsPanelTemplate) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz3Igx8jH2JowbGxnbosZfY9g\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz3Igx8jH2JowbGxnbosZfY9g\u003D, (object) value);
    }
  }

  public bool ClipOverlayAnnotations
  {
    get
    {
      return (bool) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz8254dnHLdRCy);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz8254dnHLdRCy, (object) value);
    }
  }

  public bool ClipUnderlayAnnotations
  {
    get
    {
      return (bool) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzOEM9rkU52IaaaYckycathxI\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzOEM9rkU52IaaaYckycathxI\u003D, (object) value);
    }
  }

  public ObservableCollection<IRenderableSeries> RenderableSeries
  {
    get
    {
      return (ObservableCollection<IRenderableSeries>) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzda5ZTgpF7nPj_QX8WWDrVmQ\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzda5ZTgpF7nPj_QX8WWDrVmQ\u003D, (object) value);
    }
  }

  public ObservableCollection<IRenderableSeries> SelectedRenderableSeries
  {
    get
    {
      return (ObservableCollection<IRenderableSeries>) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzQFsR0tDPWyoiatb3P7odRD8\u003D);
    }
    private set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzQFsR0tDPWyoiatb3P7odRD8\u003D, (object) value);
    }
  }

  [Obsolete("We're Sorry! The AutoRangeOnStartup property has been deprecated in Ultrachart", true)]
  public bool AutoRangeOnStartup
  {
    get
    {
      return (bool) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzrq3jmpc2lTWmgU1xYQ\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzrq3jmpc2lTWmgU1xYQ\u003D\u003D, (object) value);
    }
  }

  public ICommand ZoomExtentsCommand
  {
    get
    {
      return (ICommand) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzTX0a9G8VrfoD);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzTX0a9G8VrfoD, (object) value);
    }
  }

  public ICommand AnimateZoomExtentsCommand
  {
    get
    {
      return (ICommand) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzzGCMRya8OoUTRsROAEXfFVg\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzzGCMRya8OoUTRsROAEXfFVg\u003D, (object) value);
    }
  }

  public \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB XAxis
  {
    get
    {
      return (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DztnODn3Lbw9gQ);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DztnODn3Lbw9gQ, (object) value);
    }
  }

  public \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB YAxis
  {
    get
    {
      return (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz1Eyckd4j0Nvq);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz1Eyckd4j0Nvq, (object) value);
    }
  }

  public \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D YAxes
  {
    get
    {
      return (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzp4pbuj2W1yJe);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzp4pbuj2W1yJe, (object) value);
    }
  }

  public \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D XAxes
  {
    get
    {
      return (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzDqajrUCXt2L8);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzDqajrUCXt2L8, (object) value);
    }
  }

  public \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D Annotations
  {
    get
    {
      return (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzCQBWGrk\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzCQBWGrk\u003D, (object) value);
    }
  }

  public \u0023\u003Dz3RRntx4pzkd854dIVpLK6Ww8ODIV2zPrRw\u003D\u003D ViewportManager
  {
    get
    {
      return (\u0023\u003Dz3RRntx4pzkd854dIVpLK6Ww8ODIV2zPrRw\u003D\u003D) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz8o0yiN0I55Gm);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz8o0yiN0I55Gm, (object) value);
    }
  }

  [SpecialName]
  public \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk \u0023\u003DzFPPJbPlQRagwT6aZuQ\u003D\u003D()
  {
    return (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk) this.\u0023\u003DzJCSLrMbWu3xb\u00240pkXO0DnxY\u003D;
  }

  [SpecialName]
  public \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk \u0023\u003Dz7EP15yq7Yz\u0024jLVX6GgE8gjs\u003D()
  {
    return (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk) this.\u0023\u003Dzm\u0024RjME9m9HW8iNSk3pzjUqs\u003D;
  }

  [SpecialName]
  public Canvas \u0023\u003DzjEjGZ817bm4EOO82ig\u003D\u003D() => this.\u0023\u003Dzn_X_okoYEhwx;

  public \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D ChartModifier
  {
    get
    {
      return (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzj87lYSja1JrB);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzj87lYSja1JrB, (object) value);
    }
  }

  [SpecialName]
  public \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpB4GFFdsIQ_FR8tlLNjHr1X3p7javA\u003D\u003D \u0023\u003DzTRL\u0024Xy0vYDigfJ9YNg\u003D\u003D()
  {
    return (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpB4GFFdsIQ_FR8tlLNjHr1X3p7javA\u003D\u003D) this.\u0023\u003Dz21FrxnEmUmBR;
  }

  public Style GridLinesPanelStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzKIR3fEiw\u0024nx0X6LDLQ\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzKIR3fEiw\u0024nx0X6LDLQ\u003D\u003D, (object) value);
    }
  }

  public Style RenderSurfaceStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz9HyEohEm8\u0024tx);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz9HyEohEm8\u0024tx, (object) value);
    }
  }

  public ObservableCollection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D> SeriesSource
  {
    get
    {
      return (ObservableCollection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzhi6ewyq0WHcz);
    }
    set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzhi6ewyq0WHcz, (object) value);
    }
  }

  public bool IsPolarChart
  {
    get
    {
      return (bool) this.GetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzgbpAybKiYbKbKG238g\u003D\u003D);
    }
    private set
    {
      this.SetValue(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzgbpAybKiYbKbKG238g\u003D\u003D, (object) value);
    }
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.\u0023\u003Dzk084l\u00247aS4G3();
    if (this.\u0023\u003DzGFGsUr_xxE4H != null)
      this.\u0023\u003DzGFGsUr_xxE4H.Items.Clear();
    if (this.\u0023\u003Dz0YK5mfh0G_wP != null)
      this.\u0023\u003Dz0YK5mfh0G_wP.Items.Clear();
    if (this.\u0023\u003DzK\u0024t4oL\u0024CAaKU != null)
      this.\u0023\u003DzK\u0024t4oL\u0024CAaKU.Items.Clear();
    if (this.\u0023\u003DztKgn6GxCcL\u0024B != null)
      this.\u0023\u003DztKgn6GxCcL\u0024B.Items.Clear();
    if (this.\u0023\u003Dz1mGpFN8tTa5PB1b5eA\u003D\u003D != null)
      this.\u0023\u003Dz1mGpFN8tTa5PB1b5eA\u003D\u003D.Items.Clear();
    if (this.\u0023\u003DzkW\u0024qJVeNKX1VtemTog\u003D\u003D != null)
      this.\u0023\u003DzkW\u0024qJVeNKX1VtemTog\u003D\u003D.Items.Clear();
    this.\u0023\u003Dz21FrxnEmUmBR = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLTCNAKZHF2LCBZNSWLDVPYTWWEKD4_ejd>("");
    this.\u0023\u003Dz0YK5mfh0G_wP = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd>("");
    this.\u0023\u003DzGFGsUr_xxE4H = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd>("");
    this.\u0023\u003DzK\u0024t4oL\u0024CAaKU = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd>("");
    this.\u0023\u003DztKgn6GxCcL\u0024B = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd>("");
    this.\u0023\u003Dz1mGpFN8tTa5PB1b5eA\u003D\u003D = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd>("");
    this.\u0023\u003DzkW\u0024qJVeNKX1VtemTog\u003D\u003D = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd>("");
    this.\u0023\u003DzJCSLrMbWu3xb\u00240pkXO0DnxY\u003D = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zL3F4JFPFYA57TDFR6E5RGMBJ4VYTG4UGR3GC8GM6BQ3B9PA_ejd>("");
    this.\u0023\u003Dzm\u0024RjME9m9HW8iNSk3pzjUqs\u003D = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zL3F4JFPFYA57TDFR6E5RGMBJ4VYTG4UGR3GC8GM6BQ3B9PA_ejd>("");
    this.\u0023\u003Dzn_X_okoYEhwx = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<Canvas>("");
    ((\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh76xifd94OJRDA\u003D\u003D) this.\u0023\u003Dzu\u0024P3XgkcE7BC()).\u0023\u003Dz7wSH25w\u003D<\u0023\u003DzeOTgfMmJN9ezcFvs39Ju8q\u0024wkROgPo2o_c9nq8U\u003D>(this.\u0023\u003DzBgWxEdRxHdEh());
    if (this.GridLinesPanelStyle != null)
      this.\u0023\u003Dz21FrxnEmUmBR.Style = this.GridLinesPanelStyle;
    this.\u0023\u003Dz21FrxnEmUmBR.\u0023\u003DzmoTeDkJOBCv3(this.\u0023\u003DzlisSOG7yPz_K);
    this.YAxes.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(new Action<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(this.\u0023\u003Dzud3rSZ9s2fpe));
    this.XAxes.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(new Action<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(this.\u0023\u003Dzud3rSZ9s2fpe));
    this.\u0023\u003DzLVfmNBp16wJn();
    this.\u0023\u003Dz5q8i9C4\u003D();
  }

  public override void \u0023\u003Dzgpj0vvz6kQmYv5labCkwdSo\u003D()
  {
    if (this.ViewportManager != null)
      this.ViewportManager.\u0023\u003DzY1JcdEJm3Ryc((\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) this);
    base.\u0023\u003Dzgpj0vvz6kQmYv5labCkwdSo\u003D();
    this.\u0023\u003DzAfFKB64u0O_s();
  }

  private void \u0023\u003DzAfFKB64u0O_s()
  {
    lock (this.\u0023\u003DzL79ssDGYe7GX)
    {
      HashSet<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB> dynWmoFzgH4RlWB0lBSet = new HashSet<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>();
      foreach (IRenderableSeries s1JolYrWoYpqmQ6ug in (Collection<IRenderableSeries>) this.RenderableSeries)
      {
        if (this.\u0023\u003DzL79ssDGYe7GX.Contains(s1JolYrWoYpqmQ6ug.get_DataSeries()))
        {
          dynWmoFzgH4RlWB0lBSet.Add(s1JolYrWoYpqmQ6ug.YAxis);
          dynWmoFzgH4RlWB0lBSet.Add(s1JolYrWoYpqmQ6ug.XAxis);
        }
      }
      dynWmoFzgH4RlWB0lBSet.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzCNnJa2MB\u00244vpz715iA\u003D\u003D ?? (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzCNnJa2MB\u00244vpz715iA\u003D\u003D = new Action<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz2KWD3lC7hdm1)));
      this.\u0023\u003DzL79ssDGYe7GX.Clear();
    }
  }

  protected override void \u0023\u003DzTuM3X1E\u003D(bool _param1)
  {
    lock (this.\u0023\u003DzL79ssDGYe7GX)
      this.\u0023\u003DzL79ssDGYe7GX.Clear();
    base.\u0023\u003DzTuM3X1E\u003D(_param1);
  }

  protected override Size MeasureOverride(Size _param1)
  {
    this.YAxes.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(new Action<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(this.\u0023\u003Dzud3rSZ9s2fpe));
    this.XAxes.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(new Action<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(this.\u0023\u003Dzud3rSZ9s2fpe));
    return base.MeasureOverride(_param1);
  }

  private void \u0023\u003Dzud3rSZ9s2fpe(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
  {
    Tuple<dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd, bool> tuple = this.\u0023\u003DzbnHWjFJbHdsR(_param1);
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd demydmpA2K68QEjd = tuple.Item1;
    bool flag = tuple.Item2;
    if (demydmpA2K68QEjd == _param1.get_AxisAlignment() && flag == _param1.get_IsCenterAxis())
      return;
    this.\u0023\u003DzDhEcdoGF8rbq(_param1, demydmpA2K68QEjd, flag);
  }

  private Tuple<dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd, bool> \u0023\u003DzbnHWjFJbHdsR(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
  {
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd demydmpA2K68QEjd = dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Default;
    bool flag = false;
    if (this.\u0023\u003DztKgn6GxCcL\u0024B.Items.Contains((object) _param1))
      demydmpA2K68QEjd = dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Left;
    else if (this.\u0023\u003DzK\u0024t4oL\u0024CAaKU.Items.Contains((object) _param1))
      demydmpA2K68QEjd = dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Right;
    else if (this.\u0023\u003DzGFGsUr_xxE4H.Items.Contains((object) _param1))
      demydmpA2K68QEjd = dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top;
    else if (this.\u0023\u003Dz0YK5mfh0G_wP.Items.Contains((object) _param1))
      demydmpA2K68QEjd = dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom;
    else if (this.\u0023\u003Dz1mGpFN8tTa5PB1b5eA\u003D\u003D.Items.Contains((object) _param1) || this.\u0023\u003DzkW\u0024qJVeNKX1VtemTog\u003D\u003D.Items.Contains((object) _param1))
    {
      demydmpA2K68QEjd = _param1.get_AxisAlignment();
      flag = true;
    }
    return new Tuple<dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd, bool>(demydmpA2K68QEjd, flag);
  }

  private void \u0023\u003Dzk084l\u00247aS4G3()
  {
    this.\u0023\u003DzVM1_npDqI38t();
    if (this.ChartModifier != null)
      this.\u0023\u003DzSksGoLn96rP3(this.ChartModifier);
    if (this.Annotations == null)
      return;
    this.Annotations.\u0023\u003DzIDbfqzYC9gDs((\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) this);
  }

  private void \u0023\u003DzLVfmNBp16wJn()
  {
    if (this.ChartModifier != null)
      this.\u0023\u003Dzo3mAQ8z0oY6l(this.ChartModifier);
    if (this.Annotations == null)
      return;
    this.Annotations.\u0023\u003Dz5ClIah4mXevT((\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) this);
  }

  public virtual void \u0023\u003Dzn72LMZ0738BY() => this.\u0023\u003Dzn72LMZ0738BY(TimeSpan.Zero);

  public void \u0023\u003Dz5v2P2MZQ6Mg5pHstYQ\u003D\u003D(TimeSpan _param1)
  {
    this.\u0023\u003Dzn72LMZ0738BY(_param1);
  }

  private void \u0023\u003Dzn72LMZ0738BY(TimeSpan _param1)
  {
    if (this.XAxes.\u0023\u003DzCCMM80zDpO6N<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>() || this.YAxes.\u0023\u003DzCCMM80zDpO6N<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>())
    {
      if (!this.\u0023\u003DzBR\u0024QbsqohI1Fe2cEP8ix4DYKHfMwrzbUAA\u003D\u003D())
        return;
      string str = "" + \u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzSPgiXVV8P9OntbEWmi9QaNcIfsic?.ToString();
      Console.WriteLine(str);
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(str, Array.Empty<object>());
    }
    else
    {
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("", Array.Empty<object>());
      using (this.SuspendUpdates())
        this.\u0023\u003Dz7mFu4O\u0024TokaR(this.\u0023\u003Dz9l24o\u0024KwqoZC(_param1), _param1);
    }
  }

  private IDictionary<string, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D> \u0023\u003Dz9l24o\u0024KwqoZC(
    TimeSpan _param1)
  {
    Dictionary<string, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D> dictionary = new Dictionary<string, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>();
    foreach (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB xax in (Collection<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>) this.XAxes)
    {
      \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = xax.\u0023\u003DzFwoMKP9juTnt();
      xax.\u0023\u003DzB4GssLEPDUHfRR_NuUKVKvc\u003D(abyLt9clZggmJsWhw, _param1);
      dictionary.Add(xax.get_Id(), abyLt9clZggmJsWhw);
    }
    return (IDictionary<string, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>) dictionary;
  }

  private void \u0023\u003Dz7mFu4O\u0024TokaR(
    IDictionary<string, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D> _param1,
    TimeSpan _param2)
  {
    foreach (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB yax in (Collection<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>) this.YAxes)
      this.\u0023\u003DzKvN9OsBw3ge94Ie3GA\u003D\u003D(yax, _param1, _param2);
  }

  private void \u0023\u003DzKvN9OsBw3ge94Ie3GA\u003D\u003D(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1,
    IDictionary<string, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D> _param2,
    TimeSpan _param3)
  {
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = _param1.\u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(_param2);
    _param1.\u0023\u003DzB4GssLEPDUHfRR_NuUKVKvc\u003D(abyLt9clZggmJsWhw, _param3);
  }

  public void \u0023\u003Dz7mFu4O\u0024TokaR()
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("", Array.Empty<object>());
    using (this.SuspendUpdates())
      this.\u0023\u003Dz7mFu4O\u0024TokaR((IDictionary<string, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>) null, TimeSpan.Zero);
  }

  public void \u0023\u003Dzlt5y\u0024abM\u0024EiJBWUsR3G_Wrc\u003D(TimeSpan _param1)
  {
    using (this.SuspendUpdates())
      this.\u0023\u003Dz7mFu4O\u0024TokaR((IDictionary<string, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>) null, _param1);
  }

  public void \u0023\u003Dz8zwqAzdRsuc\u0024()
  {
    this.\u0023\u003Dz9l24o\u0024KwqoZC(TimeSpan.Zero);
  }

  public void \u0023\u003Dz8NovIOacEzVlET_SOgsaL_w\u003D(TimeSpan _param1)
  {
    this.\u0023\u003Dz9l24o\u0024KwqoZC(_param1);
  }

  public new \u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote SuspendUpdates()
  {
    return (\u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote) new \u0023\u003DzuPRmIFUVJkGxyCE55JH19ZE5sEUdF5DXPLZ7U6Rxl0An((\u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D) this);
  }

  public new void ResumeUpdates(
    \u0023\u003DzPauio66DvxKtWOFEEHOV9VFlFQ05jnDG3bOrIrgCJote _param1)
  {
    if (!_param1.\u0023\u003DzuWdUDFWIQOsx())
      return;
    this.\u0023\u003Dz5q8i9C4\u003D();
  }

  public new void DecrementSuspend()
  {
  }

  [Obsolete("We're Sorry but IUltrachartSurface.Clear() has been deprecated. Please call IUltrachartSurface.RenderableSeries.Clear() to clear the chart", true)]
  public void \u0023\u003Dzqtb9toLjXu0t()
  {
  }

  public static string \u0023\u003Dz8at5FfiITdEk()
  {
    Version version = new AssemblyName(typeof (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd).Assembly.FullName).Version;
    return string.Format("", (object) version);
  }

  private void \u0023\u003DzeKdxSypd_FVL(
    object _param1,
    \u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy _param2)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz041D7jmffErIs2Wft03rpCc\u003D d7jmffErIs2Wft03rpCc = new dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz041D7jmffErIs2Wft03rpCc\u003D();
    d7jmffErIs2Wft03rpCc.\u0023\u003DzRRvwDu67s9Rm = this;
    if (!this.IsLoaded || this.\u0023\u003DzQ6xddArfD502() == (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D) 0 || this.\u0023\u003DzwRf5PdbT802y)
      return;
    this.\u0023\u003DzwRf5PdbT802y = true;
    d7jmffErIs2Wft03rpCc.\u0023\u003Dz5vxB7hs\u003D = new Action(d7jmffErIs2Wft03rpCc.\u0023\u003DzPEXAppbcaBgnas\u00248pco9O5Y\u003D);
    if (this.\u0023\u003DzQ6xddArfD502() == (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D) 1)
    {
      d7jmffErIs2Wft03rpCc.\u0023\u003Dz5vxB7hs\u003D();
    }
    else
    {
      if (this.\u0023\u003DzQ6xddArfD502() != (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D) 2)
        return;
      this.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzK74oGPE3yyB7zop8uDdzn_\u0024VotJ4cPSCfA7R2Hc\u003D>().\u0023\u003DzbMTrUAQ\u003D(new Action(d7jmffErIs2Wft03rpCc.\u0023\u003Dz7n\u0024Gp4RnkI3hGyrPb7eAGT0\u003D), (DispatcherPriority) 5);
    }
  }

  protected override void \u0023\u003DzOB4dXYeIFfiB()
  {
    if (this.\u0023\u003Dz5OzJ0EHhtC8P() || this.\u0023\u003DzMZpCUE8eL2X8 == null)
      return;
    if (this.Visibility != Visibility.Visible)
      return;
    try
    {
      using (\u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I mvXdEdq1k7UiFd2I = this.\u0023\u003DzMZpCUE8eL2X8.\u0023\u003Dz1cRMfLZU4Eo2())
      {
        try
        {
          \u0023\u003Dzgg5QOmcWitJriAsXqwM_mn\u0024OgQPZalPRDrRrx2Q\u003D ogQpZalPrDrRrx2Q = this.\u0023\u003DzKPDuUZmOc92KysMGPQ\u003D\u003D.\u0023\u003Dz\u0024ccLugjL4c3p(mvXdEdq1k7UiFd2I);
          if (\u0023\u003DzTbSy5Tg7CNKewHb2FguXq09eZMREAJl6IstiMmQ\u003D.\u0023\u003DzWsHTUE0\u003D.Equals((object) ogQpZalPrDrRrx2Q) || !this.\u0023\u003DzBR\u0024QbsqohI1Fe2cEP8ix4DYKHfMwrzbUAA\u003D\u003D())
            return;
          string str = "" + ogQpZalPrDrRrx2Q?.ToString();
          Console.WriteLine(str);
          \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(str, Array.Empty<object>());
        }
        catch (Exception ex)
        {
          this.\u0023\u003DzVjhvGWo3fUWc(ex);
        }
      }
    }
    catch (Exception ex)
    {
      this.\u0023\u003DzVjhvGWo3fUWc(ex);
    }
  }

  [Obsolete("Obsolete. Please use UltrachartSurface.RootGrid.TranslatePoint instead", true)]
  public Point \u0023\u003DzaPPLsvfM_Sst(
    Point _param1,
    \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z _param2)
  {
    throw new NotImplementedException("");
  }

  [Obsolete("Obsolete. Please use UltrachartSurface.RootGrid.IsPointWithinBounds instead", true)]
  public bool \u0023\u003DzbOxVzAyGdX66(Point _param1)
  {
    throw new NotImplementedException("");
  }

  [Obsolete("Obsolete. Please use UltrachartSurface.RootGrid.GetBoundsRelativeTo instead", true)]
  public Rect \u0023\u003DzdC9whUui_gN\u0024(
    \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z _param1)
  {
    throw new NotImplementedException("");
  }

  public void \u0023\u003DzbsWonVbyfEPS(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1)
  {
    if (_param1 == null)
      return;
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D ns01UjmP40FpxAl2jmQ = _param1;
    if (ns01UjmP40FpxAl2jmQ.get_ParentSurface() == null)
      ns01UjmP40FpxAl2jmQ.set_ParentSurface((\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) this);
    if (this.\u0023\u003DzNIujeQAKJfHjmKL2ONO8jgpF_OJU())
    {
      _param1.remove_DataSeriesChanged(new EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X>(this.\u0023\u003DztwHsLGWnHCVU));
      _param1.add_DataSeriesChanged(new EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X>(this.\u0023\u003DztwHsLGWnHCVU));
    }
    this.\u0023\u003Dzx87Ma1DdPIPxJ8sodA\u003D\u003D(_param1);
  }

  public void \u0023\u003Dzf72QDPKj6m\u0024z(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1)
  {
    if (_param1 == null)
      return;
    if (_param1.get_ParentSurface() == this)
      _param1.set_ParentSurface((\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) null);
    _param1.remove_DataSeriesChanged(new EventHandler<\u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X>(this.\u0023\u003DztwHsLGWnHCVU));
    this.\u0023\u003Dzx87Ma1DdPIPxJ8sodA\u003D\u003D(_param1);
  }

  private void \u0023\u003DztwHsLGWnHCVU(
    object _param1,
    \u0023\u003Dz5hVyTN88kBn45NAfOxK7MD4fbuZgSG4uWo9Ll25GzP7X _param2)
  {
    this.\u0023\u003Dzx87Ma1DdPIPxJ8sodA\u003D\u003D(_param1 as \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D);
    this.\u0023\u003Dz\u0024YPacLjgy1DJ(_param1, (EventArgs) _param2);
  }

  private void \u0023\u003Dzx87Ma1DdPIPxJ8sodA\u003D\u003D(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1)
  {
    if (_param1 == null)
      return;
    lock (this.\u0023\u003DzL79ssDGYe7GX)
      this.\u0023\u003DzL79ssDGYe7GX.Add(_param1);
  }

  private void \u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    if (_param2.OldItems != null)
    {
      foreach (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB oldItem in (IEnumerable) _param2.OldItems)
        this.\u0023\u003Dzy9wEH1BYapxA(oldItem);
    }
    if (_param2.NewItems != null)
    {
      foreach (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB newItem in (IEnumerable) _param2.NewItems)
        this.\u0023\u003DzoHEODaiFnJb8(newItem, false);
    }
    if (_param2.Action == NotifyCollectionChangedAction.Reset)
      this.\u0023\u003DzBI6yWYsdlZIP(false);
    if (this.Annotations != null)
      this.Annotations.\u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(_param1, _param2);
    if (this.ChartModifier != null)
      this.ChartModifier.\u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D(_param1, _param2);
    this.\u0023\u003Dz5q8i9C4\u003D();
  }

  private void \u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    if (_param2.OldItems != null)
    {
      foreach (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB oldItem in (IEnumerable) _param2.OldItems)
        this.\u0023\u003Dzy9wEH1BYapxA(oldItem);
    }
    if (_param2.NewItems != null)
    {
      foreach (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB newItem in (IEnumerable) _param2.NewItems)
        this.\u0023\u003DzoHEODaiFnJb8(newItem, true);
    }
    if (_param2.Action == NotifyCollectionChangedAction.Reset)
      this.\u0023\u003DzBI6yWYsdlZIP(true);
    if (this.Annotations != null)
      this.Annotations.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(_param1, _param2);
    if (this.ChartModifier != null)
      this.ChartModifier.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D(_param1, _param2);
    this.\u0023\u003Dz5q8i9C4\u003D();
  }

  private void \u0023\u003DzBI6yWYsdlZIP(bool _param1)
  {
    this.\u0023\u003DzYzffTGu8LB8M(this.\u0023\u003DzK\u0024t4oL\u0024CAaKU, _param1);
    this.\u0023\u003DzYzffTGu8LB8M(this.\u0023\u003DztKgn6GxCcL\u0024B, _param1);
    this.\u0023\u003DzYzffTGu8LB8M(this.\u0023\u003DzGFGsUr_xxE4H, _param1);
    this.\u0023\u003DzYzffTGu8LB8M(this.\u0023\u003Dz0YK5mfh0G_wP, _param1);
    this.\u0023\u003DzYzffTGu8LB8M(this.\u0023\u003Dz1mGpFN8tTa5PB1b5eA\u003D\u003D, _param1);
    this.\u0023\u003DzYzffTGu8LB8M(this.\u0023\u003DzkW\u0024qJVeNKX1VtemTog\u003D\u003D, _param1);
  }

  private void \u0023\u003DzYzffTGu8LB8M(
    dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd _param1,
    bool _param2)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7c5Jf9Xcn7g7gyAWka3cVyc\u003D xcn7g7gyAwka3cVyc = new dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7c5Jf9Xcn7g7gyAWka3cVyc\u003D();
    xcn7g7gyAwka3cVyc.\u0023\u003DzPhfEcNQm65OS = _param2;
    if (_param1 == null)
      return;
    foreach (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB in _param1.Items.Cast<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>().Where<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(new Func<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, bool>(xcn7g7gyAwka3cVyc.\u0023\u003Dzn\u0024oF_i5RJAgifiBFYicprPI\u003D)).ToList<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>())
      this.\u0023\u003Dzy9wEH1BYapxA(dynWmoFzgH4RlWB0lB);
  }

  private void \u0023\u003Dzy9wEH1BYapxA(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
  {
    this.\u0023\u003DzIiVzW6zWrcUz(_param1, _param1.get_AxisAlignment(), _param1.get_IsCenterAxis());
    _param1.set_ParentSurface((\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) null);
    _param1.set_Services((\u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D) null);
  }

  private void \u0023\u003DzoHEODaiFnJb8(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1,
    bool _param2)
  {
    _param1.set_ParentSurface((\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) this);
    _param1.\u0023\u003Dz\u0024mpHDOeBCMkH(_param2);
    if (_param1.get_AxisAlignment() == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Default)
    {
      dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd demydmpA2K68QEjd = _param1.\u0023\u003DzFrVmckt\u0024NpG6() ? dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom : dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Right;
      ((DependencyObject) _param1).SetCurrentValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzfMY988N0StOA, (object) demydmpA2K68QEjd);
    }
    else
      this.\u0023\u003DzU8nRfCfIU\u0024K_(_param1, _param1.get_AxisAlignment(), _param1.get_IsCenterAxis());
  }

  private void \u0023\u003DzlRPtJNE7UDOTZfXPK13GjY4\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    if (_param2.Action == NotifyCollectionChangedAction.Reset)
      this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D.Clear();
    if (_param2.OldItems != null)
      _param2.OldItems.Cast<IRenderableSeries>().\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(this.\u0023\u003Dz6fV1owXqTlzBWkKTRBWCs_aD\u0024Qt79YCLHsMl_d0\u003D));
    if (_param2.NewItems == null)
      return;
    _param2.NewItems.Cast<IRenderableSeries>().\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(((Collection<IRenderableSeries>) this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D).Add));
  }

  private void \u0023\u003Dz6fuulLxxSq\u0024eCw5_MNj_8i4\u003D(
    IRenderableSeries _param1)
  {
    if (this.\u0023\u003DzMZpCUE8eL2X8 != null)
    {
      _param1.\u0023\u003Dz_bNSX12Vpev3(new EventHandler(this.\u0023\u003DzTAXK1GE2_\u0024jJ));
      this.\u0023\u003DzMZpCUE8eL2X8.\u0023\u003Dz_SCZwjM\u003D(_param1);
    }
    if (_param1.get_DataSeries() != null)
      this.\u0023\u003Dzf72QDPKj6m\u0024z(_param1.get_DataSeries());
    if (_param1.get_IsSelected())
      ((DependencyObject) _param1).SetCurrentValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzHttRjYlEOUXJ, (object) false);
    if (this.SelectedRenderableSeries.Contains(_param1))
      this.SelectedRenderableSeries.Remove(_param1);
    if (_param1 is \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D)
    {
      \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D vosPhel85wyPwiDyo = (\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D) _param1;
      this.\u0023\u003DzGqYqkF73Z9yr0zUWMg\u003D\u003D().\u0023\u003Dz_SCZwjM\u003D(vosPhel85wyPwiDyo);
      if (this.\u0023\u003DzGqYqkF73Z9yr0zUWMg\u003D\u003D().\u0023\u003DzBRQKPSBN1vq7() == 0)
        this.\u0023\u003DzB\u0024taTl5OHzRG5U3QXw\u003D\u003D((\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUtzzzd5rSNXl95sF5MghysRDMZyklVKg61SC2QL8) null);
    }
    if (_param1 is \u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D)
    {
      \u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D jdEr0Gb4PvgE1uuw = (\u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D) _param1;
      this.\u0023\u003DzZ5VDq\u0024Vzik5LlJ4Fd5jKuBg\u003D().\u0023\u003Dz_SCZwjM\u003D(jdEr0Gb4PvgE1uuw);
      if (this.\u0023\u003DzZ5VDq\u0024Vzik5LlJ4Fd5jKuBg\u003D().\u0023\u003DzBRQKPSBN1vq7() == 0)
        this.\u0023\u003DzPUfITvj9OihLWDW1T8K_pLs\u003D((\u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKg1uYp9C5FyfAbaz1_Vr5UELTst58YqUlllvt2EY65UXJw\u003D\u003D) null);
    }
    this.\u0023\u003Dz5q8i9C4\u003D();
  }

  private void \u0023\u003Dzk6sVBMw95BkmcWhiKErWml4\u003D(
    IRenderableSeries _param1)
  {
    if (this.\u0023\u003DzMZpCUE8eL2X8 != null)
    {
      this.\u0023\u003DzMZpCUE8eL2X8.\u0023\u003DzJoneIt0\u003D(_param1);
      _param1.\u0023\u003Dz_bNSX12Vpev3(new EventHandler(this.\u0023\u003DzTAXK1GE2_\u0024jJ));
      _param1.\u0023\u003DzhBqSd5Scc0Hy(new EventHandler(this.\u0023\u003DzTAXK1GE2_\u0024jJ));
      if (_param1.get_IsSelected() && !this.SelectedRenderableSeries.Contains(_param1))
        this.SelectedRenderableSeries.Add(_param1);
    }
    if (_param1.get_DataSeries() != null)
      this.\u0023\u003DzbsWonVbyfEPS(_param1.get_DataSeries());
    if (_param1 is \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D)
    {
      if (this.\u0023\u003DzGqYqkF73Z9yr0zUWMg\u003D\u003D() == null)
        this.\u0023\u003DzB\u0024taTl5OHzRG5U3QXw\u003D\u003D((\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUtzzzd5rSNXl95sF5MghysRDMZyklVKg61SC2QL8) new \u0023\u003Dz6SSn5QQkepq6NeBmeacJnKTMXO44jrRgiFgx1yG83uHFlgDZuwIzh90\u003D());
      \u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D vosPhel85wyPwiDyo = (\u0023\u003DzJLUdDOVbHWuhCASQiXx2GJkyMd4jQIWNjWnikm93dwBZyHAJzXm4T0VosPhel85wyPWiDYo\u003D) _param1;
      this.\u0023\u003DzGqYqkF73Z9yr0zUWMg\u003D\u003D().\u0023\u003DzJoneIt0\u003D(vosPhel85wyPwiDyo);
    }
    if (_param1 is \u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D)
    {
      if (this.\u0023\u003DzZ5VDq\u0024Vzik5LlJ4Fd5jKuBg\u003D() == null)
        this.\u0023\u003DzPUfITvj9OihLWDW1T8K_pLs\u003D((\u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKg1uYp9C5FyfAbaz1_Vr5UELTst58YqUlllvt2EY65UXJw\u003D\u003D) new \u0023\u003Dz8b2iwQyC3tYOGumtm_saeEnFs\u0024SRQLEosv4pfSjj0tkAQV_GK7UbDd1NY3xuBRiQqw\u003D\u003D());
      \u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D jdEr0Gb4PvgE1uuw = (\u0023\u003DzbZGwufOdFTewaG24h4AgEnzuMpofQ03Hc8jI6jE7b1HUFPrXQWmwDjkmjGOLarsxpjJdER0Gb4PvgE1uuw\u003D\u003D) _param1;
      this.\u0023\u003DzZ5VDq\u0024Vzik5LlJ4Fd5jKuBg\u003D().\u0023\u003DzJoneIt0\u003D(jdEr0Gb4PvgE1uuw);
    }
    this.\u0023\u003Dz5q8i9C4\u003D();
  }

  private void \u0023\u003DzzAzmhJu\u00240jW8(
    \u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D _param1)
  {
    _param1.PropertyChanged -= new PropertyChangedEventHandler(this.\u0023\u003Dz_2Z37e3SOi5eNmQj2g\u003D\u003D);
    if (_param1.RenderSeries == null || !this.RenderableSeries.Contains(_param1.RenderSeries))
      return;
    this.RenderableSeries.Remove(_param1.RenderSeries);
  }

  private void \u0023\u003Dzog1ZIjwbjbmK(
    \u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D _param1)
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) _param1, "");
    _param1.PropertyChanged -= new PropertyChangedEventHandler(this.\u0023\u003Dz_2Z37e3SOi5eNmQj2g\u003D\u003D);
    _param1.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dz_2Z37e3SOi5eNmQj2g\u003D\u003D);
    int index = this.SeriesSource.IndexOf(_param1);
    if (_param1.RenderSeries == null)
      return;
    if (!this.RenderableSeries.Contains(_param1.RenderSeries))
      this.RenderableSeries.Insert(index, _param1.RenderSeries);
    _param1.RenderSeries.set_DataSeries(_param1.DataSeries);
  }

  private void \u0023\u003DzTAXK1GE2_\u0024jJ(object _param1, EventArgs _param2)
  {
    if (!(_param1 is IRenderableSeries s1JolYrWoYpqmQ6ug))
      return;
    if (s1JolYrWoYpqmQ6ug.get_IsSelected())
      this.SelectedRenderableSeries.Add(s1JolYrWoYpqmQ6ug);
    else
      this.SelectedRenderableSeries.Remove(s1JolYrWoYpqmQ6ug);
  }

  protected override void \u0023\u003DzSwq\u0024W16DpTIPh6J9Mw3YqhI\u003D()
  {
    if (this.\u0023\u003DzNIujeQAKJfHjmKL2ONO8jgpF_OJU() || this.\u0023\u003Dz5OzJ0EHhtC8P())
      return;
    base.\u0023\u003DzSwq\u0024W16DpTIPh6J9Mw3YqhI\u003D();
    Interlocked.Increment(ref dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DziUTIXAQ\u003D);
    if (this.SeriesSource != null)
    {
      this.SeriesSource.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003DzCL4dS9OFXaqI);
      this.SeriesSource.CollectionChanged += new NotifyCollectionChangedEventHandler(this.\u0023\u003DzCL4dS9OFXaqI);
      foreach (\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D koh9jO5RuUcFiAqLc in (Collection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>) this.SeriesSource)
        this.\u0023\u003Dzog1ZIjwbjbmK(koh9jO5RuUcFiAqLc);
    }
    this.RenderableSeries.\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(this.\u0023\u003Dzk6sVBMw95BkmcWhiKErWml4\u003D));
    this.XAxes.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(new Action<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(this.\u0023\u003DzJU3JfpRgwPjQa9jDcK3_BJ5p46XJKnhSkbPsgKE\u003D));
    this.YAxes.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(new Action<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(this.\u0023\u003Dz1cXbEwF3yU52tbr4hhxqLCeJfg\u0024S5cdqfXumnuU\u003D));
    this.\u0023\u003DzLVfmNBp16wJn();
  }

  protected override void \u0023\u003Dz90WLXdWl_vULnMk6kNFZNC4\u003D()
  {
    if (!this.\u0023\u003DzNIujeQAKJfHjmKL2ONO8jgpF_OJU())
      return;
    base.\u0023\u003Dz90WLXdWl_vULnMk6kNFZNC4\u003D();
    Interlocked.Decrement(ref dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DziUTIXAQ\u003D);
    if (this.SeriesSource != null)
    {
      foreach (\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D koh9jO5RuUcFiAqLc in (Collection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>) this.SeriesSource)
        this.\u0023\u003DzzAzmhJu\u00240jW8(koh9jO5RuUcFiAqLc);
      this.SeriesSource.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003DzCL4dS9OFXaqI);
    }
    this.RenderableSeries.\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(this.\u0023\u003Dz6fuulLxxSq\u0024eCw5_MNj_8i4\u003D));
    this.XAxes.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(new Action<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(this.\u0023\u003Dzy9wEH1BYapxA));
    this.YAxes.\u0023\u003Dz30RSSSygABj_<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(new Action<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(this.\u0023\u003Dzy9wEH1BYapxA));
    this.\u0023\u003Dzk084l\u00247aS4G3();
  }

  public Size \u0023\u003DzBr6p5Qw\u0024W6BFNGQPNFOKrj0\u003D()
  {
    this.RenderSurface.\u0023\u003Dzn2tbOrs4ILOI();
    return new Size(this.RenderSurface.\u0023\u003Dzu2ObQ3hMALTN(), this.RenderSurface.\u0023\u003Dz2kO1mtG\u0024bEUM());
  }

  [Obsolete("IUltrachartSurface.GetWindowedYRange is obsolete. Use IAxis.GetWindowedYRange instead", true)]
  public \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1,
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param2)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzmHUZq1a0vCkinf2sWxYoWqM\u003D zq1a0vCkinf2sWxYoWqM = new dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzmHUZq1a0vCkinf2sWxYoWqM\u003D();
    zq1a0vCkinf2sWxYoWqM.\u0023\u003DzMM5Kl1w\u003D = _param2;
    zq1a0vCkinf2sWxYoWqM.\u0023\u003DzS7JsfCE\u003D = _param1;
    IEnumerable<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D> source = this.\u0023\u003DzlwAvJczjNvUE(zq1a0vCkinf2sWxYoWqM.\u0023\u003DzS7JsfCE\u003D.get_Id());
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw1 = zq1a0vCkinf2sWxYoWqM.\u0023\u003DzS7JsfCE\u003D.\u0023\u003DzFwoMKP9juTnt();
    if (!source.Any<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D>())
      return zq1a0vCkinf2sWxYoWqM.\u0023\u003DzS7JsfCE\u003D.VisibleRange != null && !zq1a0vCkinf2sWxYoWqM.\u0023\u003DzS7JsfCE\u003D.VisibleRange.IsDefined ? abyLt9clZggmJsWhw1 : zq1a0vCkinf2sWxYoWqM.\u0023\u003DzS7JsfCE\u003D.VisibleRange;
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D[] array = source.Select<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>(new Func<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D, \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>(zq1a0vCkinf2sWxYoWqM.\u0023\u003Dzv6ZL84K3ljUM9AvngG5_IKXd32Mz)).Where<\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzoz5TxoGG1694NBizKw\u003D\u003D ?? (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzoz5TxoGG1694NBizKw\u003D\u003D = new Func<\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D, bool>(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzoZM1fSvQVSQ7YXyQfMSGaGZWjDLCOmVAEA\u003D\u003D))).ToArray<\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>();
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw2 = \u0023\u003DzsIIzg9COgILMyUKVNisy8sT1ePq3.\u0023\u003DzVqxLKNDqEV82<\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D>(array);
    if (abyLt9clZggmJsWhw2 != null)
    {
      for (int index = 1; index < array.Length; ++index)
        abyLt9clZggmJsWhw2 = abyLt9clZggmJsWhw2.\u0023\u003DzeiifnZI\u003D(array[index]);
      if (zq1a0vCkinf2sWxYoWqM.\u0023\u003DzS7JsfCE\u003D.get_GrowBy() != null && zq1a0vCkinf2sWxYoWqM.\u0023\u003DzS7JsfCE\u003D.get_GrowBy().IsDefined)
        abyLt9clZggmJsWhw2.\u0023\u003DzzXTqVFg\u003D(zq1a0vCkinf2sWxYoWqM.\u0023\u003DzS7JsfCE\u003D.get_GrowBy().Min, zq1a0vCkinf2sWxYoWqM.\u0023\u003DzS7JsfCE\u003D.get_GrowBy().Max);
    }
    return abyLt9clZggmJsWhw2 ?? abyLt9clZggmJsWhw1;
  }

  public void \u0023\u003DzFrczvpG2vhM5(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
  {
    this.\u0023\u003DzDhEcdoGF8rbq(_param1, _param1.get_AxisAlignment(), !_param1.get_IsCenterAxis());
  }

  public void \u0023\u003DzOPvUPixjU\u00244Y(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1,
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd _param2)
  {
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd axisAlignment = _param1.get_AxisAlignment();
    this.\u0023\u003DzDhEcdoGF8rbq(_param1, _param2, _param1.get_IsCenterAxis());
    this.\u0023\u003DzOPvUPixjU\u00244Y(new \u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D(_param1.get_Id(), _param2, axisAlignment));
  }

  private void \u0023\u003DzDhEcdoGF8rbq(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1,
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd _param2,
    bool _param3)
  {
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd axisAlignment = _param1.get_AxisAlignment();
    bool isCenterAxis = _param1.get_IsCenterAxis();
    using (_param1.SuspendUpdates())
    {
      this.\u0023\u003DzIiVzW6zWrcUz(_param1, _param2, _param3);
      this.\u0023\u003DzU8nRfCfIU\u0024K_(_param1, axisAlignment, isCenterAxis);
    }
  }

  private void \u0023\u003DzIiVzW6zWrcUz(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1,
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd _param2,
    bool _param3)
  {
    dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd yg78Km6T7DkngtaEjd = this.\u0023\u003Dz5v44edx1gaaI(_param1, _param2, _param3);
    if (yg78Km6T7DkngtaEjd != null && _param1.get_ParentSurface() != null)
      yg78Km6T7DkngtaEjd.\u0023\u003DzXp3QEOzPyMYR((object) _param1);
    this.\u0023\u003Dzd54ofk38XbF1j_bR\u0024w\u003D\u003D();
  }

  private void \u0023\u003Dzd54ofk38XbF1j_bR\u0024w\u003D\u003D()
  {
    if (this.\u0023\u003Dz1mGpFN8tTa5PB1b5eA\u003D\u003D == null || this.\u0023\u003DzkW\u0024qJVeNKX1VtemTog\u003D\u003D == null)
      return;
    this.IsPolarChart = this.\u0023\u003Dz1mGpFN8tTa5PB1b5eA\u003D\u003D.Items.OfType<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>().Any<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DziZ6WqO9c0bLyreXcPw\u003D\u003D ?? (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DziZ6WqO9c0bLyreXcPw\u003D\u003D = new Func<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, bool>(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzHqCL4TMIgkX9lgbfbVwoLztjktmcnH1IkQ\u003D\u003D))) || this.\u0023\u003DzkW\u0024qJVeNKX1VtemTog\u003D\u003D.Items.OfType<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>().Any<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz2u81qtV0w4N3y6_wTA\u003D\u003D ?? (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz2u81qtV0w4N3y6_wTA\u003D\u003D = new Func<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, bool>(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzqleqgxOwnD86h0g7xgV6jwj2FfjN9XQ2Tw\u003D\u003D)));
  }

  private dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003Dz5v44edx1gaaI(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1,
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd _param2,
    bool _param3)
  {
    if (_param3)
      return !_param1.\u0023\u003DzFrVmckt\u0024NpG6() ? this.\u0023\u003DzkW\u0024qJVeNKX1VtemTog\u003D\u003D : this.\u0023\u003Dz1mGpFN8tTa5PB1b5eA\u003D\u003D;
    dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd yg78Km6T7DkngtaEjd = (dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd) null;
    switch (_param2)
    {
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Right:
        yg78Km6T7DkngtaEjd = this.\u0023\u003DzK\u0024t4oL\u0024CAaKU;
        break;
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Left:
        yg78Km6T7DkngtaEjd = this.\u0023\u003DztKgn6GxCcL\u0024B;
        break;
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top:
        yg78Km6T7DkngtaEjd = this.\u0023\u003DzGFGsUr_xxE4H;
        break;
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom:
        yg78Km6T7DkngtaEjd = this.\u0023\u003Dz0YK5mfh0G_wP;
        break;
      case dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Default:
        yg78Km6T7DkngtaEjd = _param1.\u0023\u003DzFrVmckt\u0024NpG6() ? this.\u0023\u003Dz0YK5mfh0G_wP : this.\u0023\u003DzK\u0024t4oL\u0024CAaKU;
        break;
    }
    return yg78Km6T7DkngtaEjd;
  }

  private void \u0023\u003DzU8nRfCfIU\u0024K_(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1,
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd _param2,
    bool _param3)
  {
    dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd yg78Km6T7DkngtaEjd = this.\u0023\u003Dz5v44edx1gaaI(_param1, _param2, _param3);
    if (yg78Km6T7DkngtaEjd != null && !yg78Km6T7DkngtaEjd.Items.Contains((object) _param1))
    {
      if ((yg78Km6T7DkngtaEjd == this.\u0023\u003Dz1mGpFN8tTa5PB1b5eA\u003D\u003D || yg78Km6T7DkngtaEjd == this.\u0023\u003DzkW\u0024qJVeNKX1VtemTog\u003D\u003D ? 0 : (_param2 == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Left ? 1 : (_param2 == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Top ? 1 : 0))) != 0)
        yg78Km6T7DkngtaEjd.Items.Insert(0, (object) _param1);
      else
        yg78Km6T7DkngtaEjd.Items.Add((object) _param1);
    }
    this.\u0023\u003Dzd54ofk38XbF1j_bR\u0024w\u003D\u003D();
  }

  private void \u0023\u003DzOPvUPixjU\u00244Y(
    \u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D _param1)
  {
    EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D> zpet4TiFkmcjB = this.\u0023\u003Dzpet4TiFkmcjB;
    if (zpet4TiFkmcjB == null)
      return;
    zpet4TiFkmcjB((object) this, _param1);
  }

  public XmlSchema GetSchema() => (XmlSchema) null;

  public virtual void ReadXml(XmlReader _param1)
  {
    if (_param1.MoveToContent() != XmlNodeType.Element || !(_param1.LocalName == ((object) this).GetType().Name))
      return;
    \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz4EJs3pc\u003D(this, _param1);
  }

  public virtual void WriteXml(XmlWriter _param1)
  {
    \u0023\u003DzybqkC8coKG2MWZVsSaL09O1_xNRcS\u0024xwBWc4mutdCmTdKj2X3QJ3_KQ\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz7SZ\u0024Lrw\u003D(this, _param1);
  }

  public BitmapSource \u0023\u003Dz12fMuw\u0024m\u002480t()
  {
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D ssr9Vw2SiWlaFw0wjAu = this.\u0023\u003DzQ6xddArfD502();
    try
    {
      this.\u0023\u003DzP3DHJfIEl0iL(this.Width, this.Height);
      return \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u9INFqZgJCYx2M2HF\u0024eccqaxUkBriA\u003D\u003D.\u0023\u003Dz12fMuw\u0024m\u002480t(this);
    }
    finally
    {
      this.\u0023\u003DzB9Fp01Mv4lWu(ssr9Vw2SiWlaFw0wjAu);
    }
  }

  private void \u0023\u003DzP3DHJfIEl0iL(double _param1, double _param2)
  {
    if (this.IsLoaded)
      return;
    Size availableSize = new Size(_param1, _param2);
    this.\u0023\u003DzB9Fp01Mv4lWu((\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D) 0);
    this.ApplyTemplate();
    this.\u0023\u003DznV0cpNo\u003D();
    if (this.Annotations != null)
      this.Annotations.OfType<FrameworkElement>().\u0023\u003Dz30RSSSygABj_<FrameworkElement>(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzkVHywpw3evyESZQ1iA\u003D\u003D ?? (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzkVHywpw3evyESZQ1iA\u003D\u003D = new Action<FrameworkElement>(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzSnploAESTvQvxQJvSegWmmQ\u003D)));
    this.Measure(availableSize);
    this.Arrange(new Rect(new Point(0.0, 0.0), availableSize));
    this.UpdateLayout();
    this.\u0023\u003Dz5q8i9C4\u003D();
    this.UpdateLayout();
    this.\u0023\u003Dz5q8i9C4\u003D();
    this.UpdateLayout();
  }

  public void \u0023\u003Dz6c9L2i_CI8rd(
    string _param1,
    \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA8xQithIyJQfPQ\u003D\u003D _param2)
  {
    \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u9INFqZgJCYx2M2HF\u0024eccqaxUkBriA\u003D\u003D.\u0023\u003DzopWkwiY5TTl5(this.\u0023\u003Dz12fMuw\u0024m\u002480t(), _param1, _param2);
  }

  public void \u0023\u003Dz90p4sVE\u003D(string _param1)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzafpfrngHfftFzifQOwPtvqY\u003D hfftFzifQowPtvqY = new dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003DzafpfrngHfftFzifQOwPtvqY\u003D();
    hfftFzifQowPtvqY.\u0023\u003DzRRvwDu67s9Rm = this;
    hfftFzifQowPtvqY.\u0023\u003Dzop4x1aU\u003D = _param1;
    \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsr9VW2SiWlaFw0wjAU\u003D ssr9Vw2SiWlaFw0wjAu = this.\u0023\u003DzQ6xddArfD502();
    try
    {
      double width = this.Width;
      double height = this.Height;
      hfftFzifQowPtvqY.\u0023\u003Dzk1hW\u0024Z4\u003D = new PrintDialog();
      if (!hfftFzifQowPtvqY.\u0023\u003Dzk1hW\u0024Z4\u003D.ShowDialog().GetValueOrDefault())
        return;
      this.\u0023\u003DzP3DHJfIEl0iL(hfftFzifQowPtvqY.\u0023\u003Dzk1hW\u0024Z4\u003D.PrintableAreaWidth, hfftFzifQowPtvqY.\u0023\u003Dzk1hW\u0024Z4\u003D.PrintableAreaHeight);
      ((DispatcherObject) this).Dispatcher.BeginInvoke((Delegate) new Action(hfftFzifQowPtvqY.\u0023\u003DzwAbd934jQU3AcJ7xVQ\u003D\u003D), Array.Empty<object>());
    }
    finally
    {
      this.\u0023\u003DzB9Fp01Mv4lWu(ssr9Vw2SiWlaFw0wjAu);
    }
  }

  protected override void \u0023\u003Dz4NN4xmXb4DExeo6zN7elWhI\u003D()
  {
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("", new object[2]
    {
      (object) this.ActualWidth,
      (object) this.ActualHeight
    });
    this.\u0023\u003DzlisSOG7yPz_K.\u0023\u003DzosHqOAc\u003D<\u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_jomZOYO0q8PJEzIbNOUHHd4U5XZg\u003D\u003D>(new \u0023\u003DzFXfXgyJ9DFiOo1IYbwdMA_jomZOYO0q8PJEzIbNOUHHd4U5XZg\u003D\u003D((object) this));
    base.\u0023\u003Dz4NN4xmXb4DExeo6zN7elWhI\u003D();
  }

  protected override void \u0023\u003DzUq0D2jBe2UY\u0024(DependencyPropertyChangedEventArgs _param1)
  {
    if (this.ChartModifier == null)
      return;
    this.ChartModifier.DataContext = _param1.NewValue;
  }

  private static void \u0023\u003Dz2I6m3INvNdYt(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D ii8u0KoV3jCaMdYpfetQ = (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) _param0;
    \u0023\u003Dz3RRntx4pzkd854dIVpLK6Ww8ODIV2zPrRw\u003D\u003D newValue = _param1.NewValue as \u0023\u003Dz3RRntx4pzkd854dIVpLK6Ww8ODIV2zPrRw\u003D\u003D;
    if (_param1.OldValue is \u0023\u003Dz3RRntx4pzkd854dIVpLK6Ww8ODIV2zPrRw\u003D\u003D oldValue)
      oldValue.\u0023\u003Dzpcs_ok3YoH9BrujbKxTSzYg\u003D();
    newValue?.\u0023\u003DzPA8CxqX98AVD701gF5MzwGc\u003D(ii8u0KoV3jCaMdYpfetQ);
    ii8u0KoV3jCaMdYpfetQ.\u0023\u003Dz5q8i9C4\u003D();
  }

  private static void \u0023\u003DzTwHOANN3ITOI(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd) _param0;
    \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D newValue = _param1.NewValue as \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D;
    if (_param1.OldValue is \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D oldValue)
    {
      oldValue.ParentSurface = (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) null;
      oldValue.CollectionChanged -= dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzyRWKaHIr4Mj4SoYRZA\u003D\u003D ?? (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzyRWKaHIr4Mj4SoYRZA\u003D\u003D = new NotifyCollectionChangedEventHandler(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzdb4OQr1\u0024A5Qg));
    }
    if (newValue != null)
    {
      newValue.ParentSurface = (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) elwvdvgwnmJ5AjuaEjd;
      newValue.CollectionChanged += dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzyRWKaHIr4Mj4SoYRZA\u003D\u003D ?? (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzj2IY6aE\u003D.\u0023\u003DzyRWKaHIr4Mj4SoYRZA\u003D\u003D = new NotifyCollectionChangedEventHandler(dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzdb4OQr1\u0024A5Qg));
      elwvdvgwnmJ5AjuaEjd.\u0023\u003Dz5q8i9C4\u003D();
    }
    if (elwvdvgwnmJ5AjuaEjd.ChartModifier == null)
      return;
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzdb4OQr1\u0024A5Qg((object) elwvdvgwnmJ5AjuaEjd, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
  }

  private static void \u0023\u003Dzdb4OQr1\u0024A5Qg(
    object _param0,
    NotifyCollectionChangedEventArgs _param1)
  {
    \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D ii8u0KoV3jCaMdYpfetQ = _param0 is \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D jh5kehUwCFhZpDlpYa ? jh5kehUwCFhZpDlpYa.ParentSurface : _param0 as \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D;
    if (ii8u0KoV3jCaMdYpfetQ == null || ii8u0KoV3jCaMdYpfetQ.get_ChartModifier() == null)
      return;
    ii8u0KoV3jCaMdYpfetQ.get_ChartModifier().\u0023\u003Dzok6jmLaiH5ai(_param0, _param1);
  }

  private static void \u0023\u003DzqAA3vS5ME5QQOVXt1JQV8cO54LGA(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd) _param0;
    using (elwvdvgwnmJ5AjuaEjd.SuspendUpdates())
    {
      elwvdvgwnmJ5AjuaEjd.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D.Clear();
      ObservableCollection<IRenderableSeries> newValue = _param1.NewValue as ObservableCollection<IRenderableSeries>;
      if (_param1.OldValue is ObservableCollection<IRenderableSeries> oldValue)
        oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(elwvdvgwnmJ5AjuaEjd.\u0023\u003DzlRPtJNE7UDOTZfXPK13GjY4\u003D);
      if (newValue == null)
        return;
      newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(elwvdvgwnmJ5AjuaEjd.\u0023\u003DzlRPtJNE7UDOTZfXPK13GjY4\u003D);
      newValue.\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(((Collection<IRenderableSeries>) elwvdvgwnmJ5AjuaEjd.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D).Add));
    }
  }

  private static void \u0023\u003Dzw83elPE6IaNqQIYgfw\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd) _param0;
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D newValue = _param1.NewValue as \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D;
    if (_param1.OldValue is \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D oldValue)
    {
      foreach (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB in (Collection<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>) oldValue)
        elwvdvgwnmJ5AjuaEjd.\u0023\u003Dzy9wEH1BYapxA(dynWmoFzgH4RlWB0lB);
      oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(elwvdvgwnmJ5AjuaEjd.\u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D);
    }
    if (newValue != null)
    {
      newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(elwvdvgwnmJ5AjuaEjd.\u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D);
      foreach (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB in (Collection<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>) newValue)
        elwvdvgwnmJ5AjuaEjd.\u0023\u003DzoHEODaiFnJb8(dynWmoFzgH4RlWB0lB, false);
    }
    if (elwvdvgwnmJ5AjuaEjd.Annotations != null)
      elwvdvgwnmJ5AjuaEjd.Annotations.\u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D((object) elwvdvgwnmJ5AjuaEjd, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    if (elwvdvgwnmJ5AjuaEjd.ChartModifier == null)
      return;
    elwvdvgwnmJ5AjuaEjd.ChartModifier.\u0023\u003DzBwMZf3Z18v_xRgVWSw\u003D\u003D((object) elwvdvgwnmJ5AjuaEjd, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
  }

  private static void \u0023\u003Dz6cV0sTdoa0aXB7oEvg\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd) _param0;
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D newValue = _param1.NewValue as \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D;
    if (_param1.OldValue is \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D oldValue)
    {
      foreach (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB in (Collection<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>) oldValue)
        elwvdvgwnmJ5AjuaEjd.\u0023\u003Dzy9wEH1BYapxA(dynWmoFzgH4RlWB0lB);
      oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(elwvdvgwnmJ5AjuaEjd.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D);
    }
    if (newValue != null)
    {
      newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(elwvdvgwnmJ5AjuaEjd.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D);
      foreach (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB dynWmoFzgH4RlWB0lB in (Collection<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB>) newValue)
        elwvdvgwnmJ5AjuaEjd.\u0023\u003DzoHEODaiFnJb8(dynWmoFzgH4RlWB0lB, true);
    }
    if (elwvdvgwnmJ5AjuaEjd.Annotations != null)
      elwvdvgwnmJ5AjuaEjd.Annotations.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D((object) elwvdvgwnmJ5AjuaEjd, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    if (elwvdvgwnmJ5AjuaEjd.ChartModifier == null)
      return;
    elwvdvgwnmJ5AjuaEjd.ChartModifier.\u0023\u003DzMQsNWQqev3ol8vdG1w\u003D\u003D((object) elwvdvgwnmJ5AjuaEjd, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
  }

  private static void \u0023\u003DzUYXUmgjXHZra(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd) _param0;
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB oldValue = _param1.OldValue as \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB;
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB newValue = _param1.NewValue as \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB;
    if (oldValue != null)
      elwvdvgwnmJ5AjuaEjd.YAxes.Remove(oldValue);
    if (newValue == null)
      return;
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("", Array.Empty<object>());
    elwvdvgwnmJ5AjuaEjd.YAxes.Insert(0, newValue);
  }

  private static void \u0023\u003Dz2xH_KOTJk1FH(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd) _param0;
    if (_param1.OldValue is \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB oldValue)
      elwvdvgwnmJ5AjuaEjd.XAxes.Remove(oldValue);
    if (!(_param1.NewValue is \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB newValue))
      return;
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("", Array.Empty<object>());
    elwvdvgwnmJ5AjuaEjd.XAxes.Insert(0, newValue);
  }

  private static void \u0023\u003DzEXdV0ADnLt07AffGX3qcEdc\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd))
      return;
    \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZbHiFZEipPcMLCVdoP4\u003D hiFzEipPcMlcVdoP4 = (bool) _param1.NewValue ? \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZbHiFZEipPcMLCVdoP4\u003D.Polar : \u0023\u003Dz0w5QTi_Hwx2Q\u0024WqRdQ\u0024aZbHiFZEipPcMLCVdoP4\u003D.Cartesian;
    elwvdvgwnmJ5AjuaEjd.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().\u0023\u003DzosHqOAc\u003D<\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMyyFQ23l9dmBE_BFsxBElcEz4>(new \u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMyyFQ23l9dmBE_BFsxBElcEz4((object) elwvdvgwnmJ5AjuaEjd, hiFzEipPcMlcVdoP4));
  }

  private static void \u0023\u003DzYXVDCElLYMeX(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd) _param0;
    if (elwvdvgwnmJ5AjuaEjd.\u0023\u003DzTRL\u0024Xy0vYDigfJ9YNg\u003D\u003D() is dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLTCNAKZHF2LCBZNSWLDVPYTWWEKD4_ejd lcbznswldvpytwwekD4Ejd)
      lcbznswldvpytwwekD4Ejd.Style = elwvdvgwnmJ5AjuaEjd.GridLinesPanelStyle;
    if (elwvdvgwnmJ5AjuaEjd.RenderSurface is \u0023\u003DzYB09msiytIDFpDsyaHpANNcjrPNql900yccNyuZHBUW6 renderSurface)
      renderSurface.\u0023\u003DzlqgXLHiucpqc(elwvdvgwnmJ5AjuaEjd.RenderSurfaceStyle);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("", Array.Empty<object>());
  }

  protected override void \u0023\u003DzbJLRM0DSJETF\u0024zflkg\u003D\u003D(
    DependencyPropertyChangedEventArgs _param1)
  {
    base.\u0023\u003DzbJLRM0DSJETF\u0024zflkg\u003D\u003D(_param1);
    if (_param1.OldValue is \u0023\u003DzYB09msiytIDFpDsyaHpANNcjrPNql900yccNyuZHBUW6 oldValue)
    {
      oldValue.\u0023\u003DzvM8pYfLF8h8E(new EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy>(this.\u0023\u003DzeKdxSypd_FVL));
      oldValue.\u0023\u003DzrEoWi5uPS0Yz((\u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D) null);
      oldValue.\u0023\u003Dzqtb9toLjXu0t();
      oldValue.Dispose();
    }
    if (_param1.NewValue is \u0023\u003DzYB09msiytIDFpDsyaHpANNcjrPNql900yccNyuZHBUW6 newValue)
    {
      newValue.\u0023\u003DzQ_ByYlCf\u0024Kac(new EventHandler<\u0023\u003DzawTMm83sNsuVHdgLsihy4QTJhW0jm4VXhKnou19_nziy>(this.\u0023\u003DzeKdxSypd_FVL));
      newValue.\u0023\u003DzrEoWi5uPS0Yz(this.\u0023\u003Dzu\u0024P3XgkcE7BC());
      if (this.RenderSurfaceStyle != null)
        newValue.\u0023\u003DzlqgXLHiucpqc(this.RenderSurfaceStyle);
      newValue.\u0023\u003DzJoneIt0\u003D((IEnumerable<IRenderableSeries>) this.RenderableSeries);
    }
    this.\u0023\u003DzMZpCUE8eL2X8 = newValue;
  }

  private static void \u0023\u003DzURlSYmINpP\u0024HWseZpg\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd) _param0;
    ObservableCollection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D> newValue = _param1.NewValue as ObservableCollection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>;
    if (_param1.OldValue is ObservableCollection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D> oldValue)
    {
      foreach (\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D koh9jO5RuUcFiAqLc in (Collection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>) oldValue)
        elwvdvgwnmJ5AjuaEjd.\u0023\u003DzzAzmhJu\u00240jW8(koh9jO5RuUcFiAqLc);
      oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(elwvdvgwnmJ5AjuaEjd.\u0023\u003DzCL4dS9OFXaqI);
    }
    if (newValue == null)
      return;
    newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(elwvdvgwnmJ5AjuaEjd.\u0023\u003DzCL4dS9OFXaqI);
    foreach (\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D koh9jO5RuUcFiAqLc in (Collection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>) newValue)
      elwvdvgwnmJ5AjuaEjd.\u0023\u003Dzog1ZIjwbjbmK(koh9jO5RuUcFiAqLc);
  }

  private static void \u0023\u003DzD3CxC6LQYAec(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = (dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd) _param0;
    if (_param1.OldValue is \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D oldValue)
      elwvdvgwnmJ5AjuaEjd.\u0023\u003DzSksGoLn96rP3(oldValue);
    if (_param1.NewValue is \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D newValue)
      elwvdvgwnmJ5AjuaEjd.\u0023\u003Dzo3mAQ8z0oY6l(newValue);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("", Array.Empty<object>());
    elwvdvgwnmJ5AjuaEjd.\u0023\u003Dz5q8i9C4\u003D();
  }

  internal \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D \u0023\u003DzlwAvJczjNvUE(
    IRenderableSeries _param1)
  {
    throw new NotImplementedException("");
  }

  internal IEnumerable<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D> \u0023\u003DzlwAvJczjNvUE(
    string _param1)
  {
    return (IEnumerable<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D>) new dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz_Echccw8OyZAdo96L0cd918\u003D(-2)
    {
      \u0023\u003DzRRvwDu67s9Rm = this,
      \u0023\u003Dz62suKlK33zDjzYMs3Q\u003D\u003D = _param1
    };
  }

  protected override void \u0023\u003DzLIrH2H7exOOX(
    \u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D _param1)
  {
    base.\u0023\u003DzLIrH2H7exOOX(_param1);
    _param1.\u0023\u003Dz7wSH25w\u003D<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyNMn5sYJZKzXDppYgrIuwPB>((\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyNMn5sYJZKzXDppYgrIuwPB) new \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSsG9Ekw_rh0WrSEo3VvZ6\u00248Y(this));
    _param1.\u0023\u003Dz7wSH25w\u003D<\u0023\u003Dz\u0024xWvhP6es1QcQRB94rX4cNVVgfiVZTLWRhl\u00244Uw\u003D>((\u0023\u003Dz\u0024xWvhP6es1QcQRB94rX4cNVVgfiVZTLWRhl\u00244Uw\u003D) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vgE1y_16\u0024Ql8QLBV6E\u003D());
    _param1.\u0023\u003Dz7wSH25w\u003D<\u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7WIKy8yFmS0qz5aG2LjQ9ZhaLqHaajO4_nAIgryYYasWa8dMpfY\u003D>((\u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7WIKy8yFmS0qz5aG2LjQ9ZhaLqHaajO4_nAIgryYYasWa8dMpfY\u003D) new \u0023\u003DzPql\u0024onrPHiHfWZj7w2jaKmkEzvtrSPmPXCypCYSvlJxk\u0024K3oG\u0024G8T9G0Kw\u0024aeAQVBAnAZzg\u003D());
    _param1.\u0023\u003Dz7wSH25w\u003D<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6S86EZeND1KSf7Q5ckAbN6LxyEWNToOUjo1\u00243K\u00241Ho2jpA\u003D\u003D>((\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6S86EZeND1KSf7Q5ckAbN6LxyEWNToOUjo1\u00243K\u00241Ho2jpA\u003D\u003D) new \u0023\u003DzMLvZWaqDqEKovfY1GVv1jIthZnhPHuHdNTeekcYACCJOAHdnvwa8qvrQ_TgmYt5MkQ\u003D\u003D());
    _param1.\u0023\u003Dz7wSH25w\u003D<\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D>((\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) this);
    _param1.\u0023\u003Dz7wSH25w\u003D<\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D>((\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D) new \u0023\u003DzXx19aMi46NZ0khxEGqLHPA9GkqwI680fTt\u0024V_4g\u003D(this));
    this.\u0023\u003DzlisSOG7yPz_K = _param1.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>();
    this.\u0023\u003DzlisSOG7yPz_K.\u0023\u003DzZcbqdpE\u003D<\u0023\u003DzAuXtmwo_UFdzWVVSiImlM2m3\u0024u6KUwtD4Q\u003D\u003D>(new Action<\u0023\u003DzAuXtmwo_UFdzWVVSiImlM2m3\u0024u6KUwtD4Q\u003D\u003D>(this.\u0023\u003DzlsbMLWTG6\u0024uOBbAnnH_p7Wc\u003D), true);
    this.\u0023\u003DzlisSOG7yPz_K.\u0023\u003DzZcbqdpE\u003D<\u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B>(new Action<\u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B>(this.\u0023\u003DzUXod0Df2Zg_aX3wExpLQ2\u0024Y\u003D), true);
    this.\u0023\u003DzKPDuUZmOc92KysMGPQ\u003D\u003D = _param1.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzEJoJjwSelM_K3zbmiw1OAyNMn5sYJZKzXDppYgrIuwPB>();
  }

  private void \u0023\u003Dzo3mAQ8z0oY6l(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
  {
    if (_param1.IsAttached)
      this.\u0023\u003DzSksGoLn96rP3(_param1);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("", new object[1]
    {
      (object) _param1.GetType()
    });
    this.\u0023\u003DzBEVA4HwNdiRzJVOjWA\u003D\u003D(_param1);
    _param1.ParentSurface = (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) this;
    _param1.Services = this.\u0023\u003Dzu\u0024P3XgkcE7BC();
    if (this.\u0023\u003Dzwc4Gzka23TGB() != null)
      this.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003Dz\u0024xWvhP6es1QcQRB94rX4cNVVgfiVZTLWRhl\u00244Uw\u003D>().\u0023\u003DzZcbqdpE\u003D((\u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV) this.\u0023\u003Dzwc4Gzka23TGB(), (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpOgj\u0024HEwAG4ZlfwSGT7i2APW) _param1);
    _param1.DataContext = this.DataContext;
    _param1.IsAttached = true;
    _param1.OnAttached();
  }

  private void \u0023\u003DzBEVA4HwNdiRzJVOjWA\u003D\u003D(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
  {
    if (!(_param1 is FrameworkElement frameworkElement) || frameworkElement.Parent != null)
      return;
    frameworkElement.Visibility = Visibility.Collapsed;
    this.\u0023\u003Dzwc4Gzka23TGB().\u0023\u003DzH0osWQkV_Y8_((object) frameworkElement, -1);
  }

  private void \u0023\u003Dzc361Wm8dSdGbJ8oq6w\u003D\u003D(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
  {
    if (!(_param1 is FrameworkElement frameworkElement) || frameworkElement.Parent != this.\u0023\u003Dzwc4Gzka23TGB())
      return;
    this.\u0023\u003Dzwc4Gzka23TGB().\u0023\u003DziYdJ\u00246cCiBha((object) frameworkElement);
  }

  private void \u0023\u003DzSksGoLn96rP3(
    \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWhChhAr3Kksm46UY2ZY\u003D _param1)
  {
    if (!_param1.IsAttached)
      return;
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("", new object[1]
    {
      (object) _param1.GetType()
    });
    this.\u0023\u003DzVM1_npDqI38t();
    _param1.OnDetached();
    _param1.ParentSurface = (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) null;
    _param1.Services = (\u0023\u003Dz_QZ2gpRafNgOtcPtA9qy6nUSt5OBncbXZA\u003D\u003D) null;
    _param1.IsAttached = false;
    this.\u0023\u003Dzc361Wm8dSdGbJ8oq6w\u003D\u003D(_param1);
  }

  private void \u0023\u003DzVM1_npDqI38t()
  {
    if (this.\u0023\u003Dzwc4Gzka23TGB() == null || this.\u0023\u003Dzu\u0024P3XgkcE7BC() == null)
      return;
    this.\u0023\u003Dzu\u0024P3XgkcE7BC().\u0023\u003Dz2VqWonc\u003D<\u0023\u003Dz\u0024xWvhP6es1QcQRB94rX4cNVVgfiVZTLWRhl\u00244Uw\u003D>().\u0023\u003DzfttffOE\u003D((\u0023\u003DzQ4iRj1YTApc8D349VbLPOXcxSYN1XwlnLQBsgQeCUZnV) this.\u0023\u003Dzwc4Gzka23TGB());
  }

  private void \u0023\u003Dz_2Z37e3SOi5eNmQj2g\u003D\u003D(
    object _param1,
    PropertyChangedEventArgs _param2)
  {
    \u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D koh9jO5RuUcFiAqLc = (\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D) _param1;
    int index = this.SeriesSource.IndexOf(koh9jO5RuUcFiAqLc);
    if (index == -1)
    {
      koh9jO5RuUcFiAqLc.PropertyChanged -= new PropertyChangedEventHandler(this.\u0023\u003Dz_2Z37e3SOi5eNmQj2g\u003D\u003D);
    }
    else
    {
      if (_param2.PropertyName == "")
      {
        \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) koh9jO5RuUcFiAqLc.DataSeries, "");
        if (koh9jO5RuUcFiAqLc.RenderSeries != null)
          koh9jO5RuUcFiAqLc.RenderSeries.set_DataSeries(koh9jO5RuUcFiAqLc.DataSeries);
      }
      if (!(_param2.PropertyName == ""))
        return;
      if (koh9jO5RuUcFiAqLc.RenderSeries == null)
      {
        this.RenderableSeries.RemoveAt(index);
      }
      else
      {
        this.RenderableSeries[index] = koh9jO5RuUcFiAqLc.RenderSeries;
        koh9jO5RuUcFiAqLc.RenderSeries.set_DataSeries(koh9jO5RuUcFiAqLc.DataSeries);
      }
    }
  }

  private void \u0023\u003DzCL4dS9OFXaqI(object _param1, NotifyCollectionChangedEventArgs _param2)
  {
    using (this.SuspendUpdates())
    {
      if (_param2.Action == NotifyCollectionChangedAction.Reset)
        this.RenderableSeries.Clear();
      if (_param2.OldItems != null)
        _param2.OldItems.Cast<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>().\u0023\u003Dz30RSSSygABj_<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>(new Action<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>(this.\u0023\u003DzzAzmhJu\u00240jW8));
      if (_param2.NewItems == null)
        return;
      _param2.NewItems.Cast<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>().\u0023\u003Dz30RSSSygABj_<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>(new Action<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D>(this.\u0023\u003Dzog1ZIjwbjbmK));
    }
  }

  public dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003DzTUFNh6E2QDAjxphyfw\u003D\u003D()
  {
    return this.\u0023\u003Dz0YK5mfh0G_wP;
  }

  public dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003DzxzuKb2Lb2Gtlzy1zQA\u003D\u003D()
  {
    return this.\u0023\u003DzK\u0024t4oL\u0024CAaKU;
  }

  public dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003Dz3ALyJldLTYC8rUKNSw\u003D\u003D()
  {
    return this.\u0023\u003DzGFGsUr_xxE4H;
  }

  public dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003Dz7jQDxh7oPk4IxxFHHA\u003D\u003D()
  {
    return this.\u0023\u003DztKgn6GxCcL\u0024B;
  }

  public dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003Dz7pyJs9hGLLCXVUwrCA\u003D\u003D()
  {
    return this.\u0023\u003Dz1mGpFN8tTa5PB1b5eA\u003D\u003D;
  }

  public dje_zCK22KM22D3NQP9CPE8LK3RM7LVMDSW44P9Q363YG78KM6T7DKNGTA_ejd \u0023\u003DzISmOsrinWHM9BHkJ6g\u003D\u003D()
  {
    return this.\u0023\u003DzkW\u0024qJVeNKX1VtemTog\u003D\u003D;
  }

  internal \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUtzzzd5rSNXl95sF5MghysRDMZyklVKg61SC2QL8 \u0023\u003DzGqYqkF73Z9yr0zUWMg\u003D\u003D()
  {
    return this.\u0023\u003DzBF9EFrE8iOBKBpnp2YeZjCs\u003D;
  }

  internal void \u0023\u003DzB\u0024taTl5OHzRG5U3QXw\u003D\u003D(
    \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUtzzzd5rSNXl95sF5MghysRDMZyklVKg61SC2QL8 _param1)
  {
    this.\u0023\u003DzBF9EFrE8iOBKBpnp2YeZjCs\u003D = _param1;
  }

  internal \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKg1uYp9C5FyfAbaz1_Vr5UELTst58YqUlllvt2EY65UXJw\u003D\u003D \u0023\u003DzZ5VDq\u0024Vzik5LlJ4Fd5jKuBg\u003D()
  {
    return this.\u0023\u003DzUpDNoiuyFkQgKnG1rLeDiyFufOOLPECfpQ\u003D\u003D;
  }

  internal void \u0023\u003DzPUfITvj9OihLWDW1T8K_pLs\u003D(
    \u0023\u003DzVsUQ9A_2kGjOa2mh\u00241UNKg1uYp9C5FyfAbaz1_Vr5UELTst58YqUlllvt2EY65UXJw\u003D\u003D _param1)
  {
    this.\u0023\u003DzUpDNoiuyFkQgKnG1rLeDiyFufOOLPECfpQ\u003D\u003D = _param1;
  }

  internal dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dzym7l7vrt6xywpseFzgnpRX8\u003D \u0023\u003DzkUqP45_cbI3uaDDcHzV3Yqclunb3()
  {
    return this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D;
  }

  internal HashSet<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D> \u0023\u003DzMdHN\u0024fr78SvYPfTQ5A\u003D\u003D()
  {
    return this.\u0023\u003DzL79ssDGYe7GX;
  }

  bool \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv1ei5a44WdHi6c16UXGWhmY1mMHOZA\u003D\u003D.\u0023\u003Dz59_koqr2EQdapDcFKycZuEkqSVjDl1YEst5SQzVFpw8OzdcTdx\u0024O8XAY037X()
  {
    return this.IsVisible;
  }

  private void \u0023\u003DzS2ufUgCmj4iLhzxXcG9wL_0\u003D(object _param1)
  {
    this.\u0023\u003Dz5v2P2MZQ6Mg5pHstYQ\u003D\u003D(TimeSpan.FromMilliseconds(500.0));
  }

  private void \u0023\u003Dz6fV1owXqTlzBWkKTRBWCs_aD\u0024Qt79YCLHsMl_d0\u003D(
    IRenderableSeries _param1)
  {
    this.\u0023\u003DzAv5_jWmna8cmcLsfgD0Ew8k\u003D.Remove(_param1);
  }

  private void \u0023\u003DzJU3JfpRgwPjQa9jDcK3_BJ5p46XJKnhSkbPsgKE\u003D(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
  {
    this.\u0023\u003DzoHEODaiFnJb8(_param1, true);
  }

  private void \u0023\u003Dz1cXbEwF3yU52tbr4hhxqLCeJfg\u0024S5cdqfXumnuU\u003D(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
  {
    this.\u0023\u003DzoHEODaiFnJb8(_param1, false);
  }

  private void \u0023\u003DzlsbMLWTG6\u0024uOBbAnnH_p7Wc\u003D(
    \u0023\u003DzAuXtmwo_UFdzWVVSiImlM2m3\u0024u6KUwtD4Q\u003D\u003D _param1)
  {
    if (_param1.\u0023\u003DzXMS_5Qya5w0n())
      this.\u0023\u003Dz7mFu4O\u0024TokaR();
    else
      this.\u0023\u003Dzn72LMZ0738BY();
  }

  private void \u0023\u003DzUXod0Df2Zg_aX3wExpLQ2\u0024Y\u003D(
    \u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B _param1)
  {
    this.\u0023\u003Dz5q8i9C4\u003D();
  }

  private sealed class \u0023\u003Dz041D7jmffErIs2Wft03rpCc\u003D
  {
    public dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd \u0023\u003DzRRvwDu67s9Rm;
    public Action \u0023\u003Dz5vxB7hs\u003D;

    internal void \u0023\u003DzPEXAppbcaBgnas\u00248pco9O5Y\u003D()
    {
      // ISSUE: explicit non-virtual call
      object obj = __nonvirtual (this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dzjatnj7TNvda7());
      Monitor.Enter(obj);
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzOB4dXYeIFfiB();
      Monitor.Exit(obj);
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzwRf5PdbT802y = false;
    }

    internal void \u0023\u003Dz7n\u0024Gp4RnkI3hGyrPb7eAGT0\u003D()
    {
      \u0023\u003DzyhcKXWtKNGzWjpotzZwU9whBU9Yw699_DztiM3x3BrpxyflUng\u003D\u003D dztiM3x3BrpxyflUng = new \u0023\u003DzyhcKXWtKNGzWjpotzZwU9whBU9Yw699_DztiM3x3BrpxyflUng\u003D\u003D(this.\u0023\u003Dz5vxB7hs\u003D);
    }
  }

  private sealed class \u0023\u003Dz7c5Jf9Xcn7g7gyAWka3cVyc\u003D
  {
    public bool \u0023\u003DzPhfEcNQm65OS;

    internal bool \u0023\u003Dzn\u0024oF_i5RJAgifiBFYicprPI\u003D(
      \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
    {
      return _param1.\u0023\u003DzFrVmckt\u0024NpG6() == this.\u0023\u003DzPhfEcNQm65OS;
    }
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D, bool> \u0023\u003Dzoz5TxoGG1694NBizKw\u003D\u003D;
    public static Func<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, bool> \u0023\u003DziZ6WqO9c0bLyreXcPw\u003D\u003D;
    public static Func<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB, bool> \u0023\u003Dz2u81qtV0w4N3y6_wTA\u003D\u003D;
    public static Action<FrameworkElement> \u0023\u003DzkVHywpw3evyESZQ1iA\u003D\u003D;

    internal bool \u0023\u003DzoZM1fSvQVSQ7YXyQfMSGaGZWjDLCOmVAEA\u003D\u003D(
      \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D _param1)
    {
      return _param1.IsDefined;
    }

    internal bool \u0023\u003DzHqCL4TMIgkX9lgbfbVwoLztjktmcnH1IkQ\u003D\u003D(
      \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
    {
      return _param1.get_IsPolarAxis();
    }

    internal bool \u0023\u003DzqleqgxOwnD86h0g7xgV6jwj2FfjN9XQ2Tw\u003D\u003D(
      \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
    {
      return _param1.get_IsPolarAxis();
    }

    internal void \u0023\u003DzSnploAESTvQvxQJvSegWmmQ\u003D(FrameworkElement _param1)
    {
      _param1.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
    }
  }

  private sealed class \u0023\u003Dz_Echccw8OyZAdo96L0cd918\u003D : 
    IEnumerable<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D>,
    IEnumerable,
    IEnumerator<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D>,
    IEnumerator,
    IDisposable
  {
    
    private int \u0023\u003Dz4fzyEZ1SsHYa;
    
    private \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D \u0023\u003Dzaev1bhaFFIDX;
    
    private int \u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D;
    
    public dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd \u0023\u003DzRRvwDu67s9Rm;
    
    private string \u0023\u003Dzb0xKp\u0024w\u003D;
    
    public string \u0023\u003Dz62suKlK33zDjzYMs3Q\u003D\u003D;
    
    private IEnumerator<IRenderableSeries> \u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D;

    [DebuggerHidden]
    public \u0023\u003Dz_Echccw8OyZAdo96L0cd918\u003D(int _param1)
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = _param1;
      this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D = Environment.CurrentManagedThreadId;
    }

    [DebuggerHidden]
    void IDisposable.\u0023\u003DzyDgD8d_Zy8d21234Xw\u003D\u003D()
    {
      switch (this.\u0023\u003Dz4fzyEZ1SsHYa)
      {
        case -3:
        case 1:
          try
          {
          }
          finally
          {
            this.\u0023\u003Dzs9oL4F7laYDny0tTHw\u003D\u003D();
          }
          break;
      }
    }

    bool IEnumerator.MoveNext()
    {
      // ISSUE: fault handler
      try
      {
        int z4fzyEz1SsHya = this.\u0023\u003Dz4fzyEZ1SsHYa;
        dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd zRrvwDu67s9Rm = this.\u0023\u003DzRRvwDu67s9Rm;
        switch (z4fzyEz1SsHya)
        {
          case 0:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
            // ISSUE: explicit non-virtual call
            if (__nonvirtual (zRrvwDu67s9Rm.RenderableSeries) == null)
              return false;
            // ISSUE: explicit non-virtual call
            this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D = __nonvirtual (zRrvwDu67s9Rm.RenderableSeries).GetEnumerator();
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            break;
          case 1:
            this.\u0023\u003Dz4fzyEZ1SsHYa = -3;
            break;
          default:
            return false;
        }
        while (this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D.MoveNext())
        {
          IRenderableSeries current = this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D.Current;
          if (!(current.get_YAxisId() != this.\u0023\u003Dzb0xKp\u0024w\u003D) && current.IsVisible)
          {
            \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D dataSeries = current.get_DataSeries();
            if (dataSeries != null)
            {
              this.\u0023\u003Dzaev1bhaFFIDX = dataSeries;
              this.\u0023\u003Dz4fzyEZ1SsHYa = 1;
              return true;
            }
          }
        }
        this.\u0023\u003Dzs9oL4F7laYDny0tTHw\u003D\u003D();
        this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D = (IEnumerator<IRenderableSeries>) null;
        return false;
      }
      __fault
      {
        this.\u0023\u003DzyDgD8d_Zy8d21234Xw\u003D\u003D();
      }
    }

    private void \u0023\u003Dzs9oL4F7laYDny0tTHw\u003D\u003D()
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
      if (this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D == null)
        return;
      this.\u0023\u003DzOh2sOcw05f8kGtMekg\u003D\u003D.Dispose();
    }

    [DebuggerHidden]
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D IEnumerator<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D>.\u0023\u003DzEEqjFyKghwogrkqtjZQX\u00240Nsr40kBtDKNIeV66\u0024ZNExZqEP8hUuVzLwO8H\u0024RcOjQiA\u003D\u003D()
    {
      return this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    void IEnumerator.\u0023\u003Dz__yDkd4DQlAhNe9vxQ\u003D\u003D()
    {
      throw new NotSupportedException();
    }

    [DebuggerHidden]
    object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
    {
      return (object) this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator<
    #nullable disable
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D> IEnumerable<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D>.\u0023\u003Dz_KshCohrxuVJkfLqw0AvWWrTaY6cXjYVhgO4bRzo1ywFV3nVDl2NucbzPyYsesEV0w\u003D\u003D()
    {
      dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz_Echccw8OyZAdo96L0cd918\u003D echccw8OyZado96L0cd918;
      if (this.\u0023\u003Dz4fzyEZ1SsHYa == -2 && this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D == Environment.CurrentManagedThreadId)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = 0;
        echccw8OyZado96L0cd918 = this;
      }
      else
      {
        echccw8OyZado96L0cd918 = new dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd.\u0023\u003Dz_Echccw8OyZAdo96L0cd918\u003D(0);
        echccw8OyZado96L0cd918.\u0023\u003DzRRvwDu67s9Rm = this.\u0023\u003DzRRvwDu67s9Rm;
      }
      echccw8OyZado96L0cd918.\u0023\u003Dzb0xKp\u0024w\u003D = this.\u0023\u003Dz62suKlK33zDjzYMs3Q\u003D\u003D;
      return (IEnumerator<\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D>) echccw8OyZado96L0cd918;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      return (IEnumerator) this.\u0023\u003Dz_KshCohrxuVJkfLqw0AvWWrTaY6cXjYVhgO4bRzo1ywFV3nVDl2NucbzPyYsesEV0w\u003D\u003D();
    }
  }

  private sealed class \u0023\u003DzafpfrngHfftFzifQOwPtvqY\u003D
  {
    public 
    #nullable disable
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd \u0023\u003DzRRvwDu67s9Rm;
    public string \u0023\u003Dzop4x1aU\u003D;
    public PrintDialog \u0023\u003Dzk1hW\u0024Z4\u003D;

    internal void \u0023\u003DzwAbd934jQU3AcJ7xVQ\u003D\u003D()
    {
      this.\u0023\u003Dzk1hW\u0024Z4\u003D.PrintVisual((Visual) this.\u0023\u003DzRRvwDu67s9Rm, this.\u0023\u003Dzop4x1aU\u003D);
    }
  }

  private static class \u0023\u003Dzj2IY6aE\u003D
  {
    public static Action<\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB> \u0023\u003DzCNnJa2MB\u00244vpz715iA\u003D\u003D;
    public static NotifyCollectionChangedEventHandler \u0023\u003DzyRWKaHIr4Mj4SoYRZA\u003D\u003D;
  }

  private sealed class \u0023\u003DzmHUZq1a0vCkinf2sWxYoWqM\u003D
  {
    public \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzMM5Kl1w\u003D;
    public \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB \u0023\u003DzS7JsfCE\u003D;

    internal \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003Dzv6ZL84K3ljUM9AvngG5_IKXd32Mz(
      \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1)
    {
      return _param1.\u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(this.\u0023\u003DzMM5Kl1w\u003D, this.\u0023\u003DzS7JsfCE\u003D.get_IsLogarithmicAxis());
    }
  }

  internal sealed class \u0023\u003Dzym7l7vrt6xywpseFzgnpRX8\u003D : 
    ObservableCollection<IRenderableSeries>
  {
    
    private readonly dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd \u0023\u003Dzos6SMwAMXZ33;

    public \u0023\u003Dzym7l7vrt6xywpseFzgnpRX8\u003D(
      dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd _param1)
    {
      this.\u0023\u003Dzos6SMwAMXZ33 = _param1;
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs _param1)
    {
      using (this.\u0023\u003Dzos6SMwAMXZ33.SuspendUpdates())
      {
        if (_param1.OldItems != null)
          _param1.OldItems.Cast<IRenderableSeries>().\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(this.\u0023\u003Dzos6SMwAMXZ33.\u0023\u003Dz6fuulLxxSq\u0024eCw5_MNj_8i4\u003D));
        if (_param1.NewItems != null)
          _param1.NewItems.Cast<IRenderableSeries>().\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(this.\u0023\u003Dzos6SMwAMXZ33.\u0023\u003Dzk6sVBMw95BkmcWhiKErWml4\u003D));
      }
      base.OnCollectionChanged(_param1);
    }

    protected override void ClearItems()
    {
      using (this.\u0023\u003Dzos6SMwAMXZ33.SuspendUpdates())
        this.\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(this.\u0023\u003Dzos6SMwAMXZ33.\u0023\u003Dz6fuulLxxSq\u0024eCw5_MNj_8i4\u003D));
      base.ClearItems();
    }
  }
}
