using Ecng.Xaml;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Common.Helpers;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;

namespace StockSharp.Xaml.Charting;


/// <summary>
/// Since LegendModifier is part of Scichart, I have developed my own version of LegendModifierEx.
/// 
/// I don't remember why I did that, will update the document later.
/// </summary>
public sealed class LegendModifierVM : ChartBaseViewModel
{
    private LegendModifierEx _legendModifier;

    private bool _allowToHide = true;

    private IEnumerable<ChartComponentViewModel> _componentsCache;

    private readonly DrawingSurfaceViewModel _scichartSurfaceVM;

    private readonly ICommand _removeElementCommand;

    public event Action<IChartElement> RemoveElementEvent;

    public LegendModifierVM( DrawingSurfaceViewModel vm )
    {
        this._scichartSurfaceVM = vm ?? throw new ArgumentNullException( "pane" );
        this.Elements = ( IEnumerable<ChartComponentViewModel> ) vm.LegendElements;

        // BUG: need to work on ChartComponentViewModel first
        //this._removeElementCommand = new ActionCommand<ChartComponentViewModel>(
        //    vm => RemoveElementEvent?.Invoke( vm.ChartComponent ),
        //    p => p.AllowToRemove );
    }

    public DrawingSurfaceViewModel Pane
    {
        get => this._scichartSurfaceVM;
    }

    public ICommand RemoveElementCommand => this._removeElementCommand;

    public IEnumerable<ChartComponentViewModel> Elements
    {
        get => this._componentsCache;
        set
        {
            this.SetField<IEnumerable<ChartComponentViewModel>>( ref this._componentsCache, value, nameof( Elements ) );
        }
    }

    public LegendModifierEx LegendModifier
    {
        get => this._legendModifier;
        set
        {
            this.SetField<LegendModifierEx>( ref this._legendModifier, value, nameof( LegendModifier ) );
        }
    }

    public bool AllowToHide
    {
        get => this._allowToHide;
        set
        {
            this.SetField<bool>( ref this._allowToHide, value, nameof( AllowToHide ) );
        }
    }
}