// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.LabelProviders.LogarithmicNumericLabelProvider
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Globalization;
namespace Ecng.Xaml.Charting
{
    public class LogarithmicNumericLabelProvider : NumericLabelProvider
    {
        public override void Init( IAxis parentAxis )
        {
            if ( !( parentAxis is ILogarithmicAxis ) )
                throw new ArgumentException( "LogarithmicNumericLabelProvider can be used with the LogarithmicNumericAxis only." );
            base.Init( parentAxis );
        }

        public override ITickLabelViewModel UpdateDataContext( ITickLabelViewModel labelDataContext, IComparable dataValue )
        {
            ILogarithmicAxis parentAxis = this.ParentAxis as ILogarithmicAxis;
            NumericTickLabelViewModel tickLabelViewModel = (NumericTickLabelViewModel) labelDataContext;
            tickLabelViewModel.HasExponent = parentAxis != null && parentAxis.ScientificNotation == ScientificNotation.LogarithmicBase;
            if ( tickLabelViewModel.HasExponent )
            {
                string textFormatting = this.ParentAxis.TextFormatting;
                int num1 = textFormatting.IndexOfAny(new char[2]{ 'e', 'E' });
                string str1 = textFormatting.Substring(0, num1);
                string str2 = textFormatting.Substring(num1 + 1);
                string str3 = "###.##";
                string str4 = str3;
                if ( str2.StartsWith( "+" ) )
                    str4 = "+" + str4;
                string str5 = str4 + ";-" + str3 + ";0";
                double a = dataValue.ToDouble();
                double y = Math.Log(a, parentAxis.LogarithmicBase);
                double num2 = a / Math.Pow(parentAxis.LogarithmicBase, y);
                tickLabelViewModel.HasExponent = true;
                tickLabelViewModel.Text = string.Format( ( IFormatProvider ) CultureInfo.InvariantCulture, "{0:" + str1 + "}x", new object[ 1 ]
                {
          (object) num2
                } );
                tickLabelViewModel.Exponent = string.Format( ( IFormatProvider ) CultureInfo.InvariantCulture, "{0:" + str5 + "}", new object[ 1 ]
                {
          (object) y
                } );
                tickLabelViewModel.Separator = parentAxis.LogarithmicBase.Equals( Math.E ) ? textFormatting.Substring( num1, 1 ) : parentAxis.LogarithmicBase.ToString( ( IFormatProvider ) CultureInfo.InvariantCulture );
            }
            else
                labelDataContext = base.UpdateDataContext( labelDataContext, dataValue );
            return labelDataContext;
        }
    }
}
