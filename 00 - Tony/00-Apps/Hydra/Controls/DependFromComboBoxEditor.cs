// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Controls.DependFromComboBoxEditor
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using Ecng.Xaml;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using System;
using System.Windows.Input;

namespace StockSharp.Hydra.Controls
{
  public class DependFromComboBoxEditor : ComboBoxEditSettings
  {
    private ComboBoxEdit _edit;

    public DependFromComboBoxEditor()
    {
      DisplayMember = "Title";
      ItemsSource = HydraTaskManager.Instance.Tasks;
      AllowNullInput = true;
      ButtonInfoCollection buttons = Buttons;
      ButtonInfo buttonInfo = new ButtonInfo();
      buttonInfo.GlyphKind = GlyphKind.Cancel;
      buttonInfo.Command = new DelegateCommand( a => _edit.EditValue = null, a => _edit?.EditValue is IHydraTask );
      buttonInfo.ToolTip = LocalizedStrings.Str2060;
      buttons.Add(buttonInfo);
    }

    protected override void AssignToEditCore(IBaseEdit edit)
    {
      _edit = edit as ComboBoxEdit;
      base.AssignToEditCore(edit);
    }
  }
}
