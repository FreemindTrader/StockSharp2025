using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Net;
using Ecng.Serialization;
using StockSharp.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;

namespace StockSharp.Fix
{
    /// <summary>Network configuration group.</summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FastFeedGroup : NotifiableObject, IPersistable
    {
        /// <summary>Network configuration group address.</summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class FastFeedGroupAddress : IPersistable
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly MulticastSourceAddress _incremental = new MulticastSourceAddress();
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly MulticastSourceAddress _snapshot = new MulticastSourceAddress();

            /// <summary>Incremental.</summary>
            [Display(Description = "IncrementalFeed", GroupName = "General", Name = "Incremental", Order = 0, ResourceType = typeof(LocalizedStrings))]
            public MulticastSourceAddress Incremental => _incremental;

            /// <summary>Snapshot.</summary>
            [Display(Description = "SnapshotFeed", GroupName = "General", Name = "Snapshot", Order = 1, ResourceType = typeof(LocalizedStrings))]
            public MulticastSourceAddress Snapshot => _snapshot;

            /// <summary>Load settings.</summary>
            /// <param name="storage">Settings storage.</param>
            public void Load(SettingsStorage storage)
            {
                Incremental.Load(storage.GetValue<SettingsStorage>(nameof(Incremental)));
                Snapshot.Load(storage.GetValue<SettingsStorage>(nameof(Snapshot)));
            }

            /// <summary>Save settings.</summary>
            /// <param name="storage">Settings storage.</param>
            public void Save(SettingsStorage storage)
            {
                storage.SetValue(nameof(Incremental), Incremental.Save());
                storage.SetValue(nameof(Snapshot), Snapshot.Save());
            }

            /// <inheritdoc />
            public override string ToString() => string.Format("{0} {1}", Snapshot, Incremental);
        }

        private readonly FastFeedGroupAddress _udpAddressChannelA = new FastFeedGroupAddress();

        private readonly FastFeedGroupAddress _udpAddressChannelB = new FastFeedGroupAddress();

        private readonly FastFeedGroupAddress _udpAddressChannelC = new FastFeedGroupAddress();

        private EndPoint _replayAddress;

        /// <summary>UDP channel A.</summary>
        [Display(Description = "MainUDPDot", GroupName = "General", Name = "UdpA", Order = 0, ResourceType = typeof(LocalizedStrings))]
        public FastFeedGroupAddress UdpA => _udpAddressChannelA;

        /// <summary>UDP channel B.</summary>
        [Display(Description = "DuplicateUDPDot", GroupName = "General", Name = "UdpB", Order = 1, ResourceType = typeof(LocalizedStrings))]
        public FastFeedGroupAddress UdpB => _udpAddressChannelB;

        /// <summary>UDP channel C.</summary>
        [Display(Description = "DuplicateUDPDot", GroupName = "General", Name = "UdpC", Order = 2, ResourceType = typeof(LocalizedStrings))]
        public FastFeedGroupAddress UdpC => _udpAddressChannelC;

        /// <summary>Replay state server address.</summary>
        [Display(Description = "ReplayServerDot", GroupName = "General", Name = "Replay", Order = 2, ResourceType = typeof(LocalizedStrings))]
        public EndPoint ReplayAddress
        {
            get => _replayAddress;
            set
            {
                _replayAddress = value;
                NotifyChanged(nameof(ReplayAddress));
            }
        }

        /// <summary>Load settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public virtual void Load(SettingsStorage storage)
        {
            UdpA.Load(storage.GetValue<SettingsStorage>(nameof(UdpA)));
            UdpB.Load(storage.GetValue<SettingsStorage>(nameof(UdpB)));
            if (storage.ContainsKey(nameof(UdpC)))
                UdpC.Load(storage.GetValue<SettingsStorage>(nameof(UdpC)));
            if (!storage.ContainsKey(nameof(ReplayAddress)))
                return;
            ReplayAddress = storage.GetValue<EndPoint>(nameof(ReplayAddress));
        }

        /// <summary>Save settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public virtual void Save(SettingsStorage storage)
        {
            storage.SetValue(nameof(UdpA), UdpA.Save());
            storage.SetValue(nameof(UdpB), UdpB.Save());
            storage.SetValue(nameof(UdpC), UdpC.Save());
            if (ReplayAddress == null)
                return;
            storage.SetValue(nameof(ReplayAddress), ReplayAddress.To<string>());
        }

        /// <inheritdoc />
        public override string ToString() => string.Format("{0} {1} {2} {3}", UdpA, UdpB, UdpC, ReplayAddress);


    }
}
