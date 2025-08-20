using SciChart.Charting.ChartModifiers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Xaml.Charting
{
    public partial class UltrachartCursormodifier : CursorModifier
    {
        public static readonly DependencyProperty InPlaceTooltipProperty = DependencyProperty.Register( nameof( InPlaceTooltip ), typeof( bool ), typeof( UltrachartCursormodifier ), new PropertyMetadata( true, new PropertyChangedCallback( OnUltrachartCursormodifierChanged ) ) );
        //private readonly ControlTemplate _tooltipTemplateBackup;
        

        public UltrachartCursormodifier( )
        {
            InitializeComponent( );
            //_tooltipTemplateBackup = TooltipLabelTemplate;
            UseInterpolation = false;
        }

        private static void OnUltrachartCursormodifierChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( UltrachartCursormodifier )d ).UpdateTooltip( ( bool )e.NewValue );
        }

        public bool InPlaceTooltip
        {
            get
            {
                return ( bool )GetValue( InPlaceTooltipProperty );
            }
            set
            {
                SetValue( InPlaceTooltipProperty, value );
            }
        }

        private void UpdateTooltip( bool shouldSet )
        {
            //TooltipLabelTemplate = shouldSet ? _tooltipTemplateBackup : null;
        }


    }
}
