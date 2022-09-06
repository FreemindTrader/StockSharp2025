using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;

namespace StockSharp.Server.Core
{
    /// <summary>Credentials with set of permissions.</summary>
    public class PermissionCredentials : ServerCredentials
    {
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private IEnumerable<IPAddress> _ipRestriction = Enumerable.Empty<IPAddress>();
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private readonly SynchronizedDictionary<UserPermissions, SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool>> _userPermissionDictionary = new SynchronizedDictionary<UserPermissions, SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool>>();

        /// <summary>IP address restrictions.</summary>
        public IEnumerable<IPAddress> IpRestrictions
        {
            get => _ipRestriction;
            set
            {
                _ipRestriction = value ?? throw new ArgumentNullException( nameof( value ) );
                NotifyChanged( nameof( _ipRestriction ) );
            }
        }

        /// <summary>Permission set.</summary>
        public SynchronizedDictionary<UserPermissions, SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool>> Permissions => _userPermissionDictionary;

        /// <inheritdoc />
        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( nameof( IpRestrictions ), IpRestrictions.Select( p => p.To<string>() ).JoinComma() );
            SyncObject syncRoot = Permissions.SyncRoot;
            bool flag = false;
            try
            {
                Monitor.Enter( syncRoot, ref flag );
                storage.SetValue( nameof( Permissions ), Permissions.ToDictionary( p => p.To<IPAddress>() ) );
            }
            finally
            {
                if ( flag )
                    Monitor.Exit( syncRoot );
            }
        }

        /// <inheritdoc />
        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            IpRestrictions = storage.GetValue<string>( nameof( IpRestrictions ) ).SplitByComma().Select( p => p.To<IPAddress>() ).ToArray();
            var permissions = storage.GetValue<IDictionary<UserPermissions, IDictionary<Tuple<string, string, object, DateTime?>, bool>>>( nameof( Permissions ) );
            SyncObject syncRoot = Permissions.SyncRoot;

            bool lockTaken = false;
            try
            {
                Monitor.Enter( syncRoot, ref lockTaken );
                Permissions.Clear();
                if ( permissions == null )
                    return;

                foreach ( var permission in permissions )
                    Permissions.SafeAdd( permission.Key ).AddRange( permission.Value );
            }
            finally
            {
                if ( lockTaken )
                    Monitor.Exit( syncRoot );
            }
        }

        //[Serializable]
        //private sealed class SomeClass
        //{
        //    public static readonly SomeClass _someClassMember = new SomeClass();
        //    public static Func<IPAddress, string> _funtion1;
        //    public static Func<KeyValuePair<UserPermissions, SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool>>, UserPermissions> _funtion2;
        //    public static Func<KeyValuePair<Tuple<string, string, object, DateTime?>, bool>, Tuple<string, string, object, DateTime?>> _funtion3;
        //    public static Func<KeyValuePair<Tuple<string, string, object, DateTime?>, bool>, bool> _funtion4;
        //    public static Func<KeyValuePair<UserPermissions, SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool>>, IDictionary<Tuple<string, string, object, DateTime?>, bool>> _funtion5;
        //    public static Func<string, IPAddress> _funtion6;

        //    internal string _funtion7( IPAddress _param1 ) => _param1.To<string>();

        //    internal UserPermissions _funtion8(
        //      KeyValuePair<UserPermissions, SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool>> _param1 )
        //    {
        //        return _param1.Key;
        //    }

        //    internal IDictionary<Tuple<string, string, object, DateTime?>, bool> _funtion9(
        //      KeyValuePair<UserPermissions, SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool>> _param1 )
        //    {
        //        return Enumerable.ToDictionary<KeyValuePair<Tuple<string, string, object, DateTime?>, bool>, Tuple<string, string, object, DateTime?>, bool>( ( IEnumerable<M0> ) _param1.Value, ( Func<M0, M1> ) ( PermissionCredentials.SomeClass._funtion3 ?? ( PermissionCredentials.SomeClass._funtion3 = new Func<KeyValuePair<Tuple<string, string, object, DateTime?>, bool>, Tuple<string, string, object, DateTime?>>( PermissionCredentials.SomeClass._someClassMember._funtion10 ) ) ), ( Func<M0, M2> ) ( PermissionCredentials.SomeClass._funtion4 ?? ( PermissionCredentials.SomeClass._funtion4 = new Func<KeyValuePair<Tuple<string, string, object, DateTime?>, bool>, bool>( PermissionCredentials.SomeClass._someClassMember._funtion11 ) ) ) );
        //    }

        //    internal Tuple<string, string, object, DateTime?> _funtion10(
        //      KeyValuePair<Tuple<string, string, object, DateTime?>, bool> _param1 )
        //    {
        //        return _param1.Key;
        //    }

        //    internal bool _funtion11(
        //      KeyValuePair<Tuple<string, string, object, DateTime?>, bool> _param1 )
        //    {
        //        return _param1.Value;
        //    }

        //    internal IPAddress _funtion12( string _param1 ) => _param1.To<IPAddress>();
        //}
    }
}
