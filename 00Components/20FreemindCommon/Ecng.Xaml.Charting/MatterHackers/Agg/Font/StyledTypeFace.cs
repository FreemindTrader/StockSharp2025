// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Font.StyledTypeFace
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using MatterHackers.Agg.Image;
using MatterHackers.Agg.Transform;
using MatterHackers.Agg.VertexSource;

namespace MatterHackers.Agg.Font
{
    internal class StyledTypeFace
    {
        private bool flatenCurves = true;
        private TypeFace typeFace;
        private const int PointsPerInch = 72;
        private const int PixelsPerInch = 96;
        private double emSizeInPixels;
        private double currentEmScalling;

        public StyledTypeFace( TypeFace typeFace, double emSizeInPoints )
        {
            this.typeFace = typeFace;
            this.emSizeInPixels = emSizeInPoints / 72.0 * 96.0;
            this.currentEmScalling = this.emSizeInPixels / ( double ) typeFace.UnitsPerEm;
        }

        public bool DoUnderline
        {
            get; set;
        }

        public bool FlatenCurves
        {
            get
            {
                return this.flatenCurves;
            }
            set
            {
                this.flatenCurves = value;
            }
        }

        public double EmSizeInPixels
        {
            get
            {
                return this.emSizeInPixels;
            }
        }

        public double EmSizeInPoints
        {
            get
            {
                return this.emSizeInPixels / 96.0 * 72.0;
            }
        }

        public double AscentInPixels
        {
            get
            {
                return ( double ) this.typeFace.Ascent * this.currentEmScalling;
            }
        }

        public double DescentInPixels
        {
            get
            {
                return ( double ) this.typeFace.Descent * this.currentEmScalling;
            }
        }

        public double XHeightInPixels
        {
            get
            {
                return ( double ) this.typeFace.X_height * this.currentEmScalling;
            }
        }

        public double CapHeightInPixels
        {
            get
            {
                return ( double ) this.typeFace.Cap_height * this.currentEmScalling;
            }
        }

        public RectangleDouble BoundingBoxInPixels
        {
            get
            {
                return new RectangleDouble( this.typeFace.BoundingBox ) * this.currentEmScalling;
            }
        }

        public double UnderlineThicknessInPixels
        {
            get
            {
                return ( double ) this.typeFace.Underline_thickness * this.currentEmScalling;
            }
        }

        public double UnderlinePositionInPixels
        {
            get
            {
                return ( double ) this.typeFace.Underline_position * this.currentEmScalling;
            }
        }

        public ImageBuffer GetImageForCharacter( char character, double xFraction, double yFraction )
        {
            if ( xFraction > 1.0 || xFraction < 0.0 || ( yFraction > 1.0 || yFraction < 0.0 ) )
                throw new ArgumentException( "The x and y fractions must both be between 0 and 1." );
            Dictionary<char, ImageBuffer> correctCache = StyledTypeFaceImageCache.GetCorrectCache(this.typeFace, this.emSizeInPixels);
            ImageBuffer imageBuffer1;
            correctCache.TryGetValue( character, out imageBuffer1 );
            if ( imageBuffer1 != ( ImageBuffer ) null )
                return imageBuffer1;
            IVertexSource glyphForCharacter = this.GetGlyphForCharacter(character);
            if ( glyphForCharacter == null )
                return ( ImageBuffer ) null;
            glyphForCharacter.rewind( 0 );
            double x;
            double y;
            Path.FlagsAndCommand flagsAndCommand = glyphForCharacter.vertex(out x, out y);
            RectangleDouble rectangleDouble = new RectangleDouble(x, y, x, y);
            for ( ; flagsAndCommand != Path.FlagsAndCommand.CommandStop ; flagsAndCommand = glyphForCharacter.vertex( out x, out y ) )
                rectangleDouble.ExpandToInclude( x, y );
            ImageBuffer imageBuffer2 = new ImageBuffer((int) rectangleDouble.Width, (int) rectangleDouble.Height, 32, (IBlenderByte) new BlenderBGRA());
            imageBuffer2.NewGraphics2D().Render( glyphForCharacter, xFraction, yFraction, RGBA_Bytes.Black );
            correctCache[ character ] = imageBuffer2;
            return imageBuffer2;
        }

        public IVertexSource GetGlyphForCharacter( char character )
        {
            IVertexSource glyphForCharacter = this.typeFace.GetGlyphForCharacter(character);
            if ( glyphForCharacter == null )
                return ( IVertexSource ) null;
            Affine affine = Affine.NewIdentity() * Affine.NewScaling(this.currentEmScalling);
            IVertexSource source = (IVertexSource) new VertexSourceApplyTransform(glyphForCharacter, (ITransform) affine);
            if ( this.flatenCurves )
                source = ( IVertexSource ) new FlattenCurves( source );
            return source;
        }

        public double GetAdvanceForCharacter( char character, char nextCharacterToKernWith )
        {
            return this.typeFace.GetAdvanceForCharacter( character, nextCharacterToKernWith ) * this.currentEmScalling;
        }

        public double GetAdvanceForCharacter( char character )
        {
            return this.typeFace.GetAdvanceForCharacter( character ) * this.currentEmScalling;
        }
    }
}
