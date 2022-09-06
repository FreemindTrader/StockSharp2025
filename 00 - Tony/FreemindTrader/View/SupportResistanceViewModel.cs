using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Ecng.ComponentModel;
using Ecng.Xaml;
using fx.Algorithm;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FreemindTrader
{
    public class SupportResistanceViewModel : DevExpress.Mvvm.ViewModelBase
    {
        private Security _security;

        SRlevel _srLevelClosest = null;

        double _lastDiff = 0;

        private decimal _instrumentPointSize;

        double _average;

        private SRlevelsTsoCollection _srlevelsList;
        private ObservableCollectionEx<SRlevel> _srLevelsCollection;

        int _lastRowIndex = -1;

        bool _isUpdating = false;

        public SupportResistanceViewModel( )
        {
            Messenger.Default.Register< SelectSecurityMessage>( this, x => OnSelectSecurityMessage( x ) );
            Messenger.Default.Register< QuoteMessage         >( this, x => OnQuoteUpdate( x ) );
            Messenger.Default.Register< PivotPointMessage    >( this, x => OnPivotPointMessage( x ) );
        }

        private void OnPivotPointMessage( PivotPointMessage msg )
        {
            _isUpdating = true;

            SRlevelsTsoCollection srList = null;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                srList = aa.SupportResistanceBindingList;
            }
            else
            {
                return;
            }            

            srList.AddSRlines( msg.SrLevels, _security.Decimals.Value );

            _isUpdating = false;
        }

        private void OnSelectSecurityMessage( SelectSecurityMessage x )
        {
            _security = x.Symbol;

            _instrumentPointSize = x.Symbol.PriceStep.Value;            

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

            if ( aa != null )
            {
                _srlevelsList = aa.SupportResistanceBindingList;
            }
            else
            {
                return;
            }             

            RaisePropertyChanged( nameof( SrLevelsItemSource ) );

        }

        protected IFocusCellService FocusCellService
        {
            get
            {
                return GetService<IFocusCellService>( );
            }
        }

        
        public int LastRowIndex
        {
            get { return _lastRowIndex; }
            set
            {
                if ( _lastRowIndex == value )
                    return;
                _lastRowIndex = value;
                RaisePropertyChanged( nameof( LastRowIndex ) );
            }
        }
        

        private int _srRowHandle;

        public int SRRowHandle
        {
            get { return _srRowHandle; }
            set
            {
                SetValue( ref _srRowHandle, value );
            }
        }

        private int _theTopRow = 0;
        public int TheTopRow
        {
            get { return _theTopRow; }
            set
            {
                SetValue( ref _theTopRow, value );
            }
        }

        string _closestPP = "PP";

        public string ClosestPP
        {
            get { return _closestPP; }
            set
            {
                SetValue( ref _closestPP, value );
                RaisePropertyChanged( nameof( ClosestPP ) );
            }
        }

        public ObservableCollectionEx<SRlevel> SrLevelsItemSource
        {
            get
            {
                if ( _security != null )
                {
                    var aa =( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                    if ( aa != null )
                    {
                        _srLevelsCollection = aa.SRLevelsItemSource;
                    }
                    else
                    {
                        return null;
                    }                     

                    return _srLevelsCollection;
                }

                return null;
            }
        }

        private void OnQuoteUpdate( QuoteMessage quote )
        {
            if ( _srLevelsCollection == null || _srLevelsCollection.Count == 0 )
                return;

            if( quote.Security.Code != _security.Code )
                return;

            if( _isUpdating )
                return;

            _average = ( quote.Ask + quote.Bid ) / 2;

            int index = _srlevelsList.GetClosestSrLine( _average );

            SRlevel srLevel = null;
            SRlevel srLevelNext = null;

            //bool firstTime = false;

            int selectedIndex = -1;


            if ( index > -1 )
            {
                selectedIndex = index;
                srLevel = _srLevelsCollection[ index ];

                if ( _srLevelClosest == null )
                {
                    _srLevelClosest = srLevel;
                    //firstTime = true;
                }

                //var rowHandle = _SRLevelGridView.FocusedRowHandle;

                //if ( rowHandle != _lastRowIndex )
                //{
                //    _SRLevelGridView.SelectRow( _lastRowIndex );
                //    _SRLevelGridView.FocusedRowHandle = _lastRowIndex;
                //    _SRLevelGridView.MakeRowVisible( _lastRowIndex );
                //}

                if ( index != _lastRowIndex )
                {
                    if ( index + 1 < _srLevelsCollection.Count )
                    {
                        srLevelNext = _srLevelsCollection[ index + 1 ];
                    }

                    _lastRowIndex = index;

                    if ( SRRowHandle != _lastRowIndex )
                    {
                        SRRowHandle = _lastRowIndex;

                        if ( _lastRowIndex - 7 > 0 )
                        {
                            TheTopRow = _lastRowIndex - 7;
                        }

                        GuiDispatcher.GlobalDispatcher.AddAction( ( ) => FocusCell( ) );
                    }

                    

                    if ( srLevelNext != null )
                    {
                        var diff2 = Math.Abs( _average - srLevel.SRvalue );
                        var diff3 = Math.Abs( _average - srLevelNext.SRvalue );

                        if ( diff2 < diff3 )
                        {
                            _srLevelClosest = srLevel;
                        }
                        else if ( diff2 > diff3 )
                        {
                            _srLevelClosest = srLevelNext;
                            selectedIndex = index + 1;
                        }
                    }

                    ClosestPP = _srLevelClosest.SR3rdType.ToDescription( );

                    //_SRLevelGridView.RefreshRow( selectedIndex );
                }

                var diff = ( _average - _srLevelClosest.SRvalue ) / (double)_instrumentPointSize;

                if ( diff != _lastDiff )
                {
                    _lastDiff = diff;

                    var whole = Math.Round( diff, 1 );

                    PipsFromPivotPoint = whole;


                }

            }
        }

        public void FocusCell( )
        {
            FocusCellService.FocusCell( );
        }

        private double _pipsFromPivotPoint;
        public double PipsFromPivotPoint
        {
            get { return _pipsFromPivotPoint; }
            set
            {
                SetValue( ref _pipsFromPivotPoint, value );
                RaisePropertyChanged( nameof( PipsFromPivotPoint ) );
            }
        }

    }
}

