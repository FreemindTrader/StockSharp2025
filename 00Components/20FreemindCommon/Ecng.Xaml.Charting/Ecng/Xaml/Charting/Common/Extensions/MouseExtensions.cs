// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.MouseExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows.Input;
namespace Ecng.Xaml.Charting
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
