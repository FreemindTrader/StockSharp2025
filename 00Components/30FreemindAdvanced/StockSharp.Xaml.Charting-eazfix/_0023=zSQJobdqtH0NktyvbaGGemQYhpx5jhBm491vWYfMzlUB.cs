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
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH \u0023\u003DzRrbXXZfT16PhrWmykQ\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private bool \u0023\u003DzcMpkjcKq6\u0024YY = true;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private IEnumerable<\u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB> \u0023\u003DzH31vDNM\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj \u0023\u003DznCb1THp8SgddfKhj6w\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly ICommand \u0023\u003DzEOuQIBJYN0WfTnKFnA\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Action<IChartElement> \u0023\u003DzeBeQVx4\u003D;

  public \u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS(
    \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj _param1)
  {
    this.\u0023\u003DznCb1THp8SgddfKhj6w\u003D\u003D = _param1 ?? throw new ArgumentNullException("pane");
    this.Elements = (IEnumerable<\u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB>) _param1.LegendElements;
    this.\u0023\u003DzEOuQIBJYN0WfTnKFnA\u003D\u003D = (ICommand) new DelegateCommand<\u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB>(new Action<\u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB>(this.\u0023\u003DzB6IuSPwhBuqMEjjiXTaDcdI\u003D), \u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D ?? (\u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D = new Func<\u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB, bool>(\u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzarIPT2hM\u0024qUQo3Kg7yhEyJY\u003D)));
  }

  public \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj Pane
  {
    get => this.\u0023\u003DznCb1THp8SgddfKhj6w\u003D\u003D;
  }

  public ICommand RemoveElementCommand => this.\u0023\u003DzEOuQIBJYN0WfTnKFnA\u003D\u003D;

  public IEnumerable<\u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB> Elements
  {
    get => this.\u0023\u003DzH31vDNM\u003D;
    set
    {
      this.SetField<IEnumerable<\u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB>>(ref this.\u0023\u003DzH31vDNM\u003D, value, nameof (Elements));
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

  public void \u0023\u003DzPvEital2M7gh(Action<IChartElement> _param1)
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

  public void \u0023\u003Dzfj2KEivrD_Sr(Action<IChartElement> _param1)
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
    \u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB _param1)
  {
    Action<IChartElement> zeBeQvx4 = this.\u0023\u003DzeBeQVx4\u003D;
    if (zeBeQvx4 == null)
      return;
    zeBeQvx4((IChartElement) _param1.ChartElement);
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003DzSQJobdqtH0NktyvbaGGemQYhpx5jhBm491vWYfMzlUBNK7y2\u0024onauDvAOLeS.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<\u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB, bool> \u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D;

    internal bool \u0023\u003DzarIPT2hM\u0024qUQo3Kg7yhEyJY\u003D(
      \u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB _param1)
    {
      return _param1 != null && _param1.AllowToRemove;
    }
  }
}
