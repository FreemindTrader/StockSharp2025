// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.VertexSourceAdapter
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg.VertexSource
{
    internal class VertexSourceAdapter
    {
        private IVertexSource source;
        private IGenerator generator;
        private IMarkers markers;
        private VertexSourceAdapter.status m_status;
        private Path.FlagsAndCommand m_last_cmd;
        private double m_start_x;
        private double m_start_y;

        public VertexSourceAdapter( IVertexSource source, IGenerator generator )
        {
            this.markers = ( IMarkers ) new null_markers();
            this.source = source;
            this.generator = generator;
            this.m_status = VertexSourceAdapter.status.initial;
        }

        public VertexSourceAdapter( IVertexSource source, IGenerator generator, IMarkers markers )
          : this( source, generator )
        {
            this.markers = markers;
        }

        private void Attach( IVertexSource source )
        {
            this.source = source;
        }

        protected IGenerator GetGenerator()
        {
            return this.generator;
        }

        private IMarkers GetMarkers()
        {
            return this.markers;
        }

        public void rewind( int path_id )
        {
            this.source.rewind( path_id );
            this.m_status = VertexSourceAdapter.status.initial;
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            x = 0.0;
            y = 0.0;
            Path.FlagsAndCommand c = Path.FlagsAndCommand.CommandStop;
            bool flag = false;
            while ( !flag )
            {
                switch ( this.m_status )
                {
                    case VertexSourceAdapter.status.initial:
                        this.markers.remove_all();
                        this.m_last_cmd = this.source.vertex( out this.m_start_x, out this.m_start_y );
                        this.m_status = VertexSourceAdapter.status.accumulate;
                        goto case VertexSourceAdapter.status.accumulate;
                    case VertexSourceAdapter.status.accumulate:
                        if ( Path.is_stop( this.m_last_cmd ) )
                            return Path.FlagsAndCommand.CommandStop;
                        this.generator.RemoveAll();
                        this.generator.AddVertex( this.m_start_x, this.m_start_y, Path.FlagsAndCommand.CommandMoveTo );
                        this.markers.add_vertex( this.m_start_x, this.m_start_y, Path.FlagsAndCommand.CommandMoveTo );
                        Path.FlagsAndCommand flagsAndCommand;
                        do
                        {
                            flagsAndCommand = this.source.vertex( out x, out y );
                            if ( Path.is_vertex( flagsAndCommand ) )
                            {
                                this.m_last_cmd = flagsAndCommand;
                                if ( Path.is_move_to( flagsAndCommand ) )
                                {
                                    this.m_start_x = x;
                                    this.m_start_y = y;
                                    goto label_14;
                                }
                                else
                                {
                                    this.generator.AddVertex( x, y, flagsAndCommand );
                                    this.markers.add_vertex( x, y, Path.FlagsAndCommand.CommandLineTo );
                                }
                            }
                            else if ( Path.is_stop( flagsAndCommand ) )
                            {
                                this.m_last_cmd = Path.FlagsAndCommand.CommandStop;
                                goto label_14;
                            }
                        }
                        while ( !Path.is_end_poly( flagsAndCommand ) );
                        this.generator.AddVertex( x, y, flagsAndCommand );
                    label_14:
                        this.generator.Rewind( 0 );
                        this.m_status = VertexSourceAdapter.status.generate;
                        goto case VertexSourceAdapter.status.generate;
                    case VertexSourceAdapter.status.generate:
                        c = this.generator.Vertex( ref x, ref y );
                        if ( Path.is_stop( c ) )
                        {
                            this.m_status = VertexSourceAdapter.status.accumulate;
                            continue;
                        }
                        flag = true;
                        continue;
                    default:
                        continue;
                }
            }
            return c;
        }

        private enum status
        {
            initial,
            accumulate,
            generate,
        }
    }
}
