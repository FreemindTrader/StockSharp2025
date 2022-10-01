
using Ecng.Collections;
using Ecng.Common;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockSharp.Server.Core
{
    /// <summary>
    /// <see cref="T:StockSharp.Messages.Level1ChangeMessage" /> snapshots holder.
    /// </summary>
    public class Level1SnapshotHolder : BaseLogReceiver, ISnapshotHolder<Level1ChangeMessage>
    {
        
        private readonly Dictionary<SecurityId, Level1ChangeMessage> _secIdToLevel1ChangeMsgDictionary = new Dictionary<SecurityId, Level1ChangeMessage>();

        /// <inheritdoc />
        public Level1ChangeMessage TryGetSnapshot( SecurityId securityId )
        {
            Level1ChangeMessage level1ChangeMessage = _secIdToLevel1ChangeMsgDictionary.TryGetValue( securityId );
            return level1ChangeMessage == null ? null : level1ChangeMessage.TypedClone();
        }

        /// <inheritdoc />
        public Level1ChangeMessage Process( Level1ChangeMessage level1Msg, bool needResponse )
        {
            SecurityId key = level1Msg != null ? level1Msg.SecurityId : throw new ArgumentNullException( nameof( level1Msg ) );
            Level1ChangeMessage level1ChangeMessage1;
            if ( _secIdToLevel1ChangeMsgDictionary.TryGetValue( key, out level1ChangeMessage1 ) )
            {
                Level1ChangeMessage level1ChangeMessage2;
                if ( !needResponse )
                {
                    level1ChangeMessage2 = null;
                }
                else
                {
                    level1ChangeMessage2 = new Level1ChangeMessage();
                    level1ChangeMessage2.SecurityId = key;
                    level1ChangeMessage2.ServerTime = level1Msg.ServerTime;
                    level1ChangeMessage2.LocalTime = level1Msg.LocalTime;
                    level1ChangeMessage2.BuildFrom = level1Msg.BuildFrom;
                }
                Level1ChangeMessage level1ChangeMessage3 = level1ChangeMessage2;
                IDictionary<Level1Fields, object> changes = level1ChangeMessage1.Changes;
                foreach ( KeyValuePair<Level1Fields, object> change in level1Msg.Changes )
                {
                    object obj;
                    if ( changes.TryGetValue( change.Key, out obj ) )
                    {
                        if ( ( obj != null ? ( !obj.Equals( change.Value ) ? 1 : 0 ) : 1 ) != 0 )
                        {
                            changes[change.Key] = change.Value;
                            level1ChangeMessage3?.Changes.Add( change );
                        }
                    }
                    else
                    {
                        changes.Add( change );
                        level1ChangeMessage3?.Changes.Add( change );
                    }
                }
                level1ChangeMessage1.LocalTime = level1Msg.LocalTime;
                level1ChangeMessage1.ServerTime = level1Msg.ServerTime;
                return level1ChangeMessage3;
            }
            this.AddDebugLog( LocalizedStrings.SnapshotFormed, "L1", key );
            _secIdToLevel1ChangeMsgDictionary.Add( key, level1Msg.TypedClone() );
            return level1Msg;
        }

        /// <inheritdoc />
        public void ResetSnapshot( SecurityId securityId )
        {
            if ( securityId == new SecurityId() )
                _secIdToLevel1ChangeMsgDictionary.Clear();
            else
                _secIdToLevel1ChangeMsgDictionary.Remove( securityId );
        }
    }
}
