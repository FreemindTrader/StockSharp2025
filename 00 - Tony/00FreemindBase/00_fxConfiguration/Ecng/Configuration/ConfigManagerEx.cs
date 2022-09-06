using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace fx.Configuration
{
    public static class ConfigManagerEx
    {
        private static readonly Dictionary<Type, ConfigurationSection> _sections = new Dictionary<Type, ConfigurationSection>();
        private static readonly Dictionary<Type, ConfigurationSectionGroup> _sectionGroups = new Dictionary<Type, ConfigurationSectionGroup>();
        private static readonly SyncObject _sync = new SyncObject();
        private static readonly Dictionary<Type, Dictionary<string, object>> _services = new Dictionary<Type, Dictionary<string, object>>();
        private static readonly Dictionary<Type, List<Action<object>>> _subscribers = new Dictionary<Type, List<Action<object>>>();

        static ConfigManagerEx()
        {
            try
            {
                ConfigManagerEx.InnerConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            catch (Exception ex)
            {
                Trace.WriteLine((object)ex);
                Console.WriteLine((object)ex);
            }
            if (ConfigManagerEx.InnerConfig == null)
                return;
            Trace.WriteLine("ConfigManager FilePath=" + ConfigManagerEx.InnerConfig.FilePath);
            InitSections(ConfigManagerEx.InnerConfig.Sections);
            InitSectionGroups(ConfigManagerEx.InnerConfig.SectionGroups);

            static void InitSections(ConfigurationSectionCollection sections)
            {
                try
                {
                    foreach (ConfigurationSection section in (NameObjectCollectionBase)sections)
                    {
                        if (!ConfigManagerEx._sections.ContainsKey(section.GetType()))
                            ConfigManagerEx._sections.Add(section.GetType(), section);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine((object)ex);
                }
            }

            static void InitSectionGroups(ConfigurationSectionGroupCollection groups)
            {
                foreach (ConfigurationSectionGroup group in (NameObjectCollectionBase)groups)
                {
                    if (!ConfigManagerEx._sectionGroups.ContainsKey(group.GetType()))
                        ConfigManagerEx._sectionGroups.Add(group.GetType(), group);
                    InitSections(group.Sections);
                    InitSectionGroups(group.SectionGroups);
                }
            }
        }

        public static System.Configuration.Configuration InnerConfig { get; }

        public static T GetSection<T>() where T : ConfigurationSection => (T)ConfigManagerEx.GetSection(typeof(T));

        public static ConfigurationSection GetSection(Type sectionType) => !ConfigManagerEx._sections.ContainsKey(sectionType) ? (ConfigurationSection)null : ConfigManagerEx._sections[sectionType];

        public static T GetSection<T>(string sectionName) where T : ConfigurationSection => (T)ConfigManagerEx.GetSection(sectionName);

        public static ConfigurationSection GetSection(string sectionName) => ConfigManagerEx.InnerConfig.GetSection(sectionName);

        public static T GetSectionByType<T>() where T : ConfigurationSection => (T)ConfigManagerEx.GetSectionByType(typeof(T));

        public static ConfigurationSection GetSectionByType(Type type) => ConfigManagerEx.GetSection((type.GetAttribute<ConfigSectionAttribute>() ?? throw new ArgumentException("Type '{0}' isn't marked ConfigSectionAttribute.".Put((object)type))).SectionType);

        public static T GetGroup<T>() where T : ConfigurationSectionGroup => (T)ConfigManagerEx.GetGroup(typeof(T));

        public static ConfigurationSectionGroup GetGroup(Type sectionGroupType) => !ConfigManagerEx._sectionGroups.ContainsKey(sectionGroupType) ? (ConfigurationSectionGroup)null : ConfigManagerEx._sectionGroups[sectionGroupType];

        public static T GetGroup<T>(string sectionName) where T : ConfigurationSectionGroup => (T)ConfigManagerEx.GetGroup(sectionName);

        public static ConfigurationSectionGroup GetGroup(string sectionName) => ConfigManagerEx.InnerConfig.GetSectionGroup(sectionName);

        public static T TryGet<T>(string name, T defaultValue = default)
        {
            try
            {
                string str = ConfigManagerEx.AppSettings.Get(name);
                if (!str.IsEmpty())
                    return str.To<T>();
            }
            catch (Exception ex)
            {
                Trace.WriteLine((object)ex);
            }
            return defaultValue;
        }

        public static NameValueCollection AppSettings => ConfigurationManager.AppSettings;

        public static event Action<Type, object> ServiceRegistered;

        public static void SubscribeOnRegister<T>(Action<T> registered)
        {
            if (registered == null)
                throw new ArgumentNullException(nameof(registered));
            List<Action<object>> actionList;
            if (!ConfigManagerEx._subscribers.TryGetValue(typeof(T), out actionList))
            {
                actionList = new List<Action<object>>();
                ConfigManagerEx._subscribers.Add(typeof(T), actionList);
            }
            actionList.Add((Action<object>)(svc => registered((T)svc)));
        }

        private static void RaiseServiceRegistered(Type type, object service)
        {
            Action<Type, object> serviceRegistered = ConfigManagerEx.ServiceRegistered;
            if (serviceRegistered != null)
                serviceRegistered(type, service);
            List<Action<object>> actionList;
            if (!ConfigManagerEx._subscribers.TryGetValue(type, out actionList))
                return;
            foreach (Action<object> action in actionList)
                action(service);
        }

        private static Dictionary<string, object> GetDict<T>() => ConfigManagerEx.GetDict(typeof(T));

        private static Dictionary<string, object> GetDict(Type type)
        {
            Dictionary<string, object> dictionary1;
            if (ConfigManagerEx._services.TryGetValue(type, out dictionary1))
                return dictionary1;
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase);
            ConfigManagerEx._services.Add(type, dictionary2);
            return dictionary2;
        }

        public static void RegisterService<T>(T service) => ConfigManagerEx.RegisterService<T>(typeof(T).AssemblyQualifiedName, service);

        public static void RegisterService<T>(string name, T service)
        {
            lock (ConfigManagerEx._sync)
                ConfigManagerEx.GetDict<T>()[name] = (object)service;
            ConfigManagerEx.RaiseServiceRegistered(typeof(T), (object)service);
        }

        public static bool IsServiceRegistered<T>() => ConfigManagerEx.IsServiceRegistered<T>(typeof(T).AssemblyQualifiedName);

        public static bool IsServiceRegistered<T>(string name)
        {
            lock (ConfigManagerEx._sync)
                return ConfigManagerEx.GetDict<T>().ContainsKey(name);
        }

        public static T TryGetService<T>() => !ConfigManagerEx.IsServiceRegistered<T>() ? default(T) : ConfigManagerEx.GetService<T>();

        public static void TryRegisterService<T>(T service)
        {
            if (ConfigManagerEx.IsServiceRegistered<T>())
                return;
            ConfigManagerEx.RegisterService<T>(service);
        }

        public static T GetService<T>() => ConfigManagerEx.GetService<T>(typeof(T).AssemblyQualifiedName);

        public static T GetService<T>(string name)
        {
            object obj = (object)null;
            lock (ConfigManagerEx._sync)
            {
                Dictionary<string, object> dict = ConfigManagerEx.GetDict<T>();
                if (dict.TryGetValue(name, out obj))
                    return (T)obj;
                if (obj != null)
                {
                    if (!dict.ContainsKey(name))
                        dict.Add(name, obj);
                }
            }
            return (T)obj;
        }

        public static IEnumerable<T> GetServices<T>()
        {
            IEnumerable<T> array;
            lock (ConfigManagerEx._sync)
                array = (IEnumerable<T>)ConfigManagerEx.GetDict<T>().Values.Cast<T>().ToArray<T>();
            return array.Distinct<T>();
        }
    }
}
