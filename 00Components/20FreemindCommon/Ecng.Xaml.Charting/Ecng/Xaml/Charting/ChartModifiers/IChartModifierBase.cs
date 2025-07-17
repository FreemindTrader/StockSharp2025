// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ChartModifiers.IChartModifierBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.ComponentModel;
using Ecng.Xaml.Charting.Utility.Mouse;
using Ecng.Xaml.Charting.Visuals;

namespace Ecng.Xaml.Charting.ChartModifiers
{
    public interface IChartModifierBase : IReceiveMouseEvents, INotifyPropertyChanged
    {
        IServiceContainer Services
        {
            get; set;
        }

        IChartModifierSurface ModifierSurface
        {
            get;
        }

        string ModifierName
        {
            get;
        }

        bool IsAttached
        {
            get; set;
        }

        object DataContext
        {
            get; set;
        }

        bool ReceiveHandledEvents
        {
            get;
        }

        void OnAttached();

        void OnDetached();
    }
}
