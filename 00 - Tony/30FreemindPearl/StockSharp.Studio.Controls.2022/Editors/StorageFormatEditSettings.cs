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
                return ( bool )GetValue( IsDefaultEditorProperty );
            }
            set
            {
                SetValue( IsDefaultEditorProperty, value );
            }
        }

        static StorageFormatEditSettings()
        {
            RegisterCustomEdit();
        }

        public static void RegisterCustomEdit()
        {
            EditorSettingsProvider.Default.RegisterUserEditor( typeof( StorageFormatEdit ), typeof( StorageFormatEditSettings ), () => new StorageFormatEdit(), () => new StorageFormatEditSettings() );
        }

        public StorageFormatEditSettings()
        {
            ItemsSource = Ecng.Common.Enumerator.GetValues<StorageFormats>().Select( v => new Tuple<string, object>( v.GetDisplayName(), v ) ).ToArray();
            DisplayMember = "Item1";
            ValueMember = "Item2";
        }
    }
}
