using MatterHackers.Agg.Transform;

namespace MatterHackers.Agg
{
    internal interface ISpanInterpolator
    {
        void begin( double x, double y, int len );

        void coordinates( out int x, out int y );

        void Next();

        ITransform transformer();

        void transformer( ITransform trans );

        void resynchronize( double xe, double ye, int len );

        void local_scale( out int x, out int y );
    }
}
