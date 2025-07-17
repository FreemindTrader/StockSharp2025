// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.PropertyChangedEventArgsWithValues
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.ComponentModel;

namespace StockSharp.Xaml.Charting
{
    public class PropertyChangedEventArgsWithValues : PropertyChangedEventArgs
    {
        public PropertyChangedEventArgsWithValues( string propertyName, object oldValue, object newValue )
          : base( propertyName )
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public object OldValue
        {
            get; protected set;
        }

        public object NewValue
        {
            get; protected set;
        }
    }
}
