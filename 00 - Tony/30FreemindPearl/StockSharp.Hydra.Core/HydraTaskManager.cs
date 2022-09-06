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
        return (IEnumerable<IHydraTask>) this._taskSecurities.CachedKeys;
      }
    }

    /// <summary>All available tasks.</summary>
    public Type[] AvailableTasks { get; private set; }

    /// <summary>All created settings.</summary>
    public IEnumerable<HydraTaskInfo> Settings
    {
      get
      {
        return (IEnumerable<HydraTaskInfo>) this._settings.Cache;
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
      DataType[] array = task.SupportedDataTypes.Where<DataType>((Func<DataType, bool>) (t => t.IsMarketData)).ToArray<DataType>();
      if (array.IsEmpty<DataType>())
        array = task.SupportedDataTypes.ToArray<DataType>();
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
      typeList.AddRange(adapters.Select<Type, Type>((Func<Type, Type>) (a => typeof (ConnectorHydraTask<>).Make(a))));
      Assembly[] array = ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).ToArray<Assembly>();
      if (Directory.Exists("Plugins"))
      {
        foreach (string str in ((IEnumerable<string>) Directory.GetFiles("Plugins", "*.dll")).Where<string>((Func<string, bool>) (p => Path.GetFileNameWithoutExtension(p).StartsWithIgnoreCase("StockSharp.Hydra."))))
        {
          if (!str.IsAssembly())
          {
            this.AddWarningLog(LocalizedStrings.Str2897Params, (object) str);
          }
          else
          {
            AssemblyName asmName = AssemblyName.GetAssemblyName(str);
            if (((IEnumerable<Assembly>) array).FirstOrDefault<Assembly>((Func<Assembly, bool>) (a => asmName.Name.EqualsIgnoreCase(a.GetName().Name))) != (Assembly) null)
            {
              this.AddWarningLog("{0} already loaded. ignoring.", (object) str);
            }
            else
            {
              try
              {
                Assembly assembly = Assembly.LoadFrom(str);
                this.AddDebugLog("loaded plugin {0}", (object) str);
                typeList.AddRange((IEnumerable<Type>) ((IEnumerable<Type>) assembly.GetTypes()).Where<Type>((Func<Type, bool>) (t =>
                {
                  if (typeof (IHydraTask).IsAssignableFrom(t))
                    return !t.IsAbstract;
                  return false;
                })).ToArray<Type>());
              }
              catch (Exception ex)
              {
                ex.LogError((string) null);
              }
            }
          }
        }
      }
      this.AvailableTasks = typeList.ToArray();
      ContinueOnExceptionContext exceptionContext = new ContinueOnExceptionContext();
      exceptionContext.Error += (Action<Exception>) (ex => ex.LogError((string) null));
      IDictionary<HydraTaskInfo, IEnumerable<HydraTaskSecurity>> dictionary = HydraTaskManager.Storage.Load((ILogReceiver) this);
      using (exceptionContext.ToScope<ContinueOnExceptionContext>(true))
        this._settings.AddRange((IEnumerable<HydraTaskInfo>) dictionary.Keys);
      foreach (IGrouping<string, HydraTaskInfo> grouping in this.Settings.GroupBy<HydraTaskInfo, string>((Func<HydraTaskInfo, string>) (set => set.TaskType)))
      {
        Type type = Type.GetType(grouping.Key, false, true);
        if (type == (Type) null)
        {
          type = migration(grouping.Key);
          if (type == (Type) null)
          {
            this.AddErrorLog(LocalizedStrings.Str2899Params, (object) grouping.Key);
            using (IEnumerator<HydraTaskInfo> enumerator = grouping.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                HydraTaskInfo current = enumerator.Current;
                this._settings.Remove(current);
                HydraTaskManager.Storage.Delete(current.Id);
              }
              continue;
            }
          }
        }
        Type type1 = ((IEnumerable<Type>) this.AvailableTasks).FirstOrDefault<Type>((Func<Type, bool>) (t => t == type));
        if (type1 == (Type) null)
        {
          this.AddWarningLog(LocalizedStrings.Str2900Params, (object) grouping.Key);
        }
        else
        {
          foreach (HydraTaskInfo index in (IEnumerable<HydraTaskInfo>) grouping)
          {
            try
            {
              IHydraTask instance = type1.CreateInstance<IHydraTask>();
              instance.Parent = (ILogSource) this;
              instance.Init(index.Id);
              CachedSynchronizedSet<HydraTaskSecurity> cachedSynchronizedSet = new CachedSynchronizedSet<HydraTaskSecurity>();
              cachedSynchronizedSet.AddRange(dictionary[index]);
              if (cachedSynchronizedSet.Count == 0)
              {
                HydraTaskSecurity all = this.CreateAll(instance);
                cachedSynchronizedSet.Add(all);
                HydraTaskManager.Storage.Add(index.Id, (IEnumerable<HydraTaskSecurity>) new HydraTaskSecurity[1]
                {
                  all
                });
              }
              instance.Securities = (IEnumerable<HydraTaskSecurity>) cachedSynchronizedSet.Cache;
              instance.Load(index.Settings);
              this._taskSecurities.Add(instance, cachedSynchronizedSet);
              Action<IHydraTask> taskAdded = this.TaskAdded;
              if (taskAdded != null)
                taskAdded(instance);
            }
            catch (Exception ex)
            {
              ex.LogError((string) null);
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
          instance.Parent = (ILogSource) this;
          HydraTaskSecurity all = this.CreateAll(instance);
          instance.Init(Guid.NewGuid());
          instance.Securities = (IEnumerable<HydraTaskSecurity>) new HydraTaskSecurity[1]
          {
            all
          };
          CachedSynchronizedSet<HydraTaskSecurity> cachedSynchronizedSet1 = new CachedSynchronizedSet<HydraTaskSecurity>();
          cachedSynchronizedSet1.Add(all);
          CachedSynchronizedSet<HydraTaskSecurity> cachedSynchronizedSet2 = cachedSynchronizedSet1;
          this.Save(instance);
          HydraTaskManager.Storage.Add(instance.Id, (IEnumerable<HydraTaskSecurity>) instance.Securities.ToArray<HydraTaskSecurity>());
          this._taskSecurities.Add(instance, cachedSynchronizedSet2);
          Action<IHydraTask> taskAdded = this.TaskAdded;
          if (taskAdded != null)
            taskAdded(instance);
          hydraTaskList.Add(instance);
        }
        catch (Exception ex)
        {
          ex.LogError((string) null);
        }
      }
      return (IEnumerable<IHydraTask>) hydraTaskList;
    }

    /// <summary>Add or update the task.</summary>
    /// <param name="task">Task.</param>
    public void Save(IHydraTask task)
    {
      if (task == null)
        throw new ArgumentNullException(nameof (task));
      HydraTaskManager.Storage.Save(new HydraTaskInfo(task.Id, task.GetType().GetTypeName(false), task.Save()));
    }

    /// <summary>Delete the task.</summary>
    /// <param name="task">Task.</param>
    public void Delete(IHydraTask task)
    {
      if (task == null)
        throw new ArgumentNullException(nameof (task));
      this.Tasks.ForEach<IHydraTask>((Action<IHydraTask>) (t =>
      {
        if (t.DependFrom != task)
          return;
        t.DependFrom = (IHydraTask) null;
      }));
      HydraTaskManager.Storage.Delete(task.Id);
      this._taskSecurities.Remove(task);
      BaseUserConfig<StudioUserConfig>.Instance.LogConfig.Manager.Sources.Remove((ILogSource) task);
      Action<IHydraTask> taskRemoved = this.TaskRemoved;
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
      this.Save(task, new HydraTaskSecurity[1]{ security });
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
      HydraTaskSecurity[] hydraTaskSecurityArray = (HydraTaskSecurity[]) null;
      CachedSynchronizedSet<HydraTaskSecurity> taskSecurity = this._taskSecurities[task];
      bool flag = taskSecurity.Count == 1 && taskSecurity.Cache[0].IsAllSecurity();
      lock (taskSecurity.SyncRoot)
      {
        if (securities.Length == 1 && securities[0].IsAllSecurity())
        {
          if (flag)
          {
            HydraTaskManager.Storage.Update(task.Id, (IEnumerable<HydraTaskSecurity>) securities);
          }
          else
          {
            hydraTaskSecurityArray = taskSecurity.Cache;
            taskSecurity.Clear();
            HydraTaskManager.Storage.DeleteAll(task.Id);
            hydraTaskSecuritySet1.Add(this.AddAllSecurity(task, (ISet<HydraTaskSecurity>) taskSecurity));
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
          task.Securities = (IEnumerable<HydraTaskSecurity>) taskSecurity.Cache;
          if (hydraTaskSecurityArray != null)
            HydraTaskManager.Storage.Delete(task.Id, (IEnumerable<HydraTaskSecurity>) hydraTaskSecurityArray);
          if (hydraTaskSecuritySet1.Count > 0)
            HydraTaskManager.Storage.Add(task.Id, (IEnumerable<HydraTaskSecurity>) hydraTaskSecuritySet1);
          if (hydraTaskSecuritySet2.Count > 0)
            HydraTaskManager.Storage.Update(task.Id, (IEnumerable<HydraTaskSecurity>) hydraTaskSecuritySet2);
        }
      }
      if (hydraTaskSecurityArray != null)
      {
        Action<IHydraTask, IEnumerable<HydraTaskSecurity>, bool> securitiesRemoved = this.SecuritiesRemoved;
        if (securitiesRemoved != null)
          securitiesRemoved(task, (IEnumerable<HydraTaskSecurity>) hydraTaskSecurityArray, true);
      }
      Action<IHydraTask, IEnumerable<HydraTaskSecurity>> securitiesAdded = this.SecuritiesAdded;
      if (securitiesAdded == null)
        return;
      securitiesAdded(task, (IEnumerable<HydraTaskSecurity>) hydraTaskSecuritySet1);
    }

    /// <summary>Remove the specified security from the task.</summary>
    /// <param name="task">Task.</param>
    /// <param name="security">Security.</param>
    public void Delete(IHydraTask task, HydraTaskSecurity security)
    {
      if (security == null)
        throw new ArgumentNullException(nameof (security));
      this.Delete(task, new HydraTaskSecurity[1]{ security });
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
      CachedSynchronizedSet<HydraTaskSecurity> taskSecurity = this._taskSecurities[task];
      HydraTaskSecurity hydraTaskSecurity;
      lock (taskSecurity.SyncRoot)
      {
        taskSecurity.RemoveRange((IEnumerable<HydraTaskSecurity>) securities);
        task.Securities = (IEnumerable<HydraTaskSecurity>) taskSecurity.Cache;
        HydraTaskManager.Storage.Delete(task.Id, (IEnumerable<HydraTaskSecurity>) securities);
        hydraTaskSecurity = this.TryAddAllSecurity(task, (ISet<HydraTaskSecurity>) taskSecurity);
      }
      Action<IHydraTask, IEnumerable<HydraTaskSecurity>, bool> securitiesRemoved = this.SecuritiesRemoved;
      if (securitiesRemoved != null)
        securitiesRemoved(task, (IEnumerable<HydraTaskSecurity>) securities, false);
      if (hydraTaskSecurity == null)
        return;
      Action<IHydraTask, IEnumerable<HydraTaskSecurity>> securitiesAdded = this.SecuritiesAdded;
      if (securitiesAdded == null)
        return;
      securitiesAdded(task, (IEnumerable<HydraTaskSecurity>) new HydraTaskSecurity[1]
      {
        hydraTaskSecurity
      });
    }

    /// <summary>Remove all securities from the task.</summary>
    /// <param name="task">Task.</param>
    public void DeleteAll(IHydraTask task)
    {
      if (task == null)
        throw new ArgumentNullException(nameof (task));
      CachedSynchronizedSet<HydraTaskSecurity> taskSecurity = this._taskSecurities[task];
      HydraTaskSecurity[] cache;
      HydraTaskSecurity hydraTaskSecurity;
      lock (taskSecurity.SyncRoot)
      {
        cache = taskSecurity.Cache;
        taskSecurity.Clear();
        task.Securities = (IEnumerable<HydraTaskSecurity>) taskSecurity.Cache;
        HydraTaskManager.Storage.DeleteAll(task.Id);
        hydraTaskSecurity = this.AddAllSecurity(task, (ISet<HydraTaskSecurity>) taskSecurity);
      }
      Action<IHydraTask, IEnumerable<HydraTaskSecurity>, bool> securitiesRemoved = this.SecuritiesRemoved;
      if (securitiesRemoved != null)
        securitiesRemoved(task, (IEnumerable<HydraTaskSecurity>) cache, true);
      Action<IHydraTask, IEnumerable<HydraTaskSecurity>> securitiesAdded = this.SecuritiesAdded;
      if (securitiesAdded == null)
        return;
      securitiesAdded(task, (IEnumerable<HydraTaskSecurity>) new HydraTaskSecurity[1]
      {
        hydraTaskSecurity
      });
    }

    private HydraTaskSecurity TryAddAllSecurity(
      IHydraTask task,
      ISet<HydraTaskSecurity> taskSecurities)
    {
      if (taskSecurities.Count != 0)
        return (HydraTaskSecurity) null;
      return this.AddAllSecurity(task, taskSecurities);
    }

    private HydraTaskSecurity AddAllSecurity(
      IHydraTask task,
      ISet<HydraTaskSecurity> taskSecurities)
    {
      HydraTaskSecurity all = this.CreateAll(task);
      taskSecurities.Add(all);
      task.Securities = (IEnumerable<HydraTaskSecurity>) new HydraTaskSecurity[1]
      {
        all
      };
      HydraTaskManager.Storage.Add(task.Id, (IEnumerable<HydraTaskSecurity>) task.Securities.ToArray<HydraTaskSecurity>());
      return all;
    }

    /// <summary>Remove security.</summary>
    /// <param name="security">Security.</param>
    public void Delete(Security security)
    {
      if (security == null)
        throw new ArgumentNullException(nameof (security));
      this.Delete(new Security[1]{ security });
    }

    /// <summary>Remove securities.</summary>
    /// <param name="securities">Securities.</param>
    public void Delete(Security[] securities)
    {
      if (securities == null)
        throw new ArgumentNullException(nameof (securities));
      if (securities.Length == 0)
        return;
      securities = ((IEnumerable<Security>) securities).Where<Security>((Func<Security, bool>) (s => !s.IsAllSecurity())).ToArray<Security>();
      IEntityRegistry entityRegistry = ServicesRegistry.EntityRegistry;
      ISecurityStorage securityStorage = ServicesRegistry.SecurityStorage;
      foreach (IHydraTask task in this.Tasks)
      {
        CachedSynchronizedSet<HydraTaskSecurity> taskSecurities = this._taskSecurities[task];
        HydraTaskSecurity[] array;
        lock (taskSecurities.SyncRoot)
          array = ((IEnumerable<Security>) securities).Select<Security, HydraTaskSecurity>((Func<Security, HydraTaskSecurity>) (s => taskSecurities.FirstOrDefault<HydraTaskSecurity>((Func<HydraTaskSecurity, bool>) (s1 => s1.Security == s)))).Where<HydraTaskSecurity>((Func<HydraTaskSecurity, bool>) (s => s != null)).ToArray<HydraTaskSecurity>();
        if (array.Length != 0)
          this.Delete(task, array);
      }
      securityStorage.DeleteRange((IEnumerable<Security>) securities);
      entityRegistry.WaitSecuritiesFlush();
    }

    /// <summary>Reset data.</summary>
    public void Reset()
    {
      this.AvailableTasks = Array.Empty<Type>();
      this._taskSecurities.Clear();
      this._settings.Clear();
      HydraTaskManager.Storage.Reset();
    }
  }
}
