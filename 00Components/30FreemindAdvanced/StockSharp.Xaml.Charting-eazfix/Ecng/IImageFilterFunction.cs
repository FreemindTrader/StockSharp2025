namespace MatterHackers.Agg
{
    internal interface IImageFilterFunction
    {
        double radius();

        double calc_weight( double x );
    }
}
