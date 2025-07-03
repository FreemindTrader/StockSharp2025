// Decompiled with JetBrains decompiler
// Type: #=zVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd$YpfetQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

#nullable disable
internal interface ISciChartSurface : 
  IDisposable,
  ISuspendable,
  \u0023\u003DzRxKCQfwuO1Ym7C1efUUjv1ei5a44WdHi6c16UXGWhmY1mMHOZA\u003D\u003D,
  \u0023\u003DzUib3SzczDtLU7txM4YiSeNZjP0NRThUE6PRgmDMkI3UwPa6FIQ\u003D\u003D,
  IInvalidatableElement
{
  void \u0023\u003DzexJGsaAi6rVI(
    EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D> _param1);

  void \u0023\u003Dz38JNnebwqLph(
    EventHandler<\u0023\u003Dzro0Io1hfSw7LlH634iIk6OhlZAht_WgAqXxl1bw\u003D> _param1);

  void \u0023\u003DzVms\u0024rml2A7zL1pwmGg\u003D\u003D(EventHandler _param1);

  void \u0023\u003Dz6gcEAkTU68jnRIWXQQ\u003D\u003D(EventHandler _param1);

  void \u0023\u003DzLOJcvYqnP9Y4L3YbD2yw_yg\u003D(EventHandler _param1);

  void \u0023\u003DzYo\u0024r0wypzk52wwMTFQ\u0024KNCQ\u003D(EventHandler _param1);

  void \u0023\u003DzgJ2fVeQPjKRri1qXw3qPlis\u003D(EventHandler _param1);

  void \u0023\u003DzxnGwvDMxcoluh8vrt7fwxBo\u003D(EventHandler _param1);

  IChartModifier get_ChartModifier();

  IChartModifier ChartModifier { get; set; }

  void set_ChartModifier(
    IChartModifier _param1);

  \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D get_Annotations();

  \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh5kehUw\u0024c\u0024fhZpDlpYA\u003D Annotations { get; }

  IAxis XAxis { get; set; }

  IAxis YAxis { get; set; }

  \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D get_YAxes();

  \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D YAxes { get; }

  \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D get_XAxes();

  \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUrpBxQ9IN4CBXg\u003D\u003D XAxes { get; }

  \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpB4GFFdsIQ_FR8tlLNjHr1X3p7javA\u003D\u003D \u0023\u003DzTRL\u0024Xy0vYDigfJ9YNg\u003D\u003D();

  ObservableCollection<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D> RenderableSeries { get; }

  ObservableCollection<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D> get_RenderableSeries();

  ObservableCollection<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D> SelectedRenderableSeries { get; }

  ObservableCollection<\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D> get_SelectedRenderableSeries();

  \u0023\u003DzlvwXE9mBO1uItIXfGGLJcGAvOm_MyInBFl6FOhs\u003D \u0023\u003Dzwc4Gzka23TGB();

  \u0023\u003Dz3RRntx4pzkd854dIVpLK6Ww8ODIV2zPrRw\u003D\u003D ViewportManager { get; set; }

  \u0023\u003Dz3RRntx4pzkd854dIVpLK6Ww8ODIV2zPrRw\u003D\u003D get_ViewportManager();

  void set_ViewportManager(
    \u0023\u003Dz3RRntx4pzkd854dIVpLK6Ww8ODIV2zPrRw\u003D\u003D _param1);

  \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk \u0023\u003DzFPPJbPlQRagwT6aZuQ\u003D\u003D();

  \u0023\u003DzNCT3Gnfe2tX07N5vDTkaUhyX2ALXUxEIchh7AgNDmShk \u0023\u003Dz7EP15yq7Yz\u0024jLVX6GgE8gjs\u003D();

  Canvas \u0023\u003DzjEjGZ817bm4EOO82ig\u003D\u003D();

  ObservableCollection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D> SeriesSource { get; set; }

  ObservableCollection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D> get_SeriesSource();

  void set_SeriesSource(
    ObservableCollection<\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D> _param1);

  void \u0023\u003Dzqtb9toLjXu0t();

  Size \u0023\u003DzBr6p5Qw\u0024W6BFNGQPNFOKrj0\u003D();

  bool \u0023\u003DzbOxVzAyGdX66(Point _param1);

  Rect \u0023\u003DzdC9whUui_gN\u0024(
    IHitTestable _param1);

  Point \u0023\u003DzaPPLsvfM_Sst(
    Point _param1,
    IHitTestable _param2);

  IRange \u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D(
    IAxis _param1,
    IRange _param2);

  void \u0023\u003DzOPvUPixjU\u00244Y(
    IAxis _param1,
    dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd _param2);

  void \u0023\u003DzFrczvpG2vhM5(
    IAxis _param1);

  void \u0023\u003Dzf72QDPKj6m\u0024z(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1);

  void \u0023\u003DzbsWonVbyfEPS(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D _param1);

  BitmapSource \u0023\u003Dz12fMuw\u0024m\u002480t();
}
