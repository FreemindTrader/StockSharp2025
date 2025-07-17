// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.ModifierMouseArgs
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using StockSharp.Xaml.Charting.Utility.Mouse;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public class ModifierMouseArgs : ModifierEventArgsBase
    {
        public int Delta
        {
            get; set;
        }

        public Point MousePoint
        {
            get; set;
        }

        public MouseButtons MouseButtons
        {
            get; set;
        }

        public MouseModifier Modifier
        {
            get; set;
        }

        public ModifierMouseArgs()
        {
        }

        public ModifierMouseArgs( Point mousePoint, MouseButtons mouseButtons, MouseModifier modifier, bool isMaster, IReceiveMouseEvents master )
          : this( mousePoint, mouseButtons, modifier, 0, isMaster, master )
        {
        }

        public ModifierMouseArgs( Point mousePoint, MouseButtons mouseButtons, MouseModifier modifier, int wheelDelta, bool isMaster, IReceiveMouseEvents master = null )
          : base( master, isMaster )
        {
            this.MousePoint = mousePoint;
            this.MouseButtons = mouseButtons;
            this.Modifier = modifier;
            this.Delta = wheelDelta;
        }
    }
}
