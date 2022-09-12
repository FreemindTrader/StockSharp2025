using Ecng.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StockSharp.Xaml.Code
{
    public static class CodeExtensions
    {
        public static CompilationResult CompileCode( this ICompiler language, string code, string name, IEnumerable<CodeReference> references )
        {
            return language.Compile( name, code, references.Select( s => s.Location ).ToArray() );
        }

        public static bool HasErrors( this CompilationResult result )
        {
            return result.Errors.Any( e => e.Type == CompilationErrorTypes.Error );
        }

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

            Assembly asm = assemblies.FirstOrDefault(a => a.ManifestModule.Name == referenceName + ".dll");

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