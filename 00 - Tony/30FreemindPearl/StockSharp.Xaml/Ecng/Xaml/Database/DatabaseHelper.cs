using Ecng.Configuration;
using Ecng.Data;
using StockSharp.Localization;
using System;
using System.Windows;

namespace Ecng.Xaml.Database
{
    /// <summary>
    /// Extensions for <see cref="N:Ecng.Xaml.Database" />.
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>Cache.</summary>
        public static DatabaseConnectionCache Cache
        {
            get
            {
                return ConfigManager.TryGetService<DatabaseConnectionCache>();
            }
        }

        /// <summary>Verify the connection is ok.</summary>
        /// <param name="pair">Connection.</param>
        /// <param name="owner">UI owner (in case of <paramref name="showMessageBox" /> is <see langword="true" />).</param>
        /// <param name="showMessageBox">Show UI message.</param>
        /// <returns>Check result.</returns>
        public static bool Verify( this DatabaseConnectionPair pair, DependencyObject owner, bool showMessageBox = true )
        {
            if ( pair == null )
                throw new ArgumentNullException("pair == null" );
            try
            {
                DataHelper.Verify( pair );
                if ( showMessageBox )
                {
                    int num = ( int )new MessageBoxBuilder().Text( LocalizedStrings.Str1560 ).Owner( owner ).Show();
                }
                return true;
            }
            catch ( Exception ex )
            {
                if ( showMessageBox )
                {
                    int num = ( int )new MessageBoxBuilder().Error( ex ).Owner( owner ).Show();
                }
                return false;
            }
        }
    }
}
