// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Controls.TaskSettingsPanel
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Hydra.Controls
{
    public partial class TaskSettingsPanel : UserControl, IComponentConnector
    {
        private readonly Dictionary<DataType, string> _dataTypes = new Dictionary<DataType, string>() { { DataType.Ticks, LocalizedStrings.Str985 }, { DataType.MarketDepth, LocalizedStrings.MarketDepths }, { DataType.OrderLog, LocalizedStrings.OrderLog }, { DataType.Level1, LocalizedStrings.Level1 }, { DataType.Transactions, LocalizedStrings.Transactions }, { DataType.News, LocalizedStrings.News }, { DataType.BoardState, LocalizedStrings.BoardInfo }, { DataType.PositionChanges, LocalizedStrings.Str972 } };
        private IHydraTask _task;
        private bool _loadedPropDef;

        public TaskSettingsPanel()
        {
            InitializeComponent();
        }

        public event Action Changed;

        public IHydraTask Task
        {
            get
            {
                return _task;
            }
            set
            {
                if ( value == null )
                {
                    _task = null;
                    TaskSettings.IsEnabled = false;
                    TaskSettings.SelectedObject = null;
                    DescriptionCtrl.Text = AbilitiesCtrl.Text = string.Empty;
                    Test.Visibility = Visibility.Collapsed;
                    HelpButton.DocUrl = null;
                }
                else
                {
                    _task = value;
                    TaskSettings.IsEnabled = true;
                    TaskSettings.SelectedObject = _task;
                    DescriptionCtrl.Text = _task.GetDescription();
                    AbilitiesCtrl.Text = _task.SupportedDataTypes.Select( t =>
                    {
                        if ( t.IsCandles )
                            return LocalizedStrings.Candles;
                        string str;
                        if ( !_dataTypes.TryGetValue( t, out str ) )
                            return t.MessageType.Name;
                        return str;
                    } ).Distinct().JoinCommaSpace();
                    if ( _task.CanTestConnect )
                        Test.Visibility = Visibility.Visible;
                    HelpButton.DocUrl = _task.GetType().GetReflectTaskType().GetDocUrl();
                }
            }
        }

        private void SourceSettings_OnError( object sender, ValidationErrorEventArgs e )
        {
        }

        private void Test_Click( object sender, RoutedEventArgs e )
        {
            BusyIndicator.IsBusy = true;
            Test.IsEnabled = false;
            _task.TestConnect( error => GuiDispatcher.GlobalDispatcher.AddAction( () =>
            {
                if ( error == null )
                {
                    int num1 = ( int )new MessageBoxBuilder().Text( LocalizedStrings.Str1560 ).Owner( this ).Show();
                }
                else
                {
                    int num2 = ( int )new MessageBoxBuilder().Text( error.Message ).Caption( LocalizedStrings.Str1561 ).Error().Owner( this ).Show();
                }
                BusyIndicator.IsBusy = false;
                Test.IsEnabled = true;
            } ) );
        }

        private void TaskSettings_OnCellValueChanged( object sender, CellValueChangedEventArgs e )
        {
            if ( Task == null )
                return;
            Task.IsDefault = false;
            Task.SaveSettings();
            Action changed = Changed;
            if ( changed == null )
                return;
            changed();
        }

        private void TaskSettings_OnCustomExpand( object sender, CustomExpandEventArgs args )
        {
            if ( !args.IsInitializing || !args.Row.Path.EqualsIgnoreCase( "Adapter" ) )
                return;
            IHydraTask task = Task;
            if ( !( ( task != null ? task.GetType().GetAdapterType() : null ) != null ) )
                return;
            args.IsExpanded = true;
        }

        private void TaskSettings_OnLoaded( object sender, RoutedEventArgs e )
        {
            if ( _loadedPropDef )
                return;
            _loadedPropDef = true;
            TaskSettings.PropertyDefinitions.Add( ( PropertyDefinitionBase )TaskSettings.FindResource( "taskPD" ) );
        }

        
    }
}
