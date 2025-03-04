using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Windows;
using StockSharp.Localization;
using StockSharp.Xaml;
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
    public partial class SecurityPickerButton : SimpleButton, IComponentConnector
    {
        private Security[ ] _selectedSecurities = Array.Empty<Security>();
        private string _contentTemplate;
        
        public SecurityPickerButton()
        {
            InitializeComponent();
            _contentTemplate = Title;
        }

        public void InitTitle( string title )
        {
            if ( title.IsEmpty() )
                throw new ArgumentNullException( nameof( title ) );
            Title = title;
            _contentTemplate = Title;
        }

        public string Title
        {
            get
            {
                return TitleCtrl.Text;
            }
            private set
            {
                TitleCtrl.Text = value;
            }
        }

        public Security SelectedSecurity
        {
            get
            {
                return SelectedSecurities.FirstOrDefault();
            }
            set
            {
                IEnumerable<Security> securities;
                if ( value != null )
                    securities =   ( new Security[1]
                    {
            value
                    } );
                else
                    securities = Enumerable.Empty<Security>();
                SelectedSecurities = securities;
            }
        }

        public IEnumerable<Security> SelectedSecurities
        {
            get
            {
                return _selectedSecurities;
            }
            set
            {
                _selectedSecurities = value.ToArray();
                if ( _selectedSecurities.IsEmpty() )
                {
                    Title = _contentTemplate;
                }
                else
                {
                    Title = _selectedSecurities.Take( 2 ).Select( s => s.Id ).JoinCommaSpace();
                    if ( _selectedSecurities.Length > 2 )
                        Title = Title + " " + LocalizedStrings.NMore.Put( _selectedSecurities.Length - 2 );
                }
                Action securitySelected = SecuritySelected;
                if ( securitySelected == null )
                    return;
                securitySelected();
            }
        }

        public bool IsSingleSecurity { get; set; }

        public event Action SecuritySelected;

        protected override void OnClick()
        {
            base.OnClick();
            ISecurityProvider securityProvider = ServicesRegistry.SecurityProvider;
            if ( IsSingleSecurity )
            {
                SecurityPickerWindow wnd = new SecurityPickerWindow() { SecurityProvider = securityProvider, SelectionMode = MultiSelectMode.None, SelectedSecurity = SelectedSecurities.FirstOrDefault() };
                wnd.ExcludeSecurities.Add( TraderHelper.AllSecurity );
                if ( !wnd.ShowModal( this ) )
                    return;
                SelectedSecurities =   ( new Security[1]
                {
                    wnd.SelectedSecurity
                } );
            }
            else
            {
                SecuritiesWindowEx wnd = new SecuritiesWindowEx() { SecurityProvider = securityProvider };
                wnd.SecuritiesAll.ExcludeAllSecurity();
                wnd.SelectedSecurities = SelectedSecurities;
                if ( !wnd.ShowModal( this ) )
                    return;
                SelectedSecurities = wnd.SelectedSecurities.ToArray();
            }
        }

        
    }
}
