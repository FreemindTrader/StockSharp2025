// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.VerticalLineAnnotationCollection
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StockSharp.Xaml.Charting.Visuals.Annotations
{
    public class VerticalLineAnnotationCollection : ObservableCollection<VerticalLineAnnotation>
    {
        internal IList<VerticalLineAnnotation> OldItems;

        protected override void ClearItems()
        {
            this.OldItems = ( IList<VerticalLineAnnotation> ) new List<VerticalLineAnnotation>( ( IEnumerable<VerticalLineAnnotation> ) this.Items );
            base.ClearItems();
        }
    }
}
