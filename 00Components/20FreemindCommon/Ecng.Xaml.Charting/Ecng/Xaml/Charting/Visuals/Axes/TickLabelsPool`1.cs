// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.TickLabelsPool`1
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
namespace fx.Xaml.Charting
{
    internal class TickLabelsPool<T> : ObjectPool<T>, ITickLabelsPool where T : DefaultTickLabel, new()
    {
        public TickLabelsPool()
        {
        }

        public TickLabelsPool( int initAmount, Func<DefaultTickLabel, DefaultTickLabel> actionOnCreation )
          : base( initAmount, ( Func<T, T> ) ( T => ( T ) actionOnCreation( ( DefaultTickLabel ) T ) ) )
        {
        }

        public DefaultTickLabel Get()
        {
            return ( DefaultTickLabel ) base.Get();
        }

        public DefaultTickLabel Get( Func<DefaultTickLabel, DefaultTickLabel> actionOnCreation )
        {
            return ( DefaultTickLabel ) this.Get( ( Func<T, T> ) ( T => ( T ) actionOnCreation( ( DefaultTickLabel ) T ) ) );
        }

        public void Put( DefaultTickLabel item )
        {
            this.Put( ( T ) item );
        }
    }
}
