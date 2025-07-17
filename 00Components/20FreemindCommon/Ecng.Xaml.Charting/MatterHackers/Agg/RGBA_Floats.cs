// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.RGBA_Floats
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal struct RGBA_Floats : IColorType
    {
        public static readonly RGBA_Floats White = new RGBA_Floats(1f, 1f, 1f, 1f);
        public static readonly RGBA_Floats Black = new RGBA_Floats(0.0f, 0.0f, 0.0f, 1f);
        public static readonly RGBA_Floats Red = new RGBA_Floats(1f, 0.0f, 0.0f, 1f);
        public static readonly RGBA_Floats Green = new RGBA_Floats(0.0f, 1f, 0.0f, 1f);
        public static readonly RGBA_Floats Blue = new RGBA_Floats(0.0f, 0.0f, 1f, 1f);
        public static readonly RGBA_Floats Cyan = new RGBA_Floats(0.0f, 1f, 1f, 1f);
        public static readonly RGBA_Floats Magenta = new RGBA_Floats(1f, 0.0f, 1f, 1f);
        public static readonly RGBA_Floats Yellow = new RGBA_Floats(1f, 1f, 0.0f, 1f);
        private const int base_shift = 8;
        private const int base_scale = 256;
        private const int base_mask = 255;
        public float red;
        public float green;
        public float blue;
        public float alpha;

        public int Red0To255
        {
            get
            {
                return agg_basics.uround( ( double ) this.red * ( double ) byte.MaxValue );
            }
            set
            {
                this.red = ( float ) value / ( float ) byte.MaxValue;
            }
        }

        public int Green0To255
        {
            get
            {
                return agg_basics.uround( ( double ) this.green * ( double ) byte.MaxValue );
            }
            set
            {
                this.green = ( float ) value / ( float ) byte.MaxValue;
            }
        }

        public int Blue0To255
        {
            get
            {
                return agg_basics.uround( ( double ) this.blue * ( double ) byte.MaxValue );
            }
            set
            {
                this.blue = ( float ) value / ( float ) byte.MaxValue;
            }
        }

        public int Alpha0To255
        {
            get
            {
                return agg_basics.uround( ( double ) this.alpha * ( double ) byte.MaxValue );
            }
            set
            {
                this.alpha = ( float ) value / ( float ) byte.MaxValue;
            }
        }

        public RGBA_Floats( double r_, double g_, double b_ )
        {
            this = new RGBA_Floats( r_, g_, b_, 1.0 );
        }

        public RGBA_Floats( double r_, double g_, double b_, double a_ )
        {
            this.red = ( float ) r_;
            this.green = ( float ) g_;
            this.blue = ( float ) b_;
            this.alpha = ( float ) a_;
        }

        public RGBA_Floats( float r_, float g_, float b_ )
        {
            this = new RGBA_Floats( r_, g_, b_, 1f );
        }

        public RGBA_Floats( float r_, float g_, float b_, float a_ )
        {
            this.red = r_;
            this.green = g_;
            this.blue = b_;
            this.alpha = a_;
        }

        public RGBA_Floats( RGBA_Floats c )
        {
            this = new RGBA_Floats( c, c.alpha );
        }

        public RGBA_Floats( RGBA_Floats c, float a_ )
        {
            this.red = c.red;
            this.green = c.green;
            this.blue = c.blue;
            this.alpha = a_;
        }

        public RGBA_Floats( float wavelen )
        {
            this = new RGBA_Floats( wavelen, 1f );
        }

        public RGBA_Floats( float wavelen, float gamma )
        {
            this = RGBA_Floats.from_wavelength( wavelen, gamma );
        }

        public static bool operator ==( RGBA_Floats a, RGBA_Floats b )
        {
            return ( double ) a.red == ( double ) b.red && ( double ) a.green == ( double ) b.green && ( ( double ) a.blue == ( double ) b.blue && ( double ) a.alpha == ( double ) b.alpha );
        }

        public static bool operator !=( RGBA_Floats a, RGBA_Floats b )
        {
            return ( double ) a.red != ( double ) b.red || ( double ) a.green != ( double ) b.green || ( ( double ) a.blue != ( double ) b.blue || ( double ) a.alpha != ( double ) b.alpha );
        }

        public override bool Equals( object obj )
        {
            if ( obj.GetType() == typeof( RGBA_Floats ) )
                return this == ( RGBA_Floats ) obj;
            return false;
        }

        public override int GetHashCode()
        {
            return new { blue = this.blue, green = this.green, red = this.red, alpha = this.alpha }.GetHashCode();
        }

        public RGBA_Bytes GetAsRGBA_Bytes()
        {
            return new RGBA_Bytes( this.Red0To255, this.Green0To255, this.Blue0To255, this.Alpha0To255 );
        }

        public RGBA_Floats GetAsRGBA_Floats()
        {
            return this;
        }

        public static RGBA_Floats operator +( RGBA_Floats A, RGBA_Floats B )
        {
            return new RGBA_Floats() { red = A.red + B.red, green = A.green + B.green, blue = A.blue + B.blue, alpha = A.alpha + B.alpha };
        }

        public static RGBA_Floats operator -( RGBA_Floats A, RGBA_Floats B )
        {
            return new RGBA_Floats() { red = A.red - B.red, green = A.green - B.green, blue = A.blue - B.blue, alpha = A.alpha - B.alpha };
        }

        public static RGBA_Floats operator *( RGBA_Floats A, RGBA_Floats B )
        {
            return new RGBA_Floats() { red = A.red * B.red, green = A.green * B.green, blue = A.blue * B.blue, alpha = A.alpha * B.alpha };
        }

        public static RGBA_Floats operator /( RGBA_Floats A, RGBA_Floats B )
        {
            return new RGBA_Floats() { red = A.red / B.red, green = A.green / B.green, blue = A.blue / B.blue, alpha = A.alpha / B.alpha };
        }

        public static RGBA_Floats operator /( RGBA_Floats A, float B )
        {
            return new RGBA_Floats() { red = A.red / B, green = A.green / B, blue = A.blue / B, alpha = A.alpha / B };
        }

        public static RGBA_Floats operator /( RGBA_Floats A, double doubleB )
        {
            float num = (float) doubleB;
            return new RGBA_Floats() { red = A.red / num, green = A.green / num, blue = A.blue / num, alpha = A.alpha / num };
        }

        public static RGBA_Floats operator *( RGBA_Floats A, float B )
        {
            return new RGBA_Floats() { red = A.red * B, green = A.green * B, blue = A.blue * B, alpha = A.alpha * B };
        }

        public static RGBA_Floats operator *( RGBA_Floats A, double doubleB )
        {
            float num = (float) doubleB;
            return new RGBA_Floats() { red = A.red * num, green = A.green * num, blue = A.blue * num, alpha = A.alpha * num };
        }

        public void clear()
        {
            this.red = this.green = this.blue = this.alpha = 0.0f;
        }

        public RGBA_Floats transparent()
        {
            this.alpha = 0.0f;
            return this;
        }

        public RGBA_Floats opacity( float a_ )
        {
            if ( ( double ) a_ < 0.0 )
                a_ = 0.0f;
            if ( ( double ) a_ > 1.0 )
                a_ = 1f;
            this.alpha = a_;
            return this;
        }

        public float opacity()
        {
            return this.alpha;
        }

        public RGBA_Floats premultiply()
        {
            this.red *= this.alpha;
            this.green *= this.alpha;
            this.blue *= this.alpha;
            return this;
        }

        public RGBA_Floats premultiply( float a_ )
        {
            if ( ( double ) this.alpha <= 0.0 || ( double ) a_ <= 0.0 )
            {
                this.red = this.green = this.blue = this.alpha = 0.0f;
                return this;
            }
            a_ /= this.alpha;
            this.red *= a_;
            this.green *= a_;
            this.blue *= a_;
            this.alpha = a_;
            return this;
        }

        public RGBA_Floats demultiply()
        {
            if ( ( double ) this.alpha == 0.0 )
            {
                this.red = this.green = this.blue = 0.0f;
                return this;
            }
            float num = 1f / this.alpha;
            this.red *= num;
            this.green *= num;
            this.blue *= num;
            return this;
        }

        public RGBA_Bytes gradient( RGBA_Bytes c_8, double k )
        {
            RGBA_Floats asRgbaFloats = c_8.GetAsRGBA_Floats();
            RGBA_Floats rgbaFloats;
            rgbaFloats.red = this.red + ( float ) ( ( ( double ) asRgbaFloats.red - ( double ) this.red ) * k );
            rgbaFloats.green = this.green + ( float ) ( ( ( double ) asRgbaFloats.green - ( double ) this.green ) * k );
            rgbaFloats.blue = this.blue + ( float ) ( ( ( double ) asRgbaFloats.blue - ( double ) this.blue ) * k );
            rgbaFloats.alpha = this.alpha + ( float ) ( ( ( double ) asRgbaFloats.alpha - ( double ) this.alpha ) * k );
            return rgbaFloats.GetAsRGBA_Bytes();
        }

        public static IColorType no_color()
        {
            return ( IColorType ) new RGBA_Floats( 0.0f, 0.0f, 0.0f, 0.0f );
        }

        public static RGBA_Floats from_wavelength( float wl )
        {
            return RGBA_Floats.from_wavelength( wl, 1f );
        }

        public static RGBA_Floats from_wavelength( float wl, float gamma )
        {
            RGBA_Floats rgbaFloats = new RGBA_Floats(0.0f, 0.0f, 0.0f);
            if ( ( double ) wl >= 380.0 && ( double ) wl <= 440.0 )
            {
                rgbaFloats.red = ( float ) ( -1.0 * ( ( double ) wl - 440.0 ) / 60.0 );
                rgbaFloats.blue = 1f;
            }
            else if ( ( double ) wl >= 440.0 && ( double ) wl <= 490.0 )
            {
                rgbaFloats.green = ( float ) ( ( ( double ) wl - 440.0 ) / 50.0 );
                rgbaFloats.blue = 1f;
            }
            else if ( ( double ) wl >= 490.0 && ( double ) wl <= 510.0 )
            {
                rgbaFloats.green = 1f;
                rgbaFloats.blue = ( float ) ( -1.0 * ( ( double ) wl - 510.0 ) / 20.0 );
            }
            else if ( ( double ) wl >= 510.0 && ( double ) wl <= 580.0 )
            {
                rgbaFloats.red = ( float ) ( ( ( double ) wl - 510.0 ) / 70.0 );
                rgbaFloats.green = 1f;
            }
            else if ( ( double ) wl >= 580.0 && ( double ) wl <= 645.0 )
            {
                rgbaFloats.red = 1f;
                rgbaFloats.green = ( float ) ( -1.0 * ( ( double ) wl - 645.0 ) / 65.0 );
            }
            else if ( ( double ) wl >= 645.0 && ( double ) wl <= 780.0 )
                rgbaFloats.red = 1f;
            float num = 1f;
            if ( ( double ) wl > 700.0 )
                num = ( float ) ( 0.3 + 0.7 * ( 780.0 - ( double ) wl ) / 80.0 );
            else if ( ( double ) wl < 420.0 )
                num = ( float ) ( 0.3 + 0.7 * ( ( double ) wl - 380.0 ) / 40.0 );
            rgbaFloats.red = ( float ) Math.Pow( ( double ) rgbaFloats.red * ( double ) num, ( double ) gamma );
            rgbaFloats.green = ( float ) Math.Pow( ( double ) rgbaFloats.green * ( double ) num, ( double ) gamma );
            rgbaFloats.blue = ( float ) Math.Pow( ( double ) rgbaFloats.blue * ( double ) num, ( double ) gamma );
            return rgbaFloats;
        }

        public static RGBA_Floats rgba_pre( double r, double g, double b )
        {
            return RGBA_Floats.rgba_pre( ( float ) r, ( float ) g, ( float ) b, 1f );
        }

        public static RGBA_Floats rgba_pre( float r, float g, float b )
        {
            return RGBA_Floats.rgba_pre( r, g, b, 1f );
        }

        public static RGBA_Floats rgba_pre( float r, float g, float b, float a )
        {
            return new RGBA_Floats( r, g, b, a ).premultiply();
        }

        public static RGBA_Floats rgba_pre( double r, double g, double b, double a )
        {
            return new RGBA_Floats( ( float ) r, ( float ) g, ( float ) b, ( float ) a ).premultiply();
        }

        public static RGBA_Floats rgba_pre( RGBA_Floats c )
        {
            return new RGBA_Floats( c ).premultiply();
        }

        public static RGBA_Floats rgba_pre( RGBA_Floats c, float a )
        {
            return new RGBA_Floats( c, a ).premultiply();
        }

        public static RGBA_Floats GetTweenColor( RGBA_Floats Color1, RGBA_Floats Color2, double RatioOf2 )
        {
            if ( RatioOf2 <= 0.0 )
                return new RGBA_Floats( Color1 );
            if ( RatioOf2 >= 1.0 )
                return new RGBA_Floats( Color2 );
            double num = 1.0 - RatioOf2;
            return new RGBA_Floats( ( double ) Color1.red * num + ( double ) Color2.red * RatioOf2, ( double ) Color1.green * num + ( double ) Color2.green * RatioOf2, ( double ) Color1.blue * num + ( double ) Color2.blue * RatioOf2 );
        }

        public RGBA_Floats Blend( RGBA_Floats other, double weight )
        {
            RGBA_Floats rgbaFloats = new RGBA_Floats(this);
            return this * ( 1.0 - weight ) + other * weight;
        }

        public double SumOfDistances( RGBA_Floats other )
        {
            return ( double ) Math.Abs( this.red - other.red ) + ( double ) Math.Abs( this.green - other.green ) + ( double ) Math.Abs( this.blue - other.blue );
        }

        private void Clamp0To1( ref float value )
        {
            if ( ( double ) value < 0.0 )
            {
                value = 0.0f;
            }
            else
            {
                if ( ( double ) value <= 1.0 )
                    return;
                value = 1f;
            }
        }

        public void Clamp0To1()
        {
            this.Clamp0To1( ref this.red );
            this.Clamp0To1( ref this.green );
            this.Clamp0To1( ref this.blue );
            this.Clamp0To1( ref this.alpha );
        }
    }
}
