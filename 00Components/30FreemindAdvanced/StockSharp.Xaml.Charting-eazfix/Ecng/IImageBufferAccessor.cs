using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal interface IImageBufferAccessor
    {
        byte[ ] span( int x, int y, int len, out int bufferIndex );

        byte[ ] next_x( out int bufferByteOffset );

        byte[ ] next_y( out int bufferByteOffset );

        IImageByte SourceImage
        {
            get;
        }
    }
}