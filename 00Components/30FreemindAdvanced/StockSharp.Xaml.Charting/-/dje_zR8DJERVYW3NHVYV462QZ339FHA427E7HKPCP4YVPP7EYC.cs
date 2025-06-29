// Decompiled with JetBrains decompiler
// Type: -.dje_zR8DJERVYW3NHVYV462QZ339FHA427E7HKPCP4YVPP7EYCT45YZDYE_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zR8DJERVYW3NHVYV462QZ339FHA427E7HKPCP4YVPP7EYCT45YZDYE_ejd : 
  MarkupExtension
{
  
  private static readonly IValueConverter \u0023\u003DzMeh\u0024Scs\u003D = (IValueConverter) new dje_zR8DJERVYW3NHVYV462QZ339FHA427E7HKPCP4YVPP7EYCT45YZDYE_ejd.\u0023\u003Dza2zbQRRjMsrw();
  
  private static readonly RelativeSource \u0023\u003DzCtHv3OM\u003D = RelativeSource.Self;
  
  private static readonly object[] \u0023\u003Dz6VPkvJI\u003D = Array.Empty<object>();
  
  private string \u0023\u003DzcKkMkrPBYL5xXPBm5Q\u003D\u003D;
  
  private BindingMode \u0023\u003DzQDtv5lyKsEt_C2MmGg\u003D\u003D;

  public dje_zR8DJERVYW3NHVYV462QZ339FHA427E7HKPCP4YVPP7EYCT45YZDYE_ejd()
  {
    this.\u0023\u003DzWsF8IZXUPckE(BindingMode.OneWay);
  }

  
  public string dje_zMW6EZHW5_ejd
  {
    get => this.\u0023\u003DzcKkMkrPBYL5xXPBm5Q\u003D\u003D;
    set => this.\u0023\u003DzcKkMkrPBYL5xXPBm5Q\u003D\u003D = value;
  }

  public string \u0023\u003DzM_1QeNM\u003D() => this.\u0023\u003DzcKkMkrPBYL5xXPBm5Q\u003D\u003D;

  public void \u0023\u003DzJG5C6JI\u003D(string _param1)
  {
    this.\u0023\u003DzcKkMkrPBYL5xXPBm5Q\u003D\u003D = _param1;
  }

  public BindingMode \u0023\u003DzTa7BrWoJT_E2()
  {
    return this.\u0023\u003DzQDtv5lyKsEt_C2MmGg\u003D\u003D;
  }

  public void \u0023\u003DzWsF8IZXUPckE(BindingMode _param1)
  {
    this.\u0023\u003DzQDtv5lyKsEt_C2MmGg\u003D\u003D = _param1;
  }

  public override object ProvideValue(IServiceProvider _param1)
  {
    if (!(_param1.GetService(typeof (IProvideValueTarget)) is IProvideValueTarget service) || service.TargetObject.GetType().Name == XXX.SSS(-539441826))
      return (object) this;
    if (service.TargetObject is Setter)
      return (object) new Binding()
      {
        Path = new PropertyPath((object) ThemeManager.\u0023\u003DzOJJsEsQ\u003D),
        ConverterParameter = (object) typeof (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D).GetProperty(this.\u0023\u003DzM_1QeNM\u003D()).GetGetMethod(),
        Converter = dje_zR8DJERVYW3NHVYV462QZ339FHA427E7HKPCP4YVPP7EYCT45YZDYE_ejd.\u0023\u003DzMeh\u0024Scs\u003D,
        Mode = BindingMode.OneWay,
        RelativeSource = dje_zR8DJERVYW3NHVYV462QZ339FHA427E7HKPCP4YVPP7EYCT45YZDYE_ejd.\u0023\u003DzCtHv3OM\u003D
      };
    if (!(service.TargetObject is DependencyObject targetObject))
      throw new Exception(XXX.SSS(-539441873));
    return this.\u0023\u003DzA13mM7FCopU4(targetObject);
  }

  private object \u0023\u003DzA13mM7FCopU4(DependencyObject _param1)
  {
    string theme = ThemeManager.GetTheme(_param1);
    return typeof (\u0023\u003DzjIfS4CbXGFDPWmVOPAZGmt9VmM5IkN_VTVwybkk\u003D).GetProperty(this.\u0023\u003DzM_1QeNM\u003D()).GetValue((object) ThemeManager.\u0023\u003DzrILtKW7bADnV(theme), dje_zR8DJERVYW3NHVYV462QZ339FHA427E7HKPCP4YVPP7EYCT45YZDYE_ejd.\u0023\u003Dz6VPkvJI\u003D);
  }

  internal sealed class \u0023\u003Dza2zbQRRjMsrw : IValueConverter
  {
    public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
    {
      return (_param3 as MethodInfo).Invoke((object) ThemeManager.\u0023\u003DzrILtKW7bADnV(_param1 as string), dje_zR8DJERVYW3NHVYV462QZ339FHA427E7HKPCP4YVPP7EYCT45YZDYE_ejd.\u0023\u003Dz6VPkvJI\u003D);
    }

    public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
    {
      throw new NotImplementedException();
    }
  }
}
