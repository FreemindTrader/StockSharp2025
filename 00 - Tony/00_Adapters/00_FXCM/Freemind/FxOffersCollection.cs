using Ecng.Collections;
using StockSharp.FxConnectFXCM.Freemind;
using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public interface IOfferCollection : IEnumerable<IOffer>
    {
        /// <summary>
        /// Get number of the offers in the collection
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the offer by its index
        /// </summary>
        /// <param name="index">The index of the offer (0 is first)</param>
        /// <returns></returns>
        IOffer this[ int index ] { get; }
    }

    public class FxOffersCollection : IOfferCollection
    {
        #region Data fields

        private static readonly Func< string , FxOffer > CreateValueDelegate = _CreateValue;
        private static FxOffer _CreateValue( string input )
        {
            return null;
        }

        /// <summary>
        /// Storage for the offers
        /// </summary>
        private readonly List< FxOffer > _offersCollection            = new List< FxOffer >( );

        /// <summary>
        /// The dictionary to search the offer by the offer id
        /// </summary>
        private readonly SynchronizedDictionary< string, FxOffer > _idToOffer         = new SynchronizedDictionary< string, FxOffer >(  );

        /// <summary>
        /// The dictionary to search the offer by the instrument name
        /// </summary>
        private readonly SynchronizedDictionary< string, FxOffer > _instrumentToOffer = new SynchronizedDictionary< string, FxOffer >(  );


        #endregion

        #region IOfferCollection members

        public int Count => _offersCollection.Count;


        public IOffer this[ int index ]
        {
            get { return _offersCollection[ index ]; }
        }

        public FxOffer this[ string key ]
        {
            get
            {
                if ( _instrumentToOffer.ContainsKey( key ) )
                {
                    return _instrumentToOffer[ key ];
                }
                

                return null;
            }
        }

        public IEnumerator<IOffer> GetEnumerator( )
        {
            return new EnumeratorHelper<FxOffer, IOffer>( _offersCollection.GetEnumerator( ) );
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator( )
        {
            return _offersCollection.GetEnumerator( );
        }

        

        #endregion

        /// <summary>
        /// Adds a new offer
        /// </summary>
        /// <param name="offerid">OfferId</param>
        /// <param name="instrument">The name of the instrument</param>
        /// <param name="lastChange">The date/time of the last change of the offer</param>
        /// <param name="bid">The current bid price</param>
        /// <param name="ask">The current ask price</param>
        /// <param name="minuteVolume">The current accumulated minute tick volume</param>
        /// <param name="digits">The precision</param>
        public void Add( string offerid,
                            string instrument,
                            DateTime lastChange,
                            double bid,
                            double ask,
                            int minuteVolume,
                            int digits

                       )
        {
            FxOffer fxOffer;

            _offersCollection.Add( fxOffer = new FxOffer( instrument, lastChange, bid, ask, minuteVolume, digits ) );

            _idToOffer.Add( offerid, fxOffer );
            _instrumentToOffer.Add( instrument, fxOffer );
        }

        public void Add(
                            string offerid,
                            FxOffer offerObj
                       )
        {
            _offersCollection.Add( offerObj );

            _idToOffer.Add( offerid, offerObj );
            _instrumentToOffer.Add( offerObj.Instrument, offerObj );
        }

        /// <summary>
        /// Removes all offers from the collection
        /// </summary>
        public void Clear( )
        {
            _offersCollection.Clear( );
            _idToOffer.Clear( );
            _instrumentToOffer.Clear( );
        }

        
    }
}
