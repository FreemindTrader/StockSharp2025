using System.Linq;
using Ecng.Collections;
using StockSharp.Xaml.Charting;

/// < summary >
///
/// The view model to draw the ChartAnnotation on the chart.
/// 
/// </summary>
internal sealed class ChartAnnotationViewModel : ChartCompentWpfBaseViewModel<ChartAnnotation>
{
    public ChartAnnotationViewModel(ChartAnnotation annotation) : base(annotation)
    {
    }

    protected override void UpdateUi()
    {
    }

    protected override void Clear()
    {
        DrawingSurface.RemoveAnnotation(ChartComponentView, null);
    }

    public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> e)
    {
        PerformUIAction2(() => DrawingSurface.AnnotationModifier.Draw(ChartComponentView, e.Cast<ChartDrawData.AnnotationData>().Single()), true);

        return true;
    }
}



//using Ecng.Collections;
//using StockSharp.Xaml.Charting;
//using System;
//using System.Collections;
//using System.Linq;

//#nullable disable
//internal sealed class ChartAnnotationViewModel(
//  ChartAnnotation _param1 ) :
//  ChartCompentWpfBaseViewModel<ChartAnnotation>( _param1 )
//{
//    protected override void UpdateUi()
//    {
//    }

//    protected override void Clear()
//    {
//        DrawingSurface.AnnotationModifier.GuiUpdateAndClear( ChartComponentView );
//    }

//    public override bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> _param1 )
//    {
//        PerformUIAction2( new Action( new ChartAnnotationViewModel.SomeSealClass03823()
//        {
//            _ChartAnnotationViewModel = this,
//            _IDrawValue = _param1
//        }.SomeInternalMethod ), true );
//        return true;
//    }

//    private sealed class SomeSealClass03823
//    {
//        public ChartAnnotationViewModel _ChartAnnotationViewModel;
//        public IEnumerableEx<ChartDrawData.IDrawValue> _IDrawValue;

//        internal void SomeInternalMethod()
//        {
//            _ChartAnnotationViewModel.DrawingSurface.AnnotationModifier.Draw( _ChartAnnotationViewModel.ChartComponentView, ( ( IEnumerable ) _IDrawValue ).Cast<ChartDrawData.AnnotationData>().Single<ChartDrawData.AnnotationData>() );
//        }
//    }
//}

//using Ecng.Collections;
//using StockSharp.Xaml.Charting;
//using System;
//using System.Linq;



