using Ecng.Common;
using Ecng.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Diagram
{
    /// <summary>
    /// <see cref="F:StockSharp.Messages.OrderTypes.Conditional" /> settings.
    ///     </summary>
    public class OrderConditionSettings : IPersistable
    {

        private Type _adapterType;

        private readonly IDictionary<string, object> _parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.OrderConditionSettings" />.
        /// </summary>
        public OrderConditionSettings()
        {
            this._parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// <see cref="T:StockSharp.Messages.IMessageAdapter" /> type.
        ///     </summary>
        public Type AdapterType
        {
            get
            {
                return this._adapterType;
            }
            set
            {
                this._adapterType = value;
            }
        }

        /// <summary>Condition parameters.</summary>
        public IDictionary<string, object> Parameters
        {
            get
            {
                return this._parameters;
            }
        }

        void IPersistable.Load( SettingsStorage settings )
        {
            this.AdapterType = settings.GetValue<Type>( nameof( AdapterType ), null );
            foreach ( KeyValuePair<string, object> keyValuePair in settings.GetValue<SettingsStorage>( nameof( Parameters ), null ) )
                this.Parameters[keyValuePair.Key] = keyValuePair.Value;
        }

        void IPersistable.Save( SettingsStorage setting )
        {
            Type adapterType = this.AdapterType;
            string str = adapterType != null ? adapterType.GetTypeName( false ) : null;
            setting.SetValue<string>( nameof( AdapterType ), str );

            var param = new SettingsStorage();

            foreach ( var parameter in Parameters )
                param.SetValue<object>( parameter.Key, parameter.Value );

            setting.SetValue<SettingsStorage>( nameof( Parameters ), param );
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if ( !( this.AdapterType != null ) )
                return string.Empty;

            return string.Concat( nameof( AdapterType ), this.Parameters.Select<KeyValuePair<string, object>, string>( OrderConditionSettings.LamdaShit.Func00023 ?? ( OrderConditionSettings.LamdaShit.Func00023 = new Func<KeyValuePair<string, object>, string>( OrderConditionSettings.LamdaShit._lamdaShit.Func00024 ) ) ).JoinCommaSpace(), nameof( -1260197415 ) );
        }

        [Serializable]
        private sealed class LamdaShit
        {
            public static readonly OrderConditionSettings.LamdaShit _lamdaShit = new OrderConditionSettings.LamdaShit();
            public static Func<KeyValuePair<string, object>, string> Func00023;

            internal string Func00024( KeyValuePair<string, object> _param1 )
            {
                return string.Format( nameof( -1260192548 ), ( object )_param1.Key, _param1.Value );
            }
        }
    }
}
