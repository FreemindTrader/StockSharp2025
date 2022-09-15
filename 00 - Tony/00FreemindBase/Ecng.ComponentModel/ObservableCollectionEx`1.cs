// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.ObservableCollectionEx`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Collections;
using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Ecng.ComponentModel
{
  public class ObservableCollectionEx<TItem> : IListEx<TItem>, IList<TItem>, ICollection<TItem>, IEnumerable<TItem>, IEnumerable, ICollectionEx<TItem>, IList, ICollection, INotifyCollectionChanged, INotifyPropertyChanged
  {
    private readonly List<TItem> _items = new List<TItem>();
    private const string _countString = "Count";
    private const string _indexerName = "Item[]";
    private Action<IEnumerable<TItem>> _addedRange;
    private Action<IEnumerable<TItem>> _removedRange;

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public event PropertyChangedEventHandler PropertyChanged;

    public event Action<IEnumerable<TItem>> AddedRange
    {
      add
      {
        this._addedRange += value;
      }
      remove
      {
        this._addedRange -= value;
      }
    }

    public event Action<IEnumerable<TItem>> RemovedRange
    {
      add
      {
        this._removedRange += value;
      }
      remove
      {
        this._removedRange -= value;
      }
    }

    public virtual void AddRange(IEnumerable<TItem> items)
    {
      TItem[] array = items.ToArray<TItem>();
      if (array.Length == 0)
        return;
      int count = this._items.Count;
      this._items.AddRange((IEnumerable<TItem>) array);
      this.OnPropertyChanged("Count");
      this.OnPropertyChanged("Item[]");
      this.OnCollectionChanged(NotifyCollectionChangedAction.Add, (IList<TItem>) array, count);
    }

    public virtual void RemoveRange(IEnumerable<TItem> items)
    {
      items = (IEnumerable<TItem>) items.ToArray<TItem>();
      if (items.Count<TItem>() > 10000 || (double) items.Count<TItem>() > (double) this.Count * 0.1)
      {
        HashSet<TItem> source = new HashSet<TItem>((IEnumerable<TItem>) this._items);
        source.RemoveRange<TItem>(items);
        this.Clear();
        this.AddRange((IEnumerable<TItem>) source);
      }
      else
        items.ForEach<TItem>((Action<TItem>) (i => this.Remove(i)));
    }

    public virtual int RemoveRange(int index, int count)
    {
      TItem[] array = this._items.GetRange(index, count).ToArray();
      if (array.Length == 0)
        return 0;
      this._items.RemoveRange(index, count);
      this.OnRemove((IList<TItem>) array, index);
      return array.Length;
    }

    public IEnumerator<TItem> GetEnumerator()
    {
      return (IEnumerator<TItem>) this._items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public virtual void Add(TItem item)
    {
      this.AddRange((IEnumerable<TItem>) new TItem[1]
      {
        item
      });
    }

    public virtual bool Remove(TItem item)
    {
      int index = this._items.IndexOf(item);
      if (index == -1)
        return false;
      this._items.RemoveAt(index);
      this.OnRemove((IList<TItem>) new TItem[1]{ item }, index);
      return true;
    }

    int IList.Add(object value)
    {
      this.Add((TItem) value);
      return this.Count - 1;
    }

    bool IList.Contains(object value)
    {
      return this.Contains((TItem) value);
    }

    public virtual void Clear()
    {
      this._items.Clear();
      this.OnPropertyChanged("Count");
      this.OnPropertyChanged("Item[]");
      this.OnCollectionReset();
    }

    int IList.IndexOf(object value)
    {
      return this.IndexOf((TItem) value);
    }

    void IList.Insert(int index, object value)
    {
      this.Insert(index, (TItem) value);
    }

    void IList.Remove(object value)
    {
      this.Remove((TItem) value);
    }

    public bool Contains(TItem item)
    {
      return this._items.Contains(item);
    }

    public void CopyTo(TItem[] array, int arrayIndex)
    {
      this._items.CopyTo(array, arrayIndex);
    }

    void ICollection.CopyTo(Array array, int index)
    {
      this.CopyTo(array as TItem[] ?? array.Cast<TItem>().ToArray<TItem>(), index);
    }

    public int Count
    {
      get
      {
        return this._items.Count;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return true;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    public int IndexOf(TItem item)
    {
      return this._items.IndexOf(item);
    }

    public void Insert(int index, TItem item)
    {
      throw new NotSupportedException();
    }

    public void RemoveAt(int index)
    {
      throw new NotSupportedException();
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this[index];
      }
      set
      {
        this[index] = (TItem) value;
      }
    }

    public TItem this[int index]
    {
      get
      {
        return this._items[index];
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    private void OnRemove(IList<TItem> items, int index)
    {
      this.OnPropertyChanged("Count");
      this.OnPropertyChanged("Item[]");
      this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, items, index);
    }

    protected void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnCollectionChanged(
      NotifyCollectionChangedAction action,
      IList<TItem> items,
      int index)
    {
      if (items == null)
        throw new ArgumentNullException(nameof (items));
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      switch (action)
      {
        case NotifyCollectionChangedAction.Add:
          Action<IEnumerable<TItem>> addedRange = this._addedRange;
          if (addedRange != null)
          {
            addedRange((IEnumerable<TItem>) items);
            break;
          }
          break;
        case NotifyCollectionChangedAction.Remove:
          Action<IEnumerable<TItem>> removedRange = this._removedRange;
          if (removedRange != null)
          {
            removedRange((IEnumerable<TItem>) items);
            break;
          }
          break;
      }
      NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
      if (collectionChanged == null)
        return;
      this.ProcessCollectionChanged(Enumerable.Cast<NotifyCollectionChangedEventHandler>(collectionChanged.GetInvocationList()), action, items, index);
    }

    protected virtual void ProcessCollectionChanged(
      IEnumerable<NotifyCollectionChangedEventHandler> subscribers,
      NotifyCollectionChangedAction action,
      IList<TItem> items,
      int index)
    {
      NotifyCollectionChangedEventArgs e = new NotifyCollectionChangedEventArgs(action, (IList) items, index);
      foreach (NotifyCollectionChangedEventHandler subscriber in subscribers)
        subscriber((object) this, e);
    }

    private void OnCollectionReset()
    {
      NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
      if (collectionChanged == null)
        return;
      collectionChanged((object) this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
  }
}
