using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Visuals;

namespace StockSharp.Xaml.Charting.Common.Helpers
{
    internal static class BitmapPrintingHelper
    {
        public static BitmapSource ExportToBitmapSource( this UIElement source )
        {
            double height = source.RenderSize.Height;
            Size renderSize = source.RenderSize;
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)renderSize.Width, (int)height, 96, 96, PixelFormats.Pbgra32);
            UIElement uIElement = (UIElement)source.GetVisualChildren().Single<DependencyObject>();
            renderTargetBitmap.Render( uIElement );
            return renderTargetBitmap;
        }

        private static BitmapEncoder GetEncoder( ExportType exportType )
        {
            if ( exportType == ExportType.Png )
            {
                return new PngBitmapEncoder();
            }
            if ( exportType == ExportType.Bmp )
            {
                return new BmpBitmapEncoder();
            }
            if ( exportType != ExportType.Jpeg )
            {
                throw new InvalidEnumArgumentException( "Unsupported ExportType" );
            }
            return new JpegBitmapEncoder();
        }

        public static void SaveToFile( BitmapSource bitmap, string fileName, ExportType imageType )
        {
            BitmapEncoder encoder = BitmapPrintingHelper.GetEncoder(imageType);
            using ( FileStream fileStream = new FileStream( fileName, FileMode.Create ) )
            {
                bitmap.WriteToStream( fileStream, encoder );
            }
        }

        private static void WriteToStream( this BitmapSource bitmap, Stream stream, BitmapEncoder encoder )
        {
            encoder.Frames.Add( BitmapFrame.Create( bitmap ) );
            encoder.Save( stream );
        }
    }
}
