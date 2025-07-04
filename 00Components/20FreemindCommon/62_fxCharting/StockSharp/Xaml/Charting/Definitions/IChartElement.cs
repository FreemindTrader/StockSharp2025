using Ecng.Common;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;

namespace fx.Charting
{
    public interface IChartElement : ICloneable< IChartElement >, INotifyPropertyChanged, ICloneable, INotifyPropertyChanging
    {
        Guid Id { get; }

        IChartElement ParentElement { get; }

        IEnumerable< IChartElement > ChildElements { get; }

        bool IsVisible { get; set; }

        bool IsLegend { get; set; }

        string XAxisId { get; set; }

        string YAxisId { get; set; }

        IChart Chart { get; }

        ChartArea ChartArea { get; }

        ChartArea PersistantChartArea { get; }
    }
}
