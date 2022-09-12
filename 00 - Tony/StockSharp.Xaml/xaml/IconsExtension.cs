using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using Ecng.Xaml;
using System;
using System.Windows.Media;

namespace StockSharp.Xaml
{
    public class IconsExtension : ResourseDictExtension
    {
        public IconsExtension( ) 
        {

            AssemblyName = "StockSharp.Xaml";
            Path         = "xaml/Themes/Icons.xaml";
        }

        public static DrawingImage GetImage( string key )
        {
            if ( InnerDict == null )
            {
                new IconsExtension( ).InitDict( );
            }
                
            return ( DrawingImage )InnerDict[ key ];
        }

        public static ImageSource GetSvgImage( string vectorIconName )
        {
            string galleryMatch = "";

            switch ( vectorIconName )
            {
                case "Order":
                {
                    galleryMatch = @"SvgImages/RichEdit/RichEditBookmark.svg";
                }                
                break;

                case "CandleStick":
                {
                    galleryMatch = @"SvgImages/Chart/ChartType_CandleStick.svg";
                }
                break;

                case "Chat":
                {
                    galleryMatch = @"SvgImages/Outlook Inspired/Glyph_Message.svg";
                }
                break;

                case "Chart1":
                {
                    galleryMatch = @"SvgImages/XAF/Navigation_Item_PivotChart.svg";
                }
                break;

                case "Chart3":
                {
                    galleryMatch = @"SvgImages/Outlook Inspired/High.svg";
                }
                break;

                case "Bank":
                {
                    galleryMatch = @"SvgImages/RichEdit/RichEditBookmark.svg";
                }
                break;

                case "Table":
                {
                    galleryMatch = @"SvgImages/Dashboards/ChangeChartSeriesType.svg";
                }
                break;

                case "Logs":
                {
                    galleryMatch = @"SvgImages/Business Objects/BO_Document.svg";
                }
                break;

                case "Storage":
                {
                    galleryMatch = @"SvgImages/Snap/Datasource.svg";
                }
                break;

                case "Deal":
                {
                    galleryMatch = @"SvgImages/Content/CheckBox.svg";
                }
                break;

                case "Telegram":
                {
                    galleryMatch = @"SvgImages/Miscellaneous/Language.svg";
                }
                break;

                case "OptionChart":
                {
                    galleryMatch = @"SvgImages/DiagramIcons/colors.svg";
                }
                break;

                case "Parabola":
                {
                    galleryMatch = @"SvgImages/Chart/ChartType_Spline.svg";
                }
                break;

                case "Portfolio":
                {
                    galleryMatch = @"SvgImages/Dashboards/DataLabels.svg";
                }
                break;

                case "Money":
                {
                    galleryMatch = @"SvgImages/XAF/Demo_SalesOverview.svg";
                }
                break;

                case "Edit":
                {
                    galleryMatch = @"SvgImages/Dashboards/AutomaticUpdates.svg";
                }
                break;

                case "Dices":
                {
                    galleryMatch = @"SvgImages/Outlook Inspired/Unlike.svg";
                }
                break;

                case "UpDown":
                {
                    galleryMatch = @"SvgImages/Outlook Inspired/LineSpacing.svg";
                }
                break;

                case "Certificate":
                {
                    galleryMatch = @"SvgImages/Dashboards/Currency.svg";
                }
                break;

                case "Clipboard2":
                {
                    galleryMatch = @"SvgImages/Dashboards/Chart.svg";
                }
                break;

                case "Bell":
                {
                    galleryMatch = @"SvgImages/Icon Builder/Actions_Bell.svg";
                }
                break;

                default:
                {
                    galleryMatch = @"SvgImages/Icon Builder/Business_DollarCircled.svg";
                }
                break;
            }
            
            var imageUri    = DXImageHelper.GetImageUri( galleryMatch );

            var imageSrc    = WpfSvgRenderer.CreateImageSource( imageUri );

            if ( imageSrc == null )
            {

            }

            return imageSrc;
        }
    }
}
