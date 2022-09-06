using Ecng.Common;
using LinqToDB.Data;
using System;

namespace Ecng.Data
{
    public static class DataHelper
    {
        public static DataConnection CreateConnection( this DatabaseConnectionPair pair )
        {
            if ( pair == null )
                throw new ArgumentNullException( nameof( pair ) );
            Type provider = pair.Provider;
            if ( ( object )provider == null )
                throw new InvalidOperationException( "Provider is not set." );
            string connectionString = pair.ConnectionString;
            if ( connectionString.IsEmpty() )
                throw new InvalidOperationException( "Cannot create a connection, because some data was not entered." );
            return new DataConnection( DatabaseProviderRegistry.CreateProvider( provider ), connectionString );
        }

        public static void Verify( this DatabaseConnectionPair pair )
        {
            using ( DataConnection connection = pair.CreateConnection() )
                connection.Connection.Open();
        }
    }
}
