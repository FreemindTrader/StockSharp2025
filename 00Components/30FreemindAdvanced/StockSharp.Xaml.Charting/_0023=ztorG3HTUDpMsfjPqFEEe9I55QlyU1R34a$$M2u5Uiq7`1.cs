// Decompiled with JetBrains decompiler
// Type: #=ztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a$$M2u5Uiq7Pu7_oc1A1JQ8nQQRm
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Drawing;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D> : 
  \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartBandElement>
  where \u0023\u003DzulcL8RA\u003D : struct, IComparable
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<\u0023\u003DzulcL8RA\u003D, double> \u0023\u003DzDh1lJfFlHUWk;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<\u0023\u003DzulcL8RA\u003D, double> \u0023\u003Dzva\u002460XCiLL2n;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<\u0023\u003DzulcL8RA\u003D, double> \u0023\u003DzRSDkBpQ1QWG0;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D \u0023\u003DzYirGqB2gXz09;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D \u0023\u003DzIHjxTqC159pe;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D \u0023\u003DzRm0WUjzJSu8n;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy \u0023\u003Dzh0ozsIDILK5b;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy \u0023\u003DzXNWLRaQhQW_0;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IComparable \u0023\u003DzFEDR40ugZMK3;

  public \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm(
    ChartBandElement _param1)
    : base(_param1)
  {
    Type type = typeof (\u0023\u003DzulcL8RA\u003D);
    if (type != typeof (DateTime) && type != typeof (double))
      throw new NotSupportedException(XXX.SSS(-539330337) + type.Name + XXX.SSS(-539330387));
    this.\u0023\u003DzDh1lJfFlHUWk = new \u0023\u003DzpKvy0OA0_My0Sg27HiUJaXC_MvbCQSSYY52HUUP8Kgkq<\u0023\u003DzulcL8RA\u003D, double>();
    this.\u0023\u003Dzva\u002460XCiLL2n = new \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<\u0023\u003DzulcL8RA\u003D, double>();
    this.\u0023\u003DzRSDkBpQ1QWG0 = new \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<\u0023\u003DzulcL8RA\u003D, double>();
  }

  protected override void \u0023\u003DzY0x9JtY\u003D()
  {
    base.\u0023\u003DzY0x9JtY\u003D();
    DrawStyles[] drawStylesArray = new DrawStyles[4]
    {
      DrawStyles.Line,
      DrawStyles.NoGapLine,
      DrawStyles.StepLine,
      DrawStyles.DashedLine
    };
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartBandElement>.\u0023\u003Dz9tL3mkpMz5PJ<DrawStyles>((\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D) this.\u0023\u003DzeaszzAAoBOY9().Line1, XXX.SSS(-539433382), drawStylesArray);
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartBandElement>.\u0023\u003Dz9tL3mkpMz5PJ<DrawStyles>((\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D) this.\u0023\u003DzeaszzAAoBOY9().Line2, XXX.SSS(-539433382), drawStylesArray);
    string[] strArray = new string[2]
    {
      XXX.SSS(-539433444),
      XXX.SSS(-539433418)
    };
    this.\u0023\u003DzXU6fBD\u0024oSmCR().\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003Dzh0ozsIDILK5b = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy((INotifyPropertyChanged) this.\u0023\u003DzeaszzAAoBOY9().Line1, new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dzgl15kO2PtK5giK8GzTIwj08\u003D), \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D ?? (\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz_kGaLH\u0024IiOuHLLvhkS2WN_s\u003D)), strArray));
    this.\u0023\u003DzXU6fBD\u0024oSmCR().\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003DzXNWLRaQhQW_0 = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy((INotifyPropertyChanged) this.\u0023\u003DzeaszzAAoBOY9().Line2, new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003Dz7GRJChTRxjdOfay3RCW2oqs\u003D), \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzuwIb9LpQWiX75WO6PQ\u003D\u003D ?? (\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzuwIb9LpQWiX75WO6PQ\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzMUMqMtnuX9rATNeQa7kIAUA\u003D)), strArray));
    this.\u0023\u003DzZcbqdpE\u003D((\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D) this.\u0023\u003DzeaszzAAoBOY9().Line1);
    this.\u0023\u003DzZcbqdpE\u003D((\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D) this.\u0023\u003DzeaszzAAoBOY9().Line2);
    this.\u0023\u003DzYirGqB2gXz09 = new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.\u0023\u003Dzva\u002460XCiLL2n, (\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D) null);
    this.\u0023\u003DzIHjxTqC159pe = new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.\u0023\u003DzRSDkBpQ1QWG0, (\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D) null);
    this.\u0023\u003DzRm0WUjzJSu8n = new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.\u0023\u003DzDh1lJfFlHUWk, (\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D) null);
    this.\u0023\u003DzcIqdE4oVd9lsrOCnFSgflME\u003D();
    this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzYirGqB2gXz09, this.\u0023\u003DzeaszzAAoBOY9().Line1, this.\u0023\u003Dzh0ozsIDILK5b);
    this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzIHjxTqC159pe, this.\u0023\u003DzeaszzAAoBOY9().Line2, this.\u0023\u003DzXNWLRaQhQW_0);
    this.\u0023\u003Dz7GhHTEkMkDYT(this.\u0023\u003DzYirGqB2gXz09.RenderSeries, (\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D) this.\u0023\u003DzeaszzAAoBOY9().Line1, XXX.SSS(-539434714), XXX.SSS(-539433444));
    this.\u0023\u003Dz7GhHTEkMkDYT(this.\u0023\u003DzIHjxTqC159pe.RenderSeries, (\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D) this.\u0023\u003DzeaszzAAoBOY9().Line2, XXX.SSS(-539434714), XXX.SSS(-539433444));
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzBE5I4io\u003D(this.RootElem, (\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D) this.\u0023\u003DzYirGqB2gXz09);
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzBE5I4io\u003D(this.RootElem, (\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D) this.\u0023\u003DzIHjxTqC159pe);
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzBE5I4io\u003D(this.RootElem, (\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D) this.\u0023\u003DzRm0WUjzJSu8n);
    this.\u0023\u003DzJGn0U4ESy8cx();
  }

  private void \u0023\u003DzJGn0U4ESy8cx()
  {
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartBandElement>.\u0023\u003DzpbLgaWJ0hngn(this.\u0023\u003DzRm0WUjzJSu8n.RenderSeries, false);
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartBandElement>.\u0023\u003DzpbLgaWJ0hngn(this.\u0023\u003DzYirGqB2gXz09.RenderSeries, true);
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartBandElement>.\u0023\u003DzpbLgaWJ0hngn(this.\u0023\u003DzIHjxTqC159pe.RenderSeries, this.\u0023\u003DzeaszzAAoBOY9().Style == DrawStyles.Band);
  }

  private void \u0023\u003DzcIqdE4oVd9lsrOCnFSgflME\u003D()
  {
    if (this.\u0023\u003DzRm0WUjzJSu8n.RenderSeries is dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd)
      return;
    dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd xc8MfylkhywkcxaEjd = this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd>(Array.Empty<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy>());
    this.\u0023\u003DzRm0WUjzJSu8n.RenderSeries = (\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D) xc8MfylkhywkcxaEjd;
    xc8MfylkhywkcxaEjd.SeriesColor = xc8MfylkhywkcxaEjd.Series1Color = Colors.Transparent;
    xc8MfylkhywkcxaEjd.SetBindings(dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd.\u0023\u003DzqXdfa4iudfGA, (object) this.\u0023\u003DzeaszzAAoBOY9().Line1, XXX.SSS(-539433418));
    xc8MfylkhywkcxaEjd.SetBindings(dje_zHZJUNELMY3BAWUYNNRAVXVEJSHT9V29B8J9F3NL335LNBX9S38NTQ32RC2TS5VYKTNJEX9XC8MFYLKHYWKCXA_ejd.\u0023\u003DzJJDbrFb7cXV\u0024, (object) this.\u0023\u003DzeaszzAAoBOY9().Line2, XXX.SSS(-539433418));
  }

  private void \u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(
    \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D _param1,
    ChartLineElement _param2,
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy _param3)
  {
    if (!(_param1.RenderSeries is dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLQD93DU4393K4ENMLPSZDUWE6QXAWS9WCUYMARFXHNJL76MQ47Z7V297V2U22_ejd mq47Z7V297V2U22Ejd))
    {
      \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D js00Vef8BmoZoBlhDa = _param1;
      \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[] h0icPdKkp5z7HsjcOyArray = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
      {
        _param3
      };
      dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLQD93DU4393K4ENMLPSZDUWE6QXAWS9WCUYMARFXHNJL76MQ47Z7V297V2U22_ejd mq47Z7V297V2U22Ejd1;
      mq47Z7V297V2U22Ejd = mq47Z7V297V2U22Ejd1 = this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLQD93DU4393K4ENMLPSZDUWE6QXAWS9WCUYMARFXHNJL76MQ47Z7V297V2U22_ejd>(h0icPdKkp5z7HsjcOyArray);
      js00Vef8BmoZoBlhDa.RenderSeries = (\u0023\u003DzA\u0024A4W5SfT\u0024DiuyUN7UYciXZRQS6mpDuG09xUExO4eQukbot9S1JOL\u0024YRWoYpqmQ6ug\u003D\u003D) mq47Z7V297V2U22Ejd1;
      mq47Z7V297V2U22Ejd.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIcVMwZBBZ1n3, (object) _param2, XXX.SSS(-539433444));
      mq47Z7V297V2U22Ejd.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz13qAkT\u0024eFXTPuGCUqjA\u0024Svw\u003D, (object) _param2, XXX.SSS(-539428560));
      mq47Z7V297V2U22Ejd.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzTe_gV3cWjEp7, (object) _param2, XXX.SSS(-539434641));
      mq47Z7V297V2U22Ejd.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzdr5RTntdbeN7, (object) _param2, XXX.SSS(-539434635));
      dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLQD93DU4393K4ENMLPSZDUWE6QXAWS9WCUYMARFXHNJL76MQ47Z7V297V2U22_ejd mq47Z7V297V2U22Ejd2 = mq47Z7V297V2U22Ejd;
      DependencyProperty z8b6MqaiE8Uzn = dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz8b6MQAIE8UZn;
      BoolAllConverter conv = new BoolAllConverter();
      conv.Value = true;
      Binding[] bindingArray = new Binding[3]
      {
        new Binding(XXX.SSS(-539433813))
        {
          Source = (object) _param2
        },
        new Binding(XXX.SSS(-539433813))
        {
          Source = (object) this.\u0023\u003DzeaszzAAoBOY9()
        },
        new Binding(XXX.SSS(-539433813))
        {
          Source = (object) ((\u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D) this.\u0023\u003DzeaszzAAoBOY9()).RootElement
        }
      };
      mq47Z7V297V2U22Ejd2.SetMultiBinding(z8b6MqaiE8Uzn, (IMultiValueConverter) conv, bindingArray);
    }
    mq47Z7V297V2U22Ejd.StrokeDashArray = (double[]) null;
    mq47Z7V297V2U22Ejd.IsDigitalLine = false;
    if (_param2.Style == DrawStyles.DashedLine)
    {
      mq47Z7V297V2U22Ejd.StrokeDashArray = new double[2]
      {
        5.0,
        5.0
      };
    }
    else
    {
      if (_param2.Style != DrawStyles.StepLine)
        return;
      mq47Z7V297V2U22Ejd.IsDigitalLine = true;
    }
  }

  protected override void \u0023\u003DzXfak0jM\u003D()
  {
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003Dzwh_e_TheVZKh(this.RootElem);
  }

  protected override void \u0023\u003DzowR7R4A\u003D()
  {
    this.\u0023\u003Dzva\u002460XCiLL2n.Clear();
    this.\u0023\u003DzRSDkBpQ1QWG0.Clear();
    this.\u0023\u003DzDh1lJfFlHUWk.Clear();
    this.\u0023\u003DzFEDR40ugZMK3 = (IComparable) default (\u0023\u003DzulcL8RA\u003D);
  }

  public override bool \u0023\u003DzjgUUUJE\u003D(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.\u0023\u003DzjgUUUJE\u003D<\u0023\u003DzulcL8RA\u003D>(CollectionHelper.ToEx<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D>>(((IEnumerable) _param1).Cast<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D>>(), ((IEnumerableEx) _param1).Count));
  }

  public bool \u0023\u003DzjgUUUJE\u003D<TX1>(
    IEnumerableEx<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1>> _param1)
    where TX1 : struct, IComparable
  {
    if (_param1 == null)
      return false;
    int count = ((IEnumerableEx) _param1).Count;
    IComparable comparable = this.\u0023\u003DzFEDR40ugZMK3;
    int index = -1;
    \u0023\u003DzulcL8RA\u003D[] array1 = new \u0023\u003DzulcL8RA\u003D[count];
    double[] array2 = new double[count];
    double[] array3 = new double[count];
    foreach (ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1> z6MdlWkBsH4 in (IEnumerable<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1>>) _param1)
    {
      \u0023\u003DzulcL8RA\u003D zulcL8Ra = (\u0023\u003DzulcL8RA\u003D) (ValueType) z6MdlWkBsH4.\u0023\u003Dz2_4KSTY\u003D();
      switch (zulcL8Ra.CompareTo((object) comparable))
      {
        case -1:
          throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.CannotChangeCandleValue, new object[2]
          {
            (object) zulcL8Ra,
            (object) comparable
          }));
        case 0:
          this.\u0023\u003Dzva\u002460XCiLL2n.\u0023\u003DzFkV86a8\u003D(zulcL8Ra, z6MdlWkBsH4.\u0023\u003DzZB\u0024O5xT4bzKv());
          this.\u0023\u003DzRSDkBpQ1QWG0.\u0023\u003DzFkV86a8\u003D(zulcL8Ra, z6MdlWkBsH4.\u0023\u003Dzggdh\u0024\u00245CXRMA());
          this.\u0023\u003DzDh1lJfFlHUWk.\u0023\u003DzFkV86a8\u003D(zulcL8Ra, z6MdlWkBsH4.\u0023\u003DzZB\u0024O5xT4bzKv(), z6MdlWkBsH4.\u0023\u003Dzggdh\u0024\u00245CXRMA());
          --count;
          break;
        default:
          ++index;
          array1[index] = zulcL8Ra;
          array2[index] = z6MdlWkBsH4.\u0023\u003DzZB\u0024O5xT4bzKv();
          array3[index] = z6MdlWkBsH4.\u0023\u003Dzggdh\u0024\u00245CXRMA();
          break;
      }
      comparable = (IComparable) zulcL8Ra;
    }
    if (count == 0)
      return false;
    Array.Resize<\u0023\u003DzulcL8RA\u003D>(ref array1, count);
    Array.Resize<double>(ref array2, count);
    Array.Resize<double>(ref array3, count);
    this.\u0023\u003Dzva\u002460XCiLL2n.\u0023\u003Dznc8esWY\u003D((IEnumerable<\u0023\u003DzulcL8RA\u003D>) array1, (IEnumerable<double>) array2);
    this.\u0023\u003DzRSDkBpQ1QWG0.\u0023\u003Dznc8esWY\u003D((IEnumerable<\u0023\u003DzulcL8RA\u003D>) array1, (IEnumerable<double>) array3);
    this.\u0023\u003DzDh1lJfFlHUWk.\u0023\u003Dznc8esWY\u003D((IEnumerable<\u0023\u003DzulcL8RA\u003D>) array1, (IEnumerable<double>) array2, (IEnumerable<double>) array3);
    this.\u0023\u003DzFEDR40ugZMK3 = comparable;
    return true;
  }

  protected override void \u0023\u003Dz3u1qwgvgJlZC(
    \u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D _param1,
    string _param2)
  {
    base.\u0023\u003Dz3u1qwgvgJlZC(_param1, _param2);
    if (_param2 == XXX.SSS(-539433382))
    {
      this.\u0023\u003DzcIqdE4oVd9lsrOCnFSgflME\u003D();
      this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzYirGqB2gXz09, this.\u0023\u003DzeaszzAAoBOY9().Line1, this.\u0023\u003Dzh0ozsIDILK5b);
      this.\u0023\u003DzD3DulDZVZwBVkucTxfPDSEA\u003D(this.\u0023\u003DzIHjxTqC159pe, this.\u0023\u003DzeaszzAAoBOY9().Line2, this.\u0023\u003DzXNWLRaQhQW_0);
    }
    if (!(_param2 == XXX.SSS(-539433382)))
      return;
    this.\u0023\u003DzJGn0U4ESy8cx();
  }

  private Color \u0023\u003Dzgl15kO2PtK5giK8GzTIwj08\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    return \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy.\u0023\u003DzLHZrYbpAZyAx(this.\u0023\u003DzeaszzAAoBOY9().Line1.Color, this.\u0023\u003DzeaszzAAoBOY9().Line1.AdditionalColor);
  }

  private Color \u0023\u003Dz7GRJChTRxjdOfay3RCW2oqs\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    return \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy.\u0023\u003DzLHZrYbpAZyAx(this.\u0023\u003DzeaszzAAoBOY9().Line2.Color, this.\u0023\u003DzeaszzAAoBOY9().Line2.AdditionalColor);
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<\u0023\u003DzulcL8RA\u003D>.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003DzQKnO3tXJvatO0gqnsg\u003D\u003D;
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003DzuwIb9LpQWiX75WO6PQ\u003D\u003D;

    internal string \u0023\u003Dz_kGaLH\u0024IiOuHLLvhkS2WN_s\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return _param1.FormattedYValue;
    }

    internal string \u0023\u003DzMUMqMtnuX9rATNeQa7kIAUA\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return _param1.FormattedYValue;
    }
  }
}
