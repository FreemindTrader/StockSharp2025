//using Ecng.Common;
//using System;
//using System.Windows.Media;

//namespace StockSharp.Xaml
//{
//    public static class ThemeExtensions
//    {
//        public static string DefaultTheme = "VS2017Dark";

//        public static bool IsDark( this string appTheme )
//        {
//            if ( StringHelper.IsEmpty( appTheme ) )
//                throw new ArgumentNullException( nameof( appTheme ) );
//            return appTheme == "Office2016BlackSE" || appTheme == "Office2016BlackSE;Touch" || ( appTheme == "MetropolisDark" || appTheme == "VS2017Dark" ) || appTheme == "TouchlineDark";
//        }

//        public static Color ToTextColor( this string appTheme )
//        {
//            if ( !appTheme.IsDark() )
//                return Colors.DimGray;
//            return Colors.Lavender;
//        }

//        public static Brush ToThemeToolbarIconBrush( this string appTheme )
//        {
//            if ( !appTheme.IsDark() )
//                return ( Brush ) Brushes.DimGray;
//            return ( Brush ) Brushes.Lavender;
//        }
//    }
//}
