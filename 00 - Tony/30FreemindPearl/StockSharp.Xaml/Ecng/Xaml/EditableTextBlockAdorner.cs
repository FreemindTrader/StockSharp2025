// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.EditableTextBlockAdorner
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Ecng.Xaml
{
    /// <summary>
    /// Adorner class which shows TextBox over the text block when the Edit mode is on.
    /// </summary>
    public class EditableTextBlockAdorner : Adorner
    {
        
        private readonly VisualCollection _visualCollectionD;
        
        private readonly TextBox _textBox;
        
        private readonly TextBlock _textBlock;

        /// <summary>
        /// </summary>
        public EditableTextBlockAdorner( EditableTextBlock adornedElement ) : base( ( UIElement )adornedElement )
        {
            Binding binding = new Binding( nameof( 2127280916 ) ) { Source = ( object )adornedElement };
            this._visualCollectionD = new VisualCollection( ( Visual )this );
            this._textBlock = ( TextBlock )adornedElement;
            TextBox textBox = new TextBox();
            textBox.AcceptsReturn = true;
            textBox.MaxLength = adornedElement.MaxLength;
            textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            this._textBox = textBox;
            this._textBox.SetBinding( TextBox.TextProperty, ( BindingBase )binding );
            this._textBox.KeyUp += new KeyEventHandler( this.OnKeyUp );
            this._textBox.Loaded += new RoutedEventHandler( this.OnLoaded );
            this._visualCollectionD.Add( ( Visual )this._textBox );
        }

        private void OnKeyUp( object _param1, KeyEventArgs _param2 )
        {
            if ( _param2.Key != Key.Return && _param2.Key != Key.Escape )
                return;
            this._textBox.Text = this._textBox.Text.Remove( Environment.NewLine, false );
            this._textBox.GetBindingExpression( TextBox.TextProperty )?.UpdateSource();
        }

        /// <inheritdoc />
        protected override Visual GetVisualChild( int index )
        {
            return this._visualCollectionD[index];
        }

        /// <inheritdoc />
        protected override int VisualChildrenCount
        {
            get
            {
                return this._visualCollectionD.Count;
            }
        }

        /// <inheritdoc />
        protected override Size ArrangeOverride( Size finalSize )
        {
            Rect finalRect = new Rect( -5.0, -3.0, this._textBlock.ActualWidth + 60.0, this._textBlock.ActualHeight * 1.5 );

            this._textBox.Arrange( finalRect );
            this._textBox.Focus();
            return finalSize;
        }

        /// <summary>
        /// </summary>
        public event RoutedEventHandler TextBoxLostFocus
        {
            add
            {
                this._textBox.LostFocus += value;
            }
            remove
            {
                this._textBox.LostFocus -= value;
            }
        }

        /// <summary>
        /// </summary>
        public event KeyEventHandler TextBoxKeyUp
        {
            add
            {
                this._textBox.KeyUp += value;
            }
            remove
            {
                this._textBox.KeyUp -= value;
            }
        }

        private void OnLoaded( object _param1, RoutedEventArgs _param2 )
        {
            this._textBox.SelectAll();
        }
    }
}
