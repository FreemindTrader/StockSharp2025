// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.StrategiesRegistrySettings
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using System.IO;

namespace StockSharp.Studio.Core
{
    public class StrategiesRegistrySettings
    {
        public string Compositions { get; }

        public string Strategies { get; }

        public string LiveStrategies { get; }

        public string RemoteStrategies { get; }

        public string SourceCodeInfos { get; }

        public string PaletteTree { get; }

        public string SolutionTree { get; }

        public string Templates { get; }

        public StrategiesRegistrySettings( string path )
        {
            Compositions = Path.GetFullPath( Path.Combine( path, nameof( Compositions ) ) );
            Strategies = Path.GetFullPath( Path.Combine( path, nameof( Strategies ) ) );
            LiveStrategies = Path.GetFullPath( Path.Combine( path, nameof( LiveStrategies ) ) );
            RemoteStrategies = Path.GetFullPath( Path.Combine( path, nameof( RemoteStrategies ) ) );
            SourceCodeInfos = Path.GetFullPath( Path.Combine( path, "SourceCode" ) );
            Templates = Path.GetFullPath( Path.Combine( path, nameof( Templates ) ) );
            PaletteTree = Path.GetFullPath( Path.Combine( path, "paletteTree.json" ) );
            SolutionTree = Path.GetFullPath( Path.Combine( path, "solutionTree.json" ) );
        }
    }
}
