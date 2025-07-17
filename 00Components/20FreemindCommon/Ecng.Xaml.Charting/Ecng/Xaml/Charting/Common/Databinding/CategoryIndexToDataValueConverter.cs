// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Databinding.CategoryIndexToDataValueConverter
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.Utility;
using Ecng.Xaml.Charting.Visuals.Annotations;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting.Common.Databinding
{
    internal class CategoryIndexToDataValueConverter : IValueConverter
    {
        private readonly LineAnnotationWithLabelsBase _annotationSource;

        internal CategoryIndexToDataValueConverter( LineAnnotationWithLabelsBase annotationSource )
        {
            Guard.NotNull( annotationSource, nameof( annotationSource ) );
            this._annotationSource = annotationSource;
        }

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == null || !this._annotationSource.XAxes.Any<IAxis>() )
                return value;
            IAxis usedAxis = this._annotationSource.GetUsedAxis();
            if ( usedAxis == null )
                return null;
            if ( usedAxis is ICategoryAxis )
            {
                ICategoryCoordinateCalculator coordinateCalculator = usedAxis.GetCurrentCoordinateCalculator() as ICategoryCoordinateCalculator;
                if ( coordinateCalculator != null )
                {
                    object obj;

                    if ( ( obj = value ) is DateTime )
                    {
                        value = ( DateTime ) obj;
                    }
                    else
                    {
                        int index = (int) (value is int ? value : System.Convert.ChangeType(value, typeof (int), (IFormatProvider) CultureInfo.InvariantCulture));
                        value = coordinateCalculator.TransformIndexToData( index );
                    }
                }
            }
            double coordinate = usedAxis.GetCoordinate((IComparable) value);
            value = usedAxis.GetDataValue( coordinate );
            return value;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
