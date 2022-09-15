// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.DispatcherObservableCollection`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Collections;
using Ecng.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ecng.ComponentModel
{
  public class DispatcherObservableCollection<TItem> : BaseObservableCollection, ISynchronizedCollection<TItem>, ISynchronizedCollection, ICollection<TItem>, IEnumerable<TItem>, IEnumerable, IListEx<TItem>, IList<TItem>, ICollectionEx<TItem>, IList, ICollection
  {
    private readonly Queue<DispatcherObservableCollection<TItem>.CollectionAction> _pendingActions = new Queue<DispatcherObservableCollection<TItem>.CollectionAction>();
    private int _pendingCount;
    private bool _isTimerStarted;

    public event Action BeforeUpdate;

    public event Action AfterUpdate;

    public DispatcherObservableCollection(IDispatcher dispatcher, IListEx<TItem> items)
    {
      IDispatcher dispatcher1 = dispatcher;
      if (dispatcher1 == null)
        throw new ArgumentNullException(nameof (dispatcher));
      this.Dispatcher = dispatcher1;
      IListEx<TItem> listEx = items;
      if (listEx == null)
        throw new ArgumentNullException(nameof (items));
      this.Items = listEx;
    }

    public IListEx<TItem> Items { get; }

    public IDispatcher Dispatcher { get; }

    public event Action<IEnumerable<TItem>> AddedRange
    {
      add
      {
        this.Items.AddedRange += value;
      }
      remove
      {
        this.Items.AddedRange -= value;
      }
    }

    public event Action<IEnumerable<TItem>> RemovedRange
    {
      add
      {
        this.Items.RemovedRange += value;
      }
      remove
      {
        this.Items.RemovedRange -= value;
      }
    }

    public virtual void AddRange(IEnumerable<TItem> items)
    {
      if (!this.Dispatcher.CheckAccess())
      {
        this.AddAction(new DispatcherObservableCollection<TItem>.CollectionAction(DispatcherObservableCollection<TItem>.ActionTypes.Add, items.ToArray<TItem>()));
      }
      else
      {
        this.Items.AddRange(items);
        this._pendingCount = this.Items.Count;
        this.CheckCount();
      }
    }

    public virtual void RemoveRange(IEnumerable<TItem> items)
    {
      if (!this.Dispatcher.CheckAccess())
      {
        this.AddAction(new DispatcherObservableCollection<TItem>.CollectionAction(DispatcherObservableCollection<TItem>.ActionTypes.Remove, items.ToArray<TItem>()));
      }
      else
      {
        this.Items.RemoveRange(items);
        this._pendingCount = this.Items.Count;
      }
    }

    public override int RemoveRange(int index, int count)
    {
      if (index < -1)
        throw new ArgumentOutOfRangeException(nameof (index));
      if (count <= 0)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (this.Dispatcher.CheckAccess())
        return this.Items.RemoveRange(index, count);
      int num1 = this._pendingCount - index;
      this.AddAction(new DispatcherObservableCollection<TItem>.CollectionAction(index, count));
      int num2 = count;
      return num1.Min(num2).Max(0);
    }

    public IEnumerator<TItem> GetEnumerator()
    {
      return this.Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public virtual void Add(TItem item)
    {
      if (!this.Dispatcher.CheckAccess())
      {
        this.AddAction(new DispatcherObservableCollection<TItem>.CollectionAction(DispatcherObservableCollection<TItem>.ActionTypes.Add, new TItem[1]
        {
          item
        }));
      }
      else
      {
        this.Items.Add(item);
        this._pendingCount = this.Items.Count;
        this.CheckCount();
      }
    }

    public virtual bool Remove(TItem item)
    {
      if (!this.Dispatcher.CheckAccess())
      {
        this.AddAction(new DispatcherObservableCollection<TItem>.CollectionAction(DispatcherObservableCollection<TItem>.ActionTypes.Remove, new TItem[1]
        {
          item
        }));
        return true;
      }
      int num = this.Items.Remove(item) ? 1 : 0;
      this._pendingCount = this.Items.Count;
      return num != 0;
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
      if (!this.Dispatcher.CheckAccess())
      {
        this.AddAction(new DispatcherObservableCollection<TItem>.CollectionAction(DispatcherObservableCollection<TItem>.ActionTypes.Clear, Array.Empty<TItem>()));
      }
      else
      {
        this.Items.Clear();
        this._pendingCount = 0;
      }
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
      if (!this.Dispatcher.CheckAccess())
        throw new NotSupportedException();
      return this.Items.Contains(item);
    }

    public void CopyTo(TItem[] array, int arrayIndex)
    {
      if (!this.Dispatcher.CheckAccess())
        throw new NotSupportedException();
      this.Items.CopyTo(array, arrayIndex);
    }

    void ICollection.CopyTo(Array array, int index)
    {
      this.CopyTo((TItem[]) array, index);
    }

    public override int Count
    {
      get
      {
        if (!this.Dispatcher.CheckAccess())
          throw new NotSupportedException();
        return this.Items.Count;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return (object) this.SyncRoot;
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
      if (!this.Dispatcher.CheckAccess())
        return (int) this.Do((Func<object>) (() => (object) this.IndexOf(item)));
      return this.Items.IndexOf(item);
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
        if (!this.Dispatcher.CheckAccess())
          return (TItem) this.Do((Func<object>) (() => (object) this[index]));
        return this.Items[index];
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    public object Do(Func<object> func)
    {
      if (func == null)
        throw new ArgumentNullException(nameof (func));
      DispatcherObservableCollection<TItem>.CollectionAction collectionAction = new DispatcherObservableCollection<TItem>.CollectionAction(func)
      {
        SyncRoot = new SyncObject()
      };
      this.AddAction(collectionAction);
      lock (collectionAction.SyncRoot)
      {
        if (collectionAction.ConvertResult == null)
          collectionAction.SyncRoot.Wait(new TimeSpan?());
        return collectionAction.ConvertResult;
      }
    }

    private void AddAction(
      DispatcherObservableCollection<TItem>.CollectionAction item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      lock (this.SyncRoot)
      {
        switch (item.Type)
        {
          case DispatcherObservableCollection<TItem>.ActionTypes.Add:
            this._pendingCount += item.Count;
            break;
          case DispatcherObservableCollection<TItem>.ActionTypes.Remove:
            if (item.Items == null)
            {
              this._pendingCount -= item.Count;
              break;
            }
            this._pendingCount -= item.Items.Length;
            break;
          case DispatcherObservableCollection<TItem>.ActionTypes.Clear:
            this._pendingCount = 0;
            break;
        }
        this._pendingActions.Enqueue(item);
        if (this._isTimerStarted)
          return;
        this._isTimerStarted = true;
      }
      new Action(this.OnFlush).Timer().Interval(TimeSpan.FromMilliseconds(300.0), new TimeSpan(-1L));
    }

    private void OnFlush()
    {
      List<DispatcherObservableCollection<TItem>.CollectionAction> pendingActions = new List<DispatcherObservableCollection<TItem>.CollectionAction>();
      bool hasClear = false;
      Exception error = (Exception) null;
      try
      {
        DispatcherObservableCollection<TItem>.CollectionAction[] array;
        lock (this.SyncRoot)
        {
          this._isTimerStarted = false;
          array = this._pendingActions.ToArray();
          this._pendingActions.Clear();
        }
        foreach (DispatcherObservableCollection<TItem>.CollectionAction collectionAction in array)
        {
          switch (collectionAction.Type)
          {
            case DispatcherObservableCollection<TItem>.ActionTypes.Add:
            case DispatcherObservableCollection<TItem>.ActionTypes.Remove:
              pendingActions.Add(collectionAction);
              break;
            case DispatcherObservableCollection<TItem>.ActionTypes.Clear:
              pendingActions.Clear();
              hasClear = true;
              break;
            case DispatcherObservableCollection<TItem>.ActionTypes.Wait:
              pendingActions.Add(collectionAction);
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
        }
      }
      catch (Exception ex)
      {
        error = ex;
      }
      this.Dispatcher.InvokeAsync((Action) (() =>
      {
        Action beforeUpdate = this.BeforeUpdate;
        if (beforeUpdate != null)
          beforeUpdate();
        if (hasClear)
          this.Items.Clear();
        foreach (DispatcherObservableCollection<TItem>.CollectionAction collectionAction in pendingActions)
        {
          switch (collectionAction.Type)
          {
            case DispatcherObservableCollection<TItem>.ActionTypes.Add:
              this.Items.AddRange((IEnumerable<TItem>) collectionAction.Items);
              this.CheckCount();
              continue;
            case DispatcherObservableCollection<TItem>.ActionTypes.Remove:
              if (collectionAction.Items != null)
              {
                this.Items.RemoveRange((IEnumerable<TItem>) collectionAction.Items);
                continue;
              }
              this.Items.RemoveRange(collectionAction.Index, collectionAction.Count);
              continue;
            case DispatcherObservableCollection<TItem>.ActionTypes.Wait:
              object obj = collectionAction.Convert();
              lock (collectionAction.SyncRoot)
              {
                collectionAction.ConvertResult = obj;
                collectionAction.SyncRoot.Pulse();
                continue;
              }
            default:
              throw new ArgumentOutOfRangeException();
          }
        }
        Action afterUpdate = this.AfterUpdate;
        if (afterUpdate != null)
          afterUpdate();
        if (error != null)
          throw error;
      }));
    }

    public SyncObject SyncRoot { get; } = new SyncObject();

    private enum ActionTypes
    {
      Add,
      Remove,
      Clear,
      Wait,
    }

    private class CollectionAction
    {
      public CollectionAction(
        DispatcherObservableCollection<TItem>.ActionTypes type,
        params TItem[] items)
      {
        this.Type = type;
        TItem[] objArray = items;
        if (objArray == null)
          throw new ArgumentNullException(nameof (items));
        this.Items = objArray;
      }

      public CollectionAction(int index, int count)
      {
        this.Type = DispatcherObservableCollection<TItem>.ActionTypes.Remove;
        this.Index = index;
        this.Count = count;
      }

      public CollectionAction(Func<object> convert)
      {
        this.Type = DispatcherObservableCollection<TItem>.ActionTypes.Wait;
        this.Items = Array.Empty<TItem>();
        Func<object> func = convert;
        if (func == null)
          throw new ArgumentNullException(nameof (convert));
        this.Convert = func;
      }

      public DispatcherObservableCollection<TItem>.ActionTypes Type { get; }

      public TItem[] Items { get; }

      public int Index { get; }

      public int Count { get; }

      public SyncObject SyncRoot { get; set; }

      public Func<object> Convert { get; }

      public object ConvertResult { get; set; }
    }
  }
}
