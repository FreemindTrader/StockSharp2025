using Ecng.Collections;
using Ecng.Common;
using StockSharp.Algo;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Diagnostics;
using System.Threading;

namespace StockSharp.Server.Core
{
    /// <summary>
    /// <see cref="T:StockSharp.Messages.QuoteChangeMessage" /> snapshots holder.
    /// </summary>
    public class OrderBookSnapshotHolder : BaseLogReceiver, ISnapshotHolder<QuoteChangeMessage>
    {
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private readonly SynchronizedDictionary<SecurityId, RefTriple<QuoteChangeMessage, OrderBookIncrementBuilder, int>> _secIdTripleDictionary = new SynchronizedDictionary<SecurityId, RefTriple<QuoteChangeMessage, OrderBookIncrementBuilder, int>>();

        /// <inheritdoc />
        public QuoteChangeMessage TryGetSnapshot( SecurityId securityId )
        {
            RefTriple<QuoteChangeMessage, OrderBookIncrementBuilder, int> refTriple = _secIdTripleDictionary.TryGetValue( securityId );
            return refTriple == null ? null : refTriple.First.TypedClone();
        }

        /// <inheritdoc />
        public QuoteChangeMessage Process( QuoteChangeMessage quoteMsg, bool needResponse )
        {
            SecurityId securityId = quoteMsg != null ? quoteMsg.SecurityId : throw new ArgumentNullException( nameof( quoteMsg ) );
            SyncObject syncRoot = _secIdTripleDictionary.SyncRoot;
            bool flag = false;
            try
            {
                Monitor.Enter( syncRoot, ref flag );
                if ( !quoteMsg.State.HasValue )
                {
                    QuoteChangeMessage quoteChangeMessage1 = quoteMsg.TypedClone();
                    quoteChangeMessage1.State = new QuoteChangeStates?( QuoteChangeStates.SnapshotComplete );
                    RefTriple<QuoteChangeMessage, OrderBookIncrementBuilder, int> refTriple;
                    if ( _secIdTripleDictionary.TryGetValue( securityId, out refTriple ) )
                    {
                        if ( refTriple.Third == 100 )
                            return null;
                        try
                        {
                            QuoteChangeMessage quoteChangeMessage2 = needResponse ? refTriple.First.GetDelta( quoteMsg ) : null;
                            refTriple.First = quoteChangeMessage1;
                            refTriple.Third = 0;
                            return quoteChangeMessage2;
                        }
                        catch ( Exception ex )
                        {
                            if ( ++refTriple.Third == 100 )
                                this.AddErrorLog( LocalizedStrings.SnapshotTurnedOff, securityId, refTriple.Third, 100 );
                            throw new InvalidOperationException( LocalizedStrings.MessageWithError.Put( ( object )quoteMsg ), ex );
                        }
                    }
                    else
                    {
                        this.AddDebugLog( LocalizedStrings.SnapshotFormed, "OB", securityId );
                        OrderBookIncrementBuilder incrementBuilder = new OrderBookIncrementBuilder( securityId );
                        incrementBuilder.Parent = this;
                        OrderBookIncrementBuilder second = incrementBuilder;
                        if ( second.TryApply( quoteChangeMessage1 ) == null )
                            throw new InvalidOperationException();
                        _secIdTripleDictionary.Add( securityId, RefTuple.Create( quoteChangeMessage1, second, 0 ) );
                        return quoteChangeMessage1.TypedClone();
                    }
                }
                else
                {
                    RefTriple<QuoteChangeMessage, OrderBookIncrementBuilder, int> refTriple;
                    if ( _secIdTripleDictionary.TryGetValue( securityId, out refTriple ) )
                    {
                        if ( refTriple.Third == 100 )
                            return null;
                        try
                        {
                            QuoteChangeMessage quoteChangeMessage = refTriple.Second.TryApply( quoteMsg );
                            refTriple.Third = 0;
                            if ( quoteChangeMessage == null )
                                return null;
                            quoteChangeMessage.State = new QuoteChangeStates?( QuoteChangeStates.SnapshotComplete );
                            refTriple.First = quoteChangeMessage;
                            return quoteMsg;
                        }
                        catch ( Exception ex )
                        {
                            if ( ++refTriple.Third == 100 )
                                this.AddErrorLog( LocalizedStrings.SnapshotTurnedOff, securityId, refTriple.Third, 100 );
                            throw new InvalidOperationException( LocalizedStrings.MessageWithError.Put( ( object )quoteMsg ), ex );
                        }
                    }
                    else
                    {
                        OrderBookIncrementBuilder incrementBuilder = new OrderBookIncrementBuilder( securityId );
                        incrementBuilder.Parent = this;
                        OrderBookIncrementBuilder second = incrementBuilder;
                        QuoteChangeMessage first = second.TryApply( quoteMsg );
                        if ( first == null )
                            return null;
                        first.State = new QuoteChangeStates?( QuoteChangeStates.SnapshotComplete );
                        this.AddDebugLog( LocalizedStrings.SnapshotFormed, "OB", securityId );
                        _secIdTripleDictionary.Add( securityId, RefTuple.Create( first, second, 0 ) );
                        return first;
                    }
                }
            }
            finally
            {
                if ( flag )
                    Monitor.Exit( syncRoot );
            }
        }

        /// <inheritdoc />
        public void ResetSnapshot( SecurityId securityId )
        {
            if ( securityId == new SecurityId() )
                _secIdTripleDictionary.Clear();
            else
                _secIdTripleDictionary.Remove( securityId );
        }
    }
}
