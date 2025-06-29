// Decompiled with JetBrains decompiler
// Type: XamlGeneratedNamespace.GeneratedInternalTypeHelper
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows.Markup;

#nullable disable
namespace XamlGeneratedNamespace;

[DebuggerNonUserCode]
[GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class GeneratedInternalTypeHelper : InternalTypeHelper
{
  protected virtual object CreateInstance(Type type, CultureInfo culture)
  {
    return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, (object[]) null, culture);
  }

  protected virtual object GetPropertyValue(
    PropertyInfo propertyInfo,
    object target,
    CultureInfo culture)
  {
    return propertyInfo.GetValue(target, BindingFlags.Default, (Binder) null, (object[]) null, culture);
  }

  protected virtual void SetPropertyValue(
    PropertyInfo propertyInfo,
    object target,
    object value,
    CultureInfo culture)
  {
    propertyInfo.SetValue(target, value, BindingFlags.Default, (Binder) null, (object[]) null, culture);
  }

  protected virtual Delegate CreateDelegate(Type delegateType, object target, string handler)
  {
    return (Delegate) target.GetType().InvokeMember(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539351245), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, target, new object[2]
    {
      (object) delegateType,
      (object) handler
    }, (CultureInfo) null);
  }

  protected virtual void AddEventHandler(EventInfo eventInfo, object target, Delegate handler)
  {
    eventInfo.AddEventHandler(target, handler);
  }
}
