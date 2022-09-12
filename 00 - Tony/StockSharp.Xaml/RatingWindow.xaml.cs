using DevExpress.Xpf.Core;
using Ecng.Common;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class RatingWindow : DXWindow
    {
        public static readonly DependencyProperty RatingValueProperty = DependencyProperty.Register(nameof (RatingValue), typeof (int), typeof (RatingWindow), new PropertyMetadata((object) 0, new PropertyChangedCallback(RatingWindow.OnRatingValuePropertyChanged)));
        private int? _numberOfStars;
        
        public RatingWindow( )
        {
            
            InitializeComponent();
        }

        private static void OnRatingValuePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            RatingWindow ratingWindow = (RatingWindow) d;
            int newValue = (int) e.NewValue;
            UIElementCollection children = ratingWindow.RatingPanel.Children;
            for ( int index = 0; index < newValue; ++index )
            {
                ( ( ToggleButton ) children[ index ] ).IsChecked = new bool?( true );
            }

            for ( int index = newValue; index < children.Count; ++index )
            {
                ( ( ToggleButton ) children[ index ] ).IsChecked = new bool?( false );
            }
        }

        public int RatingValue
        {
            get
            {
                return ( int ) GetValue( RatingWindow.RatingValueProperty );
            }
            set
            {
                switch ( value )
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        SetValue( RatingWindow.RatingValueProperty, ( object ) value );
                        break;
                    default:
                        throw new ArgumentOutOfRangeException( nameof( value ) );
                }
            }
        }

        public string Comment
        {
            get
            {
                return CommentTxt.Text;
            }
            set
            {
                CommentTxt.Text = value;
            }
        }

        private int GetSendTag( object sender )
        {
            if ( sender == null )
            {
                throw new ArgumentNullException( "sender" );
            }

            return ( int ) Converter.To<int>( ( ( FrameworkElement ) sender ).Tag );
        }

        private void OnStarClick( object sender, RoutedEventArgs e )
        {
            int num = GetSendTag(sender);

            if ( !_numberOfStars.HasValue )
            {
                _numberOfStars = new int?( num );
                ( ( ToggleButton ) sender ).IsChecked = new bool?( true );
                OkBtn.IsEnabled = true;
            }
            else
            {
                _numberOfStars = new int?( RatingValue = num );
            }
        }

        private void OnMouseEnterStars( object sender, MouseEventArgs e )
        {
            if ( _numberOfStars.HasValue )
            {
                return;
            }

            RatingValue = GetSendTag( sender );
        }
    }
}
