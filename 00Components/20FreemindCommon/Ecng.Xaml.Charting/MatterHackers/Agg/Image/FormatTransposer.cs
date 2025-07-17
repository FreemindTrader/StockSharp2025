// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.FormatTransposer
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg.Image
{
    internal sealed class FormatTransposer : ImageProxy
    {
        public FormatTransposer( IImageByte pixelFormat )
          : base( pixelFormat )
        {
        }

        public override int Width
        {
            get
            {
                return this._linkedImage.Height;
            }
        }

        public override int Height
        {
            get
            {
                return this._linkedImage.Width;
            }
        }

        public override RGBA_Bytes GetPixel( int x, int y )
        {
            return this._linkedImage.GetPixel( y, x );
        }

        public override void copy_pixel( int x, int y, byte[ ] c, int ByteOffset )
        {
            this._linkedImage.copy_pixel( y, x, c, ByteOffset );
        }

        public override void copy_hline( int x, int y, int len, RGBA_Bytes c )
        {
            this._linkedImage.copy_vline( y, x, len, c );
        }

        public override void copy_vline( int x, int y, int len, RGBA_Bytes c )
        {
            this._linkedImage.copy_hline( y, x, len, c );
        }

        public override void blend_hline( int x1, int y, int x2, RGBA_Bytes c, byte cover )
        {
            this._linkedImage.blend_vline( y, x1, x2, c, cover );
        }

        public override void blend_vline( int x, int y1, int y2, RGBA_Bytes c, byte cover )
        {
            this._linkedImage.blend_hline( y1, x, y2, c, cover );
        }

        public override void blend_solid_hspan( int x, int y, int len, RGBA_Bytes c, byte[ ] covers, int coversIndex )
        {
            this._linkedImage.blend_solid_vspan( y, x, len, c, covers, coversIndex );
        }

        public override void blend_solid_vspan( int x, int y, int len, RGBA_Bytes c, byte[ ] covers, int coversIndex )
        {
            this._linkedImage.blend_solid_hspan( y, x, len, c, covers, coversIndex );
        }

        public override void copy_color_hspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex )
        {
            this._linkedImage.copy_color_vspan( y, x, len, colors, colorsIndex );
        }

        public override void copy_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex )
        {
            this._linkedImage.copy_color_hspan( y, x, len, colors, colorsIndex );
        }

        public override void blend_color_hspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            this._linkedImage.blend_color_vspan( y, x, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll );
        }

        public override void blend_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            this._linkedImage.blend_color_hspan( y, x, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll );
        }
    }
}
