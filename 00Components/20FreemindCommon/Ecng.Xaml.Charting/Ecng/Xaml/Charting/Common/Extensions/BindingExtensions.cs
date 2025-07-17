// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.BindingExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows.Controls;
using System.Windows.Data;

namespace Ecng.Xaml.Charting.Common.Extensions
{
    internal static class BindingExtensions
    {
        internal static Binding CloneBinding( this Binding original )
        {
            Binding binding1 = new Binding();
            binding1.Path = original.Path;
            binding1.Mode = original.Mode;
            binding1.Converter = original.Converter;
            binding1.ConverterCulture = original.ConverterCulture;
            binding1.ConverterParameter = original.ConverterParameter;
            binding1.FallbackValue = original.FallbackValue;
            binding1.TargetNullValue = original.TargetNullValue;
            binding1.NotifyOnValidationError = original.NotifyOnValidationError;
            binding1.UpdateSourceTrigger = original.UpdateSourceTrigger;
            binding1.ValidatesOnDataErrors = original.ValidatesOnDataErrors;
            binding1.ValidatesOnExceptions = original.ValidatesOnExceptions;
            binding1.BindsDirectlyToSource = original.BindsDirectlyToSource;
            binding1.StringFormat = original.StringFormat;
            binding1.AsyncState = original.AsyncState;
            binding1.IsAsync = original.IsAsync;
            binding1.NotifyOnSourceUpdated = original.NotifyOnSourceUpdated;
            binding1.NotifyOnTargetUpdated = original.NotifyOnTargetUpdated;
            binding1.BindingGroupName = original.BindingGroupName;
            binding1.UpdateSourceExceptionFilter = original.UpdateSourceExceptionFilter;
            binding1.XPath = original.XPath;
            Binding binding2 = binding1;
            if ( original.Source != null )
                binding2.Source = original.Source;
            if ( original.RelativeSource != null )
                binding2.RelativeSource = original.RelativeSource;
            if ( original.ElementName != null )
                binding2.ElementName = original.ElementName;
            foreach ( ValidationRule validationRule in original.ValidationRules )
                binding2.ValidationRules.Add( validationRule );
            return binding2;
        }
    }
}
