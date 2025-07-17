// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.RGBA_Bytes
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal struct RGBA_Bytes : IColorType
    {
        public static readonly RGBA_Bytes White = new RGBA_Bytes((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        public static readonly RGBA_Bytes LightGray = new RGBA_Bytes(225, 225, 225, (int) byte.MaxValue);
        public static readonly RGBA_Bytes DarkGray = new RGBA_Bytes(125, 125, 125, (int) byte.MaxValue);
        public static readonly RGBA_Bytes Black = new RGBA_Bytes(0, 0, 0, (int) byte.MaxValue);
        public static readonly RGBA_Bytes Red = new RGBA_Bytes((int) byte.MaxValue, 0, 0, (int) byte.MaxValue);
        public static readonly RGBA_Bytes Orange = new RGBA_Bytes((int) byte.MaxValue, (int) sbyte.MaxValue, 0, (int) byte.MaxValue);
        public static readonly RGBA_Bytes Green = new RGBA_Bytes(0, (int) byte.MaxValue, 0, (int) byte.MaxValue);
        public static readonly RGBA_Bytes Blue = new RGBA_Bytes(0, 0, (int) byte.MaxValue, (int) byte.MaxValue);
        public static readonly RGBA_Bytes Indigo = new RGBA_Bytes(75, 0, 130, (int) byte.MaxValue);
        public static readonly RGBA_Bytes Violet = new RGBA_Bytes(143, 0, (int) byte.MaxValue, (int) byte.MaxValue);
        public static readonly RGBA_Bytes Cyan = new RGBA_Bytes(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        public static readonly RGBA_Bytes Magenta = new RGBA_Bytes((int) byte.MaxValue, 0, (int) byte.MaxValue, (int) byte.MaxValue);
        public static readonly RGBA_Bytes Yellow = new RGBA_Bytes((int) byte.MaxValue, (int) byte.MaxValue, 0, (int) byte.MaxValue);
        public static readonly RGBA_Bytes YellowGreen = new RGBA_Bytes(154, 205, 50, (int) byte.MaxValue);
        public const int cover_shift = 8;
        public const int cover_size = 256;
        public const int cover_mask = 255;
        public const int base_shift = 8;
        public const int base_scale = 256;
        public const int base_mask = 255;
        public byte blue;
        public byte green;
        public byte red;
        public byte alpha;

        public int Red0To255
        {
            get
            {
                return ( int ) this.red;
            }
            set
            {
                this.red = ( byte ) value;
            }
        }

        public int Green0To255
        {
            get
            {
                return ( int ) this.green;
            }
            set
            {
                this.green = ( byte ) value;
            }
        }

        public int Blue0To255
        {
            get
            {
                return ( int ) this.blue;
            }
            set
            {
                this.blue = ( byte ) value;
            }
        }

        public int Alpha0To255
        {
            get
            {
                return ( int ) this.alpha;
            }
            set
            {
                this.alpha = ( byte ) value;
            }
        }

        public RGBA_Bytes( int r_, int g_, int b_ )
        {
            this = new RGBA_Bytes( r_, g_, b_, ( int ) byte.MaxValue );
        }

        public RGBA_Bytes( int r_, int g_, int b_, int a_ )
        {
            this.red = ( byte ) r_;
            this.green = ( byte ) g_;
            this.blue = ( byte ) b_;
            this.alpha = ( byte ) a_;
        }

        public RGBA_Bytes( double r_, double g_, double b_, double a_ )
        {
            this.red = ( byte ) agg_basics.uround( r_ * ( double ) byte.MaxValue );
            this.green = ( byte ) agg_basics.uround( g_ * ( double ) byte.MaxValue );
            this.blue = ( byte ) agg_basics.uround( b_ * ( double ) byte.MaxValue );
            this.alpha = ( byte ) agg_basics.uround( a_ * ( double ) byte.MaxValue );
        }

        public RGBA_Bytes( double r_, double g_, double b_ )
        {
            this.red = ( byte ) agg_basics.uround( r_ * ( double ) byte.MaxValue );
            this.green = ( byte ) agg_basics.uround( g_ * ( double ) byte.MaxValue );
            this.blue = ( byte ) agg_basics.uround( b_ * ( double ) byte.MaxValue );
            this.alpha = byte.MaxValue;
        }

        public RGBA_Bytes( RGBA_Floats c, double a_ )
        {
            this.red = ( byte ) agg_basics.uround( ( double ) c.red * ( double ) byte.MaxValue );
            this.green = ( byte ) agg_basics.uround( ( double ) c.green * ( double ) byte.MaxValue );
            this.blue = ( byte ) agg_basics.uround( ( double ) c.blue * ( double ) byte.MaxValue );
            this.alpha = ( byte ) agg_basics.uround( a_ * ( double ) byte.MaxValue );
        }

        public RGBA_Bytes( RGBA_Bytes c )
        {
            this = new RGBA_Bytes( c, ( int ) c.alpha );
        }

        public RGBA_Bytes( RGBA_Bytes c, int a_ )
        {
            this.red = c.red;
            this.green = c.green;
            this.blue = c.blue;
            this.alpha = ( byte ) a_;
        }

        public RGBA_Bytes( uint fourByteColor )
        {
            this.red = ( byte ) ( fourByteColor >> 16 & ( uint ) byte.MaxValue );
            this.green = ( byte ) ( fourByteColor >> 8 & ( uint ) byte.MaxValue );
            this.blue = ( byte ) ( fourByteColor & ( uint ) byte.MaxValue );
            this.alpha = ( byte ) ( fourByteColor >> 24 & ( uint ) byte.MaxValue );
        }

        public RGBA_Bytes( RGBA_Floats c )
        {
            this.red = ( byte ) agg_basics.uround( ( double ) c.red * ( double ) byte.MaxValue );
            this.green = ( byte ) agg_basics.uround( ( double ) c.green * ( double ) byte.MaxValue );
            this.blue = ( byte ) agg_basics.uround( ( double ) c.blue * ( double ) byte.MaxValue );
            this.alpha = ( byte ) agg_basics.uround( ( double ) c.alpha * ( double ) byte.MaxValue );
        }

        public static bool operator ==( RGBA_Bytes a, RGBA_Bytes b )
        {
            return ( int ) a.red == ( int ) b.red && ( int ) a.green == ( int ) b.green && ( ( int ) a.blue == ( int ) b.blue && ( int ) a.alpha == ( int ) b.alpha );
        }

        public static bool operator !=( RGBA_Bytes a, RGBA_Bytes b )
        {
            return ( int ) a.red != ( int ) b.red || ( int ) a.green != ( int ) b.green || ( ( int ) a.blue != ( int ) b.blue || ( int ) a.alpha != ( int ) b.alpha );
        }

        public override bool Equals( object obj )
        {
            if ( obj.GetType() == typeof( RGBA_Bytes ) )
                return this == ( RGBA_Bytes ) obj;
            return false;
        }

        public override int GetHashCode()
        {
            return new { blue = this.blue, green = this.green, red = this.red, alpha = this.alpha }.GetHashCode();
        }

        public RGBA_Floats GetAsRGBA_Floats()
        {
            return new RGBA_Floats( ( float ) this.red / ( float ) byte.MaxValue, ( float ) this.green / ( float ) byte.MaxValue, ( float ) this.blue / ( float ) byte.MaxValue, ( float ) this.alpha / ( float ) byte.MaxValue );
        }

        public RGBA_Bytes GetAsRGBA_Bytes()
        {
            return this;
        }

        private void clear()
        {
            this.red = this.green = this.blue = this.alpha = ( byte ) 0;
        }

        public RGBA_Bytes gradient( RGBA_Bytes c, double k )
        {
            RGBA_Bytes rgbaBytes = new RGBA_Bytes();
            int num = agg_basics.uround(k * 256.0);
            rgbaBytes.Red0To255 = ( int ) ( byte ) ( this.Red0To255 + ( ( c.Red0To255 - this.Red0To255 ) * num >> 8 ) );
            rgbaBytes.Green0To255 = ( int ) ( byte ) ( this.Green0To255 + ( ( c.Green0To255 - this.Green0To255 ) * num >> 8 ) );
            rgbaBytes.Blue0To255 = ( int ) ( byte ) ( this.Blue0To255 + ( ( c.Blue0To255 - this.Blue0To255 ) * num >> 8 ) );
            rgbaBytes.Alpha0To255 = ( int ) ( byte ) ( this.Alpha0To255 + ( ( c.Alpha0To255 - this.Alpha0To255 ) * num >> 8 ) );
            return rgbaBytes;
        }

        public static RGBA_Bytes operator +( RGBA_Bytes A, RGBA_Bytes B )
        {
            return new RGBA_Bytes() { red = ( int ) A.red + ( int ) B.red > ( int ) byte.MaxValue ? byte.MaxValue : ( byte ) ( ( int ) A.red + ( int ) B.red ), green = ( int ) A.green + ( int ) B.green > ( int ) byte.MaxValue ? byte.MaxValue : ( byte ) ( ( int ) A.green + ( int ) B.green ), blue = ( int ) A.blue + ( int ) B.blue > ( int ) byte.MaxValue ? byte.MaxValue : ( byte ) ( ( int ) A.blue + ( int ) B.blue ), alpha = ( int ) A.alpha + ( int ) B.alpha > ( int ) byte.MaxValue ? byte.MaxValue : ( byte ) ( ( int ) A.alpha + ( int ) B.alpha ) };
        }

        public static RGBA_Bytes operator -( RGBA_Bytes A, RGBA_Bytes B )
        {
            return new RGBA_Bytes() { red = ( int ) A.red - ( int ) B.red < 0 ? ( byte ) 0 : ( byte ) ( ( int ) A.red - ( int ) B.red ), green = ( int ) A.green - ( int ) B.green < 0 ? ( byte ) 0 : ( byte ) ( ( int ) A.green - ( int ) B.green ), blue = ( int ) A.blue - ( int ) B.blue < 0 ? ( byte ) 0 : ( byte ) ( ( int ) A.blue - ( int ) B.blue ), alpha = byte.MaxValue };
        }

        public static RGBA_Bytes operator *( RGBA_Bytes A, double doubleB )
        {
            float num = (float) doubleB;
            return new RGBA_Bytes( new RGBA_Floats() { red = ( float ) A.red / ( float ) byte.MaxValue * num, green = ( float ) A.green / ( float ) byte.MaxValue * num, blue = ( float ) A.blue / ( float ) byte.MaxValue * num, alpha = ( float ) A.alpha / ( float ) byte.MaxValue * num } );
        }

        public void add( RGBA_Bytes c, int cover )
        {
            if ( cover == ( int ) byte.MaxValue )
            {
                if ( c.Alpha0To255 == ( int ) byte.MaxValue )
                {
                    this = c;
                }
                else
                {
                    int num1 = this.Red0To255 + c.Red0To255;
                    this.Red0To255 = num1 > ( int ) byte.MaxValue ? ( int ) byte.MaxValue : num1;
                    int num2 = this.Green0To255 + c.Green0To255;
                    this.Green0To255 = num2 > ( int ) byte.MaxValue ? ( int ) byte.MaxValue : num2;
                    int num3 = this.Blue0To255 + c.Blue0To255;
                    this.Blue0To255 = num3 > ( int ) byte.MaxValue ? ( int ) byte.MaxValue : num3;
                    int num4 = this.Alpha0To255 + c.Alpha0To255;
                    this.Alpha0To255 = num4 > ( int ) byte.MaxValue ? ( int ) byte.MaxValue : num4;
                }
            }
            else
            {
                int num1 = this.Red0To255 + (c.Red0To255 * cover + (int) sbyte.MaxValue >> 8);
                int num2 = this.Green0To255 + (c.Green0To255 * cover + (int) sbyte.MaxValue >> 8);
                int num3 = this.Blue0To255 + (c.Blue0To255 * cover + (int) sbyte.MaxValue >> 8);
                int num4 = this.Alpha0To255 + (c.Alpha0To255 * cover + (int) sbyte.MaxValue >> 8);
                this.Red0To255 = num1 > ( int ) byte.MaxValue ? ( int ) byte.MaxValue : num1;
                this.Green0To255 = num2 > ( int ) byte.MaxValue ? ( int ) byte.MaxValue : num2;
                this.Blue0To255 = num3 > ( int ) byte.MaxValue ? ( int ) byte.MaxValue : num3;
                this.Alpha0To255 = num4 > ( int ) byte.MaxValue ? ( int ) byte.MaxValue : num4;
            }
        }

        public void apply_gamma_dir( GammaLookUpTable gamma )
        {
            this.Red0To255 = ( int ) gamma.dir( ( int ) ( byte ) this.Red0To255 );
            this.Green0To255 = ( int ) gamma.dir( ( int ) ( byte ) this.Green0To255 );
            this.Blue0To255 = ( int ) gamma.dir( ( int ) ( byte ) this.Blue0To255 );
        }

        public static IColorType no_color()
        {
            return ( IColorType ) new RGBA_Bytes( 0, 0, 0, 0 );
        }

        public static RGBA_Bytes rgb8_packed( int v )
        {
            return new RGBA_Bytes( v >> 16 & ( int ) byte.MaxValue, v >> 8 & ( int ) byte.MaxValue, v & ( int ) byte.MaxValue );
        }

        public RGBA_Bytes Blend( RGBA_Bytes other, double weight )
        {
            RGBA_Bytes rgbaBytes = new RGBA_Bytes(this);
            return this * ( 1.0 - weight ) + other * weight;
        }
    }
}
