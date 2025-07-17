// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ActionCommand
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace StockSharp.Xaml.Charting
{
    public class ActionCommand : ActionCommand<object>
    {
        public ActionCommand( Action execute )
          : base( ( Action<object> ) ( arg => execute() ) )
        {
        }

        public ActionCommand( Action execute, Func<bool> canExecute )
          : base( ( Action<object> ) ( arg => execute() ), ( Predicate<object> ) ( arg => canExecute() ) )
        {
        }
    }
}
