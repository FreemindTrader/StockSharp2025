using fx.Bars;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    /// <summary>
    /// Manages the operation and storage of trading indicators.
    /// </summary>
    [Serializable]
    public class IndicatorManager : IIndicatorManager
    {
        private PooledList< Indicator > _indicators = new PooledList< Indicator >( );

        /// <summary>
        /// All indicators currently assigned to this session.
        /// </summary>
        public ReadOnlyCollection< Indicator > IndicatorsUnsafe
        {
            get
            {
                lock( this )
                {
                    return _indicators.AsReadOnly( );
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Indicator[ ] IndicatorsArray
        {
            get
            {
                lock( this )
                {
                    return _indicators.ToArray( );
                }
            }
        }

        private fxHistoricBarsRepo _selectedPeriodXProvider;

        public fxHistoricBarsRepo SelectedPeriodXProvider
        {
            get { return _selectedPeriodXProvider; }
            set { _selectedPeriodXProvider = value; }
        }

        
[field: NonSerialized]
        public event IndicatorUpdateDelegate IndicatorAddedEvent;

[field: NonSerialized]
        public event IndicatorUpdateDelegate IndicatorRemovedEvent;

/// <summary>
/// Uninitialization of indicators occurs both - on removing it and on uninit of the session. Note that remove of
/// indicator does not occur on uninit of the session, due to serialization needs. It is prefferably to catch this
/// instead of the remove.
/// </summary>
[field: NonSerialized]
        public event IndicatorUpdateDelegate IndicatorUnInitializedEvent;

[field: NonSerialized]
        public event IndicatorUpdateDelegate IndicatorManagerFullCalculationDoneEvent;

        /// <summary>
        /// Constructor.
        /// </summary>
        public IndicatorManager( fxHistoricBarsRepo dataProvider )
        {
            _selectedPeriodXProvider = dataProvider;
        }

        public IndicatorManager(  )
        {
            
        }

        public bool Initialize( )
        {
            //foreach (Evaluator evaluator in _evaluators)
            //{
            //    evaluator.Initialize(this);
            //}

            foreach( Indicator indicator in _indicators )
            {
                indicator.AttachDatasource( _selectedPeriodXProvider );
            }

            return true;
        }

        public TimeSpan Period
        {
            get
            {
                return _selectedPeriodXProvider.Period.Value;
            }
        }

        public void UnInitialize( )
        {
            foreach( Indicator indicator in _indicators )
            {
                indicator.UnInitialize( );
            }
        }

        protected void UnInitializeIndicator( Indicator indicator )
        {
            indicator.UnInitialize( );

            if( IndicatorUnInitializedEvent != null )
            {
                IndicatorUnInitializedEvent( this, indicator );
            }
        }

        public void IndicatorFullRecalculation( )
        {
            foreach( Indicator _indicator in _indicators )
            {
                _indicator.StartPerformingLongCalculationTask( "FullCalculation", null );
            }
        }

        //public void FullRecalculationAfterLoading( DoneReloadDatabars update )
        //{
        //    foreach ( Indicator _indicator in _indicators )
        //    {
        //        _indicator.FullCalculationDoneEvent += _indicator_FullCalculationDoneEvent;
        //        _indicator.PerformFullCalculationTask( update );
        //    }
        //}

        //private void _indicator_FullCalculationDoneEvent( IMyIndicator indicator, HistoricBarsUpdateEventArg e )
        //{
        //    if( IndicatorManagerFullCalculationDoneEvent != null )
        //    {
        //        IndicatorManagerFullCalculationDoneEvent( this, indicator );
        //    }
        //}

        /// <summary>
        /// Add a new indicator.
        /// </summary>
        /// <param name="indicator"></param>
        /// <returns></returns>
        public bool AddIndicator( IMyIndicator indicator )
        {
            if( indicator.AttachDatasource( _selectedPeriodXProvider ) == false )
            {
                indicator.Dispose( );
                return false;
            }

            //InternalAddIndicator( indicator );

            Task.Factory.StartNew( ( ) => InternalAddIndicator( indicator ), GeneralHelper.GlobalExitToken( ) );

            return true;
        }

        private void InternalAddIndicator( IMyIndicator indicator )
        {
            indicator.FreemindCalculateIndicators( true, new HistoricBarsUpdateEventArg( DataBarUpdateType.Initial, 0, _selectedPeriodXProvider.TotalBarCount ) );

            lock( this )
            {
                _indicators.Add( ( Indicator ) indicator );

                indicator.FullCalculationDoneEvent += Indicator_FullCalculationDoneEvent; ;
            }

            if( IndicatorAddedEvent != null )
            {
                IndicatorAddedEvent( this, indicator );
            }
        }

        private void Indicator_FullCalculationDoneEvent( object sender, HistoricBarsUpdateEventArg e )
        {
            if ( IndicatorManagerFullCalculationDoneEvent != null )
            {
                IndicatorManagerFullCalculationDoneEvent( this, ( IMyIndicator ) sender );
            }
        }

        public void RemoveIndicator( Indicator indicator )
        {
            indicator.UnInitialize( );

            bool removeResult;
            lock( this )
            {
                removeResult = _indicators.Remove( indicator );
            }

            if( IndicatorUnInitializedEvent != null )
            {
                IndicatorUnInitializedEvent( this, indicator );
            }

            if( removeResult && IndicatorRemovedEvent != null )
            {
                IndicatorRemovedEvent( this, indicator );
            }
        }

        /// <summary>
        /// Keep in mind many indicators with the same name can be present at the same time, so this will return the
        /// first found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Indicator GetFirstIndicatorByName( string name )
        {
            lock( this )
            {
                foreach( Indicator indicator in _indicators )
                {
                    if( indicator.Name.ToLower( ) == name.ToLower( ) )
                    {
                        return indicator;
                    }
                }
            }

            return null;
        }
    }
}