using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo.Candles;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml.PropertyGrid
{
    public partial class CandleSettingsEditor : DropDownButton
    {
        private static readonly IDictionary<Type, string> _candleTypes = new Dictionary<Type, string>();
        private readonly Dictionary<Type, FrameworkElement> _visibility = new Dictionary<Type, FrameworkElement>();
        private bool _initializing;
        private bool _isLoaded;

        public event Action SettingsChanged;

        static CandleSettingsEditor( )
        {
            foreach ( var type in new[ ]
                                        {
                                            typeof(TimeFrameCandle),
                                            typeof(TickCandle),
                                            typeof(VolumeCandle),
                                            typeof(RangeCandle),
                                            typeof(RenkoCandle),
                                            typeof(PnFCandle)
                                        } 
                    )
            {
                _candleTypes.Add( type, type.GetDisplayName() );
            }
        }

        public CandleSettingsEditor( )
        {
            InitializeComponent();

            CandleType.ItemsSource = _candleTypes;

            _visibility.Add( typeof( TimeFrameCandle ), TimeFramePanel );
            _visibility.Add( typeof( TickCandle ), IntValuePanel );
            _visibility.Add( typeof( VolumeCandle ), DecimalValuePanel );
            _visibility.Add( typeof( RangeCandle ), UnitValuePanel );
            _visibility.Add( typeof( RenkoCandle ), UnitValuePanel );
            _visibility.Add( typeof( PnFCandle ), PnfValuePanel );

            //Settings = new CandleSeries
            //{
            //    CandleType = typeof( TimeFrameCandle ),
            //    Arg = TimeSpan.FromMinutes( 1 ),
            //};

            UnitValue.Value  = new Unit( 1 );
            PnfBoxSize.Value = new Unit( 1 );
        }

        public static readonly DependencyProperty SettingsProperty = DependencyProperty.Register(nameof (Settings), typeof (CandleSeries), typeof (CandleSettingsEditor), new PropertyMetadata((object) null, new PropertyChangedCallback(CandleSettingsEditor.OnSettingsChanged)));

        public CandleSeries Settings
        {
            get { return ( CandleSeries ) GetValue( SettingsProperty ); }

            set
            {
                SetValue( SettingsProperty, value );
            }
        }

        private static void OnSettingsChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var editor = (CandleSettingsEditor) d;
            var settings = (CandleSeries) e.NewValue;

            editor._initializing = true;

            try
            {                
                if ( settings == null )
                {
                    editor.Content = string.Empty;
                    editor.IsEnabled = false;
                }
                else
                {
                    var candleType = settings.CandleType;

                    if ( candleType != null )
                    {
                        editor.CandleType.SelectedItem = _candleTypes.First( p => p.Key == candleType );

                        object obj = settings.Arg;

                        if ( candleType == typeof( TimeFrameCandle ) )
                        {
                            editor.TimeFrame.Value = ( TimeSpan ) settings.Arg;
                        }
                        else if ( candleType == typeof( TickCandle ) )
                        {
                            editor.IntValue.Value = settings.Arg.To<int>();
                        }
                        else if ( candleType == typeof( VolumeCandle ) )
                        {
                            editor.DecimalValue.Value = settings.Arg.To<decimal>();
                        }
                        else if ( candleType == typeof( RangeCandle ) )
                        {
                            editor.UnitValue.Value = settings.Arg.To<Unit>();
                        }
                        else if ( candleType == typeof( PnFCandle ) )
                        {
                            var value = settings.Arg.To<PnFArg>();
                            editor.PnfReversalAmount.Value = value.ReversalAmount;
                            editor.PnfBoxSize.Value = value.BoxSize;
                        }
                        else if ( candleType == typeof( RenkoCandle ) )
                        {
                            editor.UnitValue.Value = settings.Arg.To<Unit>();
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException( "e", ( object ) candleType, LocalizedStrings.WrongCandleType );
                        }
                                                                                
                        if ( editor._isLoaded )
                        {
                            editor.UpdateVisibility( candleType );
                        } ( ( ContentControl ) editor ).Content = ( object ) string.Format( "{0} {1}", ( object ) CandleSettingsEditor._candleTypes[ candleType ], obj );
                    }
                    else
                    {
                        editor.CandleType.SelectedItem = null;
                        if ( editor._isLoaded )
                        {
                            editor._visibility.Values.ForEach( x => x.Visibility = Visibility.Collapsed );
                            
                        } 
                        
                        editor.Content = string.Empty;
                    }

                    editor.RaiseSettingsChangedEvent();
                }
            }
            finally
            {
                editor._initializing = false;
            }
        }

        private void RaiseSettingsChangedEvent( )
        {
            Action myEvent = SettingsChanged;

            if ( myEvent == null )
            {
                return;
            }

            myEvent();
        }

        private void UpdateVisibility( Type ctrlType )
        {
            FrameworkElement activeCtrl = _visibility[ctrlType];
            activeCtrl.Visibility = Visibility.Visible;

            foreach ( FrameworkElement element in _visibility.Values )
            {
                if ( !Equals( activeCtrl, element ) )
                {
                    element.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UnitValue_ValueChanged( Unit obj )
        {
            FillSettingsArg();
        }

        private void CandleType_EditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            if ( _initializing )
            {
                return;
            }

            Type key = ( (KeyValuePair<Type, string>) CandleType.SelectedItem ).Key;

            if ( Settings == null )
            {
                var candleSeries = new CandleSeries();
                candleSeries.CandleType = key;
                candleSeries.Arg = GetSettingsArg( key );
                Settings = candleSeries;
            }
            else
            {
                Settings.CandleType =  key;
            }

            FillSettingsArg();
            UpdateVisibility( key );
        }

        private static object GetSettingsArg( Type candleType )
        {
            if ( candleType == typeof( TimeFrameCandle ) )
            {
                return ( object ) TimeSpan.FromMinutes( 1.0 );
            }

            if ( candleType == typeof( TickCandle ) )
            {
                return ( object ) 1;
            }

            if ( candleType == typeof( VolumeCandle ) )
            {
                return ( object ) Decimal.One;
            }

            if ( candleType == typeof( RangeCandle ) )
            {
                return ( object ) new Unit( Decimal.One );
            }

            if ( candleType == typeof( PnFCandle ) )
            {
                return ( object ) new PnFArg();
            }

            if ( !( candleType == typeof( RenkoCandle ) ) )
            {
                throw new ArgumentOutOfRangeException( "candleType", ( object ) candleType, LocalizedStrings.WrongCandleType );
            }

            return ( object ) new Unit( Decimal.One );
        }

        private void FillSettingsArg( )
        {
            if ( _initializing || Settings == null )
            {
                return;
            }

            if ( Settings.CandleType != ( Type ) null )
            {
                if ( Settings.CandleType == typeof( TimeFrameCandle ) )
                {
                    Settings.Arg = TimeFrame.Value ?? TimeSpan.Zero;
                }
                else if ( Settings.CandleType == typeof( VolumeCandle ) )
                {
                    Settings.Arg = DecimalValue.Value;
                }
                else if ( Settings.CandleType == typeof( TickCandle ) )
                {
                    Settings.Arg = IntValue.Value.To<int>(  ) ;
                }
                else if ( !( Settings.CandleType == typeof( RangeCandle ) ) && !( Settings.CandleType == typeof( RenkoCandle ) ) )
                {
                    if ( !( Settings.CandleType == typeof( PnFCandle ) ) )
                    {
                        throw new ArgumentOutOfRangeException( "Settings", ( object ) Settings.CandleType, LocalizedStrings.WrongCandleType );
                    }

                    var pnFarg            = Settings.Arg as PnFArg ?? new PnFArg();
                    pnFarg.ReversalAmount = ( ( int ) PnfReversalAmount.Value );
                    pnFarg.BoxSize        = ( PnfBoxSize.Value ?? new Unit() );
                    Settings.Arg          = pnFarg;
                }
                else
                {
                    Settings.Arg = ( UnitValue.Value ?? new Unit() );
                }
                
                Content  = string.Format( "{0} {1}", _candleTypes[ Settings.CandleType ], Settings.Arg );
            }
            else
            {                
                Content  = string.Empty;
            }

            RaiseSettingsChangedEvent();
        }

        private void OnEditValueChanged( object sender, EditValueChangedEventArgs e )
        {
            FillSettingsArg( );
        }

        private void TimeFrame_ValueChanged( TimeSpan? obj )
        {
            FillSettingsArg();
        }

        private void CandleSettingsEditorOnLoaded( object sender, RoutedEventArgs e )
        {
            _isLoaded = true;
            Type candleType = this.Settings?.CandleType;

            if ( !( candleType != ( Type ) null ) )
            {
                return;
            }

            this._visibility[ candleType ].Visibility = Visibility.Visible;
        }
    }
}
