// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.SecuritiesWindowEx
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Core;
using Ecng.Collections;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    public partial class SecuritiesWindowEx : ThemedWindow, ISecuritiesSelectWindow, IComponentConnector
    {
        public static RoutedCommand SelectSecurityCommand = new RoutedCommand();
        public static RoutedCommand UnselectSecurityCommand = new RoutedCommand();
        

        public bool IsLookup { get; set; }

        public IEnumerable<Security> SelectedSecurities
        {
            get
            {
                return this.SecuritiesSelected.Securities.LookupAll();
            }
            set
            {
                this.SelectSecurities( value.ToArray<Security>() );
            }
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return this.SecuritiesAll.SecurityProvider;
            }
            set
            {
                this.SecuritiesAll.SecurityProvider = value;
            }
        }

        public bool AllowDuplicates { get; set; }

        public SecuritiesWindowEx()
        {
            this.InitializeComponent();
            this.SecuritiesAll.SecurityDoubleClick += ( Action<Security> )( security =>
              {
                  if ( security == null )
                      return;
                  this.SelectSecurities( new Security[1] { security } );
              } );
            this.SecuritiesSelected.SecurityDoubleClick += ( Action<Security> )( security =>
              {
                  if ( security == null )
                      return;
                  this.UnselectSecurities( new Security[1] { security } );
              } );
        }

        private void SecuritiesWindowEx_OnLoaded( object sender, RoutedEventArgs e )
        {
            if ( this.IsLookup )
            {
                this.SecuritiesAll.Title = LocalizedStrings.Str3255;
                this.SecuritiesSelected.Title = LocalizedStrings.Str3256;
                StudioServicesRegistry.CommandService.Register<LookupSecuritiesResultCommand>( ( object )this, false, ( Action<LookupSecuritiesResultCommand> )( cmd => this.SecuritiesAll.Securities.AddRange( cmd.Securities ) ), ( Func<LookupSecuritiesResultCommand, bool> )null );
            }
            else
                this.LookupPanel.Visibility = Visibility.Collapsed;
        }

        protected override void OnClosed( EventArgs e )
        {
            StudioServicesRegistry.CommandService.UnRegister<LookupSecuritiesResultCommand>( ( object )this );
            base.OnClosed( e );
        }

        private void SelectSecurities( Security[ ] securities )
        {
            this.SecuritiesSelected.Securities.AddRange( ( IEnumerable<Security> )securities );
            if ( !this.AllowDuplicates )
                this.SecuritiesAll.ExcludeSecurities.AddRange<Security>( ( IEnumerable<Security> )securities );
            this.EnableOk();
        }

        private void UnselectSecurities( Security[ ] securities )
        {
            this.SecuritiesSelected.Securities.RemoveRange( ( IEnumerable<Security> )securities );
            if ( !this.AllowDuplicates )
                this.SecuritiesAll.ExcludeSecurities.RemoveRange<Security>( ( IEnumerable<Security> )securities );
            this.EnableOk();
        }

        private void ExecutedSelectSecurity( object sender, ExecutedRoutedEventArgs e )
        {
            this.SelectSecurities( this.SecuritiesAll.SelectedSecurities.ToArray<Security>() );
        }

        private void CanExecuteSelectSecurity( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.SecuritiesAll.SelectedSecurities.Any<Security>();
        }

        private void ExecutedUnselectSecurity( object sender, ExecutedRoutedEventArgs e )
        {
            this.UnselectSecurities( this.SecuritiesSelected.SelectedSecurities.ToArray<Security>() );
        }

        private void CanExecuteUnselectSecurity( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.SecuritiesSelected.SelectedSecurities.Any<Security>();
        }

        private void EnableOk()
        {
            this.Ok.IsEnabled = true;
        }

        private void LookupPanel_OnLookup( Security filter )
        {
            this.SecuritiesAll.Securities.Clear();
            new LookupSecuritiesCommand( filter ).Process( ( object )this, false );
        }

        
    }
}
