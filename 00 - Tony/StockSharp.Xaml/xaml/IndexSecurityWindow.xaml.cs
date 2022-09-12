using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Expressions;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class IndexSecurityWindow : DXWindow, ISecurityWindow
    {
        private Func<string, string> _validateId = id => null ;
        private ISecurityStorage _storage;
        private Security _security;
        

        public IndexSecurityWindow( )
        {
            InitializeComponent( );

            var indexSec = new ExpressionIndexSecurity( );

            indexSec.Decimals = new int?( 4 );
            indexSec.PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, ( byte )4 ) );
            indexSec.VolumeStep = new Decimal?( ( Decimal )1 );

            Security = ( Security )indexSec;
            AvailableFunctions.Content = ( object )( LocalizedStrings.AvailableFunctions + ": " + ExpressionFormula.Functions.Join( ", " ) );
        }

        public ISecurityStorage SecurityStorage
        {
            get
            {
                return _storage;
            }
            set
            {
                _storage = value;
            }
        }

        public Func<string, string> ValidateId
        {
            get
            {
                return _validateId;
            }
            set
            {                
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );
                _validateId = value;
            }
        }

        public Security Security
        {
            get
            {
                return _security;
            }
            set
            {
                Security security = value;
                if ( security == null )
                    throw new ArgumentNullException( nameof( value ) );
                _security = security;

                SecurityId.Text  = value.Id;
                IndexEditor.Text = value.BasketExpression;
                Title            = Title + " " + value.Id;
                

                PropGrid.SelectedObject = ( object )new IndexSecurityWindow.SimpleSecurityInfo( )
                {
                    Decimals   = value.Decimals,
                    PriceStep  = value.PriceStep,
                    VolumeStep = value.VolumeStep
                };
            }
        }

        private string method_0( )
        {
            return SecurityId.Text;
        }

        private void method_1( string string_0 )
        {
            SecurityId.Text = string_0;
        }

        private string method_2( )
        {
            return IndexEditor.Text;
        }

        private void method_3( string string_0 )
        {
            IndexEditor.Text = string_0;
        }

        private void SecurityId_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            EnableOkayButton( );
        }

        private void textBox_0_TextChanged( object sender, TextChangedEventArgs e )
        {
            EnableOkayButton( );
            Verify.IsEnabled = !method_2( ).IsEmpty( );
        }

        private void EnableOkayButton( )
        {
            OkBtn.IsEnabled = !method_0( ).IsEmpty( ) && !method_2( ).IsEmpty( );
        }

        private void method_6( string string_0 )
        {
            int num = ( int )new MessageBoxBuilder( ).Text( string_0 ).Owner( ( Window )this ).Warning( ).Show( );
        }

        private void OkBtn_Click( object sender, RoutedEventArgs e )
        {
            string str = method_7( );
            if ( !str.IsEmpty( ) )
            {
                method_6( str );
            }
            else
            {
                if ( Security.Id.IsEmpty( ) )
                {
                    string id = method_0( );
                    if ( SecurityStorage.LookupById( id ) != null )
                    {
                        method_6( LocalizedStrings.Str2927Params.Put( ( object )id ) );
                        return;
                    }
                    string string_0 = TraderHelper.ValidateId( ref id );
                    if ( string_0 != null )
                    {
                        method_6( string_0 );
                        return;
                    }
                    SecurityId securityId = id.ToSecurityId( ( SecurityIdGenerator )null );
                    Security.Id = id;
                    Security.Code = securityId.SecurityCode;
                    Security.Board = ServicesRegistry.ExchangeInfoProvider.GetOrCreateBoard( securityId.BoardCode, ( Func<string, ExchangeBoard> )null );
                }
                Security.BasketExpression = method_2( );
                IndexSecurityWindow.SimpleSecurityInfo selectedObject = ( IndexSecurityWindow.SimpleSecurityInfo )PropGrid.SelectedObject;
                Security.PriceStep = selectedObject.PriceStep;
                Security.VolumeStep = selectedObject.VolumeStep;
                Security.Decimals = selectedObject.Decimals;
                SecurityStorage.Save( Security, true );
                DialogResult = new bool?( true );
            }
        }

        private string method_7( )
        {
            ExpressionFormula expressionFormula = ExpressionFormula.Compile( method_2( ), true );
            if ( !expressionFormula.Error.IsEmpty( ) )
                return LocalizedStrings.Str1523Params.Put( ( object )expressionFormula.Error );
            string str = expressionFormula.SecurityIds.FirstOrDefault<string>( new Func<string, bool>( method_9 ) );
            if ( str == null )
                return ( string )null;
            return LocalizedStrings.Str704Params.Put( ( object )str );
        }

        private void Verify_Click( object sender, RoutedEventArgs e )
        {
            string str = method_7( );
            if ( str.IsEmpty( ) )
            {
                int num = ( int )new MessageBoxBuilder( ).Text( LocalizedStrings.Str1421 ).Owner( ( Window )this ).Show( );
            }
            else
                method_6( str );
        }

        
        private bool method_9( string string_0 )
        {
            return SecurityStorage.LookupById( string_0 ) == null;
        }

        private sealed class SimpleSecurityInfo
        {
            private Decimal? _priceStep;
            private Decimal? _volumeStep;
            private int? _decimals;

            [Display( Description = "MinPriceStep", GroupName = "General", Name = "PriceStep", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
            public Decimal? PriceStep
            {
                get
                {
                    return _priceStep;
                }
                set
                {
                    Decimal? nullable0 = _priceStep;
                    Decimal? nullable1 = value;
                    if ( nullable0.GetValueOrDefault( ) == nullable1.GetValueOrDefault( ) & nullable0.HasValue == nullable1.HasValue )
                        return;
                    Decimal? nullable2 = value;
                    Decimal num = new Decimal( );
                    if ( nullable2.GetValueOrDefault( ) < num & nullable2.HasValue )
                        throw new ArgumentOutOfRangeException( nameof( value ) );
                    _priceStep = value;
                }
            }

            [Display( Description = "Str366", GroupName = "General", Name = "VolumeStep", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
            public Decimal? VolumeStep
            {
                get
                {
                    return _volumeStep;
                }
                set
                {
                    Decimal? nullable1 = _volumeStep;
                    Decimal? nullable2 = value;
                    if ( nullable1.GetValueOrDefault( ) == nullable2.GetValueOrDefault( ) & nullable1.HasValue == nullable2.HasValue )
                        return;
                    Decimal? nullable3 = value;
                    Decimal num = new Decimal( );
                    if ( nullable3.GetValueOrDefault( ) < num & nullable3.HasValue )
                        throw new ArgumentOutOfRangeException( nameof( value ) );
                    _volumeStep = value;
                }
            }

            [Display( Description = "Str548", GroupName = "General", Name = "Decimals", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
            public int? Decimals
            {
                get
                {
                    return _decimals;
                }
                set
                {
                    int? nullable2 = _decimals;
                    int? nullable1 = value;
                    if ( nullable2.GetValueOrDefault( ) == nullable1.GetValueOrDefault( ) & nullable2.HasValue == nullable1.HasValue )
                        return;
                    int? nullable3 = value;
                    if ( nullable3.GetValueOrDefault( ) < 0 & nullable3.HasValue )
                        throw new ArgumentOutOfRangeException( nameof( value ) );
                    _decimals = value;
                }
            }
        }

        
    }
}
