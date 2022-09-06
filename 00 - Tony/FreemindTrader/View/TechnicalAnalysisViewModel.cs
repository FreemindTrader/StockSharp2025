using DevExpress.Mvvm;
using Ecng.ComponentModel;
using Ecng.Xaml;
using fx.Algorithm;
using fx.Collections;
using fx.Definitions;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase = DevExpress.Mvvm.ViewModelBase;

namespace FreemindTrader
{
    public class TechnicalAnalysisViewModel : ViewModelBase, ISupportParameter
    {
        private Security _symbol;        
        private AdvancedAnalysisResult _taResult;

        public TechnicalAnalysisViewModel( )
        {
            Messenger.Default.Register<SelectSecurityMessage>( this, x => OnSelectSecurityMessage( x ) );
        }

        private void OnSelectSecurityMessage( SelectSecurityMessage x )
        {
            _symbol = x.Symbol;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _symbol );

            if ( aa != null )
            {
                AaResult = aa.GetAnalysisResult( _symbol.Code );
            }
            else
            {
                return;
            }             

            RaisePropertyChanged( nameof( BarPercentageItemSource ) );
        }

        public AdvancedAnalysisResult AaResult
        {
            get { return _taResult; }
            set
            {
                SetValue( ref _taResult, value );
            }
        }

        public ObservableCollectionEx<FxBarPercentage> BarPercentageItemSource
        {

            get
            {
                var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _symbol );

                if ( aa == null )
                {
                    return null;
                }
                
                var barPrecentage = aa.BarPercentageItemSource;

                return barPrecentage;
            }
        }        
    }
}
