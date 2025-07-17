// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Databinding.GetAxisFormattedValueConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows.Data;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting.Common.Databinding
{
    public class GetAxisFormattedValueConverter : IValueConverter
    {
        private readonly LineAnnotationWithLabelsBase _parentAnnotation;

        public GetAxisFormattedValueConverter( LineAnnotationWithLabelsBase parentAnnotation )
        {
            this._parentAnnotation = parentAnnotation;
        }

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            IComparable comparable = value as IComparable;
            string str = value == null ? string.Empty : value.ToString();
            if ( this._parentAnnotation != null && comparable != null )
            {
                IAxis usedAxis = this._parentAnnotation.GetUsedAxis();
                BindingExpression bindingExpression = this._parentAnnotation.GetBindingExpression(LineAnnotationWithLabelsBase.LabelTextFormattingProperty);
                bool flag = bindingExpression != null;
                if ( flag )
                    flag = bindingExpression.ParentBinding.Path.Path == "DefaultTextFormatting";
                str = !flag || usedAxis == null ? string.Format( "{0:" + this._parentAnnotation.LabelTextFormatting + "}", ( object ) comparable ) : usedAxis.FormatCursorText( comparable );
            }
            return ( object ) str;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
