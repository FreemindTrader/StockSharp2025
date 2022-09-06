using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using fx.Algorithm;
using fx.Definitions;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreemindTrader
{    
    public class DailyRangeViewModel : ViewModelBase
    {
        private string _security;
        
        private PeriodXTaManager _manager;

        public DailyRangeViewModel( )
        {
            Messenger.Default.Register<SelectSecurityMessage>( this, x => OnSelectSecurityMessage( x ) );
        }

        private void OnSelectSecurityMessage( SelectSecurityMessage x )
        {
            _security = x.Symbol.Code;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );
            if ( aa == null )
                return;

            TaManager = ( PeriodXTaManager ) aa.GetPeriodXTa( TimeSpan.FromDays( 1 ) );            
        }




        // Instead of using AverageDailyRange, in the XAML, the right way to use is TaManager.AverageDailyRange
        public PeriodXTaManager TaManager
        {
            get { return _manager; }
            set
            {
                SetValue( ref _manager, value );                
            }
        }

        //public double AverageDailyRange
        //{
        //    get
        //    {
        //        if ( _manager != null )
        //        {
        //            return _manager.AverageDailyRange;
        //        }

        //        return 0;
        //    }            
        //}

        //public double TodayRange
        //{
        //    get
        //    {
        //        if ( _manager != null )
        //        {
        //            return _manager.TodayRange;
        //        }

        //        return 0;
        //    }            
        //}
    }
}
