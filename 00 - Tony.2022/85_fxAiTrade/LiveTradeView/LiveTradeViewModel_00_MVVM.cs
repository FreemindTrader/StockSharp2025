using DevExpress.Mvvm;
using Ecng.Common;
using Ecng.Serialization;
using fx.Charting;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using System;
using System.Linq;

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
            storage.SetValue( "OrderSettings", OrderSettings.Save() );

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
