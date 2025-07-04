// Decompiled with JetBrains decompiler
// Type: #=z3arZou$KE51WuqbncgcGPqIAlVoKLppDhkPhwMk=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
internal static class \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPqIAlVoKLppDhkPhwMk\u003D
{
  internal static Binding \u0023\u003DzskQgoNbhmIMD(this Binding _param0)
  {
    Binding binding1 = new Binding();
    binding1.Path = _param0.Path;
    binding1.Mode = _param0.Mode;
    binding1.Converter = _param0.Converter;
    binding1.ConverterCulture = _param0.ConverterCulture;
    binding1.ConverterParameter = _param0.ConverterParameter;
    binding1.FallbackValue = _param0.FallbackValue;
    binding1.TargetNullValue = _param0.TargetNullValue;
    binding1.NotifyOnValidationError = _param0.NotifyOnValidationError;
    binding1.UpdateSourceTrigger = _param0.UpdateSourceTrigger;
    binding1.ValidatesOnDataErrors = _param0.ValidatesOnDataErrors;
    binding1.ValidatesOnExceptions = _param0.ValidatesOnExceptions;
    binding1.BindsDirectlyToSource = _param0.BindsDirectlyToSource;
    binding1.StringFormat = _param0.StringFormat;
    binding1.AsyncState = _param0.AsyncState;
    binding1.IsAsync = _param0.IsAsync;
    binding1.NotifyOnSourceUpdated = _param0.NotifyOnSourceUpdated;
    binding1.NotifyOnTargetUpdated = _param0.NotifyOnTargetUpdated;
    binding1.BindingGroupName = _param0.BindingGroupName;
    binding1.UpdateSourceExceptionFilter = _param0.UpdateSourceExceptionFilter;
    binding1.XPath = _param0.XPath;
    Binding binding2 = binding1;
    if (_param0.Source != null)
      binding2.Source = _param0.Source;
    if (_param0.RelativeSource != null)
      binding2.RelativeSource = _param0.RelativeSource;
    if (_param0.ElementName != null)
      binding2.ElementName = _param0.ElementName;
    foreach (ValidationRule validationRule in _param0.ValidationRules)
      binding2.ValidationRules.Add(validationRule);
    return binding2;
  }
}
