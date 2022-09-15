// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.ConvertibleObservableCollection`2
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Ecng.ComponentModel
{
  public class ConvertibleObservableCollection<TItem, TDisplay> : BaseObservableCollection, IListEx<TItem>, IList<TItem>, ICollection<TItem>, IEnumerable<TItem>, IEnumerable, ICollectionEx<TItem>
    where TDisplay : class
  {
    private readonly OrderedDictionary _convertedValues = new OrderedDictionary();
    private readonly ICollection<TDisplay> _collection;
    private readonly Func<TItem, TDisplay> _converter;

    public ConvertibleObservableCollection(
      ICollection<TDisplay> collection,
      Func<TItem, TDisplay> converter)
    {
      ICollection<TDisplay> displays = collection;
      if (displays == null)
        throw new ArgumentNullException(nameof (collection));
      this._collection = displays;
      Func<TItem, TDisplay> func = converter;
      if (func == null)
        throw new ArgumentNullException(nameof (converter));
      this._converter = func;
    }

    private object SyncRoot
    {
      get
      {
        return ((ICollection) this._collection).SyncRoot;
      }
    }

    public TItem[] Items
    {
      get
      {
        lock (this.SyncRoot)
          return this._convertedValues.Keys.Cast<TItem>().ToArray<TItem>();
      }
    }

    public TDisplay TryGet(TItem item)
    {
      lock (this.SyncRoot)
        return this._convertedValues.Contains((object) item) ? (TDisplay) this._convertedValues[(object) item] : default (TDisplay);
    }

    public event Action<IEnumerable<TItem>> AddedRange;

    public event Action<IEnumerable<TItem>> RemovedRange;

    public void AddRange(IEnumerable<TItem> items)
    {
      TItem[] array = items.ToArray<TItem>();
      List<TItem> objList = new List<TItem>();
      lock (this.SyncRoot)
      {
        List<TDisplay> displayList = new List<TDisplay>();
        foreach (TItem obj in array)
        {
          TDisplay display = this._converter(obj);
          if (!this._convertedValues.Contains((object) obj))
          {
            this._convertedValues.Add((object) obj, (object) display);
            displayList.Add(display);
            objList.Add(obj);
          }
        }
        if (displayList.Count == 0)
          return;
        this._collection.AddRange<TDisplay>((IEnumerable<TDisplay>) displayList);
      }
      Action<IEnumerable<TItem>> addedRange = this.AddedRange;
      if (addedRange != null)
        addedRange((IEnumerable<TItem>) objList);
      this.CheckCount();
    }

    public void RemoveRange(IEnumerable<TItem> items)
    {
      TItem[] array = items.ToArray<TItem>();
      lock (this.SyncRoot)
      {
        List<TDisplay> displayList = new List<TDisplay>();
        foreach (TItem obj in array)
        {
          TDisplay display = this.TryGet(obj);
          if ((object) display != null)
          {
            this._convertedValues.Remove((object) obj);
            displayList.Add(display);
          }
        }
        this._collection.RemoveRange<TDisplay>((IEnumerable<TDisplay>) displayList);
      }
      Action<IEnumerable<TItem>> removedRange = this.RemovedRange;
      if (removedRange == null)
        return;
      removedRange((IEnumerable<TItem>) array);
    }

    public override int RemoveRange(int index, int count)
    {
      lock (this.SyncRoot)
      {
        TItem[] array = this._convertedValues.Keys.Cast<TItem>().Skip<TItem>(index).Take<TItem>(count).ToArray<TItem>();
        this.RemoveRange((IEnumerable<TItem>) array);
        return array.Length;
      }
    }

    public void Clear()
    {
      TItem[] array;
      lock (this.SyncRoot)
      {
        array = this._convertedValues.Keys.Cast<TItem>().ToArray<TItem>();
        this._convertedValues.Clear();
        this._collection.Clear();
      }
      Action<IEnumerable<TItem>> removedRange = this.RemovedRange;
      if (removedRange == null)
        return;
      removedRange((IEnumerable<TItem>) array);
    }

    public IEnumerator<TItem> GetEnumerator()
    {
      lock (this.SyncRoot)
        return this._convertedValues.Keys.Cast<TItem>().GetEnumerator();
    }

    public void Add(TItem item)
    {
      lock (this.SyncRoot)
      {
        TDisplay display = this._converter(item);
        this._convertedValues.Add((object) item, (object) display);
        this._collection.Add(display);
      }
      this.CheckCount();
    }

    public bool Remove(TItem item)
    {
      lock (this.SyncRoot)
      {
        TDisplay display = this.TryGet(item);
        if ((object) display == null)
          return false;
        this._convertedValues.Remove((object) item);
        this._collection.Remove(display);
        return true;
      }
    }

    public override int Count
    {
      get
      {
        lock (this.SyncRoot)
          return this._convertedValues.Count;
      }
    }

    bool ICollection<TItem>.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool Contains(TItem item)
    {
      lock (this.SyncRoot)
        return this._convertedValues.Contains((object) item);
    }

    public void CopyTo(TItem[] array, int arrayIndex)
    {
      lock (this.SyncRoot)
        this._convertedValues.Keys.CopyTo((Array) array, arrayIndex);
    }

    public int IndexOf(TItem item)
    {
      throw new NotSupportedException();
    }

    public void Insert(int index, TItem item)
    {
      throw new NotSupportedException();
    }

    public void RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    public TItem this[int index]
    {
      get
      {
        lock (this.SyncRoot)
          return (TItem) this._convertedValues.Cast<DictionaryEntry>().ElementAt<DictionaryEntry>(index).Key;
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }
  }
}
