// Decompiled with JetBrains decompiler
// Type: #=zSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2$onauDvAOLeS
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;

#nullable disable
internal sealed class \u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS : 
  ChartBaseViewModel
{
  
  private \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH \u0023\u003DzRrbXXZfT16PhrWmykQ\u003D\u003D;
  
  private bool \u0023\u003DzcMpkjcKq6\u0024YY = true;
  
  private IEnumerable<ParentVM> _childElements;
  
  private readonly ScichartSurfaceMVVM _scichartSurfaceVM;;
  
  private readonly ICommand \u0023\u003DzEOuQIBJYN0WfTnKFnA\u003D\u003D;
  
  private Action<IChartElement> \u0023\u003DzeBeQVx4\u003D;

  public \u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS(
    ScichartSurfaceMVVM _param1)
  {
    this._scichartSurfaceVM; = _param1 ?? throw new ArgumentNullException("pane");
    this.Elements = (IEnumerable<ParentVM>) _param1.LegendElements;
    this.\u0023\u003DzEOuQIBJYN0WfTnKFnA\u003D\u003D = (ICommand) new DelegateCommand<ParentVM>(new Action<ParentVM>(this.\u0023\u003DzB6IuSPwhBuqMEjjiXTaDcdI\u003D), \u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS.SomeClass34343383.\u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D ?? (\u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS.SomeClass34343383.\u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D = new Func<ParentVM, bool>(\u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS.SomeClass34343383.SomeMethond0343.\u0023\u003DzarIPT2hM\u0024qUQo3Kg7yhEyJY\u003D)));
  }

  public ScichartSurfaceMVVM Pane
  {
    get => this._scichartSurfaceVM;;
  }

  public ICommand RemoveElementCommand => this.\u0023\u003DzEOuQIBJYN0WfTnKFnA\u003D\u003D;

  public IEnumerable<ParentVM> Elements
  {
    get => this._childElements;
    set
    {
      this.SetField<IEnumerable<ParentVM>>(ref this._childElements, value, nameof (Elements));
    }
  }

  public \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH LegendModifier
  {
    get => this.\u0023\u003DzRrbXXZfT16PhrWmykQ\u003D\u003D;
    set
    {
      this.SetField<\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH>(ref this.\u0023\u003DzRrbXXZfT16PhrWmykQ\u003D\u003D, value, nameof (LegendModifier));
    }
  }

  public bool AllowToHide
  {
    get => this.\u0023\u003DzcMpkjcKq6\u0024YY;
    set
    {
      this.SetField<bool>(ref this.\u0023\u003DzcMpkjcKq6\u0024YY, value, nameof (AllowToHide));
    }
  }

  public void RemoveElementEvent(Action<IChartElement> _param1)
  {
    Action<IChartElement> action = this.\u0023\u003DzeBeQVx4\u003D;
    Action<IChartElement> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<IChartElement>>(ref this.\u0023\u003DzeBeQVx4\u003D, comparand + _param1, comparand);
    }
    while (action != comparand);
  }

  public void RemoveElementEvent(Action<IChartElement> _param1)
  {
    Action<IChartElement> action = this.\u0023\u003DzeBeQVx4\u003D;
    Action<IChartElement> comparand;
    do
    {
      comparand = action;
      action = Interlocked.CompareExchange<Action<IChartElement>>(ref this.\u0023\u003DzeBeQVx4\u003D, comparand - _param1, comparand);
    }
    while (action != comparand);
  }

  private void \u0023\u003DzB6IuSPwhBuqMEjjiXTaDcdI\u003D(
    ParentVM _param1)
  {
    Action<IChartElement> zeBeQvx4 = this.\u0023\u003DzeBeQVx4\u003D;
    if (zeBeQvx4 == null)
      return;
    zeBeQvx4((IChartElement) _param1.ChartElement);
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS.SomeClass34343383();
    public static Func<ParentVM, bool> \u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D;

    internal bool \u0023\u003DzarIPT2hM\u0024qUQo3Kg7yhEyJY\u003D(
      ParentVM _param1)
    {
      return _param1 != null && _param1.AllowToRemove;
    }
  }
}
