using fx.Collections;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Ecng.Xaml;
using fx.Algorithm;
using fx.Definitions;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase = DevExpress.Mvvm.ViewModelBase;
using System.Collections.ObjectModel;
using Ecng.ComponentModel;

namespace FreemindTrader
{
    public class TradingEventViewModel : ViewModelBase
    {
        private string _symbol;
        public TradingEventViewModel( )
        {
            Messenger.Default.Register<SelectSecurityMessage>( this, x => OnSelectSecurityMessage( x ) );
        }

        private void OnSelectSecurityMessage( SelectSecurityMessage x )
        {
            _symbol = x.Symbol.Code;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _symbol );

            if ( aa == null )
            {
                return;
            }

            aa.InitializeTechnicalAnalysis( x.SelectedTF );

            RaisePropertyChanged( nameof( TradingEventsItemSource ) );
        }        

        public ObservableCollectionEx<FxTradingEvents> TradingEventsItemSource
        {
            get
            {
                if ( _symbol != null )
                {                    
                    var tradingEventList = (( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _symbol )).TradingEventsItemSource;

                    return tradingEventList;
                }

                return null;
            }
        } 
        
        //public PooledList< > 
    }

    public class CandlesData : PooledList<CandleImage>
    {
        static PooledList<CandleImage> dataSource = null;
        public static PooledList<CandleImage> DataSource
        {
            get
            {
                if ( dataSource != null )
                    return dataSource;
                //XmlSerializer s = new XmlSerializer( typeof( CountriesData ) );
                //Assembly assembly = typeof( CountriesData ).Assembly;
                //dataSource = ( PooledList<CandleImage> )s.Deserialize( assembly.GetManifestResourceStream( DemoHelper.GetPath( "GridDemo.Data.", assembly ) + "Countries.xml" ) );
                return dataSource;
            }
        }
    }

    public class CandleImage
    {
        public string ActualNWindName { get { return NWindName ?? Name; } }
        public string ActualName { get { return Name ?? NWindName; } }
        public string Name { get; set; }
        public string NWindName { get; set; }
        public byte[ ] Flag { get; set; }
    }
}


