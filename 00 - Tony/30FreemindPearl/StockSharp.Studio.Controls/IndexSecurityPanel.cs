
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
                return _editor.Text;
            }
            set
            {
                _editor.Text = value;
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
            _editor = textBox;
            
            InputBorder.Child = _editor;
            IndexSecurityWindow.Caption = LocalizedStrings.Index;
            _editor.TextChanged += ( s, a ) => Validate();
            _editor.Drop += new DragEventHandler( InputTextBox_OnDrop );
            _editor.PreviewDragOver += new DragEventHandler( InputTextBox_OnPreviewDragOver );
            DateTime today = DateTime.Today;
            DateTime from = today.AddMonths( -4 );
            today = DateTime.Today;
            DateTime to = today.AddMonths( 1 );
            Security[ ] array = "RI".GetFortsJumps( from, to, code => new Security() { Id = code + "@" + ExchangeBoard.Forts.Code, Code = code, Board = ExchangeBoard.Forts }, true ).Take( 2 ).ToArray();
            Expression = "{0} / {1}".Put( array[1], array[0] );
        }

        protected override bool OnSecurityChanged( Security security )
        {
            if ( !security.IsBasket() )
                return false;
            Expression = security.BasketExpression;
            return true;
        }

        protected override void UpdateSecurity( Security security )
        {
            security.BasketExpression = Expression;
        }

        protected override void InsertSecurity( Security security )
        {
            _editor.Text = _editor.Text.Insert( _editor.CaretIndex, " " + security.Id + " " );
        }

        private void Validate()
        {
            if ( !Expression.IsEmpty() )
            {
                ExpressionFormula expressionFormula = Expression.Compile( true );
                if ( !expressionFormula.Error.IsEmpty() )
                {
                    ShowError( expressionFormula.Error );
                }
                else
                {
                    var array = expressionFormula.Identifiers.Select( id => new { Id = id, Security = TryGetSecurity( id ) } ).ToArray();
                    var data = array.FirstOrDefault( m => m.Security == null );
                    string errorText;
                    if ( data == null )
                        errorText = Validate( array.Select( s => s.Security ), null );
                    else
                        errorText = LocalizedStrings.Str704Params.Put( data.Id );
                    ShowError( errorText );
                }
            }
            else
                ShowError( null );
        }

        private Security TryGetSecurity( string id )
        {
            Security security1 = _securities.TryGetValue( id );
            if ( security1 != null )
                return security1;
            Security security2 = SecurityProvider.LookupById( id );
            if ( security2 != null )
                _securities.Add( id, security2 );
            return security2;
        }

        private void InputTextBox_OnDrop( object sender, DragEventArgs e )
        {
            _editor.Text = _editor.Text.Insert( _editor.SelectionStart, " " + ( ( Security )e.Data.GetData( typeof( Security ) ) ).Id + " " );
        }

        private void InputTextBox_OnPreviewDragOver( object sender, DragEventArgs e )
        {
            _editor.SelectionStart = GetCaretIndexFromPoint( _editor, e.GetPosition( _editor ) );
            _editor.SelectionLength = 0;
            _editor.Focus();
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
