using System;
using DevExpress.Xpf.Core.Native;

namespace MatterHackers.Agg.Image
{
    internal interface IImageByte : IImage
    {
        int StrideInBytes();

        int StrideInBytesAbs();

        IBlenderByte GetBlender();

        void SetBlender( IBlenderByte value );

        int GetBytesBetweenPixelsInclusive();

        byte[ ] GetBuffer();

        RGBA_Bytes GetPixel( int x, int y );

        void copy_pixel( int x, int y, byte[ ] c, int ByteOffset );

        void CopyFrom( IImageByte sourceImage );

        void CopyFrom( IImageByte sourceImage, RectangleInt sourceImageRect, int destXOffset, int destYOffset );

        void SetPixel( int x, int y, RGBA_Bytes color );

        void BlendPixel( int x, int y, RGBA_Bytes sourceColor, byte cover );

        void copy_hline( int x, int y, int len, RGBA_Bytes sourceColor );

        void copy_vline( int x, int y, int len, RGBA_Bytes sourceColor );

        void blend_hline( int x, int y, int x2, Func<int, int, RGBA_Bytes> sourceColorCb, byte cover );

        void blend_hline( int x, int y, int x2, RGBA_Bytes sourceColor, byte cover );

        void blend_vline( int x, int y1, int y2, RGBA_Bytes sourceColor, byte cover );

        void copy_color_hspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorIndex );

        void copy_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorIndex );

        void blend_solid_hspan( int x, int y, int len, Func<int, int, RGBA_Bytes> sourceColorCb, byte[ ] covers, int coversIndex );

        void blend_solid_hspan( int x, int y, int len, RGBA_Bytes sourceColor, byte[ ] covers, int coversIndex );

        void blend_solid_vspan( int x, int y, int len, RGBA_Bytes sourceColor, byte[ ] covers, int coversIndex );

        void blend_color_hspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll );

        void blend_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll );
    }
}
