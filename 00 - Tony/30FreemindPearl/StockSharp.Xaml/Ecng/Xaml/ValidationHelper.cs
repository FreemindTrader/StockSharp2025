// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.ValidationHelper
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Validation;
using Ecng.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  public static class ValidationHelper
  {
    /// <summary>
    /// </summary>
    public static readonly DependencyProperty ValidationRulesProperty = DependencyProperty.RegisterAttached(nameof(2127278609), typeof (ValidationRulesCollection), typeof (ValidationHelper), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    /// </summary>
    public static readonly DependencyProperty BaseEditProperty = DependencyProperty.RegisterAttached(nameof(2127278599), typeof (BaseEdit), typeof (ValidationHelper), new PropertyMetadata((object) null, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzuIdYLUGjyNwx))));
    
    private static readonly Dictionary<BaseEditSettings, BaseEdit> \u0023\u003DzFki1ktc\u003D = new Dictionary<BaseEditSettings, BaseEdit>();

    /// <summary>
    /// </summary>
    public static ValidationRulesCollection GetValidationRules(
      DependencyObject obj)
    {
      return (ValidationRulesCollection) obj.GetValue(ValidationHelper.ValidationRulesProperty);
    }

    /// <summary>
    /// </summary>
    public static void SetValidationRules(DependencyObject obj, ValidationRulesCollection value)
    {
      obj.SetValue(ValidationHelper.ValidationRulesProperty, (object) value);
    }

    /// <summary>
    /// </summary>
    public static BaseEdit GetBaseEdit(BaseEditSettings settings)
    {
      return (BaseEdit) ((DependencyObject) settings).GetValue(ValidationHelper.BaseEditProperty);
    }

    /// <summary>
    /// </summary>
    public static void SetBaseEdit(BaseEditSettings settings, BaseEdit edit)
    {
      ((DependencyObject) settings).SetValue(ValidationHelper.BaseEditProperty, (object) edit);
    }

    private static void \u0023\u003DzuIdYLUGjyNwx(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
      BaseEditSettings key = _param0 as BaseEditSettings;
      if (key == null)
        return;
      // ISSUE: method pointer
      ((IDictionary<BaseEditSettings, BaseEdit>) ValidationHelper.\u0023\u003DzFki1ktc\u003D).TryGetValue<BaseEditSettings, BaseEdit>(key)?.remove_Validate(new ValidateEventHandler((object) null, __methodptr(\u0023\u003Dz1UC7LBc\u003D)));
      if (((DependencyPropertyChangedEventArgs) ref _param1).get_NewValue() == null)
        return;
      BaseEdit newValue = (BaseEdit) ((DependencyPropertyChangedEventArgs) ref _param1).get_NewValue();
      ValidationHelper.\u0023\u003DzFki1ktc\u003D[key] = newValue;
      // ISSUE: method pointer
      newValue.add_Validate(new ValidateEventHandler((object) null, __methodptr(\u0023\u003Dz1UC7LBc\u003D)));
    }

    private static void \u0023\u003Dz1UC7LBc\u003D(object _param0, ValidationEventArgs _param1)
    {
      foreach (ValidationRule validationRule in (List<ValidationRule>) ValidationHelper.GetValidationRules((DependencyObject) ((IEnumerable<KeyValuePair<BaseEditSettings, BaseEdit>>) ValidationHelper.\u0023\u003DzFki1ktc\u003D).FirstOrDefault<KeyValuePair<BaseEditSettings, BaseEdit>>(new Func<KeyValuePair<BaseEditSettings, BaseEdit>, bool>(new ValidationHelper.\u0023\u003DzgHony0yN7oYNf8HI\u0024w\u003D\u003D() { \u0023\u003DzE21Enyc\u003D = (BaseEdit) _param0 }.\u0023\u003Dz3P85fvDCIhaXK0q1Mg\u003D\u003D)).Key))
      {
        ValidationResult validationResult = validationRule.Validate(_param1.get_Value(), CultureInfo.CurrentCulture);
        if (!validationResult.IsValid)
        {
          _param1.set_IsValid(false);
          _param1.set_ErrorContent(validationResult.ErrorContent);
          break;
        }
      }
    }

    private sealed class \u0023\u003DzgHony0yN7oYNf8HI\u0024w\u003D\u003D
    {
      public BaseEdit \u0023\u003DzE21Enyc\u003D;

      internal bool \u0023\u003Dz3P85fvDCIhaXK0q1Mg\u003D\u003D(
        KeyValuePair<BaseEditSettings, BaseEdit> _param1)
      {
        return object.Equals((object) _param1.Value, (object) this.\u0023\u003DzE21Enyc\u003D);
      }
    }
  }
}
