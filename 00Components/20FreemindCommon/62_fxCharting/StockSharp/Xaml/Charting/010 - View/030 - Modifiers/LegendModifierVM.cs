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

    private IEnumerable<ChartCompentViewModel> _componentsCache;

    private readonly ScichartSurfaceMVVM _scichartSurfaceVM;

    private readonly ICommand _removeElementCommand;

    public event Action<IChartElement> RemoveElmentEvent;

    public LegendModifierVM( ScichartSurfaceMVVM vm )
    {
        this._scichartSurfaceVM = vm ?? throw new ArgumentNullException( "pane" );
        this.Elements = ( IEnumerable<ChartCompentViewModel> ) vm.LegendElements;

        // BUG: need to work on ChartCompentViewModel first
        //this._removeElementCommand = new ActionCommand<ChartCompentViewModel>(
        //    vm => RemoveElmentEvent?.Invoke( vm.ChartComponent ),
        //    p => p.AllowToRemove );
    }

    public ScichartSurfaceMVVM Pane
    {
        get => this._scichartSurfaceVM;
    }

    public ICommand RemoveElementCommand => this._removeElementCommand;

    public IEnumerable<ChartCompentViewModel> Elements
    {
        get => this._componentsCache;
        set
        {
            this.SetField<IEnumerable<ChartCompentViewModel>>( ref this._componentsCache, value, nameof( Elements ) );
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