// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.rasterizer_compound_aa
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using MatterHackers.Agg.VertexSource;

namespace MatterHackers.Agg
{
    internal sealed class rasterizer_compound_aa : IRasterizer
    {
        private rasterizer_cells_aa m_Rasterizer;
        private VectorClipper m_VectorClipper;
        private agg_basics.filling_rule_e m_filling_rule;
        private layer_order_e m_layer_order;
        private VectorPOD<rasterizer_compound_aa.style_info> m_styles;
        private VectorPOD<int> m_ast;
        private VectorPOD<byte> m_asm;
        private VectorPOD<cell_aa> m_cells;
        private VectorPOD<byte> m_cover_buf;
        private VectorPOD<int> m_master_alpha;
        private int m_min_style;
        private int m_max_style;
        private int m_start_x;
        private int m_start_y;
        private int m_scan_y;
        private int m_sl_start;
        private int m_sl_len;
        private const int aa_shift = 8;
        private const int aa_scale = 256;
        private const int aa_mask = 255;
        private const int aa_scale2 = 512;
        private const int aa_mask2 = 511;
        private const int poly_subpixel_shift = 8;

        public rasterizer_compound_aa()
        {
            this.m_Rasterizer = new rasterizer_cells_aa();
            this.m_VectorClipper = new VectorClipper();
            this.m_filling_rule = agg_basics.filling_rule_e.fill_non_zero;
            this.m_layer_order = layer_order_e.layer_direct;
            this.m_styles = new VectorPOD<rasterizer_compound_aa.style_info>();
            this.m_ast = new VectorPOD<int>();
            this.m_asm = new VectorPOD<byte>();
            this.m_cells = new VectorPOD<cell_aa>();
            this.m_cover_buf = new VectorPOD<byte>();
            this.m_master_alpha = new VectorPOD<int>();
            this.m_min_style = int.MaxValue;
            this.m_max_style = -2147483647;
            this.m_start_x = 0;
            this.m_start_y = 0;
            this.m_scan_y = int.MaxValue;
            this.m_sl_start = 0;
            this.m_sl_len = 0;
        }

        public void gamma( IGammaFunction gamma_function )
        {
            throw new NotImplementedException();
        }

        public void reset()
        {
            this.m_Rasterizer.reset();
            this.m_min_style = int.MaxValue;
            this.m_max_style = -2147483647;
            this.m_scan_y = int.MaxValue;
            this.m_sl_start = 0;
            this.m_sl_len = 0;
        }

        private void filling_rule( agg_basics.filling_rule_e filling_rule )
        {
            this.m_filling_rule = filling_rule;
        }

        private void layer_order( layer_order_e order )
        {
            this.m_layer_order = order;
        }

        private void clip_box( double x1, double y1, double x2, double y2 )
        {
            this.reset();
            this.m_VectorClipper.clip_box( this.m_VectorClipper.upscale( x1 ), this.m_VectorClipper.upscale( y1 ), this.m_VectorClipper.upscale( x2 ), this.m_VectorClipper.upscale( y2 ) );
        }

        private void reset_clipping()
        {
            this.reset();
            this.m_VectorClipper.reset_clipping();
        }

        public void styles( int left, int right )
        {
            cell_aa style_cell = new cell_aa();
            style_cell.initial();
            style_cell.left = left;
            style_cell.right = right;
            this.m_Rasterizer.style( style_cell );
            if ( left >= 0 && left < this.m_min_style )
                this.m_min_style = left;
            if ( left >= 0 && left > this.m_max_style )
                this.m_max_style = left;
            if ( right >= 0 && right < this.m_min_style )
                this.m_min_style = right;
            if ( right < 0 || right <= this.m_max_style )
                return;
            this.m_max_style = right;
        }

        public void move_to( int x, int y )
        {
            if ( this.m_Rasterizer.sorted() )
                this.reset();
            this.m_VectorClipper.move_to( this.m_start_x = this.m_VectorClipper.downscale( x ), this.m_start_y = this.m_VectorClipper.downscale( y ) );
        }

        public void line_to( int x, int y )
        {
            this.m_VectorClipper.line_to( this.m_Rasterizer, this.m_VectorClipper.downscale( x ), this.m_VectorClipper.downscale( y ) );
        }

        public void move_to_d( double x, double y )
        {
            if ( this.m_Rasterizer.sorted() )
                this.reset();
            this.m_VectorClipper.move_to( this.m_start_x = this.m_VectorClipper.upscale( x ), this.m_start_y = this.m_VectorClipper.upscale( y ) );
        }

        public void line_to_d( double x, double y )
        {
            this.m_VectorClipper.line_to( this.m_Rasterizer, this.m_VectorClipper.upscale( x ), this.m_VectorClipper.upscale( y ) );
        }

        private void add_vertex( double x, double y, Path.FlagsAndCommand cmd )
        {
            if ( Path.is_move_to( cmd ) )
                this.move_to_d( x, y );
            else if ( Path.is_vertex( cmd ) )
            {
                this.line_to_d( x, y );
            }
            else
            {
                if ( !Path.is_close( cmd ) )
                    return;
                this.m_VectorClipper.line_to( this.m_Rasterizer, this.m_start_x, this.m_start_y );
            }
        }

        private void edge( int x1, int y1, int x2, int y2 )
        {
            if ( this.m_Rasterizer.sorted() )
                this.reset();
            this.m_VectorClipper.move_to( this.m_VectorClipper.downscale( x1 ), this.m_VectorClipper.downscale( y1 ) );
            this.m_VectorClipper.line_to( this.m_Rasterizer, this.m_VectorClipper.downscale( x2 ), this.m_VectorClipper.downscale( y2 ) );
        }

        private void edge_d( double x1, double y1, double x2, double y2 )
        {
            if ( this.m_Rasterizer.sorted() )
                this.reset();
            this.m_VectorClipper.move_to( this.m_VectorClipper.upscale( x1 ), this.m_VectorClipper.upscale( y1 ) );
            this.m_VectorClipper.line_to( this.m_Rasterizer, this.m_VectorClipper.upscale( x2 ), this.m_VectorClipper.upscale( y2 ) );
        }

        private void sort()
        {
            this.m_Rasterizer.sort_cells();
        }

        public bool rewind_scanlines()
        {
            this.m_Rasterizer.sort_cells();
            if ( this.m_Rasterizer.total_cells() == 0 || this.m_max_style < this.m_min_style )
                return false;
            this.m_scan_y = this.m_Rasterizer.min_y();
            this.m_styles.Allocate( this.m_max_style - this.m_min_style + 2, 128 );
            this.allocate_master_alpha();
            return true;
        }

        public int sweep_styles()
        {
            for ( ; this.m_scan_y <= this.m_Rasterizer.max_y() ; ++this.m_scan_y )
            {
                int num1 = this.m_Rasterizer.scanline_num_cells(this.m_scan_y);
                int Offset = 0;
                cell_aa[] CellData;
                this.m_Rasterizer.scanline_cells( this.m_scan_y, out CellData, out Offset );
                int newCapacity = this.m_max_style - this.m_min_style + 2;
                int index1 = 0;
                this.m_cells.Allocate( num1 * 2, 256 );
                this.m_ast.Capacity( newCapacity, 64 );
                this.m_asm.Allocate( newCapacity + 7 >> 3, 8 );
                this.m_asm.zero();
                if ( num1 > 0 )
                {
                    this.m_asm.Array[ 0 ] |= ( byte ) 1;
                    this.m_ast.add( 0 );
                    this.m_styles.Array[ index1 ].start_cell = 0;
                    this.m_styles.Array[ index1 ].num_cells = 0;
                    this.m_styles.Array[ index1 ].last_x = -2147483647;
                    this.m_sl_start = CellData[ 0 ].x;
                    this.m_sl_len = CellData[ num1 - 1 ].x - this.m_sl_start + 1;
                    while ( num1-- != 0 )
                    {
                        int index2 = Offset++;
                        this.add_style( CellData[ index2 ].left );
                        this.add_style( CellData[ index2 ].right );
                    }
                    int num2 = 0;
                    rasterizer_compound_aa.style_info[] array = this.m_styles.Array;
                    for ( int index2 = 0 ; index2 < this.m_ast.size() ; ++index2 )
                    {
                        int index3 = this.m_ast[index2];
                        int startCell = array[index3].start_cell;
                        array[ index3 ].start_cell = num2;
                        num2 += startCell;
                    }
                    int num3 = this.m_Rasterizer.scanline_num_cells(this.m_scan_y);
                    this.m_Rasterizer.scanline_cells( this.m_scan_y, out CellData, out Offset );
                    while ( num3-- > 0 )
                    {
                        int index2 = Offset++;
                        int index3 = CellData[index2].left < 0 ? 0 : CellData[index2].left - this.m_min_style + 1;
                        if ( CellData[ index2 ].x == array[ index3 ].last_x )
                        {
                            Offset = array[ index3 ].start_cell + array[ index3 ].num_cells - 1;
                            CellData[ Offset ].area += CellData[ index2 ].area;
                            CellData[ Offset ].cover += CellData[ index2 ].cover;
                        }
                        else
                        {
                            Offset = array[ index3 ].start_cell + array[ index3 ].num_cells;
                            CellData[ Offset ].x = CellData[ index2 ].x;
                            CellData[ Offset ].area = CellData[ index2 ].area;
                            CellData[ Offset ].cover = CellData[ index2 ].cover;
                            array[ index3 ].last_x = CellData[ index2 ].x;
                            ++array[ index3 ].num_cells;
                        }
                        int index4 = CellData[index2].right < 0 ? 0 : CellData[index2].right - this.m_min_style + 1;
                        if ( CellData[ index2 ].x == array[ index4 ].last_x )
                        {
                            Offset = array[ index4 ].start_cell + array[ index4 ].num_cells - 1;
                            CellData[ Offset ].area -= CellData[ index2 ].area;
                            CellData[ Offset ].cover -= CellData[ index2 ].cover;
                        }
                        else
                        {
                            Offset = array[ index4 ].start_cell + array[ index4 ].num_cells;
                            CellData[ Offset ].x = CellData[ index2 ].x;
                            CellData[ Offset ].area = -CellData[ index2 ].area;
                            CellData[ Offset ].cover = -CellData[ index2 ].cover;
                            array[ index4 ].last_x = CellData[ index2 ].x;
                            ++array[ index4 ].num_cells;
                        }
                    }
                }
                if ( this.m_ast.size() > 1 )
                {
                    ++this.m_scan_y;
                    if ( this.m_layer_order != layer_order_e.layer_unsorted )
                    {
                        VectorPOD_RangeAdaptor dataToSort = new VectorPOD_RangeAdaptor(this.m_ast, 1, this.m_ast.size() - 1);
                        if ( this.m_layer_order != layer_order_e.layer_direct )
                            throw new NotImplementedException();
                        new QuickSort_range_adaptor_uint().Sort( dataToSort );
                    }
                    return this.m_ast.size() - 1;
                }
            }
            return 0;
        }

        public int style( int style_idx )
        {
            return this.m_ast[ style_idx + 1 ] + this.m_min_style - 1;
        }

        private bool navigate_scanline( int y )
        {
            this.m_Rasterizer.sort_cells();
            if ( this.m_Rasterizer.total_cells() == 0 || this.m_max_style < this.m_min_style || ( y < this.m_Rasterizer.min_y() || y > this.m_Rasterizer.max_y() ) )
                return false;
            this.m_scan_y = y;
            this.m_styles.Allocate( this.m_max_style - this.m_min_style + 2, 128 );
            this.allocate_master_alpha();
            return true;
        }

        private bool hit_test( int tx, int ty )
        {
            if ( !this.navigate_scanline( ty ) || this.sweep_styles() <= 0 )
                return false;
            scanline_hit_test scanlineHitTest = new scanline_hit_test(tx);
            this.sweep_scanline( ( IScanlineCache ) scanlineHitTest, -1 );
            return scanlineHitTest.hit();
        }

        private byte[ ] allocate_cover_buffer( int len )
        {
            this.m_cover_buf.Allocate( len, 256 );
            return this.m_cover_buf.Array;
        }

        private void master_alpha( int style, double alpha )
        {
            if ( style < 0 )
                return;
            while ( this.m_master_alpha.size() <= style )
                this.m_master_alpha.add( ( int ) byte.MaxValue );
            this.m_master_alpha.Array[ style ] = agg_basics.uround( alpha * ( double ) byte.MaxValue );
        }

        public void add_path( IVertexSource vs )
        {
            this.add_path( vs, 0 );
        }

        public void add_path( IVertexSource vs, int path_id )
        {
            vs.rewind( path_id );
            if ( this.m_Rasterizer.sorted() )
                this.reset();
            double x;
            double y;
            Path.FlagsAndCommand cmd;
            while ( !Path.is_stop( cmd = vs.vertex( out x, out y ) ) )
                this.add_vertex( x, y, cmd );
        }

        public int min_x()
        {
            return this.m_Rasterizer.min_x();
        }

        public int min_y()
        {
            return this.m_Rasterizer.min_y();
        }

        public int max_x()
        {
            return this.m_Rasterizer.max_x();
        }

        public int max_y()
        {
            return this.m_Rasterizer.max_y();
        }

        public int min_style()
        {
            return this.m_min_style;
        }

        public int max_style()
        {
            return this.m_max_style;
        }

        public int scanline_start()
        {
            return this.m_sl_start;
        }

        public int scanline_length()
        {
            return this.m_sl_len;
        }

        public int calculate_alpha( int area, int master_alpha )
        {
            int num = area >> 9;
            if ( num < 0 )
                num = -num;
            if ( this.m_filling_rule == agg_basics.filling_rule_e.fill_even_odd )
            {
                num &= 511;
                if ( num > 256 )
                    num = 512 - num;
            }
            if ( num > ( int ) byte.MaxValue )
                num = ( int ) byte.MaxValue;
            return num * master_alpha + ( int ) byte.MaxValue >> 8;
        }

        public bool sweep_scanline( IScanlineCache sl )
        {
            throw new NotImplementedException();
        }

        public bool sweep_scanline( IScanlineCache sl, int style_idx )
        {
            int y = this.m_scan_y - 1;
            if ( y > this.m_Rasterizer.max_y() )
                return false;
            sl.ResetSpans();
            int maxValue = (int) byte.MaxValue;
            if ( style_idx < 0 )
            {
                style_idx = 0;
            }
            else
            {
                ++style_idx;
                maxValue = this.m_master_alpha[ this.m_ast[ style_idx ] + this.m_min_style - 1 ];
            }
            rasterizer_compound_aa.style_info style = this.m_styles[this.m_ast[style_idx]];
            int numCells = style.num_cells;
            int startCell = style.start_cell;
            cell_aa cell = this.m_cells[startCell];
            int num = 0;
            while ( numCells-- != 0 )
            {
                int x = cell.x;
                int area = cell.area;
                num += cell.cover;
                cell = this.m_cells[ ++startCell ];
                if ( area != 0 )
                {
                    int alpha = this.calculate_alpha((num << 9) - area, maxValue);
                    sl.add_cell( x, alpha );
                    ++x;
                }
                if ( numCells != 0 && cell.x > x )
                {
                    int alpha = this.calculate_alpha(num << 9, maxValue);
                    if ( alpha != 0 )
                        sl.add_span( x, cell.x - x, alpha );
                }
            }
            if ( sl.num_spans() == 0 )
                return false;
            sl.finalize( y );
            return true;
        }

        private void add_style( int style_id )
        {
            if ( style_id < 0 )
                style_id = 0;
            else
                style_id -= this.m_min_style - 1;
            int index = style_id >> 3;
            int num = 1 << (style_id & 7);
            rasterizer_compound_aa.style_info[] array = this.m_styles.Array;
            if ( ( ( int ) this.m_asm[ index ] & num ) == 0 )
            {
                this.m_ast.add( style_id );
                this.m_asm.Array[ index ] |= ( byte ) num;
                array[ style_id ].start_cell = 0;
                array[ style_id ].num_cells = 0;
                array[ style_id ].last_x = -2147483647;
            }
            ++array[ style_id ].start_cell;
        }

        private void allocate_master_alpha()
        {
            while ( this.m_master_alpha.size() <= this.m_max_style )
                this.m_master_alpha.add( ( int ) byte.MaxValue );
        }

        private struct style_info
        {
            internal int start_cell;
            internal int num_cells;
            internal int last_x;
        }
    }
}
