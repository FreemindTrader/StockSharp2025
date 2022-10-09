using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Model.ChartData;
using System;
using System.Windows;
using System.Windows.Controls;

namespace fx.Charting
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

        //protected override void HandleMasterMouseEvent( Point mousePoint )
        //{
        //    Point wheretoShow;

        //    switch ( this.SnappingMode )
        //    {
        //        case CursorSnappingMode.CrosshairToSeries:
        //        {

        //        }
        //        break;
                
        //        case CursorSnappingMode.CrosshairToSeriesX:
        //        {

        //        }
        //        break;
                
        //        case CursorSnappingMode.TooltipToSeries:
        //        {

        //        }
        //        break;

        //        case CursorSnappingMode.TooltipToCrosshair:
        //        {

        //        }
        //        break;
        //    }

        //    if ( this.SnappingMode != CursorSnappingMode.TooltipToCrosshair )
        //    {

        //    }
        //    this.ShowCrosshairCursor( mousePoint, true, true );

        //    var snappingPoint = this.GetSnappingPoint( mousePoint );

        //    if ( this.ShowTooltip || this.ShowAxisLabels )
        //    {
        //        this.GetSeriesData( mousePoint );
        //        this.GetAxesData( mousePoint );
        //    }
        //    if ( this.ShowTooltip )
        //    {
        //        if ( this.SeriesData.SeriesInfo.IsNullOrEmpty<SeriesInfo>() || !this.HasToShowTooltip() )
        //            this.ClearCursorOverlay();
        //        else if ( this.ShowTooltipOn == ShowTooltipOptions.MouseHover )
        //        {
        //            this.ClearCursorOverlay();
        //            this._delayActionHelper.Start( ( Action )( () => this.PlaceTooltip( mousePoint ) ) );
        //        }
        //        else
        //            this.PlaceTooltip( mousePoint );
        //    }
        //    if ( !this.ShowAxisLabels )
        //        return;
        //    this.UpdateAxesLabels( mousePoint );
        //}

        //private void GetSeriesData( Point currentPoint )
        //{
        //    this.SeriesData.UpdateSeriesInfo( this.GetSeriesInfoAt( currentPoint ) );
        //}

        //protected override void HandleMasterMouseEvent( Point mousePoint )
        //{
        //    Point hitTestPoint;

        //    object tooltipDataContext = this.GetTooltipDataContext( mousePoint, out hitTestPoint );

        //    if ( tooltipDataContext != null && this._tooltipLabelCache != null )
        //    {
        //        this._tooltipLabelCache.DataContext = tooltipDataContext;
        //        this.UpdateTooltipOverlay( mousePoint );
        //    }
        //    else
        //        this.ClearTooltipOverlay();
        //}

        //private void UpdateTooltipOverlay( Point mousePoint )
        //{
        //    if ( this._tooltipLabelCache == null )
        //        return;
        //    this.PlaceOverlay( ( FrameworkElement )this._tooltipLabelCache, mousePoint );
        //    if ( this.ModifierSurface.Children.Contains( ( UIElement )this._tooltipLabelCache ) )
        //        return;
        //    this.ModifierSurface.Children.Add( ( UIElement )this._tooltipLabelCache );
        //}


        //private void ClearTooltipOverlay()
        //{
        //    if ( this._tooltipLabelCache == null || this.ModifierSurface == null )
        //        return;
        //    if ( this.ModifierSurface.Children.Contains( ( UIElement )this._tooltipLabelCache ) )
        //        this.ModifierSurface.Children.Remove( ( UIElement )this._tooltipLabelCache );
        //    this.CurrentPoint = new Point( double.NaN, double.NaN );
        //}

        //private object GetTooltipDataContext( Point currentPoint, out Point hitTestPoint )
        //{
        //    hitTestPoint = new Point();
        //    object obj = ( object )null;

        //    foreach ( SeriesInfo si in this.GetSeriesInfoAt( currentPoint ) )
        //    {
        //        if ( si.IsHit && TooltipModifier.GetIncludeSeries( ( DependencyObject )si.RenderableSeries ) )
        //        {
        //            hitTestPoint = si.XyCoordinate;
        //            obj = this.TooltipLabelDataContextSelector != null ? this.TooltipLabelDataContextSelector( si ) : si;
        //            break;
        //        }
        //    }
        //    return obj;
        //}


    }
}
