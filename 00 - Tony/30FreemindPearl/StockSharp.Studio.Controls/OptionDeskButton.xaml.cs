using DevExpress.Xpf.Core;
using Ecng.Serialization;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using System;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    public partial class OptionDeskButton : SimpleButton, IStudioControl, IPersistable, IDisposable, IComponentConnector
    {        
        public OptionDeskButton()
        {
            InitializeComponent();
        }

        protected override void OnClick()
        {
            new OpenWindowCommand( Guid.NewGuid().ToString(), typeof( OptionDeskPanel ), false ).Process( this, false );
            base.OnClick();
        }

        void IPersistable.Load( SettingsStorage storage )
        {
        }

        void IPersistable.Save( SettingsStorage storage )
        {
        }

        void IDisposable.Dispose()
        {
        }

        void IStudioControl.FirstTimeInit()
        {
        }

        void IStudioControl.SendCommand( IStudioCommand command )
        {
        }

        string IStudioControl.Title
        {
            get
            {
                return ( string )ToolTip;
            }
            set
            {
            }
        }

        Uri IStudioControl.Icon
        {
            get
            {
                return null;
            }
        }

        public string Key { get; set; }

        bool IStudioControl.SaveWithLayout
        {
            get
            {
                return true;
            }
        }

        bool IStudioControl.IsTitleEditable
        {
            get
            {
                return false;
            }
        }        
    }
}
