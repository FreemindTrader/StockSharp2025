// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.NumericTickLabelViewModel
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    public class NumericTickLabelViewModel : DefaultTickLabelViewModel
    {
        private string _separator = string.Empty;
        private string _exponent = string.Empty;
        private bool _hasExponent;

        public override bool HasExponent
        {
            get
            {
                return this._hasExponent;
            }
            set
            {
                if ( this._hasExponent == value )
                    return;
                this._hasExponent = value;
                this.OnPropertyChanged( nameof( HasExponent ) );
            }
        }

        public override string Separator
        {
            get
            {
                return this._separator;
            }
            set
            {
                if ( !( this._separator != value ) )
                    return;
                this._separator = value;
                this.OnPropertyChanged( nameof( Separator ) );
            }
        }

        public override string Exponent
        {
            get
            {
                return this._exponent;
            }
            set
            {
                if ( !( this._exponent != value ) )
                    return;
                this._exponent = value;
                this.OnPropertyChanged( nameof( Exponent ) );
            }
        }
    }
}
