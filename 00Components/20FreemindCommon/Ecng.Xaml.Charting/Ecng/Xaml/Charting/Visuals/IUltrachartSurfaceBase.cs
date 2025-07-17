// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.IUltrachartSurfaceBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows.Input;
namespace Ecng.Xaml.Charting
{
    public interface IUltrachartSurfaceBase : ISuspendable, IInvalidatableElement
    {
        bool IsVisible
        {
            get;
        }

        bool DebugWhyDoesntUltrachartRender
        {
            get; set;
        }

        IServiceContainer Services
        {
            get;
        }

        object SyncRoot
        {
            get;
        }

        string ChartTitle
        {
            get; set;
        }

        bool ClipModifierSurface
        {
            get; set;
        }

        IChartModifierSurface ModifierSurface
        {
            get;
        }

        RenderPriority RenderPriority
        {
            get; set;
        }

        IRenderSurface RenderSurface
        {
            get; set;
        }

        event EventHandler<EventArgs> Rendered;

        void OnUltrachartRendered();

        void SetMouseCursor( Cursor cursor );
    }
}
