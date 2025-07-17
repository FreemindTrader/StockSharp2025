// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Licensing.Core.ActivationException
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Reflection;

namespace StockSharp.Xaml.Licensing.Core
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    [Serializable]
    public class ActivationException : ApplicationException
    {
        public static readonly Dictionary<ActivationFailureReason, string> ActivationFailureStrings = new Dictionary<ActivationFailureReason, string>() { { ActivationFailureReason.InvalidSerialKey, "Invalid Serial Key" }, { ActivationFailureReason.InvalidUser, "Invalid User" }, { ActivationFailureReason.MaxActivationsReached, "Max Activations Reached" }, { ActivationFailureReason.SerialActivatedBySomeoneElse, "Serial Activated by Somebody Else" }, { ActivationFailureReason.ServerError, "Server Error" }, { ActivationFailureReason.NotActivated, "Not Activated" }, { ActivationFailureReason.ServerUnreachable, "Server Unreacheable" }, { ActivationFailureReason.SerializationError, "Serialization Error" } };

        public ActivationFailureReason FailureReason
        {
            get; private set;
        }

        public ActivationException( string message, Exception innerException, ActivationFailureReason failureReason )
          : base( message, innerException )
        {
            this.FailureReason = failureReason;
        }
    }
}
