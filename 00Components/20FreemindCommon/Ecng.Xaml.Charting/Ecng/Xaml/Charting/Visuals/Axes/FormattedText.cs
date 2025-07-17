// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Axes.FormattedText
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting.Visuals.Axes
{
    internal class FormattedText
    {
        private bool _hasExponent;
        private const int MinDistance = 1;
        private const double ExpFontSizeCoef = 1.2;

        public TextBlock Significand
        {
            get; private set;
        }

        public TextBlock Exponent
        {
            get; private set;
        }

        public TextBlock Separator
        {
            get; private set;
        }

        public Rect LabelRect
        {
            get; private set;
        }

        public Thickness Indents
        {
            get; private set;
        }

        public double FontSize
        {
            get; private set;
        }

        public bool HasExponent
        {
            get
            {
                return this._hasExponent;
            }
        }

        public FormattedText( double x, double y, string text, TextAlignment textAlignment, ScientificNotation mode = ScientificNotation.None )
          : this( x, y, new Thickness(), text, textAlignment, 0.0, mode )
        {
        }

        public FormattedText( double x, double y, string text, TextAlignment textAlignment, double fontSize, ScientificNotation mode = ScientificNotation.None )
          : this( x, y, new Thickness(), text, textAlignment, fontSize, mode )
        {
        }

        public FormattedText( double x, double y, Thickness indents, string text, TextAlignment textAlignment, double fontSize, ScientificNotation mode = ScientificNotation.None )
        {
            this._hasExponent = false;
            this.Indents = indents;
            if ( fontSize > 0.0 )
                this.FontSize = fontSize;
            this.Parse( text, mode );
            this.Measure( x, y, textAlignment );
        }

        private void Parse( string text, ScientificNotation mode )
        {
            string str1 = text;
            string str2 = "";
            string str3 = "";
            int length = text.IndexOfAny(new char[2]{ 'e', 'E' });
            if ( length > 0 && mode != ScientificNotation.None )
            {
                this._hasExponent = true;
                str1 = text.Substring( 0, length );
                str2 = text.Substring( length + 1 );
                str3 = text[ length ].ToString( ( IFormatProvider ) CultureInfo.InvariantCulture );
                if ( mode == ScientificNotation.Normalized )
                    str3 = "x10";
            }
            this.Significand = new TextBlock() { Text = str1 };
            if ( this.FontSize.CompareTo( 0.0 ) != 0 )
                this.Significand.FontSize = this.FontSize;
            TextBlock textBlock1;
            if ( !this._hasExponent )
            {
                textBlock1 = ( TextBlock ) null;
            }
            else
            {
                textBlock1 = new TextBlock();
                textBlock1.Text = str2;
            }
            this.Exponent = textBlock1;
            TextBlock textBlock2;
            if ( !this._hasExponent )
            {
                textBlock2 = ( TextBlock ) null;
            }
            else
            {
                textBlock2 = new TextBlock();
                textBlock2.Text = str3;
            }
            this.Separator = textBlock2;
        }

        private void Measure( double x, double y, TextAlignment textAlignment )
        {
            this.Significand.MeasureArrange();
            if ( this._hasExponent )
            {
                this.Exponent.FontSize = this.Significand.FontSize / 1.2;
                this.Separator.FontSize = this.Significand.FontSize;
                this.Separator.MeasureArrange();
                this.Exponent.MeasureArrange();
            }
            double num1 = this.Separator == null ? 0.0 : this.Separator.ActualWidth;
            double num2 = this.Exponent == null ? 0.0 : this.Exponent.ActualWidth;
            string input = this.Exponent == null ? string.Empty : this.Exponent.Text;
            double num3 = this.Exponent == null ? 0.0 : this.Exponent.ActualHeight;
            double width1 = this.Significand.ActualWidth + num1 + num2 + (this._hasExponent ? 0.0 : 2.0);
            double height1 = this.Significand.ActualHeight + num3 / 2.0;
            double top1 = y + num3 / 2.0;
            double num4 = x - width1 / 2.0;
            double y1;
            switch ( textAlignment )
            {
                case TextAlignment.Left:
                case TextAlignment.Right:
                    num4 = x;
                    double top2 = y - this.Significand.ActualHeight / 2.0;
                    this.SetLeft( ( FrameworkElement ) this.Significand, num4 );
                    this.SetTop( ( FrameworkElement ) this.Significand, top2 );
                    if ( this._hasExponent )
                    {
                        this.SetLeft( ( FrameworkElement ) this.Separator, num4 + this.Significand.ActualWidth + 1.0 );
                        this.SetTop( ( FrameworkElement ) this.Separator, top2 );
                        this.SetLeft( ( FrameworkElement ) this.Exponent, num4 + this.Significand.ActualWidth + num1 + 1.0 );
                        this.SetTop( ( FrameworkElement ) this.Exponent, top2 - num3 / 2.0 );
                    }
                    y1 = top2 - num3 / 2.0;
                    double x1 = x - this.Indents.Left;
                    double y2 = y1 - this.Indents.Top;
                    double num5 = this.Significand.ActualWidth + num1 + num2 + (input.IsNullOrWhiteSpace() ? 0.0 : 2.0);
                    Thickness indents1 = this.Indents;
                    double right = indents1.Right;
                    double width2 = num5 + right;
                    double num6 = this.Significand.ActualHeight + num3 / 2.0;
                    indents1 = this.Indents;
                    double bottom = indents1.Bottom;
                    double height2 = num6 + bottom;
                    this.LabelRect = new Rect( x1, y2, width2, height2 );
                    break;
                case TextAlignment.Center:
                    this.SetLeft( ( FrameworkElement ) this.Significand, num4 );
                    this.SetTop( ( FrameworkElement ) this.Significand, top1 );
                    if ( this._hasExponent )
                    {
                        this.SetLeft( ( FrameworkElement ) this.Separator, num4 + this.Significand.ActualWidth + 1.0 );
                        this.SetTop( ( FrameworkElement ) this.Separator, top1 );
                        this.SetLeft( ( FrameworkElement ) this.Exponent, num4 + this.Significand.ActualWidth + num1 + 1.0 );
                        this.SetTop( ( FrameworkElement ) this.Exponent, y );
                    }
                    y1 = y;
                    double num7 = num4;
                    Thickness indents2 = this.Indents;
                    double left = indents2.Left;
                    double x2 = num7 - left;
                    double num8 = y1;
                    indents2 = this.Indents;
                    double top3 = indents2.Top;
                    double y3 = num8 - top3;
                    double width3 = width1 + this.Indents.Right;
                    double height3 = height1 + this.Indents.Bottom;
                    this.LabelRect = new Rect( x2, y3, width3, height3 );
                    break;
                default:
                    throw new InvalidOperationException( "Invalid TextAlignment" );
            }
            this.LabelRect = new Rect( num4, y1, width1, height1 );
        }

        private void SetTop( FrameworkElement element, double top )
        {
            FrameworkElement frameworkElement = element;
            Thickness margin = element.Margin;
            double left = margin.Left;
            double top1 = top;
            margin = element.Margin;
            double right = margin.Right;
            margin = element.Margin;
            double bottom = margin.Bottom;
            Thickness thickness = new Thickness(left, top1, right, bottom);
            frameworkElement.Margin = thickness;
        }

        private void SetLeft( FrameworkElement element, double left )
        {
            FrameworkElement frameworkElement = element;
            double left1 = left;
            Thickness margin = element.Margin;
            double top = margin.Top;
            margin = element.Margin;
            double right = margin.Right;
            margin = element.Margin;
            double bottom = margin.Bottom;
            Thickness thickness = new Thickness(left1, top, right, bottom);
            frameworkElement.Margin = thickness;
        }
    }
}
