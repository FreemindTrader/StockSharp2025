// System.Windows.Media.Imaging.WriteableBitmapContextExtensions
using System.Windows.Media.Imaging;

internal static class WriteableBitmapContextExtensions
{
    internal static BitmapContext GetBitmapContext( this WriteableBitmap bmp )
    {
        return new BitmapContext( bmp );
    }

    internal static BitmapContext GetBitmapContext( this WriteableBitmap bmp, ReadWriteMode mode )
    {
        return new BitmapContext( bmp, mode );
    }
}
