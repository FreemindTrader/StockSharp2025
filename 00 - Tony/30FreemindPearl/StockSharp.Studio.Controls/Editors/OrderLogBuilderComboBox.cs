// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Editors.OrderLogBuilderComboBox
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using System;

namespace StockSharp.Studio.Controls.Editors
{
  public class OrderLogBuilderComboBox : ComboBoxEdit
  {
    public OrderLogBuilderComboBox()
    {
      this.DisplayMember = "Name";
      this.ValueMember = "Type";
      this.IsTextEditable = new bool?(false);
      this.AutoComplete = true;
      Type defaultBuilder;
      this.ItemsSource = OrderLogBuilderItem.CreateSource(out defaultBuilder);
      this.SelectedBuilder = defaultBuilder;
    }

    protected override BaseEditSettings CreateEditorSettings()
    {
      return (BaseEditSettings) new OrderLogBuilderComboEditor();
    }

    protected override void OnLoadedInternal()
    {
      base.OnLoadedInternal();
      if (this.EditMode != EditMode.Standalone)
        return;
      this.Settings.ApplyToEdit((IBaseEdit) this, true, (IDefaultEditorViewInfo) EmptyDefaultEditorViewInfo.Instance);
    }

    public Type SelectedBuilder
    {
      get
      {
        return (Type) this.EditValue;
      }
      set
      {
        this.EditValue = (object) value;
      }
    }
  }
}
