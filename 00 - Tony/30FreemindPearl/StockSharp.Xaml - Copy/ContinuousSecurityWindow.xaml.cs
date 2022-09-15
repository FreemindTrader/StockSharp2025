using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using static StockSharp.Xaml.SecurityJumpsEditor;

namespace StockSharp.Xaml
{
    /// <summary>
    /// Interaction logic for ContinuousSecurityWindow.xaml
    /// </summary>
    public partial class ContinuousSecurityWindow : DXWindow, ISecurityWindow
    {
        private readonly ExpirationContinuousSecurity _expirationSecurity = new ExpirationContinuousSecurity();
        private readonly VolumeContinuousSecurity _volumeSecurity = new VolumeContinuousSecurity();

        
        private ISecurityStorage _securityStorage;
        private IExchangeInfoProvider _provider;
        private Security _security;

        public ContinuousSecurityWindow( )
        {
            InitializeComponent( );
            Security security = new Security();
            security.BasketCode = ( "CE" );
            Security = security;
            ExchangeInfoProvider = ( IExchangeInfoProvider ) ConfigManager.TryGetService<IExchangeInfoProvider>( );
        }

        public ISecurityStorage SecurityStorage
        {
            get
            {
                return _securityStorage;
            }
            set
            {
                JumpsGrid.SecurityProvider = ( ISecurityProvider ) ( _securityStorage = value );
            }
        }

        public IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return _provider;
            }
            set
            {
                _provider = value;
            }
        }


        private Func<string, string> _validateId = id => null;

        /// <summary>
		/// The handler checking the entered identifier availability for <see cref="Security"/>.
		/// </summary>
		public Func<string, string> ValidateId
        {
            get { return _validateId; }
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
                if ( value == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                _security = value;
                string basketCode = _security.BasketCode;

                if ( basketCode != "CE" )
                {
                    if ( basketCode != "CV" )
                    {
                        throw new ArgumentOutOfRangeException( _security.BasketCode );
                    }

                    _volumeSecurity.BasketExpression = _security.BasketExpression;

                    TabCtrl.SelectedContainer = VolTab;
                }
                else
                {
                    _expirationSecurity.BasketExpression = _security.BasketExpression;
                    TabCtrl.SelectedContainer = ExpTab;
                }

                SecurityId.Text = value.Id;
                Title = Title + " " + value.Id;
                JumpsGrid.Jumps.Clear( );

                if ( TabCtrl.SelectedContainer == VolTab )
                {
                    VolSecGrid.Securities.AddRange( _volumeSecurity.InnerSecurities.Select( i => SecurityStorage.LookupById( i ) ).Where( s => s != null ) );
                }
                else
                {
                    JumpsGrid.Jumps.AddRange( _expirationSecurity.ExpirationJumps.Select( k => new SecurityJump( ) { Security = TraderHelper.LookupById( ( ISecurityProvider ) SecurityStorage, k.Key ), Date = k.Value.UtcDateTime } ) );
                }

                ExpirationPropGrid.SelectedObject = ( new SimpleSecurityInfo( )
                {
                    Decimals   = value.Decimals,
                    PriceStep  = value.PriceStep,
                    VolumeStep = value.VolumeStep
                } );

                var infoEx = new SimpleSecurityInfoEx();

                infoEx.Decimals       = value.Decimals;
                infoEx.PriceStep      = value.PriceStep;
                infoEx.VolumeStep     = value.VolumeStep;
                infoEx.IsOpenInterest = _volumeSecurity.IsOpenInterest;
                infoEx.VolumeLevel    = _volumeSecurity.VolumeLevel;

                VolumePropGrid.SelectedObject =  infoEx;
            }
        }

        private SimpleSecurityInfo SelectedExpirationProp
        {
            get
            {
                return ( SimpleSecurityInfo ) ExpirationPropGrid.SelectedObject;
            }

            set
            {
                ExpirationPropGrid.SelectedObject = value;
            }
            
        }

        private SimpleSecurityInfoEx SelectedVolumeProp
        {
            get
            {
                return ( SimpleSecurityInfoEx ) VolumePropGrid.SelectedObject;
            }

            set
            {
                VolumePropGrid.SelectedObject = value;
            }

        }
        
        

        private void OnAddRowClicked( object sender, RoutedEventArgs e )
        {            
            var secJump = new SecurityJump( );

            secJump.PropertyChanged += ( s, evt ) => 
            {
                var expired = SelectedExpirationProp;

                if ( ( evt.PropertyName != "Security" ) || secJump.Security == null )
                    return;
                if ( !expired.Decimals.HasValue )
                    expired.Decimals = secJump.Security.Decimals;
                if ( !expired.PriceStep.HasValue )
                    expired.PriceStep = secJump.Security.PriceStep;
                if ( expired.VolumeStep.HasValue )
                    return;
                expired.VolumeStep = secJump.Security.VolumeStep;
            };

            JumpsGrid.Jumps.Add( secJump );

            RefreshView( );
        }
        
        private void RemoveRow_Click( object sender, RoutedEventArgs e )
        {
            int index = JumpsGrid.Jumps.IndexOf( JumpsGrid.SelectedJump );
            JumpsGrid.Jumps.RemoveRange( JumpsGrid.SelectedJumps );
            RefreshView( );

            if ( JumpsGrid.Jumps.Count <= 0 )
                return;

            JumpsGrid.SelectedJump = JumpsGrid.Jumps.Count > index ? JumpsGrid.Jumps[ index ] : JumpsGrid.Jumps.First( );
        }

        

        private void UpRow_Click( object sender, RoutedEventArgs e )
        {
            var newList = JumpsGrid.SelectedJumps.OrderBy( x => JumpsGrid.Jumps.IndexOf( x ) ).ToArray( );

            foreach ( SecurityJump securityJump in newList )
            {
                int num = JumpsGrid.Jumps.IndexOf( securityJump );

                JumpsGrid.Jumps.Remove( securityJump );
                JumpsGrid.Jumps.Insert( num - 1, securityJump );
            }

            RefreshView( );

        }

        private void DownRow_Click( object sender, RoutedEventArgs e )
        {
            var newList = JumpsGrid.SelectedJumps.OrderByDescending( x => JumpsGrid.Jumps.IndexOf( x ) ).ToArray( );

            foreach ( SecurityJump securityJump in newList )
            {
                int num = JumpsGrid.Jumps.IndexOf( securityJump );

                JumpsGrid.Jumps.Remove( securityJump );
                JumpsGrid.Jumps.Insert( num + 1, securityJump );
            }

            RefreshView( );
        }

        private void Ok_Click( object sender, RoutedEventArgs e )
        {            
            Security security = Security;

            if ( security.Id.IsEmpty(  ) )
            {
                string text = SecurityId.Text;

                if ( SecurityStorage.LookupById( text ) != null )
                {
                    new MessageBoxBuilder( )
                            .Text( LocalizedStrings.Str2927Params.Put( text ) )
                            .Owner( this )
                            .Warning( )
                            .Show( );

                    return;
                }

                string validIdString = TraderHelper.ValidateId( ref text );

                if ( validIdString != null )
                {
                    new MessageBoxBuilder( )
                    .Owner( this )
                    .Warning( )
                    .Text( validIdString )
                    .Show( );

                    return;
                }

                SecurityId securityId = text.ToSecurityId( );
                security.Id = text;
                Security.Code = securityId.SecurityCode;
                Security.Board = ExchangeInfoProvider.GetOrCreateBoard( securityId.BoardCode );
            }

            if ( TabCtrl.SelectedContainer == VolTab )
            {
                var selectedVol = SelectedVolumeProp;

                if ( selectedVol.VolumeLevel.Value == Decimal.Zero )
                {
                    new MessageBoxBuilder( )
                            .Text( LocalizedStrings.VolumeMustBeGreaterThanZero )
                            .Owner( this )
                            .Warning( )
                            .Show( );
                    
                    return;
                }

                _volumeSecurity.IsOpenInterest = selectedVol.IsOpenInterest;
                _volumeSecurity.VolumeLevel = selectedVol.VolumeLevel;
                _volumeSecurity.InnerSecurities.Clear( );
                _volumeSecurity.InnerSecurities.AddRange( VolSecGrid.Securities.Select( s => s.ToSecurityId( ) ) );

                security.BasketCode       = _volumeSecurity.BasketCode;
                security.BasketExpression = _volumeSecurity.BasketExpression;
                security.PriceStep        = selectedVol.PriceStep ;
                security.VolumeStep       = selectedVol.VolumeStep;
                security.Decimals         = selectedVol.Decimals;
            }
            else
            {                
                string valMsg = JumpsGrid.Validate();

                if ( valMsg != null )
                {
                    new MessageBoxBuilder( )
                            .Text( valMsg )
                            .Owner( this )
                            .Warning( )
                            .Show( );
                    return;
                }

                var mySecurity = JumpsGrid.Jumps[ 0 ].Security;
                _expirationSecurity.ExpirationJumps.Clear( );

                _expirationSecurity.ExpirationJumps.AddRange( JumpsGrid.Jumps.Select( j => new KeyValuePair<SecurityId, DateTimeOffset>( j.Security.ToSecurityId( ), j.Date.ApplyTimeZone( mySecurity.Board.TimeZone ) ) ) );

                security.BasketCode       = _expirationSecurity.BasketCode;
                security.BasketExpression = _expirationSecurity.BasketExpression;

                var selectedObject        = SelectedExpirationProp;
                security.PriceStep        = selectedObject.PriceStep;
                security.VolumeStep       = selectedObject.VolumeStep;
                security.Decimals         = selectedObject.Decimals;
            }            

            SecurityStorage.Save( security, true );
            DialogResult = true;
        }

        private void SecurityId_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            TryEnableOk( );
        }

        private void TryEnableOk( )
        {
            Ok.IsEnabled = !SecurityId.Text.IsEmpty( ) && ( ( TabCtrl.SelectedContainer == VolTab ) ? VolSecGrid.Securities.Any( ) : !JumpsGrid.Jumps.IsEmpty( ) );
        }

        private void JumpsGrid_JumpSelected( object obj )
        {
            RemoveRow.IsEnabled = JumpsGrid.SelectedJump != null;
            RefreshView( );
        }

        private void RefreshView( )
        {
            if ( JumpsGrid.SelectedJump != null )
            {
                int count         = JumpsGrid.Jumps.Count - 1;
                UpRow.IsEnabled = JumpsGrid.SelectedJumps.All<SecurityJump>( x => JumpsGrid.Jumps.IndexOf( x ) > 0 );
                DownRow.IsEnabled = JumpsGrid.SelectedJumps.All<SecurityJump>( x => JumpsGrid.Jumps.IndexOf( x ) < count );
            }
            else
            {
                DownRow.IsEnabled = false;
                UpRow.IsEnabled = false;
            }
        }

        private void JumpsGrid_Changed( )
        {
            TryEnableOk( );
        }

        private void AssetCode_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Auto.IsEnabled = !StringHelper.IsEmpty( AssetCode.Text );
        }

        private void Auto_Click( object sender, RoutedEventArgs e )
        {
            Security[] newSecurities = (Security[]) ArrayHelper.Empty<Security>();
            string[] split = StringHelper.Split(AssetCode.Text, "@", true);

            if ( split.Any( ) && split.Length != 0 )
            {
                string str = split[0];

                if ( ! str.IsEmpty(  ) )
                {                    
                    newSecurities = TraderHelper.GetFortsJumps( _expirationSecurity, ( ISecurityProvider ) SecurityStorage, str, DateTime.Today - TimeSpan.FromTicks( TimeHelper.TicksPerYear * 10 ), DateTime.Today, false ).ToArray<Security>( );
                }
            }

            if ( !newSecurities.IsEmpty( ) )
            {
                JumpsGrid.Jumps.Clear( );
                JumpsGrid.Jumps.AddRange( newSecurities.Select( s => new SecurityJump
                {
                    Security = s,
                    Date = s.ExpiryDate != null ? s.ExpiryDate.Value.UtcDateTime : DateTime.UtcNow.Date
                } ) );


                var selectedObject = SelectedExpirationProp;
                selectedObject.PriceStep = newSecurities[ 0 ].PriceStep;
                selectedObject.VolumeStep = newSecurities[ 0 ].VolumeStep;
                selectedObject.Decimals = newSecurities[ 0 ].Decimals;
            }
            else
            {
                new MessageBoxBuilder( )
                                        .Owner( this )
                                        .Error( )
                                        .Text( LocalizedStrings.Str1456 )
                                        .Show( );
            }
        }


        

        private void TabCtrl_SelectionChanged( object sender, TabControlSelectionChangedEventArgs e )
        {
            if ( Ok == null )
            {
                return;
            }

            TryEnableOk( );
        }

        

        

        StockSharp.BusinessEntities.Security ISecurityWindow.Security
        {
            get
            {
                return ( StockSharp.BusinessEntities.Security ) Security;
            }
            set
            {
                Security = ( ContinuousSecurity ) value;
            }
        }

        private void VolSecGrid_SelectedItemChanged( object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e )
        {
            RemoveRowVolSecGrid.IsEnabled = VolSecGrid.SelectedSecurity != null;
        }

        private void AddRowVolSecGrid_Click( object sender, RoutedEventArgs e )
        {
            SecurityPickerWindow securityPickerWindow = new SecurityPickerWindow() { SecurityProvider = (ISecurityProvider) SecurityStorage };
            CollectionHelper.AddRange<Security>( securityPickerWindow.ExcludeSecurities, VolSecGrid.Securities );

            if ( !securityPickerWindow.ShowModal( this ) )
            {
                return;
            } 
            
            VolSecGrid.Securities.AddRange( securityPickerWindow.SelectedSecurities );
        }

        private void RemoveRowVolSecGrid_Click( object sender, RoutedEventArgs e )
        {
            VolSecGrid.Securities.RemoveRange( ( IEnumerable<Security> ) ( ( IEnumerable<Security> ) VolSecGrid.SelectedSecurities ).ToArray<Security>( ) );
        }
    }
}
