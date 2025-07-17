// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.IReceiveMouseEvents
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    public interface IReceiveMouseEvents
    {
        bool IsEnabled
        {
            get; set;
        }

        string MouseEventGroup
        {
            get; set;
        }

        bool CanReceiveMouseEvents();

        void OnModifierDoubleClick( ModifierMouseArgs e );

        void OnModifierMouseDown( ModifierMouseArgs e );

        void OnModifierMouseMove( ModifierMouseArgs e );

        void OnModifierMouseUp( ModifierMouseArgs e );

        void OnModifierMouseWheel( ModifierMouseArgs e );

        void OnModifierTouchDown( ModifierTouchManipulationArgs e );

        void OnModifierTouchMove( ModifierTouchManipulationArgs e );

        void OnModifierTouchUp( ModifierTouchManipulationArgs e );

        void OnMasterMouseLeave( ModifierMouseArgs e );
    }
}
