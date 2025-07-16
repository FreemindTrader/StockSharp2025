// Decompiled with JetBrains decompiler
// Type: #=zS5mFHV$eXnkCjzbt0Dx26rQihHYkQVY4W4XDIDHw9OYR
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows;
using System.Windows.Media;

#nullable disable
internal static class \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26rQihHYkQVY4W4XDIDHw9OYR
{
  public static T \u0023\u003DzD_LPZWnSn3m1<T>(this DependencyObject _param0) where T : DependencyObject
  {
    int childrenCount = VisualTreeHelper.GetChildrenCount(_param0);
    for (int childIndex = 0; childIndex < childrenCount; ++childIndex)
    {
      DependencyObject child = VisualTreeHelper.GetChild(_param0, childIndex);
      if (child != null && typeof (T).IsAssignableFrom(child.GetType()))
        return (T) child;
      DependencyObject dependencyObject = (DependencyObject) child.\u0023\u003DzD_LPZWnSn3m1<T>();
      if (dependencyObject != null)
        return (T) dependencyObject;
    }
    return default (T);
  }
}
