// MatterHackers.Agg.span_image_filter_rgba_nn_stepXby1
using System;
using MatterHackers.Agg;
using MatterHackers.Agg.Image;

internal class span_image_filter_rgba_nn_stepXby1 : span_image_filter
{
    private const int base_shift = 8;

    private const int base_scale = 256;

    private const int base_mask = 255;

    public span_image_filter_rgba_nn_stepXby1( IImageBufferAccessor sourceAccessor, ISpanInterpolator spanInterpolator )
        : base( sourceAccessor, spanInterpolator, null )
    {
    }

    public unsafe override void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
    {
        ImageBuffer imageBuffer = (ImageBuffer)GetImageBufferAccessor().SourceImage;
        if ( imageBuffer.BitDepth != 32 )
        {
            throw new NotSupportedException( "The source is expected to be 32 bit." );
        }
        ISpanInterpolator spanInterpolator = interpolator();
        spanInterpolator.begin( ( double ) x + filter_dx_dbl(), ( double ) y + filter_dy_dbl(), len );
        spanInterpolator.coordinates( out int x2, out int y2 );
        int x3 = x2 >> 8;
        int y3 = y2 >> 8;
        int num = imageBuffer.GetBufferOffsetXY(x3, y3);
        byte[] buffer = imageBuffer.GetBuffer();
        fixed ( byte* ptr = buffer )
        {
            do
            {
                span[ spanIndex++ ] = *( RGBA_Bytes* ) ( ptr + num );
                num += 4;
            }
            while ( --len != 0 );
        }
    }
}
