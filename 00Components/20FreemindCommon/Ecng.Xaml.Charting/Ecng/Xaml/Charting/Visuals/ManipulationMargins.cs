// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.ManipulationMargins
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace StockSharp.Xaml.Charting.Visuals
{
    public static class ManipulationMargins
    {
        private static double _annotationResizingThumbSize = 20.0;
        private static double _annotationLineWidth = 11.0;

        public static double AnnotationResizingThumbSize
        {
            get
            {
                return ManipulationMargins._annotationResizingThumbSize;
            }
            set
            {
                ManipulationMargins._annotationResizingThumbSize = value;
            }
        }

        public static double AnnotationLineWidth
        {
            get
            {
                return ManipulationMargins._annotationLineWidth;
            }
            set
            {
                ManipulationMargins._annotationLineWidth = value;
            }
        }
    }
}
