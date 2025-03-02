// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OptionDeskButton
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Core;
using Ecng.Serialization;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;

namespace StockSharp.Studio.Controls
{
    public partial class OptionDeskButton : SimpleButton, IStudioControl, IPersistable, IDisposable
    {
        

        public OptionDeskButton()
        {
            this.InitializeComponent();
        }

        protected override void OnClick()
        {
            new OpenWindowCommand( typeof( OptionDeskPanel ), false ).Process(  this, false );
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
            GC.SuppressFinalize(  this );
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
                return ( string ) this.ToolTip;
            }
            set
            {
            }
        }

        ImageSource IStudioControl.Icon
        {
            get
            {
                return ( ImageSource ) null;
            }
        }

        string IStudioControl.DocUrl
        {
            get
            {
                return ( string ) null;
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
