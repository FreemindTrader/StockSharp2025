// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.ItemsSourceBase
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Ecng.ComponentModel
{
  public class ItemsSourceBase : ItemsSourceBase<object>
  {
    private static IItemsSource Create(
      IEnumerable values,
      Type itemValueType,
      bool? excludeObsolete,
      ListSortDirection? sortOrder,
      Func<IItemsSourceItem, bool> filter,
      Func<object, string> getName,
      Func<object, string> getDescription)
    {
      if ((object) itemValueType == null)
        itemValueType = ItemsSourceBase.GetSourceValueType(values);
      Type type = typeof (ItemsSourceBase<>).Make(itemValueType);
      excludeObsolete.GetValueOrDefault();
      if (!excludeObsolete.HasValue)
        excludeObsolete = new bool?(true);
      object[] args = new object[6]
      {
        (object) values,
        (object) excludeObsolete.Value,
        (object) sortOrder,
        (object) filter,
        (object) getName,
        (object) getDescription
      };
      return (IItemsSource) Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, args, (CultureInfo) null, (object[]) null);
    }

    public static IItemsSource Create(
      object val,
      Type itemValueType,
      bool? excludeObsolete = null,
      ListSortDirection? sortOrder = null,
      Func<IItemsSourceItem, bool> filter = null,
      Func<object, string> getName = null,
      Func<object, string> getDescription = null)
    {
      if (val != null)
      {
        IItemsSource itemsSource = val as IItemsSource;
        if (itemsSource == null)
        {
          IEnumerable values = val as IEnumerable;
          if (values != null)
            return ItemsSourceBase.Create(values, itemValueType, excludeObsolete, sortOrder, filter, getName, getDescription);
          throw new ArgumentException("cannot create " + typeof (IItemsSource).FullName + " from '" + val.GetType().FullName + "'");
        }
        if ((object) itemValueType == null || itemsSource.ValueType == itemValueType)
        {
          if (excludeObsolete.HasValue)
          {
            bool? nullable = excludeObsolete;
            bool excludeObsolete1 = itemsSource.ExcludeObsolete;
            if (!(nullable.GetValueOrDefault() == excludeObsolete1 & nullable.HasValue))
              goto label_13;
          }
          if (sortOrder.HasValue)
          {
            ListSortDirection? nullable = sortOrder;
            ListSortDirection? sortOrder1 = itemsSource.SortOrder;
            if (!(nullable.GetValueOrDefault() == sortOrder1.GetValueOrDefault() & nullable.HasValue == sortOrder1.HasValue))
              goto label_13;
          }
          if (filter == null)
            return itemsSource;
        }
label_13:
        return ItemsSourceBase.Create((IEnumerable) itemsSource.Values, itemValueType, excludeObsolete, sortOrder, filter, getName, getDescription);
      }
      if ((object) itemValueType == null)
        itemValueType = typeof (object);
      return ItemsSourceBase.Create((IEnumerable) itemValueType.CreateArray(0), itemValueType, excludeObsolete, sortOrder, filter, getName, getDescription);
    }

    private static Type GetSourceValueType(IEnumerable values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      Type paramType1 = ItemsSourceBase.GetParamType(values.GetType(), typeof (IEnumerable<>));
      Type paramType2 = ItemsSourceBase.GetParamType(paramType1, typeof (IItemsSourceItem<>));
      if (paramType2 != (Type) null && paramType2 != typeof (object))
        return paramType2;
      if (paramType1 != (Type) null && !paramType1.Is<IItemsSourceItem>() && paramType1 != typeof (object))
        return paramType1;
      bool foundValues;
      bool foundItems = foundValues = false;
      Type[] array = values.Cast<object>().Select<object, Type>((Func<object, Type>) (o =>
      {
        Type type = o.GetType();
        Type paramType3 = ItemsSourceBase.GetParamType(type, typeof (IItemsSourceItem<>));
        if (paramType3 != (Type) null)
        {
          foundItems = true;
          return paramType3;
        }
        IItemsSourceItem itemsSourceItem = o as IItemsSourceItem;
        if (itemsSourceItem != null)
        {
          foundItems = true;
          return itemsSourceItem.Value.GetType();
        }
        foundValues = true;
        return type;
      })).ToArray<Type>();
      if (foundItems & foundValues)
        throw new ArgumentException("values contains elements of incompatible types");
      return ItemsSourceBase.GetCommonType(array);
    }

    private static Type GetParamType(Type type, Type genericInterfaceType)
    {
      if ((object) type == null)
        return (Type) null;
      return ((IEnumerable<Type>) new Type[1]
      {
        type
      }.Concat<Type>(type.GetInterfaces())).Where<Type>((Func<Type, bool>) (i =>
      {
        if (i.IsGenericType)
          return i.GetGenericTypeDefinition() == genericInterfaceType;
        return false;
      })).Select<Type, Type>((Func<Type, Type>) (i => i.GetGenericArguments()[0])).FirstOrDefault<Type>();
    }

    private static Type GetCommonType(Type[] types)
    {
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      if (types.Length == 0)
        return typeof (object);
      Type c = types[0];
      for (int index = 1; index < types.Length; ++index)
      {
        if (types[index].IsAssignableFrom(c))
        {
          c = types[index];
        }
        else
        {
          while (!c.IsAssignableFrom(types[index]))
            c = c.BaseType;
        }
      }
      return c;
    }
  }
}
