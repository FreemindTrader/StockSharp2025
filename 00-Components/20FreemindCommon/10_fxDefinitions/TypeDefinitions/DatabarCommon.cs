using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public enum PivotPointReactionType
    {
        NoTouchingAnything = 0,
        ExtremeTouch       = 1,
        WithinTail_SINGLE = 10,
        WithinTail_MULTIP = 11,
        WithinBody_SINGLE = 20,
        WithinBody_MULTIP = 21,
    }



    public enum DataBarUpdateType
    {
        // In order of importance.
        None,
        Initial,
        NewPeriod,
        HistoryUpdate,
        CurrentBarUpdate,
        Reloaded
    }

    public enum DivergenceBoolean
    {
        False = 0,
        True = 1,
        CloselyEqual = 2
    }


    public delegate void NullBarUpdatedDelegate(DataBarUpdateType updateType, IAskBidBar changedBar );

    public class NullBarEventArgs : EventArgs
    {
        
        public DataBarUpdateType UpdateType
        {
            get { return _updateType; }
            set
            {
                _updateType = value;
            }
        }

        
        public IAskBidBar ChangedBar
        {
            get { return _changedBar; }
            set
            {
                _changedBar = value;
            }
        }
        

        IAskBidBar _changedBar;
        DataBarUpdateType _updateType;
        

        public NullBarEventArgs( DataBarUpdateType updateType, IAskBidBar changedBar )
        {
            _updateType = updateType;
            _changedBar = changedBar;
        }        
    }
}
