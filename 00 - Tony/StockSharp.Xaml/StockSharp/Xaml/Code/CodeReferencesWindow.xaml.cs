// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Code.CodeReferencesWindow
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Reflection;
using Ecng.Serialization;
using Ecng.Xaml;
using Microsoft.Win32;
using StockSharp.Localization;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml.Code
{
    /// <summary>
    /// The window for editing the list of references to the .NET builds.
    /// </summary>
    /// <summary>CodeReferencesWindow</summary>
    public partial class CodeReferencesWindow : ThemedWindow, IPersistable, IComponentConnector
    {
        /// <summary>The command for the applying changes.</summary>
        public static readonly RoutedCommand OkCommand = new RoutedCommand();
        /// <summary>The command for the adding references.</summary>
        public static readonly RoutedCommand AddCommand = new RoutedCommand();
        /// <summary>The command for the removing references.</summary>
        public static readonly RoutedCommand RemoveCommand = new RoutedCommand();

        private readonly ObservableCollection<CodeReference> _references = new ObservableCollection<CodeReference>();

        private bool _isReady;



        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Code.CodeReferencesWindow" />.
        /// </summary>
        public CodeReferencesWindow()
        {

            this.InitializeComponent();
            ReferencesListView.ItemsSource = _references;
        }

        /// <summary>References.</summary>
        public IList<CodeReference> References
        {
            get
            {
                return ( IList<CodeReference> )this._references;
            }
        }

        private void CanOk( object _param1, CanExecuteRoutedEventArgs _param2 )
        {
            _param2.CanExecute = this._isReady;
        }

        private void ExecuteOk( object _param1, ExecutedRoutedEventArgs _param2 )
        {
            ( ( Window )this ).Close();
        }

        private void CanAdd( object _param1, CanExecuteRoutedEventArgs _param2 )
        {
            _param2.CanExecute = true;
        }

        private void ExecuteAdd( object _param1, ExecutedRoutedEventArgs _param2 )
        {
            DXOpenFileDialog dialog = new DXOpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            dialog.Filter = LocalizedStrings.Str1422;


            if ( !dialog.ShowModal( this ) )
                return;

            AssemblyName assemblyName = ReflectionHelper.VerifyAssembly( dialog.FileName );

            if ( assemblyName == null )
            {
                int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.Str1423 ).Warning().Owner( ( Window )this ).Show();
            }
            else
            {
                this._references.Add( new CodeReference()
                {
                    Name = assemblyName.Name,
                    Location = dialog.FileName
                } );
                this._isReady = true;
            }
        }

        private void CanRemove( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = ( ( DataControlBase )ReferencesListView )?.SelectedItem != null;
        }

        private void ExecuteRemove( object _param1, ExecutedRoutedEventArgs _param2 )
        {
            _references.RemoveRange( ReferencesListView.SelectedItems.Cast<CodeReference>().ToArray() );
            _isReady = true;
        }

        /// <summary>Load settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Load( SettingsStorage storage )
        {
            ( ( DependencyObject )this.BarManager ).LoadDevExpressControl( ( string )storage.GetValue<string>( "BarManager", null ) );
            this.ReferencesListView.Load( ( SettingsStorage )storage.GetValue<SettingsStorage>( "ReferencesListView", null ) );
        }

        /// <summary>Save settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Save( SettingsStorage storage )
        {
            storage.SetValue<string>( "BarManager", ( ( DependencyObject )this.BarManager ).SaveDevExpressControl() );
            storage.SetValue<SettingsStorage>( "ReferencesListView", PersistableHelper.Save( ( IPersistable )this.ReferencesListView ) );
        }

        //    /// <summary>InitializeComponent</summary>
        //    [DebuggerNonUserCode]
        //    [GeneratedCode( "PresentationBuildTasks", "6.0.8.0" )]
        //    public void InitializeComponent()
        //    {
        //        if ( this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        //        return;
        //        this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
        //        Application.LoadComponent( ( object )this, new Uri( nameof( 2127268208 ), UriKind.Relative ) );
        //    }

        //    [DebuggerNonUserCode]
        //    [GeneratedCode( "PresentationBuildTasks", "6.0.8.0" )]
        //    internal Delegate \u0023\u003Dzk_6RQsm5oL9L( Type _param1, string _param2 )
        //    {
        //        return Delegate.CreateDelegate( _param1, ( object )this, _param2 );
        //    }

        //    [DebuggerNonUserCode]
        //    [EditorBrowsable( EditorBrowsableState.Never )]
        //    [GeneratedCode( "PresentationBuildTasks", "6.0.8.0" )]
        //    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
        //      int _param1,
        //      object _param2)
        //        {
        //        switch ( _param1 )
        //        {
        //            case 1:
        //            ( ( CommandBinding )_param2 ).CanExecute += new CanExecuteRoutedEventHandler( this.CanOk );
        //            ( ( CommandBinding )_param2 ).Executed += new ExecutedRoutedEventHandler( this.ExecuteOk );
        //            break;
        //            case 2:
        //            ( ( CommandBinding )_param2 ).CanExecute += new CanExecuteRoutedEventHandler( this.CanAdd );
        //            ( ( CommandBinding )_param2 ).Executed += new ExecutedRoutedEventHandler( this.ExecuteAdd );
        //            break;
        //            case 3:
        //            ( ( CommandBinding )_param2 ).CanExecute += new CanExecuteRoutedEventHandler( this.CanRemove);
        //            ( ( CommandBinding )_param2 ).Executed += new ExecutedRoutedEventHandler( this.ExecuteRemove );
        //            break;
        //            case 4:
        //            this.\u0023\u003DzzLrbrvw\u003D = ( BarManager )_param2;
        //            break;
        //            case 5:
        //            this.\u0023\u003DzniS3Bl2QrzBn = ( BaseGridControl )_param2;
        //            break;
        //            default:
        //            this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
        //            break;
        //        }
        //    }
        //}
    }
}
