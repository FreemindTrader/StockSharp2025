using DevExpress.Mvvm;
using Ecng.ComponentModel;
using fx.Algorithm;
using fx.Collections;
using fx.Definitions;
using System;
using System.Linq;
using ViewModelBase = DevExpress.Mvvm.ViewModelBase;

namespace FreemindTrader
{
    public class TradingEventViewModel : ViewModelBase
    {
        private string _symbol;
        public TradingEventViewModel()
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
                    var tradingEventList = ( ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _symbol ) ).TradingEventsItemSource;

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


