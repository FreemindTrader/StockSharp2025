// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Code.CodeExtensions
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using Ecng.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace StockSharp.Xaml.Code
{
    /// <summary>Extension class.</summary>
    public static class CodeExtensions
    {
        /// <summary>To compile the code.</summary>
        /// <param name="compiler">Compiler.</param>
        /// <param name="code">Code.</param>
        /// <param name="name">The build name.</param>
        /// <param name="references">References.</param>
        /// <returns>The result of the compilation.</returns>
        public static CompilationResult CompileCode( this ICompiler compiler, string code, string name, IEnumerable<CodeReference> references )
        {
            return compiler.Compile( name, code, references.Select<CodeReference, string>( s => s.Location ).ToArray<string>() );
        }

        /// <summary>Are there any errors in the compilation.</summary>
        /// <param name="result">The result of the compilation.</param>
        /// <returns>
        /// <see langword="true" /> - If there are errors, <see langword="true" /> - If the compilation is performed without errors.</returns>
        public static bool HasErrors( this CompilationResult result )
        {
            return result.Errors.Any<CompilationError>( e => e.Type == CompilationErrorTypes.Error );
        }

        /// <summary>Default builds.</summary>
        public static IEnumerable<string> DefaultReferences
        {
            get
            {
                return new string[ ]
                {
                    "mscorlib",
                    "System",
                    "System.Core",
                    "System.Configuration",
                    "System.Data",
                    "System.Xaml",
                    "System.Xml",
                    "System.Xaml",
                    "WindowsBase",
                    "PresentationCore",
                    "PresentationFramework",
                    "System.ComponentModel.DataAnnotations",

                    "Ecng.Common",
                    "Ecng.Collections",
                    "Ecng.ComponentModel",
                    "Ecng.Configuration",
                    "Ecng.Localization",
                    "Ecng.Serialization",
                    "Ecng.Xaml",
                    "Ecng.Xaml.Charting",

                    "MoreLinq",
                    "MathNet.Numerics",

                    "StockSharp.Algo",
                    "StockSharp.Algo.Strategies",
                    "StockSharp.Algo.History",
                    "StockSharp.Messages",
                    "StockSharp.BusinessEntities",
                    "StockSharp.Logging",
                    "StockSharp.Localization",
                    "StockSharp.Xaml",
                    "StockSharp.Xaml.Charting",
                    "StockSharp.Xaml.Diagram",
                    "StockSharp.Studio.Core",
                    "StockSharp.Studio.Controls"
                };
            }
        }

        private static CodeReference ToReference( this string referenceName, Assembly[ ] assemblies )
        {

            if ( referenceName.IsEmpty() )
            {
                throw new ArgumentNullException( "referenceName" );
            }

            if ( assemblies == null )
            {
                throw new ArgumentNullException( "assemblies" );
            }

            Assembly asm = assemblies.FirstOrDefault( a => a.ManifestModule.Name == referenceName + ".dll" );

            if ( asm == null )
            {
                try
                {
                    asm = Assembly.Load( referenceName );
                }
                catch ( FileNotFoundException )
                {
                    return null;
                }
            }

            return new CodeReference
            {
                Name = referenceName,
                Location = asm.Location
            };
        }
        /// <summary>
        /// To modify build names to the <see cref="T:StockSharp.Xaml.Code.CodeReference" />.
        /// </summary>
        /// <param name="referenceNames">Build names.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Xaml.Code.CodeReference" />.</returns>
        public static IEnumerable<CodeReference> ToReferences( this IEnumerable<string> referenceNames )
        {
            if ( referenceNames == null )
            {
                throw new ArgumentNullException( nameof( referenceNames ) );
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            return referenceNames
                .Select( r => ToReference( r, assemblies ) )
                .Where( r => r != null )
                .ToArray();
        }

    }
}
