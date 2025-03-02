// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PropertiesPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using Ecng.ComponentModel;
using StockSharp.Localization;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str1507" )]
    [DescriptionLoc( "Str3270", false )]
    [Guid( "A9BC4619-81DF-4C36-8696-39D754D7DA75" )]
    [VectorIcon( "Edit" )]
    public partial class PropertiesPanel : BaseStudioControl, IComponentConnector
    {        
        public PropertiesPanel()
        {
            PropertiesPanel propertiesPanel = this;
            InitializeComponent();
            Type prevType = null;
            CommandService.Register<SelectCommand>( this, true, cmd =>
                {
                    if ( cmd.Instance == null && cmd.InstanceType != prevType )
                        return;
                    prevType = cmd.InstanceType;
                    string instance = cmd.Instance as string;
                    if ( instance != null )
                    {
                        propertiesPanel.WatermarkTextBlock.Text = instance;
                        propertiesPanel.WatermarkTextBlock.Visibility = Visibility.Visible;
                        propertiesPanel.PropertyGrid.SelectedObject = null;
                    }
                    else
                    {
                        propertiesPanel.WatermarkTextBlock.Visibility = Visibility.Collapsed;
                        if ( propertiesPanel.PropertyGrid.SelectedObject == cmd.Instance )
                            propertiesPanel.PropertyGrid.SelectedObject = null;
                        propertiesPanel.PropertyGrid.ReadOnly = !cmd.CanEdit;
                        propertiesPanel.PropertyGrid.SelectedObject = cmd.Instance;
                    }
                }, null );
        }

        public override void Dispose()
        {
            CommandService.UnRegister<SelectCommand>( this );
            base.Dispose();
        }        
    }
}
