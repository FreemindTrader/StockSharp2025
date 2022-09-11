using StockSharp.BusinessEntities;
using System;

namespace fx.Algorithm
{
    /// <summary>
    /// Indicator class child, that joins the platform requirements for an indicator, with the indicator class. Contains
    /// orderInfo for the UI of the indicator (PlatformIndicatorChartSeries).
    /// </summary>
    [Serializable]
    public abstract class PlatformIndicator : Indicator
    {
        private float? _rangeMinimum = null;

        /// <summary>
        /// If the indicator is ranged (has a fixed minimum and maximum value), this must be assigned to minimum.
        /// </summary>
        public float? RangeMinimum
        {
            get
            {
                return _rangeMinimum;
            }
            set
            {
                _rangeMinimum = value;
            }
        }

        private float? _rangeMaximum = null;

        /// <summary>
        /// If the indicator is ranged (has a fixed minimum and maximum value), this must be assigned to minimum.
        /// </summary>
        public float? RangeMaximum
        {
            get
            {
                return _rangeMaximum;
            }
            set
            {
                _rangeMaximum = value;
            }
        }

        TimeSpan _period; 
        public TimeSpan TimeSpan
        {
            get
            {
                return _period;
            }

            set
            {
                _period = value;
            }
        }

        string _symbol;

        public string Symbol
        {
            get
            {
                return _symbol;
            }

            set
            {
                _symbol = value;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlatformIndicator( string name, bool isIndicatorVisible, bool? isTradeable, bool? isShowInMasterPane, string[ ] resultSetNames ) : base( name, isIndicatorVisible, isTradeable, isShowInMasterPane, resultSetNames )
        {
        }

        /// <summary>
        /// Deserialization callback.
        /// </summary>
        public override void OnDeserialization( object sender )
        {
            base.OnDeserialization( sender );

            // Needs immediate initialization since it will be added to chart.
            //_chartSeries.Initialize( this );
        }

        public override void UnInitialize( )
        {
            //_chartSeries.UnInitialize( );

            base.UnInitialize( );
        }

        public PlatformIndicator SimpleClone( )
        {
            PlatformIndicator indicator = OnSimpleClone( );
            indicator.RangeMaximum = RangeMaximum;
            indicator.RangeMinimum = RangeMinimum;

            return indicator;
        }

        /// <summary>
        /// Create a shallow copy of the instance.
        /// </summary>
        public abstract PlatformIndicator OnSimpleClone( );
    }
}