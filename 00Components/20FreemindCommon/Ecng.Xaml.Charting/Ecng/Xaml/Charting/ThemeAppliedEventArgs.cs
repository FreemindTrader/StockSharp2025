// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ThemeAppliedEventArgs
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Windows;

namespace fx.Xaml.Charting
{
    public class ThemeAppliedEventArgs : EventArgs
    {
        private readonly FrameworkElement _control;
        private readonly string _newTheme;

        public ThemeAppliedEventArgs( FrameworkElement control, string newTheme )
        {
            this._control = control;
            this._newTheme = newTheme;
        }

        public FrameworkElement Control
        {
            get
            {
                return this._control;
            }
        }

        public string NewTheme
        {
            get
            {
                return this._newTheme;
            }
        }
    }
}
