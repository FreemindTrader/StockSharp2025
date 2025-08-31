using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using SciChart.Drawing.Common;
using System.Windows.Controls;

namespace StockSharp.Xaml.Charting;

public static class TimeSeriesHelper2025
{
    internal static Dictionary<Tuple<string, float, FontWeight>, Size> _fontSizeMap = new Dictionary<Tuple<string, float, FontWeight>, Size>();
    internal static Dictionary<CharSpriteKey, ISprite2D> _fontCache = new Dictionary<CharSpriteKey, ISprite2D>();
    private static readonly Dictionary<string, FontFamily> _fonts = new Dictionary<string, FontFamily>();
    internal class CharSpriteKey : IEquatable<CharSpriteKey>
    {
        public Color ForeColor
        {
            get; set;
        }

        public char Character
        {
            get; set;
        }

        public string FontFamily
        {
            get; set;
        }

        public FontWeight FontWeight
        {
            get; set;
        }

        public float FontSize
        {
            get; set;
        }

        public override int GetHashCode()
        {
            return ForeColor.GetHashCode() ^ Character.GetHashCode() ^ FontFamily.GetHashCode() ^ FontWeight.GetHashCode() ^ FontSize.GetHashCode();
        }

        public bool Equals(CharSpriteKey other)
        {
            if ( other == null || (int)other.Character != (int)Character || ( !( other.ForeColor == ForeColor ) || !( other.FontFamily == FontFamily ) ) || !other.FontWeight.Equals(FontWeight) )
                return false;
            return (double)other.FontSize == (double)FontSize;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CharSpriteKey);
        }
    }

    /// <summary>
    /// We use the renderContext to draw a textblock with the digit character and measure its size.
    /// 
    /// This function will get the maximum width and height for digits from 0-9 and return it as a Size object.
    /// </summary>
    /// <param name="renderContext"></param>
    /// <param name="fontSize"></param>
    /// <param name="fontFamily"></param>
    /// <param name="fontWeight"></param>
    /// <returns></returns>
    public static Size DigitMaxSize( this IRenderContext2D renderContext, float fontSize, string fontFamily = null, FontWeight fontWeight = default(FontWeight))
    {
        if ( fontFamily == null )
        {
            fontFamily = "Tahoma";
        }
            
        if ( fontWeight == new FontWeight() )
        {
            fontWeight = FontWeights.Regular;
        }
            
        fontSize   = fontSize.Round(0.5f);
        var myFont = Tuple.Create( fontFamily, fontSize, fontWeight );

        Size size;
        if ( _fontSizeMap.TryGetValue(myFont, out size) )
            return size;

        double width  = double.MinValue;
        double height = double.MinValue;

        var digits = new char[10]
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9'
        };

        foreach ( ISprite2D s in digits.Select( d => TimeSeriesHelper2025.GetCharSprite(renderContext, d, fontFamily, fontSize, fontWeight, Colors.White) )) 
        {
            if ( (double)s.Width > width )
                width = (double)s.Width;

            if ( (double)s.Height > height )
                height = (double)s.Height;
        }

        size = new Size(width, height);
        _fontSizeMap[myFont] = size;

        return size;
    }

    private static ISprite2D GetCharSprite(this IRenderContext2D renderContext, char character, string fontFamily, float fontSize, FontWeight fontWeight, Color color)
    {
        CharSpriteKey key = new CharSpriteKey()
        {
            Character  = character,
            ForeColor  = color,
            FontFamily = fontFamily,
            FontWeight = fontWeight,
            FontSize   = fontSize
        };

        ISprite2D sprite;
        
        if ( !_fontCache.TryGetValue(key, out sprite) )
        {
            TextBlock txt  = new TextBlock();
            txt.Text       = new string(character, 1);
            txt.Foreground = (Brush)new SolidColorBrush(color);
            txt.FontFamily = GetFontByName(fontFamily);
            txt.FontSize   = (double)fontSize;
            txt.FontWeight = fontWeight;
            txt.Margin     = new Thickness(0.0);
            sprite         = renderContext.CreateSprite((FrameworkElement)txt);

            _fontCache.Add(key, sprite);
        }
        return sprite;
    }

    private static FontFamily GetFontByName(string fontName)
    {        
        if ( _fonts.TryGetValue(fontName, out var ff) )
            return ff;

        var fontFamily = new FontFamily(fontName);
        _fonts.Add(fontName, fontFamily);

        return fontFamily;
    }

}

