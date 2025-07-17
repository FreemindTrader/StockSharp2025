// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.StringlyTyped
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    public abstract class StringlyTyped
    {
        protected StringlyTyped( string value )
        {
            this.Value = value;
        }

        protected bool Equals( StringlyTyped other )
        {
            return string.Equals( this.Value, other?.Value );
        }

        public override int GetHashCode()
        {
            if ( this.Value == null )
                return 0;
            return this.Value.GetHashCode();
        }

        public string Value
        {
            get; private set;
        }

        public override bool Equals( object obj )
        {
            return this.Equals( obj as StringlyTyped );
        }

        public override string ToString()
        {
            return string.Format( "{0}: [{1}]", ( object ) this.GetType().Name, ( object ) ( this.Value ?? "Null" ) );
        }
    }
}
