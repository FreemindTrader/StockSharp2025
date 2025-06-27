using Ecng.Common;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;

namespace fx.Charting
{
    public interface IfxChartElement : ICloneable< IfxChartElement >, INotifyPropertyChanged, ICloneable, INotifyPropertyChanging
    {
        Guid Id { get; }

        IfxChartElement ParentElement { get; }

        IEnumerable< IfxChartElement > ChildElements { get; }

        bool IsVisible { get; set; }

        bool IsLegend { get; set; }

        string XAxisId { get; set; }

        string YAxisId { get; set; }

        IChart Chart { get; }

        ChartArea ChartArea { get; }

        ChartArea PersistantChartArea { get; }
    }
}
