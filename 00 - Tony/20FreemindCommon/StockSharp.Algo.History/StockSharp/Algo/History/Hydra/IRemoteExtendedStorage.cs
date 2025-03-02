using StockSharp.Messages;
using System;
using System.Collections.Generic;

namespace StockSharp.Algo.History.Hydra
{
    public interface IRemoteExtendedStorage
    {
        string StorageName
        {
            get;
        }

        IEnumerable< SecurityId > Securities
        {
            get;
        }

        Tuple< string, Type >[] Fields
        {
            get;
        }

        ISecurityRemoteExtendedStorage GetSecurityStorage( SecurityId securityId );

        Tuple< SecurityId, object[] >[] GetAllExtendedInfo( );

        void CreateSecurityExtendedFields( Tuple< string, Type >[] fields );
    }
}
