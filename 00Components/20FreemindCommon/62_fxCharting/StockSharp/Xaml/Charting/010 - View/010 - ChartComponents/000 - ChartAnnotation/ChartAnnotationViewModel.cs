//using Ecng.Collections;
//using StockSharp.Xaml.Charting;
//using System;
//using System.Collections;
//using System.Linq;
//namespace StockSharp.Xaml.Charting;
//#nullable disable
//internal sealed class ChartAnnotationViewModel( ChartAnnotation annotation ) : ChartCompentWpfBaseViewModel<ChartAnnotation>( annotation )
//{
//    protected override void UpdateUi()
//    {
//    }

//    protected override void Clear()
//    {
//        this.DrawingSurface.AnnotationModifier.GuiUpdateAndClear( this.ChartComponentView );
//    }

//    public override bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> _param1 )
//    {
//        this.\u0023\u003DzY_lPK_VP\u0024B7_( new Action( new ChartAnnotationViewModel.SomeShit333()
//        {
//            _variableSome3535 = this,
//            _drawValue0384 = _param1
//        }.SomeMethod0333 ), true );
//        return true;
//    }

//    private sealed class SomeShit333
//    {
//        public ChartAnnotationViewModel _variableSome3535;
//        public IEnumerableEx<ChartDrawData.IDrawValue> _drawValue0384;

//        internal void SomeMethod0333()
//        {
//            this._variableSome3535.ScichartSurfaceMVVM.AnnotationModifier.Draw( this._variableSome3535.ChartComponentView, ( ( IEnumerable ) this._drawValue0384 ).Cast<ChartDrawData.AnnotationData>().Single<ChartDrawData.AnnotationData>() );
//        }
//    }
//}


////using Ecng.Collections;
////using StockSharp.Xaml.Charting;
////using System;
////using System.Linq;



using System.Linq;
using Ecng.Collections;
using StockSharp.Xaml.Charting;

/// < summary >
///
/// NOT DONE YET
/// 
/// </summary>
internal sealed class ChartAnnotationViewModel : ChartCompentWpfBaseViewModel<ChartAnnotation>
{
    public ChartAnnotationViewModel( ChartAnnotation annotation ) : base( annotation )
    {
    }

    protected override void UpdateUi()
    {
    }

    protected override void Clear()
    {
        DrawingSurface.RemoveAnnotation( ChartComponentView );
    }

    public override bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> e )
    {
        PerformUIAction2( () => DrawingSurface.SetupAnnotation( ChartComponentView, e.Cast<ChartDrawData.AnnotationData>().Single() ), true );

        return true;
    }
}
