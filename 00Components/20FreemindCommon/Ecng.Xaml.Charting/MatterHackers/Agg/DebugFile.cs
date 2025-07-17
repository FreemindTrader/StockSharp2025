// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.DebugFile
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.IO;

namespace MatterHackers.Agg
{
    internal static class DebugFile
    {
        private static bool m_FileOpenedOnce;

        public static void Print( string message )
        {
            FileStream fileStream;
            if ( DebugFile.m_FileOpenedOnce )
            {
                fileStream = new FileStream( "test.txt", FileMode.Append, FileAccess.Write );
            }
            else
            {
                fileStream = new FileStream( "test.txt", FileMode.Create, FileAccess.Write );
                DebugFile.m_FileOpenedOnce = true;
            }
            StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
            streamWriter.Write( message );
            streamWriter.Close();
            fileStream.Close();
        }
    }
}
