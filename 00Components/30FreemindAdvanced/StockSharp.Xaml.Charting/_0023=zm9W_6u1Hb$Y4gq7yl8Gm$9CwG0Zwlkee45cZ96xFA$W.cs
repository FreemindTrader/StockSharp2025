// Decompiled with JetBrains decompiler
// Type: #=zm9W_6u1Hb$Y4gq7yl8Gm$9CwG0Zwlkee45cZ96xFA$Wy
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Xaml.Charting.Visuals.Annotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00249CwG0Zwlkee45cZ96xFA\u0024Wy : 
  ObservableCollection<VerticalLineAnnotation>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  internal IList<VerticalLineAnnotation> \u0023\u003DzqSDPyRh2Gp9P;

  protected override void ClearItems()
  {
    IList<VerticalLineAnnotation> items = this.Items;
    List<VerticalLineAnnotation> verticalLineAnnotationList = new List<VerticalLineAnnotation>(items.Count);
    verticalLineAnnotationList.AddRange((IEnumerable<VerticalLineAnnotation>) items);
    this.\u0023\u003DzqSDPyRh2Gp9P = (IList<VerticalLineAnnotation>) verticalLineAnnotationList;
    base.ClearItems();
  }
}
