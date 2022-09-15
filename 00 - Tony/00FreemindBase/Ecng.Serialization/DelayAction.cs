// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.DelayAction
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using Ecng.Collections;
using Ecng.Common;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ecng.Serialization
{
  public class DelayAction : Ecng.Common.Disposable
  {
    private static readonly IEqualityComparer<DelayAction.IGroupItem> _itemComparer = (IEqualityComparer<DelayAction.IGroupItem>) new DelayAction.ItemComparer();
    private readonly CachedSynchronizedList<DelayAction.IGroup> _groups = new CachedSynchronizedList<DelayAction.IGroup>();
    private TimeSpan _flushInterval = TimeSpan.FromSeconds(1.0);
    private int _maxBatchSize = 1000;
    private readonly DelayAction.DummyBatchContext _batchContext = new DelayAction.DummyBatchContext();
    private readonly Action<Exception> _errorHandler;
    private Timer _flushTimer;
    private bool _isFlushing;

    public DelayAction(Action<Exception> errorHandler)
    {
      Action<Exception> action = errorHandler;
      if (action == null)
        throw new ArgumentNullException(nameof (errorHandler));
      this._errorHandler = action;
      this.DefaultGroup = (DelayAction.IGroup) this.CreateGroup<IDisposable>((Func<IDisposable>) null);
    }

    public DelayAction.IGroup DefaultGroup { get; }

    public TimeSpan FlushInterval
    {
      get
      {
        return this._flushInterval;
      }
      set
      {
        this._flushInterval = value;
        lock (this._groups.SyncRoot)
        {
          if (this._flushTimer == null)
            return;
          this._flushTimer.Interval(this._flushInterval);
        }
      }
    }

    public DelayAction.IGroup<T> CreateGroup<T>(Func<T> init) where T : IDisposable
    {
      DelayAction.Group<T> group = new DelayAction.Group<T>(this, init);
      this._groups.Add((DelayAction.IGroup) group);
      return (DelayAction.IGroup<T>) group;
    }

    public void DeleteGroup(DelayAction.IGroup group)
    {
      if (group == null)
        throw new ArgumentNullException(nameof (group));
      if (group == this.DefaultGroup)
        throw new ArgumentException();
      this._groups.Remove(group);
    }

    public int MaxBatchSize
    {
      get
      {
        return this._maxBatchSize;
      }
      set
      {
        if (value <= 0)
          throw new ArgumentOutOfRangeException();
        this._maxBatchSize = value;
      }
    }

    private void TryCreateTimer()
    {
      lock (this._groups.SyncRoot)
      {
        if (this._isFlushing || this._flushTimer != null)
          return;
        this._flushTimer = new Action(this.OnFlush).TimerInvariant().Interval(this._flushInterval);
      }
    }

    public void OnFlush()
    {
      try
      {
        DelayAction.IGroup[] cache;
        lock (this._groups.SyncRoot)
        {
          if (this._isFlushing)
            return;
          this._isFlushing = true;
          cache = this._groups.Cache;
        }
        bool flag = false;
        try
        {
          foreach (DelayAction.IGroup group in cache)
          {
            if (this.IsDisposed)
              break;
            DelayAction.IGroupItem[] itemsAndClear = ((DelayAction.IInternalGroup) group).GetItemsAndClear();
            if (itemsAndClear.Length != 0)
            {
              flag = true;
              List<DelayAction.IGroupItem> groupItemList = new List<DelayAction.IGroupItem>();
              foreach (DelayAction.IGroupItem groupItem in itemsAndClear)
              {
                if (!groupItem.CanBatch)
                {
                  this.BatchFlushAndClear(group, (ICollection<DelayAction.IGroupItem>) groupItemList);
                  groupItemList.Clear();
                  if (!this.IsDisposed)
                    this.Flush(groupItem);
                  else
                    break;
                }
                else
                  groupItemList.Add(groupItem);
              }
              if (!this.IsDisposed)
                this.BatchFlushAndClear(group, (ICollection<DelayAction.IGroupItem>) groupItemList);
            }
          }
        }
        finally
        {
          lock (this._groups.SyncRoot)
          {
            this._isFlushing = false;
            if (!flag)
            {
              if (this._flushTimer != null)
              {
                this._flushTimer.Dispose();
                this._flushTimer = (Timer) null;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        this._errorHandler(ex);
      }
    }

    private void Flush(DelayAction.IGroupItem item)
    {
      Exception exception;
      try
      {
        item.Do((IDisposable) null);
        exception = (Exception) null;
      }
      catch (Exception ex)
      {
        this._errorHandler(ex);
        exception = ex;
      }
      Action<Exception> postAction = item.PostAction;
      if (postAction == null)
        return;
      postAction(exception);
    }

    public void WaitFlush(bool dispose)
    {
      ((IEnumerable<DelayAction.IGroup>) this._groups.Cache).Where<DelayAction.IGroup>((Func<DelayAction.IGroup, bool>) (g => g != this.DefaultGroup)).ForEach<DelayAction.IGroup>((Action<DelayAction.IGroup>) (g => g.WaitFlush(false)));
      this.DefaultGroup.WaitFlush(dispose);
    }

    private void BatchFlushAndClear(
      DelayAction.IGroup group,
      ICollection<DelayAction.IGroupItem> actions)
    {
      if (actions.IsEmpty<DelayAction.IGroupItem>())
        return;
      Exception error = (Exception) null;
      try
      {
        using (IDisposable scope = ((DelayAction.IInternalGroup) group).Init())
        {
          foreach (IEnumerable<DelayAction.IGroupItem> groupItems in actions.Distinct<DelayAction.IGroupItem>(DelayAction._itemComparer).Batch<DelayAction.IGroupItem>(this.MaxBatchSize))
          {
            using (IBatchContext batchContext = this.BeginBatch(group))
            {
              foreach (DelayAction.IGroupItem groupItem in groupItems)
              {
                if (groupItem.BreakBatchOnError)
                  groupItem.Do(scope);
                else
                  this.Flush(groupItem);
                if (this.IsDisposed)
                  break;
              }
              batchContext.Commit();
            }
            if (this.IsDisposed)
              break;
          }
        }
      }
      catch (Exception ex)
      {
        this._errorHandler(ex);
        error = ex;
      }
      actions.Where<DelayAction.IGroupItem>((Func<DelayAction.IGroupItem, bool>) (a => a.PostAction != null)).ForEach<DelayAction.IGroupItem>((Action<DelayAction.IGroupItem>) (a => a.PostAction(error)));
    }

    protected virtual IBatchContext BeginBatch(DelayAction.IGroup group)
    {
      return (IBatchContext) this._batchContext;
    }

    public interface IGroup
    {
      void Add(Action action, Action<Exception> postAction = null, bool canBatch = true, bool breakBatchOnError = true);

      void Add(
        Action<IDisposable> action,
        Action<Exception> postAction = null,
        bool canBatch = true,
        bool breakBatchOnError = true);

      void WaitFlush(bool dispose);
    }

    public interface IGroup<T> : DelayAction.IGroup where T : IDisposable
    {
      void Add(
        Action<T> action,
        Action<Exception> postAction = null,
        bool canBatch = true,
        bool breakBatchOnError = true);

      void Add<TState>(
        Action<T, TState> action,
        TState state,
        Action<Exception> postAction = null,
        bool canBatch = true,
        bool breakBatchOnError = true,
        Func<TState, TState, bool> compareStates = null);
    }

    private interface IInternalGroup
    {
      IDisposable Init();

      DelayAction.IGroupItem[] GetItemsAndClear();
    }

    private interface IGroupItem
    {
      void Do(IDisposable scope);

      Action<Exception> PostAction { get; }

      bool CanBatch { get; }

      bool BreakBatchOnError { get; }
    }

    private interface IInternalGroupItem
    {
      bool Equals(DelayAction.IGroupItem other);

      int GetStateHashCode();
    }

    private class Group<T> : DelayAction.IGroup<T>, DelayAction.IGroup, DelayAction.IInternalGroup
      where T : IDisposable
    {
      private readonly SynchronizedList<DelayAction.IGroupItem> _actions = new SynchronizedList<DelayAction.IGroupItem>();
      private readonly DelayAction.Group<T>.Dummy _dummy = new DelayAction.Group<T>.Dummy();
      private readonly DelayAction _parent;
      private readonly Func<T> _init;

      public Group(DelayAction parent, Func<T> init)
      {
        DelayAction delayAction = parent;
        if (delayAction == null)
          throw new ArgumentNullException(nameof (parent));
        this._parent = delayAction;
        this._init = init;
      }

      public IDisposable Init()
      {
        if (this._init == null)
          return (IDisposable) this._dummy;
        T obj = this._init();
        if ((object) obj == null)
          throw new InvalidOperationException();
        return (IDisposable) obj;
      }

      public void Add(
        Action action,
        Action<Exception> postAction = null,
        bool canBatch = true,
        bool breakBatchOnError = true)
      {
        if (action == null)
          throw new ArgumentNullException(nameof (action));
        this.Add((Action<IDisposable>) (s => action()), postAction, canBatch, breakBatchOnError);
      }

      public void Add(
        Action<IDisposable> action,
        Action<Exception> postAction = null,
        bool canBatch = true,
        bool breakBatchOnError = true)
      {
        if (action == null)
          throw new ArgumentNullException(nameof (action));
        this.Add((Action<T>) (scope => action((IDisposable) scope)), postAction, canBatch, breakBatchOnError);
      }

      public void Add(
        Action<T> action,
        Action<Exception> postAction = null,
        bool canBatch = true,
        bool breakBatchOnError = true)
      {
        if (action == null)
          throw new ArgumentNullException(nameof (action));
        this.Add<object>((Action<T, object>) ((scope, state) => action(scope)), (object) null, postAction, canBatch, breakBatchOnError, (Func<object, object, bool>) null);
      }

      public void Add<TState>(
        Action<T, TState> action,
        TState state,
        Action<Exception> postAction = null,
        bool canBatch = true,
        bool breakBatchOnError = true,
        Func<TState, TState, bool> compareStates = null)
      {
        this.Add<TState>(new DelayAction.Group<T>.Item<TState>(action, state, postAction, canBatch, breakBatchOnError, compareStates));
      }

      private void Add<TState>(DelayAction.Group<T>.Item<TState> item)
      {
        if (item == null)
          throw new ArgumentNullException(nameof (item));
        lock (this._actions.SyncRoot)
          this._actions.Add((DelayAction.IGroupItem) item);
        this._parent.TryCreateTimer();
      }

      public void WaitFlush(bool dispose)
      {
        DelayAction.Group<T>.FlushItem flushItem = new DelayAction.Group<T>.FlushItem(this._parent, dispose);
        this.Add<object>((DelayAction.Group<T>.Item<object>) flushItem);
        flushItem.Wait();
      }

      public DelayAction.IGroupItem[] GetItemsAndClear()
      {
        return this._actions.CopyAndClear<DelayAction.IGroupItem>();
      }

      private class Dummy : IDisposable
      {
        void IDisposable.Dispose()
        {
        }
      }

      private class Item<TState> : DelayAction.IGroupItem, DelayAction.IInternalGroupItem
      {
        private readonly TState _state;
        private readonly Func<TState, TState, bool> _compareStates;
        private readonly Action<T, TState> _action;

        public virtual void Do(IDisposable scope)
        {
          this._action((T) scope, this._state);
        }

        public Action<Exception> PostAction { get; protected set; }

        public bool CanBatch { get; protected set; }

        public bool BreakBatchOnError { get; protected set; }

        protected Item()
        {
        }

        public Item(
          Action<T, TState> action,
          TState state,
          Action<Exception> postAction,
          bool canBatch,
          bool breakBatchOnError,
          Func<TState, TState, bool> compareStates)
        {
          this._state = state;
          this._compareStates = compareStates;
          this._action = action;
          this.PostAction = postAction;
          this.CanBatch = canBatch;
          this.BreakBatchOnError = breakBatchOnError;
        }

        bool DelayAction.IInternalGroupItem.Equals(DelayAction.IGroupItem other)
        {
          if (this._compareStates == null)
            return false;
          DelayAction.Group<T>.Item<TState> obj = other as DelayAction.Group<T>.Item<TState>;
          if (obj == null)
            return false;
          return this._compareStates(this._state, obj._state);
        }

        int DelayAction.IInternalGroupItem.GetStateHashCode()
        {
          return typeof (TState).GetHashCode() ^ ((object) this._state == null ? 0 : 1) ^ (this._compareStates == null ? 0 : 1);
        }
      }

      private class FlushItem : DelayAction.Group<T>.Item<object>
      {
        private readonly SyncObject _syncObject = new SyncObject();
        private bool _isProcessed;
        private Exception _err;

        public FlushItem(DelayAction parent, bool dispose)
        {
          DelayAction.Group<T>.FlushItem flushItem = this;
          if (parent == null)
            throw new ArgumentNullException(nameof (parent));
          this.PostAction = (Action<Exception>) (err =>
          {
            flushItem._err = err;
            if (dispose)
              parent.Dispose();
            lock (flushItem._syncObject)
            {
              flushItem._isProcessed = true;
              flushItem._syncObject.Pulse();
            }
          });
          this.CanBatch = true;
          this.BreakBatchOnError = true;
        }

        public override void Do(IDisposable scope)
        {
        }

        public void Wait()
        {
          lock (this._syncObject)
          {
            if (!this._isProcessed)
              this._syncObject.Wait(new TimeSpan?());
          }
          if (this._err != null)
            throw this._err;
        }
      }
    }

    private class ItemComparer : IEqualityComparer<DelayAction.IGroupItem>
    {
      bool IEqualityComparer<DelayAction.IGroupItem>.Equals(
        DelayAction.IGroupItem x,
        DelayAction.IGroupItem y)
      {
        return ((DelayAction.IInternalGroupItem) x).Equals(y);
      }

      int IEqualityComparer<DelayAction.IGroupItem>.GetHashCode(
        DelayAction.IGroupItem obj)
      {
        return ((DelayAction.IInternalGroupItem) obj).GetStateHashCode();
      }
    }

    private class DummyBatchContext : IBatchContext, IDisposable
    {
      void IDisposable.Dispose()
      {
      }

      void IBatchContext.Commit()
      {
      }
    }
  }
}
