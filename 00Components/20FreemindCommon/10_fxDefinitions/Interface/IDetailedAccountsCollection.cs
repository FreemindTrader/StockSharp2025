using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IDetailedAccountsCollection : IEnumerable< IDetailedAccount >
    {        
        int      Count              { get; }        // Get number of the offers in the collection        
        IDetailedAccount this[ int index ]  { get; }        // Gets the offer by its index
        IDetailedAccount FindAccountById( string accountId );
    }
}
