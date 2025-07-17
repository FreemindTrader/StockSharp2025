// System.Windows.Media.Imaging.BitmapFactory
using System.Windows.Media;
using System.Windows.Media.Imaging;

internal static class BitmapFactory
{
    internal static WriteableBitmap New( int pixelWidth, int pixelHeight )
    {
        if ( pixelHeight < 1 )
        {
            pixelHeight = 1;
        }
        if ( pixelWidth < 1 )
        {
            pixelWidth = 1;
        }
        return new WriteableBitmap( pixelWidth, pixelHeight, 96.0, 96.0, PixelFormats.Pbgra32, null );
    }
}
