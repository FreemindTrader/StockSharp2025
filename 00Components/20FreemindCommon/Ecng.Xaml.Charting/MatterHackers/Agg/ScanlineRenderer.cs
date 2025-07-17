// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ScanlineRenderer
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Image;
using MatterHackers.Agg.VertexSource;

namespace MatterHackers.Agg
{
    internal class ScanlineRenderer
    {
        private VectorPOD<RGBA_Bytes> tempSpanColors = new VectorPOD<RGBA_Bytes>();
        private VectorPOD<RGBA_Floats> tempSpanColorsFloats = new VectorPOD<RGBA_Floats>();

        public void render_scanlines_aa_solid( IImageByte destImage, IRasterizer rasterizer, IScanlineCache scanLine, Func<int, int, RGBA_Bytes> getColorCb )
        {
            if ( !rasterizer.rewind_scanlines() )
                return;
            scanLine.reset( rasterizer.min_x(), rasterizer.max_x() );
            while ( rasterizer.sweep_scanline( scanLine ) )
                this.RenderSolidSingleScanLine( destImage, scanLine, getColorCb );
        }

        public void render_scanlines_aa_solid( IImageByte destImage, IRasterizer rasterizer, IScanlineCache scanLine, RGBA_Bytes color )
        {
            if ( !rasterizer.rewind_scanlines() )
                return;
            scanLine.reset( rasterizer.min_x(), rasterizer.max_x() );
            while ( rasterizer.sweep_scanline( scanLine ) )
                this.RenderSolidSingleScanLine( destImage, scanLine, color );
        }

        public void RenderSolid( IImageFloat destImage, IRasterizer rasterizer, IScanlineCache scanLine, RGBA_Floats color )
        {
            if ( !rasterizer.rewind_scanlines() )
                return;
            scanLine.reset( rasterizer.min_x(), rasterizer.max_x() );
            while ( rasterizer.sweep_scanline( scanLine ) )
                this.RenderSolidSingleScanLine( destImage, scanLine, color );
        }

        protected virtual void RenderSolidSingleScanLine( IImageByte destImage, IScanlineCache scanLine, RGBA_Bytes color )
        {
            int y = scanLine.y();
            int num = scanLine.num_spans();
            ScanlineSpan scanlineSpan = scanLine.begin();
            byte[] covers = scanLine.GetCovers();
            while ( true )
            {
                int x = scanlineSpan.x;
                if ( scanlineSpan.len > 0 )
                {
                    destImage.blend_solid_hspan( x, y, scanlineSpan.len, color, covers, scanlineSpan.cover_index );
                }
                else
                {
                    int x2 = x - scanlineSpan.len - 1;
                    destImage.blend_hline( x, y, x2, color, covers[ scanlineSpan.cover_index ] );
                }
                if ( --num != 0 )
                    scanlineSpan = scanLine.GetNextScanlineSpan();
                else
                    break;
            }
        }

        protected virtual void RenderSolidSingleScanLine( IImageByte destImage, IScanlineCache scanLine, Func<int, int, RGBA_Bytes> getColorCb )
        {
            int y = scanLine.y();
            int num = scanLine.num_spans();
            ScanlineSpan scanlineSpan = scanLine.begin();
            byte[] covers = scanLine.GetCovers();
            while ( true )
            {
                int x = scanlineSpan.x;
                if ( scanlineSpan.len > 0 )
                {
                    destImage.blend_solid_hspan( x, y, scanlineSpan.len, getColorCb, covers, scanlineSpan.cover_index );
                }
                else
                {
                    int x2 = x - scanlineSpan.len - 1;
                    destImage.blend_hline( x, y, x2, getColorCb, covers[ scanlineSpan.cover_index ] );
                }
                if ( --num != 0 )
                    scanlineSpan = scanLine.GetNextScanlineSpan();
                else
                    break;
            }
        }

        private void RenderSolidSingleScanLine( IImageFloat destImage, IScanlineCache scanLine, RGBA_Floats color )
        {
            int y = scanLine.y();
            int num = scanLine.num_spans();
            ScanlineSpan scanlineSpan = scanLine.begin();
            byte[] covers = scanLine.GetCovers();
            while ( true )
            {
                int x = scanlineSpan.x;
                if ( scanlineSpan.len > 0 )
                {
                    destImage.blend_solid_hspan( x, y, scanlineSpan.len, color, covers, scanlineSpan.cover_index );
                }
                else
                {
                    int x2 = x - scanlineSpan.len - 1;
                    destImage.blend_hline( x, y, x2, color, covers[ scanlineSpan.cover_index ] );
                }
                if ( --num != 0 )
                    scanlineSpan = scanLine.GetNextScanlineSpan();
                else
                    break;
            }
        }

        public void RenderSolidAllPaths( IImageByte destImage, IRasterizer ras, IScanlineCache sl, IVertexSource vs, RGBA_Bytes[ ] color_storage, int[ ] path_id, int num_paths )
        {
            for ( int index = 0 ; index < num_paths ; ++index )
            {
                ras.reset();
                ras.add_path( vs, path_id[ index ] );
                this.render_scanlines_aa_solid( destImage, ras, sl, color_storage[ index ] );
            }
        }

        private void GenerateAndRenderSingleScanline( IScanlineCache scanLineCache, IImageByte destImage, span_allocator alloc, ISpanGenerator span_gen )
        {
            int y = scanLineCache.y();
            int num1 = scanLineCache.num_spans();
            ScanlineSpan scanlineSpan = scanLineCache.begin();
            byte[] covers = scanLineCache.GetCovers();
            while ( true )
            {
                int x = scanlineSpan.x;
                int num2 = scanlineSpan.len;
                if ( num2 < 0 )
                    num2 = -num2;
                if ( this.tempSpanColors.Capacity() < num2 )
                    this.tempSpanColors.Capacity( num2 );
                span_gen.generate( this.tempSpanColors.Array, 0, x, y, num2 );
                bool firstCoverForAll = scanlineSpan.len < 0;
                destImage.blend_color_hspan( x, y, num2, this.tempSpanColors.Array, 0, covers, scanlineSpan.cover_index, firstCoverForAll );
                if ( --num1 != 0 )
                    scanlineSpan = scanLineCache.GetNextScanlineSpan();
                else
                    break;
            }
        }

        private void GenerateAndRenderSingleScanline( IScanlineCache scanLineCache, IImageFloat destImageFloat, span_allocator alloc, ISpanGeneratorFloat span_gen )
        {
            int y = scanLineCache.y();
            int num1 = scanLineCache.num_spans();
            ScanlineSpan scanlineSpan = scanLineCache.begin();
            byte[] covers = scanLineCache.GetCovers();
            while ( true )
            {
                int x = scanlineSpan.x;
                int num2 = scanlineSpan.len;
                if ( num2 < 0 )
                    num2 = -num2;
                if ( this.tempSpanColorsFloats.Capacity() < num2 )
                    this.tempSpanColorsFloats.Capacity( num2 );
                span_gen.generate( this.tempSpanColorsFloats.Array, 0, x, y, num2 );
                bool firstCoverForAll = scanlineSpan.len < 0;
                destImageFloat.blend_color_hspan( x, y, num2, this.tempSpanColorsFloats.Array, 0, covers, scanlineSpan.cover_index, firstCoverForAll );
                if ( --num1 != 0 )
                    scanlineSpan = scanLineCache.GetNextScanlineSpan();
                else
                    break;
            }
        }

        public void GenerateAndRender( IRasterizer rasterizer, IScanlineCache scanlineCache, IImageByte destImage, span_allocator spanAllocator, ISpanGenerator spanGenerator )
        {
            if ( !rasterizer.rewind_scanlines() )
                return;
            scanlineCache.reset( rasterizer.min_x(), rasterizer.max_x() );
            spanGenerator.prepare();
            while ( rasterizer.sweep_scanline( scanlineCache ) )
                this.GenerateAndRenderSingleScanline( scanlineCache, destImage, spanAllocator, spanGenerator );
        }

        public void GenerateAndRender( IRasterizer rasterizer, IScanlineCache scanlineCache, IImageFloat destImage, span_allocator spanAllocator, ISpanGeneratorFloat spanGenerator )
        {
            if ( !rasterizer.rewind_scanlines() )
                return;
            scanlineCache.reset( rasterizer.min_x(), rasterizer.max_x() );
            spanGenerator.prepare();
            while ( rasterizer.sweep_scanline( scanlineCache ) )
                this.GenerateAndRenderSingleScanline( scanlineCache, destImage, spanAllocator, spanGenerator );
        }

        public void RenderCompound( rasterizer_compound_aa ras, IScanlineCache sl_aa, IScanlineCache sl_bin, IImageByte imageFormat, span_allocator alloc, IStyleHandler sh )
        {
        }
    }
}
