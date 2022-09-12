using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Ecng.Security
{
    public static class PermissionBarrier
    {
        public static void Check( string userName )
        {
            if ( userName.IsEmpty() )
                throw new ArgumentNullException( nameof( userName ) );
            if ( string.Compare( Thread.CurrentPrincipal.Identity.Name, userName, StringComparison.OrdinalIgnoreCase ) == 0 )
                return;
            PermissionBarrier.ThrowAccessException();
        }

        public static void Check( IEnumerable<string> roles )
        {
            if ( roles == null )
                throw new ArgumentNullException( nameof( roles ) );
            using ( IEnumerator<string> enumerator = roles.GetEnumerator() )
            {
                while ( enumerator.MoveNext() && !Thread.CurrentPrincipal.IsInRole( enumerator.Current ) )
                    PermissionBarrier.ThrowAccessException();
            }
        }

        public static void Check()
        {
            if ( Thread.CurrentPrincipal.Identity.IsAuthenticated )
                return;
            PermissionBarrier.ThrowAccessException();
        }

        private static void ThrowAccessException()
        {
            throw new UnauthorizedAccessException( "Current principal '" + Thread.CurrentPrincipal?.Identity?.Name + "' hasn't required permissions." );
        }
    }
}
