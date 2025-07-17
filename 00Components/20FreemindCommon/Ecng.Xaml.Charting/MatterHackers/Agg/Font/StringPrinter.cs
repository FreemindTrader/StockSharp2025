// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Font.StringPrinter
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using MatterHackers.Agg.VertexSource;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.Font
{
    internal class StringPrinter : IVertexSource
    {
        private string text = "";
        private int currentChar;
        private Vector2 currentOffset;
        private IVertexSource currentGlyph;
        private StyledTypeFace typeFaceStyle;
        private Vector2 totalSizeCach;

        public Justification Justification
        {
            get; set;
        }

        public Baseline Baseline
        {
            get; set;
        }

        public StyledTypeFace TypeFaceStyle
        {
            get
            {
                return this.typeFaceStyle;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        public Vector2 Origin
        {
            get; set;
        }

        public StringPrinter( string text = "", double pointSize = 12.0, Vector2 origin = default( Vector2 ), Justification justification = Justification.Left, Baseline baseline = Baseline.Text )
          : this( text, new StyledTypeFace( LiberationSansFont.Instance, pointSize ), origin, justification, baseline )
        {
        }

        public StringPrinter( string text, StyledTypeFace typeFaceStyle, Vector2 origin = default( Vector2 ), Justification justification = Justification.Left, Baseline baseline = Baseline.Text )
        {
            this.typeFaceStyle = typeFaceStyle;
            this.text = text;
            this.Justification = justification;
            this.Origin = origin;
            this.Baseline = baseline;
        }

        public StringPrinter( string text, StringPrinter copyPropertiesFrom )
          : this( text, copyPropertiesFrom.TypeFaceStyle, copyPropertiesFrom.Origin, copyPropertiesFrom.Justification, copyPropertiesFrom.Baseline )
        {
        }

        public RectangleDouble LocalBounds
        {
            get
            {
                Vector2 size = this.GetSize();
                RectangleDouble rectangleDouble;
                switch ( this.Justification )
                {
                    case Justification.Left:
                        rectangleDouble = new RectangleDouble( 0.0, this.typeFaceStyle.DescentInPixels, size.x, size.y + this.typeFaceStyle.DescentInPixels );
                        break;
                    case Justification.Center:
                        rectangleDouble = new RectangleDouble( -size.x / 2.0, this.typeFaceStyle.DescentInPixels, size.x / 2.0, size.y + this.typeFaceStyle.DescentInPixels );
                        break;
                    case Justification.Right:
                        rectangleDouble = new RectangleDouble( -size.x, this.typeFaceStyle.DescentInPixels, 0.0, size.y + this.typeFaceStyle.DescentInPixels );
                        break;
                    default:
                        throw new NotImplementedException();
                }
                rectangleDouble.Offset( this.Origin );
                return rectangleDouble;
            }
        }

        public void rewind( int pathId )
        {
            this.currentChar = 0;
            this.currentOffset = new Vector2( 0.0, 0.0 );
            if ( this.text == null || this.text.Length <= 0 )
                return;
            this.currentGlyph = this.typeFaceStyle.GetGlyphForCharacter( this.text[ this.currentChar ] );
            if ( this.currentGlyph == null )
                return;
            this.currentGlyph.rewind( 0 );
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            x = 0.0;
            y = 0.0;
            if ( this.text == null || this.text.Length <= 0 )
                return Path.FlagsAndCommand.CommandStop;
            Path.FlagsAndCommand flagsAndCommand = Path.FlagsAndCommand.CommandStop;
            if ( this.currentGlyph != null )
                flagsAndCommand = this.currentGlyph.vertex( out x, out y );
            Vector2 size = this.GetSize();
            double num1;
            switch ( this.Justification )
            {
                case Justification.Left:
                    num1 = 0.0;
                    break;
                case Justification.Center:
                    num1 = -size.x / 2.0;
                    break;
                case Justification.Right:
                    num1 = -size.x;
                    break;
                default:
                    throw new NotImplementedException();
            }
            double num2;
            switch ( this.Baseline )
            {
                case Baseline.BoundsTop:
                    num2 = -this.typeFaceStyle.AscentInPixels;
                    break;
                case Baseline.BoundsCenter:
                    num2 = -this.typeFaceStyle.AscentInPixels / 2.0;
                    break;
                case Baseline.Text:
                    num2 = 0.0;
                    break;
                default:
                    throw new NotImplementedException();
            }
            while ( flagsAndCommand == Path.FlagsAndCommand.CommandStop && this.currentChar < this.text.Length - 1 )
            {
                if ( this.currentChar < this.text.Length )
                    this.currentOffset.x += this.typeFaceStyle.GetAdvanceForCharacter( this.text[ this.currentChar ], this.text[ this.currentChar + 1 ] );
                else
                    this.currentOffset.x += this.typeFaceStyle.GetAdvanceForCharacter( this.text[ this.currentChar ] );
                ++this.currentChar;
                this.currentGlyph = this.typeFaceStyle.GetGlyphForCharacter( this.text[ this.currentChar ] );
                if ( this.currentGlyph != null )
                {
                    this.currentGlyph.rewind( 0 );
                    flagsAndCommand = this.currentGlyph.vertex( out x, out y );
                }
                else if ( this.text[ this.currentChar ] == '\n' )
                {
                    if ( this.currentChar + 1 < this.text.Length - 1 && this.text[ this.currentChar + 1 ] == '\n' && ( int ) this.text[ this.currentChar ] != ( int ) this.text[ this.currentChar + 1 ] )
                        ++this.currentChar;
                    this.currentOffset.x = 0.0;
                    this.currentOffset.y -= this.typeFaceStyle.EmSizeInPixels;
                }
            }
            x += this.currentOffset.x + num1 + this.Origin.x;
            y += this.currentOffset.y + num2 + this.Origin.y;
            return flagsAndCommand;
        }

        public void DrawFromHintedCache( Graphics2D graphics2D, RGBA_Bytes color )
        {
            graphics2D.Render( ( IVertexSource ) this, color );
        }

        public Vector2 GetSize()
        {
            if ( this.totalSizeCach.x == 0.0 )
            {
                Vector2 offset;
                this.GetSize( 0, Math.Max( 0, this.text.Length - 1 ), out offset );
                this.totalSizeCach = offset;
            }
            return this.totalSizeCach;
        }

        public void GetSize( int characterToMeasureStartIndexInclusive, int characterToMeasureEndIndexInclusive, out Vector2 offset )
        {
            offset.x = 0.0;
            offset.y = this.typeFaceStyle.EmSizeInPixels;
            double num = 0.0;
            for ( int index = characterToMeasureStartIndexInclusive ; index < characterToMeasureEndIndexInclusive ; ++index )
            {
                if ( this.text[ index ] == '\n' )
                {
                    if ( index + 1 < characterToMeasureEndIndexInclusive && this.text[ index + 1 ] == '\n' && ( int ) this.text[ index ] != ( int ) this.text[ index + 1 ] )
                        ++index;
                    num = 0.0;
                    offset.y += this.typeFaceStyle.EmSizeInPixels;
                }
                else
                {
                    num += this.typeFaceStyle.GetAdvanceForCharacter( this.text[ index ], this.text[ index + 1 ] );
                    if ( num > offset.x )
                        offset.x = num;
                }
            }
            if ( this.text.Length <= characterToMeasureEndIndexInclusive )
                return;
            offset.x += this.typeFaceStyle.GetAdvanceForCharacter( this.text[ characterToMeasureEndIndexInclusive ] );
        }

        public int NumLines()
        {
            return this.NumLines( 0, this.text.Length - 1 );
        }

        public int NumLines( int characterToMeasureStartIndexInclusive, int characterToMeasureEndIndexInclusive )
        {
            int num = 1;
            characterToMeasureStartIndexInclusive = Math.Max( 0, Math.Min( characterToMeasureStartIndexInclusive, this.text.Length - 1 ) );
            characterToMeasureEndIndexInclusive = Math.Max( 0, Math.Min( characterToMeasureEndIndexInclusive, this.text.Length - 1 ) );
            for ( int index = characterToMeasureStartIndexInclusive ; index < characterToMeasureEndIndexInclusive ; ++index )
            {
                if ( this.text[ index ] == '\n' )
                {
                    if ( index + 1 < characterToMeasureEndIndexInclusive && this.text[ index + 1 ] == '\n' && ( int ) this.text[ index ] != ( int ) this.text[ index + 1 ] )
                        ++index;
                    ++num;
                }
            }
            return num;
        }

        public void GetOffset( int characterToMeasureStartIndexInclusive, int characterToMeasureEndIndexInclusive, out Vector2 offset )
        {
            offset = Vector2.Zero;
            characterToMeasureEndIndexInclusive = Math.Min( this.text.Length - 1, characterToMeasureEndIndexInclusive );
            for ( int index = characterToMeasureStartIndexInclusive ; index <= characterToMeasureEndIndexInclusive ; ++index )
            {
                if ( this.text[ index ] == '\n' )
                {
                    offset.x = 0.0;
                    offset.y -= this.typeFaceStyle.EmSizeInPixels;
                }
                else if ( index < this.text.Length - 1 )
                    offset.x += this.typeFaceStyle.GetAdvanceForCharacter( this.text[ index ], this.text[ index + 1 ] );
                else
                    offset.x += this.typeFaceStyle.GetAdvanceForCharacter( this.text[ index ] );
            }
        }

        public Vector2 GetOffsetLeftOfCharacterIndex( int characterIndex )
        {
            Vector2 offset;
            this.GetOffset( 0, characterIndex - 1, out offset );
            return offset;
        }

        public int GetCharacterIndexToStartBefore( Vector2 position )
        {
            int clostestIndex = -1;
            double maxValue1 = double.MaxValue;
            double maxValue2 = double.MaxValue;
            Vector2 offset1 = new Vector2(0.0, this.typeFaceStyle.EmSizeInPixels * (double) this.NumLines());
            int val1_1 = 0;
            int val1_2 = this.text.Length - 1;
            if ( this.text.Length > 0 )
            {
                int num1 = Math.Max(0, Math.Min(val1_1, this.text.Length - 1));
                int num2 = Math.Max(0, Math.Min(val1_2, this.text.Length - 1));
                for ( int index = num1 ; index <= num2 ; ++index )
                {
                    StringPrinter.CheckForBetterClickPosition( ref position, ref clostestIndex, ref maxValue1, ref maxValue2, ref offset1, index );
                    if ( this.text[ index ] == '\r' )
                        throw new Exception( "All \\r's should have been converted to \\n's." );
                    if ( this.text[ index ] == '\n' )
                    {
                        offset1.x = 0.0;
                        offset1.y -= this.typeFaceStyle.EmSizeInPixels;
                    }
                    else
                    {
                        Vector2 offset2;
                        this.GetOffset( index, index, out offset2 );
                        offset1.x += offset2.x;
                    }
                }
                StringPrinter.CheckForBetterClickPosition( ref position, ref clostestIndex, ref maxValue1, ref maxValue2, ref offset1, num2 + 1 );
            }
            return clostestIndex;
        }

        private static void CheckForBetterClickPosition( ref Vector2 position, ref int clostestIndex, ref double clostestXDistSquared, ref double clostestYDistSquared, ref Vector2 offset, int i )
        {
            Vector2 vector2 = position - offset;
            double num1 = vector2.y * vector2.y;
            if ( num1 < clostestYDistSquared )
            {
                clostestYDistSquared = num1;
                clostestXDistSquared = vector2.x * vector2.x;
                clostestIndex = i;
            }
            else
            {
                if ( num1 != clostestYDistSquared )
                    return;
                double num2 = vector2.x * vector2.x;
                if ( num2 >= clostestXDistSquared )
                    return;
                clostestXDistSquared = num2;
                clostestIndex = i;
            }
        }
    }
}
