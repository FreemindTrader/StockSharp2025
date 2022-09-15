using DevExpress.Xpf.Utils.Themes;
using System;
using System.Reflection;

namespace StockSharp.Xaml
{
    internal sealed class OptionThemeKey : ThemeKeyExtensionBase<OptionEnum>
    {
        public override Assembly Assembly
        {
            get
            {
                if ( !( this.TypeInTargetAssembly != ( Type ) null ) )
                {
                    return this.GetType( ).Assembly;
                }

                return this.TypeInTargetAssembly.Assembly;
            }
        }
    }
}
