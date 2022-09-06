using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Core.Extensions;
using SciChart.Core.Framework;
using SciChart.Core.Utility.Mouse;
using fx.Charting.HewFibonacci;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace fx.Charting
{
    public class fxTouchScreenModifier : RelativeZoomModifierBase
    {
        private const int int_0 = 2;

        public fxTouchScreenModifier( )
        {
            GrowFactor = 1.0;
        }

        public override void OnModifierTouchManipulationDelta( ModifierManipulationDeltaArgs e )
        {
            base.OnModifierTouchManipulationDelta( e );

            if ( e.Manipulators.Count( ) == 2 )
            {
                var origin = e.ManipulationOrigin;
                var scale  = e.DeltaManipulation.Scale;
                
                var xValue = 1.0 - scale.X;
                var yValue = 1.0 - scale.Y;

                PerformZoom( origin, xValue, yValue );
                e.Handled = true;
            }
            else
            {
                e.Cancel( );
            }
        }
    }
}
