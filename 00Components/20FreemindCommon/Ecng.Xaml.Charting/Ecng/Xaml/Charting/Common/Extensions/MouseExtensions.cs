// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Extensions.MouseExtensions
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows.Input;
using StockSharp.Xaml.Charting.ChartModifiers;

namespace StockSharp.Xaml.Charting.Common.Extensions
{
    internal static class MouseExtensions
    {
        internal static MouseModifier GetCurrentModifier()
        {
            MouseModifier mouseModifier = MouseModifier.None;
            switch ( Keyboard.Modifiers )
            {
                case ModifierKeys.Alt:
                    mouseModifier = MouseModifier.Alt;
                    break;
                case ModifierKeys.Control:
                    mouseModifier = MouseModifier.Ctrl;
                    break;
                case ModifierKeys.Shift:
                    mouseModifier = MouseModifier.Shift;
                    break;
            }
            return mouseModifier;
        }
    }
}
