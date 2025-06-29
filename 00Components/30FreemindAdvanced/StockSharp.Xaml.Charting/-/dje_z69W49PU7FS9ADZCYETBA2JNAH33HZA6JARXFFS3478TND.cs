// Decompiled with JetBrains decompiler
// Type: -.dje_z69W49PU7FS9ADZCYETBA2JNAH33HZA6JARXFFS3478TNDWQCS56A84JEUPSV3BARVVHU59UKY87PYJ97Q4K7W5RSK8AA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal abstract class dje_z69W49PU7FS9ADZCYETBA2JNAH33HZA6JARXFFS3478TNDWQCS56A84JEUPSV3BARVVHU59UKY87PYJ97Q4K7W5RSK8AA_ejd : 
  dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd
{
  
  public static readonly DependencyProperty \u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D = DependencyProperty.Register("", typeof (bool), typeof (dje_z69W49PU7FS9ADZCYETBA2JNAH33HZA6JARXFFS3478TNDWQCS56A84JEUPSV3BARVVHU59UKY87PYJ97Q4K7W5RSK8AA_ejd), new PropertyMetadata((object) false, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzXc9apgJiH9mm = DependencyProperty.Register("", typeof (Brush), typeof (dje_z69W49PU7FS9ADZCYETBA2JNAH33HZA6JARXFFS3478TNDWQCS56A84JEUPSV3BARVVHU59UKY87PYJ97Q4K7W5RSK8AA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  private Color \u0023\u003Dz7whQ6lqoKrkWxT8_LQ\u003D\u003D;

  protected dje_z69W49PU7FS9ADZCYETBA2JNAH33HZA6JARXFFS3478TNDWQCS56A84JEUPSV3BARVVHU59UKY87PYJ97Q4K7W5RSK8AA_ejd()
  {
    this.SetCurrentValue(dje_zP6VTXEU8B2YELVEGUZAJ27BBHF7M9YENVCNNCT32898EYMRSJSW3C8HB7ESPM23CV4LS4ST64EQZFBA_ejd.\u0023\u003DzZgWT7YttYHbwyP3zHCVW0zI\u003D, (object) \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.Max);
  }

  public Brush AreaBrush
  {
    get
    {
      return (Brush) this.GetValue(dje_z69W49PU7FS9ADZCYETBA2JNAH33HZA6JARXFFS3478TNDWQCS56A84JEUPSV3BARVVHU59UKY87PYJ97Q4K7W5RSK8AA_ejd.\u0023\u003DzXc9apgJiH9mm);
    }
    set
    {
      this.SetValue(dje_z69W49PU7FS9ADZCYETBA2JNAH33HZA6JARXFFS3478TNDWQCS56A84JEUPSV3BARVVHU59UKY87PYJ97Q4K7W5RSK8AA_ejd.\u0023\u003DzXc9apgJiH9mm, (object) value);
    }
  }

  public Color \u0023\u003DzrCwKa1niXYKW() => this.\u0023\u003Dz7whQ6lqoKrkWxT8_LQ\u003D\u003D;

  public void \u0023\u003DzIIOrZiK2cjgx(Color _param1)
  {
    this.\u0023\u003Dz7whQ6lqoKrkWxT8_LQ\u003D\u003D = _param1;
  }

  public bool IsDigitalLine
  {
    get
    {
      return (bool) this.GetValue(dje_z69W49PU7FS9ADZCYETBA2JNAH33HZA6JARXFFS3478TNDWQCS56A84JEUPSV3BARVVHU59UKY87PYJ97Q4K7W5RSK8AA_ejd.\u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_z69W49PU7FS9ADZCYETBA2JNAH33HZA6JARXFFS3478TNDWQCS56A84JEUPSV3BARVVHU59UKY87PYJ97Q4K7W5RSK8AA_ejd.\u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D, (object) value);
    }
  }
}
