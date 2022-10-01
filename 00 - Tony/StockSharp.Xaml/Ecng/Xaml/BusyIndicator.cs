using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    [StyleTypedProperty( Property = "OverlayStyle", StyleTargetType = typeof( Rectangle ) )]
    public class BusyIndicator : ContentControl
    {
        /// <summary>
        /// </summary>
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register( nameof( IsBusy ), typeof( bool ), typeof( BusyIndicator ), new PropertyMetadata( false, new PropertyChangedCallback( OnIsBusyPropertyChanged ) ) );
        /// <summary>Content which is used when is busy.</summary>
        public static readonly DependencyProperty BusyContentProperty = DependencyProperty.Register( nameof( BusyContent ), typeof( object ), typeof( BusyIndicator ), new PropertyMetadata( ( PropertyChangedCallback )null ) );
        /// <summary>Content template which is used when is busy.</summary>
        public static readonly DependencyProperty BusyContentTemplateProperty = DependencyProperty.Register( nameof( BusyContentTemplate ), typeof( DataTemplate ), typeof( BusyIndicator ), new PropertyMetadata( ( PropertyChangedCallback )null ) );
        /// <summary>
        /// </summary>
        public static readonly DependencyProperty OverlayStyleProperty = DependencyProperty.Register( nameof( OverlayStyle ), typeof( Style ), typeof( BusyIndicator ), new PropertyMetadata( ( PropertyChangedCallback )null ) );

        private bool _isContentVisible;

        static BusyIndicator()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata( typeof( BusyIndicator ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )typeof( BusyIndicator ) ) );
        }



        /// <summary>
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return ( bool )this.GetValue( BusyIndicator.IsBusyProperty );
            }
            set
            {
                this.SetValue( BusyIndicator.IsBusyProperty, ( object )value );
            }
        }

        private static void OnIsBusyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( BusyIndicator )d ).OnIsBusyChanged( e );
        }



        /// <summary>
        /// </summary>
        protected virtual void OnIsBusyChanged( DependencyPropertyChangedEventArgs e )
        {
            this.IsContentVisible = this.IsBusy;
            this.ChangeVisualState( true );
        }

        /// <summary>Content which is used when is busy.</summary>
        public object BusyContent
        {
            get
            {
                return this.GetValue( BusyIndicator.BusyContentProperty );
            }
            set
            {
                this.SetValue( BusyIndicator.BusyContentProperty, value );
            }
        }

        /// <summary>Content template which is used when is busy.</summary>
        public DataTemplate BusyContentTemplate
        {
            get
            {
                return ( DataTemplate )this.GetValue( BusyIndicator.BusyContentTemplateProperty );
            }
            set
            {
                this.SetValue( BusyIndicator.BusyContentTemplateProperty, ( object )value );
            }
        }

        /// <summary>
        /// </summary>
        public Style OverlayStyle
        {
            get
            {
                return ( Style )this.GetValue( BusyIndicator.OverlayStyleProperty );
            }
            set
            {
                this.SetValue( BusyIndicator.OverlayStyleProperty, ( object )value );
            }
        }

        /// <summary>
        /// </summary>
        protected bool IsContentVisible
        {
            get
            {
                return this._isContentVisible;
            }
            set
            {
                this._isContentVisible = value;
            }
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ChangeVisualState( false );
        }

        /// <summary>
        /// </summary>
        protected new virtual void ChangeVisualState( bool useTransitions )
        {
            VisualStateManager.GoToState( ( FrameworkElement )this, this.IsBusy ? "BUSY" : "NOTBUSY", useTransitions );
            VisualStateManager.GoToState( ( FrameworkElement )this, this.IsContentVisible ? "VISIBLE" : "NOTVISIBLE", useTransitions );
        }
    }
}
