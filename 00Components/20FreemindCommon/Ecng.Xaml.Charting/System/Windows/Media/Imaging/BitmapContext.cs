// System.Windows.Media.Imaging.BitmapContext
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

public struct BitmapContext : IDisposable
{
    private readonly WriteableBitmap _writeableBitmap;

    private readonly int _pixelWidth;

    private readonly int _pixelHeight;

    private static readonly IDictionary<WriteableBitmap, int> _updateCountByBmp = new Dictionary<WriteableBitmap, int>();

    private readonly ReadWriteMode _mode;

    private unsafe readonly int* _backBuffer;

    private readonly int _length;

    internal WriteableBitmap WriteableBitmap => _writeableBitmap;

    internal int PixelWidth => _pixelWidth;

    internal int PixelHeight => _pixelHeight;

    internal unsafe int* Pixels => _backBuffer;

    internal int Length => _length;

    internal unsafe BitmapContext( WriteableBitmap writeableBitmap )
    {
        _writeableBitmap = writeableBitmap;
        _pixelWidth = _writeableBitmap.PixelWidth;
        _pixelHeight = _writeableBitmap.PixelHeight;
        _mode = ReadWriteMode.ReadWrite;
        lock ( _updateCountByBmp )
        {
            if ( !_updateCountByBmp.ContainsKey( _writeableBitmap ) )
            {
                _updateCountByBmp.Add( _writeableBitmap, 0 );
                _writeableBitmap.Lock();
            }
            IDictionary<WriteableBitmap, int> updateCountByBmp = _updateCountByBmp;
            WriteableBitmap writeableBitmap2 = _writeableBitmap;
            updateCountByBmp[ writeableBitmap2 ]++;
        }
        _backBuffer = ( int* ) ( void* ) _writeableBitmap.BackBuffer;
        double num = (double)(_writeableBitmap.BackBufferStride / 4);
        double num2 = (double)_writeableBitmap.PixelHeight;
        _length = ( int ) ( num * num2 );
    }

    internal BitmapContext( WriteableBitmap writeableBitmap, ReadWriteMode mode )
    {
        this = new BitmapContext( writeableBitmap );
        _mode = mode;
    }

    internal unsafe static void BlockCopy( BitmapContext src, int srcOffset, BitmapContext dest, int destOffset, int count )
    {
        NativeMethods.CopyUnmanagedMemory( ( byte* ) src.Pixels, srcOffset, ( byte* ) dest.Pixels, destOffset, count );
    }

    internal unsafe static void BlockCopy( int[ ] src, int srcOffset, BitmapContext dest, int destOffset, int count )
    {
        fixed ( int* srcPtr = src )
        {
            NativeMethods.CopyUnmanagedMemory( ( byte* ) srcPtr, srcOffset, ( byte* ) dest.Pixels, destOffset, count );
        }
    }

    internal unsafe static void BlockCopy( byte[ ] src, int srcOffset, BitmapContext dest, int destOffset, int count )
    {
        fixed ( byte* srcPtr = src )
        {
            NativeMethods.CopyUnmanagedMemory( srcPtr, srcOffset, ( byte* ) dest.Pixels, destOffset, count );
        }
    }

    internal unsafe static void BlockCopy( BitmapContext src, int srcOffset, byte[ ] dest, int destOffset, int count )
    {
        fixed ( byte* dstPtr = dest )
        {
            NativeMethods.CopyUnmanagedMemory( ( byte* ) src.Pixels, srcOffset, dstPtr, destOffset, count );
        }
    }

    internal unsafe static void BlockCopy( BitmapContext src, int srcOffset, int[ ] dest, int destOffset, int count )
    {
        fixed ( int* dstPtr = dest )
        {
            NativeMethods.CopyUnmanagedMemory( ( byte* ) src.Pixels, srcOffset, ( byte* ) dstPtr, destOffset, count );
        }
    }

    internal void Clear()
    {
        NativeMethods.SetUnmanagedMemory( _writeableBitmap.BackBuffer, 0, _writeableBitmap.BackBufferStride * _writeableBitmap.PixelHeight );
    }

    private static int Dec( WriteableBitmap target )
    {
        if ( !_updateCountByBmp.TryGetValue( target, out int value ) )
        {
            return -1;
        }
        value--;
        _updateCountByBmp[ target ] = value;
        return value;
    }

    public void Dispose()
    {
        lock ( _updateCountByBmp )
        {
            if ( Dec( _writeableBitmap ) == 0 )
            {
                _updateCountByBmp.Remove( _writeableBitmap );
                if ( _mode == ReadWriteMode.ReadWrite )
                {
                    _writeableBitmap.AddDirtyRect( new Int32Rect( 0, 0, _writeableBitmap.PixelWidth, _writeableBitmap.PixelHeight ) );
                }
                _writeableBitmap.Unlock();
            }
        }
    }
}
