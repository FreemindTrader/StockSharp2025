using StockSharp.Hydra.Core.Server;
using StockSharp.Studio.Core.Services;

namespace StockSharp.Hydra
{
    internal static class HydraPersistableHelper
    {
        private const string _serverSettingsKey = "ServerSettings";

        public static HydraCommonSettings GetSettings( this IPersistableService service )
        {
            return service.GetCommonSettings<HydraCommonSettings>();
        }

        public static void SetSettings( this IPersistableService service, HydraCommonSettings settings )
        {
            service.SetCommonSettings( settings );
        }

        public static HydraServerSettings GetServerSettings( this IPersistableService service )
        {
            return service.GetSettings<HydraServerSettings>( "ServerSettings" );
        }

        public static void SetServerSettings( this IPersistableService service, HydraServerSettings settings )
        {
            service.SetSettings( "ServerSettings", settings );
        }
    }
}
