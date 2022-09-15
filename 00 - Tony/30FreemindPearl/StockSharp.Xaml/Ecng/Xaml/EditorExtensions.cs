// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.EditorExtensions
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using StockSharp.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml
{
  /// <summary>
  /// </summary>
  public static class EditorExtensions
  {
    /// <summary>
    /// </summary>
    public static void AddClearButton(this ButtonEdit edit, object emptyValue = null)
    {
      EditorExtensions.LamdaFunc01 zdm1mXmqNkmKhTuqEg = new EditorExtensions.LamdaFunc01();
      zdm1mXmqNkmKhTuqEg.\u0023\u003DzE21Enyc\u003D = edit;
      zdm1mXmqNkmKhTuqEg.\u0023\u003DzJiQJMMs\u003D = emptyValue;
      if (zdm1mXmqNkmKhTuqEg.\u0023\u003DzE21Enyc\u003D == null)
        throw new ArgumentNullException(nameof(2127280911));
      ButtonInfo buttonInfo1 = new ButtonInfo();
      buttonInfo1.set_GlyphKind((GlyphKind) 9);
      buttonInfo1.set_Content((object) LocalizedStrings.XamlStr440);
      ButtonInfo buttonInfo2 = buttonInfo1;
      ((DependencyObject) buttonInfo2).SetBindings(ContentElement.IsEnabledProperty, (object) zdm1mXmqNkmKhTuqEg.\u0023\u003DzE21Enyc\u003D, nameof(2127281146), BindingMode.TwoWay, (IValueConverter) new InverseBooleanConverter(), (object) null);
      ((CommandButtonInfo) buttonInfo2).add_Click(new RoutedEventHandler(zdm1mXmqNkmKhTuqEg.\u0023\u003DzZKvXpF4Q2\u0024bV6iQdBQ\u003D\u003D));
      zdm1mXmqNkmKhTuqEg.\u0023\u003DzE21Enyc\u003D.get_Buttons().Add(buttonInfo2);
    }

    /// <summary>
    /// </summary>
    public static void RemoveClearButton(this ButtonEdit edit)
    {
      if (edit == null)
        throw new ArgumentNullException(nameof(2127281131));
      ButtonInfoBase buttonInfoBase = ((IEnumerable<ButtonInfoBase>) edit.get_Buttons()).FirstOrDefault<ButtonInfoBase>(EditorExtensions.SomeShit.ShitMethod01 ?? (EditorExtensions.SomeShit.ShitMethod01 = new Func<ButtonInfoBase, bool>(EditorExtensions.SomeShit.ShitMethod02.\u0023\u003Dz4srzTvpBEgm0a24lP5zz6QM\u003D)));
      if (buttonInfoBase == null)
        return;
      ((Collection<ButtonInfoBase>) edit.get_Buttons()).Remove(buttonInfoBase);
    }

    /// <summary>
    /// </summary>
    public static void AddClearButton(this ComboBoxEditSettings editSettings, object emptyValue = null)
    {
      EditorExtensions.\u0023\u003DzWiCnw9A2mb6WXQNQ6w\u003D\u003D cnw9A2mb6WxqnQ6w = new EditorExtensions.\u0023\u003DzWiCnw9A2mb6WXQNQ6w\u003D\u003D();
      cnw9A2mb6WxqnQ6w.\u0023\u003DzJiQJMMs\u003D = emptyValue;
      if (editSettings == null)
        throw new ArgumentNullException(nameof(2127281126));
      ButtonInfo buttonInfo1 = new ButtonInfo();
      buttonInfo1.set_GlyphKind((GlyphKind) 9);
      buttonInfo1.set_Content((object) LocalizedStrings.XamlStr440);
      ButtonInfo buttonInfo2 = buttonInfo1;
      ((CommandButtonInfo) buttonInfo2).add_Click(new RoutedEventHandler(cnw9A2mb6WxqnQ6w.\u0023\u003DzZKvXpF4Q2\u0024bV6iQdBQ\u003D\u003D));
      ((ButtonEditSettings) editSettings).get_Buttons().Add(buttonInfo2);
    }

    /// <summary>
    /// </summary>
    public static IItemsSource ToItemsSource(
      this object val,
      Type itemValueType,
      bool? excludeObsolete = null,
      ListSortDirection? sortOrder = null,
      Func<IItemsSourceItem, bool> filter = null,
      Func<object, string> getName = null,
      Func<object, string> getDescription = null)
    {
      return ItemsSourceBase.Create(val, itemValueType, excludeObsolete, sortOrder, filter, getName, getDescription);
    }

    /// <summary>
    /// </summary>
    public static IItemsSource ToItemsSource<T>(
      this IEnumerable<T> val,
      bool excludeObsolete = true,
      ListSortDirection? sortOrder = null,
      Func<IItemsSourceItem, bool> filter = null,
      Func<T, string> getName = null,
      Func<T, string> getDescription = null)
    {
      return (IItemsSource) new ItemsSourceBase<T>((IEnumerable) val, excludeObsolete, sortOrder, filter, getName, getDescription);
    }

    /// <summary>
    /// </summary>
    public static void SetItemsSource<T>(this ComboBoxEditEx cb) where T : Enum
    {
      cb.SetItemsSource(typeof (T));
    }

    /// <summary>
    /// </summary>
    public static void SetItemsSource(this ComboBoxEditEx cb, Type enumType)
    {
      if (cb == null)
        throw new ArgumentNullException(nameof(2127281105));
      if (!enumType.IsEnum)
        throw new ArgumentException(enumType.FullName + nameof(2127281100));
      cb.SetItemsSource(enumType.GetValues().ToItemsSource(enumType, new bool?(), new ListSortDirection?(), (Func<IItemsSourceItem, bool>) null, (Func<object, string>) null, (Func<object, string>) null));
    }

    /// <summary>
    /// </summary>
    public static void SetItemsSource<T>(
      this ComboBoxEditEx cb,
      IEnumerable<T> values,
      Func<T, string> getName = null,
      Func<T, string> getDescription = null)
    {
      if (cb == null)
        throw new ArgumentNullException(nameof(2127281075));
      ComboBoxEditEx cb1 = cb;
      IEnumerable<T> val = values;
      Func<T, string> func1 = getName;
      Func<T, string> func2 = getDescription;
      ListSortDirection? sortOrder = new ListSortDirection?();
      Func<T, string> getName1 = func1;
      Func<T, string> getDescription1 = func2;
      IItemsSource itemsSource = val.ToItemsSource<T>(true, sortOrder, (Func<IItemsSourceItem, bool>) null, getName1, getDescription1);
      cb1.SetItemsSource(itemsSource);
    }

    /// <summary>
    /// </summary>
    public static void SetItemsSource(this ComboBoxEditEx cb, IItemsSource source)
    {
      if (cb == null)
        throw new ArgumentNullException(nameof(2127281070));
      ((LookUpEditBase) cb).set_ItemsSource((object) source);
    }

    /// <summary>
    /// </summary>
    public static T GetSelected<T>(this ComboBoxEditEx cb)
    {
      return (T) cb.Value;
    }

    /// <summary>
    /// </summary>
    public static IEnumerable<T> GetSelecteds<T>(this ComboBoxEditEx cb)
    {
      return cb.GetSelected<IEnumerable<T>>();
    }

    /// <summary>
    /// </summary>
    public static void SetSelected<T>(this ComboBoxEditEx cb, T value)
    {
      cb.Value = (object) value;
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly EditorExtensions.SomeShit ShitMethod02 = new EditorExtensions.SomeShit();
      public static Func<ButtonInfoBase, bool> ShitMethod01;

      internal bool \u0023\u003Dz4srzTvpBEgm0a24lP5zz6QM\u003D(ButtonInfoBase _param1)
      {
        return ((ButtonInfo) _param1).get_GlyphKind() == 9;
      }
    }

    private sealed class \u0023\u003DzWiCnw9A2mb6WXQNQ6w\u003D\u003D
    {
      public object \u0023\u003DzJiQJMMs\u003D;

      internal void \u0023\u003DzZKvXpF4Q2\u0024bV6iQdBQ\u003D\u003D(
        object _param1,
        RoutedEventArgs _param2)
      {
        BaseEdit ownerEdit = BaseEdit.GetOwnerEdit((DependencyObject) _param1);
        if (ownerEdit == null || ownerEdit.get_IsReadOnly())
          return;
        ((DependencyObject) ownerEdit).SetCurrentValue((DependencyProperty) BaseEdit.EditValueProperty, this.\u0023\u003DzJiQJMMs\u003D);
      }
    }

    private sealed class LamdaFunc01
    {
      public ButtonEdit \u0023\u003DzE21Enyc\u003D;
      public object \u0023\u003DzJiQJMMs\u003D;

      internal void \u0023\u003DzZKvXpF4Q2\u0024bV6iQdBQ\u003D\u003D(
        object _param1,
        RoutedEventArgs _param2)
      {
        if (((BaseEdit) this.\u0023\u003DzE21Enyc\u003D).get_IsReadOnly())
          return;
        ((DependencyObject) this.\u0023\u003DzE21Enyc\u003D).SetCurrentValue((DependencyProperty) BaseEdit.EditValueProperty, this.\u0023\u003DzJiQJMMs\u003D);
      }
    }
  }
}
