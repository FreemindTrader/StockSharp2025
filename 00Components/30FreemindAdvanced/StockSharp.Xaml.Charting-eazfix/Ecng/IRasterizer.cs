using MatterHackers.Agg.VertexSource;

namespace MatterHackers.Agg
{
    internal interface IRasterizer
    {
        int min_x();

        int min_y();

        int max_x();

        int max_y();

        void gamma( IGammaFunction gamma_function );

        bool sweep_scanline( IScanlineCache sl );

        void reset();

        void add_path( IVertexSource vs );

        void add_path( IVertexSource vs, int path_id );

        bool rewind_scanlines();
    }
}
