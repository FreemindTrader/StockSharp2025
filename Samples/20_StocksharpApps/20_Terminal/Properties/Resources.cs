// Decompiled with JetBrains decompiler
// Type: StockSharp.Terminal.Properties.Resources
// Assembly: Terminal, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 33913FF8-0D5D-4EE9-A5BB-58AEFF5B15A5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\Terminal.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace StockSharp.Terminal.Properties
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
                if ( StockSharp.Terminal.Properties.Resources.resourceMan == null )
                    StockSharp.Terminal.Properties.Resources.resourceMan = new ResourceManager( "StockSharp.Terminal.Properties.Resources", typeof( StockSharp.Terminal.Properties.Resources ).Assembly );
                return StockSharp.Terminal.Properties.Resources.resourceMan;
            }
        }

        [EditorBrowsable( EditorBrowsableState.Advanced )]
        internal static CultureInfo Culture
        {
            get
            {
                return StockSharp.Terminal.Properties.Resources.resourceCulture;
            }
            set
            {
                StockSharp.Terminal.Properties.Resources.resourceCulture = value;
            }
        }

        internal static string DefaultAreaLayout
        {
            get
            {
                return StockSharp.Terminal.Properties.Resources.ResourceManager.GetString( nameof( DefaultAreaLayout ), StockSharp.Terminal.Properties.Resources.resourceCulture );
            }
        }

        internal static string DefaultLayout
        {
            get
            {
                return StockSharp.Terminal.Properties.Resources.ResourceManager.GetString( nameof( DefaultLayout ), StockSharp.Terminal.Properties.Resources.resourceCulture );
            }
        }
    }
}
