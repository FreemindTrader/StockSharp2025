// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.PathStorage
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Transform;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.VertexSource
{
    internal class PathStorage : IVertexSource, IVertexDest
    {
        private PathStorage.VertexStorage vertices;
        private int iteratorIndex;

        public PathStorage()
        {
            this.vertices = new PathStorage.VertexStorage();
        }

        public void add( Vector2 vertex )
        {
            throw new NotImplementedException();
        }

        public int size()
        {
            return this.vertices.size();
        }

        public Vector2 this[ int i ]
        {
            get
            {
                throw new NotImplementedException( "make this work" );
            }
        }

        public void remove_all()
        {
            this.vertices.remove_all();
            this.iteratorIndex = 0;
        }

        public void free_all()
        {
            this.vertices.free_all();
            this.iteratorIndex = 0;
        }

        public int start_new_path()
        {
            if ( !Path.is_stop( this.vertices.last_command() ) )
                this.vertices.AddVertex( 0.0, 0.0, Path.FlagsAndCommand.CommandStop );
            return this.vertices.total_vertices();
        }

        public void rel_to_abs( ref double x, ref double y )
        {
            double x1;
            double y1;
            if ( this.vertices.total_vertices() == 0 || !Path.is_vertex( this.vertices.last_vertex( out x1, out y1 ) ) )
                return;
            x += x1;
            y += y1;
        }

        public void MoveTo( double x, double y )
        {
            this.vertices.AddVertex( x, y, Path.FlagsAndCommand.CommandMoveTo );
        }

        public void LineTo( double x, double y )
        {
            this.vertices.AddVertex( x, y, Path.FlagsAndCommand.CommandLineTo );
        }

        public void HorizontalLineTo( double x )
        {
            this.vertices.AddVertex( x, this.GetLastY(), Path.FlagsAndCommand.CommandLineTo );
        }

        public void VerticalLineTo( double y )
        {
            this.vertices.AddVertex( this.GetLastX(), y, Path.FlagsAndCommand.CommandLineTo );
        }

        public void curve3( double xControl, double yControl, double x, double y )
        {
            this.vertices.AddVertex( xControl, yControl, Path.FlagsAndCommand.CommandCurve3 );
            this.vertices.AddVertex( x, y, Path.FlagsAndCommand.CommandCurve3 );
        }

        public void curve3_rel( double dx_ctrl, double dy_ctrl, double dx_to, double dy_to )
        {
            this.rel_to_abs( ref dx_ctrl, ref dy_ctrl );
            this.rel_to_abs( ref dx_to, ref dy_to );
            this.vertices.AddVertex( dx_ctrl, dy_ctrl, Path.FlagsAndCommand.CommandCurve3 );
            this.vertices.AddVertex( dx_to, dy_to, Path.FlagsAndCommand.CommandCurve3 );
        }

        public void curve3( double x, double y )
        {
            double x1;
            double y1;
            if ( !Path.is_vertex( this.vertices.last_vertex( out x1, out y1 ) ) )
                return;
            double x2;
            double y2;
            double xControl;
            double yControl;
            if ( Path.is_curve( this.vertices.prev_vertex( out x2, out y2 ) ) )
            {
                xControl = x1 + x1 - x2;
                yControl = y1 + y1 - y2;
            }
            else
            {
                xControl = x1;
                yControl = y1;
            }
            this.curve3( xControl, yControl, x, y );
        }

        public void curve3_rel( double dx_to, double dy_to )
        {
            this.rel_to_abs( ref dx_to, ref dy_to );
            this.curve3( dx_to, dy_to );
        }

        public void curve4( double x_ctrl1, double y_ctrl1, double x_ctrl2, double y_ctrl2, double x_to, double y_to )
        {
            this.vertices.AddVertex( x_ctrl1, y_ctrl1, Path.FlagsAndCommand.CommandCurve4 );
            this.vertices.AddVertex( x_ctrl2, y_ctrl2, Path.FlagsAndCommand.CommandCurve4 );
            this.vertices.AddVertex( x_to, y_to, Path.FlagsAndCommand.CommandCurve4 );
        }

        public void curve4_rel( double dx_ctrl1, double dy_ctrl1, double dx_ctrl2, double dy_ctrl2, double dx_to, double dy_to )
        {
            this.rel_to_abs( ref dx_ctrl1, ref dy_ctrl1 );
            this.rel_to_abs( ref dx_ctrl2, ref dy_ctrl2 );
            this.rel_to_abs( ref dx_to, ref dy_to );
            this.vertices.AddVertex( dx_ctrl1, dy_ctrl1, Path.FlagsAndCommand.CommandCurve4 );
            this.vertices.AddVertex( dx_ctrl2, dy_ctrl2, Path.FlagsAndCommand.CommandCurve4 );
            this.vertices.AddVertex( dx_to, dy_to, Path.FlagsAndCommand.CommandCurve4 );
        }

        public void curve4( double x_ctrl2, double y_ctrl2, double x_to, double y_to )
        {
            double x1;
            double y1;
            if ( !Path.is_vertex( this.last_vertex( out x1, out y1 ) ) )
                return;
            double x2;
            double y2;
            double x_ctrl1;
            double y_ctrl1;
            if ( Path.is_curve( this.prev_vertex( out x2, out y2 ) ) )
            {
                x_ctrl1 = x1 + x1 - x2;
                y_ctrl1 = y1 + y1 - y2;
            }
            else
            {
                x_ctrl1 = x1;
                y_ctrl1 = y1;
            }
            this.curve4( x_ctrl1, y_ctrl1, x_ctrl2, y_ctrl2, x_to, y_to );
        }

        public void curve4_rel( double dx_ctrl2, double dy_ctrl2, double dx_to, double dy_to )
        {
            this.rel_to_abs( ref dx_ctrl2, ref dy_ctrl2 );
            this.rel_to_abs( ref dx_to, ref dy_to );
            this.curve4( dx_ctrl2, dy_ctrl2, dx_to, dy_to );
        }

        public int total_vertices()
        {
            return this.vertices.total_vertices();
        }

        public Path.FlagsAndCommand last_vertex( out double x, out double y )
        {
            return this.vertices.last_vertex( out x, out y );
        }

        public Path.FlagsAndCommand prev_vertex( out double x, out double y )
        {
            return this.vertices.prev_vertex( out x, out y );
        }

        public double GetLastX()
        {
            return this.vertices.last_x();
        }

        public double GetLastY()
        {
            return this.vertices.last_y();
        }

        public Path.FlagsAndCommand vertex( int index, out double x, out double y )
        {
            return this.vertices.vertex( index, out x, out y );
        }

        public Path.FlagsAndCommand command( int index )
        {
            return this.vertices.command( index );
        }

        public void modify_vertex( int index, double x, double y )
        {
            this.vertices.modify_vertex( index, x, y );
        }

        public void modify_vertex( int index, double x, double y, Path.FlagsAndCommand PathAndFlags )
        {
            this.vertices.modify_vertex( index, x, y, PathAndFlags );
        }

        public void modify_command( int index, Path.FlagsAndCommand PathAndFlags )
        {
            this.vertices.modify_command( index, PathAndFlags );
        }

        public virtual void rewind( int path_id )
        {
            this.iteratorIndex = path_id;
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            if ( this.iteratorIndex < this.vertices.total_vertices() )
                return this.vertices.vertex( this.iteratorIndex++, out x, out y );
            x = 0.0;
            y = 0.0;
            return Path.FlagsAndCommand.CommandStop;
        }

        public int arrange_polygon_orientation( int start, Path.FlagsAndCommand orientation )
        {
            if ( orientation == Path.FlagsAndCommand.CommandStop )
                return start;
            while ( start < this.vertices.total_vertices() && !Path.is_vertex( this.vertices.command( start ) ) )
                ++start;
            while ( start + 1 < this.vertices.total_vertices() && Path.is_move_to( this.vertices.command( start ) ) && Path.is_move_to( this.vertices.command( start + 1 ) ) )
                ++start;
            int num = start + 1;
            while ( num < this.vertices.total_vertices() && !Path.is_next_poly( this.vertices.command( num ) ) )
                ++num;
            if ( num - start > 2 && this.perceive_polygon_orientation( start, num ) != orientation )
            {
                this.invert_polygon( start, num );
                Path.FlagsAndCommand flagsAndCommand;
                while ( num < this.vertices.total_vertices() && Path.is_end_poly( flagsAndCommand = this.vertices.command( num ) ) )
                    this.vertices.modify_command( num++, flagsAndCommand | orientation );
            }
            return num;
        }

        public int arrange_orientations( int start, Path.FlagsAndCommand orientation )
        {
            if ( orientation != Path.FlagsAndCommand.CommandStop )
            {
                while ( start < this.vertices.total_vertices() )
                {
                    start = this.arrange_polygon_orientation( start, orientation );
                    if ( Path.is_stop( this.vertices.command( start ) ) )
                    {
                        ++start;
                        break;
                    }
                }
            }
            return start;
        }

        public void arrange_orientations_all_paths( Path.FlagsAndCommand orientation )
        {
            if ( orientation == Path.FlagsAndCommand.CommandStop )
                return;
            int start = 0;
            while ( start < this.vertices.total_vertices() )
                start = this.arrange_orientations( start, orientation );
        }

        public void flip_x( double x1, double x2 )
        {
            for ( int index = 0 ; index < this.vertices.total_vertices() ; ++index )
            {
                double x;
                double y;
                if ( Path.is_vertex( this.vertices.vertex( index, out x, out y ) ) )
                    this.vertices.modify_vertex( index, x2 - x + x1, y );
            }
        }

        public void flip_y( double y1, double y2 )
        {
            for ( int index = 0 ; index < this.vertices.total_vertices() ; ++index )
            {
                double x;
                double y;
                if ( Path.is_vertex( this.vertices.vertex( index, out x, out y ) ) )
                    this.vertices.modify_vertex( index, x, y2 - y + y1 );
            }
        }

        public void end_poly()
        {
            this.close_polygon( Path.FlagsAndCommand.FlagClose );
        }

        public void end_poly( Path.FlagsAndCommand flags )
        {
            if ( !Path.is_vertex( this.vertices.last_command() ) )
                return;
            this.vertices.AddVertex( 0.0, 0.0, Path.FlagsAndCommand.CommandEndPoly | flags );
        }

        public void ClosePolygon()
        {
            this.close_polygon( Path.FlagsAndCommand.CommandStop );
        }

        public void close_polygon( Path.FlagsAndCommand flags )
        {
            this.end_poly( Path.FlagsAndCommand.FlagClose | flags );
        }

        public void concat_path( IVertexSource vs )
        {
            this.concat_path( vs, 0 );
        }

        public void concat_path( IVertexSource vs, int path_id )
        {
            vs.rewind( path_id );
            double x;
            double y;
            Path.FlagsAndCommand CommandAndFlags;
            while ( !Path.is_stop( CommandAndFlags = vs.vertex( out x, out y ) ) )
                this.vertices.AddVertex( x, y, CommandAndFlags );
        }

        public void join_path( PathStorage vs )
        {
            this.join_path( vs, 0 );
        }

        public void join_path( PathStorage vs, int path_id )
        {
            vs.rewind( path_id );
            double x1;
            double y1;
            Path.FlagsAndCommand flagsAndCommand = vs.vertex(out x1, out y1);
            if ( Path.is_stop( flagsAndCommand ) )
                return;
            if ( Path.is_vertex( flagsAndCommand ) )
            {
                double x2;
                double y2;
                Path.FlagsAndCommand c = this.last_vertex(out x2, out y2);
                if ( Path.is_vertex( c ) )
                {
                    if ( agg_math.calc_distance( x1, y1, x2, y2 ) > 1E-14 )
                    {
                        if ( Path.is_move_to( flagsAndCommand ) )
                            flagsAndCommand = Path.FlagsAndCommand.CommandLineTo;
                        this.vertices.AddVertex( x1, y1, flagsAndCommand );
                    }
                }
                else
                {
                    if ( Path.is_stop( c ) )
                        flagsAndCommand = Path.FlagsAndCommand.CommandMoveTo;
                    else if ( Path.is_move_to( flagsAndCommand ) )
                        flagsAndCommand = Path.FlagsAndCommand.CommandLineTo;
                    this.vertices.AddVertex( x1, y1, flagsAndCommand );
                }
            }
            Path.FlagsAndCommand c1;
            while ( !Path.is_stop( c1 = vs.vertex( out x1, out y1 ) ) )
                this.vertices.AddVertex( x1, y1, Path.is_move_to( c1 ) ? Path.FlagsAndCommand.CommandLineTo : c1 );
        }

        public void translate( double dx, double dy )
        {
            this.translate( dx, dy, 0 );
        }

        public void translate( double dx, double dy, int path_id )
        {
            for ( int index = this.vertices.total_vertices() ; path_id < index ; ++path_id )
            {
                double x;
                double y;
                Path.FlagsAndCommand c = this.vertices.vertex(path_id, out x, out y);
                if ( Path.is_stop( c ) )
                    break;
                if ( Path.is_vertex( c ) )
                {
                    x += dx;
                    y += dy;
                    this.vertices.modify_vertex( path_id, x, y );
                }
            }
        }

        public void translate_all_paths( double dx, double dy )
        {
            int num = this.vertices.total_vertices();
            for ( int index = 0 ; index < num ; ++index )
            {
                double x;
                double y;
                if ( Path.is_vertex( this.vertices.vertex( index, out x, out y ) ) )
                {
                    x += dx;
                    y += dy;
                    this.vertices.modify_vertex( index, x, y );
                }
            }
        }

        public void transform( Affine trans )
        {
            this.transform( trans, 0 );
        }

        public void transform( Affine trans, int path_id )
        {
            for ( int index = this.vertices.total_vertices() ; path_id < index ; ++path_id )
            {
                double x;
                double y;
                Path.FlagsAndCommand c = this.vertices.vertex(path_id, out x, out y);
                if ( Path.is_stop( c ) )
                    break;
                if ( Path.is_vertex( c ) )
                {
                    trans.transform( ref x, ref y );
                    this.vertices.modify_vertex( path_id, x, y );
                }
            }
        }

        public void transform_all_paths( Affine trans )
        {
            int num = this.vertices.total_vertices();
            for ( int index = 0 ; index < num ; ++index )
            {
                double x;
                double y;
                if ( Path.is_vertex( this.vertices.vertex( index, out x, out y ) ) )
                {
                    trans.transform( ref x, ref y );
                    this.vertices.modify_vertex( index, x, y );
                }
            }
        }

        public void invert_polygon( int start )
        {
            while ( start < this.vertices.total_vertices() && !Path.is_vertex( this.vertices.command( start ) ) )
                ++start;
            while ( start + 1 < this.vertices.total_vertices() && Path.is_move_to( this.vertices.command( start ) ) && Path.is_move_to( this.vertices.command( start + 1 ) ) )
                ++start;
            int num = start + 1;
            while ( num < this.vertices.total_vertices() && !Path.is_next_poly( this.vertices.command( num ) ) )
                ++num;
            this.invert_polygon( start, num );
        }

        private Path.FlagsAndCommand perceive_polygon_orientation( int start, int end )
        {
            int num1 = end - start;
            double num2 = 0.0;
            for ( int index = 0 ; index < num1 ; ++index )
            {
                double x1;
                double y1;
                int num3 = (int) this.vertices.vertex(start + index, out x1, out y1);
                double x2;
                double y2;
                int num4 = (int) this.vertices.vertex(start + (index + 1) % num1, out x2, out y2);
                num2 += x1 * y2 - y1 * x2;
            }
            return num2 >= 0.0 ? Path.FlagsAndCommand.FlagCCW : Path.FlagsAndCommand.FlagCW;
        }

        private void invert_polygon( int start, int end )
        {
            Path.FlagsAndCommand CommandAndFlags = this.vertices.command(start);
            --end;
            for ( int index = start ; index < end ; ++index )
                this.vertices.modify_command( index, this.vertices.command( index + 1 ) );
            this.vertices.modify_command( end, CommandAndFlags );
            while ( end > start )
                this.vertices.swap_vertices( start++, end-- );
        }

        private class VertexStorage
        {
            private int m_num_vertices;
            private int m_allocated_vertices;
            private double[] m_coord_x;
            private double[] m_coord_y;
            private Path.FlagsAndCommand[] m_CommandAndFlags;

            public void free_all()
            {
                this.m_coord_x = ( double[ ] ) null;
                this.m_coord_y = ( double[ ] ) null;
                this.m_CommandAndFlags = ( Path.FlagsAndCommand[ ] ) null;
                this.m_num_vertices = 0;
            }

            public int size()
            {
                return this.m_num_vertices;
            }

            public void remove_all()
            {
                this.m_num_vertices = 0;
            }

            public void AddVertex( double x, double y, Path.FlagsAndCommand CommandAndFlags )
            {
                this.allocate_if_required( this.m_num_vertices );
                this.m_coord_x[ this.m_num_vertices ] = x;
                this.m_coord_y[ this.m_num_vertices ] = y;
                this.m_CommandAndFlags[ this.m_num_vertices ] = CommandAndFlags;
                ++this.m_num_vertices;
            }

            public void modify_vertex( int index, double x, double y )
            {
                this.m_coord_x[ index ] = x;
                this.m_coord_y[ index ] = y;
            }

            public void modify_vertex( int index, double x, double y, Path.FlagsAndCommand CommandAndFlags )
            {
                this.m_coord_x[ index ] = x;
                this.m_coord_y[ index ] = y;
                this.m_CommandAndFlags[ index ] = CommandAndFlags;
            }

            public void modify_command( int index, Path.FlagsAndCommand CommandAndFlags )
            {
                this.m_CommandAndFlags[ index ] = CommandAndFlags;
            }

            public void swap_vertices( int v1, int v2 )
            {
                double num1 = this.m_coord_x[v1];
                this.m_coord_x[ v1 ] = this.m_coord_x[ v2 ];
                this.m_coord_x[ v2 ] = num1;
                double num2 = this.m_coord_y[v1];
                this.m_coord_y[ v1 ] = this.m_coord_y[ v2 ];
                this.m_coord_y[ v2 ] = num2;
                Path.FlagsAndCommand commandAndFlag = this.m_CommandAndFlags[v1];
                this.m_CommandAndFlags[ v1 ] = this.m_CommandAndFlags[ v2 ];
                this.m_CommandAndFlags[ v2 ] = commandAndFlag;
            }

            public Path.FlagsAndCommand last_command()
            {
                if ( this.m_num_vertices != 0 )
                    return this.command( this.m_num_vertices - 1 );
                return Path.FlagsAndCommand.CommandStop;
            }

            public Path.FlagsAndCommand last_vertex( out double x, out double y )
            {
                if ( this.m_num_vertices != 0 )
                    return this.vertex( this.m_num_vertices - 1, out x, out y );
                x = 0.0;
                y = 0.0;
                return Path.FlagsAndCommand.CommandStop;
            }

            public Path.FlagsAndCommand prev_vertex( out double x, out double y )
            {
                if ( this.m_num_vertices > 1 )
                    return this.vertex( this.m_num_vertices - 2, out x, out y );
                x = 0.0;
                y = 0.0;
                return Path.FlagsAndCommand.CommandStop;
            }

            public double last_x()
            {
                if ( this.m_num_vertices > 0 )
                    return this.m_coord_x[ this.m_num_vertices - 1 ];
                return 0.0;
            }

            public double last_y()
            {
                if ( this.m_num_vertices > 0 )
                    return this.m_coord_y[ this.m_num_vertices - 1 ];
                return 0.0;
            }

            public int total_vertices()
            {
                return this.m_num_vertices;
            }

            public Path.FlagsAndCommand vertex( int index, out double x, out double y )
            {
                x = this.m_coord_x[ index ];
                y = this.m_coord_y[ index ];
                return this.m_CommandAndFlags[ index ];
            }

            public Path.FlagsAndCommand command( int index )
            {
                return this.m_CommandAndFlags[ index ];
            }

            private void allocate_if_required( int indexToAdd )
            {
                if ( indexToAdd < this.m_num_vertices )
                    return;
                int length;
                for ( ; indexToAdd >= this.m_allocated_vertices ; this.m_allocated_vertices = length )
                {
                    length = this.m_allocated_vertices + 256;
                    double[] numArray1 = new double[length];
                    double[] numArray2 = new double[length];
                    Path.FlagsAndCommand[] flagsAndCommandArray = new Path.FlagsAndCommand[length];
                    if ( this.m_coord_x != null )
                    {
                        for ( int index = 0 ; index < this.m_num_vertices ; ++index )
                        {
                            numArray1[ index ] = this.m_coord_x[ index ];
                            numArray2[ index ] = this.m_coord_y[ index ];
                            flagsAndCommandArray[ index ] = this.m_CommandAndFlags[ index ];
                        }
                    }
                    this.m_coord_x = numArray1;
                    this.m_coord_y = numArray2;
                    this.m_CommandAndFlags = flagsAndCommandArray;
                }
            }
        }
    }
}
