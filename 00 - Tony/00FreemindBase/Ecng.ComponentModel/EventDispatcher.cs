// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.EventDispatcher
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Collections;
using Ecng.Common;
using MoreLinq;
using System;
using System.Collections.Generic;

namespace Ecng.ComponentModel
{
  public class EventDispatcher : Ecng.Common.Disposable
  {
    private readonly SynchronizedDictionary<string, BlockingQueue<Action>> _events = new SynchronizedDictionary<string, BlockingQueue<Action>>();
    private readonly Action<Exception> _errorHandler;

    public EventDispatcher(Action<Exception> errorHandler)
    {
      Action<Exception> action = errorHandler;
      if (action == null)
        throw new ArgumentNullException(nameof (errorHandler));
      this._errorHandler = action;
    }

    public void Add(Action evt)
    {
      this.Add(evt, string.Empty);
    }

    public virtual void Add(Action evt, string syncToken)
    {
      if (evt == null)
        throw new ArgumentNullException(nameof (evt));
      this._events.SafeAdd<string, BlockingQueue<Action>>(syncToken, new Func<string, BlockingQueue<Action>>(EventDispatcher.CreateNewThreadQueuePair)).Enqueue((Action) (() =>
      {
        try
        {
          evt();
        }
        catch (Exception ex)
        {
          this._errorHandler(ex);
        }
      }), false);
    }

    private static BlockingQueue<Action> CreateNewThreadQueuePair(
      string syncToken)
    {
      BlockingQueue<Action> queue = new BlockingQueue<Action>();
      ((Action) (() =>
      {
        Action action;
        while (!queue.IsClosed && queue.TryDequeue(out action, true, true))
          action();
      })).Thread().Name("EventDispatcher thread #" + syncToken).Start();
      return queue;
    }

    protected override void DisposeManaged()
    {
      this._events.SyncDo<SynchronizedDictionary<string, BlockingQueue<Action>>>((Action<SynchronizedDictionary<string, BlockingQueue<Action>>>) (d => d.ForEach<KeyValuePair<string, BlockingQueue<Action>>>((Action<KeyValuePair<string, BlockingQueue<Action>>>) (p => p.Value.Close()))));
      base.DisposeManaged();
    }
  }
}
