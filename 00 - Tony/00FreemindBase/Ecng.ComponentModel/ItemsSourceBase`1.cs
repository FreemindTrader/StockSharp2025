// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.ItemsSourceBase`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Ecng.ComponentModel
{
  public class ItemsSourceBase<T> : IItemsSource<T>, IItemsSource
  {
    private readonly T[] _values;
    private readonly Lazy<IEnumerable<IItemsSourceItem<T>>> _items;
    private readonly Func<IItemsSourceItem, bool> _filter;
    private readonly Func<T, string> _getName;
    private readonly Func<T, string> _getDescription;

    public bool ExcludeObsolete { get; }

    public ListSortDirection? SortOrder { get; }

    IEnumerable<IItemsSourceItem> IItemsSource.Values
    {
      get
      {
        return (IEnumerable<IItemsSourceItem>) this.Values;
      }
    }

    public IEnumerable<IItemsSourceItem<T>> Values
    {
      get
      {
        return this._items.Value;
      }
    }

    public virtual Type ValueType
    {
      get
      {
        return typeof (T);
      }
    }

    public ItemsSourceBase(
      IEnumerable values,
      bool excludeObsolete,
      ListSortDirection? sortOrder,
      Func<IItemsSourceItem, bool> filter,
      Func<T, string> getName,
      Func<T, string> getDescription)
    {
      this.SortOrder = sortOrder;
      this.ExcludeObsolete = excludeObsolete;
      this._filter = filter;
      this._getName = getName;
      this._getDescription = getDescription;
      object[] source = values != null ? values.Cast<object>().ToArray<object>() : (object[]) null;
      if (source != null)
      {
        if (((IEnumerable<object>) source).All<object>((Func<object, bool>) (o => o is T)))
        {
          this._values = Enumerable.Cast<T>(source).ToArray<T>();
          this._items = new Lazy<IEnumerable<IItemsSourceItem<T>>>((Func<IEnumerable<IItemsSourceItem<T>>>) (() => this.CreateItems(this.GetValues())));
        }
        else if (((IEnumerable<object>) source).All<object>((Func<object, bool>) (o => o is IItemsSourceItem<T>)))
        {
          IItemsSourceItem<T>[] itemsArr = Enumerable.Cast<IItemsSourceItem<T>>(source).ToArray<IItemsSourceItem<T>>();
          this._values = ((IEnumerable<IItemsSourceItem<T>>) itemsArr).Select<IItemsSourceItem<T>, T>((Func<IItemsSourceItem<T>, T>) (item => item.Value)).ToArray<T>();
          this._items = new Lazy<IEnumerable<IItemsSourceItem<T>>>((Func<IEnumerable<IItemsSourceItem<T>>>) (() => this.FilterItems((IEnumerable<IItemsSourceItem<T>>) itemsArr)));
        }
        else if (((IEnumerable<object>) source).All<object>((Func<object, bool>) (o =>
        {
          IItemsSourceItem itemsSourceItem = o as IItemsSourceItem;
          if (itemsSourceItem != null)
            return itemsSourceItem.Value is T;
          return false;
        })))
        {
          IItemsSourceItem<T>[] itemsArr = Enumerable.Cast<IItemsSourceItem>(source).Select<IItemsSourceItem, IItemsSourceItem<T>>(new Func<IItemsSourceItem, IItemsSourceItem<T>>(this.CreateNewItem)).ToArray<IItemsSourceItem<T>>();
          this._values = ((IEnumerable<IItemsSourceItem<T>>) itemsArr).Select<IItemsSourceItem<T>, T>((Func<IItemsSourceItem<T>, T>) (item => item.Value)).ToArray<T>();
          this._items = new Lazy<IEnumerable<IItemsSourceItem<T>>>((Func<IEnumerable<IItemsSourceItem<T>>>) (() => this.FilterItems((IEnumerable<IItemsSourceItem<T>>) itemsArr)));
        }
        else
          throw new ArgumentException("values is expected to contain either " + typeof (T).Name + " or IItemsSourceItem<" + typeof (T).Name + "> items (mix not supported). actual types found: " + ((IEnumerable<object>) source).Select<object, string>((Func<object, string>) (o => o.GetType().Name)).Distinct<string>().Join(","));
      }
      else
      {
        if (typeof (T).IsEnum)
          this._values = Ecng.Common.Enumerator.GetValues<T>().ToArray<T>();
        this._items = new Lazy<IEnumerable<IItemsSourceItem<T>>>((Func<IEnumerable<IItemsSourceItem<T>>>) (() => this.CreateItems(this.GetValues())));
      }
    }

    public ItemsSourceBase(
      bool excludeObsolete,
      ListSortDirection? sortOrder = null,
      Func<IItemsSourceItem, bool> filter = null,
      Func<T, string> getName = null,
      Func<T, string> getDescription = null)
      : this((IEnumerable) null, excludeObsolete, sortOrder, filter, getName, getDescription)
    {
    }

    public ItemsSourceBase(
      IEnumerable values,
      Func<T, string> getName = null,
      Func<T, string> getDescription = null)
      : this(values, true, new ListSortDirection?(), (Func<IItemsSourceItem, bool>) null, getName, getDescription)
    {
    }

    public ItemsSourceBase()
      : this((IEnumerable) null, (Func<T, string>) null, (Func<T, string>) null)
    {
    }

    protected virtual string Format
    {
      get
      {
        return (string) null;
      }
    }

    protected virtual string GetName(T value)
    {
      if (this._getName != null)
        return this._getName(value);
      string format = this.Format;
      if (!format.IsEmptyOrWhiteSpace())
        return string.Format("{0:" + format + "}", (object) value);
      return ((object) value).GetDisplayName();
    }

    protected virtual string GetDescription(T value)
    {
      if (this._getDescription != null)
        return this._getDescription(value);
      if (!typeof (T).IsEnum)
        return (string) null;
      return value.GetFieldDescription<T>();
    }

    protected virtual Uri GetIcon(T value)
    {
      if (!typeof (T).IsEnum)
        return (Uri) null;
      return value.GetFieldIcon<T>();
    }

    protected virtual bool GetIsObsolete(T value)
    {
      if (typeof (T).IsEnum)
        return ((object) value).GetAttributeOfType<ObsoleteAttribute>() != null;
      return false;
    }

    protected virtual bool Filter(IItemsSourceItem<T> item)
    {
      if (this.ExcludeObsolete && item.IsObsolete)
        return false;
      Func<IItemsSourceItem, bool> filter = this._filter;
      if (filter == null)
        return true;
      return filter((IItemsSourceItem) item);
    }

    IItemsSourceItem IItemsSource.CreateNewItem(object value)
    {
      if (value is T)
        return (IItemsSourceItem) this.CreateNewItem((T) value);
      throw new ArgumentException(nameof (value));
    }

    private IItemsSourceItem<T> CreateNewItem(IItemsSourceItem fromItem)
    {
      return (IItemsSourceItem<T>) new ItemsSourceItem<T>((T) fromItem.Value, fromItem.DisplayName, fromItem.Description, fromItem.Icon, fromItem.IsObsolete);
    }

    public virtual IItemsSourceItem<T> CreateNewItem(T value)
    {
      return (IItemsSourceItem<T>) new ItemsSourceItem<T>(value, this.GetName(value), this.GetDescription(value), this.GetIcon(value), this.GetIsObsolete(value));
    }

    protected virtual IEnumerable<T> GetValues()
    {
      return (IEnumerable<T>) this._values;
    }

    private IEnumerable<IItemsSourceItem<T>> FilterItems(
      IEnumerable<IItemsSourceItem<T>> items)
    {
      if (items == null)
        items = Enumerable.Empty<IItemsSourceItem<T>>();
      items = items.Where<IItemsSourceItem<T>>(new Func<IItemsSourceItem<T>, bool>(this.Filter));
      ListSortDirection? sortOrder = this.SortOrder;
      if (sortOrder.HasValue)
      {
        sortOrder = this.SortOrder;
        ListSortDirection listSortDirection = ListSortDirection.Ascending;
        items = sortOrder.GetValueOrDefault() == listSortDirection & sortOrder.HasValue ? (IEnumerable<IItemsSourceItem<T>>) items.OrderBy<IItemsSourceItem<T>, string>((Func<IItemsSourceItem<T>, string>) (item => item.DisplayName), (IComparer<string>) StringComparer.CurrentCultureIgnoreCase) : (IEnumerable<IItemsSourceItem<T>>) items.OrderByDescending<IItemsSourceItem<T>, string>((Func<IItemsSourceItem<T>, string>) (item => item.DisplayName), (IComparer<string>) StringComparer.CurrentCultureIgnoreCase);
      }
      return (IEnumerable<IItemsSourceItem<T>>) items.ToArray<IItemsSourceItem<T>>();
    }

    private IEnumerable<IItemsSourceItem<T>> CreateItems(
      IEnumerable<T> values)
    {
      return this.FilterItems(values != null ? values.Select<T, IItemsSourceItem<T>>(new Func<T, IItemsSourceItem<T>>(this.CreateNewItem)) : (IEnumerable<IItemsSourceItem<T>>) null);
    }
  }
}
