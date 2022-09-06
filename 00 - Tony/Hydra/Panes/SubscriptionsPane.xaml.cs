// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Panes.SubscriptionsPane
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Server.Core;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Hydra.Panes
{
    [Guid( "DECB453A-44FD-4745-90F0-AB5141ACE19E" )]
    [DisplayNameLoc( "Subscriptions" )]
    [VectorIcon( "Subscribe" )]
    public partial class SubscriptionsPane : BaseStudioControl, IPane, IStudioControl, IPersistable, IDisposable, IComponentConnector
    {
        private readonly PairSet<SessionInfo, string> _sessions = new PairSet<SessionInfo, string>( EqualityComparer<SessionInfo>.Default, StringComparer.InvariantCultureIgnoreCase );
        private readonly PairSet<SubscriptionInfo, ServerSubscription> _subscriptions = new PairSet<SubscriptionInfo, ServerSubscription>();
        private readonly IMessageListener _listener;
        

        public SubscriptionsPane()
        {
            InitializeComponent();
            _listener = ConfigManager.GetService<IMessageListener>();
            _listener.SessionConnected += new Action<IMessageListenerSession>( OnSessionConnected );
            _listener.SessionDisconnected += new Action<IMessageListenerSession>( OnSessionDisconnected );
            _listener.SubscriptionChanged += new Action<ServerSubscription>( OnSubscriptionChanged );
            foreach ( IMessageListenerSession session1 in _listener.Sessions )
            {
                SessionInfo session2 = AddSession( session1 );
                foreach ( ServerSubscription subscription in _listener.GetSubscriptions( session1 ) )
                    AddSubscription( session2, subscription );
            }
        }

        public override void Dispose()
        {
            _listener.SessionConnected -= new Action<IMessageListenerSession>( OnSessionConnected );
            _listener.SessionDisconnected -= new Action<IMessageListenerSession>( OnSessionDisconnected );
            _listener.SubscriptionChanged -= new Action<ServerSubscription>( OnSubscriptionChanged );
            base.Dispose();
        }

        private string GetFullId( IMessageListenerSession session )
        {
            if ( session == null )
                throw new ArgumentNullException( nameof( session ) );
            return session.SessionId + "-" + session.ServerSession.SessionId;
        }

        private void AddSubscription( SessionInfo session, ServerSubscription subscription )
        {
            SubscriptionInfo key = new SubscriptionInfo( session, new Subscription( subscription.SubscriptionMessage.TypedClone(), ( SecurityMessage )null ) { State = subscription.State } );
            _subscriptions.Add( key, subscription );
            SubscriptionPanel.Subscriptions.Add( key );
        }

        private void RemoveSubscription( SubscriptionInfo info )
        {
            _subscriptions.Remove( info );
            SubscriptionPanel.Subscriptions.Remove( info );
        }

        private SessionInfo AddSession( IMessageListenerSession session )
        {
            string fullId = GetFullId( session );
            SessionInfo key1;
            if ( _sessions.TryGetKey( GetFullId( session ), out key1 ) )
                return key1;
            SessionInfo key2 = new SessionInfo( fullId, session.CreationTime, session.Address ) { IsConnected = true, UpdatedTime = session.UpdatedTime };
            _sessions.Add( key2, fullId );
            SubscriptionPanel.Sessions.Add( fullId, key2 );
            return key2;
        }

        private void RemoveSession( IMessageListenerSession session )
        {
            string fullId = GetFullId( session );
            _sessions.RemoveByValue( fullId );
            foreach ( SubscriptionInfo info in _subscriptions.Keys.Where( p => p.Session.Id.EqualsIgnoreCase( fullId ) ).ToArray() )
                RemoveSubscription( info );
            SessionInfo sessionInfo;
            if ( !SubscriptionPanel.Sessions.TryGetValue( fullId, out sessionInfo ) )
                return;
            sessionInfo.IsConnected = false;
            SubscriptionPanel.Sessions.Remove( fullId );
        }

        private void OnSessionConnected( IMessageListenerSession session )
        {
            this.GuiAsync( () => AddSession( session ) );
        }

        private void OnSessionDisconnected( IMessageListenerSession session )
        {
            this.GuiAsync( () => RemoveSession( session ) );
        }

        private void OnSubscriptionChanged( ServerSubscription subscription )
        {
            bool isStopped = subscription.State == SubscriptionStates.Stopped;
            IMessageListenerSession session = subscription.Session;
            this.GuiAsync( () =>
         {
             SubscriptionInfo key;
             if ( _subscriptions.TryGetKey( subscription, out key ) )
             {
                 if ( isStopped )
                 {
                     RemoveSubscription( key );
                 }
                 else
                 {
                     if ( key.Subscription.State != subscription.State )
                     {
                         key.Subscription.State = subscription.State;
                         key.RefreshSubscription();
                     }
                     key.BytesSent = subscription.BytesSent;
                     key.BytesReceived = subscription.BytesReceived;
                     key.ErrorCount = subscription.ErrorCount;
                     key.MessagesCount = subscription.MessagesCount;
                     key.LastMessageTime = subscription.LastMessageTime;
                 }
                 key.Session.UpdatedTime = session.UpdatedTime;
             }
             else
             {
                 if ( isStopped )
                     return;
                 AddSubscription( AddSession( session ), subscription );
             }
         } );
        }

        private void SubscriptionPanel_SubscriptionRemoving( SubscriptionInfo info )
        {
            ServerSubscription subscription;
            if ( !_subscriptions.TryGetValue( info, out subscription ) )
                return;
            _listener.RemoveSubscription( subscription );
        }

        private void SubscriptionPanel_SubscriptionSuspending( SubscriptionInfo info )
        {
            ServerSubscription subscription;
            if ( !_subscriptions.TryGetValue( info, out subscription ) )
                return;
            _listener.Suspend( subscription );
        }

        private void SubscriptionPanel_SubscriptionResuming( SubscriptionInfo info )
        {
            ServerSubscription subscription;
            if ( !_subscriptions.TryGetValue( info, out subscription ) )
                return;
            _listener.Resume( subscription );
        }

        private IEnumerable<IMessageListenerSession> GetSessions(
          string fullId )
        {
            return _listener.Sessions.Where( s => GetFullId( s ).EqualsIgnoreCase( fullId ) );
        }

        private void SubscriptionPanel_SessionRemoving( SessionInfo info )
        {
            string fullId;
            if ( !_sessions.TryGetValue( info, out fullId ) )
                return;
            foreach ( IMessageListenerSession session in GetSessions( fullId ) )
                _listener.Disconnect( session );
        }

        private void SubscriptionPanel_SessionSuspending( SessionInfo info )
        {
            string fullId;
            if ( !_sessions.TryGetValue( info, out fullId ) )
                return;
            foreach ( IMessageListenerSession session in GetSessions( fullId ) )
                _listener.Suspend( session );
        }

        private void SubscriptionPanel_SessionResuming( SessionInfo info )
        {
            string fullId;
            if ( !_sessions.TryGetValue( info, out fullId ) )
                return;
            foreach ( IMessageListenerSession session in GetSessions( fullId ) )
                _listener.Resume( session );
        }

        bool IPane.IsValid
        {
            get
            {
                return true;
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "SubscriptionPanel", SubscriptionPanel.Save() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            if ( !storage.Contains( "SubscriptionPanel" ) )
                return;
            SubscriptionPanel.Load( storage.GetValue<SettingsStorage>( "SubscriptionPanel", null ) );
        }        
    }
}
