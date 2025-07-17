// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.DoubleToColorMappingSettings
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows.Media;

namespace Ecng.Xaml.Charting.Model.DataSeries
{
    internal class DoubleToColorMappingSettings
    {
        public GradientStop[] GradientStops;
        public double Minimum;
        public double ScaleFactor;
        public int[] CachedMap;

        public override bool Equals( object obj )
        {
            bool flag = false;
            DoubleToColorMappingSettings colorMappingSettings = obj as DoubleToColorMappingSettings;
            if ( colorMappingSettings != null )
                flag = colorMappingSettings.Minimum.Equals( this.Minimum ) && colorMappingSettings.ScaleFactor.Equals( this.ScaleFactor ) && colorMappingSettings.GradientStops.Length.Equals( this.GradientStops.Length ) && colorMappingSettings.GradientStops.Equals( ( object ) this.GradientStops );
            return flag;
        }
    }
}
