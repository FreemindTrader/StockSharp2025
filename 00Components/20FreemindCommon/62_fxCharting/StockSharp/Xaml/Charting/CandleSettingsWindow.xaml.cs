using DevExpress.Xpf.Core;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Candles;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Markup;

#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting
{
    public partial class CandleSettingsWindow : DXWindow, IComponentConnector
    {
        private CandleSeriesItem _candleSeriesItem = new CandleSeriesItem( );

        public CandleSettingsWindow( )
        {
            InitializeComponent( );
        }

        public CandleSeries Series
        {
            get
            {
                if ( _candleSeriesItem == null )
                {
                    return null;
                }
                    
                CandleSeries selectedObject = ( CandleSeries )SeriesCtrl.SelectedObject;
                CandleSeries candleSeries   = new CandleSeries( );
                candleSeries.Load( selectedObject.Save( ) );
                candleSeries.Security       = ( selectedObject.Security );
                candleSeries.CandleType     = ( _candleSeriesItem.CandleSeries.CandleType );
                candleSeries.Arg            = ( _candleSeriesItem.CandleSeries.Arg );
                return candleSeries;
            }
            set
            {
                if ( value != null )
                {
                    _candleSeriesItem = new CandleSeriesItem( )
                    {
                        CandleSeries = value
                    };
                    
                    _candleSeriesItem.Load( value.Save( ) );
                    
                    if ( !_candleSeriesItem.From.HasValue && !_candleSeriesItem.To.HasValue )
                    {
                        _candleSeriesItem.From = ( new DateTimeOffset?( DateTime.Today.Subtract( TimeSpan.FromDays( 10.0 ) ) ) );
                    }
                        
                }
                else
                {
                    _candleSeriesItem = null;
                }
                    
                SeriesCtrl.SelectedObject = _candleSeriesItem;
                OkBtn.IsEnabled = _candleSeriesItem != null;
            }
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return SeriesCtrl.SecurityProvider;
            }
            set
            {
                SeriesCtrl.SecurityProvider = ( value );
            }
        }

        private void OkButtonClicked( object sender, RoutedEventArgs e )
        {
            CandleSeries series = Series;
            if ( series.Security == null )
            {
                new MessageBoxBuilder( ).Owner( this ).Error( ).Text( "LocalizedStrings.Str3252" ).Show( );
            }
            else
            {
                DateTimeOffset? startDate = series.From;
                DateTimeOffset? endDate = series.To;

                if ( startDate.HasValue && endDate.HasValue && ( startDate.GetValueOrDefault( ) > endDate.GetValueOrDefault( ) ) )
                {
                    new MessageBoxBuilder( ).Owner( this ).Error( ).Text( StringHelper.Put( "LocalizedStrings.Str1119Params", new object[ 2 ] { series.From, series.To } ) ).Show( );
                }
                else
                {
                    DialogResult = true;
                }
            }
        }



        private sealed class CandleSeriesItem : CandleSeries
        {
            private CandleSeries _candleSeries;

            public CandleSeriesItem( ) : base( )
            {

            }

            [Display( Description = "CandlesType", GroupName = "General", Name = "CandlesType", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
            public CandleSeries CandleSeries
            {
                get
                {
                    return _candleSeries;
                }
                set
                {
                    _candleSeries = value;
                }
            }

            //[Editor( typeof( BuildCandlesFromEditor ), typeof( BuildCandlesFromEditor ) )]
            //[Display( Description = "CandlesBuildSource", GroupName = "Build", Name = "Str213", Order = 21, ResourceType = typeof( LocalizedStrings ) )]
            //public new Messages.DataType BuildCandlesFrom2
            //{
            //    get
            //    {
            //        return base.BuildCandlesFrom2;
            //    }
            //    set
            //    {
            //        base.BuildCandlesFrom2 = ( value );
            //    }
            //}

            //[Editor( typeof( BuildCandlesFieldEditor ), typeof( BuildCandlesFieldEditor ) )]
            [Display( Description = "Level1Field", GroupName = "Build", Name = "Str748", Order = 22, ResourceType = typeof( LocalizedStrings ) )]
            public new Level1Fields? BuildCandlesField
            {
                get
                {
                    return base.BuildCandlesField;
                }
                set
                {
                    base.BuildCandlesField = ( value );
                }
            }
        }
    }
}
