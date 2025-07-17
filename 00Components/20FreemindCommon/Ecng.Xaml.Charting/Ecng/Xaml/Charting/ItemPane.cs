// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ItemPane
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace fx.Xaml.Charting
{
    public class ItemPane : INotifyPropertyChanged
    {
        private bool _isTabbed;

        public event PropertyChangedEventHandler PropertyChanged;

        public FrameworkElement PaneElement
        {
            get; set;
        }

        public IChildPane PaneViewModel
        {
            get; set;
        }

        internal bool IsMainPane
        {
            get; set;
        }

        public bool IsTabbed
        {
            get
            {
                return this._isTabbed;
            }
            internal set
            {
                this._isTabbed = value;
                this.OnPropertyChanged( nameof( IsTabbed ) );
            }
        }

        public ICommand ChangeOrientationCommand
        {
            get; set;
        }

        public ICommand ClosePaneCommand
        {
            get; set;
        }

        private void OnPropertyChanged( string propertyName )
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ( propertyChanged == null )
                return;
            propertyChanged( ( object ) this, new PropertyChangedEventArgs( propertyName ) );
        }
    }
}
