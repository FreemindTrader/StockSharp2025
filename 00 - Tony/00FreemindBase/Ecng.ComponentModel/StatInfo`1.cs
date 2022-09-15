using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ecng.ComponentModel
{
    public struct StatInfo<TAction>
    {
        public int UniqueCount {  get; set; }

        public int PendingCount {  get; set; }

        public IPAddress AggressiveAddress {  get; set; }

        public TimeSpan AggressiveTime {  get; set; }

        public StatInfo<TAction>.Item<int>[ ] Freq {  get; set; }

        public StatInfo<TAction>.Item<TimeSpan>[ ] Longest {  get; set; }

        public StatInfo<TAction>.Item<TimeSpan>[ ] Pendings {  get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine( string.Format( "Unique count: {0}", ( object )this.UniqueCount ) );
            stringBuilder.AppendLine( string.Format( "Pending count: {0}", ( object )this.PendingCount ) );
            stringBuilder.AppendLine( string.Format( "Aggressive IP: {0}", ( object )this.AggressiveAddress ) );
            stringBuilder.AppendLine( string.Format( "Aggressive time: {0}", ( object )this.AggressiveTime ) );
            stringBuilder.AppendLine();
            stringBuilder.AppendLine( "Freq:" );
            foreach ( StatInfo<TAction>.Item<int> obj in this.Freq )
                stringBuilder.AppendLine( string.Format( "({0}): '{1}'", ( object )obj.Value, ( object )obj.Action ) );
            stringBuilder.AppendLine();
            stringBuilder.AppendLine( "Long:" );
            foreach ( StatInfo<TAction>.Item<TimeSpan> obj in this.Longest )
                stringBuilder.AppendLine( string.Format( "({0}): '{1}'", ( object )obj.Value, ( object )obj.Action ) );
            stringBuilder.AppendLine();
            stringBuilder.AppendLine( "Pend:" );
            foreach ( StatInfo<TAction>.Item<TimeSpan> pending in this.Pendings )
                stringBuilder.AppendLine( string.Format( "({0}/{1}): '{2}'", ( object )pending.Value, ( object )pending.Address, ( object )pending.Action ) );
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }

        public struct Item<TValue>
        {
            public TValue Value {  get; set; }

            public IPAddress Address {  get; set; }

            public TAction Action {  get; set; }
        }
    }
}
