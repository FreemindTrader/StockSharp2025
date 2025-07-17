// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Licensing.AnnotationCollectionLicenseProvider
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Collections.Specialized;
using System.Reflection;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting.Licensing
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    internal class AnnotationCollectionLicenseProvider : Credentials, IUltrachartLicenseProvider
    {
        public void Validate( object parameter )
        {
            AnnotationCollection annotationCollection = parameter as AnnotationCollection;
            if ( annotationCollection == null || this.LicenseType != Decoder.LicenseType.Full && this.LicenseType != Decoder.LicenseType.Trial )
                return;
            annotationCollection.CollectionChanged += new NotifyCollectionChangedEventHandler( annotationCollection.AnnotationCollectionChanged );
        }
    }
}
