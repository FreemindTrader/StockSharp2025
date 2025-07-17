// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.MarkupExtensions.TypeExtension
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml.Charting.Common.MarkupExtensions
{
    internal class TypeExtension : MarkupExtension
    {
        public TypeExtension()
        {
        }

        public TypeExtension( Type type )
        {
            this.Type = type;
        }

        public Type Type
        {
            get; set;
        }

        public string TypeName
        {
            get; set;
        }

        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            if ( this.Type == ( Type ) null )
            {
                if ( string.IsNullOrWhiteSpace( this.TypeName ) )
                    throw new InvalidOperationException( "No TypeName or Type specified." );
                if ( serviceProvider == null )
                    return DependencyProperty.UnsetValue;
                IXamlTypeResolver service = serviceProvider.GetService(typeof (IXamlTypeResolver)) as IXamlTypeResolver;
                if ( service == null )
                    return DependencyProperty.UnsetValue;
                this.Type = service.Resolve( this.TypeName );
            }
            return ( object ) this.Type;
        }
    }
}
