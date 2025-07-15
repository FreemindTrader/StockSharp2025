// Decompiled with JetBrains decompiler
// Type: -.dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable enable
namespace \u002D;

[TemplatePart(Name = "PART_AxisCanvas", Type = typeof (\u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P7\u00243UKiUGVR88DR8huM\u003D))]
[TemplatePart(Name = "PART_ModifierAxisCanvas", Type = typeof (dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd))]
[TemplatePart(Name = "PART_AxisContainer", Type = typeof (StackPanel))]
[TemplatePart(Name = "PART_AxisRenderSurface", Type = typeof (dje_zFYCV8LXGWR39M2R5WW2FCVKF529R9Q6JAUMT5WGC7MM4FFDF2V4HQQMUEFPET6GT7RXT7XFY_ejd))]
internal abstract class dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd : 
  ContentControl,
  IDrawable,
  IXmlSerializable,
  INotifyPropertyChanged,
  IAxisParams,
  ISuspendable,
  IInvalidatableElement,
  IAxis,
  IHitTestable
{
  
  public static readonly 
  #nullable disable
  DependencyProperty \u0023\u003DzfolHRDLbOj27 = DependencyProperty.Register(nameof (TickCoordinatesProvider), typeof (\u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwrm\u0024HOuIKCn_Ala213x1NpKA), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzQsSOj9uli6iK)));
  
  public static readonly DependencyProperty \u0023\u003DzclwKGsykhg0_ = DependencyProperty.Register(nameof (IsStaticAxis), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz1UOyXOfHyEI9)));
  
  public static readonly DependencyProperty \u0023\u003DzVliJ8IIRU5CI = DependencyProperty.Register(nameof (IsPrimaryAxis), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzTs\u0024_r2pUSNyL)));
  
  public static readonly DependencyProperty \u0023\u003DzcwiMJ7NmMskq = DependencyProperty.Register(nameof (IsCenterAxis), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzIgRO8VI5TESl1w9T\u0024A\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzHM9KAGx9Av_I = DependencyProperty.Register(nameof (AxisMode), typeof (\u0023\u003DzzKBN5TXUMNIGpWrDrUMXSdQaIc96_52nMJQUgeTEfBP3), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSdQaIc96_52nMJQUgeTEfBP3.Linear));
  
  public static readonly DependencyProperty \u0023\u003DzKm2_RDWENeyO = DependencyProperty.Register(nameof (AutoRange), typeof (dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Once, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzuTkKbr18L_Ur = DependencyProperty.Register(nameof (MajorDelta), typeof (IComparable), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzdg3vng1nptYM = DependencyProperty.Register(nameof (MinorDelta), typeof (IComparable), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzEt7Ui3H_z_JvbwmWQQ\u003D\u003D = DependencyProperty.Register(nameof (MinorsPerMajor), typeof (int), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) 5, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dz3kyPJRWoiKq0 = DependencyProperty.Register(nameof (GrowBy), typeof (IRange<double>), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzWl3LbWhL1z0D = DependencyProperty.Register(nameof (VisibleRange), typeof (IRange), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DziB9pY7CleJnqmfAdVQ\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzW7Xl\u00245Sj1hV\u0024 = DependencyProperty.Register(nameof (VisibleRangeLimit), typeof (IRange), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzqd3jzsY7FQsh5XQziA\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzxPOeDpgt9hi7h0ryrw\u003D\u003D = DependencyProperty.Register(nameof (VisibleRangeLimitMode), typeof (\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D.MinMax));
  
  public static readonly DependencyProperty \u0023\u003DzGqv7OFBibgO2 = DependencyProperty.Register(nameof (AnimatedVisibleRange), typeof (IRange), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzHggGkMOQckHdkG7xQA\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzeSOZpsJteV_v = DependencyProperty.Register("VisibleRangePoint", typeof (Point), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) new Point(), new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz10or8z86voVPLuMIHw\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzFb1oVqzbThlVLIru\u0024A\u003D\u003D = DependencyProperty.Register(nameof (AutoAlignVisibleRange), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzpEX3tswlNvij = DependencyProperty.Register(nameof (MaxAutoTicks), typeof (int), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) 10, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dz4PN8nI\u0024I1O63 = DependencyProperty.Register(nameof (AutoTicks), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dz1bLZaITSYGdx = DependencyProperty.Register(nameof (TickProvider), typeof (ITickProvider), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzQikQKvMZp1xk)));
  
  public static readonly DependencyProperty \u0023\u003DzjWJEoVoxRw8F = DependencyProperty.Register(nameof (MinimalZoomConstrain), typeof (IComparable), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null));
  
  public static readonly DependencyProperty \u0023\u003DzXMV_skc\u003D = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) Orientation.Horizontal, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzcZqyvQl0j0My)));
  
  public static readonly DependencyProperty \u0023\u003DzfMY988N0StOA = DependencyProperty.Register(nameof (AxisAlignment), typeof (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Default, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzoqG1CE2rlL_j)));
  
  public static readonly DependencyProperty \u0023\u003DzD\u0024wXQ8E\u003D = DependencyProperty.Register(nameof (Id), typeof (string), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzhYW6tiLSC0eZ = DependencyProperty.Register(nameof (FlipCoordinates), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzxtYfETQdftTw)));
  
  public static readonly DependencyProperty \u0023\u003Dzbkm16i6vgFnk = DependencyProperty.Register(nameof (LabelProvider), typeof (\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzGSdejefFTNCn)));
  
  public static readonly DependencyProperty \u0023\u003Dzol64nomnhHp\u0024 = DependencyProperty.Register(nameof (DefaultLabelProvider), typeof (\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DzALRRz3KBB3Uz = DependencyProperty.Register(nameof (TextFormatting), typeof (string), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzkAJ7QX7sBa0Q = DependencyProperty.Register(nameof (CursorTextFormatting), typeof (string), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzecHk\u0024RsxluvB = DependencyProperty.Register(nameof (AxisTitle), typeof (string), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzX3RG9MW1gXPS = DependencyProperty.Register(nameof (TitleStyle), typeof (Style), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzkSXCm31bzQFm = DependencyProperty.Register(nameof (TitleFontWeight), typeof (FontWeight), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) FontWeights.Normal, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzm\u0024Un1mDjW9dY = DependencyProperty.Register(nameof (TitleFontSize), typeof (double), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) 12.0, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzf2R4YGdWLzHd = DependencyProperty.Register(nameof (TickTextBrush), typeof (Brush), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata(new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(nameof (StrokeThickness), typeof (double), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) 1.0, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dz14NuCRCkl6TxRd8BEA\u003D\u003D = DependencyProperty.Register(nameof (MajorTickLineStyle), typeof (Style), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DziNYhz0DqEHOOjhljjg\u003D\u003D = DependencyProperty.Register(nameof (MinorTickLineStyle), typeof (Style), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dz6E17UGyH3Hxe = DependencyProperty.Register(nameof (DrawMajorTicks), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzHxEy7A8kQeb2 = DependencyProperty.Register(nameof (DrawMinorTicks), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dz\u0024geG9XF9qNM9 = DependencyProperty.Register(nameof (DrawLabels), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzDDf\u0024Sa19KecVtCNmpA\u003D\u003D = DependencyProperty.Register(nameof (MajorGridLineStyle), typeof (Style), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzAak9t33M4Bx00JH4Xw\u003D\u003D = DependencyProperty.Register(nameof (MinorGridLineStyle), typeof (Style), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzAaZ_8g9ACaldVRhq\u0024w\u003D\u003D = DependencyProperty.Register(nameof (DrawMajorGridLines), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzqdrX3c4RyinxmAyEug\u003D\u003D = DependencyProperty.Register(nameof (DrawMinorGridLines), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dz7NskrrDUwGfd = DependencyProperty.Register(nameof (DrawMajorBands), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzdsaZczkKof25 = DependencyProperty.Register(nameof (AxisBandsFill), typeof (Color), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) new Color(), new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzGW4hK41x8a8n = DependencyProperty.Register(nameof (TickLabelStyle), typeof (Style), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzVEJ5y7o\u003D = DependencyProperty.Register(nameof (Scrollbar), typeof (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzO13xWFct\u0024F9n)));
  
  public static readonly DependencyProperty \u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D = DependencyProperty.Register(nameof (IsLabelCullingEnabled), typeof (bool), typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));
  
  private EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> \u0023\u003Dz7F6WbE57tL8z;
  
  private EventHandler<EventArgs> \u0023\u003DzrN9MkyArV3jJ;
  
  private IServiceContainer \u0023\u003Dzg8Ufa_EMXfJU;
  
  protected \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> \u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D;
  
  protected \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c \u0023\u003DzuybmMyI_5LK1k3IeW5QCm9nPtLE9;
  
  private ISciChartSurface \u0023\u003Dzos6SMwAMXZ33;
  
  private \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P7\u00243UKiUGVR88DR8huM\u003D \u0023\u003DzdEt8bikGsITl;
  
  private dje_zCCKWRE6ZAZTUZM9ZU5ZMRRYMRCBJV5TT9Z7CKVUJYFUMMKZ_ejd \u0023\u003DzoN4RYgKqEhkM;
  
  private bool \u0023\u003DzpRrgz6k\u0024zI7k = true;
  
  private \u0023\u003DztV7TJNs8Fc3o3jygpDKIFCR_PPkbPsEQYufejDZ61O06vgNcMQ\u003D\u003D \u0023\u003DzPfs\u0024IokVcdkx;
  
  private \u0023\u003DzgeFvyoahWukw3bL8yZfVYr7JNrME_OMqiz4nIDlTZlok \u0023\u003DzpjNghke0G7gqMkLYiA\u003D\u003D;
  
  private float \u0023\u003DzRwEA_2Y\u003D;
  
  private dje_zS7YY9QYBN58NRQDVW3CHXMQTLMN3BLC47SWJ4H2SA7WZC3Q2YX9NCCQJD54Q_ejd \u0023\u003DzPtp9VPqrocT6wwO0v3a5T4g\u003D = new dje_zS7YY9QYBN58NRQDVW3CHXMQTLMN3BLC47SWJ4H2SA7WZC3Q2YX9NCCQJD54Q_ejd();
  
  private dje_zNXC69RR7GVTJT3B9825N7L2M65NDP9SPDDPVR2MSAECW2CGYHHT2Z_ejd \u0023\u003DzxYGXaxJiTgX47NaKGbeZjRI\u003D = new dje_zNXC69RR7GVTJT3B9825N7L2M65NDP9SPDDPVR2MSAECW2CGYHHT2Z_ejd();
  
  private bool \u0023\u003Dz1itgeDqdlQvE;
  
  private IRange \u0023\u003Dz6xb\u0024Nc\u0024kmU3q;
  
  private IRange \u0023\u003DztSt_uW5MzG6UbffWGg\u003D\u003D;
  
  private Point \u0023\u003DzcVHdIBCOOjXh;
  
  private static readonly int[] \u0023\u003DzHqisnGuYfim9SENYmhFqfGUvEnuX = new int[5]
  {
    2,
    4,
    8,
    16 /*0x10*/,
    32 /*0x20*/
  };
  
  private StackPanel \u0023\u003DzgTtVLFnthbIz;
  
  protected Line \u0023\u003DzeEl93ifUiK4P = new Line();

  protected dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd);
    this.\u0023\u003DztSt_uW5MzG6UbffWGg\u003D\u003D = this.\u0023\u003Dz6xb\u0024Nc\u0024kmU3q = this.\u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D();
    this.SetCurrentValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzfolHRDLbOj27, (object) new \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDO3VFM5XbySLODko9bHLvrDkMuy0qw\u003D\u003D());
    this.\u0023\u003DzqV3Pli9MtvTJ();
    this.SizeChanged += new SizeChangedEventHandler(this.\u0023\u003DzyYNf6TD2aRpOQ6mbk8EVPH8\u003D);
  }

  internal dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd(
    \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P7\u00243UKiUGVR88DR8huM\u003D _param1)
    : this()
  {
    this.\u0023\u003DzdEt8bikGsITl = _param1;
  }

  public event PropertyChangedEventHandler PropertyChanged;

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003Dzf1TnIHLmqeNf(
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> _param1)
  {
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> eventHandler = this.\u0023\u003Dz7F6WbE57tL8z;
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D>>(ref this.\u0023\u003Dz7F6WbE57tL8z, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzO6DAydbqJOaS(
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> _param1)
  {
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> eventHandler = this.\u0023\u003Dz7F6WbE57tL8z;
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D>>(ref this.\u0023\u003Dz7F6WbE57tL8z, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzF_\u0024wky5\u0024qiYa(EventHandler<EventArgs> _param1)
  {
    EventHandler<EventArgs> eventHandler = this.\u0023\u003DzrN9MkyArV3jJ;
    EventHandler<EventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.\u0023\u003DzrN9MkyArV3jJ, comparand + _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  [CompilerGenerated]
  [SpecialName]
  public void \u0023\u003DzwG_uRQ_EmTwc(EventHandler<EventArgs> _param1)
  {
    EventHandler<EventArgs> eventHandler = this.\u0023\u003DzrN9MkyArV3jJ;
    EventHandler<EventArgs> comparand;
    do
    {
      comparand = eventHandler;
      eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.\u0023\u003DzrN9MkyArV3jJ, comparand - _param1, comparand);
    }
    while (eventHandler != comparand);
  }

  private void \u0023\u003DzqV3Pli9MtvTJ()
  {
    this.\u0023\u003DzPfs\u0024IokVcdkx = this.\u0023\u003DzPfs\u0024IokVcdkx ?? (this is dje_zX3N6Q9TLRHQ6K6VPWM4NP8NG2M44BK7RCDU397R9MRFV9SQ7FUGGE_ejd ? (\u0023\u003DztV7TJNs8Fc3o3jygpDKIFCR_PPkbPsEQYufejDZ61O06vgNcMQ\u003D\u003D) new \u0023\u003DzMLvZWaqDqEKovfY1GVv1jPftG0_W7FEYnx3n6zcWwRAFpj8yug\u003D\u003D<dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAN6WMPHC7GV9YCKUF6RP5LXMYLLW5_ejd>(this.MaxAutoTicks, new Func<dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd, dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd>(this.\u0023\u003DzxKKlOa0jEDmy)) : (\u0023\u003DztV7TJNs8Fc3o3jygpDKIFCR_PPkbPsEQYufejDZ61O06vgNcMQ\u003D\u003D) new \u0023\u003DzMLvZWaqDqEKovfY1GVv1jPftG0_W7FEYnx3n6zcWwRAFpj8yug\u003D\u003D<dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd>(this.MaxAutoTicks, new Func<dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd, dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd>(this.\u0023\u003DzxKKlOa0jEDmy)));
  }

  private dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd \u0023\u003DzxKKlOa0jEDmy(
    dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd _param1)
  {
    _param1.DataContext = (object) null;
    _param1.SetBinding(dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd.\u0023\u003DzQinmCMXtHUjS, (BindingBase) new Binding("TickTextBrush")
    {
      Source = (object) this
    });
    _param1.SetBinding(dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd.\u0023\u003DzvGz4ypP\u00247z4yXGlavw\u003D\u003D, (BindingBase) new Binding("AxisAlignment")
    {
      Source = (object) this,
      Converter = (IValueConverter) this.\u0023\u003DzxYGXaxJiTgX47NaKGbeZjRI\u003D
    });
    _param1.SetBinding(dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd.\u0023\u003DzLgY36yN4n7OLzzEgDw\u003D\u003D, (BindingBase) new Binding("AxisAlignment")
    {
      Source = (object) this,
      Converter = (IValueConverter) this.\u0023\u003DzPtp9VPqrocT6wwO0v3a5T4g\u003D
    });
    _param1.SetBinding(FrameworkElement.StyleProperty, (BindingBase) new Binding("TickLabelStyle")
    {
      Source = (object) this
    });
    return _param1;
  }

  bool IAxis.\u0023\u003DzlbW67NDum9APBLuSxcbgNCIOZEiJNed4Aa\u0024MIGodDWjPqbL6pQ\u003D\u003D()
  {
    return this.IsXAxis;
  }

  void IAxis.\u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26r0giLofWLxZZe_B2vedqow4qy1QEg\u003D\u003D(
    bool _param1)
  {
    this.IsXAxis = _param1;
  }

  public bool IsXAxis
  {
    get => this.\u0023\u003DzpRrgz6k\u0024zI7k;
    private set
    {
      this.\u0023\u003DzpRrgz6k\u0024zI7k = value;
      this.\u0023\u003Dz15moWio\u003D(nameof (IsXAxis));
    }
  }

  public virtual bool IsHorizontalAxis => this.Orientation == Orientation.Horizontal;

  public bool IsAxisFlipped => this.IsHorizontalAxis != this.IsXAxis;

  public bool IsLabelCullingEnabled
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzS8sUIjkEwjmfnx6c5zL2ukc\u003D, (object) value);
    }
  }

  public bool IsCenterAxis
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzcwiMJ7NmMskq);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzcwiMJ7NmMskq, (object) value);
    }
  }

  public bool IsPrimaryAxis
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzVliJ8IIRU5CI);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzVliJ8IIRU5CI, (object) value);
    }
  }

  public bool IsStaticAxis
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzclwKGsykhg0_);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzclwKGsykhg0_, (object) value);
    }
  }

  public bool AutoAlignVisibleRange
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzFb1oVqzbThlVLIru\u0024A\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzFb1oVqzbThlVLIru\u0024A\u003D\u003D, (object) value);
    }
  }

  public bool HasValidVisibleRange => this.\u0023\u003DzwzofU1mfo8hC();

  public bool HasDefaultVisibleRange
  {
    get
    {
      IRange abyLt9clZggmJsWhw = this.\u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D();
      return this.VisibleRange.Equals((object) abyLt9clZggmJsWhw) && this.\u0023\u003DztSt_uW5MzG6UbffWGg\u003D\u003D.Equals((object) abyLt9clZggmJsWhw);
    }
  }

  double IDrawable.\u0023\u003DzEa5ACpOap4rFIaHj5p9yfH70ARbSZe0FxQ0q\u00240QfMpnPN_04zQ\u003D\u003D()
  {
    return this.ActualWidth;
  }

  void IDrawable.\u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARI4gQW2p5XnENj22E0ug7VJ0RyC3hMw\u003D\u003D(
    double _param1)
  {
  }

  double IDrawable.\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ZE9YOd5sMhl\u0024Z\u0024xSADAZlqXzWzlvA\u003D\u003D()
  {
    return this.ActualHeight;
  }

  void IDrawable.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntaAE_X2h6PHbsMnRBK9cYE8yLrOBvg\u003D\u003D(
    double _param1)
  {
  }

  public ISciChartSurface ParentSurface
  {
    get => this.\u0023\u003Dzos6SMwAMXZ33;
    set
    {
      this.\u0023\u003Dzos6SMwAMXZ33 = value;
      if (this.\u0023\u003Dzos6SMwAMXZ33 != null && this.\u0023\u003Dzos6SMwAMXZ33.\u0023\u003Dzu\u0024P3XgkcE7BC() != null)
        this.Services = this.\u0023\u003Dzos6SMwAMXZ33.\u0023\u003Dzu\u0024P3XgkcE7BC();
      this.\u0023\u003Dz15moWio\u003D(nameof (ParentSurface));
    }
  }

  public IServiceContainer Services
  {
    get => this.\u0023\u003Dzg8Ufa_EMXfJU;
    set => this.\u0023\u003Dzg8Ufa_EMXfJU = value;
  }

  public string AxisTitle
  {
    get
    {
      return (string) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzecHk\u0024RsxluvB);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzecHk\u0024RsxluvB, (object) value);
    }
  }

  public Style TitleStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzX3RG9MW1gXPS);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzX3RG9MW1gXPS, (object) value);
    }
  }

  public FontWeight TitleFontWeight
  {
    get
    {
      return (FontWeight) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzkSXCm31bzQFm);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzkSXCm31bzQFm, (object) value);
    }
  }

  public double TitleFontSize
  {
    get
    {
      return (double) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzm\u0024Un1mDjW9dY);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzm\u0024Un1mDjW9dY, (object) value);
    }
  }

  public string TextFormatting
  {
    get
    {
      return (string) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzALRRz3KBB3Uz);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzALRRz3KBB3Uz, (object) value);
    }
  }

  public string CursorTextFormatting
  {
    get
    {
      return (string) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzkAJ7QX7sBa0Q);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzkAJ7QX7sBa0Q, (object) value);
    }
  }

  public \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH LabelProvider
  {
    get
    {
      return (\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzbkm16i6vgFnk);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzbkm16i6vgFnk, (object) value);
    }
  }

  public \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH DefaultLabelProvider
  {
    get
    {
      return (\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzol64nomnhHp\u0024);
    }
    protected set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzol64nomnhHp\u0024, (object) value);
    }
  }

  [Obsolete("We're sorry! AxisBase.AxisMode is obsolete. To create a chart with Logarithmic axis, please the LogarithmicNumericAxis type instead")]
  public \u0023\u003DzzKBN5TXUMNIGpWrDrUMXSdQaIc96_52nMJQUgeTEfBP3 AxisMode
  {
    get
    {
      throw new Exception("We're sorry! AxisBase.AxisMode is obsolete. To create a chart with Logarithmic axis, please the LogarithmicNumericAxis type instead");
    }
    set
    {
      throw new Exception("We're sorry! AxisBase.AxisMode is obsolete. To create a chart with Logarithmic axis, please the LogarithmicNumericAxis type instead");
    }
  }

  public dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd AutoRange
  {
    get
    {
      return (dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzKm2_RDWENeyO);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzKm2_RDWENeyO, (object) value);
    }
  }

  [TypeConverter(typeof (StringToDoubleRangeTypeConverter))]
  public IRange<double> GrowBy
  {
    get
    {
      return (IRange<double>) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz3kyPJRWoiKq0);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz3kyPJRWoiKq0, (object) value);
    }
  }

  public bool FlipCoordinates
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzhYW6tiLSC0eZ);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzhYW6tiLSC0eZ, (object) value);
    }
  }

  public IComparable MajorDelta
  {
    get
    {
      return (IComparable) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzuTkKbr18L_Ur);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzuTkKbr18L_Ur, (object) value);
    }
  }

  public int MinorsPerMajor
  {
    get
    {
      return (int) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzEt7Ui3H_z_JvbwmWQQ\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzEt7Ui3H_z_JvbwmWQQ\u003D\u003D, (object) value);
    }
  }

  public int MaxAutoTicks
  {
    get
    {
      return (int) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzpEX3tswlNvij);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzpEX3tswlNvij, (object) value);
    }
  }

  public bool AutoTicks
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz4PN8nI\u0024I1O63);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz4PN8nI\u0024I1O63, (object) value);
    }
  }

  public ITickProvider TickProvider
  {
    get
    {
      return (ITickProvider) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz1bLZaITSYGdx);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz1bLZaITSYGdx, (object) value);
    }
  }

  public \u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwrm\u0024HOuIKCn_Ala213x1NpKA TickCoordinatesProvider
  {
    get
    {
      return (\u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwrm\u0024HOuIKCn_Ala213x1NpKA) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzfolHRDLbOj27);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzfolHRDLbOj27, (object) value);
    }
  }

  public IComparable MinorDelta
  {
    get
    {
      return (IComparable) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzdg3vng1nptYM);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzdg3vng1nptYM, (object) value);
    }
  }

  public Brush TickTextBrush
  {
    get
    {
      return (Brush) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzf2R4YGdWLzHd);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzf2R4YGdWLzHd, (object) value);
    }
  }

  [Obsolete("MajorLineStroke is obsolete, please use MajorTickLineStyle instead", true)]
  public Brush MajorLineStroke
  {
    get => (Brush) null;
    set
    {
      throw new Exception("MajorLineStroke is obsolete, please use MajorTickLineStyle instead");
    }
  }

  [Obsolete("MinorLineStroke is obsolete, please use MajorTickLineStyle instead", true)]
  public Brush MinorLineStroke
  {
    get => (Brush) null;
    set
    {
      throw new Exception("MinorLineStroke is obsolete, please use MajorTickLineStyle instead");
    }
  }

  public Style MajorTickLineStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz14NuCRCkl6TxRd8BEA\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz14NuCRCkl6TxRd8BEA\u003D\u003D, (object) value);
    }
  }

  public Style MinorTickLineStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DziNYhz0DqEHOOjhljjg\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DziNYhz0DqEHOOjhljjg\u003D\u003D, (object) value);
    }
  }

  public Style MajorGridLineStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzDDf\u0024Sa19KecVtCNmpA\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzDDf\u0024Sa19KecVtCNmpA\u003D\u003D, (object) value);
    }
  }

  public Style MinorGridLineStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzAak9t33M4Bx00JH4Xw\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzAak9t33M4Bx00JH4Xw\u003D\u003D, (object) value);
    }
  }

  public bool DrawMinorTicks
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzHxEy7A8kQeb2);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzHxEy7A8kQeb2, (object) value);
    }
  }

  public bool DrawLabels
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz\u0024geG9XF9qNM9);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz\u0024geG9XF9qNM9, (object) value);
    }
  }

  public bool DrawMajorTicks
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz6E17UGyH3Hxe);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz6E17UGyH3Hxe, (object) value);
    }
  }

  public bool DrawMajorGridLines
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzAaZ_8g9ACaldVRhq\u0024w\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzAaZ_8g9ACaldVRhq\u0024w\u003D\u003D, (object) value);
    }
  }

  public bool DrawMinorGridLines
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzqdrX3c4RyinxmAyEug\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzqdrX3c4RyinxmAyEug\u003D\u003D, (object) value);
    }
  }

  public bool DrawMajorBands
  {
    get
    {
      return (bool) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz7NskrrDUwGfd);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz7NskrrDUwGfd, (object) value);
    }
  }

  public Color AxisBandsFill
  {
    get
    {
      return (Color) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzdsaZczkKof25);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzdsaZczkKof25, (object) value);
    }
  }

  public Orientation Orientation
  {
    get
    {
      return (Orientation) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzXMV_skc\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzXMV_skc\u003D, (object) value);
    }
  }

  public dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd AxisAlignment
  {
    get
    {
      return (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzfMY988N0StOA);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzfMY988N0StOA, (object) value);
    }
  }

  public string Id
  {
    get
    {
      return (string) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzD\u0024wXQ8E\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzD\u0024wXQ8E\u003D, (object) value);
    }
  }

  public double StrokeThickness
  {
    get
    {
      return (double) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.StrokeThicknessProperty);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.StrokeThicknessProperty, (object) value);
    }
  }

  public Style TickLabelStyle
  {
    get
    {
      return (Style) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzGW4hK41x8a8n);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzGW4hK41x8a8n, (object) value);
    }
  }

  public dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd Scrollbar
  {
    get
    {
      return (dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzVEJ5y7o\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzVEJ5y7o\u003D, (object) value);
    }
  }

  public bool IsSuspended
  {
    get
    {
      return UpdateSuspender.\u0023\u003DzY5RcByYV3P6y((ISuspendable) this);
    }
  }

  public \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk ModifierAxisCanvas
  {
    get
    {
      return (\u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk) this.\u0023\u003DzoN4RYgKqEhkM;
    }
  }

  protected \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpB4GFFdsIQ_FR8tlLNjHr1X3p7javA\u003D\u003D \u0023\u003DzTRL\u0024Xy0vYDigfJ9YNg\u003D\u003D()
  {
    return this.ParentSurface == null ? (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpB4GFFdsIQ_FR8tlLNjHr1X3p7javA\u003D\u003D) null : this.ParentSurface.\u0023\u003DzTRL\u0024Xy0vYDigfJ9YNg\u003D\u003D();
  }

  protected \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7 \u0023\u003Dz\u0024XHewofFXjwz()
  {
    return this.ParentSurface == null ? (\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7) null : this.ParentSurface.get_RenderSurface();
  }

  public virtual bool IsCategoryAxis => false;

  public virtual bool IsLogarithmicAxis => false;

  public virtual bool IsPolarAxis => false;

  [TypeConverter(typeof (StringToDoubleRangeTypeConverter))]
  public IRange AnimatedVisibleRange
  {
    get
    {
      return (IRange) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzGqv7OFBibgO2);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzGqv7OFBibgO2, (object) value);
    }
  }

  [TypeConverter(typeof (StringToDoubleRangeTypeConverter))]
  public IRange VisibleRange
  {
    get
    {
      return (IRange) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzWl3LbWhL1z0D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzWl3LbWhL1z0D, (object) value);
    }
  }

  [TypeConverter(typeof (StringToDoubleRangeTypeConverter))]
  public IRange VisibleRangeLimit
  {
    get
    {
      return (IRange) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzW7Xl\u00245Sj1hV\u0024);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzW7Xl\u00245Sj1hV\u0024, (object) value);
    }
  }

  public \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D VisibleRangeLimitMode
  {
    get
    {
      return (\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzxPOeDpgt9hi7h0ryrw\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzxPOeDpgt9hi7h0ryrw\u003D\u003D, (object) value);
    }
  }

  public IComparable MinimalZoomConstrain
  {
    get
    {
      return (IComparable) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzjWJEoVoxRw8F);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzjWJEoVoxRw8F, (object) value);
    }
  }

  public IRange DataRange
  {
    get => this.\u0023\u003Dzd6x7lH_dQH0I();
  }

  internal \u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P7\u00243UKiUGVR88DR8huM\u003D \u0023\u003DzhFAmpLNTPh4n()
  {
    return this.\u0023\u003DzdEt8bikGsITl;
  }

  internal StackPanel \u0023\u003DzMUpWftuaeZ8o() => this.\u0023\u003DzgTtVLFnthbIz;

  internal \u0023\u003DztV7TJNs8Fc3o3jygpDKIFCR_PPkbPsEQYufejDZ61O06vgNcMQ\u003D\u003D \u0023\u003Dzjk5IewFx\u0024KZ81I_BPQ\u003D\u003D()
  {
    return this.\u0023\u003DzPfs\u0024IokVcdkx;
  }

  public virtual double CurrentDatapointPixelSize => double.NaN;

  [Obsolete("AxisBase.GetPointRange is obsolete, please call DataSeries.GetIndicesRange(VisibleRange) instead", true)]
  public \u0023\u003Dz59_koqr2EQdapDcFKycZuFSiStoJv1Yg1g\u003D\u003D \u0023\u003Dz53g0bO2haOY4()
  {
    throw new NotSupportedException("AxisBase.GetPointRange is obsolete, please call DataSeries.GetIndicesRange(VisibleRange) instead");
  }

  public abstract IRange \u0023\u003DzspbjXJnVtbB\u0024();

  public abstract IRange \u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D();

  public abstract IRange \u0023\u003DzzMId\u0024f67Wftb(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1);

  protected virtual IRange \u0023\u003Dzd6x7lH_dQH0I()
  {
    if (this.ParentSurface == null || this.ParentSurface.get_RenderableSeries().\u0023\u003DzCCMM80zDpO6N<IRenderableSeries>())
      return (IRange) null;
    return !this.IsXAxis ? this.\u0023\u003DzoWnNnPa93Kn8() : this.\u0023\u003DzfHPjUn3EtpHl();
  }

  private IRange \u0023\u003DzfHPjUn3EtpHl()
  {
    IRange abyLt9clZggmJsWhw1 = (IRange) null;
    foreach (IRenderableSeries s1JolYrWoYpqmQ6ug in this.ParentSurface.get_RenderableSeries().Where<IRenderableSeries>(new Func<IRenderableSeries, bool>(this.\u0023\u003DznfjxYadwxaoyd16Alq7avE0\u003D)))
    {
      IRange abyLt9clZggmJsWhw2 = s1JolYrWoYpqmQ6ug.\u0023\u003Dzq3MgExWxza1L();
      if (abyLt9clZggmJsWhw2 != null && abyLt9clZggmJsWhw2.IsDefined)
      {
        DoubleRange klqcJ87Zm8UwE3WEjd = abyLt9clZggmJsWhw2.AsDoubleRange();
        abyLt9clZggmJsWhw1 = abyLt9clZggmJsWhw1 == null ? (IRange) klqcJ87Zm8UwE3WEjd : klqcJ87Zm8UwE3WEjd.\u0023\u003DzeiifnZI\u003D(abyLt9clZggmJsWhw1);
      }
    }
    return abyLt9clZggmJsWhw1;
  }

  private IRange \u0023\u003DzoWnNnPa93Kn8()
  {
    IRange abyLt9clZggmJsWhw = (IRange) null;
    foreach (IRenderableSeries s1JolYrWoYpqmQ6ug in this.ParentSurface.get_RenderableSeries().Where<IRenderableSeries>(new Func<IRenderableSeries, bool>(this.\u0023\u003DzApQq1pC_4pCnMbzX7sr8AII\u003D)))
    {
      IRange yrange = s1JolYrWoYpqmQ6ug.get_DataSeries().get_YRange();
      if (yrange != null && yrange.IsDefined)
      {
        DoubleRange klqcJ87Zm8UwE3WEjd = yrange.AsDoubleRange();
        abyLt9clZggmJsWhw = abyLt9clZggmJsWhw == null ? (IRange) klqcJ87Zm8UwE3WEjd : klqcJ87Zm8UwE3WEjd.\u0023\u003DzeiifnZI\u003D(abyLt9clZggmJsWhw);
      }
    }
    return abyLt9clZggmJsWhw;
  }

  public virtual IRange \u0023\u003DzFwoMKP9juTnt()
  {
    IRange abyLt9clZggmJsWhw1 = (IRange) new DoubleRange(double.NaN, double.NaN);
    if (this.ParentSurface != null && !this.ParentSurface.get_RenderableSeries().\u0023\u003DzCCMM80zDpO6N<IRenderableSeries>())
    {
      if (this.IsXAxis)
      {
        abyLt9clZggmJsWhw1 = this.\u0023\u003DzfHPjUn3EtpHl() ?? abyLt9clZggmJsWhw1;
        if (abyLt9clZggmJsWhw1.IsZero)
          abyLt9clZggmJsWhw1 = this.\u0023\u003DzsB7Y9t30CQ63(abyLt9clZggmJsWhw1);
        if (this.GrowBy != null)
        {
          double num = this.IsLogarithmicAxis ? ((\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D) this).get_LogarithmicBase() : 0.0;
          abyLt9clZggmJsWhw1 = abyLt9clZggmJsWhw1.GrowBy(this.GrowBy.Min, this.GrowBy.Max, this.IsLogarithmicAxis, num);
        }
        if (this.VisibleRangeLimit != null)
          abyLt9clZggmJsWhw1.\u0023\u003DzJIqIiUw\u003D((IRange) this.VisibleRangeLimit.AsDoubleRange(), this.VisibleRangeLimitMode);
      }
      else
        abyLt9clZggmJsWhw1 = this.\u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D((IDictionary<string, IRange>) null);
    }
    IRange abyLt9clZggmJsWhw2 = this.VisibleRange == null || !this.VisibleRange.IsDefined ? this.\u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D() : this.VisibleRange;
    return abyLt9clZggmJsWhw1 == null || !abyLt9clZggmJsWhw1.IsDefined ? (IRange) abyLt9clZggmJsWhw2.AsDoubleRange() : abyLt9clZggmJsWhw1;
  }

  protected virtual IRange \u0023\u003DzsB7Y9t30CQ63(
    IRange _param1)
  {
    return _param1.GrowBy(0.01, 0.01);
  }

  public IRange \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    IDictionary<string, IRange> _param1)
  {
    IRange abyLt9clZggmJsWhw1 = (IRange) new DoubleRange(double.NaN, double.NaN);
    if (this.ParentSurface != null && !this.ParentSurface.get_RenderableSeries().\u0023\u003DzCCMM80zDpO6N<IRenderableSeries>())
    {
      foreach (IRenderableSeries s1JolYrWoYpqmQ6ug in this.ParentSurface.get_RenderableSeries().Where<IRenderableSeries>(new Func<IRenderableSeries, bool>(this.\u0023\u003DzaUGqhuODC_LS7GO3CfQn3j4SMN4z8jTtuw\u003D\u003D)))
      {
        IRange abyLt9clZggmJsWhw2 = _param1 == null || !_param1.ContainsKey(s1JolYrWoYpqmQ6ug.get_XAxisId()) ? (s1JolYrWoYpqmQ6ug.XAxis ?? this.ParentSurface.get_XAxes().\u0023\u003Dz\u0024YoxjvGBoa2C(s1JolYrWoYpqmQ6ug.get_XAxisId(), false))?.VisibleRange : _param1[s1JolYrWoYpqmQ6ug.get_XAxisId()];
        if (abyLt9clZggmJsWhw2 != null && abyLt9clZggmJsWhw2.IsDefined)
        {
          DoubleRange klqcJ87Zm8UwE3WEjd = s1JolYrWoYpqmQ6ug.\u0023\u003DzxNQHuqrEvxH2(abyLt9clZggmJsWhw2, this.IsLogarithmicAxis).AsDoubleRange();
          if (klqcJ87Zm8UwE3WEjd.IsDefined)
            abyLt9clZggmJsWhw1 = klqcJ87Zm8UwE3WEjd.\u0023\u003DzeiifnZI\u003D(abyLt9clZggmJsWhw1);
        }
      }
      if (abyLt9clZggmJsWhw1.IsZero)
        abyLt9clZggmJsWhw1 = this.\u0023\u003DzsB7Y9t30CQ63(abyLt9clZggmJsWhw1);
      if (this.GrowBy != null)
      {
        double num = this.IsLogarithmicAxis ? ((\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D) this).get_LogarithmicBase() : 0.0;
        abyLt9clZggmJsWhw1 = abyLt9clZggmJsWhw1 != null ? abyLt9clZggmJsWhw1.GrowBy(this.GrowBy.Min, this.GrowBy.Max, this.IsLogarithmicAxis, num) : (IRange) null;
      }
      if (this.VisibleRangeLimit != null)
        abyLt9clZggmJsWhw1.\u0023\u003DzJIqIiUw\u003D((IRange) this.VisibleRangeLimit.AsDoubleRange(), this.VisibleRangeLimitMode);
    }
    return abyLt9clZggmJsWhw1 == null || !abyLt9clZggmJsWhw1.IsDefined ? (this.VisibleRange != null ? (IRange) this.VisibleRange.AsDoubleRange() : (IRange) null) : abyLt9clZggmJsWhw1;
  }

  public void \u0023\u003DzquLnA5Y\u003D(
    double _param1,
    ClipMode _param2)
  {
    this.\u0023\u003DzquLnA5Y\u003D(_param1, _param2, TimeSpan.Zero);
  }

  public void \u0023\u003DzquLnA5Y\u003D(
    double _param1,
    ClipMode _param2,
    TimeSpan _param3)
  {
    \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c txZaHyXliZ9wXjzC = this.\u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0();
    if (txZaHyXliZ9wXjzC == null)
      return;
    IRange abyLt9clZggmJsWhw1 = txZaHyXliZ9wXjzC.\u0023\u003DzquLnA5Y\u003D(this.VisibleRange, _param1);
    IRange abyLt9clZggmJsWhw2 = abyLt9clZggmJsWhw1;
    if (_param2 != ClipMode.None)
    {
      IRange abyLt9clZggmJsWhw3 = this.\u0023\u003DzFwoMKP9juTnt();
      abyLt9clZggmJsWhw2 = txZaHyXliZ9wXjzC.\u0023\u003DzoaHKvRB3HZP3(abyLt9clZggmJsWhw1, abyLt9clZggmJsWhw3, _param2);
    }
    IRange abyLt9clZggmJsWhw4 = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(this.VisibleRange, abyLt9clZggmJsWhw2.Min, abyLt9clZggmJsWhw2.Max);
    this.\u0023\u003DzuPwHeHOc6hD2hGW59w\u003D\u003D(abyLt9clZggmJsWhw4);
    this.\u0023\u003DzB4GssLEPDUHfRR_NuUKVKvc\u003D(abyLt9clZggmJsWhw4, _param3);
  }

  protected void \u0023\u003DzuPwHeHOc6hD2hGW59w\u003D\u003D(
    IRange _param1)
  {
    if (this.VisibleRangeLimit == null)
      return;
    _param1.\u0023\u003DzJIqIiUw\u003D(this.VisibleRangeLimit, this.VisibleRangeLimitMode);
  }

  public void \u0023\u003Dz\u00248pSPh2nSp0Q(int _param1)
  {
    this.\u0023\u003Dz\u00248pSPh2nSp0Q(_param1, TimeSpan.Zero);
  }

  public virtual void \u0023\u003Dz\u00248pSPh2nSp0Q(int _param1, TimeSpan _param2)
  {
    throw new InvalidOperationException("ScrollByDataPoints is only valid CategoryDateTimeAxis");
  }

  public void \u0023\u003DzQdR08KQ\u003D(double _param1, double _param2)
  {
    this.\u0023\u003DzQdR08KQ\u003D(_param1, _param2, TimeSpan.Zero);
  }

  public void \u0023\u003DzQdR08KQ\u003D(double _param1, double _param2, TimeSpan _param3)
  {
    IRange abyLt9clZggmJsWhw = this.\u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0().\u0023\u003DzQdR08KQ\u003D(this.VisibleRange, _param1, _param2);
    this.\u0023\u003DzuPwHeHOc6hD2hGW59w\u003D\u003D(abyLt9clZggmJsWhw);
    this.\u0023\u003DzB4GssLEPDUHfRR_NuUKVKvc\u003D(abyLt9clZggmJsWhw, _param3);
  }

  public void \u0023\u003Dz40HnRQM\u003D(double _param1, double _param2)
  {
    this.\u0023\u003Dz40HnRQM\u003D(_param1, _param2, TimeSpan.Zero);
  }

  public void \u0023\u003Dz40HnRQM\u003D(double _param1, double _param2, TimeSpan _param3)
  {
    \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c txZaHyXliZ9wXjzC = this.\u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0();
    if (txZaHyXliZ9wXjzC == null)
      return;
    IRange abyLt9clZggmJsWhw = txZaHyXliZ9wXjzC.\u0023\u003Dz40HnRQM\u003D(this.VisibleRange, _param1, _param2);
    this.\u0023\u003DzuPwHeHOc6hD2hGW59w\u003D\u003D(abyLt9clZggmJsWhw);
    this.\u0023\u003DzB4GssLEPDUHfRR_NuUKVKvc\u003D(abyLt9clZggmJsWhw, _param3);
  }

  [Obsolete("AxisBase.ScrollTo is obsolete, please call AxisBase.Scroll(pixelsToScroll) instead")]
  public virtual void \u0023\u003DzSYETXFE\u003D(
    IRange _param1,
    double _param2)
  {
    this.\u0023\u003DzCIPDlIJQeLiZ(_param1, _param2, (IRange) null);
  }

  public virtual void \u0023\u003DzCIPDlIJQeLiZ(
    IRange _param1,
    double _param2,
    IRange _param3)
  {
    IRange abyLt9clZggmJsWhw1 = this.\u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0().\u0023\u003DzquLnA5Y\u003D(this.VisibleRange, _param2);
    IRange abyLt9clZggmJsWhw2;
    if (_param3 == null)
    {
      abyLt9clZggmJsWhw2 = abyLt9clZggmJsWhw1;
    }
    else
    {
      DoubleRange klqcJ87Zm8UwE3WEjd = abyLt9clZggmJsWhw1.AsDoubleRange();
      abyLt9clZggmJsWhw2 = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(this.VisibleRange, klqcJ87Zm8UwE3WEjd.Min, klqcJ87Zm8UwE3WEjd.Max, _param3);
    }
    if (!this.\u0023\u003Dz2OKbyRBzRCBL(abyLt9clZggmJsWhw2))
      return;
    this.VisibleRange = abyLt9clZggmJsWhw2;
  }

  public virtual bool \u0023\u003Dz2OKbyRBzRCBL(
    IRange _param1)
  {
    return this.\u0023\u003Dz9yvpaTXy3ucx(_param1) && _param1.IsDefined && _param1.Min.CompareTo((object) _param1.Max) <= 0;
  }

  public abstract bool \u0023\u003Dz9yvpaTXy3ucx(
    IRange _param1);

  public XmlSchema GetSchema() => (XmlSchema) null;

  public virtual void ReadXml(XmlReader _param1)
  {
    if (_param1.MoveToContent() != XmlNodeType.Element || !(_param1.LocalName == ((object) this).GetType().Name))
      return;
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xGkBRjz2cANt6prK2mzvoXV0.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz4EJs3pc\u003D(this, _param1);
  }

  public virtual void WriteXml(XmlWriter _param1)
  {
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xGkBRjz2cANt6prK2mzvoXV0.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz7SZ\u0024Lrw\u003D(this, _param1);
  }

  public abstract IAxis \u0023\u003DzQ8SgRgQ\u003D();

  public virtual void \u0023\u003DzQ4klw1orSVl\u0024(Type _param1)
  {
    List<Type> source = this.\u0023\u003DzvwDcRtQA0c4T();
    if (!source.Contains(_param1))
      throw new InvalidOperationException($"{((object) this).GetType().Name} does not support the type {_param1}. Supported types include {string.Join(", ", source.Select<Type, string>(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.SomeClass34343383.\u0023\u003Dzp0nohgqqqt9t7iBsrQ\u003D\u003D ?? (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.SomeClass34343383.\u0023\u003Dzp0nohgqqqt9t7iBsrQ\u003D\u003D = new Func<Type, string>(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003Dz2GlstDm2igpkLb8awKHhDD0\u003D))).ToArray<string>())}");
  }

  protected abstract List<Type> \u0023\u003DzvwDcRtQA0c4T();

  public void \u0023\u003DzpTR8\u0024ECbZOHX()
  {
    if (!this.AutoTicks && (this.MajorDelta == null || this.MajorDelta.Equals(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzuTkKbr18L_Ur.GetMetadata(typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd)).DefaultValue) || this.MinorDelta == null || this.MinorDelta.Equals(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzdg3vng1nptYM.GetMetadata(typeof (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd)).DefaultValue)))
      throw new InvalidOperationException("The MinDelta, MaxDelta properties have to be set if AutoTicks == False.");
  }

  public void \u0023\u003DzqFIyyIbnwGLq(Cursor _param1)
  {
    this.SetCurrentValue(FrameworkElement.CursorProperty, (object) _param1);
  }

  public void Clear()
  {
    if (this.\u0023\u003DzhFAmpLNTPh4n() == null)
      return;
    this.\u0023\u003DzhFAmpLNTPh4n().\u0023\u003DzWvf8ESIWLp6Z();
  }

  public \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c \u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0()
  {
    return this.\u0023\u003DzuybmMyI_5LK1k3IeW5QCm9nPtLE9;
  }

  public virtual \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> \u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D()
  {
    this.\u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D = (this.Services == null ? (\u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7WIKy8yFmS0qz5aG2LjQ9ZhaLqHaajO4_nAIgryYYasWa8dMpfY\u003D) new \u0023\u003DzPql\u0024onrPHiHfWZj7w2jaKmkEzvtrSPmPXCypCYSvlJxk\u0024K3oG\u0024G8T9G0Kw\u0024aeAQVBAnAZzg\u003D() : this.Services.\u0023\u003Dz2VqWonc\u003D<\u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7WIKy8yFmS0qz5aG2LjQ9ZhaLqHaajO4_nAIgryYYasWa8dMpfY\u003D>()).GetMath(this.\u0023\u003Dz0RktzzbyC\u002468());
    this.\u0023\u003DzuybmMyI_5LK1k3IeW5QCm9nPtLE9 = (\u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c) new \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWjyReycAnylcv4bnRW6DXa0wZWo1GN8_OWPxtqME(this.\u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D);
    return this.\u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D;
  }

  public virtual void \u0023\u003Dzs15X3Ar32F1\u0024(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1 = default (\u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D),
    \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ _param2 = null)
  {
    this.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D();
  }

  public virtual \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3_fwmcVcA0rHXkV5W8VrNVY \u0023\u003Dz0RktzzbyC\u002468()
  {
    \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7 n9miZzngvjW54Qe0d7 = this.\u0023\u003Dz\u0024XHewofFXjwz();
    DoubleRange klqcJ87Zm8UwE3WEjd = (this.VisibleRange ?? (IRange) new DoubleRange(double.NaN, double.NaN)).AsDoubleRange();
    int num = this.IsHorizontalAxis ? (int) this.ActualWidth : (int) this.ActualHeight;
    if ((double) Math.Abs(num) < double.Epsilon && n9miZzngvjW54Qe0d7 != null)
      num = this.IsHorizontalAxis ? (int) n9miZzngvjW54Qe0d7.ActualWidth : (int) n9miZzngvjW54Qe0d7.ActualHeight;
    \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3_fwmcVcA0rHXkV5W8VrNVY vcA0rHxkV5W8VrNvy = new \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3_fwmcVcA0rHXkV5W8VrNVY()
    {
      \u0023\u003DztJAA1uMf9_P8 = this.FlipCoordinates,
      \u0023\u003DzFA18Bsxthn7B = this.IsXAxis,
      \u0023\u003Dz_jNjf7U\u003D = this.IsHorizontalAxis,
      \u0023\u003Dzk0u64hpXv585 = klqcJ87Zm8UwE3WEjd.Max.ToDouble(),
      \u0023\u003DzBeOlCgb3wUDW = klqcJ87Zm8UwE3WEjd.Min.ToDouble(),
      \u0023\u003DznR9\u00242Eg\u003D = this.\u0023\u003Dz4wEfDhMr\u0024V6c(),
      \u0023\u003DzdTxNrgQ\u003D = (double) num,
      \u0023\u003Dz_WzdhI8nAiba = double.NaN
    };
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D ns01UjmP40FpxAl2jmQ = this.\u0023\u003DznRkNg0BYTU_8();
    if (ns01UjmP40FpxAl2jmQ != null)
    {
      vcA0rHxkV5W8VrNvy.\u0023\u003Dz9jZG_9RUfbqZ = ns01UjmP40FpxAl2jmQ.\u0023\u003DzwQnyySN6xaVC();
      vcA0rHxkV5W8VrNvy.\u0023\u003DzYlmrR5WNSPuW = ns01UjmP40FpxAl2jmQ.get_IsSorted();
    }
    return vcA0rHxkV5W8VrNvy;
  }

  public virtual double \u0023\u003Dz4wEfDhMr\u0024V6c()
  {
    double num = 0.0;
    if (this.\u0023\u003Dz\u0024XHewofFXjwz() != null)
    {
      Rect rect = this.\u0023\u003DzdC9whUui_gN\u0024((IHitTestable) this.\u0023\u003Dz\u0024XHewofFXjwz());
      num = Rect.op_Equality(rect, Rect.Empty) ? 0.0 : (this.IsHorizontalAxis ? rect.X : rect.Y);
    }
    return num;
  }

  private \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D \u0023\u003DznRkNg0BYTU_8()
  {
    if (this.ParentSurface?.get_RenderableSeries() == null)
      return (\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) null;
    IRenderableSeries[] array = this.ParentSurface.get_RenderableSeries().Where<IRenderableSeries>(new Func<IRenderableSeries, bool>(this.\u0023\u003DzT1eqMAzRyPWuBr9AGC0GrIg\u003D)).ToArray<IRenderableSeries>();
    return (((IEnumerable<IRenderableSeries>) array).FirstOrDefault<IRenderableSeries>(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.SomeClass34343383.\u0023\u003Dz\u0024PljuUuhiNN4HlDDAw\u003D\u003D ?? (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.SomeClass34343383.\u0023\u003Dz\u0024PljuUuhiNN4HlDDAw\u003D\u003D = new Func<IRenderableSeries, bool>(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzdmoJgxczu_t0o8hq6gcvIsE\u003D))) ?? \u0023\u003DzsIIzg9COgILMyUKVNisy8sT1ePq3.\u0023\u003DzVqxLKNDqEV82<IRenderableSeries>(array))?.get_DataSeries();
  }

  protected virtual void \u0023\u003Dz15moWio\u003D(string _param1)
  {
    PropertyChangedEventHandler zUapFgog = this.\u0023\u003DzUApFgog\u003D;
    if (zUapFgog == null)
      return;
    zUapFgog((object) this, new PropertyChangedEventArgs(_param1));
  }

  public void DecrementSuspend()
  {
  }

  public IUpdateSuspender SuspendUpdates()
  {
    return (IUpdateSuspender) new UpdateSuspender((ISuspendable) this);
  }

  public void ResumeUpdates(
    IUpdateSuspender _param1)
  {
    if (!_param1.ResumeTargetOnDispose)
      return;
    this.InvalidateElement();
  }

  public void InvalidateElement()
  {
    this.InvalidateMeasure();
    this.InvalidateArrange();
    if (this.ParentSurface == null)
      return;
    this.ParentSurface.InvalidateElement();
  }

  public virtual \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D \u0023\u003DzjuB\u0024Pa8\u003D(
    Point _param1)
  {
    return this.\u0023\u003Dz7RSLatA2csE8Xxn\u00246hZKpF8\u003D() == null ? (\u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D) null : this.\u0023\u003DzjuB\u0024Pa8\u003D(this.GetDataValue(this.IsHorizontalAxis ? _param1.X : _param1.Y));
  }

  public virtual \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D \u0023\u003DzjuB\u0024Pa8\u003D(
    IComparable _param1)
  {
    string str1 = this.\u0023\u003DzRDs3D1Q\u003D(_param1);
    string str2 = this.\u0023\u003DzRQVMnjXxoCTF(_param1, true);
    return new \u0023\u003DzT6V9cIzTPzymiPsaXC1JFEAP9ly0DLdsgjrQCUaaCm\u0024XPj7JdmPvp0w\u003D()
    {
      AxisId = this.Id,
      DataValue = _param1,
      AxisAlignment = this.AxisAlignment,
      AxisFormattedDataValue = str1,
      CursorFormattedDataValue = str2,
      AxisTitle = this.AxisTitle,
      IsHorizontal = this.IsHorizontalAxis,
      IsXAxis = this.IsXAxis
    };
  }

  [Obsolete("The FormatText method which takes a format string is obsolete. Please use the method overload with one argument instead.", true)]
  public virtual string \u0023\u003DzRDs3D1Q\u003D(IComparable _param1, string _param2)
  {
    throw new NotSupportedException("The FormatText method which takes a format string is obsolete. Please use the method overload with one argument instead.");
  }

  public virtual string \u0023\u003DzRDs3D1Q\u003D(IComparable _param1)
  {
    return this.LabelProvider == null ? _param1.ToString() : this.LabelProvider.\u0023\u003DzkqN2vZ4\u003D(_param1);
  }

  public virtual string \u0023\u003DzRQVMnjXxoCTF(IComparable _param1, bool _param2)
  {
    return this.LabelProvider == null ? _param1.ToString() : this.LabelProvider.\u0023\u003Dz\u0024WinkXTLMGVP(_param1, _param2);
  }

  public virtual IComparable GetDataValue(double _param1)
  {
    return this.\u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D == null ? (IComparable) double.NaN : (IComparable) this.\u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D.GetDataValue(_param1);
  }

  public virtual double \u0023\u003DzhL6gsJw\u003D(IComparable _param1)
  {
    return this.\u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D == null ? double.NaN : this.\u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D.\u0023\u003DzhL6gsJw\u003D(_param1.ToDouble());
  }

  public bool IsPointWithinBounds(Point _param1)
  {
    return this.\u0023\u003DzbOxVzAyGdX66(this.ParentSurface.\u0023\u003Dzwc4Gzka23TGB().TranslatePoint(_param1, (IHitTestable) this));
  }

  public Point TranslatePoint(
    Point _param1,
    IHitTestable _param2)
  {
    return this.\u0023\u003DzaPPLsvfM_Sst(_param1, _param2);
  }

  public Rect GetBoundsRelativeTo(
    IHitTestable _param1)
  {
    return this.\u0023\u003DzdC9whUui_gN\u0024(_param1);
  }

  public void OnDraw(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    if (!this.\u0023\u003DzY8pXafMRsePE())
      return;
    using (IUpdateSuspender fq05jnDg3bOrIrgCjote = this.SuspendUpdates())
    {
      fq05jnDg3bOrIrgCjote.ResumeTargetOnDispose=false;
      Stopwatch stopwatch = Stopwatch.StartNew();
      if (this.LabelProvider != null)
        this.LabelProvider.\u0023\u003DzI_kEht21kBsX();
      this.\u0023\u003DzpjNghke0G7gqMkLYiA\u003D\u003D = this.\u0023\u003DzyPl0NtN\u0024cLlA();
      this.\u0023\u003Dz0gXNT9nH\u0024Hmh(_param1, this.\u0023\u003DzpjNghke0G7gqMkLYiA\u003D\u003D);
      if (this.\u0023\u003DzG64YTDk\u003D())
        this.\u0023\u003Dz4DqqnUJ6Xy7_(this.\u0023\u003DzpjNghke0G7gqMkLYiA\u003D\u003D);
      stopwatch.Stop();
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Drawn {0}: Width={1}, Height={2} in {3}ms", new object[4]
      {
        (object) ((object) this).GetType().Name,
        (object) this.ActualWidth,
        (object) this.ActualHeight,
        (object) stopwatch.ElapsedMilliseconds
      });
    }
  }

  private bool \u0023\u003DzY8pXafMRsePE() => !this.IsSuspended && this.\u0023\u003DzwzofU1mfo8hC();

  private bool \u0023\u003DzG64YTDk\u003D()
  {
    return this.ParentSurface is SciChartSurface parentSurface && parentSurface.\u0023\u003DzST\u0024t7rI\u003D() && this.\u0023\u003DzST\u0024t7rI\u003D() && this.\u0023\u003DzmqmxAhIhbSX3();
  }

  private bool \u0023\u003DzwzofU1mfo8hC()
  {
    bool flag = this.\u0023\u003Dz2OKbyRBzRCBL(this.VisibleRange) && !this.VisibleRange.IsZero;
    if (!flag)
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("{0} is not a valid VisibleRange for {1}", new object[2]
      {
        (object) this.VisibleRange,
        (object) ((object) this).GetType()
      });
    return flag;
  }

  protected virtual \u0023\u003DzgeFvyoahWukw3bL8yZfVYr7JNrME_OMqiz4nIDlTZlok \u0023\u003DzyPl0NtN\u0024cLlA()
  {
    this.\u0023\u003Dz2RD3F8MtvzO1();
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) this.TickProvider, "TickProvider");
    IAxisParams iaxnW4w0PxdsBxUkNwS = (IAxisParams) this;
    double[] numArray = this.TickProvider.\u0023\u003Dzctqa9kMCtfQQ(iaxnW4w0PxdsBxUkNwS);
    return this.TickCoordinatesProvider.\u0023\u003DzU4j4bt2YhYuc(this.TickProvider.\u0023\u003Dz65PoZl8ZJBOc(iaxnW4w0PxdsBxUkNwS), numArray);
  }

  protected abstract void \u0023\u003Dz2RD3F8MtvzO1();

  protected abstract \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D \u0023\u003Dzgy73vTR0r5jyI3j3hAgwZho\u003D();

  protected virtual uint \u0023\u003Dzl02YIEvJDKYh() => (uint) Math.Max(1, this.MaxAutoTicks);

  protected virtual void \u0023\u003Dz0gXNT9nH\u0024Hmh(
    IRenderContext2D _param1,
    \u0023\u003DzgeFvyoahWukw3bL8yZfVYr7JNrME_OMqiz4nIDlTZlok _param2)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzDHoeMmd6l4_hHMbDXGPPWWY\u003D mmd6l4HHmbDxgppwwy = new dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzDHoeMmd6l4_hHMbDXGPPWWY\u003D();
    mmd6l4HHmbDxgppwwy._variableSome3535 = this;
    mmd6l4HHmbDxgppwwy.\u0023\u003DzC8v0b7k\u003D = _param1;
    mmd6l4HHmbDxgppwwy.\u0023\u003DzN30aRBOHzzD9WoflAw\u003D\u003D = _param2;
    if (mmd6l4HHmbDxgppwwy.\u0023\u003DzC8v0b7k\u003D == null)
      return;
    if (this.DrawMinorGridLines && mmd6l4HHmbDxgppwwy.\u0023\u003DzN30aRBOHzzD9WoflAw\u003D\u003D.\u0023\u003Dz7uJsqQByZdM3URzWdA\u003D\u003D().Length != 0)
      mmd6l4HHmbDxgppwwy.\u0023\u003DzC8v0b7k\u003D.\u0023\u003Dz7eGjoBhvKuFN().\u0023\u003Dz\u0024CeUvME\u003D((\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyZMZbx4MhrMzMW0v\u0024xo\u003D) 1).\u0023\u003DzBkxoLC0\u003D(new Action(mmd6l4HHmbDxgppwwy.\u0023\u003Dzdp_WMa5ZvPOgWUt9DZr6s8k\u003D));
    if (mmd6l4HHmbDxgppwwy.\u0023\u003DzN30aRBOHzzD9WoflAw\u003D\u003D.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D().Length == 0)
      return;
    if (this.DrawMajorBands)
      mmd6l4HHmbDxgppwwy.\u0023\u003DzC8v0b7k\u003D.\u0023\u003Dz7eGjoBhvKuFN().\u0023\u003Dz\u0024CeUvME\u003D((\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyZMZbx4MhrMzMW0v\u0024xo\u003D) 0).\u0023\u003DzBkxoLC0\u003D(new Action(mmd6l4HHmbDxgppwwy.\u0023\u003Dze0viRyfissfKu5\u0024vH3bBkCo\u003D));
    if (!this.DrawMajorGridLines)
      return;
    mmd6l4HHmbDxgppwwy.\u0023\u003DzC8v0b7k\u003D.\u0023\u003Dz7eGjoBhvKuFN().\u0023\u003Dz\u0024CeUvME\u003D((\u0023\u003DzFgfHSvJTVKiBUeYgwcNjyZMZbx4MhrMzMW0v\u0024xo\u003D) 2).\u0023\u003DzBkxoLC0\u003D(new Action(mmd6l4HHmbDxgppwwy.\u0023\u003DzBzNUoyJKShhwpK03iuRmUkw\u003D));
  }

  private void \u0023\u003DzShVkWbecUrbY(
    IRenderContext2D _param1,
    double[] _param2,
    float[] _param3)
  {
    \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7 n9miZzngvjW54Qe0d7 = this.\u0023\u003Dz\u0024XHewofFXjwz();
    if (n9miZzngvjW54Qe0d7 == null)
      return;
    dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd ks34Z259A4NengcEjd = this.IsHorizontalAxis ? dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection : dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection;
    using (IBrush2D xrgcdFbSdWgN9GcT8 = _param1.\u0023\u003Dze8WyDhI\u003D(this.AxisBandsFill, 1.0, new bool?()))
    {
      float num1 = (float) this.\u0023\u003Dz4wEfDhMr\u0024V6c();
      float num2 = this.IsHorizontalAxis ? 0.0f : num1 + (float) this.ActualHeight;
      float num3 = this.IsHorizontalAxis ? (float) n9miZzngvjW54Qe0d7.ActualWidth : num1;
      if (this.FlipCoordinates ^ this.IsAxisFlipped)
        NumberUtil.Swap(ref num2, ref num3);
      bool flag = this.\u0023\u003DzSoNtv0XsZOx0(_param2[0]) % 2M == 0M;
      for (int index = 0; index < _param3.Length; ++index)
      {
        if (flag)
        {
          float num4 = index == 0 ? num2 : _param3[index - 1];
          float num5 = _param3[index];
          this.\u0023\u003DzShVkWbecUrbY(_param1, xrgcdFbSdWgN9GcT8, ks34Z259A4NengcEjd, num4, num5);
        }
        flag = !flag;
      }
      if (!flag)
        return;
      this.\u0023\u003DzShVkWbecUrbY(_param1, xrgcdFbSdWgN9GcT8, ks34Z259A4NengcEjd, num3, ((IEnumerable<float>) _param3).Last<float>());
    }
  }

  private void \u0023\u003DzShVkWbecUrbY(
    IRenderContext2D _param1,
    IBrush2D _param2,
    dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd _param3,
    float _param4,
    float _param5)
  {
    \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7 n9miZzngvjW54Qe0d7 = this.\u0023\u003Dz\u0024XHewofFXjwz();
    if (n9miZzngvjW54Qe0d7 == null)
      return;
    Point point1 = _param3 == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection ? new Point(0.0, (double) _param4) : new Point((double) _param4, 0.0);
    Point point2 = _param3 == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection ? new Point(n9miZzngvjW54Qe0d7.ActualWidth, (double) _param5) : new Point((double) _param5, n9miZzngvjW54Qe0d7.ActualHeight);
    _param1.\u0023\u003DzVRUUvzhAr5SR(_param2, point1, point2, 0.0);
  }

  protected virtual void \u0023\u003DzbUPOl6ZpNIOI(
    IRenderContext2D _param1,
    Style _param2,
    IEnumerable<float> _param3)
  {
    dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd ks34Z259A4NengcEjd = this.IsHorizontalAxis ? dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.XDirection : dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection;
    this.\u0023\u003DzeEl93ifUiK4P.Style = _param2;
    dje_zE2RKFGSKSSRHLKHMSTKDLZ3G36L4UQCSJVCT8AU3_ejd.SetTheme((DependencyObject) this.\u0023\u003DzeEl93ifUiK4P, dje_zE2RKFGSKSSRHLKHMSTKDLZ3G36L4UQCSJVCT8AU3_ejd.GetTheme((DependencyObject) this));
    using (\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = _param1.\u0023\u003DzQCf7bpfi0DqGMauSow\u003D\u003D(this.\u0023\u003DzeEl93ifUiK4P, false))
    {
      if (rhwYsZxA33iRu6Id7J == null || rhwYsZxA33iRu6Id7J.Color.A == (byte) 0)
        return;
      foreach (float num in _param3)
        this.\u0023\u003DzbUPOl6ZpNIOI(_param1, rhwYsZxA33iRu6Id7J, ks34Z259A4NengcEjd, num);
    }
  }

  protected void \u0023\u003DzbUPOl6ZpNIOI(
    IRenderContext2D _param1,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param2,
    dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd _param3,
    float _param4)
  {
    \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC34hLHdN9miZZngvjW54Qe0d7 n9miZzngvjW54Qe0d7 = this.\u0023\u003Dz\u0024XHewofFXjwz();
    if (n9miZzngvjW54Qe0d7 == null)
      return;
    float strokeThickness = _param2.StrokeThickness;
    Point point1 = _param3 == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection ? new Point(-(double) strokeThickness, (double) _param4) : new Point((double) _param4, -(double) strokeThickness);
    Point point2 = _param3 == dje_zZ3BFCL96RVMCB9Z2ZSPMCMP45KS34Z259A4NENGC_ejd.YDirection ? new Point(n9miZzngvjW54Qe0d7.ActualWidth + (double) strokeThickness, (double) _param4) : new Point((double) _param4, n9miZzngvjW54Qe0d7.ActualHeight + (double) strokeThickness);
    _param1.\u0023\u003Dzk8_eoWQ\u003D(_param2, point1, point2);
  }

  protected virtual void \u0023\u003Dz4DqqnUJ6Xy7_(
    \u0023\u003DzgeFvyoahWukw3bL8yZfVYr7JNrME_OMqiz4nIDlTZlok _param1)
  {
    this.\u0023\u003DzRwEA_2Y\u003D = (float) this.\u0023\u003Dz46iyKtU9fraN();
    this.\u0023\u003DzhFAmpLNTPh4n().\u0023\u003DzuTwCwl07R0Mf(_param1, this.\u0023\u003DzRwEA_2Y\u003D);
    if (!this.DrawLabels)
      this.Clear();
    this.\u0023\u003DzhFAmpLNTPh4n().\u0023\u003DzEy\u0024V_bY\u003D();
  }

  protected virtual double \u0023\u003Dz46iyKtU9fraN()
  {
    return (this.IsHorizontalAxis ? this.BorderThickness.Left : this.BorderThickness.Top) + this.\u0023\u003Dz4wEfDhMr\u0024V6c();
  }

  protected virtual void \u0023\u003Dzfs4pIWbs5K_L(
    dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd _param1,
    \u0023\u003DzgeFvyoahWukw3bL8yZfVYr7JNrME_OMqiz4nIDlTZlok _param2,
    float _param3)
  {
    double[] numArray1 = _param2.\u0023\u003Dzyqh0CrzbJnzy();
    float[] numArray2 = _param2.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D();
    if (numArray1 == null || numArray2 == null)
      return;
    int num1 = _param1.Children.Count - numArray2.Length;
    if (num1 > 0)
    {
      using (_param1.SuspendUpdates())
      {
        for (int index = num1 - 1; index >= 0; --index)
        {
          int num2 = numArray2.Length + index;
          this.\u0023\u003DzPHNEYdp5xRAE(_param1, num2);
        }
      }
    }
    for (int index = 0; index < numArray2.Length; ++index)
    {
      double num3 = numArray1[index];
      dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd y9QajdhH6H6U9EEjd = index < _param1.Children.Count ? (dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd) _param1.Children[index] : this.\u0023\u003DzPfs\u0024IokVcdkx.\u0023\u003Dza7jLYgw\u003D(new Func<dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd, dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd>(this.\u0023\u003DzxKKlOa0jEDmy));
      IComparable comparable = this.\u0023\u003Dz3ZiX3E6vqtLl((IComparable) num3);
      this.\u0023\u003DzvZIFtCqEuXgD(y9QajdhH6H6U9EEjd, comparable);
      y9QajdhH6H6U9EEjd.\u0023\u003Dz9ak5jfg0CSsoUX9AxQ\u003D\u003D(this.\u0023\u003DzfMip05MguVVjuRKaIlHpAD8\u003D(num3));
      Point point = this.\u0023\u003DzY0xm0mTfvYAx(_param3, numArray2[index]);
      y9QajdhH6H6U9EEjd.Position = point;
      _param1.\u0023\u003DzH0osWQkV_Y8_((object) y9QajdhH6H6U9EEjd, -1);
    }
  }

  protected virtual Point \u0023\u003DzY0xm0mTfvYAx(float _param1, float _param2)
  {
    float num1 = _param2 - _param1;
    double num2 = this.IsHorizontalAxis ? (double) num1 : 0.0;
    return new Point(num2, (double) num1 - num2);
  }

  protected void \u0023\u003DzPHNEYdp5xRAE(
    dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd _param1,
    int _param2)
  {
    dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd child = (dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd) _param1.Children[_param2];
    _param1.Children.Remove((UIElement) child);
    this.\u0023\u003DzPfs\u0024IokVcdkx.\u0023\u003DzhggR\u00247o\u003D(child);
  }

  protected virtual IComparable \u0023\u003Dz3ZiX3E6vqtLl(IComparable _param1) => _param1;

  private void \u0023\u003DzvZIFtCqEuXgD(
    dje_zZ5YUPHV77BRLFTBNBH6Y9HYE6YMNGXV6RBS5NJ2Y9QAJDHH6H6U9E_ejd _param1,
    IComparable _param2)
  {
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH labelProvider = this.LabelProvider;
    if (labelProvider == null)
      return;
    _param1.DataContext = _param1.DataContext == null ? (object) labelProvider.\u0023\u003DzILIqSWE\u003D(_param2) : (object) labelProvider.\u0023\u003Dz9xSd9Yg\u003D((\u0023\u003DzGf68ilGq59TJ0aVKr0K_9c1X8_XLOwuwCkANZ8F3lvgpooqqVw\u003D\u003D) _param1.DataContext, _param2);
  }

  private int \u0023\u003DzfMip05MguVVjuRKaIlHpAD8\u003D(double _param1)
  {
    return ((IEnumerable<int>) dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzHqisnGuYfim9SENYmhFqfGUvEnuX).Count<int>(new Func<int, bool>(new dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzztvqky7ki\u0024I\u0024g1MAJ0wAGaM\u003D()
    {
      \u0023\u003DzvQ3M6iJZUC74 = this.\u0023\u003DzSoNtv0XsZOx0(_param1)
    }.\u0023\u003DzzR02vPcj7iEpCu37cNMcNoyJfLDl));
  }

  private Decimal \u0023\u003DzSoNtv0XsZOx0(double _param1)
  {
    if (this.\u0023\u003DzrRhlv2\u00243x_rdw41lF5j1sXE\u003D is \u0023\u003Dz5hVyTN88kBn45NAfOxK7MCQZNrLpjKlS2Qc8bb5_oiHXVWVmbJi\u0024\u0024q9i0M\u0024xI7QB9c1V6c0\u003D rhlv23xRdw41lF5j1sXe)
      _param1 = (double) rhlv23xRdw41lF5j1sXe.\u0023\u003DzFk6sufr\u0024co4e((IComparable) _param1);
    double num1 = this.MajorDelta.ToDouble();
    if (this.IsLogarithmicAxis)
    {
      \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D tmjze8uJg3v1qUa6q9M = this as \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D;
      _param1 = Math.Log(_param1, tmjze8uJg3v1qUa6q9M.get_LogarithmicBase());
    }
    double num2 = _param1 / num1;
    if (num2 >= (double) int.MaxValue)
    {
      double num3 = num2 / (double) int.MaxValue;
      num2 = (num3 - (double) (int) num3) * (double) int.MaxValue;
    }
    return (Decimal) (int) num2.\u0023\u003DzZsq6ZfbZQvsf();
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    this.\u0023\u003DzgTtVLFnthbIz = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<StackPanel>("PART_AxisContainer");
    this.\u0023\u003DzdEt8bikGsITl = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<\u0023\u003DzRYm3Fw8jwwRKksCg00\u00244P7\u00243UKiUGVR88DR8huM\u003D>("PART_AxisCanvas");
    ((dje_zQ43M2FZJBSFAUZ6Y8GRTX9AEYABXJZFQPFJ76J7ED3DXEXQ_ejd) this.\u0023\u003DzdEt8bikGsITl).AddLabels = new Action<dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd>(this.\u0023\u003DzrYui6_51Nw2UDAv2Knx8uLQ\u003D);
    this.\u0023\u003DzoN4RYgKqEhkM = this.\u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<dje_zCCKWRE6ZAZTUZM9ZU5ZMRRYMRCBJV5TT9Z7CKVUJYFUMMKZ_ejd>("PART_ModifierAxisCanvas");
    this.\u0023\u003DzoN4RYgKqEhkM.\u0023\u003DzkF76BMTQOROh(this);
    if (this.VisibleRange != null)
      return;
    this.SetCurrentValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzWl3LbWhL1z0D, (object) this.\u0023\u003DzspbjXJnVtbB\u0024());
  }

  protected T \u0023\u003DzkgqGljJ50Pjey0H53Q\u003D\u003D<T>(string _param1) where T : class
  {
    return this.GetTemplateChild(_param1) is T templateChild ? templateChild : throw new InvalidOperationException($"Unable to Apply the Control Template. {_param1} is missing or of the wrong type");
  }

  private static void \u0023\u003DziB9pY7CleJnqmfAdVQ\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLDVzQHFf5h\u00242oAiW0sh1EFU\u003D ff5h2oAiW0sh1Efu = new dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLDVzQHFf5h\u00242oAiW0sh1EFU\u003D();
    ff5h2oAiW0sh1Efu.\u0023\u003Dz6iFJYho\u003D = _param0;
    ff5h2oAiW0sh1Efu.\u0023\u003Dz1BK01YA\u003D = _param1;
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd z6iFjYho = (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) ff5h2oAiW0sh1Efu.\u0023\u003Dz6iFJYho\u003D;
    if (!((DispatcherObject) z6iFjYho).Dispatcher.CheckAccess())
    {
      Action action = new Action(ff5h2oAiW0sh1Efu.\u0023\u003DzXQKZS2jF76TH5rkf7ZqHY2c\u003D);
      ((DispatcherObject) z6iFjYho).Dispatcher.BeginInvoke((Delegate) action, Array.Empty<object>());
    }
    else
    {
      IRange oldValue = ff5h2oAiW0sh1Efu.\u0023\u003Dz1BK01YA\u003D.OldValue as IRange;
      IRange newValue = ff5h2oAiW0sh1Efu.\u0023\u003Dz1BK01YA\u003D.NewValue as IRange;
      if (oldValue != null)
        oldValue.PropertyChanged -= new PropertyChangedEventHandler(z6iFjYho.\u0023\u003DzeHyhwJZa6M2MRbCpsg\u003D\u003D);
      if (newValue == null)
        return;
      if (!z6iFjYho.HasValidVisibleRange || !z6iFjYho.\u0023\u003DzhVh515xbKXIT08tXZA\u003D\u003D())
        z6iFjYho.\u0023\u003Dz6vyemzFNfHjS();
      if (!z6iFjYho.HasValidVisibleRange || !z6iFjYho.\u0023\u003DzhVh515xbKXIT08tXZA\u003D\u003D())
      {
        z6iFjYho.SetCurrentValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzWl3LbWhL1z0D, (object) z6iFjYho.\u0023\u003Dz6xb\u0024Nc\u0024kmU3q);
        z6iFjYho.\u0023\u003DzszckdH5oxVmh(newValue);
      }
      else
      {
        newValue.PropertyChanged += new PropertyChangedEventHandler(z6iFjYho.\u0023\u003DzeHyhwJZa6M2MRbCpsg\u003D\u003D);
        if (!z6iFjYho.\u0023\u003DzP6Bap0LHVSiA(newValue, oldValue))
          return;
        z6iFjYho.\u0023\u003DztSt_uW5MzG6UbffWGg\u003D\u003D = z6iFjYho.\u0023\u003Dz6xb\u0024Nc\u0024kmU3q;
        z6iFjYho.\u0023\u003Dz6xb\u0024Nc\u0024kmU3q = newValue;
      }
    }
  }

  private void \u0023\u003DzeHyhwJZa6M2MRbCpsg\u003D\u003D(
    object _param1,
    PropertyChangedEventArgs _param2)
  {
    IComparable comparable1 = this.VisibleRange.Min;
    IComparable comparable2 = this.VisibleRange.Max;
    switch (_param2.PropertyName)
    {
      case "Min":
        comparable1 = (IComparable) ((\u0023\u003DzMv9TAT1PEEnC0UeBhCNwDCPI15WlokXZO9yDJcU\u003D) _param2).\u0023\u003DzPo6XanrX3cHa();
        break;
      case "Max":
        comparable2 = (IComparable) ((\u0023\u003DzMv9TAT1PEEnC0UeBhCNwDCPI15WlokXZO9yDJcU\u003D) _param2).\u0023\u003DzPo6XanrX3cHa();
        break;
    }
    if (!this.\u0023\u003DzP6Bap0LHVSiA(this.VisibleRange, \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(this.VisibleRange, comparable1, comparable2)))
      return;
    BindingExpression bindingExpression = this.GetBindingExpression(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzWl3LbWhL1z0D);
    if (bindingExpression == null || bindingExpression.ParentBinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit)
      return;
    bindingExpression.UpdateSource();
    bindingExpression.UpdateTarget();
  }

  private bool \u0023\u003DzP6Bap0LHVSiA(
    IRange _param1,
    IRange _param2)
  {
    bool flag = false;
    this.\u0023\u003DzQNsUgFfbQtf8(_param1);
    if (!_param1.Equals((object) _param2))
    {
      this.\u0023\u003Dz_0Le6I5slA7z(new \u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D(_param2, _param1, this.\u0023\u003Dz1itgeDqdlQvE));
      this.\u0023\u003Dzo1moqKMmSMi6();
      flag = true;
    }
    return flag;
  }

  private void \u0023\u003DzQNsUgFfbQtf8(
    IRange _param1)
  {
    this.\u0023\u003DzszckdH5oxVmh(_param1);
    if (_param1.Min.CompareTo((object) _param1.Max) > 0)
      throw new ArgumentException($"VisibleRange.Min (value={_param1.Min}) must be less than VisibleRange.Max (value={_param1.Max})");
  }

  protected virtual void \u0023\u003DzszckdH5oxVmh(
    IRange _param1)
  {
    if (!this.\u0023\u003Dz9yvpaTXy3ucx(_param1))
      throw new InvalidOperationException($"Axis type {((object) this).GetType().Name} requires that VisibleRange is of type {this.VisibleRange.GetType().FullName}");
  }

  protected virtual void \u0023\u003Dz_0Le6I5slA7z(
    \u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D _param1)
  {
    if (this.ParentSurface != null)
      this.ParentSurface.get_ViewportManager()?.\u0023\u003Dz_0Le6I5slA7z((IAxis) this);
    EventHandler<\u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D> z7F6WbE57tL8z = this.\u0023\u003Dz7F6WbE57tL8z;
    if (z7F6WbE57tL8z == null)
      return;
    z7F6WbE57tL8z((object) this, _param1);
  }

  internal static void \u0023\u003Dz2KWD3lC7hdm1(
    IAxis _param0)
  {
    if (!(_param0 is dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd))
      return;
    axF9ZgQ7NbH9KsEjd.\u0023\u003DzaNiYNgKj9pMk();
    axF9ZgQ7NbH9KsEjd.\u0023\u003Dz15moWio\u003D("DataRange");
  }

  private void \u0023\u003DzaNiYNgKj9pMk()
  {
    EventHandler<EventArgs> zrN9MkyArV3jJ = this.\u0023\u003DzrN9MkyArV3jJ;
    if (zrN9MkyArV3jJ == null)
      return;
    zrN9MkyArV3jJ((object) this, EventArgs.Empty);
  }

  private bool \u0023\u003DzhVh515xbKXIT08tXZA\u003D\u003D()
  {
    return this.MinimalZoomConstrain == null || this.VisibleRange.Diff.ToDouble() >= this.MinimalZoomConstrain.ToDouble();
  }

  protected virtual void \u0023\u003Dz6vyemzFNfHjS()
  {
  }

  private static void \u0023\u003Dzqd3jzsY7FQsh5XQziA\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd = (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) _param0;
    IRange oldValue = _param1.OldValue as IRange;
    IRange newValue = _param1.NewValue as IRange;
    if (oldValue != null)
      oldValue.PropertyChanged -= new PropertyChangedEventHandler(axF9ZgQ7NbH9KsEjd.\u0023\u003DztlBn8z1iPaTcjuWrqj791sU\u003D);
    if (newValue == null || axF9ZgQ7NbH9KsEjd.VisibleRange == null)
      return;
    newValue.PropertyChanged += new PropertyChangedEventHandler(axF9ZgQ7NbH9KsEjd.\u0023\u003DztlBn8z1iPaTcjuWrqj791sU\u003D);
    IRange abyLt9clZggmJsWhw = ((IRange) axF9ZgQ7NbH9KsEjd.VisibleRange.Clone()).\u0023\u003DzJIqIiUw\u003D(newValue, axF9ZgQ7NbH9KsEjd.VisibleRangeLimitMode);
    axF9ZgQ7NbH9KsEjd.SetCurrentValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzWl3LbWhL1z0D, (object) abyLt9clZggmJsWhw);
  }

  private void \u0023\u003DztlBn8z1iPaTcjuWrqj791sU\u003D(
    object _param1,
    PropertyChangedEventArgs _param2)
  {
    this.\u0023\u003DzuPwHeHOc6hD2hGW59w\u003D\u003D(this.VisibleRange);
    BindingExpression bindingExpression = this.GetBindingExpression(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzW7Xl\u00245Sj1hV\u0024);
    if (bindingExpression == null || bindingExpression.ParentBinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit)
      return;
    bindingExpression.UpdateSource();
    bindingExpression.UpdateTarget();
  }

  private static void \u0023\u003DzHggGkMOQckHdkG7xQA\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd = (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) _param0;
    if (_param1.NewValue == null)
      return;
    axF9ZgQ7NbH9KsEjd.\u0023\u003DzwrnVUenT8f7v7FlPviBwd40\u003D((IRange) _param1.NewValue, TimeSpan.FromMilliseconds(500.0));
  }

  public void \u0023\u003DzwrnVUenT8f7v7FlPviBwd40\u003D(
    IRange _param1,
    TimeSpan _param2)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzqHEJJ\u0024eT__xlAQw9dK5OGBo\u003D hejjETXlAqw9dK5OgBo1 = new dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzqHEJJ\u0024eT__xlAQw9dK5OGBo\u003D();
    hejjETXlAqw9dK5OgBo1._variableSome3535 = this;
    hejjETXlAqw9dK5OgBo1.\u0023\u003DzAHNI_S0\u003D = _param1;
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzVDzEWto\u003D((object) hejjETXlAqw9dK5OgBo1.\u0023\u003DzAHNI_S0\u003D, "to");
    if (!this.HasValidVisibleRange)
    {
      this.VisibleRange = hejjETXlAqw9dK5OgBo1.\u0023\u003DzAHNI_S0\u003D;
    }
    else
    {
      this.\u0023\u003DzcVHdIBCOOjXh = this.\u0023\u003DzfZ0wxly_bFc2(this.VisibleRange);
      Point point = this.\u0023\u003DzfZ0wxly_bFc2(hejjETXlAqw9dK5OgBo1.\u0023\u003DzAHNI_S0\u003D);
      dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzqHEJJ\u0024eT__xlAQw9dK5OGBo\u003D hejjETXlAqw9dK5OgBo2 = hejjETXlAqw9dK5OgBo1;
      PointAnimation pointAnimation = new PointAnimation();
      pointAnimation.From = new Point?(new Point(0.0, 0.0));
      pointAnimation.To = new Point?(new Point(point.X - this.\u0023\u003DzcVHdIBCOOjXh.X, point.Y - this.\u0023\u003DzcVHdIBCOOjXh.Y));
      pointAnimation.Duration = (Duration) _param2;
      ExponentialEase exponentialEase = new ExponentialEase();
      exponentialEase.EasingMode = EasingMode.EaseOut;
      exponentialEase.Exponent = 7.0;
      pointAnimation.EasingFunction = (IEasingFunction) exponentialEase;
      hejjETXlAqw9dK5OgBo2.\u0023\u003DzXB4BRQQhi9cE = pointAnimation;
      hejjETXlAqw9dK5OgBo1.\u0023\u003Dz\u00245UhFnPQKjzF = (IRange) this.VisibleRange.Clone();
      hejjETXlAqw9dK5OgBo1.\u0023\u003DzXB4BRQQhi9cE.Completed += new EventHandler(hejjETXlAqw9dK5OgBo1.\u0023\u003Dz90gMLvcCBhHezcHa8o6j4QFx83AR);
      Storyboard.SetTarget((DependencyObject) hejjETXlAqw9dK5OgBo1.\u0023\u003DzXB4BRQQhi9cE, (DependencyObject) this);
      Storyboard.SetTargetProperty((DependencyObject) hejjETXlAqw9dK5OgBo1.\u0023\u003DzXB4BRQQhi9cE, new PropertyPath("VisibleRangePoint", Array.Empty<object>()));
      Storyboard storyboard = new Storyboard();
      storyboard.Duration = (Duration) _param2;
      storyboard.Children.Add((Timeline) hejjETXlAqw9dK5OgBo1.\u0023\u003DzXB4BRQQhi9cE);
      this.\u0023\u003Dz1itgeDqdlQvE = true;
      storyboard.Begin();
    }
  }

  private Point \u0023\u003DzfZ0wxly_bFc2(
    IRange _param1)
  {
    DoubleRange klqcJ87Zm8UwE3WEjd = _param1.AsDoubleRange();
    double a1 = klqcJ87Zm8UwE3WEjd.Min;
    double a2 = klqcJ87Zm8UwE3WEjd.Max;
    if (this.IsLogarithmicAxis)
    {
      double logarithmicBase = ((\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D) this).get_LogarithmicBase();
      a1 = Math.Log(a1, logarithmicBase);
      a2 = Math.Log(a2, logarithmicBase);
    }
    return new Point(a1, a2);
  }

  private static void \u0023\u003Dz10or8z86voVPLuMIHw\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzOspLWCtZ4LQy_hknBYH_r14\u003D ctZ4LqyHknByhR14 = new dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzOspLWCtZ4LQy_hknBYH_r14\u003D();
    ctZ4LqyHknByhR14.\u0023\u003DzSygYxus\u003D = (Point) _param1.NewValue;
    ctZ4LqyHknByhR14.\u0023\u003Dzfl\u0024A1s0\u003D = (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) _param0;
    ((DispatcherObject) ctZ4LqyHknByhR14.\u0023\u003Dzfl\u0024A1s0\u003D).Dispatcher.\u0023\u003DznvGFbrlLtrNN(new Action(ctZ4LqyHknByhR14.\u0023\u003DzIE9AdC6Gxka684aXOhGqScGsymGy));
  }

  private static void \u0023\u003DzcZqyvQl0j0My(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd))
      return;
    axF9ZgQ7NbH9KsEjd.\u0023\u003Dz15moWio\u003D("IsHorizontalAxis");
  }

  private static void \u0023\u003DzoqG1CE2rlL_j(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd = (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) _param0;
    ISciChartSurface parentSurface = axF9ZgQ7NbH9KsEjd.ParentSurface;
    if (parentSurface != null && !axF9ZgQ7NbH9KsEjd.IsSuspended)
      parentSurface.\u0023\u003DzOPvUPixjU\u00244Y((IAxis) axF9ZgQ7NbH9KsEjd, (dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd) _param1.OldValue);
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D(_param0, _param1);
  }

  private static void \u0023\u003DzIgRO8VI5TESl1w9T\u0024A\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd = (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) _param0;
    ISciChartSurface parentSurface = axF9ZgQ7NbH9KsEjd.ParentSurface;
    if (parentSurface != null && !axF9ZgQ7NbH9KsEjd.IsSuspended)
      parentSurface.\u0023\u003DzFrczvpG2vhM5((IAxis) axF9ZgQ7NbH9KsEjd);
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D(_param0, _param1);
  }

  protected static void \u0023\u003DzLUQi5D4\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd = (dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) _param0;
    if (!axF9ZgQ7NbH9KsEjd.\u0023\u003DzmqmxAhIhbSX3())
      return;
    axF9ZgQ7NbH9KsEjd.\u0023\u003Dzo1moqKMmSMi6();
  }

  private static void \u0023\u003DzO13xWFct\u0024F9n(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd = _param0 as dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd;
    if (!(_param1.NewValue is dje_z3RW4XXVHPPTBLAGDQTKJWNS6SMENJ783R64BTQJ77TR5RZXGUXYPAD7ZYWZA_ejd newValue))
      return;
    newValue.Axis = (IAxis) axF9ZgQ7NbH9KsEjd;
  }

  private bool \u0023\u003DzmqmxAhIhbSX3() => this.\u0023\u003DzhFAmpLNTPh4n() != null;

  private void \u0023\u003Dzo1moqKMmSMi6()
  {
    if (this.Services == null || this.ParentSurface == null || this.ParentSurface.IsSuspended)
      return;
    this.Services.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u_IIwaqHR9EOQ\u003D\u003D>().\u0023\u003DzosHqOAc\u003D<\u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B>(new \u0023\u003DzFphlrC3tGBVP73muJW4N1gQtGNrHdTMuWgCKGfUfH93B((object) this));
  }

  private static void \u0023\u003DzGSdejefFTNCn(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd = _param0 as dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd;
    if (_param1.NewValue is \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH newValue)
      newValue.\u0023\u003DzWzUaFxw\u003D((IAxis) axF9ZgQ7NbH9KsEjd);
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D(_param0, _param1);
  }

  private static void \u0023\u003DzQikQKvMZp1xk(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd = _param0 as dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd;
    if (_param1.NewValue is ITickProvider newValue)
      newValue.\u0023\u003DzWzUaFxw\u003D((IAxis) axF9ZgQ7NbH9KsEjd);
    if (_param1.OldValue is ITickProvider oldValue)
      oldValue.\u0023\u003DzWzUaFxw\u003D((IAxis) null);
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D(_param0, _param1);
  }

  private static void \u0023\u003DzTs\u0024_r2pUSNyL(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd) || !axF9ZgQ7NbH9KsEjd.IsPrimaryAxis)
      return;
    axF9ZgQ7NbH9KsEjd.InvalidateElement();
  }

  private static void \u0023\u003DzxtYfETQdftTw(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd))
      return;
    if (axF9ZgQ7NbH9KsEjd.FlipCoordinates && axF9ZgQ7NbH9KsEjd.IsCategoryAxis)
      throw new InvalidOperationException("The CategoryDateTimeAxis type does not support coordinate reversal (FlipCoordinates).");
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D(_param0, _param1);
  }

  private static void \u0023\u003Dz1UOyXOfHyEI9(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd))
      return;
    if (axF9ZgQ7NbH9KsEjd.IsStaticAxis && axF9ZgQ7NbH9KsEjd.IsCategoryAxis)
      throw new InvalidOperationException("The CategoryDateTimeAxis type does not support the Static mode (IsStatic).");
    axF9ZgQ7NbH9KsEjd.TickCoordinatesProvider = axF9ZgQ7NbH9KsEjd.IsStaticAxis ? (\u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwrm\u0024HOuIKCn_Ala213x1NpKA) new \u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqEwEg9j_x8XOiyOqAPcZI6sx0QNvBw\u003D\u003D() : (\u0023\u003Dz1EupPkIlS\u0024DjzDzGIXoOwrm\u0024HOuIKCn_Ala213x1NpKA) new \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDO3VFM5XbySLODko9bHLvrDkMuy0qw\u003D\u003D();
  }

  private static void \u0023\u003DzQsSOj9uli6iK(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd axF9ZgQ7NbH9KsEjd))
      return;
    axF9ZgQ7NbH9KsEjd.TickCoordinatesProvider.\u0023\u003DzWzUaFxw\u003D((IAxis) axF9ZgQ7NbH9KsEjd);
  }

  HorizontalAlignment IAxis.\u0023\u003Dz5VLaAZX2bctAcuSoajSAXtBWUytTwKmwegWB430RRP_iyVUjrw\u003D\u003D()
  {
    return this.HorizontalAlignment;
  }

  void IAxis.\u0023\u003DzTbSy5Tg7CNKewHb2FguXqzKcL0mg\u0024lar5H2OZ3W_18PGuoI1WA\u003D\u003D(
    HorizontalAlignment _param1)
  {
    this.HorizontalAlignment = _param1;
  }

  VerticalAlignment IAxis.\u0023\u003DzSseiGdgwJmJ1pkmz7CEFfx8mbOWyc1wXvn8wBzjwACKu6EY0OQ\u003D\u003D()
  {
    return this.VerticalAlignment;
  }

  void IAxis.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntTNDUky6SmSb6\u0024FDWAQO1Y0HmfujBQ\u003D\u003D(
    VerticalAlignment _param1)
  {
    this.VerticalAlignment = _param1;
  }

  Visibility IAxis.\u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRYwo3gBY9dA\u0024Mbe\u0024dG0As1jePnhZWw\u003D\u003D()
  {
    return this.Visibility;
  }

  void IAxis.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntSyV0Rj0ibC6aIMhpwJ2VCPFsZZaBw\u003D\u003D(
    Visibility _param1)
  {
    this.Visibility = _param1;
  }

  double IHitTestable.\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk_uTcHPb_FD6TqCanmMNLu1qiOPHXwlPSNY\u003D()
  {
    return this.ActualWidth;
  }

  double IHitTestable.\u0023\u003DzzsyKnUNUDKjF7rDv70izN8J6fpW\u0024OkM14cKsD6c_CdYLZ77RJxzrNo0\u003D()
  {
    return this.ActualHeight;
  }

  private void \u0023\u003DzyYNf6TD2aRpOQ6mbk8EVPH8\u003D(
    object _param1,
    SizeChangedEventArgs _param2)
  {
    this.InvalidateElement();
  }

  private bool \u0023\u003DznfjxYadwxaoyd16Alq7avE0\u003D(
    IRenderableSeries _param1)
  {
    if (!(_param1.get_XAxisId() == this.Id) || !_param1.IsVisible)
      return false;
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D dataSeries = _param1.get_DataSeries();
    return dataSeries != null && dataSeries.get_HasValues();
  }

  private bool \u0023\u003DzApQq1pC_4pCnMbzX7sr8AII\u003D(
    IRenderableSeries _param1)
  {
    return _param1.get_YAxisId() == this.Id && _param1.IsVisible && _param1.get_DataSeries() != null;
  }

  private bool \u0023\u003DzaUGqhuODC_LS7GO3CfQn3j4SMN4z8jTtuw\u003D\u003D(
    IRenderableSeries _param1)
  {
    return _param1.get_YAxisId() == this.Id && _param1.get_DataSeries() != null && _param1.IsVisible && _param1.get_DataSeries().get_HasValues();
  }

  private bool \u0023\u003DzT1eqMAzRyPWuBr9AGC0GrIg\u003D(
    IRenderableSeries _param1)
  {
    if (!(this.Id == (this.IsXAxis ? _param1.get_XAxisId() : _param1.get_YAxisId())))
      return false;
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D dataSeries = _param1.get_DataSeries();
    return dataSeries != null && dataSeries.get_HasValues();
  }

  private void \u0023\u003DzrYui6_51Nw2UDAv2Knx8uLQ\u003D(
    dje_zW53QLLBXPN8WXR6G6DUDNUP6W4AXHHF8QUECZ77TXG2K5CA_ejd _param1)
  {
    if (!this.\u0023\u003DzY8pXafMRsePE() || !this.\u0023\u003DzG64YTDk\u003D())
      return;
    this.\u0023\u003Dzfs4pIWbs5K_L(_param1, this.\u0023\u003DzpjNghke0G7gqMkLYiA\u003D\u003D, this.\u0023\u003DzRwEA_2Y\u003D);
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.SomeClass34343383 SomeMethond0343 = new dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.SomeClass34343383();
    public static Func<Type, string> \u0023\u003Dzp0nohgqqqt9t7iBsrQ\u003D\u003D;
    public static Func<IRenderableSeries, bool> \u0023\u003Dz\u0024PljuUuhiNN4HlDDAw\u003D\u003D;

    internal string \u0023\u003Dz2GlstDm2igpkLb8awKHhDD0\u003D(Type _param1) => _param1.Name;

    internal bool \u0023\u003DzdmoJgxczu_t0o8hq6gcvIsE\u003D(
      IRenderableSeries _param1)
    {
      return _param1.get_DataSeries() is IOhlcDataSeries;
    }
  }

  private sealed class \u0023\u003DzDHoeMmd6l4_hHMbDXGPPWWY\u003D
  {
    public dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd _variableSome3535;
    public IRenderContext2D \u0023\u003DzC8v0b7k\u003D;
    public \u0023\u003DzgeFvyoahWukw3bL8yZfVYr7JNrME_OMqiz4nIDlTZlok \u0023\u003DzN30aRBOHzzD9WoflAw\u003D\u003D;

    internal void \u0023\u003Dzdp_WMa5ZvPOgWUt9DZr6s8k\u003D()
    {
      // ISSUE: explicit non-virtual call
      this._variableSome3535.\u0023\u003DzbUPOl6ZpNIOI(this.\u0023\u003DzC8v0b7k\u003D, __nonvirtual (this._variableSome3535.MinorGridLineStyle), (IEnumerable<float>) this.\u0023\u003DzN30aRBOHzzD9WoflAw\u003D\u003D.\u0023\u003Dz7uJsqQByZdM3URzWdA\u003D\u003D());
    }

    internal void \u0023\u003Dze0viRyfissfKu5\u0024vH3bBkCo\u003D()
    {
      this._variableSome3535.\u0023\u003DzShVkWbecUrbY(this.\u0023\u003DzC8v0b7k\u003D, this.\u0023\u003DzN30aRBOHzzD9WoflAw\u003D\u003D.\u0023\u003Dzyqh0CrzbJnzy(), this.\u0023\u003DzN30aRBOHzzD9WoflAw\u003D\u003D.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D());
    }

    internal void \u0023\u003DzBzNUoyJKShhwpK03iuRmUkw\u003D()
    {
      // ISSUE: explicit non-virtual call
      this._variableSome3535.\u0023\u003DzbUPOl6ZpNIOI(this.\u0023\u003DzC8v0b7k\u003D, __nonvirtual (this._variableSome3535.MajorGridLineStyle), (IEnumerable<float>) this.\u0023\u003DzN30aRBOHzzD9WoflAw\u003D\u003D.\u0023\u003Dz37wZ\u0024XVBzxVIXk7Ktw\u003D\u003D());
    }
  }

  private sealed class \u0023\u003DzLDVzQHFf5h\u00242oAiW0sh1EFU\u003D
  {
    public DependencyObject \u0023\u003Dz6iFJYho\u003D;
    public DependencyPropertyChangedEventArgs \u0023\u003Dz1BK01YA\u003D;

    internal void \u0023\u003DzXQKZS2jF76TH5rkf7ZqHY2c\u003D()
    {
      dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DziB9pY7CleJnqmfAdVQ\u003D\u003D(this.\u0023\u003Dz6iFJYho\u003D, this.\u0023\u003Dz1BK01YA\u003D);
    }
  }

  private sealed class \u0023\u003DzOspLWCtZ4LQy_hknBYH_r14\u003D
  {
    public dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd \u0023\u003Dzfl\u0024A1s0\u003D;
    public Point \u0023\u003DzSygYxus\u003D;

    internal void \u0023\u003DzIE9AdC6Gxka684aXOhGqScGsymGy()
    {
      if (this.\u0023\u003Dzfl\u0024A1s0\u003D.VisibleRange == null)
        return;
      double y1 = this.\u0023\u003Dzfl\u0024A1s0\u003D.\u0023\u003DzcVHdIBCOOjXh.X + this.\u0023\u003DzSygYxus\u003D.X;
      double y2 = this.\u0023\u003Dzfl\u0024A1s0\u003D.\u0023\u003DzcVHdIBCOOjXh.Y + this.\u0023\u003DzSygYxus\u003D.Y;
      if (this.\u0023\u003Dzfl\u0024A1s0\u003D.IsLogarithmicAxis)
      {
        double logarithmicBase = ((\u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D) this.\u0023\u003Dzfl\u0024A1s0\u003D).get_LogarithmicBase();
        y1 = Math.Pow(logarithmicBase, y1);
        y2 = Math.Pow(logarithmicBase, y2);
      }
      this.\u0023\u003Dzfl\u0024A1s0\u003D.\u0023\u003Dz1itgeDqdlQvE = true;
      this.\u0023\u003Dzfl\u0024A1s0\u003D.VisibleRange = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D(this.\u0023\u003Dzfl\u0024A1s0\u003D.VisibleRange.GetType(), (IComparable) y1, (IComparable) y2);
    }
  }

  private sealed class \u0023\u003DzqHEJJ\u0024eT__xlAQw9dK5OGBo\u003D
  {
    public dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd _variableSome3535;
    public IRange \u0023\u003DzAHNI_S0\u003D;
    public PointAnimation \u0023\u003DzXB4BRQQhi9cE;
    public IRange \u0023\u003Dz\u00245UhFnPQKjzF;

    internal void \u0023\u003Dz90gMLvcCBhHezcHa8o6j4QFx83AR(
    #nullable enable
    object? _param1, EventArgs _param2)
    {
      // ISSUE: explicit non-virtual call
      __nonvirtual (this._variableSome3535.VisibleRange) = this.\u0023\u003DzAHNI_S0\u003D;
      this._variableSome3535.\u0023\u003Dz1itgeDqdlQvE = false;
      this.\u0023\u003DzXB4BRQQhi9cE.FillBehavior = FillBehavior.Stop;
      this._variableSome3535.\u0023\u003Dz_0Le6I5slA7z(new \u0023\u003Dz9Cv\u0024UX3L5m_6hX1ogAvN6swsMiTQ4vauzZKCwXA\u003D(this.\u0023\u003Dz\u00245UhFnPQKjzF, this.\u0023\u003DzAHNI_S0\u003D, this._variableSome3535.\u0023\u003Dz1itgeDqdlQvE));
    }
  }

  private sealed class \u0023\u003Dzztvqky7ki\u0024I\u0024g1MAJ0wAGaM\u003D
  {
    public Decimal \u0023\u003DzvQ3M6iJZUC74;

    internal bool \u0023\u003DzzR02vPcj7iEpCu37cNMcNoyJfLDl(int _param1)
    {
      return this.\u0023\u003DzvQ3M6iJZUC74 % (Decimal) _param1 == 0M;
    }
  }
}
