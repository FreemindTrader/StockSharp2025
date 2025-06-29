// Decompiled with JetBrains decompiler
// Type: #=z2Qv8a0HfC9ieiy4m0TM2aAzmsNDy8Fpd$of18IRv__L_
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

#nullable disable
internal sealed class \u0023\u003Dz2Qv8a0HfC9ieiy4m0TM2aAzmsNDy8Fpd\u0024of18IRv__L_ : 
  IValueConverter
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly LineAnnotationWithLabelsBase \u0023\u003Dzoem3lxBSJacx;

  public \u0023\u003Dz2Qv8a0HfC9ieiy4m0TM2aAzmsNDy8Fpd\u0024of18IRv__L_(
    LineAnnotationWithLabelsBase _param1)
  {
    this.\u0023\u003Dzoem3lxBSJacx = _param1;
  }

  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    IComparable comparable = _param1 as IComparable;
    string str = _param1 == null ? string.Empty : _param1.ToString();
    if (this.\u0023\u003Dzoem3lxBSJacx != null && comparable != null)
    {
      \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB usedAxis = this.\u0023\u003Dzoem3lxBSJacx.GetUsedAxis();
      BindingExpression bindingExpression = this.\u0023\u003Dzoem3lxBSJacx.GetBindingExpression(LineAnnotationWithLabelsBase.LabelTextFormattingProperty);
      bool flag = bindingExpression != null;
      if (flag)
        flag = bindingExpression.ParentBinding.Path.Path == \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430181);
      str = !flag || usedAxis == null ? string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430209) + this.\u0023\u003Dzoem3lxBSJacx.LabelTextFormatting + \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539430223), (object) comparable) : usedAxis.\u0023\u003DzRQVMnjXxoCTF(comparable, false);
    }
    return (object) str;
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
