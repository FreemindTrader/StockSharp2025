// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Editors.StorageFormatEdit
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Editors;
using System.Windows;

namespace StockSharp.Studio.Controls.Editors
{
  public class StorageFormatEdit : ComboBoxEdit
  {
    public static readonly DependencyProperty IsDefaultEditorProperty = DependencyProperty.Register(nameof (IsDefaultEditor), typeof (bool), typeof (StorageFormatEdit));

    public bool IsDefaultEditor
    {
      get
      {
        return (bool) this.GetValue(StorageFormatEdit.IsDefaultEditorProperty);
      }
      set
      {
        this.SetValue(StorageFormatEdit.IsDefaultEditorProperty, (object) value);
      }
    }

    static StorageFormatEdit()
    {
      StorageFormatEditSettings.RegisterCustomEdit();
    }
  }
}
