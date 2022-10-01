// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Database.DatabaseHelper
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

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
                return ( DatabaseConnectionCache )ConfigManager.TryGetService<DatabaseConnectionCache>();
            }
        }

        /// <summary>Verify the connection is ok.</summary>
        /// <param name="pair">Connection.</param>
        /// <param name="owner">UI owner (in case of <paramref name="showMessageBox" /> is <see langword="true" />).</param>
        /// <param name="showMessageBox">Show UI message.</param>
        /// <returns>Check result.</returns>
        public static bool Verify(
          this DatabaseConnectionPair pair,
          DependencyObject owner,
          bool showMessageBox = true )
        {
            if ( pair == null )
                throw new ArgumentNullException( nameof( 2127280787 ) );
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
