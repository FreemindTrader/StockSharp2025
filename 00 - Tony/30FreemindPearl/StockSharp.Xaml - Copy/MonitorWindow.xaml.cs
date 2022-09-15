using DevExpress.Xpf.Core;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Logging;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class MonitorWindow : DXWindow, IPersistable, IDisposable, ILogListener
    {
        private bool _bringToFrontOnError;

        public MonitorWindow( )
        {
            InitializeComponent();
        }

        public bool BringToFrontOnError
        {
            get
            {
                return _bringToFrontOnError;
            }
            set
            {
                _bringToFrontOnError = value;
            }
        }

        public void Clear( )
        {
            Monitor.Clear();
        }


        public void Dispose( )
        {
            
        }

        public void Load( SettingsStorage storage )
        {
            Monitor.Load( storage );
            BringToFrontOnError = ( bool ) storage.GetValue<bool>( "BringToFrontOnError",  false );
        }

        public void Save( SettingsStorage storage )
        {
            Monitor.Save( storage );
            storage.SetValue<bool>( "BringToFrontOnError", BringToFrontOnError );
        }

        public void WriteMessages( IEnumerable<LogMessage> messages )
        {
            ( ( ILogListener ) Monitor ).WriteMessages( messages );
            if ( !BringToFrontOnError || !messages.Any( m => m.Level == LogLevels.Error ) )
            {
                return;
            }

            Ecng.Xaml.XamlHelper.BringToFront( ( Window ) this );
        }
    }
}
