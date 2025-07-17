// Decompiled with JetBrains decompiler
// Type: System.Windows.Media.Imaging.BitmapContext
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections.Generic;

namespace System.Windows.Media.Imaging
{
    public struct BitmapContext : IDisposable
    {
        private static readonly IDictionary<WriteableBitmap, int> _updateCountByBmp = (IDictionary<WriteableBitmap, int>) new Dictionary<WriteableBitmap, int>();
        private readonly WriteableBitmap _writeableBitmap;
        private readonly int _pixelWidth;
        private readonly int _pixelHeight;
        private readonly ReadWriteMode _mode;
        private readonly unsafe int* _backBuffer;
        private readonly int _length;

        internal unsafe BitmapContext( WriteableBitmap writeableBitmap )
        {
            this._writeableBitmap = writeableBitmap;
            this._pixelWidth = this._writeableBitmap.PixelWidth;
            this._pixelHeight = this._writeableBitmap.PixelHeight;
            this._mode = ReadWriteMode.ReadWrite;
            lock ( BitmapContext._updateCountByBmp )
            {
                if ( !BitmapContext._updateCountByBmp.ContainsKey( this._writeableBitmap ) )
                {
                    BitmapContext._updateCountByBmp.Add( this._writeableBitmap, 0 );
                    this._writeableBitmap.Lock();
                }
                BitmapContext._updateCountByBmp[ this._writeableBitmap ]++;
            }
            this._backBuffer = ( int* ) ( void* ) this._writeableBitmap.BackBuffer;
            this._length = ( int ) ( ( double ) ( this._writeableBitmap.BackBufferStride / 4 ) * ( double ) this._writeableBitmap.PixelHeight );
        }

        internal BitmapContext( WriteableBitmap writeableBitmap, ReadWriteMode mode )
        {
            this = new BitmapContext( writeableBitmap );
            this._mode = mode;
        }

        internal WriteableBitmap WriteableBitmap
        {
            get
            {
                return this._writeableBitmap;
            }
        }

        internal int PixelWidth
        {
            get
            {
                return this._pixelWidth;
            }
        }

        internal int PixelHeight
        {
            get
            {
                return this._pixelHeight;
            }
        }

        internal unsafe int* Pixels
        {
            get
            {
                return this._backBuffer;
            }
        }

        internal int Length
        {
            get
            {
                return this._length;
            }
        }

        internal static unsafe void BlockCopy( BitmapContext src, int srcOffset, BitmapContext dest, int destOffset, int count )
        {
            NativeMethods.CopyUnmanagedMemory( ( byte* ) src.Pixels, srcOffset, ( byte* ) dest.Pixels, destOffset, count );
        }

        internal static unsafe void BlockCopy( int[ ] src, int srcOffset, BitmapContext dest, int destOffset, int count )
        {
            int[] numArray;
            NativeMethods.CopyUnmanagedMemory( ( numArray = src ) == null || numArray.Length == 0 ? ( byte* ) null : ( byte* ) &numArray[ 0 ], srcOffset, ( byte* ) dest.Pixels, destOffset, count );
            numArray = ( int[ ] ) null;
        }

        internal static unsafe void BlockCopy( byte[ ] src, int srcOffset, BitmapContext dest, int destOffset, int count )
        {
            byte[] numArray;
            NativeMethods.CopyUnmanagedMemory( ( numArray = src ) == null || numArray.Length == 0 ? ( byte* ) null : &numArray[ 0 ], srcOffset, ( byte* ) dest.Pixels, destOffset, count );
            numArray = ( byte[ ] ) null;
        }

        internal static unsafe void BlockCopy( BitmapContext src, int srcOffset, byte[ ] dest, int destOffset, int count )
        {
            byte[] numArray;
            byte* dstPtr = (numArray = dest) == null || numArray.Length == 0 ? (byte*) null : &numArray[0];
            NativeMethods.CopyUnmanagedMemory( ( byte* ) src.Pixels, srcOffset, dstPtr, destOffset, count );
            numArray = ( byte[ ] ) null;
        }

        internal static unsafe void BlockCopy( BitmapContext src, int srcOffset, int[ ] dest, int destOffset, int count )
        {
            int[] numArray;
            int* numPtr = (numArray = dest) == null || numArray.Length == 0 ? (int*) null : &numArray[0];
            NativeMethods.CopyUnmanagedMemory( ( byte* ) src.Pixels, srcOffset, ( byte* ) numPtr, destOffset, count );
            numArray = ( int[ ] ) null;
        }

        internal void Clear()
        {
            NativeMethods.SetUnmanagedMemory( this._writeableBitmap.BackBuffer, 0, this._writeableBitmap.BackBufferStride * this._writeableBitmap.PixelHeight );
        }

        private static int Dec( WriteableBitmap target )
        {
            int num1;
            if ( !BitmapContext._updateCountByBmp.TryGetValue( target, out num1 ) )
                return -1;
            int num2 = num1 - 1;
            BitmapContext._updateCountByBmp[ target ] = num2;
            return num2;
        }

        public void Dispose()
        {
            lock ( BitmapContext._updateCountByBmp )
            {
                if ( BitmapContext.Dec( this._writeableBitmap ) != 0 )
                    return;
                BitmapContext._updateCountByBmp.Remove( this._writeableBitmap );
                if ( this._mode == ReadWriteMode.ReadWrite )
                    this._writeableBitmap.AddDirtyRect( new Int32Rect( 0, 0, this._writeableBitmap.PixelWidth, this._writeableBitmap.PixelHeight ) );
                this._writeableBitmap.Unlock();
            }
        }
    }
}
