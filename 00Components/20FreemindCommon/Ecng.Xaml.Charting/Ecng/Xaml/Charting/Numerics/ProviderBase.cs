// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.ProviderBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    public abstract class ProviderBase
    {
        private IAxis _parentAxis;

        public IAxis ParentAxis
        {
            get
            {
                return this._parentAxis;
            }
            protected set
            {
                this._parentAxis = value;
            }
        }

        public virtual void Init( IAxis parentAxis )
        {
            this.ParentAxis = parentAxis;
        }
    }
}
