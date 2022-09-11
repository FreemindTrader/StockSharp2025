// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ExchangeEditorPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Localization;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str3234" )]
    [DescriptionLoc( "Str3235", false )]
    [Guid( "66f50d15-6eaa-4692-ba88-12bee0b6e6e5" )]
    [Doc( "topics/Designer_Boards.html" )]
    [VectorIcon( "Bank" )]
    public partial class ExchangeEditorPanel : BaseStudioControl, IComponentConnector
    {        
        public ExchangeEditorPanel()
        {
            InitializeComponent();
            Panel.Changed += RaiseChangedCommand;
        }

        public override void Save( SettingsStorage storage )
        {
            Panel.Save( storage );
            base.Save( storage );
        }

        public override void Load( SettingsStorage storage )
        {
            Panel.Load( storage );
            base.Load( storage );
        }

        
    }
}
