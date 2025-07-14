using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using MoreLinq;
using SciChart.Charting.Visuals.TradeChart;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Mvvm.UI;
using StockSharp.Xaml.Charting.Definitions;
using System.Windows;
using DevExpress.Xpf.Grid;
using SciChart.Charting.Visuals;
using fx.Common;
using StockSharp.Xaml;



#pragma warning disable 067

namespace StockSharp.Xaml.Charting
{
    public partial class ChartExViewModel : DevExpress.Mvvm.ViewModelBase, IChart, IPersistable, IThemeableChart
    {
        #region Bindable Properties

        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  Tony: Well, I have just find out that BindableBase is only providing INotifyPropertyChange and not Doing anything with 
         *  Dependency Properties.
         *  
         *  So the following documentation is wrong. Well I believe I don't need the following to be dependency Properties as I am 
         *  not 
         *  
         *  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
         *   
         *  The following is DevExpress's implementation of Dependency Propoerties which is more cleaner.
         *  
         *  Normal Coding
         *          
         *      public static readonly DependencyProperty SelectedThemeProperty = DependencyProperty.Register( nameof( SelectedTheme ) , typeof( string ), typeof( ChartViewModel ) );  
         *      
         *      public string SelectedTheme
                {
                    get
                    {
                        return ( string ) GetValue( SelectedThemeProperty );
                    }
                    set
                    {
                        SetValue( SelectedThemeProperty, value );
                    }
                }
         *
         **  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
         * ------------------------------------------------------------------------------------------------------------------------------------------- */

        string _selectedTheme = string.Empty;

        public string SelectedTheme
        {
            get { return _selectedTheme; }
            set { SetValue( ref _selectedTheme, value ); }
        }

        bool _isShowOverview = false;
        public bool ShowOverview
        {
            get { return _isShowOverview; }
            set { SetValue( ref _isShowOverview, value ); }
        }

        bool _isShowLegend = false;
        public bool ShowLegend
        {
            get { return _isShowLegend; }
            set { SetValue( ref _isShowLegend, value ); }
        }

        bool _isInteracted = false;
        public bool IsInteracted
        {
            get { return _isInteracted; }
            set { SetValue( ref _isInteracted, value ); }
        }

        bool _IsProgrammable = false;
        public bool IsProgrammable
        {
            get { return _IsProgrammable; }
            set { SetValue( ref _IsProgrammable, value ); }
        }

        bool _allowAddArea = true;
        public bool AllowAddArea
        {
            get { return _allowAddArea; }
            set { SetValue( ref _allowAddArea, value, changedCallback: OnAllowAddXXXChanged ); }
        }

        bool _allowAddAxis = true;
        public bool AllowAddAxis
        {
            get
            {
                return _allowAddAxis;
            }
            set
            {
                 SetValue( ref _allowAddAxis, value, changedCallback: OnAllowAddXXXChanged );                 
            }
        }

        bool _allowAddIndicatorss = true;
        public bool AllowAddIndicators
        {
            get
            {
                return _allowAddIndicatorss;
            }
            
            set { SetValue( ref _allowAddIndicatorss, value, changedCallback: OnAllowAddXXXChanged ); }
            
        }

        bool _allowAddCandles = true;
        public bool AllowAddCandles
        {
            get
            {
                return _allowAddCandles;
            }
            
            set { SetValue( ref _allowAddCandles, value, changedCallback: OnAllowAddXXXChanged ); }
            
        }

        bool _allowAddOrders = true;
        public bool AllowAddOrders
        {
            get
            {
                return _allowAddOrders;
            }
            
            set { SetValue( ref _allowAddOrders, value, changedCallback: OnAllowAddXXXChanged ); }
            
        }

        bool _allowAddOwnTrades = true;
        public bool AllowAddOwnTrades
        {
            get
            {
                return _allowAddOwnTrades;
            }
            
            set { SetValue( ref _allowAddOwnTrades, value, changedCallback: OnAllowAddXXXChanged ); }            
        }

        int _minimumRange = 0;
        public int MinimumRange
        {
            get { return _minimumRange; }
            set { SetValue( ref _minimumRange, value ); }
        }

        ObservableCollection< ScichartSurfaceMVVM > _scichartSurfaceViewModels = null;

        public ObservableCollection<ScichartSurfaceMVVM> ScichartSurfaceViewModels
        {
            get { return _scichartSurfaceViewModels; }
            set { SetValue( ref _scichartSurfaceViewModels, value ); }
        }


        ObservableCollection<IndicatorType> _indicatorTypes = null;

        public ObservableCollection<IndicatorType> IndicatorTypes
        {
            get { return _indicatorTypes; }
            set { SetValue( ref _indicatorTypes, value ); }
        }

        void OnAllowAddXXXChanged( )
        {
            ClosePaneCommand.RaiseCanExecuteChanged( );
            Action myEvent = RefreshEvent;
            if ( myEvent == null )
                return;
            myEvent( );
        }

        #endregion
       
    }
}
