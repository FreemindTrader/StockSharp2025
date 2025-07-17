// System.Windows.Media.Imaging.NativeMethods
using System;
using System.Runtime.InteropServices;

internal static class NativeMethods
{
    internal unsafe static void CopyUnmanagedMemory( byte* srcPtr, int srcOffset, byte* dstPtr, int dstOffset, int count )
    {
        srcPtr += srcOffset;
        dstPtr += dstOffset;
        memcpy( dstPtr, srcPtr, count );
    }

    internal unsafe static void CopyUnmanagedMemory( int* srcPtr, int* dstPtr, int count )
    {
        memcpy( ( byte* ) dstPtr, ( byte* ) srcPtr, count * 4 );
    }

    internal static void SetUnmanagedMemory( IntPtr dst, int filler, int count )
    {
        memset( dst, filler, count );
    }

    [DllImport( "msvcrt.dll", CallingConvention = CallingConvention.Cdecl )]
    private unsafe static extern byte* memcpy( byte* dst, byte* src, int count );

    [DllImport( "msvcrt.dll", CallingConvention = CallingConvention.Cdecl )]
    private static extern void memset( IntPtr dst, int filler, int count );
}
