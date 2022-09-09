using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Controls;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using StockSharp.Xaml;
using System;
using System.Linq;
using System.Windows;

namespace FreemindAITrade.ViewModels
{
    [POCOViewModel]
    public class PortfolioPickerWindowViewModel : IPersistable
    {
        public void OnLoaded( RoutedEventArgs e )
        {
            BaseUserConfig<StudioUserConfig>.Instance.TryLoadSettings( "PortfolioPickerView", new Action<SettingsStorage>( Load ) );
        }

        [Command( false )]
        public void OnSelectedItemChanged( SelectedItemChangedEventArgs args )
        {
            PortfolioName = SelectedPortfolio.Portfolio.Name;
        }

        [Command( false )]
        public void OnDoubleClick( EventArgs args )
        {
            SelectedPortfolio = LastClickedPortfolio;
            PortfolioName = LastClickedPortfolio.Name;
        }

        [Command( false )]
        public void OnLayoutChanged( EventArgs args )
        {

        }


        /// <summary>The selected portfolio.</summary>
        public virtual Portfolio SelectedPortfolio { get; set; }
        public virtual Portfolio LastClickedPortfolio { get; set; }

        string _portfolioName;
        public string PortfolioName
        {
            get => _portfolioName;
            set
            {
                _portfolioName = value;

                foreach ( var port in PortfolioDataSource.Portfolios )
                {
                    if ( port.Name == _portfolioName )
                    {
                        SelectedPortfolio = port;
                    }

                }
            }
        }


        /// <summary>Available portfolios.</summary>
        public virtual PortfolioDataSource PortfolioDataSource { get; set; }


        public static PortfolioPickerWindowViewModel Create()
        {
            return ViewModelSource.Create( () => new PortfolioPickerWindowViewModel() );
        }

        public void Load( SettingsStorage storage )
        {
            PortfolioName = storage.GetValue<string>( nameof( PortfolioName ) );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( nameof( PortfolioName ), PortfolioName );
        }
    }
}
