using Ecng.Common;
using System;
using System.IO;
using System.Text;

namespace StockSharp.Fix.Native
{
    /// <summary>
    /// The interface describing the recorder of data in the FIX protocol format.
    /// </summary>
    public interface IFixWriter
    {
        /// <summary>The stream.</summary>
        Stream Stream { get; }

        /// <summary>The number of bytes write.</summary>
        int BytesCount { get; set; }

        /// <summary>
        /// <see cref="P:StockSharp.Fix.Native.IFixWriter.CheckSum" /> disabled.
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

        /// <summary>Last written tag.</summary>
        FixTags LastTag { get; }

        /// <summary>Get data log.</summary>
        /// <returns>Data log.</returns>
        string FlushDump();

        /// <summary>Last tag.</summary>
        /// <param name="tag">Tag.</param>
        void Write(FixTags tag);

        /// <summary>
        /// To record the <see cref="T:System.Boolean" /> value.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.Boolean" /> value.</param>
        void Write(bool value);

        /// <summary>
        /// To record the <see cref="T:System.Int32" /> value.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.Int32" /> value.</param>
        void Write(int value);

        /// <summary>
        /// To record the <see cref="T:System.Int64" /> value.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.Int64" /> value.</param>
        void Write(long value);

        /// <summary>
        /// To record the <see cref="T:System.decimal" /> value.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.decimal" /> value.</param>
        void Write(decimal value);

        /// <summary>
        /// To record the <see cref="T:System.DateTime" /> value.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.DateTime" /> value.</param>
        /// <param name="parser">Time parser. Required if data will be transferred as string.</param>
        void Write(DateTime value, FastDateTimeParser parser);

        /// <summary>
        /// To record the <see cref="T:System.TimeSpan" /> value.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.TimeSpan" /> value.</param>
        /// <param name="parser">Time parser. Required if data will be transferred as string.</param>
        void Write(TimeSpan value, FastTimeSpanParser parser);

        /// <summary>
        /// To record the <see cref="T:System.String" /> value.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.String" /> value.</param>
        void Write(string value);

        /// <summary>
        /// To record the <see cref="T:System.Char" /> value.
        /// </summary>
        /// <param name="value">
        /// <see cref="T:System.Char" /> value.</param>
        void Write(char value);

        /// <summary>To record an array of bytes.</summary>
        /// <param name="buffer">Bytes array.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        void WriteBytes(byte[] buffer, int offset, int count);

        /// <summary>Clear state.</summary>
        void ClearState();
    }
}
