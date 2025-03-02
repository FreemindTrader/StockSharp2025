// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Panes.DataTypeCheckBox
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using StockSharp.Messages;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Hydra.Panes
{
  internal sealed class DataTypeCheckBox : CheckBox
  {
    public static readonly DependencyProperty DataTypeProperty = DependencyProperty.Register(nameof (DataType), typeof (DataType), typeof (DataTypeCheckBox));

    public DataType DataType
    {
      get
      {
        return (DataType) GetValue( DataTypeProperty );
      }
      set
      {
        SetValue( DataTypeProperty, value );
      }
    }
  }
}
