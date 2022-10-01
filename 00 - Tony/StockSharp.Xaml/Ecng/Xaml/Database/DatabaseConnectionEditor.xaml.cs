// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Database.DatabaseConnectionEditor
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using Ecng.Collections;
using Ecng.Data;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Ecng.Xaml.Database
{
    /// <summary>
    /// Визуальный редактор для выбора строчки подключения к базе данных.
    /// </summary>
    /// <summary>DatabaseConnectionEditor</summary>
    public partial class DatabaseConnectionEditor : ComboBoxEditSettings, IComponentConnector
    {

        private readonly ObservableCollection<DatabaseConnectionPair> _dbPairs;

        private ComboBoxEdit _comboBoxEdit;

        static DatabaseConnectionEditor()
        {
            EditorSettingsProvider.Default.RegisterUserEditor2(
                                                                typeof( DatabaseConnectionComboBox ),
                                                                typeof( DatabaseConnectionEditor ),
                                                                p =>
                                                                {
                                                                    if ( !p )
                                                                        return ( IBaseEdit )new DatabaseConnectionComboBox();
                                                                    return ( IBaseEdit )new InplaceBaseEdit();
                                                                },

                                                                () => ( BaseEditSettings )new DatabaseConnectionEditor() );

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Ecng.Xaml.Database.DatabaseConnectionEditor" />.
        /// </summary>
        public DatabaseConnectionEditor()
        {
            InitializeComponent();
            DatabaseConnectionCache cache = DatabaseHelper.Cache;
            if ( cache != null )
            {
                CollectionHelper.AddRange<DatabaseConnectionPair>( this._dbPairs, cache.Connections );
                cache.ConnectionCreated += OnConnectionCreated;
                cache.ConnectionDeleted += OnConnectionDeleted;
            }

          ( ( LookUpEditSettingsBase )this ).ItemsSource = ( ( object )this._dbPairs );
        }

        private void OnConnectionDeleted( DatabaseConnectionPair pair )
        {
            this.GuiAsync( () => _dbPairs.Remove( pair ) );
        }

        private void OnConnectionCreated( DatabaseConnectionPair pair )
        {
            this.GuiAsync( () => _dbPairs.Add( pair ) );
        }

        /// <summary>
        /// </summary>
        /// <param name="edit">
        /// </param>
        protected virtual void AssignToEditCore( IBaseEdit edit )
        {
            this._comboBoxEdit = edit as ComboBoxEdit;
            base.AssignToEditCore( edit );
        }

        private void OnCommandInfoButtonClick( object o, RoutedEventArgs e )
        {
            DatabaseConnectionWindow connectionWindow = new DatabaseConnectionWindow();
            if ( !( ( Window )connectionWindow ).ShowModal( ( DependencyObject )o ) || this._comboBoxEdit == null )
                return;
            ( ( LookUpEditBase )this._comboBoxEdit ).SelectedItem = ( ( object )connectionWindow.Pair );
        }
    }
}


//        private void \u0023\u003DzhWsqNO67psyH( DatabaseConnectionPair _param1 )
//        {
//            ( ( DispatcherObject )this ).GuiAsync( new Action( new DatabaseConnectionEditor.\u0023\u003DzRGh14kP8aNdRF\u0024ajsg\u003D\u003D()
//            {
//        _ed01 = this,
//        _dbPair03 = _param1
//            }.\u0023\u003DzNCyCpt_o90m8Kyi3qw\u003D\u003D));
//        }





//    [Serializable]
//    private sealed class Lamdba0003
//    {
//        public static readonly DatabaseConnectionEditor.Lamdba0003 _this = new DatabaseConnectionEditor.Lamdba0003();

//        internal IBaseEdit \u0023\u003DzRgIpdY1PhZCzQ\u0024mYEyoJlSs\u003D(bool _param1)
//      {
//        if (!_param1)
//          return (IBaseEdit) new DatabaseConnectionComboBox();
//        return (IBaseEdit) new InplaceBaseEdit();
//    }

//    internal BaseEditSettings \u0023\u003DzDjjgEnV0af3pWy74XqVd5N0\u003D()
//      {
//        return (BaseEditSettings) new DatabaseConnectionEditor();
//}
//    }

//    private sealed class \u0023\u003DzRGh14kP8aNdRF\u0024ajsg\u003D\u003D
//    {
//      public DatabaseConnectionEditor _ed01;
//public DatabaseConnectionPair _dbPair03;

//internal void \u0023\u003DzNCyCpt_o90m8Kyi3qw\u003D\u003D()
//      {
//    this._ed01._dbPairs.Remove( this._dbPair03);
//}
//    }


//}
