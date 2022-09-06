
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
            this.InputBorder.Child = ( UIElement )this.CreateControl();
            this.IndexSecurityWindow.Caption = ( object )LocalizedStrings.ContinuousSecurity;
            this._editor.Changed += new Action( this.SecurityChanged );
            this._editor.Drop += new DragEventHandler( this.EditorOnDrop );
            ISecurityProvider secProvider = BaseStudioControl.SecurityProvider;
            DateTime today = DateTime.Today;
            DateTime from = today.AddMonths( -4 );
            today = DateTime.Today;
            DateTime to = today.AddMonths( 6 );
            Func<string, Security> getSecurity = ( Func<string, Security> )( code => secProvider.LookupById( code + "@" + ExchangeBoard.Forts.Code ) );
            foreach ( Security fortsJump in "RI".GetFortsJumps( from, to, getSecurity, true ) )
            {
                DateTimeOffset? expiryDate = fortsJump.ExpiryDate;
                DateTime dateTime;
                if ( !expiryDate.HasValue )
                {
                    if ( fortsJump.Code.Length < 4 )
                        throw new InvalidOperationException( LocalizedStrings.Str3228Params.Put( ( object )fortsJump.Code ) );
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
                this._editor.Jumps.Add( new SecurityJump()
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
            Grid.SetColumn( ( UIElement )this._editor, 0 );
            grid.Children.Add( ( UIElement )this._editor );
            StackPanel stackPanel1 = new StackPanel();
            stackPanel1.VerticalAlignment = VerticalAlignment.Center;
            UIElementCollection children1 = stackPanel1.Children;
            Button button1 = new Button();
            button1.Content = ( object )"\xF0EF";
            button1.FontFamily = new FontFamily( "Wingdings" );
            button1.FontSize = 15.0;
            button1.Margin = new Thickness( 2.0 );
            button1.Command = ( ICommand )new DelegateCommand( ( Action<object> )( o => this._editor.Jumps.Add( new SecurityJump()
            {
                Security = this.SecurityPicker.SelectedSecurity
            } ) ), ( Predicate<object> )( o => this.SecurityPicker.SelectedSecurity != null ) );
            button1.ToolTip = ( object )LocalizedStrings.Str3229;
            children1.Add( ( UIElement )button1 );
            UIElementCollection children2 = stackPanel1.Children;
            Button button2 = new Button();
            button2.Content = ( object )"\xF0F0";
            button2.FontFamily = new FontFamily( "Wingdings" );
            button2.FontSize = 15.0;
            button2.Margin = new Thickness( 2.0 );
            button2.Command = ( ICommand )new DelegateCommand( ( Action<object> )( o => this._editor.Jumps.RemoveRange<SecurityJump>( this._editor.SelectedJumps ) ), ( Predicate<object> )( o => this._editor.SelectedJump != null ) );
            button2.ToolTip = ( object )LocalizedStrings.Str2060;
            children2.Add( ( UIElement )button2 );
            StackPanel stackPanel2 = stackPanel1;
            Grid.SetColumn( ( UIElement )stackPanel2, 1 );
            grid.Children.Add( ( UIElement )stackPanel2 );
            return grid;
        }

        private void SecurityChanged()
        {
            this.ShowError( this._editor.Validate() ?? this.Validate( this._editor.Jumps.Select<SecurityJump, Security>( ( Func<SecurityJump, Security> )( j => j.Security ) ), ( Security )null ) );
        }

        protected override bool OnSecurityChanged( Security security )
        {
            ExpirationContinuousSecurity continuousSecurity = ( ExpirationContinuousSecurity )security;
            if ( continuousSecurity.ExpirationJumps.IsEmpty<KeyValuePair<SecurityId, DateTimeOffset>>() )
                return false;
            this._editor.Jumps.Clear();
            this._editor.Jumps.AddRange<SecurityJump>( continuousSecurity.ExpirationJumps.Select<KeyValuePair<SecurityId, DateTimeOffset>, SecurityJump>( ( Func<KeyValuePair<SecurityId, DateTimeOffset>, SecurityJump> )( p => new SecurityJump()
            {
                Security = BaseStudioControl.SecurityProvider.LookupById( p.Key ),
                Date = p.Value.UtcDateTime
            } ) ) );
            this.SecurityChanged();
            return true;
        }

        protected override void UpdateSecurity( Security security )
        {
            ExpirationContinuousSecurity continuousSecurity = ( ExpirationContinuousSecurity )security;
            continuousSecurity.ExpirationJumps.Clear();
            continuousSecurity.ExpirationJumps.AddRange<KeyValuePair<SecurityId, DateTimeOffset>>( this._editor.Jumps.Select<SecurityJump, KeyValuePair<SecurityId, DateTimeOffset>>( ( Func<SecurityJump, KeyValuePair<SecurityId, DateTimeOffset>> )( j => new KeyValuePair<SecurityId, DateTimeOffset>( j.Security.ToSecurityId( ( SecurityIdGenerator )null, true, false ), ( DateTimeOffset )j.Date ) ) ) );
        }

        protected override void InsertSecurity( Security security )
        {
            this._editor.Jumps.Add( new SecurityJump()
            {
                Security = security
            } );
        }

        private void EditorOnDrop( object sender, DragEventArgs e )
        {
            Security data = ( Security )e.Data.GetData( typeof( Security ) );
            this._editor.Jumps.Add( new SecurityJump()
            {
                Security = data
            } );
        }
    }
}
