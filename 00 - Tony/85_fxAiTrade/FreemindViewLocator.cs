using DevExpress.Mvvm.UI;
using fx.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using static DevExpress.Mvvm.UI.ViewLocatorExtensions;
#if !NETFX_CORE
using DevExpress.Mvvm.POCO;
#else
using Windows.UI.Xaml;
using DevExpress.Mvvm.Native;
using Windows.ApplicationModel;
#endif

namespace FreemindAITrade
{
    public class FreemindViewLocator : fxLocatorBase, IViewLocator
    {
        public static IViewLocator Default { get { return _default ?? Instance; } set { _default = value; } }
        static IViewLocator _default = null;
        internal static readonly IViewLocator Instance = new FreemindViewLocator( Application.Current );
        readonly IEnumerable<Assembly> _assemblies;

        PooledList<Assembly> _withViews = new PooledList<Assembly>();

        protected override IEnumerable<Assembly> Assemblies
        {
            get
            {
                return _withViews;
            }
        }

        public FreemindViewLocator()
        {
            var entry = EntryAssembly != null && !EntryAssembly.IsInDesignMode() ? new[ ] { EntryAssembly } : new Assembly[0];
            var thisA = typeof( FreemindViewLocator ).Assembly;

            _withViews.AddRange( entry );
            _withViews.Add( thisA );
        }

        public FreemindViewLocator( Application application )
#if !NETFX_CORE
            : this( EntryAssembly != null && !EntryAssembly.IsInDesignMode() ? new[ ] { EntryAssembly } : new Assembly[0] )
        {
#else
			: this(EntryAssembly != null && !DesignMode.DesignModeEnabled ? new[] { EntryAssembly } : new Assembly[0]) {
#endif
        }

        public FreemindViewLocator( IEnumerable<Assembly> assemblies )
        {
            _assemblies = assemblies;
        }

        public FreemindViewLocator( params Assembly[ ] assemblies )
            : this( ( IEnumerable<Assembly> )assemblies )
        {
        }

        public Type ResolveViewType( string viewName )
        {
            IDictionary<string, string> properties;
            return ResolveType( viewName, out properties );
        }

        public string GetViewTypeName( Type type )
        {
            return ResolveTypeName( type, null );
        }

        public object ResolveView( string viewName )
        {
            Type viewType = ( ( IViewLocator )this ).ResolveViewType( viewName );
            if ( viewType != null )
                return CreateInstance( viewType, viewName );
            return CreateFallbackView( viewName );
        }

        internal static object CreateFallbackView( string errorText )
        {
            return new FallbackView() { Text = errorText };
        }
    }
}


