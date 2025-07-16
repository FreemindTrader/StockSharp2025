using System;

namespace StockSharp.Xaml.Charting.Rendering.Common;

public interface ISprite2D : IDisposable
{
    float Width
    {
        get;
    }

    float Height
    {
        get;
    }
}
