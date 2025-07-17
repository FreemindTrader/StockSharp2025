// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.rasterizer_outline_aa
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using MatterHackers.Agg.VertexSource;

namespace MatterHackers.Agg
{
    internal class rasterizer_outline_aa
    {
        private line_aa_vertex_sequence m_src_vertices = new line_aa_vertex_sequence();
        private LineRenderer m_ren;
        private rasterizer_outline_aa.outline_aa_join_e m_line_join;
        private bool m_round_cap;
        private int m_start_x;
        private int m_start_y;

        public bool cmp_dist_start( int d )
        {
            return d > 0;
        }

        public bool cmp_dist_end( int d )
        {
            return d <= 0;
        }

        private void draw( ref rasterizer_outline_aa.draw_vars dv, int start, int end )
        {
            for ( int index = start ; index < end ; ++index )
            {
                if ( this.m_line_join == rasterizer_outline_aa.outline_aa_join_e.outline_round_join )
                {
                    dv.xb1 = dv.curr.x1 + ( dv.curr.y2 - dv.curr.y1 );
                    dv.yb1 = dv.curr.y1 - ( dv.curr.x2 - dv.curr.x1 );
                    dv.xb2 = dv.curr.x2 + ( dv.curr.y2 - dv.curr.y1 );
                    dv.yb2 = dv.curr.y2 - ( dv.curr.x2 - dv.curr.x1 );
                }
                switch ( dv.flags )
                {
                    case 0:
                        this.m_ren.line3( dv.curr, dv.xb1, dv.yb1, dv.xb2, dv.yb2 );
                        break;
                    case 1:
                        this.m_ren.line2( dv.curr, dv.xb2, dv.yb2 );
                        break;
                    case 2:
                        this.m_ren.line1( dv.curr, dv.xb1, dv.yb1 );
                        break;
                    case 3:
                        this.m_ren.line0( dv.curr );
                        break;
                }
                if ( this.m_line_join == rasterizer_outline_aa.outline_aa_join_e.outline_round_join && ( dv.flags & 2 ) == 0 )
                    this.m_ren.pie( dv.curr.x2, dv.curr.y2, dv.curr.x2 + ( dv.curr.y2 - dv.curr.y1 ), dv.curr.y2 - ( dv.curr.x2 - dv.curr.x1 ), dv.curr.x2 + ( dv.next.y2 - dv.next.y1 ), dv.curr.y2 - ( dv.next.x2 - dv.next.x1 ) );
                dv.x1 = dv.x2;
                dv.y1 = dv.y2;
                dv.lcurr = dv.lnext;
                dv.lnext = this.m_src_vertices[ dv.idx ].len;
                ++dv.idx;
                if ( dv.idx >= this.m_src_vertices.size() )
                    dv.idx = 0;
                dv.x2 = this.m_src_vertices[ dv.idx ].x;
                dv.y2 = this.m_src_vertices[ dv.idx ].y;
                dv.curr = dv.next;
                dv.next = new line_parameters( dv.x1, dv.y1, dv.x2, dv.y2, dv.lnext );
                dv.xb1 = dv.xb2;
                dv.yb1 = dv.yb2;
                switch ( this.m_line_join )
                {
                    case rasterizer_outline_aa.outline_aa_join_e.outline_no_join:
                        dv.flags = 3;
                        break;
                    case rasterizer_outline_aa.outline_aa_join_e.outline_miter_join:
                        dv.flags >>= 1;
                        dv.flags |= ( int ) dv.curr.diagonal_quadrant() == ( int ) dv.next.diagonal_quadrant() ? 1 : 0;
                        if ( ( dv.flags & 2 ) == 0 )
                        {
                            LineAABasics.bisectrix( dv.curr, dv.next, out dv.xb2, out dv.yb2 );
                            break;
                        }
                        break;
                    case rasterizer_outline_aa.outline_aa_join_e.outline_round_join:
                        dv.flags >>= 1;
                        dv.flags |= ( ( int ) dv.curr.diagonal_quadrant() == ( int ) dv.next.diagonal_quadrant() ? 1 : 0 ) << 1;
                        break;
                    case rasterizer_outline_aa.outline_aa_join_e.outline_miter_accurate_join:
                        dv.flags = 0;
                        LineAABasics.bisectrix( dv.curr, dv.next, out dv.xb2, out dv.yb2 );
                        break;
                }
            }
        }

        public rasterizer_outline_aa( LineRenderer ren )
        {
            this.m_ren = ren;
            this.m_line_join = OutlineRenderer.accurate_join_only() ? rasterizer_outline_aa.outline_aa_join_e.outline_miter_accurate_join : rasterizer_outline_aa.outline_aa_join_e.outline_round_join;
            this.m_round_cap = false;
            this.m_start_x = 0;
            this.m_start_y = 0;
        }

        public void attach( LineRenderer ren )
        {
            this.m_ren = ren;
        }

        public void line_join( rasterizer_outline_aa.outline_aa_join_e join )
        {
            this.m_line_join = OutlineRenderer.accurate_join_only() ? rasterizer_outline_aa.outline_aa_join_e.outline_miter_accurate_join : join;
        }

        public rasterizer_outline_aa.outline_aa_join_e line_join()
        {
            return this.m_line_join;
        }

        public void round_cap( bool v )
        {
            this.m_round_cap = v;
        }

        public bool round_cap()
        {
            return this.m_round_cap;
        }

        public void move_to( int x, int y )
        {
            this.m_src_vertices.modify_last( new line_aa_vertex( this.m_start_x = x, this.m_start_y = y ) );
        }

        public void line_to( int x, int y )
        {
            this.m_src_vertices.add( new line_aa_vertex( x, y ) );
        }

        public void move_to_d( double x, double y )
        {
            this.move_to( line_coord_sat.conv( x ), line_coord_sat.conv( y ) );
        }

        public void line_to_d( double x, double y )
        {
            this.line_to( line_coord_sat.conv( x ), line_coord_sat.conv( y ) );
        }

        public void render( bool close_polygon )
        {
            this.m_src_vertices.close( close_polygon );
            rasterizer_outline_aa.draw_vars dv = new rasterizer_outline_aa.draw_vars();
            if ( close_polygon )
            {
                if ( this.m_src_vertices.size() >= 3 )
                {
                    dv.idx = 2;
                    line_aa_vertex srcVertex1 = this.m_src_vertices[this.m_src_vertices.size() - 1];
                    int x1 = srcVertex1.x;
                    int y1 = srcVertex1.y;
                    int len = srcVertex1.len;
                    line_aa_vertex srcVertex2 = this.m_src_vertices[0];
                    int x2 = srcVertex2.x;
                    int y2 = srcVertex2.y;
                    dv.lcurr = srcVertex2.len;
                    line_parameters l1 = new line_parameters(x1, y1, x2, y2, len);
                    line_aa_vertex srcVertex3 = this.m_src_vertices[1];
                    dv.x1 = srcVertex3.x;
                    dv.y1 = srcVertex3.y;
                    dv.lnext = srcVertex3.len;
                    dv.curr = new line_parameters( x2, y2, dv.x1, dv.y1, dv.lcurr );
                    line_aa_vertex srcVertex4 = this.m_src_vertices[dv.idx];
                    dv.x2 = srcVertex4.x;
                    dv.y2 = srcVertex4.y;
                    dv.next = new line_parameters( dv.x1, dv.y1, dv.x2, dv.y2, dv.lnext );
                    dv.xb1 = 0;
                    dv.yb1 = 0;
                    dv.xb2 = 0;
                    dv.yb2 = 0;
                    switch ( this.m_line_join )
                    {
                        case rasterizer_outline_aa.outline_aa_join_e.outline_no_join:
                            dv.flags = 3;
                            break;
                        case rasterizer_outline_aa.outline_aa_join_e.outline_miter_join:
                        case rasterizer_outline_aa.outline_aa_join_e.outline_round_join:
                            dv.flags = ( ( int ) l1.diagonal_quadrant() == ( int ) dv.curr.diagonal_quadrant() ? 1 : 0 ) | ( ( int ) dv.curr.diagonal_quadrant() == ( int ) dv.next.diagonal_quadrant() ? 1 : 0 ) << 1;
                            break;
                        case rasterizer_outline_aa.outline_aa_join_e.outline_miter_accurate_join:
                            dv.flags = 0;
                            break;
                    }
                    if ( ( dv.flags & 1 ) == 0 && this.m_line_join != rasterizer_outline_aa.outline_aa_join_e.outline_round_join )
                        LineAABasics.bisectrix( l1, dv.curr, out dv.xb1, out dv.yb1 );
                    if ( ( dv.flags & 2 ) == 0 && this.m_line_join != rasterizer_outline_aa.outline_aa_join_e.outline_round_join )
                        LineAABasics.bisectrix( dv.curr, dv.next, out dv.xb2, out dv.yb2 );
                    this.draw( ref dv, 0, this.m_src_vertices.size() );
                }
            }
            else
            {
                switch ( this.m_src_vertices.size() )
                {
                    case 0:
                    case 1:
                        break;
                    case 2:
                        line_aa_vertex srcVertex5 = this.m_src_vertices[0];
                        int x3 = srcVertex5.x;
                        int y3 = srcVertex5.y;
                        int len1 = srcVertex5.len;
                        line_aa_vertex srcVertex6 = this.m_src_vertices[1];
                        int x4 = srcVertex6.x;
                        int y4 = srcVertex6.y;
                        line_parameters lp = new line_parameters(x3, y3, x4, y4, len1);
                        if ( this.m_round_cap )
                            this.m_ren.semidot( new LineRenderer.CompareFunction( this.cmp_dist_start ), x3, y3, x3 + ( y4 - y3 ), y3 - ( x4 - x3 ) );
                        this.m_ren.line3( lp, x3 + ( y4 - y3 ), y3 - ( x4 - x3 ), x4 + ( y4 - y3 ), y4 - ( x4 - x3 ) );
                        if ( this.m_round_cap )
                        {
                            this.m_ren.semidot( new LineRenderer.CompareFunction( this.cmp_dist_end ), x4, y4, x4 + ( y4 - y3 ), y4 - ( x4 - x3 ) );
                            break;
                        }
                        break;
                    case 3:
                        line_aa_vertex srcVertex7 = this.m_src_vertices[0];
                        int x5 = srcVertex7.x;
                        int y5 = srcVertex7.y;
                        int len2 = srcVertex7.len;
                        line_aa_vertex srcVertex8 = this.m_src_vertices[1];
                        int x6 = srcVertex8.x;
                        int y6 = srcVertex8.y;
                        int len3 = srcVertex8.len;
                        line_aa_vertex srcVertex9 = this.m_src_vertices[2];
                        int x7 = srcVertex9.x;
                        int y7 = srcVertex9.y;
                        line_parameters lineParameters1 = new line_parameters(x5, y5, x6, y6, len2);
                        line_parameters lineParameters2 = new line_parameters(x6, y6, x7, y7, len3);
                        if ( this.m_round_cap )
                            this.m_ren.semidot( new LineRenderer.CompareFunction( this.cmp_dist_start ), x5, y5, x5 + ( y6 - y5 ), y5 - ( x6 - x5 ) );
                        if ( this.m_line_join == rasterizer_outline_aa.outline_aa_join_e.outline_round_join )
                        {
                            this.m_ren.line3( lineParameters1, x5 + ( y6 - y5 ), y5 - ( x6 - x5 ), x6 + ( y6 - y5 ), y6 - ( x6 - x5 ) );
                            this.m_ren.pie( x6, y6, x6 + ( y6 - y5 ), y6 - ( x6 - x5 ), x6 + ( y7 - y6 ), y6 - ( x7 - x6 ) );
                            this.m_ren.line3( lineParameters2, x6 + ( y7 - y6 ), y6 - ( x7 - x6 ), x7 + ( y7 - y6 ), y7 - ( x7 - x6 ) );
                        }
                        else
                        {
                            LineAABasics.bisectrix( lineParameters1, lineParameters2, out dv.xb1, out dv.yb1 );
                            this.m_ren.line3( lineParameters1, x5 + ( y6 - y5 ), y5 - ( x6 - x5 ), dv.xb1, dv.yb1 );
                            this.m_ren.line3( lineParameters2, dv.xb1, dv.yb1, x7 + ( y7 - y6 ), y7 - ( x7 - x6 ) );
                        }
                        if ( this.m_round_cap )
                        {
                            this.m_ren.semidot( new LineRenderer.CompareFunction( this.cmp_dist_end ), x7, y7, x7 + ( y7 - y6 ), y7 - ( x7 - x6 ) );
                            break;
                        }
                        break;
                    default:
                        dv.idx = 3;
                        line_aa_vertex srcVertex10 = this.m_src_vertices[0];
                        int x8 = srcVertex10.x;
                        int y8 = srcVertex10.y;
                        int len4 = srcVertex10.len;
                        line_aa_vertex srcVertex11 = this.m_src_vertices[1];
                        int x9 = srcVertex11.x;
                        int y9 = srcVertex11.y;
                        dv.lcurr = srcVertex11.len;
                        line_parameters lineParameters3 = new line_parameters(x8, y8, x9, y9, len4);
                        line_aa_vertex srcVertex12 = this.m_src_vertices[2];
                        dv.x1 = srcVertex12.x;
                        dv.y1 = srcVertex12.y;
                        dv.lnext = srcVertex12.len;
                        dv.curr = new line_parameters( x9, y9, dv.x1, dv.y1, dv.lcurr );
                        line_aa_vertex srcVertex13 = this.m_src_vertices[dv.idx];
                        dv.x2 = srcVertex13.x;
                        dv.y2 = srcVertex13.y;
                        dv.next = new line_parameters( dv.x1, dv.y1, dv.x2, dv.y2, dv.lnext );
                        dv.xb1 = 0;
                        dv.yb1 = 0;
                        dv.xb2 = 0;
                        dv.yb2 = 0;
                        switch ( this.m_line_join )
                        {
                            case rasterizer_outline_aa.outline_aa_join_e.outline_no_join:
                                dv.flags = 3;
                                break;
                            case rasterizer_outline_aa.outline_aa_join_e.outline_miter_join:
                            case rasterizer_outline_aa.outline_aa_join_e.outline_round_join:
                                dv.flags = ( ( int ) lineParameters3.diagonal_quadrant() == ( int ) dv.curr.diagonal_quadrant() ? 1 : 0 ) | ( ( int ) dv.curr.diagonal_quadrant() == ( int ) dv.next.diagonal_quadrant() ? 1 : 0 ) << 1;
                                break;
                            case rasterizer_outline_aa.outline_aa_join_e.outline_miter_accurate_join:
                                dv.flags = 0;
                                break;
                        }
                        if ( this.m_round_cap )
                            this.m_ren.semidot( new LineRenderer.CompareFunction( this.cmp_dist_start ), x8, y8, x8 + ( y9 - y8 ), y8 - ( x9 - x8 ) );
                        if ( ( dv.flags & 1 ) == 0 )
                        {
                            if ( this.m_line_join == rasterizer_outline_aa.outline_aa_join_e.outline_round_join )
                            {
                                this.m_ren.line3( lineParameters3, x8 + ( y9 - y8 ), y8 - ( x9 - x8 ), x9 + ( y9 - y8 ), y9 - ( x9 - x8 ) );
                                this.m_ren.pie( lineParameters3.x2, lineParameters3.y2, x9 + ( y9 - y8 ), y9 - ( x9 - x8 ), dv.curr.x1 + ( dv.curr.y2 - dv.curr.y1 ), dv.curr.y1 - ( dv.curr.x2 - dv.curr.x1 ) );
                            }
                            else
                            {
                                LineAABasics.bisectrix( lineParameters3, dv.curr, out dv.xb1, out dv.yb1 );
                                this.m_ren.line3( lineParameters3, x8 + ( y9 - y8 ), y8 - ( x9 - x8 ), dv.xb1, dv.yb1 );
                            }
                        }
                        else
                            this.m_ren.line1( lineParameters3, x8 + ( y9 - y8 ), y8 - ( x9 - x8 ) );
                        if ( ( dv.flags & 2 ) == 0 && this.m_line_join != rasterizer_outline_aa.outline_aa_join_e.outline_round_join )
                            LineAABasics.bisectrix( dv.curr, dv.next, out dv.xb2, out dv.yb2 );
                        this.draw( ref dv, 1, this.m_src_vertices.size() - 2 );
                        if ( ( dv.flags & 1 ) == 0 )
                        {
                            if ( this.m_line_join == rasterizer_outline_aa.outline_aa_join_e.outline_round_join )
                                this.m_ren.line3( dv.curr, dv.curr.x1 + ( dv.curr.y2 - dv.curr.y1 ), dv.curr.y1 - ( dv.curr.x2 - dv.curr.x1 ), dv.curr.x2 + ( dv.curr.y2 - dv.curr.y1 ), dv.curr.y2 - ( dv.curr.x2 - dv.curr.x1 ) );
                            else
                                this.m_ren.line3( dv.curr, dv.xb1, dv.yb1, dv.curr.x2 + ( dv.curr.y2 - dv.curr.y1 ), dv.curr.y2 - ( dv.curr.x2 - dv.curr.x1 ) );
                        }
                        else
                            this.m_ren.line2( dv.curr, dv.curr.x2 + ( dv.curr.y2 - dv.curr.y1 ), dv.curr.y2 - ( dv.curr.x2 - dv.curr.x1 ) );
                        if ( this.m_round_cap )
                        {
                            this.m_ren.semidot( new LineRenderer.CompareFunction( this.cmp_dist_end ), dv.curr.x2, dv.curr.y2, dv.curr.x2 + ( dv.curr.y2 - dv.curr.y1 ), dv.curr.y2 - ( dv.curr.x2 - dv.curr.x1 ) );
                            break;
                        }
                        break;
                }
            }
            this.m_src_vertices.remove_all();
        }

        public void add_vertex( double x, double y, Path.FlagsAndCommand cmd )
        {
            if ( Path.is_move_to( cmd ) )
            {
                this.render( false );
                this.move_to_d( x, y );
            }
            else if ( Path.is_end_poly( cmd ) )
            {
                this.render( Path.is_closed( cmd ) );
                if ( !Path.is_closed( cmd ) )
                    return;
                this.move_to( this.m_start_x, this.m_start_y );
            }
            else
                this.line_to_d( x, y );
        }

        public void add_path( IVertexSource vs )
        {
            this.add_path( vs, 0 );
        }

        public void add_path( IVertexSource vs, int path_id )
        {
            vs.rewind( path_id );
            double x;
            double y;
            Path.FlagsAndCommand cmd;
            while ( !Path.is_stop( cmd = vs.vertex( out x, out y ) ) )
                this.add_vertex( x, y, cmd );
            this.render( false );
        }

        public void RenderAllPaths( IVertexSource vs, RGBA_Bytes[ ] colors, int[ ] path_id, int num_paths )
        {
            for ( int index = 0 ; index < num_paths ; ++index )
            {
                this.m_ren.color( ( IColorType ) colors[ index ] );
                this.add_path( vs, path_id[ index ] );
            }
        }

        public enum outline_aa_join_e
        {
            outline_no_join,
            outline_miter_join,
            outline_round_join,
            outline_miter_accurate_join,
        }

        private struct draw_vars
        {
            public int idx;
            public int x1;
            public int y1;
            public int x2;
            public int y2;
            public line_parameters curr;
            public line_parameters next;
            public int lcurr;
            public int lnext;
            public int xb1;
            public int yb1;
            public int xb2;
            public int yb2;
            public int flags;
        }
    }
}
