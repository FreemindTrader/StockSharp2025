using StockSharp.Xaml;

namespace fx.Charting
{
    public abstract class ExtendedBaseApplication : BaseApplication
    {
        protected ExtendedBaseApplication( )
        {
            LicenseManager.CreateInstance( );
        }
    }
}
