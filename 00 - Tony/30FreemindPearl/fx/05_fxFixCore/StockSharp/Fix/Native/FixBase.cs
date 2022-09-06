// Decompiled with JetBrains decompiler
// Type: StockSharp.Fix.Native.FixBase
// Assembly: StockSharp.Fix.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9148E39-A5BB-4657-14B1-EA8DED27B1C2
// Assembly location: A:\StockSharpBin\Terminal\StockSharp.Fix.Core.dll

using Ecng.Common;
using StockSharp.Localization;
using System;
using System.IO;
using System.Text;

namespace StockSharp.Fix.Native
{
    /// <summary>Data reader/writer base class.</summary>
    public abstract class FixBase
    {

        private readonly AllocationArray<byte> _byteArray = new AllocationArray<byte>(1024);
        private readonly Stream _stream;
        private readonly Encoding _encoding;
        private bool _checkSumDisabled;
        private uint _checkSum;
        private bool _isDump;
        private FixTags _lastTag;
        private bool _isValueRead;
        private int _maxBytes = int.MaxValue;
        private int _byteCount;

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Fix.Native.FixBase" />.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">Text encoding.</param>
        protected FixBase(Stream stream, Encoding encoding)
        {
            this._stream = stream ?? throw new ArgumentNullException(nameof(Stream));
            this._encoding = encoding ?? throw new ArgumentNullException(nameof(Encoding));
        }

        /// <summary>The stream.</summary>
        public Stream Stream => this._stream;

        /// <summary>Text encoding.</summary>
        public Encoding Encoding => this._encoding;

        /// <summary>
        /// <see cref="P:StockSharp.Fix.Native.FixBase.CheckSum" /> disabled.
        ///     </summary>
        public bool CheckSumDisabled
        {
            get => _checkSumDisabled;
            set => _checkSumDisabled = value;
        }

        /// <summary>Check sum.</summary>
        public uint CheckSum
        {
            get => _checkSum;
            set => _checkSum = value;
        }

        /// <summary>
        /// Gets a value indicating whether the log incoming data required.
        /// </summary>
        public bool IsDump
        {
            get => _isDump;
            set => _isDump = value;
        }

        /// <summary>Last tag.</summary>
        public FixTags LastTag
        {
            get => _lastTag;
            protected set => _lastTag = value;
        }

        /// <summary>Whether the tag value was read.</summary>
        public bool IsValueRead
        {
            get => _isValueRead;
            protected set => _isValueRead = value;
        }

        /// <summary>
        /// Gets and sets the maximum allowed bytes per read/write operation.
        /// </summary>
        public int MaxBytes
        {
            get => _maxBytes;
            set => _maxBytes = value > 0 ? value : throw new ArgumentOutOfRangeException();
        }

        /// <summary>Total read/write bytes.</summary>
        public int BytesCount
        {
            get => _byteCount;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(BytesCount), value, LocalizedStrings.Str1219);
                _byteCount = MaxBytes == int.MaxValue || value <= MaxBytes ? value : throw new InvalidOperationException(LocalizedStrings.MaxBytesExceeded.Put(value, MaxBytes));
            }
        }

        /// <summary>Add to the data log a new byte.</summary>
        /// <param name="value">New byte.</param>
        protected void Dump(byte value)
        {
            if (!CheckSumDisabled)
            {
                ++BytesCount;
                CheckSum += (uint)value;
            }
            if (!IsDump)
                return;
            this._byteArray.Add(value.TryReplaceSoh());
        }

        /// <summary>Add to the data log a news byte.</summary>
        /// <param name="value">Buffer.</param>
        /// <param name="index">Starting index.</param>
        /// <param name="count">Count of bytes should be read.</param>
        protected void Dump(byte[] value, int index, int count)
        {
            if (!CheckSumDisabled)
            {
                BytesCount += count;
                for (int index1 = 0; index1 < count; ++index1)
                    CheckSum += (uint)value[index1 + index];
            }
            if (!IsDump)
                return;
            byte[] items = new byte[count];
            for (int index1 = 0; index1 < items.Length; ++index1)
                items[index1] = value[index1 + index].TryReplaceSoh();
            this._byteArray.Add(items, 0, items.Length);
        }

        /// <summary>Clear state.</summary>
        public virtual void ClearState()
        {
            LastTag = (FixTags)(-1);
            IsValueRead = false;
            CheckSum = 0U;
            BytesCount = 0;
        }

        /// <summary>Get data log.</summary>
        /// <returns>Data log.</returns>
        public string FlushDump()
        {
            string str = Encoding.GetString(this._byteArray.Buffer, 0, this._byteArray.Count);
            this._byteArray.Reset();
            return str;
        }
    }
}
