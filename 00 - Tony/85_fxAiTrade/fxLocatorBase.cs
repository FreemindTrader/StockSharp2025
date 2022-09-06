using fx.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FreemindAITrade
{
    public abstract class fxLocatorBase
    {
        static Assembly entryAssembly;
        protected static Assembly EntryAssembly
        {
            get
            {
                if ( entryAssembly == null )
                {
#if NETFX_CORE
					entryAssembly = Assembly.GetEntryAssembly();
#else
                    entryAssembly = Assembly.GetEntryAssembly( );
#endif
                }
                return entryAssembly;
            }
            set { entryAssembly = value; }
        }
        protected abstract IEnumerable<Assembly> Assemblies { get; }

        PooledDictionary<string, Type> _registeredTypes        = new PooledDictionary<string, Type>();
        PooledDictionary<string, Type> _shortNameToTypeMapping = new PooledDictionary<string, Type>();
        PooledDictionary<string, Type> _fullNameToTypeMapping  = new PooledDictionary<string, Type>();
        
        IEnumerator<Type> enumerator;

        public void RegisterType( string name, Type type )
        {
            _registeredTypes.Add( name, type );
        }

        protected Type ResolveType( string name, out IDictionary<string, string> properties )
        {
            ResolveTypeProperties( ref name, out properties );
            if ( string.IsNullOrEmpty( name ) )
                return null;
            Type type;
            if ( _registeredTypes.TryGetValue( name, out type ) )
                return type;
            if ( _shortNameToTypeMapping.TryGetValue( name, out type ) || _fullNameToTypeMapping.TryGetValue( name, out type ) )
                return type;
            if ( enumerator == null ) enumerator = GetTypes( );
            while ( enumerator.MoveNext( ) )
            {
                if ( !_fullNameToTypeMapping.ContainsKey( enumerator.Current.FullName ) )
                {
                    _shortNameToTypeMapping[ enumerator.Current.Name ] = enumerator.Current;
                    _fullNameToTypeMapping[ enumerator.Current.FullName ] = enumerator.Current;
                }
                if ( enumerator.Current.Name == name || enumerator.Current.FullName == name )
                    return enumerator.Current;
            }
            return null;
        }
        protected string ResolveTypeName( Type type, IDictionary<string, string> properties )
        {
            if ( type == null ) return null;
            var props = CreateTypeProperties(properties);
            return props + type.FullName;
        }
        protected virtual IEnumerator<Type> GetTypes( )
        {
            foreach ( Assembly asm in Assemblies )
            {
                Type[] types = new Type[] { };
                try
                {
#if !NETFX_CORE
                    types = asm.GetTypes( );
#else
					types = asm.GetTypes();
#endif
                }
                catch ( ReflectionTypeLoadException e )
                {
                    types = e.Types;
                }
                foreach ( var type in types )
                {
                    if ( type != null )
                        yield return type;
                }
            }
        }
        protected static void ResolveTypeProperties( ref string name, out IDictionary<string, string> properties )
        {
            Func<string, string> getPropName = x =>
            {
                var ind = x.IndexOf('=');
                return ind == -1 ? null : x.Substring(0, ind);
            };
            Func<string, string> getPropValue = x =>
            {
                var ind1 = x.IndexOf('=');
                var ind2 = x.IndexOf(';');
                if(ind1 == -1 || ind2 == -1) return null;
                return x.Substring(ind1 + 1, ind2 - ind1 - 1);
            };
            Func<string, string> removeProp = x =>
            {
                var ind = x.IndexOf(';');
                return x.Remove(0, ind + 1);
            };
            properties = new PooledDictionary<string, string>( );
            if ( string.IsNullOrEmpty( name ) ) return;
            while ( true )
            {
                var propName = getPropName(name);
                var propValue = getPropValue(name);
                if ( propName == null || propValue == null ) break;
                properties.Add( propName, propValue );
                name = removeProp( name );
            }
        }
        protected static string CreateTypeProperties( IDictionary<string, string> properties )
        {
            if ( properties == null ) return null;
            string res = string.Empty;
            foreach ( var item in properties )
            {
                res += item.Key;
                res += "=";
                res += item.Value;
                res += ";";
            }
            return res;
        }
        protected virtual object CreateInstance( Type type, string typeName )
        {
            if ( type == null ) return null;
            object res = null;
            try
            {
#if NETFX_CORE
				var ctors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
				var parameterlessCtor = ctors.FirstOrDefault(x => x.GetParameters().Count() == 0);
				if(parameterlessCtor != null)
					res = parameterlessCtor.Invoke(null);
				if(res == null) {
					var optionalParametersCtor = ctors.FirstOrDefault(x => x.GetParameters().All(p => p.IsOptional));
					if(optionalParametersCtor != null)
						res = optionalParametersCtor.Invoke(optionalParametersCtor.GetParameters().Select(x => Type.Missing).ToArray());
				}
#else
                var parameterlessCtor = type.GetConstructor(
                    BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance,
                    null, new Type[] { }, null);
                if ( parameterlessCtor != null )
                    res = parameterlessCtor.Invoke( null );
                if ( res == null )
                {
                    res = Activator.CreateInstance( type,
                        BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance | BindingFlags.OptionalParamBinding,
                        null, null, null );
                }
#endif
            }
            catch ( Exception e )
            {
                throw new LocatorException( GetType( ).Name, typeName, e );
            }
            if ( res == null ) throw new LocatorException( GetType( ).Name, typeName, null );
            return res;
        }
    }
    public class LocatorException : Exception
    {
        public LocatorException( string locatorName, string type, Exception innerException )
            : base( string.Format( "{0} cannot resolve the {1} type. See the inner exception for details.", locatorName, type ),
                  innerException )
        { }
    }
}

