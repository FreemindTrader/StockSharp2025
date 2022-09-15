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
  public class DatabaseConnectionEditor : ComboBoxEditSettings, IComponentConnector
  {
    
    private readonly ObservableCollection<DatabaseConnectionPair> \u0023\u003DzcB0KWB8\u003D;
    
    private ComboBoxEdit \u0023\u003DzwylhZpc\u003D;
    
    internal ButtonInfo \u0023\u003DzKLtsLUvhiHaS;
    
    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

    static DatabaseConnectionEditor()
    {
      // ISSUE: method pointer
      // ISSUE: method pointer
      EditorSettingsProvider.get_Default().RegisterUserEditor2(typeof (DatabaseConnectionComboBox), typeof (DatabaseConnectionEditor), new CreateEditorMethod2((object) DatabaseConnectionEditor.SomeShit.ShitMethod02, __methodptr(OnItemsSourcePropertyChanged)), new CreateEditorSettingsMethod((object) DatabaseConnectionEditor.SomeShit.ShitMethod02, __methodptr(\u0023\u003DzDjjgEnV0af3pWy74XqVd5N0\u003D)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Xaml.Database.DatabaseConnectionEditor" />.
    /// </summary>
    public DatabaseConnectionEditor()
    {
      base.\u002Ector();
      this.InitializeComponent();
      DatabaseConnectionCache cache = DatabaseHelper.Cache;
      if (cache != null)
      {
        this.\u0023\u003DzcB0KWB8\u003D.AddRange<DatabaseConnectionPair>(cache.Connections);
        cache.ConnectionCreated += new Action<DatabaseConnectionPair>(this.\u0023\u003DzjCImydg0hrXd);
        cache.ConnectionDeleted += new Action<DatabaseConnectionPair>(this.\u0023\u003DzhWsqNO67psyH);
      }
      ((LookUpEditSettingsBase) this).set_ItemsSource((object) this.\u0023\u003DzcB0KWB8\u003D);
    }

    private void \u0023\u003DzjCImydg0hrXd(DatabaseConnectionPair _param1)
    {
      ((DispatcherObject) this).GuiAsync(new Action(new DatabaseConnectionEditor.\u0023\u003DzpX\u0024x09kXCZiIkFTrOA\u003D\u003D()
      {
        _delayActionHelper = this,
        \u0023\u003DzCuEJvAc\u003D = _param1
      }.\u0023\u003DznTeCHttgipqPI99rQoZKhzs\u003D));
    }

    private void \u0023\u003DzhWsqNO67psyH(DatabaseConnectionPair _param1)
    {
      ((DispatcherObject) this).GuiAsync(new Action(new DatabaseConnectionEditor.\u0023\u003DzRGh14kP8aNdRF\u0024ajsg\u003D\u003D()
      {
        _delayActionHelper = this,
        \u0023\u003DzCuEJvAc\u003D = _param1
      }.\u0023\u003DzNCyCpt_o90m8Kyi3qw\u003D\u003D));
    }

    /// <summary>
    /// </summary>
    /// <param name="edit">
    /// </param>
    protected virtual void AssignToEditCore(IBaseEdit edit)
    {
      this.\u0023\u003DzwylhZpc\u003D = edit as ComboBoxEdit;
      base.AssignToEditCore(edit);
    }

    private void \u0023\u003DzOJIjDSi7oYU1wDamQA\u003D\u003D(
      object _param1,
      RoutedEventArgs _param2)
    {
      DatabaseConnectionWindow connectionWindow = new DatabaseConnectionWindow();
      if (!((Window) connectionWindow).ShowModal((DependencyObject) _param1) || this.\u0023\u003DzwylhZpc\u003D == null)
        return;
      ((LookUpEditBase) this.\u0023\u003DzwylhZpc\u003D).set_SelectedItem((object) connectionWindow.Pair);
    }

    /// <summary>InitializeComponent</summary>
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    public void InitializeComponent()
    {
      if (this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        return;
      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      Application.LoadComponent((object) this, new Uri(nameof(2127281648), UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
      int _param1,
      object _param2)
    {
      if (_param1 == 1)
      {
        this.\u0023\u003DzKLtsLUvhiHaS = (ButtonInfo) _param2;
        ((CommandButtonInfo) this.\u0023\u003DzKLtsLUvhiHaS).add_Click(new RoutedEventHandler(this.\u0023\u003DzOJIjDSi7oYU1wDamQA\u003D\u003D));
      }
      else
        this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly DatabaseConnectionEditor.SomeShit ShitMethod02 = new DatabaseConnectionEditor.SomeShit();

      internal IBaseEdit OnItemsSourcePropertyChanged(bool _param1)
      {
        if (!_param1)
          return (IBaseEdit) new DatabaseConnectionComboBox();
        return (IBaseEdit) new InplaceBaseEdit();
      }

      internal BaseEditSettings \u0023\u003DzDjjgEnV0af3pWy74XqVd5N0\u003D()
      {
        return (BaseEditSettings) new DatabaseConnectionEditor();
      }
    }

    private sealed class \u0023\u003DzRGh14kP8aNdRF\u0024ajsg\u003D\u003D
    {
      public DatabaseConnectionEditor _delayActionHelper;
      public DatabaseConnectionPair \u0023\u003DzCuEJvAc\u003D;

      internal void \u0023\u003DzNCyCpt_o90m8Kyi3qw\u003D\u003D()
      {
        this._delayActionHelper.\u0023\u003DzcB0KWB8\u003D.Remove(this.\u0023\u003DzCuEJvAc\u003D);
      }
    }

    private sealed class \u0023\u003DzpX\u0024x09kXCZiIkFTrOA\u003D\u003D
    {
      public DatabaseConnectionEditor _delayActionHelper;
      public DatabaseConnectionPair \u0023\u003DzCuEJvAc\u003D;

      internal void \u0023\u003DznTeCHttgipqPI99rQoZKhzs\u003D()
      {
        this._delayActionHelper.\u0023\u003DzcB0KWB8\u003D.Add(this.\u0023\u003DzCuEJvAc\u003D);
      }
    }
  }
}
