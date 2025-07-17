// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.Axes.DefaultTickLabelViewModel
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.ComponentModel;

namespace Ecng.Xaml.Charting.Visuals.Axes
{
    public class DefaultTickLabelViewModel : ITickLabelViewModel, INotifyPropertyChanged
    {
        private string _text;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                if ( !( this._text != value ) )
                    return;
                this._text = value;
                this.OnPropertyChanged( nameof( Text ) );
            }
        }

        protected virtual void OnPropertyChanged( string propertyName )
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ( propertyChanged == null )
                return;
            propertyChanged( ( object ) this, new PropertyChangedEventArgs( propertyName ) );
        }

        public virtual bool HasExponent
        {
            get; set;
        }

        public virtual string Separator
        {
            get; set;
        }

        public virtual string Exponent
        {
            get; set;
        }
    }
}
