using Ecng.Collections;
using fx.Charting;
using System;
using System.Linq;

internal sealed class AnnotationVM : UIHigherVM< AnnotationUI >
{
    public AnnotationVM( AnnotationUI annotation ) : base( annotation )
    {
    }

    protected override void UpdateUi( )
    {
    }

    protected override void Clear( )
    {
        ScichartSurfaceMVVM.RemoveAnnotation( ChartElement );
    }

    public override bool Draw( IEnumerableEx< ChartDrawDataEx.IDrawValue > e )
    {
        PerformUIAction2( () => ScichartSurfaceMVVM.SetupAnnotation( ChartElement, e.Cast<ChartDrawDataEx.sAnnotation>( ).Single( ) ), true );

        return true;
    }    
}
