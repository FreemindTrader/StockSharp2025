// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.CharSpriteKey
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;

namespace Ecng.Xaml.Charting
{
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
            return this.ForeColor.GetHashCode() ^ this.Character.GetHashCode() ^ this.FontFamily.GetHashCode() ^ this.FontWeight.GetHashCode() ^ this.FontSize.GetHashCode();
        }

        public bool Equals( CharSpriteKey other )
        {
            if ( other == null || ( int ) other.Character != ( int ) this.Character || ( !( other.ForeColor == this.ForeColor ) || !( other.FontFamily == this.FontFamily ) ) || !other.FontWeight.Equals( this.FontWeight ) )
                return false;
            return ( double ) other.FontSize == ( double ) this.FontSize;
        }

        public override bool Equals( object obj )
        {
            return this.Equals( obj as CharSpriteKey );
        }
    }
}
