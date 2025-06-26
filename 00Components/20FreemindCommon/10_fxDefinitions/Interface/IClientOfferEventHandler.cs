using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IClientOfferEventHandler
    {
        void OnOfferUpdatedEvent( object sender, ItemEventArgs< IOffer > offer );

        event NullBarUpdatedDelegate    NullbarUpdateEvent;
        event NullBarUpdatedDelegate    NullbarAddedEvent;

        

        IAskBidBar CurrentNullBar { get; }

        

        TimeSpan RequestUpdatePeriod { get; set; }




        void SuspendProcessing( );
        void SetConfirmedTick( DateTime confirmedTicks, int lastProcessedIndex, int tickPerBarCount, int howManyTicksPerBar );
        void FinishDownloadHistoricBars( );
        void FinishDownloadHistoricBars( DateTime lastTickTime, int tickPerBarCount );
    }
}
