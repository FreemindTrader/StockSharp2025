// Decompiled with JetBrains decompiler
// Type: FreemindTrader.Properties.Resources
// Assembly: Terminal, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C7FF2C7B-469F-4E71-BC76-9E79C0E574D9
// Assembly location: T:\00-StockSharp\Terminal\Terminal.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FreemindTrader.Properties
{
    [GeneratedCode( "System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0" )]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable( EditorBrowsableState.Advanced )]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if ( FreemindTrader.Properties.Resources.resourceMan == null )
                    FreemindTrader.Properties.Resources.resourceMan = new ResourceManager( "FreemindTrader.Properties.Resources", typeof( FreemindTrader.Properties.Resources ).Assembly );
                return FreemindTrader.Properties.Resources.resourceMan;
            }
        }

        [EditorBrowsable( EditorBrowsableState.Advanced )]
        internal static CultureInfo Culture
        {
            get
            {
                return FreemindTrader.Properties.Resources.resourceCulture;
            }
            set
            {
                FreemindTrader.Properties.Resources.resourceCulture = value;
            }
        }

        internal static string DefaultAreaLayout
        {
            get
            {
                return FreemindTrader.Properties.Resources.ResourceManager.GetString( nameof( DefaultAreaLayout ), FreemindTrader.Properties.Resources.resourceCulture );
            }
        }

        internal static string DefaultLayout
        {
            get
            {
                return FreemindTrader.Properties.Resources.ResourceManager.GetString( nameof( DefaultLayout ), FreemindTrader.Properties.Resources.resourceCulture );
            }
        }
    }
}
