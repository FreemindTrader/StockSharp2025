using StockSharp.Messages;

namespace StockSharp.Algo.History.Hydra
{
    public interface ISecurityRemoteExtendedStorage
    {
        SecurityId SecurityId
        {
            get;
        }

        void AddSecurityExtendedInfo( object[] fieldValues );

        void DeleteSecurityExtendedInfo( );
    }
}
