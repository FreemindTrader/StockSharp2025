using DevExpress.Xpf.Grid;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    /// <summary>
    /// Interaction logic for NewsPanel.xaml
    /// </summary>
    public partial class NewsPanel : UserControl, IPersistable
    {
        public NewsPanel( )
        {
            InitializeComponent();
        }

        public INewsProvider NewsProvider
        {
            get
            {
                return this.NewsGrid.NewsProvider;
            }
            set
            {
                this.NewsGrid.NewsProvider = value;
            }
        }

        public void Load( SettingsStorage storage )
        {
            NewsGrid.Load( ( SettingsStorage ) storage.GetValue<SettingsStorage>( "NewsGrid", null ) );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue<SettingsStorage>( "NewsGrid", NewsGrid.Save( ) );
        }

        private void OnSelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {
            News firstSelectedNews = this.NewsGrid.FirstSelectedNews;
            string text = "<HTML/>";

            if ( firstSelectedNews != null && !StringHelper.IsEmpty( firstSelectedNews.Story ) )
            {
                text = "<meta http-equiv=Content-Type content='text/html;charset=UTF-8'>" + firstSelectedNews.Story;
            }
                
            NewsBrowser.NavigateToString( text );
        }
    }
}
