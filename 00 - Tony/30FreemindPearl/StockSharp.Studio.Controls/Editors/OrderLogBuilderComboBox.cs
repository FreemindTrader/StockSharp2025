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
      DisplayMember = "Name";
      ValueMember = "Type";
      IsTextEditable = new bool?(false);
      AutoComplete = true;
      Type defaultBuilder;
      ItemsSource = OrderLogBuilderItem.CreateSource(out defaultBuilder);
      SelectedBuilder = defaultBuilder;
    }

    protected override BaseEditSettings CreateEditorSettings()
    {
      return new OrderLogBuilderComboEditor();
    }

    protected override void OnLoadedInternal()
    {
      base.OnLoadedInternal();
      if (EditMode != EditMode.Standalone)
        return;
      Settings.ApplyToEdit( this, true, EmptyDefaultEditorViewInfo.Instance );
    }

    public Type SelectedBuilder
    {
      get
      {
        return (Type) EditValue;
      }
      set
      {
        EditValue = value;
      }
    }
  }
}
