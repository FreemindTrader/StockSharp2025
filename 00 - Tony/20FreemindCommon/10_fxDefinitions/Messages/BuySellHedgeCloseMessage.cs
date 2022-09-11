using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum TradingSignalEnum
    {
        None = 0,
        Buy  = 1,
        Sell = 2,
        Hedge = 3,
        Close = 4
        
    }

    public class StartTrailingStopLossMessage
    {
        int _trailingStopBarCount;
        public int TrailingStopBarCount
        {
            get { return _trailingStopBarCount; }
            set
            {
                _trailingStopBarCount = value;
            }
        }

        public StartTrailingStopLossMessage( int trailngStopBarCount )
        {
            TrailingStopBarCount = trailngStopBarCount;
        }
    }

    public class BuySellHedgeCloseMessage
    {// Fields...
        private TradingSignalEnum _buyOrSell;
        private TradingSignalEnum _closeOrHedge;


        public TradingSignalEnum BuyOrSell
        {
            get { return _buyOrSell; }
            set { _buyOrSell = value; }
        }

        public bool IsBuy
        {
            get
            {
                return _buyOrSell == TradingSignalEnum.Buy;
            }
        }

        public bool IsSell
        {
            get
            {
                return _buyOrSell == TradingSignalEnum.Sell;
            }
        }

        public bool IsClose
        {
            get
            {
                return _closeOrHedge == TradingSignalEnum.Close;
            }
        }

        public bool IsHedge
        {
            get
            {
                return _closeOrHedge == TradingSignalEnum.Hedge;
            }
        }

        public BuySellHedgeCloseMessage( TradingSignalEnum buyOrSell, TradingSignalEnum closeOrHedge )
        {
            _buyOrSell = buyOrSell;
            _closeOrHedge = closeOrHedge;
        }


        
    }
}