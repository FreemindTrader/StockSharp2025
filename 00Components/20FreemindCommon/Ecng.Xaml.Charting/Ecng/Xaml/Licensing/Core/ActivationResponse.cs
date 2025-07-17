// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Licensing.Core.ActivationResponse
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Reflection;

namespace StockSharp.Xaml.Licensing.Core
{
    [Obfuscation( ApplyToMembers = true, Exclude = false, Feature = "encryptmethod;encryptstrings;encryptconstants", StripAfterObfuscation = true )]
    public class ActivationResponse
    {
        public bool Success
        {
            get; set;
        }

        public string DesignTimeLicense
        {
            get; set;
        }

        public string OrderID
        {
            get; set;
        }

        public string DeveloperName
        {
            get; set;
        }

        public string DeveloperEmail
        {
            get; set;
        }

        public int Quantity
        {
            get; set;
        }

        public string ProductCode
        {
            get; set;
        }

        public DateTime? SupportExpiryDate
        {
            get; set;
        }

        public Guid SerialKey
        {
            get; set;
        }

        public int ActivationID
        {
            get; set;
        }

        public DateTime? ActivationDate
        {
            get; set;
        }

        public string ActivationErrorMessage
        {
            get; set;
        }

        public ActivationFailureReason ActivationFailureReason
        {
            get; set;
        }

        public ActivationResponse()
        {
        }

        public ActivationResponse( Guid serialKey, int activationID, DateTime? activationDate, string orderId, string developerName, string productCode, DateTime? supportExpiry, string developerEmail, int quantity )
        {
            Success = true;
            OrderID = orderId;
            DeveloperName = developerName;
            ProductCode = productCode;
            SupportExpiryDate = supportExpiry;
            SerialKey = serialKey;
            ActivationID = activationID;
            ActivationDate = activationDate;
            DeveloperEmail = developerEmail;
            Quantity = quantity;
        }

        public ActivationResponse( ActivationException error )
        {
            Success = false;
            ActivationErrorMessage = error.Message;
            ActivationFailureReason = error.FailureReason;
        }

        public override bool Equals( object obj )
        {
            if ( obj == null )
                return false;
            if ( this == obj )
                return true;
            if ( obj.GetType() != GetType() )
                return false;
            return Equals( ( ActivationResponse ) obj );
        }

        protected bool Equals( ActivationResponse other )
        {
            if ( Success == other.Success && string.Equals( DesignTimeLicense, other.DesignTimeLicense ) && ( string.Equals( OrderID, other.OrderID ) && string.Equals( DeveloperName, other.DeveloperName ) ) && string.Equals( ProductCode, other.ProductCode ) )
            {
                DateTime? nullable = SupportExpiryDate;
                DateTime? supportExpiryDate = other.SupportExpiryDate;
                if ( ( nullable.HasValue == supportExpiryDate.HasValue ? ( nullable.HasValue ? ( nullable.GetValueOrDefault() == supportExpiryDate.GetValueOrDefault() ? 1 : 0 ) : 1 ) : 0 ) != 0 && object.Equals( ( object ) SerialKey, ( object ) other.SerialKey ) && ActivationID == other.ActivationID )
                {
                    DateTime? activationDate = ActivationDate;
                    nullable = other.ActivationDate;
                    if ( ( activationDate.HasValue == nullable.HasValue ? ( activationDate.HasValue ? ( activationDate.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0 ) : 1 ) : 0 ) != 0 && Quantity == other.Quantity && string.Equals( DeveloperEmail, other.DeveloperEmail ) )
                    {
                        if ( ActivationErrorMessage == other.ActivationErrorMessage )
                            return true;
                        if ( ActivationErrorMessage != null )
                            return ActivationErrorMessage.Equals( other.ActivationErrorMessage );
                        return false;
                    }
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int num1 = ((((Success.GetHashCode() * 397 ^ (DesignTimeLicense != null ? DesignTimeLicense.GetHashCode() : 0)) * 397 ^ (OrderID != null ? OrderID.GetHashCode() : 0)) * 397 ^ (DeveloperName != null ? DeveloperName.GetHashCode() : 0)) * 397 ^ (ProductCode != null ? ProductCode.GetHashCode() : 0)) * 397;
            DateTime? nullable = SupportExpiryDate;
            int hashCode1 = nullable.GetHashCode();
            int num2 = (num1 ^ hashCode1) * 397;
            Guid serialKey = SerialKey;
            int hashCode2 = SerialKey.GetHashCode();
            int num3 = ((num2 ^ hashCode2) * 397 ^ ActivationID.GetHashCode()) * 397;
            nullable = ActivationDate;
            int hashCode3 = nullable.GetHashCode();
            return num3 ^ hashCode3;
        }
    }
}
