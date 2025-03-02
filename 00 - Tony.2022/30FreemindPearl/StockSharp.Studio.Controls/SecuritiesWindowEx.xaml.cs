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
                return SecuritiesSelected.Securities.LookupAll();
            }
            set
            {
                SelectSecurities( value.ToArray() );
            }
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return SecuritiesAll.SecurityProvider;
            }
            set
            {
                SecuritiesAll.SecurityProvider = value;
            }
        }

        public bool AllowDuplicates { get; set; }

        public SecuritiesWindowEx()
        {
            InitializeComponent();
            SecuritiesAll.SecurityDoubleClick += security =>
              {
                  if ( security == null )
                      return;
                  SelectSecurities( new Security[1] { security } );
              };
            SecuritiesSelected.SecurityDoubleClick += security =>
              {
                  if ( security == null )
                      return;
                  UnselectSecurities( new Security[1] { security } );
              };
        }

        private void SecuritiesWindowEx_OnLoaded( object sender, RoutedEventArgs e )
        {
            if ( IsLookup )
            {
                SecuritiesAll.Title = LocalizedStrings.Str3255;
                SecuritiesSelected.Title = LocalizedStrings.Str3256;
                StudioServicesRegistry.CommandService.Register<LookupSecuritiesResultCommand>( this, false, cmd => SecuritiesAll.Securities.AddRange( cmd.Securities ), null );
            }
            else
                LookupPanel.Visibility = Visibility.Collapsed;
        }

        protected override void OnClosed( EventArgs e )
        {
            StudioServicesRegistry.CommandService.UnRegister<LookupSecuritiesResultCommand>( this );
            base.OnClosed( e );
        }

        private void SelectSecurities( Security[ ] securities )
        {
            SecuritiesSelected.Securities.AddRange( securities );
            if ( !AllowDuplicates )
                SecuritiesAll.ExcludeSecurities.AddRange( securities );
            EnableOk();
        }

        private void UnselectSecurities( Security[ ] securities )
        {
            SecuritiesSelected.Securities.RemoveRange( securities );
            if ( !AllowDuplicates )
                SecuritiesAll.ExcludeSecurities.RemoveRange( securities );
            EnableOk();
        }

        private void ExecutedSelectSecurity( object sender, ExecutedRoutedEventArgs e )
        {
            SelectSecurities( SecuritiesAll.SelectedSecurities.ToArray() );
        }

        private void CanExecuteSelectSecurity( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SecuritiesAll.SelectedSecurities.Any();
        }

        private void ExecutedUnselectSecurity( object sender, ExecutedRoutedEventArgs e )
        {
            UnselectSecurities( SecuritiesSelected.SelectedSecurities.ToArray() );
        }

        private void CanExecuteUnselectSecurity( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SecuritiesSelected.SelectedSecurities.Any();
        }

        private void EnableOk()
        {
            Ok.IsEnabled = true;
        }

        private void LookupPanel_OnLookup( Security filter )
        {
            SecuritiesAll.Securities.Clear();
            new LookupSecuritiesCommand( filter ).Process( this, false );
        }

        
    }
}
