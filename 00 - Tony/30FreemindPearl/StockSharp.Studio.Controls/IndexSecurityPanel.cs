
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.ComponentModel.Expressions;
using StockSharp.Algo;
using StockSharp.Algo.Expressions;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str2691" )]
    [DescriptionLoc( "Str3225", false )]
    [VectorIcon( "Chart3" )]
    public class IndexSecurityPanel : CompositeSecurityPanel
    {
        private readonly Dictionary<string, Security> _securities = new Dictionary<string, Security>();
        private readonly TextBox _editor;

        public override Type SecurityType
        {
            get
            {
                return typeof( ExpressionIndexSecurity );
            }
        }

        public string Expression
        {
            get
            {
                return this._editor.Text;
            }
            set
            {
                this._editor.Text = value;
            }
        }

        protected override string DefaultSecurityCode
        {
            get
            {
                return "Index";
            }
        }

        public IndexSecurityPanel() : base()
        {
            TextBox textBox = new TextBox();
            textBox.AllowDrop = true;
            this._editor = textBox;
            
            this.InputBorder.Child = ( UIElement )this._editor;
            this.IndexSecurityWindow.Caption = ( object )LocalizedStrings.Index;
            this._editor.TextChanged += ( TextChangedEventHandler )( ( s, a ) => this.Validate() );
            this._editor.Drop += new DragEventHandler( this.InputTextBox_OnDrop );
            this._editor.PreviewDragOver += new DragEventHandler( this.InputTextBox_OnPreviewDragOver );
            DateTime today = DateTime.Today;
            DateTime from = today.AddMonths( -4 );
            today = DateTime.Today;
            DateTime to = today.AddMonths( 1 );
            Security[ ] array = "RI".GetFortsJumps( from, to, ( Func<string, Security> )( code => new Security() { Id = code + "@" + ExchangeBoard.Forts.Code, Code = code, Board = ExchangeBoard.Forts } ), true ).Take<Security>( 2 ).ToArray<Security>();
            this.Expression = "{0} / {1}".Put( ( object )array[1], ( object )array[0] );
        }

        protected override bool OnSecurityChanged( Security security )
        {
            if ( !security.IsBasket() )
                return false;
            this.Expression = security.BasketExpression;
            return true;
        }

        protected override void UpdateSecurity( Security security )
        {
            security.BasketExpression = this.Expression;
        }

        protected override void InsertSecurity( Security security )
        {
            this._editor.Text = this._editor.Text.Insert( this._editor.CaretIndex, " " + security.Id + " " );
        }

        private void Validate()
        {
            if ( !this.Expression.IsEmpty() )
            {
                ExpressionFormula expressionFormula = this.Expression.Compile( true );
                if ( !expressionFormula.Error.IsEmpty() )
                {
                    this.ShowError( expressionFormula.Error );
                }
                else
                {
                    var array = expressionFormula.Identifiers.Select( id => new { Id = id, Security = this.TryGetSecurity( id ) } ).ToArray();
                    var data = array.FirstOrDefault( m => m.Security == null );
                    string errorText;
                    if ( data == null )
                        errorText = this.Validate( array.Select( s => s.Security ), ( Security )null );
                    else
                        errorText = LocalizedStrings.Str704Params.Put( ( object )data.Id );
                    this.ShowError( errorText );
                }
            }
            else
                this.ShowError( ( string )null );
        }

        private Security TryGetSecurity( string id )
        {
            Security security1 = this._securities.TryGetValue<string, Security>( id );
            if ( security1 != null )
                return security1;
            Security security2 = BaseStudioControl.SecurityProvider.LookupById( id );
            if ( security2 != null )
                this._securities.Add( id, security2 );
            return security2;
        }

        private void InputTextBox_OnDrop( object sender, DragEventArgs e )
        {
            this._editor.Text = this._editor.Text.Insert( this._editor.SelectionStart, " " + ( ( Security )e.Data.GetData( typeof( Security ) ) ).Id + " " );
        }

        private void InputTextBox_OnPreviewDragOver( object sender, DragEventArgs e )
        {
            this._editor.SelectionStart = IndexSecurityPanel.GetCaretIndexFromPoint( this._editor, e.GetPosition( ( IInputElement )this._editor ) );
            this._editor.SelectionLength = 0;
            this._editor.Focus();
            e.Handled = true;
        }

        private static int GetCaretIndexFromPoint( TextBox textBox, Point point )
        {
            int cIndex = textBox.GetCharacterIndexFromPoint( point, true );
            if ( cIndex != textBox.Text.Length - 1 )
                return cIndex;
            
            Rect myRect = textBox.GetRectFromCharacterIndex( cIndex );
            
            
            Point scrPt = new Point( myRect.X, myRect.Y );

            
            if ( point.X > scrPt.X )
                ++cIndex;

            return cIndex;
        }
    }
}
