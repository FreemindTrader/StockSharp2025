// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ExchangeEditorPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "ExchangeEditorPanel", Name = "Boards", ResourceType = typeof( LocalizedStrings ) )]
    [Guid( "66f50d15-6eaa-4692-ba88-12bee0b6e6e5" )]
    [Doc( "topics/designer/user_interface/boards.html" )]
    [VectorIcon( "Bank" )]
    public partial class ExchangeEditorPanel : BaseStudioControl
    {
        public ExchangeEditorPanel()
        {
            this.InitializeComponent();
            this.Panel.Changed += RaiseChangedCommand;
        }

        public override void Save( SettingsStorage storage )
        {
            this.Panel.Save( storage );
            base.Save( storage );
        }

        public override void Load( SettingsStorage storage )
        {
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.Panel, storage );
            base.Load( storage );
        }        
    }
}
