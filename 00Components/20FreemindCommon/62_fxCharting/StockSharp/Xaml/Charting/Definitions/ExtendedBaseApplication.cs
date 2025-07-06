using StockSharp.Xaml;

namespace StockSharp.Xaml.Charting
{
    public abstract class ExtendedBaseApplication : BaseApplication
    {
        protected ExtendedBaseApplication( )
        {
            LicenseManager.CreateInstance( );
        }
    }
}
