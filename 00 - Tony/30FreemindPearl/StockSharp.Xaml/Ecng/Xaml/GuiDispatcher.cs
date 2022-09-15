using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace Ecng.Xaml
{
  /// <summary>
  /// Специальный класс, обеспечивающий исполнение действий в графическом потоке.
  /// </summary>
  public class GuiDispatcher : Disposable, IDispatcher
  {
    
    private readonly object \u0023\u003DzwonZBj4\u003D = new object();
    
    private readonly TimeSpan \u0023\u003DzTYTyCoowz51L = TimeSpan.FromSeconds(30.0);
    
    private readonly SynchronizedList<GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D> \u0023\u003DzRpkTdvc\u003D = new SynchronizedList<GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D>();
    
    private readonly CachedSynchronizedDictionary<object, Action> \u0023\u003DzgkIOxVglTUOSrOC3NHtvsZI\u003D = new CachedSynchronizedDictionary<object, Action>();
    
    private readonly SynchronizedDictionary<Action, int> \u0023\u003DzhyYlo1s6zc32R9cLyZ9Pc1w\u003D = new SynchronizedDictionary<Action, int>();
    
    private int \u0023\u003Dz2DYBJrpiNem2lUWfazfCVH\u00240NiAH = 100;
    
    private TimeSpan \u0023\u003DzWw\u00240mVU\u003D = TimeSpan.FromMilliseconds(1.0);
    
    private DispatcherTimer _dispatcherTimer;
    
    private DateTime \u0023\u003Dz2LpHdPUTW2ba;
    
    private long \u0023\u003DznSh0mhc\u003D;
    
    private bool \u0023\u003Dzml9ie58UH7TJ;
    
    private readonly Dispatcher \u0023\u003DzXe_UQEuSfiPGdGaoBQ\u003D\u003D;
    
    private static GuiDispatcher \u0023\u003DzNgYSIt0VUZIL;

    /// <summary>
    /// Создать <see cref="T:Ecng.Xaml.GuiDispatcher" />.
    /// </summary>
    public GuiDispatcher()
      : this(XamlHelper.CurrentThreadDispatcher)
    {
    }

    /// <summary>
    /// Создать <see cref="T:Ecng.Xaml.GuiDispatcher" />.
    /// </summary>
    /// <param name="dispatcher">Объект для доступа к графическому потоку.</param>
    public GuiDispatcher(Dispatcher dispatcher)
    {
      Dispatcher dispatcher1 = dispatcher;
      if (dispatcher1 == null)
        throw new ArgumentNullException(nameof(2127279704));
      this.\u0023\u003DzXe_UQEuSfiPGdGaoBQ\u003D\u003D = dispatcher1;
    }

    /// <summary>Объект для доступа к графическому потоку.</summary>
    public Dispatcher Dispatcher
    {
      get
      {
        return this.\u0023\u003DzXe_UQEuSfiPGdGaoBQ\u003D\u003D;
      }
    }

    /// <summary>
    /// </summary>
    public event Action<Exception> Error;

    /// <summary>
    /// </summary>
    public int MaxPeriodicalActionErrors
    {
      get
      {
        return this.\u0023\u003Dz2DYBJrpiNem2lUWfazfCVH\u00240NiAH;
      }
      set
      {
        if (value < -1)
          throw new ArgumentOutOfRangeException(nameof(2127279699));
        this.\u0023\u003Dz2DYBJrpiNem2lUWfazfCVH\u00240NiAH = value;
      }
    }

    /// <summary>
    /// Интервал обработки накопленных действий. По-умолчанию равен 1 млс.
    /// </summary>
    public TimeSpan Interval
    {
      get
      {
        return this.\u0023\u003DzWw\u00240mVU\u003D;
      }
      set
      {
        if (value <= TimeSpan.Zero)
          throw new ArgumentOutOfRangeException(nameof(2127279694));
        this.\u0023\u003DzWw\u00240mVU\u003D = value;
        this.\u0023\u003DzyLTfCA9FWijL();
        this.\u0023\u003DzlndqEJc\u003D();
      }
    }

    /// <summary>Количество действий, которое ожидает обработку.</summary>
    public int PendingActionsCount
    {
      get
      {
        return this.\u0023\u003DzRpkTdvc\u003D.Count + this.\u0023\u003DzgkIOxVglTUOSrOC3NHtvsZI\u003D.Count;
      }
    }

    /// <summary>
    /// </summary>
    public object AddPeriodicalAction(Action action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof(2127279673));
      object key = new object();
      this.\u0023\u003DzgkIOxVglTUOSrOC3NHtvsZI\u003D.Add(key, action);
      this.\u0023\u003DzlndqEJc\u003D();
      return key;
    }

    /// <summary>Выполнить все действия в очереди.</summary>
    public void FlushPendingActions()
    {
      this.\u0023\u003Dzml9ie58UH7TJ = true;
    }

    /// <summary>
    /// </summary>
    public void RemovePeriodicalAction(object token)
    {
      if (token == null)
        throw new ArgumentNullException(nameof(2127279668));
      Action andRemove = this.\u0023\u003DzgkIOxVglTUOSrOC3NHtvsZI\u003D.TryGetAndRemove<object, Action>(token);
      if (andRemove == null)
        return;
      this.\u0023\u003DzhyYlo1s6zc32R9cLyZ9Pc1w\u003D.Remove(andRemove);
    }

    /// <summary>Добавить действие.</summary>
    /// <param name="action">Действие.</param>
    public void AddAction(Action action)
    {
      if (this.CheckAccess())
        action();
      else
        this.\u0023\u003DzO8a3Qfw\u003D(new GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D(action));
    }

    /// <summary>
    /// Добавить действие. Пока оно не будет обработано, метод не отдаст управление программе.
    /// </summary>
    /// <param name="action">Действие.</param>
    public void AddSyncAction(Action action)
    {
      if (this.CheckAccess())
      {
        action();
      }
      else
      {
        GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D zpv9fUo0 = new GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D(action);
        zpv9fUo0.\u0023\u003Dzz\u002442LBM\u003D();
        this.\u0023\u003DzO8a3Qfw\u003D(zpv9fUo0);
        zpv9fUo0.\u0023\u003DzXyOzPTg\u003D<VoidType>();
      }
    }

    /// <summary>
    /// Добавить действие. Пока оно не будет обработано, метод не отдаст управление программе.
    /// </summary>
    /// <param name="action">Действие, возвращающее результат.</param>
    public T AddSyncAction<T>(Func<T> action)
    {
      GuiDispatcher.\u0023\u003DzhcqGha50mwE58pT7\u00241JS4gU\u003D<T> gha50mwE58pT71Js4gU = new GuiDispatcher.\u0023\u003DzhcqGha50mwE58pT7\u00241JS4gU\u003D<T>();
      gha50mwE58pT71Js4gU._myAction = action;
      if (this.CheckAccess())
        return gha50mwE58pT71Js4gU._myAction();
      GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D zpv9fUo0 = new GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D(new Func<object>(gha50mwE58pT71Js4gU.\u0023\u003DzHzrr7JMvNb9fXsbMAw\u003D\u003D));
      zpv9fUo0.\u0023\u003Dzz\u002442LBM\u003D();
      this.\u0023\u003DzO8a3Qfw\u003D(zpv9fUo0);
      return zpv9fUo0.\u0023\u003DzXyOzPTg\u003D<T>();
    }

    private void \u0023\u003DzO8a3Qfw\u003D(GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D _param1)
    {
      this.\u0023\u003DzRpkTdvc\u003D.Add(_param1);
      this.\u0023\u003DzlndqEJc\u003D();
    }

    private void \u0023\u003DzlndqEJc\u003D()
    {
      this.\u0023\u003Dz2LpHdPUTW2ba = DateTime.Now;
      lock (this.\u0023\u003DzwonZBj4\u003D)
      {
        if (this._dispatcherTimer != null)
          return;
        this._dispatcherTimer = new DispatcherTimer((DispatcherPriority) 9, this.Dispatcher);
        this._dispatcherTimer.add_Tick(new EventHandler(this.\u0023\u003Dze7CuhbrQWbKr));
        this._dispatcherTimer.set_Interval(new TimeSpan(this.Interval.Ticks / 10L));
        this._dispatcherTimer.Start();
      }
    }

    private void \u0023\u003Dze7CuhbrQWbKr(object _param1, EventArgs _param2)
    {
      if (++this.\u0023\u003DznSh0mhc\u003D % 10L != 0L && !this.\u0023\u003Dzml9ie58UH7TJ)
        return;
      this.\u0023\u003Dzml9ie58UH7TJ = false;
      bool flag = false;
      foreach (GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D zpv9fUo0 in this.\u0023\u003DzRpkTdvc\u003D.SyncGet<SynchronizedList<GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D>, GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D[]>(GuiDispatcher.SomeShit.\u0023\u003Dz8VfsYyRx9UMCFDlB1g\u003D\u003D ?? (GuiDispatcher.SomeShit.\u0023\u003Dz8VfsYyRx9UMCFDlB1g\u003D\u003D = new Func<SynchronizedList<GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D>, GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D[]>(GuiDispatcher.SomeShit.ShitMethod02.\u0023\u003Dz8fz5_VKj41iQqQyhtWg4HK4\u003D))))
      {
        flag = true;
        try
        {
          zpv9fUo0.\u0023\u003DzkpBNXvM\u003D();
        }
        catch (Exception ex1)
        {
          try
          {
            Action<Exception> zEn5ht6U = this.\u0023\u003DzEn5ht6U\u003D;
            if (zEn5ht6U != null)
              zEn5ht6U(ex1);
          }
          catch (Exception ex2)
          {
          }
        }
      }
      foreach (KeyValuePair<object, Action> cachedPair in this.\u0023\u003DzgkIOxVglTUOSrOC3NHtvsZI\u003D.CachedPairs)
      {
        Action key = cachedPair.Value;
        flag = true;
        try
        {
          key();
          this.\u0023\u003DzhyYlo1s6zc32R9cLyZ9Pc1w\u003D.Remove(key);
        }
        catch (Exception ex1)
        {
          try
          {
            Action<Exception> zEn5ht6U = this.\u0023\u003DzEn5ht6U\u003D;
            if (zEn5ht6U != null)
              zEn5ht6U(ex1);
          }
          catch (Exception ex2)
          {
          }
          if (this.MaxPeriodicalActionErrors >= 0)
          {
            int num;
            if (!this.\u0023\u003DzhyYlo1s6zc32R9cLyZ9Pc1w\u003D.TryGetValue(key, out num))
              num = 0;
            ++num;
            if (num >= this.MaxPeriodicalActionErrors)
              this.\u0023\u003DzgkIOxVglTUOSrOC3NHtvsZI\u003D.Remove(cachedPair.Key);
            this.\u0023\u003DzhyYlo1s6zc32R9cLyZ9Pc1w\u003D[key] = num;
          }
        }
      }
      if (flag || !(DateTime.Now - this.\u0023\u003Dz2LpHdPUTW2ba > this.\u0023\u003DzTYTyCoowz51L))
        return;
      this.\u0023\u003DzyLTfCA9FWijL();
    }

    private void \u0023\u003DzyLTfCA9FWijL()
    {
      lock (this.\u0023\u003DzwonZBj4\u003D)
      {
        if (this._dispatcherTimer == null)
          return;
        this._dispatcherTimer.Stop();
        this._dispatcherTimer = (DispatcherTimer) null;
      }
    }

    /// <summary>
    /// </summary>
    public static GuiDispatcher GlobalDispatcher
    {
      get
      {
        return GuiDispatcher.\u0023\u003DzNgYSIt0VUZIL ?? (GuiDispatcher.\u0023\u003DzNgYSIt0VUZIL = new GuiDispatcher());
      }
    }

    /// <summary>
    /// </summary>
    public static void InitGlobalDispatcher()
    {
      if (GuiDispatcher.\u0023\u003DzNgYSIt0VUZIL != null)
        return;
      GuiDispatcher.\u0023\u003DzNgYSIt0VUZIL = new GuiDispatcher();
    }

    /// <summary>Освободить занятые ресурсы.</summary>
    protected override void DisposeManaged()
    {
      this.\u0023\u003DzyLTfCA9FWijL();
      base.DisposeManaged();
    }

    /// <inheritdoc />
    public bool CheckAccess()
    {
      return this.Dispatcher.CheckAccess();
    }

    void IDispatcher.\u0023\u003DzBIUAQgeeuVoldv0lZLut9xg\u003D(Action _param1)
    {
      this.AddSyncAction(_param1);
    }

    void IDispatcher.\u0023\u003Dz14fNg9rfyduZuGwlUNGhzn8\u003D(Action _param1)
    {
      this.AddAction(_param1);
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly GuiDispatcher.SomeShit ShitMethod02 = new GuiDispatcher.SomeShit();
      public static Func<SynchronizedList<GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D>, GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D[]> \u0023\u003Dz8VfsYyRx9UMCFDlB1g\u003D\u003D;

      internal GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D[] \u0023\u003Dz8fz5_VKj41iQqQyhtWg4HK4\u003D(
        SynchronizedList<GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D> _param1)
      {
        return _param1.CopyAndClear<GuiDispatcher.\u0023\u003Dzpv9fUO0\u003D>();
      }
    }

    private sealed class \u0023\u003DzhcqGha50mwE58pT7\u00241JS4gU\u003D<\u0023\u003DznSahTwA\u003D>
    {
      public Func<\u0023\u003DznSahTwA\u003D> _myAction;

      internal object \u0023\u003DzHzrr7JMvNb9fXsbMAw\u003D\u003D()
      {
        return (object) this._myAction();
      }
    }

    private sealed class \u0023\u003Dzpv9fUO0\u003D
    {
      private readonly Action _uiAction;
      private readonly Func<object> \u0023\u003DzU8MQ6us\u003D;
      private object \u0023\u003DzwonZBj4\u003D;
      private object \u0023\u003DzEZEpekA\u003D;
      private bool \u0023\u003Dz8Y8GMpE\u003D;
      private Exception \u0023\u003DziaeoVKM\u003D;

      public \u0023\u003Dzpv9fUO0\u003D(Action _param1)
      {
        Action action = _param1;
        if (action == null)
          throw new ArgumentNullException(nameof(2127280536));
        this._uiAction = action;
      }

      public \u0023\u003Dzpv9fUO0\u003D(Func<object> _param1)
      {
        Func<object> func = _param1;
        if (func == null)
          throw new ArgumentNullException(nameof(2127280531));
        this.\u0023\u003DzU8MQ6us\u003D = func;
      }

      public void \u0023\u003DzkpBNXvM\u003D()
      {
        try
        {
          if (this._uiAction != null)
            this._uiAction();
          else
            this.\u0023\u003DzEZEpekA\u003D = this.\u0023\u003DzU8MQ6us\u003D();
        }
        catch (Exception ex)
        {
          if (this.\u0023\u003DzwonZBj4\u003D == null)
            throw;
          else
            this.\u0023\u003DziaeoVKM\u003D = ex;
        }
        finally
        {
          if (this.\u0023\u003DzwonZBj4\u003D != null)
          {
            lock (this.\u0023\u003DzwonZBj4\u003D)
            {
              this.\u0023\u003Dz8Y8GMpE\u003D = true;
              Monitor.Pulse(this.\u0023\u003DzwonZBj4\u003D);
            }
          }
        }
      }

      public void \u0023\u003Dzz\u002442LBM\u003D()
      {
        this.\u0023\u003DzwonZBj4\u003D = new object();
      }

      public T \u0023\u003DzXyOzPTg\u003D<T>()
      {
        lock (this.\u0023\u003DzwonZBj4\u003D)
        {
          if (!this.\u0023\u003Dz8Y8GMpE\u003D)
            Monitor.Wait(this.\u0023\u003DzwonZBj4\u003D);
        }
        if (this.\u0023\u003DziaeoVKM\u003D != null)
          throw new InvalidOperationException(nameof(2127280526), this.\u0023\u003DziaeoVKM\u003D);
        return this.\u0023\u003DzEZEpekA\u003D.To<T>();
      }
    }
  }
}
