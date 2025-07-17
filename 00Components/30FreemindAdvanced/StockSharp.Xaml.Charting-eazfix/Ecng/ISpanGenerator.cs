namespace MatterHackers.Agg
{
    internal interface ISpanGenerator
    {
        void prepare();

        void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len );
    }
}
