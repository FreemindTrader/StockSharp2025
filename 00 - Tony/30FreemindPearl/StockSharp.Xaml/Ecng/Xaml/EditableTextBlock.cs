using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    public class EditableTextBlock : TextBlock
    {
        /// <summary>
        /// </summary>
        public static readonly DependencyProperty IsInEditModeProperty = DependencyProperty.Register( nameof( IsInEditMode ), typeof( bool ), typeof( EditableTextBlock ), ( PropertyMetadata )new UIPropertyMetadata( ( object )false, new PropertyChangedCallback( OnIsInEditModePropertyChanged ) ) );
        /// <summary>
        /// </summary>
        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register( nameof( MaxLength ), typeof( int ), typeof( EditableTextBlock ), ( PropertyMetadata )new UIPropertyMetadata( ( object )0 ) );
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private EditableTextBlockAdorner _editableTextBlockAdorner;

        /// <summary>
        /// </summary>
        public bool IsInEditMode
        {
            get
            {
                return ( bool )this.GetValue( EditableTextBlock.IsInEditModeProperty );
            }
            set
            {
                this.SetValue( EditableTextBlock.IsInEditModeProperty, ( object )value );
            }
        }

        /// <summary>
        /// </summary>
        public int MaxLength
        {
            get
            {
                return ( int )this.GetValue( EditableTextBlock.MaxLengthProperty );
            }
            set
            {
                this.SetValue( EditableTextBlock.MaxLengthProperty, ( object )value );
            }
        }

        private static void OnIsInEditModePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            EditableTextBlock adornedElement = d as EditableTextBlock;
            if ( adornedElement == null )
                return;
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer( ( Visual )adornedElement );
            if ( adornedElement.IsInEditMode )
            {
                if ( adornedElement._editableTextBlockAdorner == null )
                {
                    adornedElement._editableTextBlockAdorner = new EditableTextBlockAdorner( adornedElement );
                    adornedElement._editableTextBlockAdorner.TextBoxKeyUp += new KeyEventHandler( adornedElement.OnTextBoxKeyUp );
                    adornedElement._editableTextBlockAdorner.TextBoxLostFocus += new RoutedEventHandler( adornedElement.OnTextBoxLostFocus );
                }
                adornerLayer.Add( ( Adorner )adornedElement._editableTextBlockAdorner );
            }
            else
            {
                Adorner[ ] adorners = adornerLayer.GetAdorners( ( UIElement )adornedElement );
                if ( adorners != null )
                {
                    foreach ( Adorner adorner in adorners )
                    {
                        if ( adorner is EditableTextBlockAdorner )
                            adornerLayer.Remove( adorner );
                    }
                }
                adornedElement.GetBindingExpression( TextBlock.TextProperty )?.UpdateTarget();
            }
        }

        private void OnTextBoxLostFocus( object _param1, RoutedEventArgs _param2 )
        {
            this.IsInEditMode = false;
        }

        private void OnTextBoxKeyUp( object _param1, KeyEventArgs _param2 )
        {
            if ( _param2.Key != Key.Return )
                return;
            this.IsInEditMode = false;
        }

        /// <inheritdoc />
        protected override void OnMouseDown( MouseButtonEventArgs e )
        {
            if ( e.MiddleButton == MouseButtonState.Pressed )
            {
                this.IsInEditMode = true;
            }
            else
            {
                if ( e.ClickCount != 2 )
                    return;
                this.IsInEditMode = true;
            }
        }
    }
}
