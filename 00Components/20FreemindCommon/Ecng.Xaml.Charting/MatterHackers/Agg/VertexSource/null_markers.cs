// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.null_markers
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Runtime.InteropServices;

namespace MatterHackers.Agg.VertexSource
{
    [StructLayout( LayoutKind.Sequential, Size = 1 )]
    internal struct null_markers : IMarkers
    {
        public void remove_all()
        {
        }

        public void add_vertex( double x, double y, Path.FlagsAndCommand unknown )
        {
        }

        public void prepare_src()
        {
        }

        public void rewind( int unknown )
        {
        }

        public Path.FlagsAndCommand vertex( ref double x, ref double y )
        {
            return Path.FlagsAndCommand.CommandStop;
        }
    }
}
