// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.Stat`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Collections;
using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Ecng.ComponentModel
{
  public class Stat<TAction>
  {
    private readonly Dictionary<TAction, int> _freq = new Dictionary<TAction, int>();
    private readonly OrderedPriorityQueue<TimeSpan, TAction> _longests = new OrderedPriorityQueue<TimeSpan, TAction>((IComparer<TimeSpan>) new BackwardComparer<TimeSpan>());
    private readonly Dictionary<Stopwatch, ValueTuple<IPAddress, TAction>> _pendings = new Dictionary<Stopwatch, ValueTuple<IPAddress, TAction>>();
    private readonly Dictionary<IPAddress, RefTriple<HashSet<Stopwatch>, long, TimeSpan>> _allWatches = new Dictionary<IPAddress, RefTriple<HashSet<Stopwatch>, long, TimeSpan>>();
    private readonly SyncObject _sync = new SyncObject();
    private IPAddress _aggressiveIp;
    private TimeSpan _aggressiveTime;

    public int LongestLimit { get; set; } = 100;

    public int FreqLimit { get; set; } = 100000;

    public StatInfo<TAction> GetInfo(int skip, int take)
    {
      lock (this._sync)
      {
        StatInfo<TAction> statInfo = new StatInfo<TAction>();
        statInfo.UniqueCount = this._allWatches.Count;
        statInfo.PendingCount = this._pendings.Count;
        statInfo.AggressiveAddress = this._aggressiveIp;
        statInfo.AggressiveTime = this._aggressiveTime;
        statInfo.Freq = this._freq.OrderByDescending<KeyValuePair<TAction, int>, int>((Func<KeyValuePair<TAction, int>, int>) (p => p.Value)).Skip<KeyValuePair<TAction, int>>(skip).Take<KeyValuePair<TAction, int>>(take).Select<KeyValuePair<TAction, int>, StatInfo<TAction>.Item<int>>((Func<KeyValuePair<TAction, int>, StatInfo<TAction>.Item<int>>) (p => new StatInfo<TAction>.Item<int>()
        {
          Value = p.Value,
          Action = p.Key
        })).ToArray<StatInfo<TAction>.Item<int>>();
        statInfo.Longest = this._longests.Skip<KeyValuePair<TimeSpan, TAction>>(skip).Take<KeyValuePair<TimeSpan, TAction>>(take).Select<KeyValuePair<TimeSpan, TAction>, StatInfo<TAction>.Item<TimeSpan>>((Func<KeyValuePair<TimeSpan, TAction>, StatInfo<TAction>.Item<TimeSpan>>) (p => new StatInfo<TAction>.Item<TimeSpan>()
        {
          Value = p.Key,
          Action = p.Value
        })).ToArray<StatInfo<TAction>.Item<TimeSpan>>();
        statInfo.Pendings = this._pendings.Skip<KeyValuePair<Stopwatch, ValueTuple<IPAddress, TAction>>>(skip).Take<KeyValuePair<Stopwatch, ValueTuple<IPAddress, TAction>>>(take).Select<KeyValuePair<Stopwatch, ValueTuple<IPAddress, TAction>>, StatInfo<TAction>.Item<TimeSpan>>((Func<KeyValuePair<Stopwatch, ValueTuple<IPAddress, TAction>>, StatInfo<TAction>.Item<TimeSpan>>) (p => new StatInfo<TAction>.Item<TimeSpan>()
        {
          Value = p.Key.Elapsed,
          Address = p.Value.Item1,
          Action = p.Value.Item2
        })).ToArray<StatInfo<TAction>.Item<TimeSpan>>();
        statInfo = statInfo;
        return statInfo;
      }
    }

    public Stat<TAction>.Item Begin(TAction action, IPAddress address)
    {
      if ((object) action == null)
        throw new ArgumentNullException(nameof (action));
      if (address == null)
        throw new ArgumentNullException(nameof (address));
      Stopwatch stopwatch = Stopwatch.StartNew();
      lock (this._sync)
      {
        this._pendings.Add(stopwatch, new ValueTuple<IPAddress, TAction>(address, action));
        RefTriple<HashSet<Stopwatch>, long, TimeSpan> refTriple = this._allWatches.SafeAdd<IPAddress, RefTriple<HashSet<Stopwatch>, long, TimeSpan>>(address, (Func<IPAddress, RefTriple<HashSet<Stopwatch>, long, TimeSpan>>) (key => new RefTriple<HashSet<Stopwatch>, long, TimeSpan>(new HashSet<Stopwatch>(), 0L, new TimeSpan())));
        refTriple.First.Add(stopwatch);
        ++refTriple.Second;
        int num;
        if (this._freq.TryGetValue(action, out num))
        {
          ++num;
          this._freq[action] = num;
        }
        else if (this._freq.Count < this.FreqLimit)
          this._freq.Add(action, 1);
      }
      return new Stat<TAction>.Item(this, action, address, stopwatch);
    }

    private void End(Stat<TAction>.Item item, Stopwatch watch)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      if (watch == null)
        throw new ArgumentNullException(nameof (watch));
      TimeSpan elapsed = watch.Elapsed;
      IPAddress address = item.Address;
      lock (this._sync)
      {
        this._pendings.Remove(watch);
        RefTriple<HashSet<Stopwatch>, long, TimeSpan> refTriple;
        if (this._allWatches.TryGetValue(address, out refTriple))
        {
          refTriple.First.Remove(watch);
          refTriple.Third += elapsed;
          if (this._aggressiveTime == new TimeSpan())
          {
            this._aggressiveTime = refTriple.Third;
            this._aggressiveIp = address;
          }
          else if (this._aggressiveTime < refTriple.Third)
          {
            this._aggressiveTime = refTriple.Third;
            this._aggressiveIp = address;
          }
        }
        this._longests.Enqueue(watch.Elapsed, item.Action);
        if (this._longests.Count <= this.LongestLimit)
          return;
        this._longests.Dequeue();
      }
    }

    public void Clear()
    {
      lock (this._sync)
      {
        this._aggressiveIp = (IPAddress) null;
        this._aggressiveTime = new TimeSpan();
        this._allWatches.Clear();
        this._freq.Clear();
        this._pendings.Clear();
        this._longests.Clear();
      }
    }

    public class Item : IDisposable
    {
      private readonly Stat<TAction> _parent;
      private readonly Stopwatch _watch;
      internal readonly TAction Action;
      internal readonly IPAddress Address;

      internal Item(Stat<TAction> parent, TAction action, IPAddress address, Stopwatch watch)
      {
        Stat<TAction> stat = parent;
        if (stat == null)
          throw new ArgumentNullException(nameof (parent));
        this._parent = stat;
        TAction action1 = action;
        if ((object) action1 == null)
          throw new ArgumentNullException(nameof (action));
        this.Action = action1;
        IPAddress ipAddress = address;
        if (ipAddress == null)
          throw new ArgumentNullException(nameof (address));
        this.Address = ipAddress;
        Stopwatch stopwatch = watch;
        if (stopwatch == null)
          throw new ArgumentNullException(nameof (watch));
        this._watch = stopwatch;
      }

      void IDisposable.Dispose()
      {
        this._watch.Stop();
        this._parent.End(this, this._watch);
      }
    }
  }
}
