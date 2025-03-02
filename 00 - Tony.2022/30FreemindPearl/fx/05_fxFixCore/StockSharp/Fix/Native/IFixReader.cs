using Ecng.Common;
using System;
using System.IO;
using System.Text;

namespace StockSharp.Fix.Native
{
    /// <summary>
    /// The interface describing the reader of data recorded in the FIX protocol format.
    /// </summary>
    public interface IFixReader
    {
        /// <summary>The stream.</summary>
        Stream Stream { get; }

        /// <summary>The number of bytes read.</summary>
        int BytesCount { get; set; }

        /// <summary>
        /// <see cref="P:StockSharp.Fix.Native.IFixReader.CheckSum" /> disabled.
        ///     </summary>
        bool CheckSumDisabled { get; set; }

        /// <summary>Check sum.</summary>
        uint CheckSum { get; set; }

        /// <summary>
        /// Gets a value indicating whether the log incoming data required.
        /// </summary>
        bool IsDump { get; set; }

        /// <summary>Text encoding.</summary>
        Encoding Encoding { get; }

        /// <summary>Whether the tag value was read.</summary>
        bool IsValueRead { get; }

        /// <summary>Last read tag.</summary>
        FixTags LastTag { get; }

        /// <summary>To read the following tag.</summary>
        /// <returns>The next tag. The -1 indicates the end of data.</returns>
        FixTags ReadTag();

        /// <summary>Get data log.</summary>
        /// <returns>Data log.</returns>
        string FlushDump();

        /// <summary>
        /// Read <see cref="T:System.DateTime" /> value.
        /// </summary>
        /// <param name="parser">Time parser. Required if data will be transferred as string.</param>
        /// <returns>
        /// <see cref="T:System.DateTime" /> value.</returns>
        DateTime ReadDateTime(FastDateTimeParser parser);

        /// <summary>
        /// Read <see cref="T:System.TimeSpan" /> value.
        /// </summary>
        /// <param name="parser">Time parser. Required if data will be transferred as string.</param>
        /// <returns>
        /// <see cref="T:System.TimeSpan" /> value.</returns>
        TimeSpan ReadTimeSpan(FastTimeSpanParser parser);

        /// <summary>
        /// Read <see cref="T:System.Int32" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Int32" /> value.</returns>
        int ReadInt();

        /// <summary>
        /// Read <see cref="T:System.Int64" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Int64" /> value.</returns>
        long ReadLong();

        /// <summary>
        /// Read <see cref="T:System.decimal" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.decimal" /> value.</returns>
        decimal ReadDecimal();

        /// <summary>
        /// Read <see cref="T:System.Char" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Char" /> value.</returns>
        char ReadChar();

        /// <summary>
        /// Read <see cref="T:System.String" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.String" /> value.</returns>
        string ReadString();

        /// <summary>
        /// Read <see cref="T:System.Byte" /> array value.
        /// </summary>
        /// <param name="buffer">Buffer.</param>
        /// <param name="index">Starting index.</param>
        /// <param name="count">Count of bytes should be read.</param>
        void ReadBytes(byte[] buffer, int index, int count);

        /// <summary>
        /// Read <see cref="T:System.Boolean" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Boolean" /> value.</returns>
        bool ReadBool();

        /// <summary>Skip value.</summary>
        void SkipValue();

        /// <summary>Clear state.</summary>
        void ClearState();
    }
}
