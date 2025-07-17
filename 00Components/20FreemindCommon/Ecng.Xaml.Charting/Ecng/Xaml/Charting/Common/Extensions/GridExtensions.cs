// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.GridExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
namespace Ecng.Xaml.Charting
{
    internal static class GridExtensions
    {
        internal static void SafeAddChild( this Panel panel, object child, int index = -1 )
        {
            FrameworkElement frameworkElement = child as FrameworkElement;
            if ( panel == null || frameworkElement == null || ( panel == frameworkElement || panel.Children.Contains( ( UIElement ) frameworkElement ) ) )
                return;
            Panel parent = frameworkElement.Parent as Panel;
            if ( parent != null && parent.Children.Contains( ( UIElement ) frameworkElement ) )
                parent.Children.Remove( ( UIElement ) frameworkElement );
            if ( index >= 0 && index < panel.Children.Count )
                panel.Children.Insert( index, ( UIElement ) frameworkElement );
            else
                panel.Children.Add( ( UIElement ) frameworkElement );
        }

        internal static void SafeAddChild( this IMainGrid panel, object child, int index = -1 )
        {
            ( panel as Panel ).SafeAddChild( child, index );
        }

        internal static void SafeAddChild( this IAnnotationCanvas panel, object child, int index = -1 )
        {
            ( panel as Panel ).SafeAddChild( child, index );
        }

        internal static void SafeRemoveChild( this Panel panel, object child )
        {
            if ( panel == null )
                return;
            UIElement element = child as UIElement;
            if ( element == null || !panel.Children.Contains( element ) )
                return;
            panel.Children.Remove( element );
        }

        internal static void SafeRemoveChild( this IMainGrid panel, object child )
        {
            ( panel as Panel ).SafeRemoveChild( child );
        }

        internal static void SafeRemoveChild( this IAnnotationCanvas panel, object child )
        {
            ( panel as Panel ).SafeRemoveChild( child );
        }

        internal static void DrawLine( this Panel panel, int x1, int y1, int x2, int y2, Brush stroke, double strokeThickness )
        {
            Line line1 = new Line();
            line1.Stroke = stroke;
            line1.StrokeThickness = strokeThickness;
            line1.X1 = ( double ) x1;
            line1.X2 = ( double ) x2;
            line1.Y1 = ( double ) y1;
            line1.Y2 = ( double ) y2;
            Line line2 = line1;
            panel.Children.Add( ( UIElement ) line2 );
        }
    }
}
