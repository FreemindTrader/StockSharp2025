using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Docking.Base;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using fx.Definitions;
using fx.Common;
using fx.Definitions.UndoRedo;
using fx.Algorithm;
using Ecng.Collections;
using StockSharp.Logging;
using fx.Indicators;

using DevExpress.Xpf.Bars;
using fx.TimePeriod;
using fx.Collections;

namespace FreemindAITrade.ViewModels
{    
    public partial class TradeStationViewModel : BaseLogReceiver, IMutltiTimeFrameSessionDataRepo
    {       
        public void LocateAtLowerTF()
        {
            using ( _selectedUndoRedoArea.Start( "Find and Load Wave Lower TF" ) )
            {
                _selectedViewModel.FindAndLoadDatabarsLowerTimeFrame( _selectedViewModel.WaveScenarioNumber );

                _selectedUndoRedoArea.Commit( );
            }
        }

        public void LocateAtHigherTF( )
        {
            using ( _selectedUndoRedoArea.Start( "Find and Load Wave Higher TF" ) )
            {
                _selectedViewModel.FindAndLoadDatabarsHigherTimeFrame( _selectedViewModel.WaveScenarioNumber );

                _selectedUndoRedoArea.Commit( );
            }
        }

        [ Command( false )]
        public void OnCheckChanged( ItemClickEventArgs args )
        {
            ItemClickEventArgs myevent = args;

            if ( args.Source is BarCheckItem )
            {
                var checkItem = ( BarCheckItem )args.Source;

                if( checkItem.IsChecked.Value == false )
                {
                    return;
                }                    
            }

            var pivotPointsCount = ( ShowPivotPoints )Enum.Parse( typeof( ShowPivotPoints ), ( string )myevent.Item.Tag );

            switch ( pivotPointsCount )
            {
                case ShowPivotPoints.SHOWNONE:
                {
                    RemoveAllPivotPoints( );
                }                
                break;


                case ShowPivotPoints.SHOWALL:
                {
                    AddMonthlyPivotLevels( );
                    AddWeeklyPivotLevels( );
                    SetDailyPivotLevels( 5 );
                }                
                break;

                case ShowPivotPoints.SHOWTODAY:
                {
                    AddMonthlyPivotLevels( );
                    AddWeeklyPivotLevels( );
                    SetDailyPivotLevels( 1 );
                }
                break;

                case ShowPivotPoints.SHOWLAST2DAYS:
                {
                    AddMonthlyPivotLevels( );
                    AddWeeklyPivotLevels( );
                    SetDailyPivotLevels( 2 );
                }                
                break;

                case ShowPivotPoints.SHOWLAST3DAYS:
                {
                    AddMonthlyPivotLevels( );
                    AddWeeklyPivotLevels( );
                    SetDailyPivotLevels( 3 );
                }                
                break;

                case ShowPivotPoints.SHOWLAST4DAYS:
                {
                    AddMonthlyPivotLevels( );
                    AddWeeklyPivotLevels( );
                    SetDailyPivotLevels( 4 );
                }
                
                break;

                //case ShowPivotPoints.SHOW_SELECTEBAR:
                //if ( message.SelectedBarTime > -1 )
                //{
                //    RemoveAllPivotPoints( );
                //    AddDailyPivotLevesl( message.SelectedBarTime );
                //    AddMonthlyPivotLevels( message.SelectedBarTime );
                //    AddWeeklyPivotLevels( message.SelectedBarTime );
                //}

                //break;
            }
        }

        private void OnPivotPointsChange( PivotsPointUpdateMessage message )
        {
            Action toBeDone = delegate ( )
            {
                switch ( message.PivotPointsShowType )
                {
                    case ShowPivotPoints.SHOWNONE:
                        RemoveAllPivotPoints( );
                        break;


                    case ShowPivotPoints.SHOWALL:
                        AddMonthlyPivotLevels( );
                        AddWeeklyPivotLevels( );
                        SetDailyPivotLevels( 5 );

                        break;
                    case ShowPivotPoints.SHOWTODAY:
                        AddMonthlyPivotLevels( );
                        AddWeeklyPivotLevels( );
                        SetDailyPivotLevels( 1 );

                        break;
                    case ShowPivotPoints.SHOWLAST2DAYS:
                        AddMonthlyPivotLevels( );
                        AddWeeklyPivotLevels( );
                        SetDailyPivotLevels( 2 );
                        break;

                    case ShowPivotPoints.SHOWLAST3DAYS:
                        AddMonthlyPivotLevels( );
                        AddWeeklyPivotLevels( );
                        SetDailyPivotLevels( 3 );
                        break;

                    case ShowPivotPoints.SHOWLAST4DAYS:
                        AddMonthlyPivotLevels( );
                        AddWeeklyPivotLevels( );
                        SetDailyPivotLevels( 4 );
                        break;

                    case ShowPivotPoints.SHOW_SELECTEBAR:
                        if ( message.SelectedBarTime > -1 )
                        {
                            RemoveAllPivotPoints( );
                            AddDailyPivotLevesl( message.SelectedBarTime );
                            AddMonthlyPivotLevels( message.SelectedBarTime );
                            AddWeeklyPivotLevels( message.SelectedBarTime );
                        }

                        break;
                }
            };

            Ecng.Xaml.GuiDispatcher.GlobalDispatcher.AddAction( toBeDone );

            

        }

        private void AddMonthlyPivotLevels( long barDate )
        {
            if ( _monthlyVm != null && _monthlyVm.PivotPointIndicator != null  )
            {
                var monthlyPivotLevels = new PooledList<SRlevel>( 15 );
                DateTime barDateDt = barDate.FromLinuxTime( );                

                TimeBlockEx timePeriod;

                var pivotPoints = _monthlyVm.PivotPointIndicator.GetPivotPointsAt( barDateDt, out timePeriod );

                if ( timePeriod == TimeBlockEx.EmptyBlock )
                {

                }

                if ( pivotPoints != null )
                {
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.R3, ( int )( ( pivotPoints.R3 - pivotPoints.Pivot ) * 10000 ), SR3rdType.R3 ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.M5, ( int )( ( pivotPoints.M5 - pivotPoints.Pivot ) * 10000 ), SR3rdType.M5 ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.R2, ( int )( ( pivotPoints.R2 - pivotPoints.Pivot ) * 10000 ), SR3rdType.R2 ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.M4, ( int )( ( pivotPoints.M4 - pivotPoints.Pivot ) * 10000 ), SR3rdType.M4 ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.R1, ( int )( ( pivotPoints.R1 - pivotPoints.Pivot ) * 10000 ), SR3rdType.R1 ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.M3, ( int )( ( pivotPoints.M3 - pivotPoints.Pivot ) * 10000 ), SR3rdType.M3 ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.Pivot, ( int )( ( pivotPoints.M3 - pivotPoints.Pivot ) / 2 * 10000 ), SR3rdType.PIVOT ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.M2, ( int )( ( pivotPoints.Pivot - pivotPoints.M2 ) * 10000 ), SR3rdType.M2 ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.S1, ( int )( ( pivotPoints.Pivot - pivotPoints.S1 ) * 10000 ), SR3rdType.S1 ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.M1, ( int )( ( pivotPoints.Pivot - pivotPoints.M1 ) * 10000 ), SR3rdType.M1 ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.S2, ( int )( ( pivotPoints.Pivot - pivotPoints.S2 ) * 10000 ), SR3rdType.S2 ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.M0, ( int )( ( pivotPoints.Pivot - pivotPoints.M0 ) * 10000 ), SR3rdType.M0 ) );
                    monthlyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 30 ), pivotPoints.S3, ( int )( ( pivotPoints.Pivot - pivotPoints.S3 ) * 10000 ), SR3rdType.S3 ) );
                    
                }

                if ( monthlyPivotLevels.Count > 0 )
                {
                    _01secVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;
                    _01MinVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;
                    _04MinVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;
                    _05MinVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;
                    _15MinVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;
                    _30MinVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;
                    _01hrsVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;

                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                    SRlevelsTsoCollection srList = null;

                    if ( aa != null )
                    {
                        srList = aa.SupportResistanceBindingList;
                    }
                    else
                    {
                        return;
                    }                     

                    srList.AddSRlines( monthlyPivotLevels, _security.Decimals.Value );                    
                }
            }
        }

        private void AddDailyPivotLevesl( long barDate )
        {
            if ( _dailyVm != null && _dailyVm.PivotPointIndicator != null && _dailyVm.PivotPointIndicator.DoneInitialDataLoad )
            {
                var dailyPivotLevels = new PooledList<SRlevel>( 75 );

                DateTime barDateDt = barDate.FromLinuxTime( );

                TimeBlockEx timePeriod;

                var pivotPoints = _dailyVm.PivotPointIndicator.GetPivotPointsAt( barDateDt, out timePeriod );

                if ( timePeriod == TimeBlockEx.EmptyBlock )
                {

                }

                if ( pivotPoints != null )
                {
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.R3,    ( int )( ( pivotPoints.R3 - pivotPoints.Pivot ) * 10000 ), SR3rdType.R3 ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.M5,    ( int )( ( pivotPoints.M5 - pivotPoints.Pivot ) * 10000 ), SR3rdType.M5 ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.R2,    ( int )( ( pivotPoints.R2 - pivotPoints.Pivot ) * 10000 ), SR3rdType.R2 ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.M4,    ( int )( ( pivotPoints.M4 - pivotPoints.Pivot ) * 10000 ), SR3rdType.M4 ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.R1,    ( int )( ( pivotPoints.R1 - pivotPoints.Pivot ) * 10000 ), SR3rdType.R1 ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.M3,    ( int )( ( pivotPoints.M3 - pivotPoints.Pivot ) * 10000 ), SR3rdType.M3 ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.Pivot, ( int )( ( pivotPoints.M3 - pivotPoints.Pivot ) / 2 * 10000 ), SR3rdType.PIVOT ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.M2,    ( int )( ( pivotPoints.Pivot - pivotPoints.M2 ) * 10000 ), SR3rdType.M2 ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.S1,    ( int )( ( pivotPoints.Pivot - pivotPoints.S1 ) * 10000 ), SR3rdType.S1 ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.M1,    ( int )( ( pivotPoints.Pivot - pivotPoints.M1 ) * 10000 ), SR3rdType.M1 ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.S2,    ( int )( ( pivotPoints.Pivot - pivotPoints.S2 ) * 10000 ), SR3rdType.S2 ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.M0,    ( int )( ( pivotPoints.Pivot - pivotPoints.M0 ) * 10000 ), SR3rdType.M0 ) );
                    dailyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 1 ), pivotPoints.S3,    ( int )( ( pivotPoints.Pivot - pivotPoints.S3 ) * 10000 ), SR3rdType.S3 ) );                    

                    if ( dailyPivotLevels.Count > 0 )
                    {
                        if ( _01secVm != null ) _01secVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                        if ( _01MinVm != null ) _01MinVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                        if ( _04MinVm != null ) _04MinVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                        if ( _05MinVm != null ) _05MinVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                        if ( _15MinVm != null ) _15MinVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                        if ( _30MinVm != null ) _30MinVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                        if ( _01hrsVm != null ) _01hrsVm.DatabarRepo.DailyPivots = dailyPivotLevels;                        
                    }
                }
            }
        }

        private void SetDailyPivotLevels( int howManyday )
        {
            if ( _dailyVm != null && _dailyVm.PivotPointIndicator != null && _dailyVm.PivotPointIndicator.DoneInitialDataLoad )
            {
                var dailyPivotLevels = new PooledList<SRlevel>( 75 );

                var todayPivotLevels = new PooledList<SRlevel>( 75 );

                var calcDate         = DateTime.UtcNow;                                

                while ( howManyday > 0 )
                {                    
                    var currentTimeBlock = _dailyVm.PivotPointIndicator.GetTimeBlock( calcDate );

                    if ( currentTimeBlock != TimeBlockEx.EmptyBlock )
                    {
                        var pp = _dailyVm.PivotPointIndicator.GetPivotPointsAt( calcDate, out TimeBlockEx temp );

                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.R3, ( int ) ( ( pp.R3 - pp.Pivot ) * 10000 ), SR3rdType.R3 ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.M5, ( int ) ( ( pp.M5 - pp.Pivot ) * 10000 ), SR3rdType.M5 ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.R2, ( int ) ( ( pp.R2 - pp.Pivot ) * 10000 ), SR3rdType.R2 ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.M4, ( int ) ( ( pp.M4 - pp.Pivot ) * 10000 ), SR3rdType.M4 ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.R1, ( int ) ( ( pp.R1 - pp.Pivot ) * 10000 ), SR3rdType.R1 ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.M3, ( int ) ( ( pp.M3 - pp.Pivot ) * 10000 ), SR3rdType.M3 ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.Pivot, ( int ) ( ( pp.M3 - pp.Pivot ) / 2 * 10000 ), SR3rdType.PIVOT ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.M2, ( int ) ( ( pp.Pivot - pp.M2 ) * 10000 ), SR3rdType.M2 ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.S1, ( int ) ( ( pp.Pivot - pp.S1 ) * 10000 ), SR3rdType.S1 ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.M1, ( int ) ( ( pp.Pivot - pp.M1 ) * 10000 ), SR3rdType.M1 ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.S2, ( int ) ( ( pp.Pivot - pp.S2 ) * 10000 ), SR3rdType.S2 ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.M0, ( int ) ( ( pp.Pivot - pp.M0 ) * 10000 ), SR3rdType.M0 ) );
                        dailyPivotLevels.Add( new SRlevel( ref currentTimeBlock, TimeSpan.FromDays( 1 ), pp.S3, ( int ) ( ( pp.Pivot - pp.S3 ) * 10000 ), SR3rdType.S3 ) );

                        howManyday -= 1;
                    }

                    calcDate = calcDate.AddDays( -1 );
                }
                               

                if ( dailyPivotLevels.Count > 0 )
                {
                    if ( _01secVm != null ) _01secVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                    if ( _01MinVm != null ) _01MinVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                    if ( _04MinVm != null ) _04MinVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                    if ( _05MinVm != null ) _05MinVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                    if ( _15MinVm != null ) _15MinVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                    if ( _30MinVm != null ) _30MinVm.DatabarRepo.DailyPivots = dailyPivotLevels;
                    if ( _01hrsVm != null ) _01hrsVm.DatabarRepo.DailyPivots = dailyPivotLevels;                                                
                }
            }
        }

        private void AddWeeklyPivotLevels( )
        {
            if ( _weeklyVm != null && _weeklyVm.PivotPointIndicator != null  )
            {
                var weeklyPivotLevels = new PooledList<SRlevel>( 15 );

                var weeklyTimeBlock = _weeklyVm.PivotPointIndicator.GetTimeBlock( DateTime.UtcNow );

                if ( weeklyTimeBlock == TimeBlockEx.EmptyBlock )
                {
                    return;
                }

                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.R3, ( int )( ( _weeklyVm.PivotPointIndicator.R3 - _weeklyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.R3 ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.M5, ( int )( ( _weeklyVm.PivotPointIndicator.M5 - _weeklyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.M5 ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.R2, ( int )( ( _weeklyVm.PivotPointIndicator.R2 - _weeklyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.R2 ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.M4, ( int )( ( _weeklyVm.PivotPointIndicator.M4 - _weeklyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.M4 ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.R1, ( int )( ( _weeklyVm.PivotPointIndicator.R1 - _weeklyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.R1 ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.M3, ( int )( ( _weeklyVm.PivotPointIndicator.M3 - _weeklyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.M3 ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.Pivot, ( int )( ( _weeklyVm.PivotPointIndicator.M3 - _weeklyVm.PivotPointIndicator.Pivot ) / 2 * 10000 ), SR3rdType.PIVOT ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.M2, ( int )( ( _weeklyVm.PivotPointIndicator.Pivot - _weeklyVm.PivotPointIndicator.M2 ) * 10000 ), SR3rdType.M2 ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.S1, ( int )( ( _weeklyVm.PivotPointIndicator.Pivot - _weeklyVm.PivotPointIndicator.S1 ) * 10000 ), SR3rdType.S1 ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.M1, ( int )( ( _weeklyVm.PivotPointIndicator.Pivot - _weeklyVm.PivotPointIndicator.M1 ) * 10000 ), SR3rdType.M1 ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.S2, ( int )( ( _weeklyVm.PivotPointIndicator.Pivot - _weeklyVm.PivotPointIndicator.S2 ) * 10000 ), SR3rdType.S2 ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.M0, ( int )( ( _weeklyVm.PivotPointIndicator.Pivot - _weeklyVm.PivotPointIndicator.M0 ) * 10000 ), SR3rdType.M0 ) );
                weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.S3, ( int )( ( _weeklyVm.PivotPointIndicator.Pivot - _weeklyVm.PivotPointIndicator.S3 ) * 10000 ), SR3rdType.S3 ) );
                //weeklyPivotLevels.Add( new SRlevel( ref weeklyTimeBlock, TimeSpan.FromDays( 7 ), _weeklyVm.PivotPointIndicator.Mdn, ( int )( Math.Abs( _weeklyVm.PivotPointIndicator.Mdn - _weeklyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.MDN ) );


                if ( weeklyPivotLevels.Count > 0 )
                {
                    if ( _01secVm != null ) _01secVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _01MinVm != null ) _01MinVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _04MinVm != null ) _04MinVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _05MinVm != null ) _05MinVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _15MinVm != null ) _15MinVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _30MinVm != null ) _30MinVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _01hrsVm != null ) _01hrsVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;

                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                    SRlevelsTsoCollection srList = null;

                    if ( aa != null )
                    {
                        srList = aa.SupportResistanceBindingList;
                    }
                    else
                    {
                        return;
                    }

                    srList.AddSRlines( weeklyPivotLevels, _security.Decimals.Value );                    
                }
            }
        }

        private void AddWeeklyPivotLevels( long barDate )
        {
            if ( _weeklyVm != null && _weeklyVm.PivotPointIndicator != null )
            {
                var weeklyPivotLevels = new PooledList<SRlevel>( 15 );

                DateTime barDateDt = barDate.FromLinuxTime( );                 

                var pivotPoints = _weeklyVm.PivotPointIndicator.GetPivotPointsAt( barDateDt, out TimeBlockEx timePeriod );

                if ( pivotPoints != null )
                {
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.R3, ( int )( ( pivotPoints.R3 - pivotPoints.Pivot ) * 10000 ), SR3rdType.R3 ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.M5, ( int )( ( pivotPoints.M5 - pivotPoints.Pivot ) * 10000 ), SR3rdType.M5 ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.R2, ( int )( ( pivotPoints.R2 - pivotPoints.Pivot ) * 10000 ), SR3rdType.R2 ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.M4, ( int )( ( pivotPoints.M4 - pivotPoints.Pivot ) * 10000 ), SR3rdType.M4 ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.R1, ( int )( ( pivotPoints.R1 - pivotPoints.Pivot ) * 10000 ), SR3rdType.R1 ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.M3, ( int )( ( pivotPoints.M3 - pivotPoints.Pivot ) * 10000 ), SR3rdType.M3 ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.Pivot, ( int )( ( pivotPoints.M3 - pivotPoints.Pivot ) / 2 * 10000 ), SR3rdType.PIVOT ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.M2, ( int )( ( pivotPoints.Pivot - pivotPoints.M2 ) * 10000 ), SR3rdType.M2 ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.S1, ( int )( ( pivotPoints.Pivot - pivotPoints.S1 ) * 10000 ), SR3rdType.S1 ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.M1, ( int )( ( pivotPoints.Pivot - pivotPoints.M1 ) * 10000 ), SR3rdType.M1 ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.S2, ( int )( ( pivotPoints.Pivot - pivotPoints.S2 ) * 10000 ), SR3rdType.S2 ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.M0, ( int )( ( pivotPoints.Pivot - pivotPoints.M0 ) * 10000 ), SR3rdType.M0 ) );
                    weeklyPivotLevels.Add( new SRlevel( ref timePeriod, TimeSpan.FromDays( 7 ), pivotPoints.S3, ( int )( ( pivotPoints.Pivot - pivotPoints.S3 ) * 10000 ), SR3rdType.S3 ) );
                    
                }


                if ( weeklyPivotLevels.Count > 0 )
                {
                    if ( _01secVm != null ) _01secVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _01MinVm != null ) _01MinVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _04MinVm != null ) _04MinVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _05MinVm != null ) _05MinVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _15MinVm != null ) _15MinVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _30MinVm != null ) _30MinVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;
                    if ( _01hrsVm != null ) _01hrsVm.DatabarRepo.WeeklyPivots = weeklyPivotLevels;

                    var aa =( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                    SRlevelsTsoCollection srList = null;

                    if ( aa != null )
                    {
                        srList = aa.SupportResistanceBindingList;
                    }
                    else
                    {
                        return;
                    }

                    srList.AddSRlines( weeklyPivotLevels, _security.Decimals.Value );                    
                }
            }
        }

        private void RemoveAllPivotPoints( )
        {
            if ( _01secVm != null ) _01secVm.DatabarRepo.ClearMonthlyPivots( );
            if ( _01secVm != null ) _01secVm.DatabarRepo.ClearWeeklyPivots( );
            if ( _01secVm != null ) _01secVm.DatabarRepo.ClearDailyPivots( );

            if ( _01MinVm != null ) _01MinVm.DatabarRepo.ClearMonthlyPivots( );
            if ( _01MinVm != null ) _01MinVm.DatabarRepo.ClearWeeklyPivots( );
            if ( _01MinVm != null ) _01MinVm.DatabarRepo.ClearDailyPivots( );

            if ( _04MinVm != null ) _04MinVm.DatabarRepo.ClearMonthlyPivots( );
            if ( _04MinVm != null ) _04MinVm.DatabarRepo.ClearWeeklyPivots( );
            if ( _04MinVm != null ) _04MinVm.DatabarRepo.ClearDailyPivots( );

            if ( _05MinVm != null ) _05MinVm.DatabarRepo.ClearMonthlyPivots( );
            if ( _05MinVm != null ) _05MinVm.DatabarRepo.ClearWeeklyPivots( );
            if ( _05MinVm != null ) _05MinVm.DatabarRepo.ClearDailyPivots( );

            if ( _15MinVm != null ) _15MinVm.DatabarRepo.ClearMonthlyPivots( );
            if ( _15MinVm != null ) _15MinVm.DatabarRepo.ClearWeeklyPivots( );
            if ( _15MinVm != null ) _15MinVm.DatabarRepo.ClearDailyPivots( );

            if ( _30MinVm != null ) _30MinVm.DatabarRepo.ClearMonthlyPivots( );
            if ( _30MinVm != null ) _30MinVm.DatabarRepo.ClearWeeklyPivots( );
            if ( _30MinVm != null ) _30MinVm.DatabarRepo.ClearDailyPivots( );

            if ( _01hrsVm != null ) _01hrsVm.DatabarRepo.ClearMonthlyPivots( );
            if ( _01hrsVm != null ) _01hrsVm.DatabarRepo.ClearWeeklyPivots( );
            if ( _01hrsVm != null ) _01hrsVm.DatabarRepo.ClearDailyPivots( );
        }

        private void AddMonthlyPivotLevels( )
        {
            if ( _monthlyVm != null && _monthlyVm.PivotPointIndicator != null  )
            {
                var monthlyPivotLevels = new PooledList<SRlevel>( 15 );

                var timeBlock = _monthlyVm.PivotPointIndicator.GetTimeBlock( DateTime.UtcNow );

                if ( timeBlock == TimeBlockEx.EmptyBlock )
                {
                    return;
                }

                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.R3, ( int )( ( _monthlyVm.PivotPointIndicator.R3 - _monthlyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.R3 ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.M5, ( int )( ( _monthlyVm.PivotPointIndicator.M5 - _monthlyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.M5 ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.R2, ( int )( ( _monthlyVm.PivotPointIndicator.R2 - _monthlyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.R2 ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.M4, ( int )( ( _monthlyVm.PivotPointIndicator.M4 - _monthlyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.M4 ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.R1, ( int )( ( _monthlyVm.PivotPointIndicator.R1 - _monthlyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.R1 ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.M3, ( int )( ( _monthlyVm.PivotPointIndicator.M3 - _monthlyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.M3 ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.Pivot, ( int )( ( _monthlyVm.PivotPointIndicator.M3 - _monthlyVm.PivotPointIndicator.Pivot ) / 2 * 10000 ), SR3rdType.PIVOT ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.M2, ( int )( ( _monthlyVm.PivotPointIndicator.Pivot - _monthlyVm.PivotPointIndicator.M2 ) * 10000 ), SR3rdType.M2 ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.S1, ( int )( ( _monthlyVm.PivotPointIndicator.Pivot - _monthlyVm.PivotPointIndicator.S1 ) * 10000 ), SR3rdType.S1 ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.M1, ( int )( ( _monthlyVm.PivotPointIndicator.Pivot - _monthlyVm.PivotPointIndicator.M1 ) * 10000 ), SR3rdType.M1 ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.S2, ( int )( ( _monthlyVm.PivotPointIndicator.Pivot - _monthlyVm.PivotPointIndicator.S2 ) * 10000 ), SR3rdType.S2 ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.M0, ( int )( ( _monthlyVm.PivotPointIndicator.Pivot - _monthlyVm.PivotPointIndicator.M0 ) * 10000 ), SR3rdType.M0 ) );
                monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.S3, ( int )( ( _monthlyVm.PivotPointIndicator.Pivot - _monthlyVm.PivotPointIndicator.S3 ) * 10000 ), SR3rdType.S3 ) );
                //monthlyPivotLevels.Add( new SRlevel( ref timeBlock, TimeSpan.FromDays( 30 ), _monthlyVm.PivotPointIndicator.Mdn, ( int )( Math.Abs( _monthlyVm.PivotPointIndicator.Mdn - _monthlyVm.PivotPointIndicator.Pivot ) * 10000 ), SR3rdType.MDN ) );


                if ( monthlyPivotLevels.Count > 0 )
                {
                    if ( _01secVm != null ) _01secVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;
                    if ( _01MinVm != null ) _01MinVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;
                    if ( _04MinVm != null ) _04MinVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;                    
                    if ( _05MinVm != null ) _05MinVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;                                       
                    if ( _15MinVm != null ) _15MinVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;
                    if ( _30MinVm != null ) _30MinVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;
                    if ( _01hrsVm != null ) _01hrsVm.DatabarRepo.MonthlyPivots = monthlyPivotLevels;

                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                    SRlevelsTsoCollection srList = null;

                    if ( aa != null )
                    {
                        srList = aa.SupportResistanceBindingList;
                    }
                    else
                    {
                        return;
                    }

                    srList.AddSRlines( monthlyPivotLevels, _security.Decimals.Value );                    
                }
            }
        }
    }
}
