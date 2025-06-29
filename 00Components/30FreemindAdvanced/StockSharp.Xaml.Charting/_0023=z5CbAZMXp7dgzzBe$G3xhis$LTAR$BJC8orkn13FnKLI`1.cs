// Decompiled with JetBrains decompiler
// Type: #=z5CbAZMXp7dgzzBe$G3xhis$LTAR$BJC8orkn13FnKLIfi3A9i$6SqEijqyQF
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
internal sealed class \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<T> : 
  \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartLineElement>,
  \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D
  where T : struct, IComparable
{
  
  private readonly \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<T, double, double> \u0023\u003DzlkmfHYgr1H49;
  
  private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D \u0023\u003DzKj7nvWQ\u003D;
  
  private \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy \u0023\u003DzZYTLjjg\u003D;
  
  private IComparable \u0023\u003DzFEDR40ugZMK3;
  
  private Func<IComparable, Color?> \u0023\u003DzpGDlrKJMgW0aU9TwiA\u003D\u003D;

  public \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF(
    ChartLineElement _param1)
    : base(_param1)
  {
    Type type = typeof (T);
    if (type != typeof (DateTime) && type != typeof (double))
      throw new NotSupportedException("" + type.Name + "");
    this.\u0023\u003DzlkmfHYgr1H49 = new \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<T, double, double>();
  }

  protected override void \u0023\u003DzY0x9JtY\u003D()
  {
    base.\u0023\u003DzY0x9JtY\u003D();
    DrawStyles[] drawStylesArray = new DrawStyles[9]
    {
      DrawStyles.Line,
      DrawStyles.NoGapLine,
      DrawStyles.StepLine,
      DrawStyles.DashedLine,
      DrawStyles.Dot,
      DrawStyles.Histogram,
      DrawStyles.Bubble,
      DrawStyles.StackedBar,
      DrawStyles.Area
    };
    \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartLineElement>.\u0023\u003Dz9tL3mkpMz5PJ<DrawStyles>((IfxChartElement) this.\u0023\u003DzeaszzAAoBOY9(), "", drawStylesArray);
    string[] strArray = new string[2]
    {
      "",
      ""
    };
    this.GetParentVM().\u0023\u003Dzfc4TzKM\u003D(this.\u0023\u003DzZYTLjjg\u003D = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy((INotifyPropertyChanged) this.\u0023\u003DzeaszzAAoBOY9(), new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003DzQiS8RB0xqqQL6lh\u0024nA\u003D\u003D), \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<T>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DziezdSvgFTxAlfqI9CA\u003D\u003D ?? (\u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<T>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DziezdSvgFTxAlfqI9CA\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<T>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzHJXYrcAe2iQ0KKyLTQ\u003D\u003D)), strArray));
    this.\u0023\u003DzKj7nvWQ\u003D = new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.\u0023\u003DzlkmfHYgr1H49, (IRenderableSeries) null);
    this.\u0023\u003DzAKENEAn7IdgA5685VZNYI6E\u003D();
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003DzBE5I4io\u003D(this.RootElem, (\u0023\u003DziARJyOecclYiJO5UbZqQJ_KOh9jO5RUUcFIAqLc\u003D) this.\u0023\u003DzKj7nvWQ\u003D);
  }

  private Type \u0023\u003Dzq1K_twB2qI4CAVGTXkCEBpY\u003D()
  {
    switch (this.\u0023\u003DzeaszzAAoBOY9().Style)
    {
      case DrawStyles.Line:
      case DrawStyles.NoGapLine:
      case DrawStyles.StepLine:
      case DrawStyles.DashedLine:
        return typeof (dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLQD93DU4393K4ENMLPSZDUWE6QXAWS9WCUYMARFXHNJL76MQ47Z7V297V2U22_ejd);
      case DrawStyles.Dot:
        return typeof (dje_zSETLVVMH9MJSB35ALRM8GUWSBCUA8Q56ZUEXTQWTWAZ5UVJXFWEPJPAAT58X5T8AELBPRA2BLP3FXYY4GU9HTRCBRJYA_ejd);
      case DrawStyles.Histogram:
        return typeof (dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUZY7QRNBNLCAZF4HWQ6A3MJPY73EMQ2VAQAAB3ZH2RUU2VAK3EQE9D32HKF4_ejd);
      case DrawStyles.Bubble:
        return typeof (\u0023\u003DzriJlGz16EBVXNzRMW4Dc4qQp2pUUzikJpe9JEpGuGxRAFL8yxU2NwKkB5lfOfnUo8w7EyJw\u003D);
      case DrawStyles.StackedBar:
        return typeof (dje_zNXC69RR7GVTJT3B9825N7L2M67UQWW3U3T7CLNQCYVESFDW77UGBVTMN2R7TDSCWQ2344S4D5UC36KU2HZS32_ejd);
      case DrawStyles.Area:
        return typeof (dje_zKEMJWHZWSE279KQB6EDEEPQJWFZTHVK3RNB3SECQBVFS2MZJVYVEKR2UZXXBEHEVGBXXDD3GV8492CURMVVPXNR5WLZA_ejd);
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  private void \u0023\u003DzaQ1S7AaIPkwA4Hm2eKJs7\u0024w\u003D()
  {
    if (this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries is dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd renderSeries)
    {
      BindingOperations.ClearAllBindings((DependencyObject) renderSeries);
      this.\u0023\u003DzAgVixDQ6Vc2r();
    }
    dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd ls4St64EqzfbaEjd;
    switch (this.\u0023\u003DzeaszzAAoBOY9().Style)
    {
      case DrawStyles.Line:
      case DrawStyles.NoGapLine:
      case DrawStyles.StepLine:
      case DrawStyles.DashedLine:
        ls4St64EqzfbaEjd = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLQD93DU4393K4ENMLPSZDUWE6QXAWS9WCUYMARFXHNJL76MQ47Z7V297V2U22_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        });
        ls4St64EqzfbaEjd.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIcVMwZBBZ1n3, (object) this.\u0023\u003DzeaszzAAoBOY9(), "");
        break;
      case DrawStyles.Dot:
        ls4St64EqzfbaEjd = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zSETLVVMH9MJSB35ALRM8GUWSBCUA8Q56ZUEXTQWTWAZ5UVJXFWEPJPAAT58X5T8AELBPRA2BLP3FXYY4GU9HTRCBRJYA_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        });
        ls4St64EqzfbaEjd.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIcVMwZBBZ1n3, (object) this.\u0023\u003DzeaszzAAoBOY9(), "");
        ls4St64EqzfbaEjd.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzNGe3htdX6rpV, (object) this.\u0023\u003DzeaszzAAoBOY9(), "");
        break;
      case DrawStyles.Histogram:
        dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUZY7QRNBNLCAZF4HWQ6A3MJPY73EMQ2VAQAAB3ZH2RUU2VAK3EQE9D32HKF4_ejd k3EqE9D32HkF4Ejd;
        ls4St64EqzfbaEjd = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) (k3EqE9D32HkF4Ejd = this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zCDT3DA89AWY9Q5XX2ZVAWAKKUUZY7QRNBNLCAZF4HWQ6A3MJPY73EMQ2VAQAAB3ZH2RUU2VAK3EQE9D32HKF4_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        }));
        k3EqE9D32HkF4Ejd.SetBindings(dje_zHYRPNQHHBLUELC2PHC86VUXPKVH2ZMKXPKFTYYKX6SKX4JY2EDLNT4C8JKXXPNRQ42CXGSK8E94CEP2_ejd.\u0023\u003DzwCSejucukq6W, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", converter: (IValueConverter) new ColorToBrushConverter());
        k3EqE9D32HkF4Ejd.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIcVMwZBBZ1n3, (object) this.\u0023\u003DzeaszzAAoBOY9(), "");
        break;
      case DrawStyles.Bubble:
        \u0023\u003DzriJlGz16EBVXNzRMW4Dc4qQp2pUUzikJpe9JEpGuGxRAFL8yxU2NwKkB5lfOfnUo8w7EyJw\u003D b5lfOfnUo8w7EyJw;
        ls4St64EqzfbaEjd = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) (b5lfOfnUo8w7EyJw = this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<\u0023\u003DzriJlGz16EBVXNzRMW4Dc4qQp2pUUzikJpe9JEpGuGxRAFL8yxU2NwKkB5lfOfnUo8w7EyJw\u003D>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        }));
        b5lfOfnUo8w7EyJw.ResamplingMode = \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.None;
        b5lfOfnUo8w7EyJw.SetBindings(\u0023\u003DzriJlGz16EBVXNzRMW4Dc4qQp2pUUzikJpe9JEpGuGxRAFL8yxU2NwKkB5lfOfnUo8w7EyJw\u003D.\u0023\u003DzWsyKEigY1Lm6, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", converter: (IValueConverter) new ColorToBrushConverter());
        b5lfOfnUo8w7EyJw.SetBindings(\u0023\u003DzriJlGz16EBVXNzRMW4Dc4qQp2pUUzikJpe9JEpGuGxRAFL8yxU2NwKkB5lfOfnUo8w7EyJw\u003D.\u0023\u003DzgLLxE9j2DbxR, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", converter: (IValueConverter) new \u0023\u003DzQ4iRj1YTApc8D349VbLPOcYfVH1n3cgfJefIZNttPgl056hG45kULRE\u003D());
        break;
      case DrawStyles.StackedBar:
        dje_zNXC69RR7GVTJT3B9825N7L2M67UQWW3U3T7CLNQCYVESFDW77UGBVTMN2R7TDSCWQ2344S4D5UC36KU2HZS32_ejd d5Uc36Ku2HzS32Ejd;
        ls4St64EqzfbaEjd = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) (d5Uc36Ku2HzS32Ejd = this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zNXC69RR7GVTJT3B9825N7L2M67UQWW3U3T7CLNQCYVESFDW77UGBVTMN2R7TDSCWQ2344S4D5UC36KU2HZS32_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        }));
        d5Uc36Ku2HzS32Ejd.UseUniformWidth = true;
        d5Uc36Ku2HzS32Ejd.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIcVMwZBBZ1n3, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", converter: (IValueConverter) new \u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqKuhsUbi2xjkbRlj6EVaEl1lCbDsuw\u003D\u003D(), parameter: (object) 51);
        d5Uc36Ku2HzS32Ejd.SetBindings(dje_zHYRPNQHHBLUELC2PHC86VUXPKVH2ZMKXPKFTYYKX6SKX4JY2EDLNT4C8JKXXPNRQ42CXGSK8E94CEP2_ejd.\u0023\u003DzwCSejucukq6W, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", converter: (IValueConverter) new ColorToBrushConverter());
        d5Uc36Ku2HzS32Ejd.SetBindings(dje_zHYRPNQHHBLUELC2PHC86VUXPKVH2ZMKXPKFTYYKX6SKX4JY2EDLNT4C8JKXXPNRQ42CXGSK8E94CEP2_ejd.\u0023\u003DzVvc2lVdKTrj8, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", converter: (IValueConverter) new \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhiiYRKe0897RDjLr\u0024L9wcxjXImUKaPnpxZj0\u003D());
        break;
      case DrawStyles.Area:
        dje_zKEMJWHZWSE279KQB6EDEEPQJWFZTHVK3RNB3SECQBVFS2MZJVYVEKR2UZXXBEHEVGBXXDD3GV8492CURMVVPXNR5WLZA_ejd curmvvpxnR5WlzaEjd;
        ls4St64EqzfbaEjd = (dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd) (curmvvpxnR5WlzaEjd = this.\u0023\u003Dzj4cwTqTBSZ3fAaZzTX46uig\u003D<dje_zKEMJWHZWSE279KQB6EDEEPQJWFZTHVK3RNB3SECQBVFS2MZJVYVEKR2UZXXBEHEVGBXXDD3GV8492CURMVVPXNR5WLZA_ejd>(new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        }));
        curmvvpxnR5WlzaEjd.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzIcVMwZBBZ1n3, (object) this.\u0023\u003DzeaszzAAoBOY9(), "");
        curmvvpxnR5WlzaEjd.SetBindings(dje_z69W49PU7FS9ADZCYETBA2JNAH33HZA6JARXFFS3478TNDWQCS56A84JEUPSV3BARVVHU59UKY87PYJ97Q4K7W5RSK8AA_ejd.\u0023\u003DzXc9apgJiH9mm, (object) this.\u0023\u003DzeaszzAAoBOY9(), "", converter: (IValueConverter) new ColorToBrushConverter());
        break;
      default:
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnsupportedType, new object[1]
        {
          (object) this.\u0023\u003DzeaszzAAoBOY9().Style
        }));
    }
    ls4St64EqzfbaEjd.SetBindings(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dz13qAkT\u0024eFXTPuGCUqjA\u0024Svw\u003D, (object) this.\u0023\u003DzeaszzAAoBOY9(), "");
    ls4St64EqzfbaEjd.PaletteProvider = (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D) this;
    this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries = (IRenderableSeries) ls4St64EqzfbaEjd;
    this.\u0023\u003Dz7GhHTEkMkDYT(this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries, (IfxChartElement) this.\u0023\u003DzeaszzAAoBOY9(), "", "");
    this.\u0023\u003DzAKENEAn7IdgA5685VZNYI6E\u003D();
  }

  private void \u0023\u003DzAKENEAn7IdgA5685VZNYI6E\u003D()
  {
    Type type = this.\u0023\u003Dzq1K_twB2qI4CAVGTXkCEBpY\u003D();
    if (this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries?.GetType() != type)
    {
      this.\u0023\u003DzaQ1S7AaIPkwA4Hm2eKJs7\u0024w\u003D();
    }
    else
    {
      if (!(this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries is dje_zN8RD3UL4Q5RJJYR8DXFMVFNTLQD93DU4393K4ENMLPSZDUWE6QXAWS9WCUYMARFXHNJL76MQ47Z7V297V2U22_ejd renderSeries))
        return;
      renderSeries.StrokeDashArray = (double[]) null;
      renderSeries.IsDigitalLine = false;
      renderSeries.DrawNaNAs = \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.Gaps;
      if (this.\u0023\u003DzeaszzAAoBOY9().Style == DrawStyles.DashedLine)
        renderSeries.StrokeDashArray = new double[2]
        {
          5.0,
          5.0
        };
      else if (this.\u0023\u003DzeaszzAAoBOY9().Style == DrawStyles.StepLine)
      {
        renderSeries.IsDigitalLine = true;
      }
      else
      {
        if (this.\u0023\u003DzeaszzAAoBOY9().Style != DrawStyles.NoGapLine)
          return;
        renderSeries.DrawNaNAs = \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.ClosedLines;
      }
    }
  }

  protected override void \u0023\u003DzXfak0jM\u003D()
  {
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().\u0023\u003Dzwh_e_TheVZKh(this.RootElem);
  }

  protected override void \u0023\u003DzowR7R4A\u003D()
  {
    this.\u0023\u003DzlkmfHYgr1H49.Clear();
    this.\u0023\u003DzFEDR40ugZMK3 = (IComparable) default (T);
  }

  public override bool \u0023\u003DzjgUUUJE\u003D(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.\u0023\u003DzjgUUUJE\u003D<T>(CollectionHelper.ToEx<ChartDrawData.sxTuple<T>>(((IEnumerable) _param1).Cast<ChartDrawData.sxTuple<T>>(), ((IEnumerableEx) _param1).Count));
  }

  public bool \u0023\u003DzjgUUUJE\u003D<TX1>(
    IEnumerableEx<ChartDrawData.sxTuple<TX1>> _param1)
    where TX1 : struct, IComparable
  {
    if (this.\u0023\u003DzpGDlrKJMgW0aU9TwiA\u003D\u003D != this.\u0023\u003DzeaszzAAoBOY9().Colorer)
    {
      this.\u0023\u003DzpGDlrKJMgW0aU9TwiA\u003D\u003D = this.\u0023\u003DzeaszzAAoBOY9().Colorer;
      this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries.\u0023\u003Dzu\u0024P3XgkcE7BC()?.\u0023\u003Dz2VqWonc\u003D<\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D>()?.\u0023\u003Dz5q8i9C4\u003D();
    }
    if (_param1 == null || CollectionHelper.IsEmpty<ChartDrawData.sxTuple<TX1>>((IEnumerable<ChartDrawData.sxTuple<TX1>>) _param1))
      return false;
    int count = ((IEnumerableEx) _param1).Count;
    IComparable comparable = this.\u0023\u003DzFEDR40ugZMK3;
    int index = -1;
    T[] array1 = new T[count];
    double[] array2 = new double[count];
    double[] array3 = new double[count];
    List<ChartDrawData.sxTuple<TX1>> z6MdlWkBsH4List = new List<ChartDrawData.sxTuple<TX1>>();
    foreach (ChartDrawData.sxTuple<TX1> z6MdlWkBsH4 in (IEnumerable<ChartDrawData.sxTuple<TX1>>) _param1)
    {
      T zulcL8Ra = (T) (ValueType) z6MdlWkBsH4.Property();
      switch (zulcL8Ra.CompareTo((object) comparable))
      {
        case -1:
          throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.CannotChangeCandleValue, new object[2]
          {
            (object) zulcL8Ra,
            (object) comparable
          }));
        case 0:
          if (z6MdlWkBsH4.GetIntegerValue() == 0)
            this.\u0023\u003DzlkmfHYgr1H49.\u0023\u003DzFkV86a8\u003D(zulcL8Ra, z6MdlWkBsH4.ValueOne(), z6MdlWkBsH4.ValueTwo(), 0);
          --count;
          break;
        default:
          ++index;
          array1[index] = zulcL8Ra;
          if (z6MdlWkBsH4.GetIntegerValue() > 0)
          {
            array2[index] = double.NaN;
            array3[index] = double.NaN;
            break;
          }
          array2[index] = z6MdlWkBsH4.ValueOne();
          array3[index] = z6MdlWkBsH4.ValueTwo();
          break;
      }
      if (z6MdlWkBsH4.GetIntegerValue() > 0)
        z6MdlWkBsH4List.Add(z6MdlWkBsH4);
      comparable = (IComparable) zulcL8Ra;
    }
    if (count == 0)
      return false;
    Array.Resize<T>(ref array1, count);
    Array.Resize<double>(ref array2, count);
    Array.Resize<double>(ref array3, count);
    this.\u0023\u003DzlkmfHYgr1H49.\u0023\u003Dznc8esWY\u003D((IEnumerable<T>) array1, (IEnumerable<double>) array2, (IEnumerable<double>) array3);
    if (z6MdlWkBsH4List.Count > 0)
    {
      foreach (ChartDrawData.sxTuple<TX1> z6MdlWkBsH4 in z6MdlWkBsH4List)
        this.\u0023\u003DzlkmfHYgr1H49.\u0023\u003DzFkV86a8\u003D((T) (ValueType) z6MdlWkBsH4.Property(), z6MdlWkBsH4.ValueOne(), z6MdlWkBsH4.ValueTwo(), z6MdlWkBsH4.GetIntegerValue());
    }
    this.\u0023\u003DzFEDR40ugZMK3 = comparable;
    return true;
  }

  protected override void \u0023\u003Dz3u1qwgvgJlZC(
    IfxChartElement _param1,
    string _param2)
  {
    base.\u0023\u003Dz3u1qwgvgJlZC(_param1, _param2);
    if (!(_param2 == ""))
      return;
    this.\u0023\u003DzAKENEAn7IdgA5685VZNYI6E\u003D();
  }

  Color? \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D.\u0023\u003DzaP7vgnwtOd1ghQwnj\u00248jG1HXnj3oIOVYNqLIS92dT0MqcEWOD7IYv5ohC5gQ(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    return new Color?();
  }

  Color? \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D.\u0023\u003DzaP7vgnwtOd1ghQwnj\u00248jG1HXnj3oIOVYNqLIS92dT0MqcEWOD7IYv5ohC5gQ(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6)
  {
    return new Color?();
  }

  Color? \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D.\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC3yzxavQxmzggPmZ8V62OI0Kr0hq2Km30eq101sCk(
    IRenderableSeries _param1,
    double _param2,
    double _param3)
  {
    if (!(_param1.get_DataSeries() is \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<T, double, double> dataSeries))
      return new Color?();
    int index = (int) _param2;
    if (!(typeof (T) == typeof (DateTime)))
    {
      Func<IComparable, Color?> gdlrKjMgW0aU9TwiA = this.\u0023\u003DzpGDlrKJMgW0aU9TwiA\u003D\u003D;
      return gdlrKjMgW0aU9TwiA == null ? new Color?() : gdlrKjMgW0aU9TwiA((IComparable) _param2);
    }
    Func<IComparable, Color?> gdlrKjMgW0aU9TwiA1 = this.\u0023\u003DzpGDlrKJMgW0aU9TwiA\u003D\u003D;
    return gdlrKjMgW0aU9TwiA1 == null ? new Color?() : gdlrKjMgW0aU9TwiA1((IComparable) TimeHelper.ToDateTimeOffset((DateTime) (ValueType) dataSeries.XValues[index], TimeZoneInfo.Utc));
  }

  private Color \u0023\u003DzQiS8RB0xqqQL6lh\u0024nA\u003D\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    return this.\u0023\u003DzeaszzAAoBOY9().Style == DrawStyles.StackedBar || this.\u0023\u003DzeaszzAAoBOY9().Style == DrawStyles.Area ? \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy.\u0023\u003DzLHZrYbpAZyAx(this.\u0023\u003DzeaszzAAoBOY9().Color, this.\u0023\u003DzeaszzAAoBOY9().AdditionalColor) : this.\u0023\u003DzeaszzAAoBOY9().Color;
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<T>.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<T>.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003DziezdSvgFTxAlfqI9CA\u003D\u003D;

    internal string \u0023\u003DzHJXYrcAe2iQ0KKyLTQ\u003D\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return _param1.FormattedYValue;
    }
  }
}
