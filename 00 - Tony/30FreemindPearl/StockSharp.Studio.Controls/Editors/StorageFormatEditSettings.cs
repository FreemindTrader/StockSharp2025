// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Editors.StorageFormatEditSettings
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using Ecng.ComponentModel;
using StockSharp.Algo.Storages;
using System;
using System.Linq;
using System.Windows;

namespace StockSharp.Studio.Controls.Editors
{
    public class StorageFormatEditSettings : ComboBoxEditSettings
    {
        public static readonly DependencyProperty IsDefaultEditorProperty = DependencyProperty.Register( nameof( IsDefaultEditor ), typeof( bool ), typeof( StorageFormatEditSettings ) );

        public bool IsDefaultEditor
        {
            get
            {
                return ( bool )this.GetValue( StorageFormatEditSettings.IsDefaultEditorProperty );
            }
            set
            {
                this.SetValue( StorageFormatEditSettings.IsDefaultEditorProperty, ( object )value );
            }
        }

        static StorageFormatEditSettings()
        {
            StorageFormatEditSettings.RegisterCustomEdit();
        }

        public static void RegisterCustomEdit()
        {
            EditorSettingsProvider.Default.RegisterUserEditor( typeof( StorageFormatEdit ), typeof( StorageFormatEditSettings ), ( CreateEditorMethod )( () => ( IBaseEdit )new StorageFormatEdit() ), ( CreateEditorSettingsMethod )( () => ( BaseEditSettings )new StorageFormatEditSettings() ) );
        }

        public StorageFormatEditSettings()
        {
            this.ItemsSource = ( object )Ecng.Common.Enumerator.GetValues<StorageFormats>().Select<StorageFormats, Tuple<string, object>>( ( Func<StorageFormats, Tuple<string, object>> )( v => new Tuple<string, object>( v.GetDisplayName(), ( object )v ) ) ).ToArray<Tuple<string, object>>();
            this.DisplayMember = "Item1";
            this.ValueMember = "Item2";
        }
    }
}
