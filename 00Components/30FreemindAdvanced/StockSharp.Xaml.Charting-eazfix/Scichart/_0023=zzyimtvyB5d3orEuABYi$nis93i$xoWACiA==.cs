// Decompiled with JetBrains decompiler
// Type: #=zzyimtvyB5d3orEuABYi$nis93i$xoWACiA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

#nullable disable
internal sealed class \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nis93i\u0024xoWACiA\u003D\u003D : 
  INotifyPropertyChanged
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzObK1Pto6RdIX;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private FrameworkElement \u0023\u003Dz_dXR314ooINCiWuf9w\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IScichartSurfaceVM \u0023\u003DzdVKUBCrO0g2K1\u0024i0ZAryqfw\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzHUAO9zJM6Q65Zm\u00244E38Q24E\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ICommand \u0023\u003Dz4NBeGvQbp33yF1kDgvO7vGE\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ICommand _closePaneCommand;

  public event PropertyChangedEventHandler PropertyChanged;

  public FrameworkElement PaneElement
  {
    get => this.\u0023\u003Dz_dXR314ooINCiWuf9w\u003D\u003D;
    set => this.\u0023\u003Dz_dXR314ooINCiWuf9w\u003D\u003D = value;
  }

  public IScichartSurfaceVM PaneViewModel
  {
    get => this.\u0023\u003DzdVKUBCrO0g2K1\u0024i0ZAryqfw\u003D;
    set => this.\u0023\u003DzdVKUBCrO0g2K1\u0024i0ZAryqfw\u003D = value;
  }

  internal bool \u0023\u003DzR8SPjvFW2FAx() => this.\u0023\u003DzHUAO9zJM6Q65Zm\u00244E38Q24E\u003D;

  internal void \u0023\u003DzcXGZaBR0mVD3(bool _param1)
  {
    this.\u0023\u003DzHUAO9zJM6Q65Zm\u00244E38Q24E\u003D = _param1;
  }

  public bool IsTabbed
  {
    get => this.\u0023\u003DzObK1Pto6RdIX;
    internal set
    {
      this.\u0023\u003DzObK1Pto6RdIX = value;
      this.\u0023\u003Dz15moWio\u003D(nameof (IsTabbed));
    }
  }

  public ICommand ChangeOrientationCommand
  {
    get => this.\u0023\u003Dz4NBeGvQbp33yF1kDgvO7vGE\u003D;
    set => this.\u0023\u003Dz4NBeGvQbp33yF1kDgvO7vGE\u003D = value;
  }

  public ICommand ClosePaneCommand
  {
    get => this._closePaneCommand;
    set => this._closePaneCommand = value;
  }

  private void \u0023\u003Dz15moWio\u003D(string _param1)
  {
    PropertyChangedEventHandler zUapFgog = this.\u0023\u003DzUApFgog\u003D;
    if (zUapFgog == null)
      return;
    zUapFgog((object) this, new PropertyChangedEventArgs(_param1));
  }
}
