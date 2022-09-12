using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Core;
using Ecng.Common;

using StockSharp.BusinessEntities;

using StockSharp.Localization;
using StockSharp.Messages;

namespace StockSharp.Xaml
{    
    internal sealed class TradeSideBrushConverter : IMultiValueConverter
    {        
        private Brush _CornflowerBlueBrush = (Brush) Brushes.CornflowerBlue;
        private Brush _CadetBlueBrush = (Brush) Brushes.CadetBlue;
        private Brush _HotPinkBrush = (Brush) Brushes.HotPink;
        private Brush _LightCoralBrush = (Brush) Brushes.LightCoral;

        

        
        public Brush CornflowerBlueBrush
        {
            get { return _CornflowerBlueBrush; }
            set
            {
                _CornflowerBlueBrush = value;
            }
        }
        


        public Brush CadetBlueBrush
        {
            get { return _CadetBlueBrush; }
            set
            {
                _CadetBlueBrush = value;
            }
        }
        



        public Brush HotPinkBrush
        {
            get { return _HotPinkBrush; }
            set
            {
                _HotPinkBrush = value;
            }
        }
        

        public Brush LightCoralBrush
        {
            get { return _LightCoralBrush; }
            set
            {
                _LightCoralBrush = value;
            }
        }
        

        

        object IMultiValueConverter.Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {
            object obj = values[0];
            if ( obj == null || obj == DependencyProperty.UnsetValue )
                return Binding.DoNothing;

            bool flag = ApplicationThemeHelper.ApplicationThemeName.IsDark();
            Sides? tradeSide = (Sides?) obj;

            if ( tradeSide.HasValue )
            {
                Sides valueOrDefault = tradeSide.GetValueOrDefault();
                if ( valueOrDefault != Sides.Buy )
                {
                    if ( valueOrDefault == Sides.Sell )
                    {
                        if ( !flag )
                            return ( object ) this.LightCoralBrush;
                        return ( object ) this.HotPinkBrush;
                    }
                }
                else
                {
                    if ( !flag )
                        return ( object ) this.CadetBlueBrush;
                    return ( object ) this.CornflowerBlueBrush;
                }
            }
            return Binding.DoNothing;
        }

        object[ ] IMultiValueConverter.ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
