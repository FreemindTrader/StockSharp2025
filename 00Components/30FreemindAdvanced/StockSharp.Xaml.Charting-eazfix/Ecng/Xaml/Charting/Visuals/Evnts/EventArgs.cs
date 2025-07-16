

using System;
using System.Diagnostics;

namespace StockSharp.Xaml.Charting.Visuals.Events;

#nullable disable
public class EventArgs : System.EventArgs
{
    private readonly bool _boolean1;

    private readonly bool _boolean2;

    public EventArgs(bool _param1, bool _param2)
    {
        this._boolean1 = _param1;
        this._boolean2 = _param2;
    }

    public bool BoolOne => this._boolean1;

    public bool BoolTwo => this._boolean2;
}
