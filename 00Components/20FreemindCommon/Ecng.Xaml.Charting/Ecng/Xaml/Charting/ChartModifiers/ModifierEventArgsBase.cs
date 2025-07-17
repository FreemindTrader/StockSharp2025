// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ModifierEventArgsBase
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
{
    public abstract class ModifierEventArgsBase
    {
        protected ModifierEventArgsBase()
        {
        }

        protected ModifierEventArgsBase( IReceiveMouseEvents source, bool isMaster )
        {
            this.Source = source;
            this.IsMaster = isMaster;
        }

        public bool IsMaster
        {
            get; set;
        }

        public bool Handled
        {
            get; set;
        }

        public IReceiveMouseEvents Source
        {
            get; set;
        }
    }
}
