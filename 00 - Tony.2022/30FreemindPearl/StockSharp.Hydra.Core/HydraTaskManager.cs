// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.HydraTaskManager
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Reflection;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StockSharp.Hydra.Core
{
  /// <summary>Task manager.</summary>
  public class HydraTaskManager : BaseLogReceiver
  {
    private readonly CachedSynchronizedDictionary<IHydraTask, CachedSynchronizedSet<HydraTaskSecurity>> _taskSecurities = new CachedSynchronizedDictionary<IHydraTask, CachedSynchronizedSet<HydraTaskSecurity>>();
    private readonly CachedSynchronizedSet<HydraTaskInfo> _settings = new CachedSynchronizedSet<HydraTaskInfo>();
    private const string _pluginsDir = "Plugins";

    /// <summary>Instance.</summary>
    public static HydraTaskManager Instance
    {
      get
      {
        return ConfigManager.GetService<HydraTaskManager>();
      }
    }

    private static HydraStorage Storage
    {
      get
      {
        return ConfigManager.GetService<HydraStorage>();
      }
    }

    /// <summary>All created tasks.</summary>
    public IEnumerable<IHydraTask> Tasks
    {
      get
      {
        return _taskSecurities.CachedKeys;
      }
    }

    /// <summary>All available tasks.</summary>
    public Type[] AvailableTasks { get; private set; }

    /// <summary>All created settings.</summary>
    public IEnumerable<HydraTaskInfo> Settings
    {
      get
      {
        return _settings.Cache;
      }
    }

    /// <summary>Task added event.</summary>
    public event Action<IHydraTask> TaskAdded;

    /// <summary>Task removed event.</summary>
    public event Action<IHydraTask> TaskRemoved;

    /// <summary>Securities added event.</summary>
    public event Action<IHydraTask, IEnumerable<HydraTaskSecurity>> SecuritiesAdded;

    /// <summary>Securities removed event.</summary>
    public event Action<IHydraTask, IEnumerable<HydraTaskSecurity>, bool> SecuritiesRemoved;

    private HydraTaskSecurity CreateAll(IHydraTask task)
    {
      if (task == null)
        throw new ArgumentNullException(nameof (task));
      HydraTaskSecurity taskSecurity = task.ToTaskSecurity(TraderHelper.AllSecurity);
      DataType[] array = task.SupportedDataTypes.Where( t => t.IsMarketData ).ToArray();
      if (array.IsEmpty())
        array = task.SupportedDataTypes.ToArray();
      foreach (DataType dataType in array)
        taskSecurity.AddDataType(dataType);
      return taskSecurity;
    }

    public void Init(IEnumerable<Type> adapters, Func<string, Type> migration)
    {
      if (adapters == null)
        throw new ArgumentNullException(nameof (adapters));
      if (migration == null)
        throw new ArgumentNullException(nameof (migration));
      List<Type> typeList = new List<Type>();
      typeList.AddRange(adapters.Select( a => typeof( ConnectorHydraTask<> ).Make( a ) ) );
      Assembly[] array = AppDomain.CurrentDomain.GetAssemblies().ToArray();
      if (Directory.Exists("Plugins"))
      {
        foreach (string str in Directory.GetFiles( "Plugins", "*.dll" ).Where( p => Path.GetFileNameWithoutExtension( p ).StartsWithIgnoreCase( "StockSharp.Hydra." ) ) )
        {
          if (!str.IsAssembly())
          {
            this.AddWarningLog(LocalizedStrings.Str2897Params, str );
          }
          else
          {
            AssemblyName asmName = AssemblyName.GetAssemblyName(str);
            if ( array.FirstOrDefault( a => asmName.Name.EqualsIgnoreCase( a.GetName().Name ) ) != null )
            {
              this.AddWarningLog("{0} already loaded. ignoring.", str );
            }
            else
            {
              try
              {
                Assembly assembly = Assembly.LoadFrom(str);
                this.AddDebugLog("loaded plugin {0}", str );
                typeList.AddRange( assembly.GetTypes().Where( t =>
                {
                    if ( typeof( IHydraTask ).IsAssignableFrom( t ) )
                        return !t.IsAbstract;
                    return false;
                } ).ToArray() );
              }
              catch (Exception ex)
              {
                ex.LogError( null );
              }
            }
          }
        }
      }
      AvailableTasks = typeList.ToArray();
      ContinueOnExceptionContext exceptionContext = new ContinueOnExceptionContext();
      exceptionContext.Error += ex => ex.LogError( null );
      IDictionary<HydraTaskInfo, IEnumerable<HydraTaskSecurity>> dictionary = Storage.Load( this );
      using (exceptionContext.ToScope( true))
        _settings.AddRange( dictionary.Keys );
      foreach (IGrouping<string, HydraTaskInfo> grouping in Settings.GroupBy( set => set.TaskType ) )
      {
        Type type = Type.GetType(grouping.Key, false, true);
        if (type == null )
        {
          type = migration(grouping.Key);
          if (type == null )
          {
            this.AddErrorLog(LocalizedStrings.Str2899Params, grouping.Key );
            using (IEnumerator<HydraTaskInfo> enumerator = grouping.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                HydraTaskInfo current = enumerator.Current;
                _settings.Remove(current);
                                Storage.Delete(current.Id);
              }
              continue;
            }
          }
        }
        Type type1 = AvailableTasks.FirstOrDefault( t => t == type );
        if (type1 == null )
        {
          this.AddWarningLog(LocalizedStrings.Str2900Params, grouping.Key );
        }
        else
        {
          foreach (HydraTaskInfo index in (IEnumerable<HydraTaskInfo>) grouping)
          {
            try
            {
              IHydraTask instance = type1.CreateInstance<IHydraTask>();
              instance.Parent = this;
              instance.Init(index.Id);
              CachedSynchronizedSet<HydraTaskSecurity> cachedSynchronizedSet = new CachedSynchronizedSet<HydraTaskSecurity>();
              cachedSynchronizedSet.AddRange(dictionary[index]);
              if (cachedSynchronizedSet.Count == 0)
              {
                HydraTaskSecurity all = CreateAll(instance);
                cachedSynchronizedSet.Add(all);
                                Storage.Add(index.Id, new HydraTaskSecurity[1]
                {
                  all
                } );
              }
              instance.Securities = cachedSynchronizedSet.Cache;
              instance.Load(index.Settings);
              _taskSecurities.Add(instance, cachedSynchronizedSet);
              Action<IHydraTask> taskAdded = TaskAdded;
              if (taskAdded != null)
                taskAdded(instance);
            }
            catch (Exception ex)
            {
              ex.LogError( null );
            }
          }
        }
      }
    }

    /// <summary>Create task instances for the specified types.</summary>
    /// <param name="taskTypes">Task types.</param>
    /// <returns>Tasks.</returns>
    public IEnumerable<IHydraTask> Create(IEnumerable<Type> taskTypes)
    {
      if (taskTypes == null)
        throw new ArgumentNullException(nameof (taskTypes));
      List<IHydraTask> hydraTaskList = new List<IHydraTask>();
      foreach (Type taskType in taskTypes)
      {
        try
        {
          IHydraTask instance = taskType.CreateInstance<IHydraTask>();
          instance.Parent = this;
          HydraTaskSecurity all = CreateAll(instance);
          instance.Init(Guid.NewGuid());
          instance.Securities =  ( new HydraTaskSecurity[1]
          {
            all
          } );
          CachedSynchronizedSet<HydraTaskSecurity> cachedSynchronizedSet1 = new CachedSynchronizedSet<HydraTaskSecurity>();
          cachedSynchronizedSet1.Add(all);
          CachedSynchronizedSet<HydraTaskSecurity> cachedSynchronizedSet2 = cachedSynchronizedSet1;
          Save(instance);
                    Storage.Add(instance.Id, instance.Securities.ToArray() );
          _taskSecurities.Add(instance, cachedSynchronizedSet2);
          Action<IHydraTask> taskAdded = TaskAdded;
          if (taskAdded != null)
            taskAdded(instance);
          hydraTaskList.Add(instance);
        }
        catch (Exception ex)
        {
          ex.LogError( null );
        }
      }
      return hydraTaskList;
    }

    /// <summary>Add or update the task.</summary>
    /// <param name="task">Task.</param>
    public void Save(IHydraTask task)
    {
      if (task == null)
        throw new ArgumentNullException(nameof (task));
            Storage.Save(new HydraTaskInfo(task.Id, task.GetType().GetTypeName(false), task.Save()));
    }

    /// <summary>Delete the task.</summary>
    /// <param name="task">Task.</param>
    public void Delete(IHydraTask task)
    {
      if (task == null)
        throw new ArgumentNullException(nameof (task));
      Tasks.ForEach( t =>
      {
          if ( t.DependFrom != task )
              return;
          t.DependFrom = null;
      } );
            Storage.Delete(task.Id);
      _taskSecurities.Remove(task);
      BaseUserConfig<StudioUserConfig>.Instance.LogConfig.Manager.Sources.Remove( task );
      Action<IHydraTask> taskRemoved = TaskRemoved;
      if (taskRemoved == null)
        return;
      taskRemoved(task);
    }

    /// <summary>Add or update the specified security into the task.</summary>
    /// <param name="task">Task.</param>
    /// <param name="security">Security.</param>
    public void Save(IHydraTask task, HydraTaskSecurity security)
    {
      if (security == null)
        throw new ArgumentNullException(nameof (security));
      Save(task, new HydraTaskSecurity[1]{ security });
    }

    /// <summary>Add or update the specified security into the task.</summary>
    /// <param name="task">Task.</param>
    /// <param name="securities">Securities.</param>
    public void Save(IHydraTask task, HydraTaskSecurity[] securities)
    {
      if (task == null)
        throw new ArgumentNullException(nameof (task));
      if (securities.Length == 0)
        return;
      HashSet<HydraTaskSecurity> hydraTaskSecuritySet1 = new HashSet<HydraTaskSecurity>();
      HydraTaskSecurity[] hydraTaskSecurityArray = null;
      CachedSynchronizedSet<HydraTaskSecurity> taskSecurity = _taskSecurities[task];
      bool flag = taskSecurity.Count == 1 && taskSecurity.Cache[0].IsAllSecurity();
      lock (taskSecurity.SyncRoot)
      {
        if (securities.Length == 1 && securities[0].IsAllSecurity())
        {
          if (flag)
          {
                        Storage.Update(task.Id, securities );
          }
          else
          {
            hydraTaskSecurityArray = taskSecurity.Cache;
            taskSecurity.Clear();
                        Storage.DeleteAll(task.Id);
            hydraTaskSecuritySet1.Add(AddAllSecurity(task, taskSecurity ) );
          }
        }
        else
        {
          if (flag)
          {
            hydraTaskSecurityArray = taskSecurity.Cache;
            taskSecurity.Clear();
          }
          HashSet<HydraTaskSecurity> hydraTaskSecuritySet2 = new HashSet<HydraTaskSecurity>();
          foreach (HydraTaskSecurity security in securities)
          {
            if (taskSecurity.TryAdd(security))
              hydraTaskSecuritySet1.Add(security);
            else
              hydraTaskSecuritySet2.Add(security);
          }
          task.Securities = taskSecurity.Cache;
          if (hydraTaskSecurityArray != null)
                        Storage.Delete(task.Id, hydraTaskSecurityArray );
          if (hydraTaskSecuritySet1.Count > 0)
                        Storage.Add(task.Id, hydraTaskSecuritySet1 );
          if (hydraTaskSecuritySet2.Count > 0)
                        Storage.Update(task.Id, hydraTaskSecuritySet2 );
        }
      }
      if (hydraTaskSecurityArray != null)
      {
        Action<IHydraTask, IEnumerable<HydraTaskSecurity>, bool> securitiesRemoved = SecuritiesRemoved;
        if (securitiesRemoved != null)
          securitiesRemoved(task, hydraTaskSecurityArray, true);
      }
      Action<IHydraTask, IEnumerable<HydraTaskSecurity>> securitiesAdded = SecuritiesAdded;
      if (securitiesAdded == null)
        return;
      securitiesAdded(task, hydraTaskSecuritySet1 );
    }

    /// <summary>Remove the specified security from the task.</summary>
    /// <param name="task">Task.</param>
    /// <param name="security">Security.</param>
    public void Delete(IHydraTask task, HydraTaskSecurity security)
    {
      if (security == null)
        throw new ArgumentNullException(nameof (security));
      Delete(task, new HydraTaskSecurity[1]{ security });
    }

    /// <summary>Remove the specified securities from the task.</summary>
    /// <param name="task">Task.</param>
    /// <param name="securities">Securities.</param>
    public void Delete(IHydraTask task, HydraTaskSecurity[] securities)
    {
      if (task == null)
        throw new ArgumentNullException(nameof (task));
      if (securities.Length == 0)
        return;
      CachedSynchronizedSet<HydraTaskSecurity> taskSecurity = _taskSecurities[task];
      HydraTaskSecurity hydraTaskSecurity;
      lock (taskSecurity.SyncRoot)
      {
        taskSecurity.RemoveRange( securities );
        task.Securities = taskSecurity.Cache;
                Storage.Delete(task.Id, securities );
        hydraTaskSecurity = TryAddAllSecurity(task, taskSecurity );
      }
      Action<IHydraTask, IEnumerable<HydraTaskSecurity>, bool> securitiesRemoved = SecuritiesRemoved;
      if (securitiesRemoved != null)
        securitiesRemoved(task, securities, false);
      if (hydraTaskSecurity == null)
        return;
      Action<IHydraTask, IEnumerable<HydraTaskSecurity>> securitiesAdded = SecuritiesAdded;
      if (securitiesAdded == null)
        return;
      securitiesAdded(task, new HydraTaskSecurity[1]
      {
        hydraTaskSecurity
      } );
    }

    /// <summary>Remove all securities from the task.</summary>
    /// <param name="task">Task.</param>
    public void DeleteAll(IHydraTask task)
    {
      if (task == null)
        throw new ArgumentNullException(nameof (task));
      CachedSynchronizedSet<HydraTaskSecurity> taskSecurity = _taskSecurities[task];
      HydraTaskSecurity[] cache;
      HydraTaskSecurity hydraTaskSecurity;
      lock (taskSecurity.SyncRoot)
      {
        cache = taskSecurity.Cache;
        taskSecurity.Clear();
        task.Securities = taskSecurity.Cache;
                Storage.DeleteAll(task.Id);
        hydraTaskSecurity = AddAllSecurity(task, taskSecurity );
      }
      Action<IHydraTask, IEnumerable<HydraTaskSecurity>, bool> securitiesRemoved = SecuritiesRemoved;
      if (securitiesRemoved != null)
        securitiesRemoved(task, cache, true);
      Action<IHydraTask, IEnumerable<HydraTaskSecurity>> securitiesAdded = SecuritiesAdded;
      if (securitiesAdded == null)
        return;
      securitiesAdded(task, new HydraTaskSecurity[1]
      {
        hydraTaskSecurity
      } );
    }

    private HydraTaskSecurity TryAddAllSecurity(
      IHydraTask task,
      ISet<HydraTaskSecurity> taskSecurities)
    {
      if (taskSecurities.Count != 0)
        return null;
      return AddAllSecurity(task, taskSecurities);
    }

    private HydraTaskSecurity AddAllSecurity(
      IHydraTask task,
      ISet<HydraTaskSecurity> taskSecurities)
    {
      HydraTaskSecurity all = CreateAll(task);
      taskSecurities.Add(all);
      task.Securities =  ( new HydraTaskSecurity[1]
      {
        all
      } );
            Storage.Add(task.Id, task.Securities.ToArray() );
      return all;
    }

    /// <summary>Remove security.</summary>
    /// <param name="security">Security.</param>
    public void Delete(Security security)
    {
      if (security == null)
        throw new ArgumentNullException(nameof (security));
      Delete(new Security[1]{ security });
    }

    /// <summary>Remove securities.</summary>
    /// <param name="securities">Securities.</param>
    public void Delete(Security[] securities)
    {
      if (securities == null)
        throw new ArgumentNullException(nameof (securities));
      if (securities.Length == 0)
        return;
      securities = securities.Where( s => !s.IsAllSecurity() ).ToArray();
      IEntityRegistry entityRegistry = ServicesRegistry.EntityRegistry;
      ISecurityStorage securityStorage = ServicesRegistry.SecurityStorage;
      foreach (IHydraTask task in Tasks)
      {
        CachedSynchronizedSet<HydraTaskSecurity> taskSecurities = _taskSecurities[task];
        HydraTaskSecurity[] array;
        lock (taskSecurities.SyncRoot)
          array = securities.Select( s => taskSecurities.FirstOrDefault( s1 => s1.Security == s ) ).Where( s => s != null ).ToArray();
        if (array.Length != 0)
          Delete(task, array);
      }
      securityStorage.DeleteRange( securities );
      entityRegistry.WaitSecuritiesFlush();
    }

    /// <summary>Reset data.</summary>
    public void Reset()
    {
      AvailableTasks = Array.Empty<Type>();
      _taskSecurities.Clear();
      _settings.Clear();
            Storage.Reset();
    }
  }
}
