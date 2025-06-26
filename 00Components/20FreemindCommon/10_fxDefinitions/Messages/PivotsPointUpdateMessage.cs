using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum ShowPivotPoints
    {
        SHOWNONE = 0,
        SHOWTODAY = 1,
        SHOWLAST2DAYS = 2,
        SHOWLAST3DAYS = 3,
        SHOWLAST4DAYS = 4,
        SHOWALL = 99,
        SHOW_SELECTEBAR = 100
    }

    public enum TradingMode
    {
        NONE = 0,
        LIVETRADING = 1,
        BACKTESTING = 2,        
    }

    public class TradingApiDoneMessage
    {
        public TradingApiDoneMessage( int ClickCounts )
        {            
            this.ClickCounts = ClickCounts;            
        }

        public int ClickCounts { get; private set; }
    }

    [Serializable]
    public class PivotsPointUpdateMessage
    {// Fields...

        public ShowPivotPoints PivotPointsShowType { get; set; }        
        public string Symbol { get; set; }
        public double Opacity { get; set; }


        long _selectedBarTime;

        public long SelectedBarTime
        {
            get { return _selectedBarTime; }
            set
            {
                _selectedBarTime = value;
            }
        }
        
        public PivotsPointUpdateMessage( string symbol, ShowPivotPoints pivotPointsShowType )
        {
            Symbol = symbol;

            PivotPointsShowType = pivotPointsShowType;

            _selectedBarTime = -1;
        }

        public PivotsPointUpdateMessage( string symbol, ShowPivotPoints pivotPointsShowType, long selectedBarTime )
        {
            Symbol = symbol;

            PivotPointsShowType = pivotPointsShowType;

            _selectedBarTime = selectedBarTime;
        }
    }

    public class PivotsPointOpacityMessage
    {
        public Security Symbol { get; set; }
        public double Opacity { get; set; }
        public TimeSpan Period { get; set; }

        public PivotsPointOpacityMessage( Security symbol, double opacity, TimeSpan period )
        {
            Symbol  = symbol;
            Period  = period;
            Opacity = opacity;            
        }        
    }
}




