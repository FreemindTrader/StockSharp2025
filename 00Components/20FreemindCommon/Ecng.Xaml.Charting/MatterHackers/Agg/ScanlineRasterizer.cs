// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ScanlineRasterizer
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using MatterHackers.Agg.VertexSource;

namespace MatterHackers.Agg
{
    internal sealed class ScanlineRasterizer : IRasterizer
    {
        private int[] m_gamma = new int[256];
        private rasterizer_cells_aa m_outline;
        private VectorClipper m_VectorClipper;
        private agg_basics.filling_rule_e m_filling_rule;
        private bool m_auto_close;
        private int m_start_x;
        private int m_start_y;
        private ScanlineRasterizer.status m_status;
        private int m_scan_y;

        public ScanlineRasterizer()
          : this( new VectorClipper() )
        {
        }

        public ScanlineRasterizer( VectorClipper rasterizer_sl_clip )
        {
            this.m_outline = new rasterizer_cells_aa();
            this.m_VectorClipper = rasterizer_sl_clip;
            this.m_filling_rule = agg_basics.filling_rule_e.fill_non_zero;
            this.m_auto_close = true;
            this.m_start_x = 0;
            this.m_start_y = 0;
            this.m_status = ScanlineRasterizer.status.status_initial;
            for ( int index = 0 ; index < 256 ; ++index )
                this.m_gamma[ index ] = index;
        }

        public void reset()
        {
            this.m_outline.reset();
            this.m_status = ScanlineRasterizer.status.status_initial;
        }

        public void reset_clipping()
        {
            this.reset();
            this.m_VectorClipper.reset_clipping();
        }

        public RectangleDouble GetVectorClipBox()
        {
            return new RectangleDouble( ( double ) this.m_VectorClipper.downscale( this.m_VectorClipper.clipBox.Left ), ( double ) this.m_VectorClipper.downscale( this.m_VectorClipper.clipBox.Bottom ), ( double ) this.m_VectorClipper.downscale( this.m_VectorClipper.clipBox.Right ), ( double ) this.m_VectorClipper.downscale( this.m_VectorClipper.clipBox.Top ) );
        }

        public void SetVectorClipBox( RectangleDouble clippingRect )
        {
            this.SetVectorClipBox( clippingRect.Left, clippingRect.Bottom, clippingRect.Right, clippingRect.Top );
        }

        public void SetVectorClipBox( double x1, double y1, double x2, double y2 )
        {
            this.reset();
            this.m_VectorClipper.clip_box( this.m_VectorClipper.upscale( x1 ), this.m_VectorClipper.upscale( y1 ), this.m_VectorClipper.upscale( x2 ), this.m_VectorClipper.upscale( y2 ) );
        }

        public void filling_rule( agg_basics.filling_rule_e filling_rule )
        {
            this.m_filling_rule = filling_rule;
        }

        public void auto_close( bool flag )
        {
            this.m_auto_close = flag;
        }

        public void gamma( IGammaFunction gamma_function )
        {
            for ( int index = 0 ; index < 256 ; ++index )
                this.m_gamma[ index ] = agg_basics.uround( gamma_function.GetGamma( ( double ) index / ( double ) byte.MaxValue ) * ( double ) byte.MaxValue );
        }

        private void move_to( int x, int y )
        {
            if ( this.m_outline.sorted() )
                this.reset();
            if ( this.m_auto_close )
                this.close_polygon();
            this.m_VectorClipper.move_to( this.m_start_x = this.m_VectorClipper.downscale( x ), this.m_start_y = this.m_VectorClipper.downscale( y ) );
            this.m_status = ScanlineRasterizer.status.status_move_to;
        }

        private void line_to( int x, int y )
        {
            this.m_VectorClipper.line_to( this.m_outline, this.m_VectorClipper.downscale( x ), this.m_VectorClipper.downscale( y ) );
            this.m_status = ScanlineRasterizer.status.status_line_to;
        }

        public void move_to_d( double x, double y )
        {
            if ( this.m_outline.sorted() )
                this.reset();
            if ( this.m_auto_close )
                this.close_polygon();
            this.m_VectorClipper.move_to( this.m_start_x = this.m_VectorClipper.upscale( x ), this.m_start_y = this.m_VectorClipper.upscale( y ) );
            this.m_status = ScanlineRasterizer.status.status_move_to;
        }

        public void line_to_d( double x, double y )
        {
            this.m_VectorClipper.line_to( this.m_outline, this.m_VectorClipper.upscale( x ), this.m_VectorClipper.upscale( y ) );
            this.m_status = ScanlineRasterizer.status.status_line_to;
        }

        public void close_polygon()
        {
            if ( this.m_status != ScanlineRasterizer.status.status_line_to )
                return;
            this.m_VectorClipper.line_to( this.m_outline, this.m_start_x, this.m_start_y );
            this.m_status = ScanlineRasterizer.status.status_closed;
        }

        private void add_vertex( double x, double y, Path.FlagsAndCommand PathAndFlags )
        {
            if ( Path.is_move_to( PathAndFlags ) )
                this.move_to_d( x, y );
            else if ( Path.is_vertex( PathAndFlags ) )
            {
                this.line_to_d( x, y );
            }
            else
            {
                if ( !Path.is_close( PathAndFlags ) )
                    return;
                this.close_polygon();
            }
        }

        private void edge( int x1, int y1, int x2, int y2 )
        {
            if ( this.m_outline.sorted() )
                this.reset();
            this.m_VectorClipper.move_to( this.m_VectorClipper.downscale( x1 ), this.m_VectorClipper.downscale( y1 ) );
            this.m_VectorClipper.line_to( this.m_outline, this.m_VectorClipper.downscale( x2 ), this.m_VectorClipper.downscale( y2 ) );
            this.m_status = ScanlineRasterizer.status.status_move_to;
        }

        private void edge_d( double x1, double y1, double x2, double y2 )
        {
            if ( this.m_outline.sorted() )
                this.reset();
            this.m_VectorClipper.move_to( this.m_VectorClipper.upscale( x1 ), this.m_VectorClipper.upscale( y1 ) );
            this.m_VectorClipper.line_to( this.m_outline, this.m_VectorClipper.upscale( x2 ), this.m_VectorClipper.upscale( y2 ) );
            this.m_status = ScanlineRasterizer.status.status_move_to;
        }

        public void add_path( IVertexSource vs )
        {
            this.add_path( vs, 0 );
        }

        public void add_path( IVertexSource vs, int path_id )
        {
            double x = 0.0;
            double y = 0.0;
            vs.rewind( path_id );
            if ( this.m_outline.sorted() )
                this.reset();
            Path.FlagsAndCommand PathAndFlags;
            while ( !Path.is_stop( PathAndFlags = vs.vertex( out x, out y ) ) )
                this.add_vertex( x, y, PathAndFlags );
        }

        public int min_x()
        {
            return this.m_outline.min_x();
        }

        public int min_y()
        {
            return this.m_outline.min_y();
        }

        public int max_x()
        {
            return this.m_outline.max_x();
        }

        public int max_y()
        {
            return this.m_outline.max_y();
        }

        private void sort()
        {
            if ( this.m_auto_close )
                this.close_polygon();
            this.m_outline.sort_cells();
        }

        public bool rewind_scanlines()
        {
            if ( this.m_auto_close )
                this.close_polygon();
            this.m_outline.sort_cells();
            if ( this.m_outline.total_cells() == 0 )
                return false;
            this.m_scan_y = this.m_outline.min_y();
            return true;
        }

        private bool navigate_scanline( int y )
        {
            if ( this.m_auto_close )
                this.close_polygon();
            this.m_outline.sort_cells();
            if ( this.m_outline.total_cells() == 0 || y < this.m_outline.min_y() || y > this.m_outline.max_y() )
                return false;
            this.m_scan_y = y;
            return true;
        }

        public int calculate_alpha( int area )
        {
            int index = area >> 9;
            if ( index < 0 )
                index = -index;
            if ( this.m_filling_rule == agg_basics.filling_rule_e.fill_even_odd )
            {
                index &= 511;
                if ( index > 256 )
                    index = 512 - index;
            }
            if ( index > ( int ) byte.MaxValue )
                index = ( int ) byte.MaxValue;
            return this.m_gamma[ index ];
        }

        public bool sweep_scanline( IScanlineCache scanlineCache )
        {
            for ( ; this.m_scan_y <= this.m_outline.max_y() ; ++this.m_scan_y )
            {
                scanlineCache.ResetSpans();
                int num1 = this.m_outline.scanline_num_cells(this.m_scan_y);
                cell_aa[] CellData;
                int Offset;
                this.m_outline.scanline_cells( this.m_scan_y, out CellData, out Offset );
                int num2 = 0;
                while ( num1 != 0 )
                {
                    cell_aa cellAa = CellData[Offset];
                    int x = cellAa.x;
                    int area = cellAa.area;
                    num2 += cellAa.cover;
                    while ( --num1 != 0 )
                    {
                        ++Offset;
                        cellAa = CellData[ Offset ];
                        if ( cellAa.x == x )
                        {
                            area += cellAa.area;
                            num2 += cellAa.cover;
                        }
                        else
                            break;
                    }
                    if ( area != 0 )
                    {
                        int alpha = this.calculate_alpha((num2 << 9) - area);
                        if ( alpha != 0 )
                            scanlineCache.add_cell( x, alpha );
                        ++x;
                    }
                    if ( num1 != 0 && cellAa.x > x )
                    {
                        int alpha = this.calculate_alpha(num2 << 9);
                        if ( alpha != 0 )
                            scanlineCache.add_span( x, cellAa.x - x, alpha );
                    }
                }
                if ( scanlineCache.num_spans() != 0 )
                {
                    scanlineCache.finalize( this.m_scan_y );
                    ++this.m_scan_y;
                    return true;
                }
            }
            return false;
        }

        private bool hit_test( int tx, int ty )
        {
            return this.navigate_scanline( ty );
        }

        public enum status
        {
            status_initial,
            status_move_to,
            status_line_to,
            status_closed,
        }

        public enum aa_scale_e
        {
            aa_shift = 8,
            aa_mask = 255, // 0x000000FF
            aa_scale = 256, // 0x00000100
            aa_mask2 = 511, // 0x000001FF
            aa_scale2 = 512, // 0x00000200
        }
    }
}
