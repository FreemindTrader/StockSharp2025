// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.TextureCache
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ecng.Xaml.Charting.Common.Extensions;

namespace Ecng.Xaml.Charting.Rendering
{
    public class TextureCache : TextureCacheBase
    {
        public static int MaxMemorySize = 33554432;
        public static int MaxItemsCount = 2048;
        private readonly LinkedList<Tuple<TextureKey, TextureCache.TextureType>> _textureKeys = new LinkedList<Tuple<TextureKey, TextureCache.TextureType>>();
        private readonly Dictionary<TextureKey, byte[]> _cachedByteTextures = new Dictionary<TextureKey, byte[]>();
        private readonly Dictionary<TextureKey, WriteableBitmap> _cachedWriteableBitmapTextures = new Dictionary<TextureKey, WriteableBitmap>();
        private readonly Dictionary<TextureKey, int[]> _cachedIntTextures = new Dictionary<TextureKey, int[]>();
        private int _memorySize;

        public int MemorySize
        {
            get
            {
                return this._memorySize;
            }
        }

        private void RemoveOldEntriesIfNeeded()
        {
            while ( this._memorySize > TextureCache.MaxMemorySize || this._textureKeys.Count > TextureCache.MaxItemsCount )
            {
                Tuple<TextureKey, TextureCache.TextureType> tuple = this._textureKeys.First.Value;
                this._textureKeys.RemoveFirst();
                if ( tuple.Item2 == TextureCache.TextureType.Byte )
                {
                    this._memorySize -= this._cachedByteTextures[ tuple.Item1 ].Length;
                    this._cachedByteTextures.Remove( tuple.Item1 );
                }
                else if ( tuple.Item2 == TextureCache.TextureType.WriteableBitmap )
                {
                    WriteableBitmap writeableBitmapTexture = this._cachedWriteableBitmapTextures[tuple.Item1];
                    this._memorySize -= writeableBitmapTexture.PixelHeight * writeableBitmapTexture.PixelWidth * 4;
                    this._cachedWriteableBitmapTextures.Remove( tuple.Item1 );
                }
                else
                {
                    if ( tuple.Item2 != TextureCache.TextureType.Int )
                        throw new Exception( "unknown TextureType" );
                    this._memorySize -= this._cachedIntTextures[ tuple.Item1 ].Length * 4;
                    this._cachedIntTextures.Remove( tuple.Item1 );
                }
            }
        }

        public void AddTexture( Size size, Brush brush, byte[ ] texture )
        {
            TextureKey key = new TextureKey(size, brush);
            if ( this._cachedByteTextures.ContainsKey( key ) )
            {
                this._memorySize -= this._cachedByteTextures[ key ].Length;
                this._cachedByteTextures[ key ] = texture;
                this._memorySize += texture.Length;
            }
            else
            {
                this._cachedByteTextures.Add( key, texture );
                this._memorySize += texture.Length;
                this._textureKeys.AddLast( new Tuple<TextureKey, TextureCache.TextureType>( key, TextureCache.TextureType.Byte ) );
                this.RemoveOldEntriesIfNeeded();
            }
        }

        public byte[ ] GetByteTexture( Size size, Brush brush )
        {
            byte[] numArray;
            if ( !this._cachedByteTextures.TryGetValue( new TextureKey( size, brush ), out numArray ) )
                return ( byte[ ] ) null;
            return numArray;
        }

        public WriteableBitmap GetWriteableBitmapTexture( FrameworkElement fe )
        {
            TextureKey key = new TextureKey(fe);
            WriteableBitmap bitmap;
            if ( !this._cachedWriteableBitmapTextures.TryGetValue( key, out bitmap ) )
            {
                bitmap = fe.RenderToBitmap();
                this._cachedWriteableBitmapTextures.Add( key, bitmap );
                this._memorySize += bitmap.PixelWidth * bitmap.PixelHeight * 4;
                this._textureKeys.AddLast( new Tuple<TextureKey, TextureCache.TextureType>( key, TextureCache.TextureType.WriteableBitmap ) );
            }
            this.RemoveOldEntriesIfNeeded();
            return bitmap;
        }

        public void AddTexture( Size size, Brush brush, int[ ] texture )
        {
            TextureKey key = new TextureKey(size, brush);
            if ( this._cachedIntTextures.ContainsKey( key ) )
            {
                this._memorySize -= this._cachedByteTextures[ key ].Length * 4;
                this._cachedIntTextures[ key ] = texture;
                this._memorySize += texture.Length * 4;
            }
            else
            {
                this._cachedIntTextures.Add( key, texture );
                this._memorySize += texture.Length * 4;
                this._textureKeys.AddLast( new Tuple<TextureKey, TextureCache.TextureType>( key, TextureCache.TextureType.Int ) );
                this.RemoveOldEntriesIfNeeded();
            }
        }

        public int[ ] GetIntTexture( Size size, Brush brush )
        {
            int[] numArray;
            if ( !this._cachedIntTextures.TryGetValue( new TextureKey( size, brush ), out numArray ) )
                return ( int[ ] ) null;
            return numArray;
        }

        private enum TextureType
        {
            Byte,
            Int,
            WriteableBitmap,
        }
    }
}
