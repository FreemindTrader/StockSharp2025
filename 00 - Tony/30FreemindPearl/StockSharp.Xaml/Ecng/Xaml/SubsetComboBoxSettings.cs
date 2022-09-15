// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.SubsetComboBoxSettings
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using System;
using System.Windows;

namespace Ecng.Xaml
{
  /// <summary>
  /// Edit settings for <see cref="T:Ecng.Xaml.SubsetComboBox" />.
  /// </summary>
  public class SubsetComboBoxSettings : ComboBoxEditExSettings
  {
    /// <summary>Display selected items count.</summary>
    public static readonly DependencyProperty DisplaySelectedItemsCountProperty = DependencyProperty.Register(nameof(2127279335), typeof (bool), typeof (SubsetComboBoxSettings), (PropertyMetadata) new FrameworkPropertyMetadata((object) false));

    static SubsetComboBoxSettings()
    {
      SubsetComboBoxSettings.RegisterDefaultUserEditor();
    }

    /// <inheritdoc />
    public SubsetComboBoxSettings()
    {
      ((FrameworkContentElement) this).Style = (Style) Application.Current.FindResource((object) SubsetComboBoxSettings.SubsetComboBoxSettingsStyleKey);
    }

    /// <summary>
    /// </summary>
    public static ComponentResourceKey SubsetComboBoxSettingsStyleKey
    {
      get
      {
        return new ComponentResourceKey(typeof (ComboBoxEditEx), (object) nameof(2127279265));
      }
    }

    /// <summary>Display selected items count.</summary>
    public bool DisplaySelectedItemsCount
    {
      get
      {
        return (bool) ((DependencyObject) this).GetValue(SubsetComboBoxSettings.DisplaySelectedItemsCountProperty);
      }
      set
      {
        ((DependencyObject) this).SetValue(SubsetComboBoxSettings.DisplaySelectedItemsCountProperty, (object) value);
      }
    }

    internal new static void RegisterDefaultUserEditor()
    {
      // ISSUE: method pointer
      // ISSUE: method pointer
      EditorSettingsProvider.get_Default().RegisterUserEditor(typeof (SubsetComboBox), typeof (SubsetComboBoxSettings), SubsetComboBoxSettings.SomeShit.\u0023\u003DzJns6h7GulSXVeO_8MQ\u003D\u003D ?? (SubsetComboBoxSettings.SomeShit.\u0023\u003DzJns6h7GulSXVeO_8MQ\u003D\u003D = new CreateEditorMethod((object) SubsetComboBoxSettings.SomeShit.ShitMethod02, __methodptr(\u0023\u003DzOVSBByNFxQH5Y4\u00242UikDJd0\u003D))), SubsetComboBoxSettings.SomeShit.\u0023\u003DzCsqohosSZowNnxRG1w\u003D\u003D ?? (SubsetComboBoxSettings.SomeShit.\u0023\u003DzCsqohosSZowNnxRG1w\u003D\u003D = new CreateEditorSettingsMethod((object) SubsetComboBoxSettings.SomeShit.ShitMethod02, __methodptr(\u0023\u003DzaSBr7g\u0024XSuKyyl5W7\u0024gMzJI\u003D))));
    }

    /// <inheritdoc />
    protected override void AssignToEditCore(IBaseEdit e)
    {
      SubsetComboBoxSettings.\u0023\u003DzxS320GJPAMEmRMm8Pw\u003D\u003D s320GjpamEmRmm8Pw = new SubsetComboBoxSettings.\u0023\u003DzxS320GJPAMEmRMm8Pw\u003D\u003D();
      s320GjpamEmRmm8Pw._delayActionHelper = this;
      s320GjpamEmRmm8Pw._comboBoxEditEx = e as SubsetComboBox;
      if (s320GjpamEmRmm8Pw._comboBoxEditEx != null)
        ((BaseEditSettings) this).SetValueFromSettings(SubsetComboBoxSettings.DisplaySelectedItemsCountProperty, new Action(s320GjpamEmRmm8Pw.LambdaClass023Method01));
      base.AssignToEditCore(e);
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly SubsetComboBoxSettings.SomeShit ShitMethod02 = new SubsetComboBoxSettings.SomeShit();
      public static CreateEditorMethod \u0023\u003DzJns6h7GulSXVeO_8MQ\u003D\u003D;
      public static CreateEditorSettingsMethod \u0023\u003DzCsqohosSZowNnxRG1w\u003D\u003D;

      internal IBaseEdit \u0023\u003DzOVSBByNFxQH5Y4\u00242UikDJd0\u003D()
      {
        return (IBaseEdit) new SubsetComboBox();
      }

      internal BaseEditSettings \u0023\u003DzaSBr7g\u0024XSuKyyl5W7\u0024gMzJI\u003D()
      {
        return (BaseEditSettings) new SubsetComboBoxSettings();
      }
    }

    private sealed class \u0023\u003DzxS320GJPAMEmRMm8Pw\u003D\u003D
    {
      public SubsetComboBox _comboBoxEditEx;
      public SubsetComboBoxSettings _delayActionHelper;

      internal void LambdaClass023Method01()
      {
        this._comboBoxEditEx.DisplaySelectedItemsCount = this._delayActionHelper.DisplaySelectedItemsCount;
      }
    }
  }
}
