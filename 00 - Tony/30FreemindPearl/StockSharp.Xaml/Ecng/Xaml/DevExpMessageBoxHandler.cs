using DevExpress.Xpf.Core;
using System.Windows;

namespace Ecng.Xaml
{
    /// <summary>Message box handler implementation for devexp.</summary>
    public class DevExpMessageBoxHandler : IMessageBoxHandler
    {
        MessageBoxResult IMessageBoxHandler.Show(
          string _param1,
          string _param2,
          MessageBoxButton _param3,
          MessageBoxImage _param4,
          MessageBoxResult _param5,
          MessageBoxOptions _param6)
        {
            return ThemedMessageBox.Show( ( Window )null, _param2, _param1, _param3, new MessageBoxResult?( _param5 ), _param4, false, _param6, WindowStartupLocation.CenterOwner, ( WindowTitleAlignment )0, new bool?() );
        }

        MessageBoxResult IMessageBoxHandler.Show(
          Window _param1,
          string _param2,
          string _param3,
          MessageBoxButton _param4,
          MessageBoxImage _param5,
          MessageBoxResult _param6,
          MessageBoxOptions _param7)
        {
            return ThemedMessageBox.Show( _param1, _param3, _param2, _param4, new MessageBoxResult?( _param6 ), _param5, false, _param7, WindowStartupLocation.CenterOwner, ( WindowTitleAlignment )0, new bool?() );
        }
    }
}
