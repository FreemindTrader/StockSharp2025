// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.rasterizer_cells_aa
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal sealed class rasterizer_cells_aa
    {
        private int m_num_used_cells;
        private VectorPOD<cell_aa> m_cells;
        private VectorPOD<cell_aa> m_sorted_cells;
        private VectorPOD<rasterizer_cells_aa.sorted_y> m_sorted_y;
        private QuickSort_cell_aa m_QSorter;
        private cell_aa m_curr_cell;
        private cell_aa m_style_cell;
        private int m_min_x;
        private int m_min_y;
        private int m_max_x;
        private int m_max_y;
        private bool m_sorted;

        public rasterizer_cells_aa()
        {
            this.m_QSorter = new QuickSort_cell_aa();
            this.m_sorted_cells = new VectorPOD<cell_aa>();
            this.m_sorted_y = new VectorPOD<rasterizer_cells_aa.sorted_y>();
            this.m_min_x = int.MaxValue;
            this.m_min_y = int.MaxValue;
            this.m_max_x = -2147483647;
            this.m_max_y = -2147483647;
            this.m_sorted = false;
            this.m_style_cell.initial();
            this.m_curr_cell.initial();
        }

        public void reset()
        {
            this.m_num_used_cells = 0;
            this.m_curr_cell.initial();
            this.m_style_cell.initial();
            this.m_sorted = false;
            this.m_min_x = int.MaxValue;
            this.m_min_y = int.MaxValue;
            this.m_max_x = -2147483647;
            this.m_max_y = -2147483647;
        }

        public void style( cell_aa style_cell )
        {
            this.m_style_cell.style( style_cell );
        }

        public void line( int x1, int y1, int x2, int y2 )
        {
            int num1 = 8;
            int maxValue = (int) byte.MaxValue;
            int num2 = 256;
            int num3 = x2 - x1;
            if ( num3 >= 4194304 || num3 <= -4194304 )
            {
                int num4 = x1 + x2 >> 1;
                int num5 = y1 + y2 >> 1;
                this.line( x1, y1, num4, num5 );
                this.line( num4, num5, x2, y2 );
            }
            int num6 = y2 - y1;
            int x3 = x1 >> num1;
            int num7 = x2 >> num1;
            int num8 = y1 >> num1;
            int num9 = y2 >> num1;
            int y1_1 = y1 & maxValue;
            int y2_1 = y2 & maxValue;
            if ( x3 < this.m_min_x )
                this.m_min_x = x3;
            if ( x3 > this.m_max_x )
                this.m_max_x = x3;
            if ( num8 < this.m_min_y )
                this.m_min_y = num8;
            if ( num8 > this.m_max_y )
                this.m_max_y = num8;
            if ( num7 < this.m_min_x )
                this.m_min_x = num7;
            if ( num7 > this.m_max_x )
                this.m_max_x = num7;
            if ( num9 < this.m_min_y )
                this.m_min_y = num9;
            if ( num9 > this.m_max_y )
                this.m_max_y = num9;
            this.set_curr_cell( x3, num8 );
            if ( num8 == num9 )
            {
                this.render_hline( num8, x1, y1_1, x2, y2_1 );
            }
            else
            {
                int num4 = 1;
                if ( num3 == 0 )
                {
                    int x4 = x1 >> num1;
                    int num5 = x1 - (x4 << num1) << 1;
                    int num10 = num2;
                    if ( num6 < 0 )
                    {
                        num10 = 0;
                        num4 = -1;
                    }
                    int num11 = num10 - y1_1;
                    this.m_curr_cell.cover += num11;
                    this.m_curr_cell.area += num5 * num11;
                    int y = num8 + num4;
                    this.set_curr_cell( x4, y );
                    int num12 = num10 + num10 - num2;
                    int num13 = num5 * num12;
                    while ( y != num9 )
                    {
                        this.m_curr_cell.cover = num12;
                        this.m_curr_cell.area = num13;
                        y += num4;
                        this.set_curr_cell( x4, y );
                    }
                    int num14 = y2_1 - num2 + num10;
                    this.m_curr_cell.cover += num14;
                    this.m_curr_cell.area += num5 * num14;
                }
                else
                {
                    int num5 = (num2 - y1_1) * num3;
                    int y2_2 = num2;
                    if ( num6 < 0 )
                    {
                        num5 = y1_1 * num3;
                        y2_2 = 0;
                        num4 = -1;
                        num6 = -num6;
                    }
                    int num10 = num5 / num6;
                    int num11 = num5 % num6;
                    if ( num11 < 0 )
                    {
                        --num10;
                        num11 += num6;
                    }
                    int num12 = x1 + num10;
                    this.render_hline( num8, x1, y1_1, num12, y2_2 );
                    int num13 = num8 + num4;
                    this.set_curr_cell( num12 >> num1, num13 );
                    if ( num13 != num9 )
                    {
                        int num14 = num2 * num3;
                        int num15 = num14 / num6;
                        int num16 = num14 % num6;
                        if ( num16 < 0 )
                        {
                            --num15;
                            num16 += num6;
                        }
                        int num17 = num11 - num6;
                        while ( num13 != num9 )
                        {
                            int num18 = num15;
                            num17 += num16;
                            if ( num17 >= 0 )
                            {
                                num17 -= num6;
                                ++num18;
                            }
                            int x2_1 = num12 + num18;
                            this.render_hline( num13, num12, num2 - y2_2, x2_1, y2_2 );
                            num12 = x2_1;
                            num13 += num4;
                            this.set_curr_cell( num12 >> num1, num13 );
                        }
                    }
                    this.render_hline( num13, num12, num2 - y2_2, x2, y2_1 );
                }
            }
        }

        public int min_x()
        {
            return this.m_min_x;
        }

        public int min_y()
        {
            return this.m_min_y;
        }

        public int max_x()
        {
            return this.m_max_x;
        }

        public int max_y()
        {
            return this.m_max_y;
        }

        public void sort_cells()
        {
            if ( this.m_sorted )
                return;
            this.add_curr_cell();
            this.m_curr_cell.x = int.MaxValue;
            this.m_curr_cell.y = int.MaxValue;
            this.m_curr_cell.cover = 0;
            this.m_curr_cell.area = 0;
            if ( this.m_num_used_cells == 0 )
                return;
            this.m_sorted_cells.Allocate( this.m_num_used_cells );
            this.m_sorted_y.Allocate( this.m_max_y - this.m_min_y + 1 );
            this.m_sorted_y.zero();
            cell_aa[] array1 = this.m_cells.Array;
            rasterizer_cells_aa.sorted_y[] array2 = this.m_sorted_y.Array;
            cell_aa[] array3 = this.m_sorted_cells.Array;
            for ( int index1 = 0 ; index1 < this.m_num_used_cells ; ++index1 )
            {
                int index2 = array1[index1].y - this.m_min_y;
                ++array2[ index2 ].start;
            }
            int num1 = 0;
            int num2 = this.m_sorted_y.size();
            for ( int index = 0 ; index < num2 ; ++index )
            {
                int start = array2[index].start;
                array2[ index ].start = num1;
                num1 += start;
            }
            for ( int index1 = 0 ; index1 < this.m_num_used_cells ; ++index1 )
            {
                int index2 = array1[index1].y - this.m_min_y;
                int start = array2[index2].start;
                int num3 = array2[index2].num;
                array3[ start + num3 ] = array1[ index1 ];
                ++array2[ index2 ].num;
            }
            for ( int index = 0 ; index < num2 ; ++index )
            {
                if ( array2[ index ].num != 0 )
                    this.m_QSorter.Sort( array3, array2[ index ].start, array2[ index ].start + array2[ index ].num - 1 );
            }
            this.m_sorted = true;
        }

        public int total_cells()
        {
            return this.m_num_used_cells;
        }

        public int scanline_num_cells( int y )
        {
            return this.m_sorted_y.data()[ y - this.m_min_y ].num;
        }

        public void scanline_cells( int y, out cell_aa[ ] CellData, out int Offset )
        {
            CellData = this.m_sorted_cells.data();
            Offset = this.m_sorted_y[ y - this.m_min_y ].start;
        }

        public bool sorted()
        {
            return this.m_sorted;
        }

        private void set_curr_cell( int x, int y )
        {
            if ( !this.m_curr_cell.not_equal( x, y, this.m_style_cell ) )
                return;
            this.add_curr_cell();
            this.m_curr_cell.style( this.m_style_cell );
            this.m_curr_cell.x = x;
            this.m_curr_cell.y = y;
            this.m_curr_cell.cover = 0;
            this.m_curr_cell.area = 0;
        }

        private void add_curr_cell()
        {
            if ( ( this.m_curr_cell.area | this.m_curr_cell.cover ) == 0 || this.m_num_used_cells >= 4194304 )
                return;
            this.allocate_cells_if_required();
            this.m_cells.data()[ this.m_num_used_cells ].Set( this.m_curr_cell );
            ++this.m_num_used_cells;
        }

        private void allocate_cells_if_required()
        {
            if ( this.m_cells != null && this.m_num_used_cells + 1 < this.m_cells.Capacity() || this.m_num_used_cells >= 4194304 )
                return;
            VectorPOD<cell_aa> vectorPod = new VectorPOD<cell_aa>(this.m_num_used_cells + 4096);
            if ( this.m_cells != null )
                vectorPod.CopyFrom( this.m_cells );
            this.m_cells = vectorPod;
        }

        private void render_hline( int ey, int x1, int y1, int x2, int y2 )
        {
            int num1 = x1 >> 8;
            int x3 = x2 >> 8;
            int num2 = x1 & (int) byte.MaxValue;
            int num3 = x2 & (int) byte.MaxValue;
            if ( y1 == y2 )
                this.set_curr_cell( x3, ey );
            else if ( num1 == x3 )
            {
                int num4 = y2 - y1;
                this.m_curr_cell.cover += num4;
                this.m_curr_cell.area += ( num2 + num3 ) * num4;
            }
            else
            {
                int num4 = (256 - num2) * (y2 - y1);
                int num5 = 256;
                int num6 = 1;
                int num7 = x2 - x1;
                if ( num7 < 0 )
                {
                    num4 = num2 * ( y2 - y1 );
                    num5 = 0;
                    num6 = -1;
                    num7 = -num7;
                }
                int num8 = num4 / num7;
                int num9 = num4 % num7;
                if ( num9 < 0 )
                {
                    --num8;
                    num9 += num7;
                }
                this.m_curr_cell.cover += num8;
                this.m_curr_cell.area += ( num2 + num5 ) * num8;
                int x4 = num1 + num6;
                this.set_curr_cell( x4, ey );
                y1 += num8;
                if ( x4 != x3 )
                {
                    int num10 = 256 * (y2 - y1 + num8);
                    int num11 = num10 / num7;
                    int num12 = num10 % num7;
                    if ( num12 < 0 )
                    {
                        --num11;
                        num12 += num7;
                    }
                    int num13 = num9 - num7;
                    while ( x4 != x3 )
                    {
                        int num14 = num11;
                        num13 += num12;
                        if ( num13 >= 0 )
                        {
                            num13 -= num7;
                            ++num14;
                        }
                        this.m_curr_cell.cover += num14;
                        this.m_curr_cell.area += 256 * num14;
                        y1 += num14;
                        x4 += num6;
                        this.set_curr_cell( x4, ey );
                    }
                }
                int num15 = y2 - y1;
                this.m_curr_cell.cover += num15;
                this.m_curr_cell.area += ( num3 + 256 - num5 ) * num15;
            }
        }

        private static void swap_cells( cell_aa a, cell_aa b )
        {
            cell_aa cellAa = a;
            a = b;
            b = cellAa;
        }

        private enum cell_block_scale_e
        {
            cell_block_shift = 12, // 0x0000000C
            cell_block_pool = 256, // 0x00000100
            cell_block_mask = 4095, // 0x00000FFF
            cell_block_size = 4096, // 0x00001000
            cell_block_limit = 4194304, // 0x00400000
        }

        private struct sorted_y
        {
            internal int start;
            internal int num;
        }

        private enum dx_limit_e
        {
            dx_limit = 4194304, // 0x00400000
        }

        private enum qsort
        {
            qsort_threshold = 9,
        }
    }
}
