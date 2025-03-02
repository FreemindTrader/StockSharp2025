using Ecng.Common;
using System.IO;
using System.Text;

namespace StockSharp.Fix.Native
{
    /// <summary>Data reader base class.</summary>
    public abstract class BaseFixReader : FixBase
    {

        private readonly byte[] _oneByte = new byte[1];

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Fix.Native.BaseFixReader" />.
        /// </summary>
        /// <param name="stream">The stream from which data will be read.</param>
        /// <param name="encoding">Text encoding.</param>
        protected BaseFixReader(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }

        /// <summary>Get byte.</summary>
        /// <returns>Byte.</returns>
        public int ReadByte()
        {
            byte num = this.Stream.ReadByteEx(this._oneByte);
            this.Dump(num);
            return (int)num;
        }

        /// <summary>
        /// Read <see cref="T:System.Byte" /> array value.
        /// </summary>
        /// <param name="buffer">Buffer.</param>
        /// <param name="index">Starting index.</param>
        /// <param name="count">Count of bytes should be read.</param>
        public void ReadBytes(byte[] buffer, int index, int count)
        {
            this.Stream.ReadBytes(buffer, count, index);
            this.Dump(buffer, index, count);
        }
    }
}
