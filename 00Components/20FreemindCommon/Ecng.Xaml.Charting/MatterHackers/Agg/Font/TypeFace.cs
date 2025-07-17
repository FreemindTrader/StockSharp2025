//// Decompiled with JetBrains decompiler
//// Type: MatterHackers.Agg.Font.TypeFace
//// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
//// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
//// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

//using MatterHackers.Agg.Transform;
//using MatterHackers.Agg.VertexSource;
//using MatterHackers.VectorMath;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Text.RegularExpressions;

//namespace MatterHackers.Agg.Font
//{
//  internal class TypeFace
//  {
//    private static Regex numberRegex = new Regex("[-+]?[0-9]*\\.?[0-9]+");
//    private Dictionary<int, TypeFace.Glyph> glyphs = new Dictionary<int, TypeFace.Glyph>();
//    private Dictionary<char, Dictionary<char, int>> HKerns = new Dictionary<char, Dictionary<char, int>>();
//    private string fontId;
//    private int horiz_adv_x;
//    private string fontFamily;
//    private int font_weight;
//    private string font_stretch;
//    private int unitsPerEm;
//    private TypeFace.Panos_1 panose_1;
//    private int ascent;
//    private int descent;
//    private int x_height;
//    private int cap_height;
//    private RectangleInt boundingBox;
//    private int underline_thickness;
//    private int underline_position;
//    private string unicode_range;
//    private TypeFace.Glyph missingGlyph;

//    public int Ascent
//    {
//      get
//      {
//        return this.ascent;
//      }
//    }

//    public int Descent
//    {
//      get
//      {
//        return this.descent;
//      }
//    }

//    public int X_height
//    {
//      get
//      {
//        return this.x_height;
//      }
//    }

//    public int Cap_height
//    {
//      get
//      {
//        return this.cap_height;
//      }
//    }

//    public RectangleInt BoundingBox
//    {
//      get
//      {
//        return this.boundingBox;
//      }
//    }

//    public int Underline_thickness
//    {
//      get
//      {
//        return this.underline_thickness;
//      }
//    }

//    public int Underline_position
//    {
//      get
//      {
//        return this.underline_position;
//      }
//    }

//    public int UnitsPerEm
//    {
//      get
//      {
//        return this.unitsPerEm;
//      }
//    }

//    private static string GetSubString(string source, string start, string end)
//    {
//      int startIndex = 0;
//      return TypeFace.GetSubString(source, start, end, ref startIndex);
//    }

//    private static string GetSubString(string source, string start, string end, ref int startIndex)
//    {
//      int num1 = source.IndexOf(start, startIndex);
//      if (num1 < 0)
//        return (string) null;
//      int num2 = source.IndexOf(end, num1 + start.Length);
//      int length = num2 - (num1 + start.Length);
//      startIndex = num2 + end.Length;
//      return source.Substring(num1 + start.Length, length);
//    }

//    private static string GetStringValue(string source, string name)
//    {
//      return TypeFace.GetSubString(source, name + "=\"", "\"");
//    }

//    private static bool GetIntValue(string source, string name, out int outValue, ref int startIndex)
//    {
//      return int.TryParse(TypeFace.GetSubString(source, name + "=\"", "\"", ref startIndex), NumberStyles.Number, (IFormatProvider) null, out outValue);
//    }

//    private static bool GetIntValue(string source, string name, out int outValue)
//    {
//      int startIndex = 0;
//      return TypeFace.GetIntValue(source, name, out outValue, ref startIndex);
//    }

//    public static TypeFace LoadSVG(string filename)
//    {
//      TypeFace typeFace = new TypeFace();
//      string end = new StreamReader(filename).ReadToEnd();
//      typeFace.ReadSVG(end);
//      return typeFace;
//    }



//    private TypeFace.Glyph CreateGlyphFromSVGGlyphData(string SVGGlyphData)
//    {
//      TypeFace.Glyph glyph = new TypeFace.Glyph();
//      if (!TypeFace.GetIntValue(SVGGlyphData, "horiz-adv-x", out glyph.horiz_adv_x))
//        glyph.horiz_adv_x = this.horiz_adv_x;
//      glyph.glyphName = TypeFace.GetStringValue(SVGGlyphData, "glyph-name");
//      string stringValue1 = TypeFace.GetStringValue(SVGGlyphData, "unicode");
//      if (stringValue1 != null)
//      {
//        if (stringValue1.Length == 1)
//        {
//          glyph.unicode = (int) stringValue1[0];
//        }
//        else
//        {
//          if (stringValue1.Split(';').Length > 1)
//          {
//            if (stringValue1.Split(';')[1].Length > 0)
//              throw new NotImplementedException("We do not currently support glyphs longer than one character.  You need to wirite the seach so that it will find them if you want to support this");
//          }
//          if (!int.TryParse(stringValue1, NumberStyles.Number, (IFormatProvider) null, out glyph.unicode))
//            int.TryParse(TypeFace.GetSubString(stringValue1, "&#x", ";"), NumberStyles.HexNumber, (IFormatProvider) null, out glyph.unicode);
//        }
//      }
//      string stringValue2 = TypeFace.GetStringValue(SVGGlyphData, "d");
//      int startIndex = 0;
//      Vector2 vector2_1 = new Vector2(0.0, 0.0);
//      Vector2 vector2_2 = new Vector2(0.0, 0.0);
//      if (stringValue2 == null || stringValue2.Length == 0)
//        return glyph;
//      char ch1 = char.MinValue;
//      while (startIndex < stringValue2.Length)
//      {
//        char ch2 = stringValue2[startIndex];
//        Vector2 vector2_3;
//        switch (ch2)
//        {
//          case '\n':
//          case '\r':
//          case ' ':
//            ++startIndex;
//            vector2_3 = vector2_2;
//            break;
//          case 'H':
//          case 'h':
//            ++startIndex;
//            vector2_3.y = vector2_2.y;
//            vector2_3.x = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            if (ch2 == 'h')
//              vector2_3.x += vector2_2.x;
//            glyph.glyphData.HorizontalLineTo(vector2_3.x);
//            break;
//          case 'L':
//          case 'l':
//            ++startIndex;
//            vector2_3.x = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            vector2_3.y = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            if (ch2 == 'l')
//              vector2_3 += vector2_2;
//            glyph.glyphData.LineTo(vector2_3.x, vector2_3.y);
//            break;
//          case 'M':
//            ++startIndex;
//            vector2_3.x = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            vector2_3.y = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            glyph.glyphData.MoveTo(vector2_3.x, vector2_3.y);
//            vector2_1 = vector2_3;
//            break;
//          case 'Q':
//          case 'q':
//            ++startIndex;
//            Vector2 vector2_4;
//            vector2_4.x = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            vector2_4.y = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            vector2_3.x = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            vector2_3.y = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            if (ch2 == 'q')
//            {
//              vector2_4 += vector2_2;
//              vector2_3 += vector2_2;
//            }
//            glyph.glyphData.curve3(vector2_4.x, vector2_4.y, vector2_3.x, vector2_3.y);
//            break;
//          case 'T':
//          case 't':
//            ++startIndex;
//            vector2_3.x = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            vector2_3.y = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            if (ch2 == 't')
//              vector2_3 += vector2_2;
//            glyph.glyphData.curve3(vector2_3.x, vector2_3.y);
//            break;
//          case 'V':
//          case 'v':
//            ++startIndex;
//            vector2_3.x = vector2_2.x;
//            vector2_3.y = TypeFace.GetNextNumber(stringValue2, ref startIndex);
//            if (ch2 == 'v')
//              vector2_3.y += vector2_2.y;
//            glyph.glyphData.VerticalLineTo(vector2_3.y);
//            break;
//          case 'Z':
//          case 'z':
//            ++startIndex;
//            vector2_3 = vector2_2;
//            glyph.glyphData.ClosePolygon();
//            break;
//          default:
//            throw new NotImplementedException("unrecognized d command '" + ch2.ToString() + "'.");
//        }
//        ch1 = ch2;
//        vector2_2 = vector2_3;
//      }
//      return glyph;
//    }

//    public void ReadSVG(string svgContent)
//    {
//      int startIndex = 0;
//      string subString1 = TypeFace.GetSubString(svgContent, "<font", ">", ref startIndex);
//      this.fontId = TypeFace.GetStringValue(subString1, "id");
//      TypeFace.GetIntValue(subString1, "horiz-adv-x", out this.horiz_adv_x);
//      string subString2 = TypeFace.GetSubString(svgContent, "<font-face", "/>", ref startIndex);
//      this.fontFamily = TypeFace.GetStringValue(subString2, "font-family");
//      TypeFace.GetIntValue(subString2, "font-weight", out this.font_weight);
//      this.font_stretch = TypeFace.GetStringValue(subString2, "font-stretch");
//      TypeFace.GetIntValue(subString2, "units-per-em", out this.unitsPerEm);
//      this.panose_1 = new TypeFace.Panos_1(TypeFace.GetStringValue(subString2, "panose-1"));
//      TypeFace.GetIntValue(subString2, "ascent", out this.ascent);
//      TypeFace.GetIntValue(subString2, "descent", out this.descent);
//      TypeFace.GetIntValue(subString2, "x-height", out this.x_height);
//      TypeFace.GetIntValue(subString2, "cap-height", out this.cap_height);
//      string[] strArray = TypeFace.GetStringValue(subString2, "bbox").Split(' ');
//      int.TryParse(strArray[0], out this.boundingBox.Left);
//      int.TryParse(strArray[1], out this.boundingBox.Bottom);
//      int.TryParse(strArray[2], out this.boundingBox.Right);
//      int.TryParse(strArray[3], out this.boundingBox.Top);
//      TypeFace.GetIntValue(subString2, "underline-thickness", out this.underline_thickness);
//      TypeFace.GetIntValue(subString2, "underline-position", out this.underline_position);
//      this.unicode_range = TypeFace.GetStringValue(subString2, "unicode-range");
//      this.missingGlyph = this.CreateGlyphFromSVGGlyphData(TypeFace.GetSubString(svgContent, "<missing-glyph", "/>", ref startIndex));
//      for (string subString3 = TypeFace.GetSubString(svgContent, "<glyph", "/>", ref startIndex); subString3 != null; subString3 = TypeFace.GetSubString(svgContent, "<glyph", "/>", ref startIndex))
//      {
//        TypeFace.Glyph fromSvgGlyphData = this.CreateGlyphFromSVGGlyphData(subString3);
//        if (fromSvgGlyphData.unicode > 0)
//          this.glyphs.Add(fromSvgGlyphData.unicode, fromSvgGlyphData);
//      }
//    }

//    internal IVertexSource GetGlyphForCharacter(char character)
//    {
//      TypeFace.Glyph glyph;
//      if (this.glyphs.TryGetValue((int) character, out glyph))
//        return (IVertexSource) glyph.glyphData;
//      return (IVertexSource) null;
//    }

//    internal double GetAdvanceForCharacter(char character, char nextCharacterToKernWith)
//    {
//      TypeFace.Glyph glyph;
//      if (this.glyphs.TryGetValue((int) character, out glyph))
//        return (double) glyph.horiz_adv_x;
//      return 0.0;
//    }

//    internal double GetAdvanceForCharacter(char character)
//    {
//      TypeFace.Glyph glyph;
//      if (this.glyphs.TryGetValue((int) character, out glyph))
//        return (double) glyph.horiz_adv_x;
//      return 0.0;
//    }

//    public void ShowDebugInfo(Graphics2D graphics2D)
//    {
//      StyledTypeFace typeFaceStyle = new StyledTypeFace(this, 30.0);
//      StringPrinter stringPrinter = new StringPrinter(this.fontFamily + " - 30 point", typeFaceStyle, new Vector2(), Justification.Left, Baseline.Text);
//      RectangleDouble boundingBoxInPixels = typeFaceStyle.BoundingBoxInPixels;
//      double y = 10.0 - boundingBoxInPixels.Left;
//      double x1_1 = y;
//      double num1 = 10.0 - typeFaceStyle.DescentInPixels;
//      int num2 = 50;
//      RGBA_Bytes color1 = new RGBA_Bytes(0, 0, 0);
//      RGBA_Bytes color2 = new RGBA_Bytes(0, 0, 0);
//      RGBA_Bytes color3 = new RGBA_Bytes((int) byte.MaxValue, 0, 0);
//      RGBA_Bytes color4 = new RGBA_Bytes((int) byte.MaxValue, 0, 0);
//      RGBA_Bytes color5 = new RGBA_Bytes(12, 25, 200);
//      RGBA_Bytes color6 = new RGBA_Bytes(12, 25, 200);
//      RGBA_Bytes color7 = new RGBA_Bytes(0, 150, 55);
//      graphics2D.Line(x1_1, num1, x1_1 + (double) num2, num1, color2);
//      graphics2D.Rectangle(x1_1 + boundingBoxInPixels.Left, num1 + boundingBoxInPixels.Bottom, x1_1 + boundingBoxInPixels.Right, num1 + boundingBoxInPixels.Top, color1, 1.0);
//      double x1_2 = x1_1 + typeFaceStyle.BoundingBoxInPixels.Width * 1.5;
//      int num3 = num2 * 3;
//      double ascentInPixels = typeFaceStyle.AscentInPixels;
//      graphics2D.Line(x1_2, num1 + ascentInPixels, x1_2 + (double) num3, num1 + ascentInPixels, color3);
//      double descentInPixels = typeFaceStyle.DescentInPixels;
//      graphics2D.Line(x1_2, num1 + descentInPixels, x1_2 + (double) num3, num1 + descentInPixels, color4);
//      double xheightInPixels = typeFaceStyle.XHeightInPixels;
//      graphics2D.Line(x1_2, num1 + xheightInPixels, x1_2 + (double) num3, num1 + xheightInPixels, color5);
//      double capHeightInPixels = typeFaceStyle.CapHeightInPixels;
//      graphics2D.Line(x1_2, num1 + capHeightInPixels, x1_2 + (double) num3, num1 + capHeightInPixels, color6);
//      double positionInPixels = typeFaceStyle.UnderlinePositionInPixels;
//      graphics2D.Line(x1_2, num1 + positionInPixels, x1_2 + (double) num3, num1 + positionInPixels, color7);
//      Affine affine = Affine.NewIdentity() * Affine.NewTranslation(10.0, y);
//      VertexSourceApplyTransform sourceApplyTransform = new VertexSourceApplyTransform((IVertexSource) stringPrinter, (ITransform) affine);
//      graphics2D.Render((IVertexSource) sourceApplyTransform, RGBA_Bytes.Black);
//      StyledTypeFace styledTypeFace = new StyledTypeFace(this, 12.0);
//      Vector2 position = new Vector2(x1_2 + (double) (num3 / 2), num1 + typeFaceStyle.EmSizeInPixels * 1.5);
//      graphics2D.Render((IVertexSource) new StringPrinter("Descent", 12.0, new Vector2(), Justification.Left, Baseline.Text), position, color4);
//      position.y += styledTypeFace.EmSizeInPixels;
//      graphics2D.Render((IVertexSource) new StringPrinter("Underline", 12.0, new Vector2(), Justification.Left, Baseline.Text), position, color7);
//      position.y += styledTypeFace.EmSizeInPixels;
//      graphics2D.Render((IVertexSource) new StringPrinter("X Height", 12.0, new Vector2(), Justification.Left, Baseline.Text), position, color5);
//      position.y += styledTypeFace.EmSizeInPixels;
//      graphics2D.Render((IVertexSource) new StringPrinter("CapHeight", 12.0, new Vector2(), Justification.Left, Baseline.Text), position, color6);
//      position.y += styledTypeFace.EmSizeInPixels;
//      graphics2D.Render((IVertexSource) new StringPrinter("Ascent", 12.0, new Vector2(), Justification.Left, Baseline.Text), position, color3);
//      position.y += styledTypeFace.EmSizeInPixels;
//      graphics2D.Render((IVertexSource) new StringPrinter("Origin", 12.0, new Vector2(), Justification.Left, Baseline.Text), position, color2);
//      position.y += styledTypeFace.EmSizeInPixels;
//      graphics2D.Render((IVertexSource) new StringPrinter("Bounding Box", 12.0, new Vector2(), Justification.Left, Baseline.Text), position, color1);
//    }

//    private class Glyph
//    {
//      public PathStorage glyphData = new PathStorage();
//      public int horiz_adv_x;
//      public int unicode;
//      public string glyphName;
//    }

//    private class Panos_1
//    {
//      private TypeFace.Panos_1.Family family;
//      private TypeFace.Panos_1.Serif_Style serifStyle;
//      private TypeFace.Panos_1.Weight weight;
//      private TypeFace.Panos_1.Proportion proportion;
//      private TypeFace.Panos_1.Contrast contrast;
//      private TypeFace.Panos_1.Stroke_Variation strokeVariation;
//      private TypeFace.Panos_1.Arm_Style armStyle;
//      private TypeFace.Panos_1.Letterform letterform;
//      private TypeFace.Panos_1.Midline midline;
//      private TypeFace.Panos_1.XHeight xHeight;

//      public Panos_1(string SVGPanos1String)
//      {
//        string[] strArray = SVGPanos1String.Split(' ');
//        int result;
//        if (int.TryParse(strArray[0], out result))
//          this.family = (TypeFace.Panos_1.Family) result;
//        if (int.TryParse(strArray[1], out result))
//          this.serifStyle = (TypeFace.Panos_1.Serif_Style) result;
//        if (int.TryParse(strArray[2], out result))
//          this.weight = (TypeFace.Panos_1.Weight) result;
//        if (int.TryParse(strArray[3], out result))
//          this.proportion = (TypeFace.Panos_1.Proportion) result;
//        if (int.TryParse(strArray[4], out result))
//          this.contrast = (TypeFace.Panos_1.Contrast) result;
//        if (int.TryParse(strArray[5], out result))
//          this.strokeVariation = (TypeFace.Panos_1.Stroke_Variation) result;
//        if (int.TryParse(strArray[6], out result))
//          this.armStyle = (TypeFace.Panos_1.Arm_Style) result;
//        if (int.TryParse(strArray[7], out result))
//          this.letterform = (TypeFace.Panos_1.Letterform) result;
//        if (int.TryParse(strArray[8], out result))
//          this.midline = (TypeFace.Panos_1.Midline) result;
//        if (!int.TryParse(strArray[0], out result))
//          return;
//        this.xHeight = (TypeFace.Panos_1.XHeight) result;
//      }

//      private enum Family
//      {
//        Any,
//        No_Fit,
//        Latin_Text_and_Display,
//        Latin_Script,
//        Latin_Decorative,
//        Latin_Pictorial,
//      }

//      private enum Serif_Style
//      {
//        Any,
//        No_Fit,
//        Cove,
//        Obtuse_Cove,
//        Square_Cove,
//        Obtuse_Square_Cove,
//        Square,
//        Thin,
//        Bone,
//        Exaggerated,
//        Triangle,
//        Normal_Sans,
//        Obtuse_Sans,
//        Perp_Sans,
//        Flared,
//        Rounded,
//      }

//      private enum Weight
//      {
//        Any,
//        No_Fit,
//        Very_Light_100,
//        Light_200,
//        Thin_300,
//        Book_400_same_as_CSS1_normal,
//        Medium_500,
//        Demi_600,
//        Bold_700_same_as_CSS1_bold,
//        Heavy_800,
//        Black_900,
//        Extra_Black_Nord_900_force_mapping_to_CSS1_100_900_scale,
//      }

//      private enum Proportion
//      {
//        Any,
//        No_Fit,
//        Old_Style,
//        Modern,
//        Even_Width,
//        Expanded,
//        Condensed,
//        Very_Expanded,
//        Very_Condensed,
//        Monospaced,
//      }

//      private enum Contrast
//      {
//        Any,
//        No_Fit,
//        None,
//        Very_Low,
//        Low,
//        Medium_Low,
//        Medium,
//        Medium_High,
//        High,
//        Very_High,
//      }

//      private enum Stroke_Variation
//      {
//        Any,
//        No_Fit,
//        No_Variation,
//        Gradual_Diagonal,
//        Gradual_Transitional,
//        Gradual_Vertical,
//        Gradual_Horizontal,
//        Rapid_Vertical,
//        Rapid_Horizontal,
//        Instant_Horizontal,
//        Instant_Vertical,
//      }

//      private enum Arm_Style
//      {
//        Any,
//        No_Fit,
//        Straight_Arms_Horizontal,
//        Straight_Arms_Wedge,
//        Straight_Arms_Vertical,
//        Straight_Arms_Single_Serif,
//        Straight_Arms_Double_Serif,
//        Non_Straight_Arms_Horizontal,
//        Non_Straight_Arms_Wedge,
//        Non_Straight_Arms_Vertical_90,
//        Non_Straight_Arms_Single_Serif,
//        Non_Straight_Arms_Double_Serif,
//      }

//      private enum Letterform
//      {
//        Any,
//        No_Fit,
//        Normal_Contact,
//        Normal_Weighted,
//        Normal_Boxed,
//        Normal_Flattened,
//        Normal_Rounded,
//        Normal_Off_Center,
//        Normal_Square,
//        Oblique_Contact,
//        Oblique_Weighted,
//        Oblique_Boxed,
//        Oblique_Flattened,
//        Oblique_Rounded,
//        Oblique_Off_Center,
//        Oblique_Square,
//      }

//      private enum Midline
//      {
//        Any,
//        No_Fit,
//        Standard_Trimmed,
//        Standard_Pointed,
//        Standard_Serifed,
//        High_Trimmed,
//        High_Pointed,
//        High_Serifed,
//        Constant_Trimmed,
//        Constant_Pointed,
//        Constant_Serifed,
//        Low_Trimmed,
//        Low_Pointed,
//        Low_Serifed,
//      }

//      private enum XHeight
//      {
//        Any,
//        No_Fit,
//        Constant_Small,
//        Constant_Standard,
//        Constant_Large,
//        Ducking_Small,
//        Ducking_Standard,
//        Ducking_Large,
//      }
//    }
//  }
//}

// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Font.TypeFace
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using MatterHackers.Agg.Transform;
using MatterHackers.Agg.VertexSource;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.Font
{
    internal class TypeFace
    {
        private static Regex numberRegex = new Regex("[-+]?[0-9]*\\.?[0-9]+");
        private Dictionary<int, TypeFace.Glyph> glyphs = new Dictionary<int, TypeFace.Glyph>();
        private Dictionary<char, Dictionary<char, int>> HKerns = new Dictionary<char, Dictionary<char, int>>();
        private string fontId;
        private int horiz_adv_x;
        private string fontFamily;
        private int font_weight;
        private string font_stretch;
        private int unitsPerEm;
        private TypeFace.Panos_1 panose_1;
        private int ascent;
        private int descent;
        private int x_height;
        private int cap_height;
        private RectangleInt boundingBox;
        private int underline_thickness;
        private int underline_position;
        private string unicode_range;
        private TypeFace.Glyph missingGlyph;

        public int Ascent
        {
            get
            {
                return this.ascent;
            }
        }

        public int Descent
        {
            get
            {
                return this.descent;
            }
        }

        public int X_height
        {
            get
            {
                return this.x_height;
            }
        }

        public int Cap_height
        {
            get
            {
                return this.cap_height;
            }
        }

        public RectangleInt BoundingBox
        {
            get
            {
                return this.boundingBox;
            }
        }

        public int Underline_thickness
        {
            get
            {
                return this.underline_thickness;
            }
        }

        public int Underline_position
        {
            get
            {
                return this.underline_position;
            }
        }

        public int UnitsPerEm
        {
            get
            {
                return this.unitsPerEm;
            }
        }

        private static string GetSubString( string source, string start, string end )
        {
            int startIndex = 0;
            return TypeFace.GetSubString( source, start, end, ref startIndex );
        }

        private static string GetSubString( string source, string start, string end, ref int startIndex )
        {
            int num1 = source.IndexOf(start, startIndex);
            if ( num1 < 0 )
                return ( string ) null;
            int num2 = source.IndexOf(end, num1 + start.Length);
            int length = num2 - (num1 + start.Length);
            startIndex = num2 + end.Length;
            return source.Substring( num1 + start.Length, length );
        }

        private static string GetStringValue( string source, string name )
        {
            return TypeFace.GetSubString( source, name + "=\"", "\"" );
        }

        private static bool GetIntValue( string source, string name, out int outValue, ref int startIndex )
        {
            return int.TryParse( TypeFace.GetSubString( source, name + "=\"", "\"", ref startIndex ), NumberStyles.Number, ( IFormatProvider ) null, out outValue );
        }

        private static bool GetIntValue( string source, string name, out int outValue )
        {
            int startIndex = 0;
            return TypeFace.GetIntValue( source, name, out outValue, ref startIndex );
        }

        public static TypeFace LoadSVG( string filename )
        {
            TypeFace typeFace = new TypeFace();
            typeFace.ReadSVG( new StreamReader( filename ).ReadToEnd() );
            return typeFace;
        }

        private static double GetNextNumber( string source, ref int startIndex )
        {
            Match match = TypeFace.numberRegex.Match(source, startIndex);
            string s = match.Value;
            startIndex = match.Index + match.Length;
            double result;
            double.TryParse( s, NumberStyles.Number, ( IFormatProvider ) null, out result );
            return result;
        }

        private TypeFace.Glyph CreateGlyphFromSVGGlyphData( string SVGGlyphData )
        {
            TypeFace.Glyph glyph = new TypeFace.Glyph();
            if ( !TypeFace.GetIntValue( SVGGlyphData, "horiz-adv-x", out glyph.horiz_adv_x ) )
                glyph.horiz_adv_x = this.horiz_adv_x;
            glyph.glyphName = TypeFace.GetStringValue( SVGGlyphData, "glyph-name" );
            string stringValue1 = TypeFace.GetStringValue(SVGGlyphData, "unicode");
            if ( stringValue1 != null )
            {
                if ( stringValue1.Length == 1 )
                {
                    glyph.unicode = ( int ) stringValue1[ 0 ];
                }
                else
                {
                    if ( stringValue1.Split( ';' ).Length > 1 )
                    {
                        if ( stringValue1.Split( ';' )[ 1 ].Length > 0 )
                            throw new NotImplementedException( "We do not currently support glyphs longer than one character.  You need to wirite the seach so that it will find them if you want to support this" );
                    }
                    if ( !int.TryParse( stringValue1, NumberStyles.Number, ( IFormatProvider ) null, out glyph.unicode ) )
                        int.TryParse( TypeFace.GetSubString( stringValue1, "&#x", ";" ), NumberStyles.HexNumber, ( IFormatProvider ) null, out glyph.unicode );
                }
            }
            string stringValue2 = TypeFace.GetStringValue(SVGGlyphData, "d");
            int startIndex = 0;
            Vector2 vector2_1 = new Vector2(0.0, 0.0);
            Vector2 vector2_2 = new Vector2(0.0, 0.0);
            if ( stringValue2 == null || stringValue2.Length == 0 )
                return glyph;
            while ( startIndex < stringValue2.Length )
            {
                char ch = stringValue2[startIndex];
                Vector2 vector2_3;
                switch ( ch )
                {
                    case '\n':
                    case '\r':
                    case ' ':
                        ++startIndex;
                        vector2_3 = vector2_2;
                        break;
                    case 'H':
                    case 'h':
                        ++startIndex;
                        vector2_3.y = vector2_2.y;
                        vector2_3.x = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        if ( ch == 'h' )
                            vector2_3.x += vector2_2.x;
                        glyph.glyphData.HorizontalLineTo( vector2_3.x );
                        break;
                    case 'L':
                    case 'l':
                        ++startIndex;
                        vector2_3.x = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        vector2_3.y = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        if ( ch == 'l' )
                            vector2_3 += vector2_2;
                        glyph.glyphData.LineTo( vector2_3.x, vector2_3.y );
                        break;
                    case 'M':
                        ++startIndex;
                        vector2_3.x = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        vector2_3.y = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        glyph.glyphData.MoveTo( vector2_3.x, vector2_3.y );
                        break;
                    case 'Q':
                    case 'q':
                        ++startIndex;
                        Vector2 vector2_4;
                        vector2_4.x = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        vector2_4.y = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        vector2_3.x = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        vector2_3.y = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        if ( ch == 'q' )
                        {
                            vector2_4 += vector2_2;
                            vector2_3 += vector2_2;
                        }
                        glyph.glyphData.curve3( vector2_4.x, vector2_4.y, vector2_3.x, vector2_3.y );
                        break;
                    case 'T':
                    case 't':
                        ++startIndex;
                        vector2_3.x = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        vector2_3.y = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        if ( ch == 't' )
                            vector2_3 += vector2_2;
                        glyph.glyphData.curve3( vector2_3.x, vector2_3.y );
                        break;
                    case 'V':
                    case 'v':
                        ++startIndex;
                        vector2_3.x = vector2_2.x;
                        vector2_3.y = TypeFace.GetNextNumber( stringValue2, ref startIndex );
                        if ( ch == 'v' )
                            vector2_3.y += vector2_2.y;
                        glyph.glyphData.VerticalLineTo( vector2_3.y );
                        break;
                    case 'Z':
                    case 'z':
                        ++startIndex;
                        vector2_3 = vector2_2;
                        glyph.glyphData.ClosePolygon();
                        break;
                    default:
                        throw new NotImplementedException( "unrecognized d command '" + ch.ToString() + "'." );
                }
                vector2_2 = vector2_3;
            }
            return glyph;
        }

        public void ReadSVG( string svgContent )
        {
            int startIndex = 0;
            string subString1 = TypeFace.GetSubString(svgContent, "<font", ">", ref startIndex);
            this.fontId = TypeFace.GetStringValue( subString1, "id" );
            TypeFace.GetIntValue( subString1, "horiz-adv-x", out this.horiz_adv_x );
            string subString2 = TypeFace.GetSubString(svgContent, "<font-face", "/>", ref startIndex);
            this.fontFamily = TypeFace.GetStringValue( subString2, "font-family" );
            TypeFace.GetIntValue( subString2, "font-weight", out this.font_weight );
            this.font_stretch = TypeFace.GetStringValue( subString2, "font-stretch" );
            TypeFace.GetIntValue( subString2, "units-per-em", out this.unitsPerEm );
            this.panose_1 = new TypeFace.Panos_1( TypeFace.GetStringValue( subString2, "panose-1" ) );
            TypeFace.GetIntValue( subString2, "ascent", out this.ascent );
            TypeFace.GetIntValue( subString2, "descent", out this.descent );
            TypeFace.GetIntValue( subString2, "x-height", out this.x_height );
            TypeFace.GetIntValue( subString2, "cap-height", out this.cap_height );
            string[] strArray = TypeFace.GetStringValue(subString2, "bbox").Split(' ');
            int.TryParse( strArray[ 0 ], out this.boundingBox.Left );
            int.TryParse( strArray[ 1 ], out this.boundingBox.Bottom );
            int.TryParse( strArray[ 2 ], out this.boundingBox.Right );
            int.TryParse( strArray[ 3 ], out this.boundingBox.Top );
            TypeFace.GetIntValue( subString2, "underline-thickness", out this.underline_thickness );
            TypeFace.GetIntValue( subString2, "underline-position", out this.underline_position );
            this.unicode_range = TypeFace.GetStringValue( subString2, "unicode-range" );
            this.missingGlyph = this.CreateGlyphFromSVGGlyphData( TypeFace.GetSubString( svgContent, "<missing-glyph", "/>", ref startIndex ) );
            for ( string subString3 = TypeFace.GetSubString( svgContent, "<glyph", "/>", ref startIndex ) ; subString3 != null ; subString3 = TypeFace.GetSubString( svgContent, "<glyph", "/>", ref startIndex ) )
            {
                TypeFace.Glyph fromSvgGlyphData = this.CreateGlyphFromSVGGlyphData(subString3);
                if ( fromSvgGlyphData.unicode > 0 )
                    this.glyphs.Add( fromSvgGlyphData.unicode, fromSvgGlyphData );
            }
        }

        internal IVertexSource GetGlyphForCharacter( char character )
        {
            TypeFace.Glyph glyph;
            if ( this.glyphs.TryGetValue( ( int ) character, out glyph ) )
                return ( IVertexSource ) glyph.glyphData;
            return ( IVertexSource ) null;
        }

        internal double GetAdvanceForCharacter( char character, char nextCharacterToKernWith )
        {
            TypeFace.Glyph glyph;
            if ( this.glyphs.TryGetValue( ( int ) character, out glyph ) )
                return ( double ) glyph.horiz_adv_x;
            return 0.0;
        }

        internal double GetAdvanceForCharacter( char character )
        {
            TypeFace.Glyph glyph;
            if ( this.glyphs.TryGetValue( ( int ) character, out glyph ) )
                return ( double ) glyph.horiz_adv_x;
            return 0.0;
        }

        public void ShowDebugInfo( Graphics2D graphics2D )
        {
            StyledTypeFace typeFaceStyle = new StyledTypeFace(this, 30.0);
            StringPrinter stringPrinter = new StringPrinter(this.fontFamily + " - 30 point", typeFaceStyle, new Vector2(), Justification.Left, Baseline.Text);
            RectangleDouble boundingBoxInPixels = typeFaceStyle.BoundingBoxInPixels;
            double y = 10.0 - boundingBoxInPixels.Left;
            double x1_1 = y;
            double num1 = 10.0 - typeFaceStyle.DescentInPixels;
            int num2 = 50;
            RGBA_Bytes color1 = new RGBA_Bytes(0, 0, 0);
            RGBA_Bytes color2 = new RGBA_Bytes(0, 0, 0);
            RGBA_Bytes color3 = new RGBA_Bytes((int) byte.MaxValue, 0, 0);
            RGBA_Bytes color4 = new RGBA_Bytes((int) byte.MaxValue, 0, 0);
            RGBA_Bytes color5 = new RGBA_Bytes(12, 25, 200);
            RGBA_Bytes color6 = new RGBA_Bytes(12, 25, 200);
            RGBA_Bytes color7 = new RGBA_Bytes(0, 150, 55);
            graphics2D.Line( x1_1, num1, x1_1 + ( double ) num2, num1, color2 );
            graphics2D.Rectangle( x1_1 + boundingBoxInPixels.Left, num1 + boundingBoxInPixels.Bottom, x1_1 + boundingBoxInPixels.Right, num1 + boundingBoxInPixels.Top, color1, 1.0 );
            double x1_2 = x1_1 + typeFaceStyle.BoundingBoxInPixels.Width * 1.5;
            int num3 = num2 * 3;
            double ascentInPixels = typeFaceStyle.AscentInPixels;
            graphics2D.Line( x1_2, num1 + ascentInPixels, x1_2 + ( double ) num3, num1 + ascentInPixels, color3 );
            double descentInPixels = typeFaceStyle.DescentInPixels;
            graphics2D.Line( x1_2, num1 + descentInPixels, x1_2 + ( double ) num3, num1 + descentInPixels, color4 );
            double xheightInPixels = typeFaceStyle.XHeightInPixels;
            graphics2D.Line( x1_2, num1 + xheightInPixels, x1_2 + ( double ) num3, num1 + xheightInPixels, color5 );
            double capHeightInPixels = typeFaceStyle.CapHeightInPixels;
            graphics2D.Line( x1_2, num1 + capHeightInPixels, x1_2 + ( double ) num3, num1 + capHeightInPixels, color6 );
            double positionInPixels = typeFaceStyle.UnderlinePositionInPixels;
            graphics2D.Line( x1_2, num1 + positionInPixels, x1_2 + ( double ) num3, num1 + positionInPixels, color7 );
            // ISSUE: variable of a boxed type
            var local = (ValueType) (Affine.NewIdentity() * Affine.NewTranslation(10.0, y));
            VertexSourceApplyTransform sourceApplyTransform = new VertexSourceApplyTransform((IVertexSource) stringPrinter, (ITransform) local);
            graphics2D.Render( ( IVertexSource ) sourceApplyTransform, RGBA_Bytes.Black );
            StyledTypeFace styledTypeFace = new StyledTypeFace(this, 12.0);
            Vector2 position = new Vector2(x1_2 + (double) (num3 / 2), num1 + typeFaceStyle.EmSizeInPixels * 1.5);
            graphics2D.Render( ( IVertexSource ) new StringPrinter( "Descent", 12.0, new Vector2(), Justification.Left, Baseline.Text ), position, color4 );
            position.y += styledTypeFace.EmSizeInPixels;
            graphics2D.Render( ( IVertexSource ) new StringPrinter( "Underline", 12.0, new Vector2(), Justification.Left, Baseline.Text ), position, color7 );
            position.y += styledTypeFace.EmSizeInPixels;
            graphics2D.Render( ( IVertexSource ) new StringPrinter( "X Height", 12.0, new Vector2(), Justification.Left, Baseline.Text ), position, color5 );
            position.y += styledTypeFace.EmSizeInPixels;
            graphics2D.Render( ( IVertexSource ) new StringPrinter( "CapHeight", 12.0, new Vector2(), Justification.Left, Baseline.Text ), position, color6 );
            position.y += styledTypeFace.EmSizeInPixels;
            graphics2D.Render( ( IVertexSource ) new StringPrinter( "Ascent", 12.0, new Vector2(), Justification.Left, Baseline.Text ), position, color3 );
            position.y += styledTypeFace.EmSizeInPixels;
            graphics2D.Render( ( IVertexSource ) new StringPrinter( "Origin", 12.0, new Vector2(), Justification.Left, Baseline.Text ), position, color2 );
            position.y += styledTypeFace.EmSizeInPixels;
            graphics2D.Render( ( IVertexSource ) new StringPrinter( "Bounding Box", 12.0, new Vector2(), Justification.Left, Baseline.Text ), position, color1 );
        }

        private class Glyph
        {
            public PathStorage glyphData = new PathStorage();
            public int horiz_adv_x;
            public int unicode;
            public string glyphName;
        }

        private class Panos_1
        {
            private TypeFace.Panos_1.Family family;
            private TypeFace.Panos_1.Serif_Style serifStyle;
            private TypeFace.Panos_1.Weight weight;
            private TypeFace.Panos_1.Proportion proportion;
            private TypeFace.Panos_1.Contrast contrast;
            private TypeFace.Panos_1.Stroke_Variation strokeVariation;
            private TypeFace.Panos_1.Arm_Style armStyle;
            private TypeFace.Panos_1.Letterform letterform;
            private TypeFace.Panos_1.Midline midline;
            private TypeFace.Panos_1.XHeight xHeight;

            public Panos_1( string SVGPanos1String )
            {
                string[] strArray = SVGPanos1String.Split(' ');
                int result;
                if ( int.TryParse( strArray[ 0 ], out result ) )
                    this.family = ( TypeFace.Panos_1.Family ) result;
                if ( int.TryParse( strArray[ 1 ], out result ) )
                    this.serifStyle = ( TypeFace.Panos_1.Serif_Style ) result;
                if ( int.TryParse( strArray[ 2 ], out result ) )
                    this.weight = ( TypeFace.Panos_1.Weight ) result;
                if ( int.TryParse( strArray[ 3 ], out result ) )
                    this.proportion = ( TypeFace.Panos_1.Proportion ) result;
                if ( int.TryParse( strArray[ 4 ], out result ) )
                    this.contrast = ( TypeFace.Panos_1.Contrast ) result;
                if ( int.TryParse( strArray[ 5 ], out result ) )
                    this.strokeVariation = ( TypeFace.Panos_1.Stroke_Variation ) result;
                if ( int.TryParse( strArray[ 6 ], out result ) )
                    this.armStyle = ( TypeFace.Panos_1.Arm_Style ) result;
                if ( int.TryParse( strArray[ 7 ], out result ) )
                    this.letterform = ( TypeFace.Panos_1.Letterform ) result;
                if ( int.TryParse( strArray[ 8 ], out result ) )
                    this.midline = ( TypeFace.Panos_1.Midline ) result;
                if ( !int.TryParse( strArray[ 0 ], out result ) )
                    return;
                this.xHeight = ( TypeFace.Panos_1.XHeight ) result;
            }

            private enum Family
            {
                Any,
                No_Fit,
                Latin_Text_and_Display,
                Latin_Script,
                Latin_Decorative,
                Latin_Pictorial,
            }

            private enum Serif_Style
            {
                Any,
                No_Fit,
                Cove,
                Obtuse_Cove,
                Square_Cove,
                Obtuse_Square_Cove,
                Square,
                Thin,
                Bone,
                Exaggerated,
                Triangle,
                Normal_Sans,
                Obtuse_Sans,
                Perp_Sans,
                Flared,
                Rounded,
            }

            private enum Weight
            {
                Any,
                No_Fit,
                Very_Light_100,
                Light_200,
                Thin_300,
                Book_400_same_as_CSS1_normal,
                Medium_500,
                Demi_600,
                Bold_700_same_as_CSS1_bold,
                Heavy_800,
                Black_900,
                Extra_Black_Nord_900_force_mapping_to_CSS1_100_900_scale,
            }

            private enum Proportion
            {
                Any,
                No_Fit,
                Old_Style,
                Modern,
                Even_Width,
                Expanded,
                Condensed,
                Very_Expanded,
                Very_Condensed,
                Monospaced,
            }

            private enum Contrast
            {
                Any,
                No_Fit,
                None,
                Very_Low,
                Low,
                Medium_Low,
                Medium,
                Medium_High,
                High,
                Very_High,
            }

            private enum Stroke_Variation
            {
                Any,
                No_Fit,
                No_Variation,
                Gradual_Diagonal,
                Gradual_Transitional,
                Gradual_Vertical,
                Gradual_Horizontal,
                Rapid_Vertical,
                Rapid_Horizontal,
                Instant_Horizontal,
                Instant_Vertical,
            }

            private enum Arm_Style
            {
                Any,
                No_Fit,
                Straight_Arms_Horizontal,
                Straight_Arms_Wedge,
                Straight_Arms_Vertical,
                Straight_Arms_Single_Serif,
                Straight_Arms_Double_Serif,
                Non_Straight_Arms_Horizontal,
                Non_Straight_Arms_Wedge,
                Non_Straight_Arms_Vertical_90,
                Non_Straight_Arms_Single_Serif,
                Non_Straight_Arms_Double_Serif,
            }

            private enum Letterform
            {
                Any,
                No_Fit,
                Normal_Contact,
                Normal_Weighted,
                Normal_Boxed,
                Normal_Flattened,
                Normal_Rounded,
                Normal_Off_Center,
                Normal_Square,
                Oblique_Contact,
                Oblique_Weighted,
                Oblique_Boxed,
                Oblique_Flattened,
                Oblique_Rounded,
                Oblique_Off_Center,
                Oblique_Square,
            }

            private enum Midline
            {
                Any,
                No_Fit,
                Standard_Trimmed,
                Standard_Pointed,
                Standard_Serifed,
                High_Trimmed,
                High_Pointed,
                High_Serifed,
                Constant_Trimmed,
                Constant_Pointed,
                Constant_Serifed,
                Low_Trimmed,
                Low_Pointed,
                Low_Serifed,
            }

            private enum XHeight
            {
                Any,
                No_Fit,
                Constant_Small,
                Constant_Standard,
                Constant_Large,
                Ducking_Small,
                Ducking_Standard,
                Ducking_Large,
            }
        }
    }
}

