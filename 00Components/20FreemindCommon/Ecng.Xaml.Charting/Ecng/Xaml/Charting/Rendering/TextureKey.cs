// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.TextureKey
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;
using System.Windows.Media;

namespace Ecng.Xaml.Charting.Rendering
{
    internal class TextureKey
    {
        private readonly Brush _brush;
        private readonly FrameworkElement _frameworkElement;
        private readonly Size _size;

        public TextureKey( Size size, Brush brush )
        {
            this._size = size;
            this._brush = brush;
        }

        public TextureKey( FrameworkElement frameworkElement )
        {
            this._frameworkElement = frameworkElement;
        }

        public override int GetHashCode()
        {
            if ( this._frameworkElement != null )
                return this._frameworkElement.GetHashCode();
            double num1 = this._size.Width;
            int hashCode1 = num1.GetHashCode();
            num1 = this._size.Height;
            int hashCode2 = num1.GetHashCode();
            int num2 = hashCode1 ^ hashCode2;
            int num3;
            if ( this._brush is LinearGradientBrush )
            {
                foreach ( GradientStop gradientStop in ( ( GradientBrush ) this._brush ).GradientStops )
                {
                    num2 ^= gradientStop.Color.GetHashCode();
                    int num4 = num2;
                    num1 = gradientStop.Offset;
                    int hashCode3 = num1.GetHashCode();
                    num2 = num4 ^ hashCode3;
                }
                int num5 = num2;
                Point point = ((LinearGradientBrush) this._brush).StartPoint;
                int hashCode4 = point.GetHashCode();
                int num6 = num5 ^ hashCode4;
                point = ( ( LinearGradientBrush ) this._brush ).EndPoint;
                int hashCode5 = point.GetHashCode();
                num3 = num6 ^ hashCode5;
            }
            else
                num3 = num2 ^ this._brush.GetHashCode();
            return num3;
        }

        public override bool Equals( object obj )
        {
            TextureKey textureKey = obj as TextureKey;
            if ( textureKey == null )
                return false;
            if ( this._frameworkElement != null )
                return this._frameworkElement.Equals( ( object ) textureKey._frameworkElement );
            if ( textureKey._size.Width != this._size.Width || textureKey._size.Height != this._size.Height )
                return false;
            if ( !( this._brush is LinearGradientBrush ) )
                return this._brush.Equals( ( object ) textureKey._brush );
            if ( !( textureKey._brush is LinearGradientBrush ) || ( ( LinearGradientBrush ) this._brush ).StartPoint != ( ( LinearGradientBrush ) textureKey._brush ).StartPoint || ( ( LinearGradientBrush ) this._brush ).EndPoint != ( ( LinearGradientBrush ) textureKey._brush ).EndPoint )
                return false;
            GradientStopCollection gradientStops1 = ((GradientBrush) this._brush).GradientStops;
            GradientStopCollection gradientStops2 = ((GradientBrush) textureKey._brush).GradientStops;
            if ( gradientStops1.Count != gradientStops2.Count )
                return false;
            for ( int index = 0 ; index < gradientStops1.Count ; ++index )
            {
                GradientStop gradientStop1 = gradientStops1[index];
                GradientStop gradientStop2 = gradientStops2[index];
                if ( gradientStop1.Color != gradientStop2.Color || gradientStop1.Offset != gradientStop2.Offset )
                    return false;
            }
            return true;
        }
    }
}
