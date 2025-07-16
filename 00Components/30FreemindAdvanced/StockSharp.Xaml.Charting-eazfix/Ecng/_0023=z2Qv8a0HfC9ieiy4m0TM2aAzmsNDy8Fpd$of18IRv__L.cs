// Decompiled with JetBrains decompiler
// Type: #=z2Qv8a0HfC9ieiy4m0TM2aAzmsNDy8Fpd$of18IRv__L_
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

#nullable disable
public sealed class \u0023\u003Dz2Qv8a0HfC9ieiy4m0TM2aAzmsNDy8Fpd\u0024of18IRv__L_ : 
  IValueConverter
{
  
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
      IAxis usedAxis = this.\u0023\u003Dzoem3lxBSJacx.GetUsedAxis();
      BindingExpression bindingExpression = this.\u0023\u003Dzoem3lxBSJacx.GetBindingExpression(LineAnnotationWithLabelsBase.LabelTextFormattingProperty);
      bool flag = bindingExpression != null;
      if (flag)
        flag = bindingExpression.ParentBinding.Path.Path == "DefaultTextFormatting";
      str = !flag || usedAxis == null ? string.Format($"{{0:{this.\u0023\u003Dzoem3lxBSJacx.LabelTextFormatting}}}", (object) comparable) : usedAxis.\u0023\u003DzRQVMnjXxoCTF(comparable, false);
    }
    return (object) str;
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }
}
