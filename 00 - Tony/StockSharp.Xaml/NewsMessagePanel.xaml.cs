using DevExpress.Xpf.Grid;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
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
    /// Interaction logic for NewsMessagePanel.xaml
    /// </summary>
    public partial class NewsMessagePanel : UserControl, IPersistable
    {
        public NewsMessagePanel( )
        {
            InitializeComponent();
        }

        public INewsProvider NewsProvider
        {
            get
            {
                return NewsGrid.NewsProvider;
            }
            set
            {
                NewsGrid.NewsProvider = value;
            }
        }

        public void Load( SettingsStorage storage )
        {
            NewsGrid.Load( ( SettingsStorage ) storage.GetValue<SettingsStorage>( "NewsGrid",  null ) );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue<SettingsStorage>( "NewsGrid", NewsGrid.Save(  ) );
        }

        private void OnSelectionChanged( object sender, GridSelectionChangedEventArgs e )
        {
            var selectedMessage = this.NewsGrid.SelectedMessage;

            string text = "<HTML/>";

            if ( selectedMessage != null && !StringHelper.IsEmpty( selectedMessage.Story ) )
            {
                text = "<meta http-equiv=Content-Type content='text/html;charset=UTF-8'>" + selectedMessage.Story;
            }
                
            NewsBrowser.NavigateToString( text );
        }
    }
}
