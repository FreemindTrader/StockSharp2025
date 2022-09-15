using Ecng.Xaml.DevExp;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace StockSharp.Xaml
{
    public class ThemedIconsExtension : IconsExtension
    {
        public ThemedIconsExtension( )
        {
            AssemblyName = "StockSharp.Xaml";
            Path = "xaml/Themes/Icons.xaml";

            TestStream( );
        }

        protected static ResourceDictionary MyInnerDict { get; private set; }

        public void TestStream()
        {
            var myAssembly = Assembly.Load( ( string )this.AssemblyName );

            var mainfest = this.AssemblyName.ToString( ) + "." + ( ( string )this.Path ).Replace( '/', '.' );

            var names = System.Reflection.Assembly.GetExecutingAssembly( ).GetManifestResourceNames( );

            using ( Stream manifestResourceStream = myAssembly.GetManifestResourceStream( mainfest ) )
            {
                MyInnerDict = ( ResourceDictionary )XamlReader.Load( manifestResourceStream );
            }
                
        }

        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            if ( MyInnerDict == null )
                this.TestStream( );

            //return MyInnerDict[ this.Key ];

            DrawingImage drawingImage = ( DrawingImage )MyInnerDict[ this.Key ];
            if ( drawingImage == null )
            {
                return ( object )null;
            }

            return ( ( MarkupExtension )new ThemedIconBinding( drawingImage ) ).ProvideValue( serviceProvider );

            //DrawingImage drawingImage = ( DrawingImage )base.ProvideValue( serviceProvider );
            //if ( drawingImage == null )
            //{
            //    return ( object )null;
            //}

            //return ( ( MarkupExtension )new ThemedIconBinding( drawingImage ) ).ProvideValue( serviceProvider );
        }
    }
}
