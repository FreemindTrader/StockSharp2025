using Ecng.Collections;
using StockSharp.Xaml.Charting;
using System;
using System.Linq;


/// <summary>
/// 
/// NOT DONE YET
/// 
/// </summary>

internal sealed class ChartAnnotationViewModel : ChartCompentWpfBaseViewModel< ChartAnnotation >
{
    public ChartAnnotationViewModel( ChartAnnotation annotation ) : base( annotation )
    {
    }

    protected override void UpdateUi( )
    {
    }

    protected override void Clear( )
    {
        DrawingSurface.RemoveAnnotation( ChartComponentView );
    }

    public override bool Draw( IEnumerableEx< ChartDrawData.IDrawValue > e )
    {
        PerformUIAction2( () => DrawingSurface.SetupAnnotation( ChartComponentView, e.Cast<ChartDrawData.sAnnotation>( ).Single( ) ), true );

        return true;
    }    
}
