using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace fx.Common
{
    public static class GlyphHelper
    {
        public static BitmapImage GetGlyph( string path )
        {
            return new BitmapImage( DevExpress.Utils.AssemblyHelper.GetResourceUri( typeof( GlyphHelper ).Assembly, path ) );
        }

        public static ImageSource GetSvgImage( string path )
        {
            Uri imageUri = DXImageHelper.GetImageUri( path );

            var imageSize = new System.Windows.Size( 16, 16 );

            var output = WpfSvgRenderer.CreateImageSource( imageUri, null, imageSize );

            return output;
        }
    }
}