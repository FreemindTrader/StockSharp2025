using Ecng.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace StockSharp.Fix.Native
{
    /// <summary>The base class of the recorder.</summary>
    public abstract class BaseFixWriter : FixBase
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly byte[] _oneByte = new byte[1];

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Fix.Native.BaseFixWriter" />.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">Text encoding.</param>
        protected BaseFixWriter(Stream stream, Encoding encoding)
          : base(stream, encoding)
        {
        }

        /// <summary>To record the SOH symbol.</summary>
        protected void WriteSoh() => WriteByte((byte)1);

        /// <summary>
        /// To record the <see cref="T:System.Byte" /> value.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.Byte" /> value.</param>
        protected void WriteByte(byte value)
        {
            Stream.WriteByteEx(_oneByte, value);
            Dump(value);
        }

        /// <summary>To record an array of bytes.</summary>
        /// <param name="buffer">Bytes array.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public void WriteBytes(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException("Buffer is Null");
            Stream.Write(buffer, offset, count);
            Dump(buffer, offset, count);
        }
    }
}
