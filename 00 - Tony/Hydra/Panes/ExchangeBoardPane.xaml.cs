using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Hydra.Panes
{
    [DisplayNameLoc( "Str2831" )]
    [Guid( "4D70A7BB-FDCD-48BF-9714-CD13F01712BA" )]
    [Icon( "Bank", false )]
    public partial class ExchangeBoardPane : BaseStudioControl, IPane, IStudioControl, IPersistable, IDisposable, IComponentConnector
    {

        public ExchangeBoardPane()
        {
            InitializeComponent();
        }

        public override void Load( SettingsStorage storage )
        {
            Panel.Load( storage );
            base.Load( storage );
        }

        public override void Save( SettingsStorage storage )
        {
            Panel.Save( storage );
            base.Save( storage );
        }

        bool IPane.IsValid
        {
            get
            {
                return true;
            }
        }


    }
}
