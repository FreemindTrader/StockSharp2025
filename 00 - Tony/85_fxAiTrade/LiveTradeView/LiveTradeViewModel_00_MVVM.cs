using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using fx.Charting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using fx.Definitions;
using fx.Common;
using Ecng.ComponentModel;
using fx.Database;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using fx.Algorithm;
using System.Windows.Media;
using StockSharp.Xaml;

namespace FreemindAITrade.ViewModels
{    
    public partial class LiveTradeViewModel 
    {
        #region Bindable Properties

        private ChartPanelOrderSettings _orderSettings = null;
        
        public ChartPanelOrderSettings OrderSettings
        {
            get
            {
                return _orderSettings;
            }

            set { SetValue( ref _orderSettings, value ); }
        }
               

        string _tabName;
        public string TabName
        {
            get { return _tabName; }
            set { SetValue( ref _tabName, value ); }
        }

        

        

        
                

        public IMarketDataDrive Drive
        {
            get
            {
                if ( _drive == null )
                {
                    _drive = _storageRegistry.DefaultDrive;
                }

                return _drive;
            }
            set
            {
                _drive = value;
            }
        }

        public StorageFormats Format { get; set; } = StorageFormats.Binary;

        public bool CacheBuildableCandles
        {
            get;
            set;
        }

        #endregion



        #region Commands

        public DelegateCommand OnSettingClickedCommand
        {
            get;
            private set;
        }

        public DelegateCommand RegisterOrderCommand
        {
            get;
            private set;
        }

        public DelegateCommand SaveAsCommand
        {
            get;
            private set;
        }

        public DelegateCommand AddAreaCommand
        {
            get;
            private set;
        }


        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  Hi Wang,
         *  
         *  https://supportcenter.devexpress.com/ticket/details/t386209/use-eventtocommand-to-binding-custom-event-of-usercontrol-not-work

            It appears that you have faced one of limitations of EventToCommand - it does not work with custom actions and 
            requires either EventHandler or RoutedEventHandler.
         * 
         * ------------------------------------------------------------------------------------------------------------------------------------------- */

        public DelegateCommand QuickOrderSettingChangedCommand
        {
            get;
            private set;
        }

        

        #endregion

        /// <summary>
        /// Load settings.
        /// </summary>
        /// <param name="storage">Settings storage.</param>
        public override void Load( SettingsStorage storage )
        {            
            OrderSettings.Load( storage.GetValue<SettingsStorage>( "OrderSettings", null ) );

            base.Load( storage );
        }

        /// <summary>
        /// Save settings.
        /// </summary>
        /// <param name="storage">Settings storage.</param>
        public override void Save( SettingsStorage storage )
        {            
            storage.SetValue( "OrderSettings", OrderSettings.Save( ) );

            base.Save( storage );
        }

        public void OnQuickPanelRegisterOrder( Order myOrder )
        {
            myOrder.Portfolio = OrderSettings.Portfolio;
            Action<ChartArea, Order> registerOrder = RegisterOrder;
            if ( registerOrder == null )
                return;

            //registerOrder( this.Areas.FirstOrDefault<ChartArea>( ), myOrder );
        }

        #region Events

        /// <summary>The event of the order registration.</summary>
        public event Action<ChartArea, Order> RegisterOrder;

        #endregion

        

                      
    }
}
