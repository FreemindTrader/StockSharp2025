using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class SecurityCreateWindow : DXWindow, IComponentConnector, ISecurityWindow
    {
        private ISecurityStorage _securityStorage;
        private Security _security;
        private IEnumerable<Security> _securities;        

        public SecurityCreateWindow( )
        {
            InitializeComponent();
            Security security = new Security();
            security.Board =  ExchangeBoard.Nasdaq;
            Security = security;
        }

        public ISecurityStorage SecurityStorage
        {
            get
            {
                return _securityStorage;
            }
            set
            {
                _securityStorage = value;
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
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                _security = security;
                SecurityId.IsReadOnly = true;
                PropertyGrid.SelectedObject = value.Clone();
                if ( !value.Id.IsEmpty(  ) )
                {
                    Title = LocalizedStrings.Str1545Params.Put( value );
                }
                
                ( ( INotifyPropertyChanged ) PropertyGrid.SelectedObject ).PropertyChanged += new PropertyChangedEventHandler( method_0 );

                GetBoardCode();
            }
        }

        private void method_0( object sender, PropertyChangedEventArgs e )
        {
            if ( !( e.PropertyName == "Code" ) && !( e.PropertyName == "Board" ) )
            {
                return;
            }

            GetBoardCode();
        }

        private Security SelectedSecurity
        {
            get
            {
                return ( Security ) PropertyGrid.SelectedObject;
            }
            
        }

        private void GetBoardCode( )
        {
            string strBoard = ( SelectedSecurity.Board == null ) ? string.Empty : ( SelectedSecurity.Board.Code ?? string.Empty ) ;

            SecurityId.Text = SelectedSecurity.Code + "@" + strBoard;
        }

        public IEnumerable<Security> Securities
        {
            get
            {
                return _securities;
            }
            set
            {                
                if ( value == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                Security security = value.FirstOrDefault<Security>();
                if ( security == null )
                {
                    throw new ArgumentOutOfRangeException();
                }

                if ( value.Count() == 1 )
                {
                    Security = security;
                }
                else
                {
                    _securities = value;

                    var first = value.First<Security>();

                    PropertyGrid.SelectedObject = ( new SecurityItem()
                    {
                        VolumeStep           = ( value.All( x => x.VolumeStep.HasValue     && first.VolumeStep.HasValue     && ( x.VolumeStep.Value == first.VolumeStep.Value         ) ) ? first.VolumeStep :    new Decimal?() ),
                        PriceStep            = ( value.All( x => x.PriceStep.HasValue      && first.PriceStep.HasValue      && ( x.PriceStep.Value == first.PriceStep.Value           ) ) ? first.PriceStep:      new Decimal?() ),
                        Decimals             = ( value.All( x => x.Decimals.HasValue       && first.Decimals.HasValue       && ( x.Decimals.Value == first.Decimals.Value             ) ) ? first.Decimals:       new int?() ),
                        Type                 = ( value.All( x => x.Type.HasValue           && first.Type.HasValue           && ( x.Type.Value == first.Type.Value                     ) ) ? first.Type:           new SecurityTypes?() ),
                        UnderlyingSecurityId = ( value.All( x => StringHelper.CompareIgnoreCase( x.UnderlyingSecurityId, first.UnderlyingSecurityId                                   ) ) ? first.UnderlyingSecurityId: ( string ) null ),
                        SettlementDate       = ( value.All( x => x.SettlementDate.HasValue && first.SettlementDate.HasValue && ( x.SettlementDate.Value == first.SettlementDate.Value ) ) ? first.SettlementDate: new DateTimeOffset?() ),
                        ExpiryDate           = ( value.All( x => x.ExpiryDate.HasValue     && first.ExpiryDate.HasValue     && ( x.ExpiryDate.Value == first.ExpiryDate.Value         ) ) ? first.ExpiryDate:     new DateTimeOffset?() ),
                        OptionType           = ( value.All( x => x.OptionType.HasValue     && first.OptionType.HasValue     && ( x.OptionType.Value == first.OptionType.Value         ) ) ? first.OptionType:     new OptionTypes?() ),
                        Multiplier           = ( value.All( x => x.Multiplier.HasValue     && first.Multiplier.HasValue     && ( x.Multiplier.Value == first.Multiplier.Value         ) ) ? first.Multiplier:     new Decimal?() ),
                        Currency             = ( value.All( x => x.Currency.HasValue       && first.Currency.HasValue       && ( x.Currency.Value == first.Currency.Value             ) ) ? first.Currency:       new CurrencyTypes?() )
                    } );
                }
            }
        }

        

        private void SecurityId_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            Ok.IsEnabled = ! SecurityId.Text.IsEmpty( );
        }

        private void ShowMessage( string message )
        {
            int num = (int) new MessageBoxBuilder().Text(message).Owner((Window) this).Warning().Show();
        }

        private void OnOkayButtonClicked( object sender, RoutedEventArgs e )
        {
            var item = PropertyGrid.SelectedObject as SecurityCreateWindow.SecurityItem;

            if ( item == null )
            {
                var security = SelectedSecurity;

                if ( security.Code.IsEmpty( ) )
                {
                    ShowMessage( LocalizedStrings.Str2923 );
                    return;
                }
                if (  security.Board == null  )
                {
                    ShowMessage( LocalizedStrings.Str2926 );
                    return;
                }

                Decimal? priceStep = security.PriceStep;                 
                
                if ( priceStep.HasValue )
                {                                        
                    if ( priceStep.Value != 0 || ! priceStep.HasValue )
                    {
                        Decimal? volumeStep = security.VolumeStep;

                        if ( volumeStep.HasValue )
                        {                            
                            if ( volumeStep.Value != 0 || ! volumeStep.HasValue )
                            {
                                if ( security.Id.IsEmpty( ) )
                                {
                                    string id = new SecurityIdGenerator().GenerateId(security.Code, security.Board);

                                    if ( SecurityStorage.LookupById( id ) != null )
                                    {
                                        ShowMessage( LocalizedStrings.Str2927Params.Put(  id ) );

                                        return;
                                    }

                                    security.Id  = id;
                                }
                                
                                SecurityStorage.Save( Security, true );
                                DialogResult = true;

                                return;
                            }
                        }
                        else
                        {
                            ShowMessage( LocalizedStrings.Str2924 );
                        }
                        
                        return;
                    }
                }
                ShowMessage( LocalizedStrings.Str2925 );
                return;
            }
            
            if ( item.PriceStep.HasValue && item.PriceStep.Value == 0 )
            {
                ShowMessage( LocalizedStrings.Str2925 );
                return;
            }
            
            if ( item.VolumeStep.HasValue && item.VolumeStep.Value == 0 )
            {
                ShowMessage( LocalizedStrings.Str2924 );
                return;
            }

            foreach ( Security mySecurity in Securities )
            {
                if ( item.VolumeStep.HasValue )
                {
                    mySecurity.VolumeStep =  item.VolumeStep.Value;
                }
                if ( item.PriceStep.HasValue )
                {
                    mySecurity.PriceStep =   item.PriceStep.Value ;
                }
                if ( item.Decimals.HasValue )
                {
                    mySecurity.Decimals =  item.Decimals.Value;
                }
                if ( item.Type.HasValue )
                {
                    mySecurity.Type =  item.Type.Value;
                }
                if ( item.UnderlyingSecurityId != null )
                {
                    mySecurity.UnderlyingSecurityId = item.UnderlyingSecurityId;
                }
                if ( item.OptionType.HasValue )
                {
                    mySecurity.OptionType = item.OptionType.Value;
                }
                if ( item.ExpiryDate.HasValue )
                {
                    mySecurity.ExpiryDate =   item.ExpiryDate.Value;
                }
                if ( item.SettlementDate.HasValue )
                {
                    mySecurity.SettlementDate =  item.SettlementDate.Value;
                }
                if ( item.Currency.HasValue )
                {
                    mySecurity.Currency =  item.Currency.Value;
                }
                if ( item.Multiplier.HasValue )
                {
                    mySecurity.Multiplier =  item.Multiplier.Value;
                }

                SecurityStorage.Save( mySecurity, true );
            }
            
            DialogResult = true;
        }

        

        

        private sealed class SecurityItem : NotifiableObject
        {
            private SecurityTypes?  _type;
            private CurrencyTypes?  _currency;
            private Decimal?        _priceStep;
            private Decimal?        _volumeStep;
            private Decimal?        _multiplier;
            private int?            _decimals;
            private DateTimeOffset? _expiryDate;
            private DateTimeOffset? _settlementDate;
            private string          _securityId;
            private OptionTypes?    _optionType;

            public SecurityItem( )
            {
                
            }

            [Display( Description = "Str360", GroupName = "General", Name = "Type", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
            public SecurityTypes? Type
            {
                get
                {
                    return _type;
                }
                set
                {
                    SecurityTypes? nullable0 = _type;
                    SecurityTypes? nullable = value;
                    if ( ( nullable0.GetValueOrDefault() == nullable.GetValueOrDefault() ? ( nullable0.HasValue == nullable.HasValue ? 1 : 0 ) : 0 ) != 0 )
                    {
                        return;
                    }

                    _type = value;
                    NotifyChanged( nameof( Type ) );
                }
            }

            [Display( Description = "Str382", GroupName = "General", Name = "Currency", Order = 6, ResourceType = typeof( LocalizedStrings ) )]
            public CurrencyTypes? Currency
            {
                get
                {
                    return _currency;
                }
                set
                {
                    _currency = value;
                    NotifyChanged( nameof( Currency ) );
                }
            }

            [GreaterThanZero]
            [Display( Description = "MinPriceStep", GroupName = "General", Name = "PriceStep", Order = 9, ResourceType = typeof( LocalizedStrings ) )]
            public Decimal? PriceStep
            {
                get
                {
                    return _priceStep;
                }
                set
                {
                    Decimal? nullable2 = _priceStep;
                    Decimal? nullable = value;
                    if ( ( nullable2.GetValueOrDefault() == nullable.GetValueOrDefault() ? ( nullable2.HasValue == nullable.HasValue ? 1 : 0 ) : 0 ) != 0 )
                    {
                        return;
                    }

                    nullable = value;
                    Decimal num = new Decimal();
                    if ( ( nullable.GetValueOrDefault() < num ? ( nullable.HasValue ? 1 : 0 ) : 0 ) != 0 )
                    {
                        throw new ArgumentOutOfRangeException( nameof( value ) );
                    }

                    _priceStep = value;
                    NotifyChanged( nameof( PriceStep ) );
                }
            }

            [Display( Description = "Str366", GroupName = "General", Name = "VolumeStep", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
            [GreaterThanZero]
            public Decimal? VolumeStep
            {
                get
                {
                    return _volumeStep;
                }
                set
                {
                    Decimal? nullable3 = _volumeStep;
                    Decimal? nullable = value;
                    if ( ( nullable3.GetValueOrDefault() == nullable.GetValueOrDefault() ? ( nullable3.HasValue == nullable.HasValue ? 1 : 0 ) : 0 ) != 0 )
                    {
                        return;
                    }

                    nullable = value;
                    Decimal num = new Decimal();
                    if ( ( nullable.GetValueOrDefault() < num ? ( nullable.HasValue ? 1 : 0 ) : 0 ) != 0 )
                    {
                        throw new ArgumentOutOfRangeException( nameof( value ) );
                    }

                    _volumeStep = value;
                    NotifyChanged( nameof( VolumeStep ) );
                }
            }

            [Display( Description = "LotVolume", GroupName = "General", Name = "Str330", Order = 11, ResourceType = typeof( LocalizedStrings ) )]
            public Decimal? Multiplier
            {
                get
                {
                    return _multiplier;
                }
                set
                {
                    Decimal? nullable4 = _multiplier;
                    Decimal? nullable = value;
                    if ( ( nullable4.GetValueOrDefault() == nullable.GetValueOrDefault() ? ( nullable4.HasValue == nullable.HasValue ? 1 : 0 ) : 0 ) != 0 )
                    {
                        return;
                    }

                    nullable = value;
                    Decimal num = new Decimal();
                    if ( ( nullable.GetValueOrDefault() < num ? ( nullable.HasValue ? 1 : 0 ) : 0 ) != 0 )
                    {
                        throw new ArgumentOutOfRangeException( nameof( value ) );
                    }

                    _multiplier = value;
                    NotifyChanged( nameof( Multiplier ) );
                }
            }

            [Display( Description = "Str548", GroupName = "General", Name = "Decimals", Order = 12, ResourceType = typeof( LocalizedStrings ) )]
            public int? Decimals
            {
                get
                {
                    return _decimals;
                }
                set
                {
                    int? nullable5 = _decimals;
                    int? nullable = value;
                    if ( ( nullable5.GetValueOrDefault() == nullable.GetValueOrDefault() ? ( nullable5.HasValue == nullable.HasValue ? 1 : 0 ) : 0 ) != 0 )
                    {
                        return;
                    }

                    nullable = value;
                    if ( ( nullable.GetValueOrDefault() < 0 ? ( nullable.HasValue ? 1 : 0 ) : 0 ) != 0 )
                    {
                        throw new ArgumentOutOfRangeException( nameof( value ) );
                    }

                    _decimals = value;
                    NotifyChanged( nameof( Decimals ) );
                }
            }

            [Display( Description = "Str371", GroupName = "General", Name = "ExpiryDate", Order = 13, ResourceType = typeof( LocalizedStrings ) )]
            public DateTimeOffset? ExpiryDate
            {
                get
                {
                    return _expiryDate;
                }
                set
                {
                    DateTimeOffset? nullable6 = _expiryDate;
                    DateTimeOffset? nullable = value;
                    if ( ( nullable6.HasValue == nullable.HasValue ? ( nullable6.HasValue ? ( nullable6.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0 ) : 1 ) : 0 ) != 0 )
                    {
                        return;
                    }

                    _expiryDate = value;
                    NotifyChanged( nameof( ExpiryDate ) );
                }
            }

            [Display( Description = "Str373", GroupName = "General", Name = "SettlementDate", Order = 14, ResourceType = typeof( LocalizedStrings ) )]
            public DateTimeOffset? SettlementDate
            {
                get
                {
                    return _settlementDate;
                }
                set
                {
                    DateTimeOffset? nullable7 = _settlementDate;
                    DateTimeOffset? nullable = value;
                    if ( ( nullable7.HasValue == nullable.HasValue ? ( nullable7.HasValue ? ( nullable7.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0 ) : 1 ) : 0 ) != 0 )
                    {
                        return;
                    }

                    _settlementDate = value;
                    NotifyChanged( nameof( SettlementDate ) );
                }
            }

            [Display( Description = "Str550", GroupName = "Str437", Name = "UnderlyingAsset", Order = 100, ResourceType = typeof( LocalizedStrings ) )]
            public string UnderlyingSecurityId
            {
                get
                {
                    return _securityId;
                }
                set
                {
                    if ( _securityId == value )
                    {
                        return;
                    }

                    _securityId = value;
                    NotifyChanged( nameof( UnderlyingSecurityId ) );
                }
            }

            [Display( Description = "OptionContractType", GroupName = "Str437", Name = "Str551", Order = 101, ResourceType = typeof( LocalizedStrings ) )]
            public OptionTypes? OptionType
            {
                get
                {
                    return _optionType;
                }
                set
                {
                    OptionTypes? nullable8 = _optionType;
                    OptionTypes? nullable = value;
                    if ( ( nullable8.GetValueOrDefault() == nullable.GetValueOrDefault() ? ( nullable8.HasValue == nullable.HasValue ? 1 : 0 ) : 0 ) != 0 )
                    {
                        return;
                    }

                    _optionType = value;
                    NotifyChanged( nameof( OptionType ) );
                }
            }
        }
    }
}
