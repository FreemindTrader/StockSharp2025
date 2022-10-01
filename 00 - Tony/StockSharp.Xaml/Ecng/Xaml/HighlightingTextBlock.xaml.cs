// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.HighlightingTextBlock
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace Ecng.Xaml
{
    /// <summary>
    /// A specialized highlighting text block control.
    /// http://www.jeff.wilcox.name/2008/11/highlighting-autocompletebox/
    /// </summary>
    /// <summary>HighlightingTextBlock</summary>
    public partial class HighlightingTextBlock : TextBlock, IComponentConnector
    {
        /// <summary>Identifies the HighlightText dependency property.</summary>
        public static readonly DependencyProperty HighlightTextProperty = DependencyProperty.Register( nameof( HighlightText ), typeof( string ), typeof( HighlightingTextBlock ), new PropertyMetadata( new PropertyChangedCallback( OnHighlightTextPropertyChanged ) ) );
        /// <summary>Identifies the HighlightBrush dependency property.</summary>
        public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.Register( nameof( HighlightBrush ), typeof( Brush ), typeof( HighlightingTextBlock ), new PropertyMetadata( ( object )null, new PropertyChangedCallback( OnHighlightBrushPropertyChanged ) ) );
        /// <summary>
        /// Identifies the HighlightFontWeight dependency property.
        /// </summary>
        public static readonly DependencyProperty HighlightFontWeightProperty = DependencyProperty.Register( nameof( HighlightFontWeight ), typeof( FontWeight ), typeof( HighlightingTextBlock ), new PropertyMetadata( ( object )FontWeights.Normal, new PropertyChangedCallback( OnHighlightFontWeightPropertyChanged ) ) );

        private bool _initialized;



        /// <summary>Initializes a new HighlightingTextBlock class.</summary>
        public HighlightingTextBlock()
        {
            this.DefaultStyleKey = ( object )typeof( HighlightingTextBlock );
        }

        /// <summary>Gets or sets the highlighted text.</summary>
        public string HighlightText
        {
            get
            {
                return this.GetValue( HighlightingTextBlock.HighlightTextProperty ) as string;
            }
            set
            {
                this.SetValue( HighlightingTextBlock.HighlightTextProperty, ( object )value );
            }
        }

        private static void OnHighlightTextPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( HighlightingTextBlock )d ).ApplyHighlighting();
        }

        /// <summary>Gets or sets the highlight brush.</summary>
        public Brush HighlightBrush
        {
            get
            {
                return this.GetValue( HighlightingTextBlock.HighlightBrushProperty ) as Brush;
            }
            set
            {
                this.SetValue( HighlightingTextBlock.HighlightBrushProperty, ( object )value );
            }
        }

        private static void OnHighlightBrushPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( HighlightingTextBlock )d ).ApplyHighlighting();
        }

        /// <summary>
        /// Gets or sets the font weight used on highlighted text.
        /// </summary>
        public FontWeight HighlightFontWeight
        {
            get
            {
                return ( FontWeight )this.GetValue( HighlightingTextBlock.HighlightFontWeightProperty );
            }
            set
            {
                this.SetValue( HighlightingTextBlock.HighlightFontWeightProperty, ( object )value );
            }
        }

        private static void OnHighlightFontWeightPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
        }

        private void ApplyHighlighting()
        {
            if ( !this._initialized )
            {
                this.Inlines.Clear();
                foreach ( char ch in this.Text )
                    this.Inlines.Add( ( Inline )new Run()
                    {
                        Text = ( string )Converter.To<string>( ( object )ch )
                    } );
                this._initialized = true;
            }
            string str1 = this.Text ?? string.Empty;
            string str2 = this.HighlightText ?? string.Empty;
            int num1 = 0;
            label_12:
            while ( num1 < str1.Length )
            {
                int num2 = str2.Length == 0 ? -1 : str1.IndexOf( str2, num1, StringComparison.OrdinalIgnoreCase );
                for ( int index = num2 < 0 ? str1.Length : num2; num1 < index && num1 < str1.Length; ++num1 )
                {
                    Inline inline = this.Inlines.ElementAt<Inline>( num1 );
                    inline.Foreground = this.Foreground;
                    inline.FontWeight = this.FontWeight;
                }
                int num3 = num1;
                while ( true )
                {
                    if ( num1 < num3 + str2.Length && num1 < str1.Length )
                    {
                        Inline inline = this.Inlines.ElementAt<Inline>( num1 );
                        inline.Foreground = this.HighlightBrush;
                        inline.FontWeight = this.HighlightFontWeight;
                        ++num1;
                    }
                    else
                        goto label_12;
                }
            }
        }        
    }
}
