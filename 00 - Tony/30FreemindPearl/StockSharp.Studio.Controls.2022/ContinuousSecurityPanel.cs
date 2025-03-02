
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str3226" )]
    [DescriptionLoc( "Str3227", false )]
    [VectorIcon( "Chart1" )]
    public class ContinuousSecurityPanel : CompositeSecurityPanel
    {
        private readonly SecurityJumpsEditor _editor = new SecurityJumpsEditor();

        public override Type SecurityType
        {
            get
            {
                return typeof( ContinuousSecurity );
            }
        }

        protected override string DefaultSecurityCode
        {
            get
            {
                return "Continuous";
            }
        }

        public ContinuousSecurityPanel()
        {
            InputBorder.Child = CreateControl();
            IndexSecurityWindow.Caption = LocalizedStrings.ContinuousSecurity;
            _editor.Changed += new Action( SecurityChanged );
            _editor.Drop += new DragEventHandler( EditorOnDrop );
            ISecurityProvider secProvider = SecurityProvider;
            DateTime today = DateTime.Today;
            DateTime from = today.AddMonths( -4 );
            today = DateTime.Today;
            DateTime to = today.AddMonths( 6 );
            Func<string, Security> getSecurity = code => secProvider.LookupById( code + "@" + ExchangeBoard.Forts.Code );
            foreach ( Security fortsJump in "RI".GetFortsJumps( from, to, getSecurity, true ) )
            {
                DateTimeOffset? expiryDate = fortsJump.ExpiryDate;
                DateTime dateTime;
                if ( !expiryDate.HasValue )
                {
                    if ( fortsJump.Code.Length < 4 )
                        throw new InvalidOperationException( LocalizedStrings.Str3228Params.Put( fortsJump.Code ) );
                    int year = DateTime.Today.Year / 10 * 10 + fortsJump.Code.Substring( 3, 1 ).To<int>();
                    int num;
                    switch ( fortsJump.Code[2] )
                    {
                        case 'H':
                            num = 3;
                            break;
                        case 'M':
                            num = 6;
                            break;
                        case 'U':
                            num = 9;
                            break;
                        case 'Z':
                            num = 12;
                            break;
                        default:
                            throw new InvalidOperationException( fortsJump.Code[2].To<string>() );
                    }
                    int month = num;
                    dateTime = new DateTime( year, month, 15 );
                }
                else
                {
                    expiryDate = fortsJump.ExpiryDate;
                    dateTime = expiryDate.Value.UtcDateTime;
                }
                _editor.Jumps.Add( new SecurityJump()
                {
                    Security = fortsJump,
                    Date = dateTime
                } );
            }
        }

        private Grid CreateControl()
        {
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add( new ColumnDefinition() );
            grid.ColumnDefinitions.Add( new ColumnDefinition()
            {
                Width = GridLength.Auto
            } );
            Grid.SetColumn( _editor, 0 );
            grid.Children.Add( _editor );
            StackPanel stackPanel1 = new StackPanel();
            stackPanel1.VerticalAlignment = VerticalAlignment.Center;
            UIElementCollection children1 = stackPanel1.Children;
            Button button1 = new Button();
            button1.Content = "\xF0EF";
            button1.FontFamily = new FontFamily( "Wingdings" );
            button1.FontSize = 15.0;
            button1.Margin = new Thickness( 2.0 );
            button1.Command = new DelegateCommand( o => _editor.Jumps.Add( new SecurityJump()
            {
                Security = SecurityPicker.SelectedSecurity
            } ), o => SecurityPicker.SelectedSecurity != null );
            button1.ToolTip = LocalizedStrings.Str3229;
            children1.Add( button1 );
            UIElementCollection children2 = stackPanel1.Children;
            Button button2 = new Button();
            button2.Content = "\xF0F0";
            button2.FontFamily = new FontFamily( "Wingdings" );
            button2.FontSize = 15.0;
            button2.Margin = new Thickness( 2.0 );
            button2.Command = new DelegateCommand( o => _editor.Jumps.RemoveRange( _editor.SelectedJumps ), o => _editor.SelectedJump != null );
            button2.ToolTip = LocalizedStrings.Str2060;
            children2.Add( button2 );
            StackPanel stackPanel2 = stackPanel1;
            Grid.SetColumn( stackPanel2, 1 );
            grid.Children.Add( stackPanel2 );
            return grid;
        }

        private void SecurityChanged()
        {
            ShowError( _editor.Validate() ?? Validate( _editor.Jumps.Select( j => j.Security ), null ) );
        }

        protected override bool OnSecurityChanged( Security security )
        {
            ExpirationContinuousSecurity continuousSecurity = ( ExpirationContinuousSecurity )security;
            if ( continuousSecurity.ExpirationJumps.IsEmpty() )
                return false;
            _editor.Jumps.Clear();
            _editor.Jumps.AddRange( continuousSecurity.ExpirationJumps.Select( p => new SecurityJump()
            {
                Security = SecurityProvider.LookupById( p.Key ),
                Date = p.Value.UtcDateTime
            } ) );
            SecurityChanged();
            return true;
        }

        protected override void UpdateSecurity( Security security )
        {
            ExpirationContinuousSecurity continuousSecurity = ( ExpirationContinuousSecurity )security;
            continuousSecurity.ExpirationJumps.Clear();
            continuousSecurity.ExpirationJumps.AddRange( _editor.Jumps.Select( j => new KeyValuePair<SecurityId, DateTimeOffset>( j.Security.ToSecurityId( null, true, false ), ( DateTimeOffset )j.Date ) ) );
        }

        protected override void InsertSecurity( Security security )
        {
            _editor.Jumps.Add( new SecurityJump()
            {
                Security = security
            } );
        }

        private void EditorOnDrop( object sender, DragEventArgs e )
        {
            Security data = ( Security )e.Data.GetData( typeof( Security ) );
            _editor.Jumps.Add( new SecurityJump()
            {
                Security = data
            } );
        }
    }
}
