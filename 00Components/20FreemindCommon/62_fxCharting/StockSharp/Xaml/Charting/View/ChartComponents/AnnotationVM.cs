using Ecng.Collections;
using StockSharp.Xaml.Charting;
using System;
using System.Linq;

internal sealed class ChartAnnotationVM : UIHigherVM< ChartAnnotation >
{
    public ChartAnnotationVM( ChartAnnotation annotation ) : base( annotation )
    {
    }

    protected override void UpdateUi( )
    {
    }

    protected override void Clear( )
    {
        ScichartSurfaceMVVM.RemoveAnnotation( ChartElement );
    }

    public override bool Draw( IEnumerableEx< ChartDrawData.IDrawValue > e )
    {
        PerformUIAction2( () => ScichartSurfaceMVVM.SetupAnnotation( ChartElement, e.Cast<ChartDrawData.sAnnotation>( ).Single( ) ), true );

        return true;
    }    
}
