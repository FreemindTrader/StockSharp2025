using DevExpress.Xpf.Core;
using Ecng.Serialization;
using StockSharp.Logging;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class LogWindow : DXWindow, IPersistable, IDisposable, ILogListener
    {
        public LogWindow( )
        {
            InitializeComponent();
        }

        /// <summary>
		/// Format for conversion time into a string.
		/// </summary>
		public string TimeFormat
        {
            get { return LogCtrl.TimeFormat; }
            set { LogCtrl.TimeFormat = value; }
        }

        /// <summary>
		/// The log entries collection.
		/// </summary>
		public IList<LogMessage> Messages => LogCtrl.Messages;

        void ILogListener.WriteMessages( IEnumerable<LogMessage> messages )
        {
            ( ( ILogListener ) LogCtrl ).WriteMessages( messages );
        }

        void IPersistable.Load( SettingsStorage storage )
        {
            LogCtrl.Load( storage );
        }

        void IPersistable.Save( SettingsStorage storage )
        {
            LogCtrl.Save( storage );
        }

        void IDisposable.Dispose( )
        {
        }
    }
}
