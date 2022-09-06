using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using Ecng.Xaml;
using FreemindAITrade.ViewModels;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Messages;
using StockSharp.Localization;
using fx.Charting;
using StockSharp.Xaml;
using System.Collections.ObjectModel;
using StockSharp.Studio.Controls;
using fx.Collections;
using DevExpress.Xpf.Docking;
using System.Threading;
//using TracerAttributes;

namespace FreemindAITrade.View
{
    

    /// <summary>
    /// Interaction logic for fxTradeStation.xaml
    /// </summary>
    public partial class TradeStationView : DXRibbonWindow
    {
        //private readonly LayoutManager _layoutManager;
        TradeStationViewModel _viewModel;
        private readonly fxSecurityTrie _allSecurities = new fxSecurityTrie( );

        //[TraceOn]
        public TradeStationView()
        {
            InitializeComponent();

            SourceInitialized += MyWindow_SourceInitialized;

            DataContext = _viewModel = TradeStationViewModel.Create();

            CandleStylesSettings.ItemsSource = TradeStationViewModel.CandleDrawStyleImages;

            WaveDegressSettings.ItemsSource = TradeStationViewModel.WaveCycleImages;

            WaveImportanceSettings.ItemsSource = TradeStationViewModel.WaveImptImages;
        }

        public TradeStationViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }

            private set
            {
                ;
            }
        }

        public Guid ViewGuid { get; set; }

        public static bool IsDebug
        {
#if DEBUG
            get { return true; }
#else
            get { return false; }
#endif
        }

        private void SelectSymbol_ItemClick( object sender, ItemClickEventArgs e )
        {            
            
        }

        private void DockLayoutManager_DockOperationCompleted( object sender, DevExpress.Xpf.Docking.Base.DockOperationCompletedEventArgs e )
        {
            if ( e.Item is LayoutGroup )
            {
                var widths = (e.Item as LayoutGroup).Items.Where(c => c.ItemWidth.IsAbsolute).Select(c => c.ItemWidth.Value);
                if ( widths.Count() > 0 )
                {
                    ( e.Item as LayoutGroup ).ItemWidth = new GridLength( widths.Max() );
                }
                else
                {
                    ( e.Item as LayoutGroup ).ItemWidth = new GridLength( 200 );
                }

            }
        }

        protected static ISecurityProvider SecurityProvider
        {
            get
            {
                return ServicesRegistry.SecurityProvider;
            }
        }

        // https://stackoverflow.com/questions/753552/going-fullscreen-on-secondary-monitor
        /*
         * I notice an answer which advocates setting the position in the Loaded event, but this causes flicker when the window is first shown normal then maximized. 
         * If you subscribe to the SourceInitialized event in your constructor and set the position in there it will handle maximizing onto secondary monitors without flicker
         * 
         */

        void MyWindow_SourceInitialized( object sender, EventArgs e )
        {
            var senderWindow = sender as Window;
            senderWindow.WindowState = WindowState.Maximized;
        }

        private void WaveAnalysisClicked( object sender, ItemClickEventArgs e )
        {            
            var selectSecWnd = new SecuritiesWindowView( )
            {                
                SecurityProvider = SecurityProvider
            };
            
            selectSecWnd.SelectedSecurities = _allSecurities.Retrieve( string.Empty ).Where( s => !s.IsAllSecurity( ) );
            
            if ( !selectSecWnd.ShowModal( this ) )
                return;

            var selected = selectSecWnd.SelectedSecurities;

            _viewModel.StartWaveAnalysis( selected );
        }

        private void DXRibbonWindow_Loaded( object sender, RoutedEventArgs e )
        {
            ThemeManager.SetTheme( TradeControl, Theme.HybridApp );
        }

        //private void DXRibbonWindow_Loaded( object sender, RoutedEventArgs e )
        //{
        //    var senderWindow = sender as Window;
        //    senderWindow.WindowState = WindowState.Maximized;
        //}
    }

    public class fxSecurityTrie : SecurityTrie
    {
        private readonly PooledDictionary<Security, HydraTaskSecurity> _securities = new PooledDictionary<Security, HydraTaskSecurity>( );

        public void AddRange( IEnumerable<HydraTaskSecurity> securities )
        {
            if ( securities == null )
                throw new ArgumentNullException( nameof( securities ) );
            foreach ( HydraTaskSecurity security in securities )
            {
                Add( security.Security );
                _securities.Add( security.Security, security );
            }
        }

        public HydraTaskSecurity GetAllSecurity( )
        {
            return RetrieveHydra( "ALL@ALL" ).FirstOrDefault( );
        }

        public IEnumerable<HydraTaskSecurity> RetrieveHydra( string filter )
        {
            return Retrieve( filter ).Select( s => _securities[ s ] );
        }

        public void RemoveRange( IEnumerable<HydraTaskSecurity> securities )
        {
            if ( securities == null )
                throw new ArgumentNullException( nameof( securities ) );
            Security[ ] array = securities.Select( s => s.Security ).ToArray( );
            RemoveRange( array );
            foreach ( Security key in array )
                _securities.Remove( key );
        }

        public override void Clear( )
        {
            base.Clear( );
            _securities.Clear( );
        }
    }

    public class HydraTaskSecurity : NotifiableObject
    {
        /// <summary>
        /// Хэш-коллекция для быстрой проверки <see cref="P:StockSharp.Hydra.Core.HydraTaskSecurity.DataTypes"/>.
        /// </summary>
        
        public readonly CachedSynchronizedSet< DataType > DataTypesSet = new CachedSynchronizedSet< DataType >( );
        private TypeInfo _tradeInfo = new TypeInfo( );
        private TypeInfo _depthInfo = new TypeInfo( );
        private TypeInfo _orderLogInfo = new TypeInfo( );
        private TypeInfo _level1Info = new TypeInfo( );
        private TypeInfo _candleInfo = new TypeInfo( );
        private TypeInfo _transactionInfo = new TypeInfo( );
        private TypeInfo _positionChangeInfo = new TypeInfo( );

        /// <summary>
        /// Уникальный идентификатор инструмента.
        /// </summary>
        public long Id
        {
            get;
            set;
        }

        /// <summary>
        /// Настройки.
        /// </summary>
        //[RelationSingle( IdentityType = typeof( Guid ) )]
        //public HydraTaskSettings Settings
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// Биржевой инструмент.
        /// </summary>
        
        public Security Security
        {
            get;
            set;
        }

        /// <summary>
        /// Типы данных, которые нужно получать для данного инструмента.
        /// </summary>
        
        public DataType[ ] DataTypes
        {
            get
            {
                return DataTypesSet.Cache;
            }
            set
            {
                if ( value == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }
                if ( value.Any( t => t.MessageType == null ) )
                {
                    throw new ArgumentException( nameof( value ) );
                }
                DataTypesSet.Clear( );
                DataTypesSet.AddRange( value );
            }
        }

        

        /// <summary>
        /// Информация о сделках.
        /// </summary>
        public TypeInfo TradeInfo
        {
            get
            {
                return _tradeInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }
                _tradeInfo = typeInfo;
            }
        }

        /// <summary>
        /// Информация о стаканах.
        /// </summary>
        public TypeInfo DepthInfo
        {
            get
            {
                return _depthInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }
                _depthInfo = typeInfo;
            }
        }

        /// <summary>
        /// Информация о логе заявок.
        /// </summary>
        public TypeInfo OrderLogInfo
        {
            get
            {
                return _orderLogInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }
                _orderLogInfo = typeInfo;
            }
        }

        /// <summary>
        /// Информация о Level1.
        /// </summary>
        public TypeInfo Level1Info
        {
            get
            {
                return _level1Info;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }
                _level1Info = typeInfo;
            }
        }

        /// <summary>
        /// Информация о свечах.
        /// </summary>
        public TypeInfo CandleInfo
        {
            get
            {
                return _candleInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }
                _candleInfo = typeInfo;
            }
        }

        /// <summary>
        /// Информация о логе собственных транзакций.
        /// </summary>
        public TypeInfo TransactionInfo
        {
            get
            {
                return _transactionInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }
                _transactionInfo = typeInfo;
            }
        }

        /// <summary>
        /// Информация об изменениях позиций.
        /// </summary>
        public TypeInfo PositionChangeInfo
        {
            get
            {
                return _positionChangeInfo;
            }
            set
            {
                TypeInfo typeInfo = value;
                if ( typeInfo == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }
                _positionChangeInfo = typeInfo;
            }
        }

        /// <summary>
        /// Информация по типу данных.
        /// </summary>
        public IDictionary<DataType, DateTypeInfo> InfoDict
        {
            get;
            set;
        } = new PooledDictionary<DataType, DateTypeInfo>( );

        /// <summary>
        /// Получить строковое представление.
        /// </summary>
        /// <returns>Строковое представление.</returns>
        public override string ToString( )
        {
            return Security?.ToString( ) ?? string.Empty;
        }

        /// <summary>
        /// Информация по типу данных.
        /// </summary>
        public class TypeInfo : NotifiableObject
        {
            private long _count;
            private DateTime? _lastTime;

            /// <summary>
            /// Обработанное количество данных.
            /// </summary>
            public long Count
            {
                get
                {
                    return _count;
                }
                set
                {
                    _count = value;
                    NotifyPropertyChanged( nameof( Count ) );
                }
            }

            /// <summary>
            /// Временная метка последних обработанных данных.
            /// </summary>
           
            public DateTime? LastTime
            {
                get
                {
                    return _lastTime;
                }
                set
                {
                    _lastTime = value;
                    NotifyPropertyChanged( nameof( LastTime ) );
                }
            }
        }

        /// <summary>
        /// Информация по типу данных.
        /// </summary>
        public class DateTypeInfo : TypeInfo
        {
            private DateTime? _beginDate;
            private DateTime? _endDate;

            /// <summary>
            /// Дата начала загрузки данных.
            /// </summary>
            
            public DateTime? BeginDate
            {
                get
                {
                    return _beginDate;
                }
                set
                {
                    _beginDate = value;
                    NotifyChanged( nameof( BeginDate ) );
                }
            }

            /// <summary>
            /// Дата окончания загрузки данных.
            /// </summary>
            
            public DateTime? EndDate
            {
                get
                {
                    return _endDate;
                }
                set
                {
                    _endDate = value;
                    NotifyChanged( nameof( EndDate ) );
                }
            }
        }
    }
}
