// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Panes.NameColumnTemplateSelector
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using DevExpress.Xpf.Grid;
using Ecng.Common;
using StockSharp.Messages;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Hydra.Panes
{
  internal sealed class NameColumnTemplateSelector : DataTemplateSelector
  {
    public DataTemplate HighlightTemplate { get; set; }

    public DataTemplate SimpleTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      RowData rowData = (item as EditGridCellData)?.RowData;
      if (rowData == null)
        return base.SelectTemplate(item, container);
      if (!( ( ( rowData.Row as TaskPane.TaskVisualSecurity )?.DataType ) == null ) )
        return SimpleTemplate;
      return HighlightTemplate;
    }
  }
}
