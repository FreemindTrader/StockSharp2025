// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.gsv_text
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.VertexSource
{
    [Obsolete( "All of these shoud use the new font stuff.  You probably want a StringPrinter or a TextWidget in this spot." )]
    internal sealed class gsv_text : IVertexSource
    {
        private double m_StartX;
        private double m_CurrentX;
        private double m_CurrentY;
        private double m_WidthRatioOfHeight;
        private double m_FontSize;
        private double m_SpaceBetweenCharacters;
        private double m_SpaceBetweenLines;
        private string m_Text;
        private int m_CurrentCharacterIndex;
        private byte[] m_font;
        private gsv_text.status m_status;
        private int m_StartOfIndicesIndex;
        private int m_StartOfGlyphsIndex;
        private int m_BeginGlyphIndex;
        private int m_EndGlyphIndex;
        private double m_WidthScaleRatio;
        private double m_HeightScaleRatio;

        public double FontSize
        {
            get
            {
                return m_FontSize;
            }
            set
            {
                m_FontSize = value;
                m_HeightScaleRatio = m_FontSize / ( double ) translateIndex( 4 );
                m_WidthScaleRatio = m_HeightScaleRatio * m_WidthRatioOfHeight;
            }
        }

        public double AscenderHeight
        {
            get
            {
                return m_FontSize * 0.15;
            }
        }

        public double DescenderHeight
        {
            get
            {
                return m_FontSize * 0.2;
            }
        }

        public gsv_text()
        {
            m_font = CGSVDefaultFont.gsv_default_font;
            m_CurrentX = 0.0;
            m_CurrentY = 0.0;
            m_StartX = 0.0;
            m_WidthRatioOfHeight = 1.0;
            FontSize = 0.0;
            m_SpaceBetweenCharacters = 0.0;
            m_status = gsv_text.status.initial;
            m_SpaceBetweenLines = 0.0;
        }

        public void load_font( string file )
        {
            throw new NotImplementedException();
        }

        public void SetFontSize( double fontSize )
        {
            SetFontSizeAndWidthRatio( fontSize, 1.0 );
        }

        public void SetFontSizeAndWidthRatio( double fontSize, double widthRatioOfHeight )
        {
            if ( fontSize == 0.0 || widthRatioOfHeight == 0.0 )
                throw new Exception( "You can't have a font with 0 width or height.  Nothing will render." );
            m_WidthRatioOfHeight = widthRatioOfHeight;
            FontSize = fontSize;
            m_SpaceBetweenLines = FontSize * 1.5;
        }

        public void SetSpaceBetweenCharacters( double spaceBetweenCharacters )
        {
            m_SpaceBetweenCharacters = spaceBetweenCharacters;
        }

        public void line_space( double spaceBetweenLines )
        {
            m_SpaceBetweenLines = spaceBetweenLines;
        }

        public void start_point( double x, double y )
        {
            m_CurrentX = m_StartX = x;
            m_CurrentY = y;
        }

        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
            }
        }

        public void text( string text )
        {
            m_Text = text;
        }

        private ushort translateIndex( int indicesIndex )
        {
            return ( ushort ) ( ( uint ) m_font[ indicesIndex ] | ( uint ) ( ushort ) ( ( uint ) m_font[ indicesIndex + 1 ] << 8 ) );
        }

        public void rewind( int nothing )
        {
            m_status = gsv_text.status.initial;
            if ( m_font == null )
                return;
            m_StartOfIndicesIndex = ( int ) translateIndex( 0 );
            m_StartOfGlyphsIndex = m_StartOfIndicesIndex + 514;
            m_CurrentCharacterIndex = 0;
        }

        private void GetSize( char characterToMeasure, out double width, out double height )
        {
            width = 0.0;
            height = 0.0;
            if ( m_font == null )
                return;
            int num1 = (int) characterToMeasure & (int) byte.MaxValue;
            switch ( num1 )
            {
                case 10:
                case 13:
                    height -= FontSize + m_SpaceBetweenLines;
                    break;
                default:
                    int num2 = num1 * 2;
                    int num3 = m_StartOfGlyphsIndex + (int) translateIndex(m_StartOfIndicesIndex + num2);
                    int num4 = m_StartOfGlyphsIndex + (int) translateIndex(m_StartOfIndicesIndex + num2 + 2);
                    while ( num3 < num4 )
                    {
                        byte[] font1 = m_font;
                        int index1 = num3;
                        int num5 = index1 + 1;
                        int num6 = (int) (sbyte) font1[index1];
                        byte[] font2 = m_font;
                        int index2 = num5;
                        num3 = index2 + 1;
                        int num7 = (int) (sbyte) ((int) (sbyte) ((int) (sbyte) font2[index2] << 1) >> 1);
                        width += ( double ) num6 * m_WidthScaleRatio;
                        height += ( double ) num7 * m_HeightScaleRatio;
                    }
                    break;
            }
        }

        public int GetCharacterIndexToStartBefore( Vector2 position )
        {
            int num1 = -1;
            double num2 = double.MaxValue;
            Vector2 vector2;
            vector2.x = 0.0;
            vector2.y = 0.0;
            int val1_1 = 0;
            int val1_2 = m_Text.Length - 1;
            if ( m_Text.Length > 0 )
            {
                int num3 = Math.Max(0, Math.Min(val1_1, m_Text.Length - 1));
                int num4 = Math.Max(0, Math.Min(val1_2, m_Text.Length - 1));
                for ( int index = num3 ; index <= num4 ; ++index )
                {
                    double length = (vector2 - position).Length;
                    if ( length < num2 )
                    {
                        num2 = length;
                        num1 = index;
                    }
                    char characterToMeasure = m_Text[index];
                    switch ( characterToMeasure )
                    {
                        case '\n':
                        case '\r':
                            vector2.x = 0.0;
                            vector2.y -= FontSize + m_SpaceBetweenLines;
                            break;
                        default:
                            double width;
                            double height;
                            GetSize( characterToMeasure, out width, out height );
                            vector2.x += width + m_SpaceBetweenCharacters;
                            vector2.y += height;
                            break;
                    }
                }
                if ( ( vector2 - position ).Length < num2 )
                    num1 = num4 + 1;
            }
            return num1;
        }

        public void GetSize( out Vector2 pixelSize )
        {
            GetSize( 0, m_Text.Length - 1, out pixelSize );
        }

        public void GetSize( int characterToMeasureStartIndexInclusive, int characterToMeasureEndIndexInclusive, out Vector2 pixelSize )
        {
            double val1 = 0.0;
            pixelSize.x = 0.0;
            pixelSize.y = 0.0;
            if ( m_Text.Length <= 0 )
                return;
            characterToMeasureStartIndexInclusive = Math.Max( 0, Math.Min( characterToMeasureStartIndexInclusive, m_Text.Length - 1 ) );
            characterToMeasureEndIndexInclusive = Math.Max( 0, Math.Min( characterToMeasureEndIndexInclusive, m_Text.Length - 1 ) );
            for ( int index = characterToMeasureStartIndexInclusive ; index <= characterToMeasureEndIndexInclusive ; ++index )
            {
                char characterToMeasure = m_Text[index];
                switch ( characterToMeasure )
                {
                    case '\n':
                    case '\r':
                        val1 = 0.0;
                        pixelSize.y -= FontSize + m_SpaceBetweenLines;
                        break;
                    default:
                        double width;
                        double height;
                        GetSize( characterToMeasure, out width, out height );
                        val1 += width + m_SpaceBetweenCharacters;
                        pixelSize.x = Math.Max( val1, pixelSize.x );
                        pixelSize.y += height;
                        break;
                }
            }
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            x = 0.0;
            y = 0.0;
            bool flag = false;
            while ( !flag )
            {
                switch ( m_status )
                {
                    case gsv_text.status.initial:
                        if ( m_font == null )
                        {
                            flag = true;
                            continue;
                        }
                        m_status = gsv_text.status.next_char;
                        goto case gsv_text.status.next_char;
                    case gsv_text.status.next_char:
                        if ( m_CurrentCharacterIndex == m_Text.Length )
                        {
                            flag = true;
                            continue;
                        }
                        int num1 = (int) m_Text[m_CurrentCharacterIndex++] & (int) byte.MaxValue;
                        switch ( num1 )
                        {
                            case 10:
                            case 13:
                                m_CurrentX = m_StartX;
                                m_CurrentY -= FontSize + m_SpaceBetweenLines;
                                continue;
                            default:
                                int num2 = num1 * 2;
                                m_BeginGlyphIndex = m_StartOfGlyphsIndex + ( int ) translateIndex( m_StartOfIndicesIndex + num2 );
                                m_EndGlyphIndex = m_StartOfGlyphsIndex + ( int ) translateIndex( m_StartOfIndicesIndex + num2 + 2 );
                                m_status = gsv_text.status.start_glyph;
                                goto label_10;
                        }
                    case gsv_text.status.start_glyph:
                    label_10:
                        x = m_CurrentX;
                        y = m_CurrentY;
                        m_status = gsv_text.status.glyph;
                        return Path.FlagsAndCommand.CommandMoveTo;
                    case gsv_text.status.glyph:
                        if ( m_BeginGlyphIndex >= m_EndGlyphIndex )
                        {
                            m_status = gsv_text.status.next_char;
                            m_CurrentX += m_SpaceBetweenCharacters;
                            continue;
                        }
                        int num3 = (int) (sbyte) m_font[m_BeginGlyphIndex++];
                        int num4 = (int) (sbyte) m_font[m_BeginGlyphIndex++];
                        sbyte num5 = (sbyte) (num4 & 128);
                        int num6 = (int) (sbyte) ((int) (sbyte) (num4 << 1) >> 1);
                        m_CurrentX += ( double ) num3 * m_WidthScaleRatio;
                        m_CurrentY += ( double ) num6 * m_HeightScaleRatio;
                        x = m_CurrentX;
                        y = m_CurrentY;
                        return num5 != ( sbyte ) 0 ? Path.FlagsAndCommand.CommandMoveTo : Path.FlagsAndCommand.CommandLineTo;
                    default:
                        throw new Exception( "Unknown Status" );
                }
            }
            return Path.FlagsAndCommand.CommandStop;
        }

        private enum status
        {
            initial,
            next_char,
            start_glyph,
            glyph,
        }
    }
}
