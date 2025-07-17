// Decompiled with JetBrains decompiler
// Type: System.Windows.Media.Imaging.NativeMethods
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Runtime.InteropServices;

namespace System.Windows.Media.Imaging
{
    internal static class NativeMethods
    {
        internal static unsafe void CopyUnmanagedMemory( byte* srcPtr, int srcOffset, byte* dstPtr, int dstOffset, int count )
        {
            srcPtr += srcOffset;
            dstPtr += dstOffset;
            NativeMethods.memcpy( dstPtr, srcPtr, count );
        }

        internal static unsafe void CopyUnmanagedMemory( int* srcPtr, int* dstPtr, int count )
        {
            NativeMethods.memcpy( ( byte* ) dstPtr, ( byte* ) srcPtr, count * 4 );
        }

        internal static void SetUnmanagedMemory( IntPtr dst, int filler, int count )
        {
            NativeMethods.memset( dst, filler, count );
        }

        [DllImport( "msvcrt.dll", CallingConvention = CallingConvention.Cdecl )]
        private static extern unsafe byte* memcpy( byte* dst, byte* src, int count );

        [DllImport( "msvcrt.dll", CallingConvention = CallingConvention.Cdecl )]
        private static extern void memset( IntPtr dst, int filler, int count );
    }
}
