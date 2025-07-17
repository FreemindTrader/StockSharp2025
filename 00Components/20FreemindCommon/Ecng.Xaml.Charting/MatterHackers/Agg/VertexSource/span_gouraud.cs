// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.span_gouraud
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg.VertexSource
{
    internal class span_gouraud : IVertexSource
    {
        private span_gouraud.coord_type[] m_coord = new span_gouraud.coord_type[3];
        private double[] m_x = new double[8];
        private double[] m_y = new double[8];
        private Path.FlagsAndCommand[] m_cmd = new Path.FlagsAndCommand[8];
        private int m_vertex;

        public span_gouraud()
        {
            this.m_vertex = 0;
            this.m_cmd[ 0 ] = Path.FlagsAndCommand.CommandStop;
        }

        public span_gouraud( RGBA_Bytes c1, RGBA_Bytes c2, RGBA_Bytes c3, double x1, double y1, double x2, double y2, double x3, double y3, double d )
        {
            this.m_vertex = 0;
            this.colors( ( IColorType ) c1, ( IColorType ) c2, ( IColorType ) c3 );
            this.triangle( x1, y1, x2, y2, x3, y3, d );
        }

        public void colors( IColorType c1, IColorType c2, IColorType c3 )
        {
            this.m_coord[ 0 ].color = c1.GetAsRGBA_Bytes();
            this.m_coord[ 1 ].color = c2.GetAsRGBA_Bytes();
            this.m_coord[ 2 ].color = c3.GetAsRGBA_Bytes();
        }

        public void triangle( double x1, double y1, double x2, double y2, double x3, double y3, double d )
        {
            this.m_coord[ 0 ].x = this.m_x[ 0 ] = x1;
            this.m_coord[ 0 ].y = this.m_y[ 0 ] = y1;
            this.m_coord[ 1 ].x = this.m_x[ 1 ] = x2;
            this.m_coord[ 1 ].y = this.m_y[ 1 ] = y2;
            this.m_coord[ 2 ].x = this.m_x[ 2 ] = x3;
            this.m_coord[ 2 ].y = this.m_y[ 2 ] = y3;
            this.m_cmd[ 0 ] = Path.FlagsAndCommand.CommandMoveTo;
            this.m_cmd[ 1 ] = Path.FlagsAndCommand.CommandLineTo;
            this.m_cmd[ 2 ] = Path.FlagsAndCommand.CommandLineTo;
            this.m_cmd[ 3 ] = Path.FlagsAndCommand.CommandStop;
            if ( d == 0.0 )
                return;
            agg_math.dilate_triangle( this.m_coord[ 0 ].x, this.m_coord[ 0 ].y, this.m_coord[ 1 ].x, this.m_coord[ 1 ].y, this.m_coord[ 2 ].x, this.m_coord[ 2 ].y, this.m_x, this.m_y, d );
            agg_math.calc_intersection( this.m_x[ 4 ], this.m_y[ 4 ], this.m_x[ 5 ], this.m_y[ 5 ], this.m_x[ 0 ], this.m_y[ 0 ], this.m_x[ 1 ], this.m_y[ 1 ], out this.m_coord[ 0 ].x, out this.m_coord[ 0 ].y );
            agg_math.calc_intersection( this.m_x[ 0 ], this.m_y[ 0 ], this.m_x[ 1 ], this.m_y[ 1 ], this.m_x[ 2 ], this.m_y[ 2 ], this.m_x[ 3 ], this.m_y[ 3 ], out this.m_coord[ 1 ].x, out this.m_coord[ 1 ].y );
            agg_math.calc_intersection( this.m_x[ 2 ], this.m_y[ 2 ], this.m_x[ 3 ], this.m_y[ 3 ], this.m_x[ 4 ], this.m_y[ 4 ], this.m_x[ 5 ], this.m_y[ 5 ], out this.m_coord[ 2 ].x, out this.m_coord[ 2 ].y );
            this.m_cmd[ 3 ] = Path.FlagsAndCommand.CommandLineTo;
            this.m_cmd[ 4 ] = Path.FlagsAndCommand.CommandLineTo;
            this.m_cmd[ 5 ] = Path.FlagsAndCommand.CommandLineTo;
            this.m_cmd[ 6 ] = Path.FlagsAndCommand.CommandStop;
        }

        public void rewind( int idx )
        {
            this.m_vertex = 0;
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            x = this.m_x[ this.m_vertex ];
            y = this.m_y[ this.m_vertex ];
            return this.m_cmd[ this.m_vertex++ ];
        }

        protected void arrange_vertices( span_gouraud.coord_type[ ] coord )
        {
            coord[ 0 ] = this.m_coord[ 0 ];
            coord[ 1 ] = this.m_coord[ 1 ];
            coord[ 2 ] = this.m_coord[ 2 ];
            if ( this.m_coord[ 0 ].y > this.m_coord[ 2 ].y )
            {
                coord[ 0 ] = this.m_coord[ 2 ];
                coord[ 2 ] = this.m_coord[ 0 ];
            }
            if ( coord[ 0 ].y > coord[ 1 ].y )
            {
                span_gouraud.coord_type coordType = coord[1];
                coord[ 1 ] = coord[ 0 ];
                coord[ 0 ] = coordType;
            }
            if ( coord[ 1 ].y <= coord[ 2 ].y )
                return;
            span_gouraud.coord_type coordType1 = coord[2];
            coord[ 2 ] = coord[ 1 ];
            coord[ 1 ] = coordType1;
        }

        internal struct coord_type
        {
            public double x;
            public double y;
            public RGBA_Bytes color;
        }
    }
}
