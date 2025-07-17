// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.StrokeGenerator
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.VertexSource
{
    internal class StrokeGenerator : IGenerator
    {
        private StrokeMath m_stroker;
        private VertexSequence m_src_vertices;
        private Vector2Container m_out_vertices;
        private double m_shorten;
        private int m_closed;
        private StrokeMath.status_e m_status;
        private StrokeMath.status_e m_prev_status;
        private int m_src_vertex;
        private int m_out_vertex;

        public StrokeGenerator()
        {
            this.m_stroker = new StrokeMath();
            this.m_src_vertices = new VertexSequence();
            this.m_out_vertices = new Vector2Container();
            this.m_status = StrokeMath.status_e.initial;
        }

        public void line_cap( LineCap lc )
        {
            this.m_stroker.line_cap( lc );
        }

        public void line_join( LineJoin lj )
        {
            this.m_stroker.line_join( lj );
        }

        public void inner_join( InnerJoin ij )
        {
            this.m_stroker.inner_join( ij );
        }

        public LineCap line_cap()
        {
            return this.m_stroker.line_cap();
        }

        public LineJoin line_join()
        {
            return this.m_stroker.line_join();
        }

        public InnerJoin inner_join()
        {
            return this.m_stroker.inner_join();
        }

        public void width( double w )
        {
            this.m_stroker.width( w );
        }

        public void miter_limit( double ml )
        {
            this.m_stroker.miter_limit( ml );
        }

        public void miter_limit_theta( double t )
        {
            this.m_stroker.miter_limit_theta( t );
        }

        public void inner_miter_limit( double ml )
        {
            this.m_stroker.inner_miter_limit( ml );
        }

        public void approximation_scale( double approx_scale )
        {
            this.m_stroker.approximation_scale( approx_scale );
        }

        public double width()
        {
            return this.m_stroker.width();
        }

        public double miter_limit()
        {
            return this.m_stroker.miter_limit();
        }

        public double inner_miter_limit()
        {
            return this.m_stroker.inner_miter_limit();
        }

        public double approximation_scale()
        {
            return this.m_stroker.approximation_scale();
        }

        public void auto_detect_orientation( bool v )
        {
            throw new Exception();
        }

        public bool auto_detect_orientation()
        {
            throw new Exception();
        }

        public void shorten( double s )
        {
            this.m_shorten = s;
        }

        public double shorten()
        {
            return this.m_shorten;
        }

        public void RemoveAll()
        {
            this.m_src_vertices.remove_all();
            this.m_closed = 0;
            this.m_status = StrokeMath.status_e.initial;
        }

        public void AddVertex( double x, double y, Path.FlagsAndCommand cmd )
        {
            this.m_status = StrokeMath.status_e.initial;
            if ( Path.is_move_to( cmd ) )
                this.m_src_vertices.modify_last( new VertexDistance( x, y ) );
            else if ( Path.is_vertex( cmd ) )
                this.m_src_vertices.add( new VertexDistance( x, y ) );
            else
                this.m_closed = ( int ) Path.get_close_flag( cmd );
        }

        public void Rewind( int idx )
        {
            if ( this.m_status == StrokeMath.status_e.initial )
            {
                this.m_src_vertices.close( ( uint ) this.m_closed > 0U );
                Path.shorten_path( this.m_src_vertices, this.m_shorten, this.m_closed );
                if ( this.m_src_vertices.size() < 3 )
                    this.m_closed = 0;
            }
            this.m_status = StrokeMath.status_e.ready;
            this.m_src_vertex = 0;
            this.m_out_vertex = 0;
        }

        public Path.FlagsAndCommand Vertex( ref double x, ref double y )
        {
            Path.FlagsAndCommand c = Path.FlagsAndCommand.CommandLineTo;
            while ( !Path.is_stop( c ) )
            {
                switch ( this.m_status )
                {
                    case StrokeMath.status_e.initial:
                        this.Rewind( 0 );
                        goto case StrokeMath.status_e.ready;
                    case StrokeMath.status_e.ready:
                        if ( this.m_src_vertices.size() < 2 + ( this.m_closed != 0 ? 1 : 0 ) )
                        {
                            c = Path.FlagsAndCommand.CommandStop;
                            continue;
                        }
                        this.m_status = this.m_closed != 0 ? StrokeMath.status_e.outline1 : StrokeMath.status_e.cap1;
                        c = Path.FlagsAndCommand.CommandMoveTo;
                        this.m_src_vertex = 0;
                        this.m_out_vertex = 0;
                        continue;
                    case StrokeMath.status_e.cap1:
                        this.m_stroker.calc_cap( ( IVertexDest ) this.m_out_vertices, this.m_src_vertices[ 0 ], this.m_src_vertices[ 1 ], this.m_src_vertices[ 0 ].dist );
                        this.m_src_vertex = 1;
                        this.m_prev_status = StrokeMath.status_e.outline1;
                        this.m_status = StrokeMath.status_e.out_vertices;
                        this.m_out_vertex = 0;
                        continue;
                    case StrokeMath.status_e.cap2:
                        this.m_stroker.calc_cap( ( IVertexDest ) this.m_out_vertices, this.m_src_vertices[ this.m_src_vertices.size() - 1 ], this.m_src_vertices[ this.m_src_vertices.size() - 2 ], this.m_src_vertices[ this.m_src_vertices.size() - 2 ].dist );
                        this.m_prev_status = StrokeMath.status_e.outline2;
                        this.m_status = StrokeMath.status_e.out_vertices;
                        this.m_out_vertex = 0;
                        continue;
                    case StrokeMath.status_e.outline1:
                        if ( this.m_closed != 0 )
                        {
                            if ( this.m_src_vertex >= this.m_src_vertices.size() )
                            {
                                this.m_prev_status = StrokeMath.status_e.close_first;
                                this.m_status = StrokeMath.status_e.end_poly1;
                                continue;
                            }
                        }
                        else if ( this.m_src_vertex >= this.m_src_vertices.size() - 1 )
                        {
                            this.m_status = StrokeMath.status_e.cap2;
                            continue;
                        }
                        this.m_stroker.calc_join( ( IVertexDest ) this.m_out_vertices, this.m_src_vertices.prev( this.m_src_vertex ), this.m_src_vertices.curr( this.m_src_vertex ), this.m_src_vertices.next( this.m_src_vertex ), this.m_src_vertices.prev( this.m_src_vertex ).dist, this.m_src_vertices.curr( this.m_src_vertex ).dist );
                        ++this.m_src_vertex;
                        this.m_prev_status = this.m_status;
                        this.m_status = StrokeMath.status_e.out_vertices;
                        this.m_out_vertex = 0;
                        continue;
                    case StrokeMath.status_e.close_first:
                        this.m_status = StrokeMath.status_e.outline2;
                        c = Path.FlagsAndCommand.CommandMoveTo;
                        goto case StrokeMath.status_e.outline2;
                    case StrokeMath.status_e.outline2:
                        if ( this.m_src_vertex <= ( this.m_closed == 0 ? 1 : 0 ) )
                        {
                            this.m_status = StrokeMath.status_e.end_poly2;
                            this.m_prev_status = StrokeMath.status_e.stop;
                            continue;
                        }
                        --this.m_src_vertex;
                        this.m_stroker.calc_join( ( IVertexDest ) this.m_out_vertices, this.m_src_vertices.next( this.m_src_vertex ), this.m_src_vertices.curr( this.m_src_vertex ), this.m_src_vertices.prev( this.m_src_vertex ), this.m_src_vertices.curr( this.m_src_vertex ).dist, this.m_src_vertices.prev( this.m_src_vertex ).dist );
                        this.m_prev_status = this.m_status;
                        this.m_status = StrokeMath.status_e.out_vertices;
                        this.m_out_vertex = 0;
                        continue;
                    case StrokeMath.status_e.out_vertices:
                        if ( this.m_out_vertex >= this.m_out_vertices.size() )
                        {
                            this.m_status = this.m_prev_status;
                            continue;
                        }
                        Vector2 outVertex = this.m_out_vertices[this.m_out_vertex++];
                        x = outVertex.x;
                        y = outVertex.y;
                        return c;
                    case StrokeMath.status_e.end_poly1:
                        this.m_status = this.m_prev_status;
                        return Path.FlagsAndCommand.CommandEndPoly | Path.FlagsAndCommand.FlagCCW | Path.FlagsAndCommand.FlagClose;
                    case StrokeMath.status_e.end_poly2:
                        this.m_status = this.m_prev_status;
                        return Path.FlagsAndCommand.CommandEndPoly | Path.FlagsAndCommand.FlagCW | Path.FlagsAndCommand.FlagClose;
                    case StrokeMath.status_e.stop:
                        c = Path.FlagsAndCommand.CommandStop;
                        continue;
                    default:
                        continue;
                }
            }
            return c;
        }
    }
}
