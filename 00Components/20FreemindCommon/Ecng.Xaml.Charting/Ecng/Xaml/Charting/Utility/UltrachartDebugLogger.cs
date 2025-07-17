// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Utility.UltrachartDebugLogger
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
{
    public class UltrachartDebugLogger
    {
        private static readonly UltrachartDebugLogger _instance = new UltrachartDebugLogger();
        private IUltrachartLoggerFacade _loggerFacade;

        private UltrachartDebugLogger()
        {
        }

        public static UltrachartDebugLogger Instance
        {
            get
            {
                return UltrachartDebugLogger._instance;
            }
        }

        public void WriteLine( string formatString, params object[ ] args )
        {
            if ( this._loggerFacade == null )
                return;
            this._loggerFacade.Log( formatString, args );
        }

        public void SetLogger( IUltrachartLoggerFacade loggerFacade )
        {
            this._loggerFacade = loggerFacade;
        }
    }
}
