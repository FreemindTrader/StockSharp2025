using Ecng.Common;
using System;
using System.IO;
using System.Text;

namespace StockSharp.Fix.Native
{
    /// <summary>
    /// The reader of data recorded in the binary FIX protocol format (FAST).
    /// </summary>
    public class BinaryFixReader : BaseFixReader, IFixReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Fix.Native.BinaryFixReader" />.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">Text encoding.</param>
        public BinaryFixReader(Stream stream, Encoding encoding)
          : base(stream, encoding)
        {
        }

        /// <inheritdoc />
        public FixTags ReadTag() => throw new NotSupportedException();

        /// <inheritdoc />
        public DateTime ReadDateTime(FastDateTimeParser parser) => new DateTime(ReadLong());

        /// <inheritdoc />
        public TimeSpan ReadTimeSpan(FastTimeSpanParser parser) => new TimeSpan(ReadLong());

        /// <summary>
        /// Read <see cref="T:System.UInt32" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.UInt32" /> value.</returns>
        [CLSCompliant(false)]
        public uint ReadUInt()
        {
            uint num1 = 0;
            byte thisByte;
            while (true)
            {
                thisByte = (byte)ReadByte();
                if ((thisByte & 128) == 0)
                    num1 = (num1 << 7) + thisByte;
                else
                    break;
            }
            return (num1 << 7) + (byte)(thisByte & UInt32.MaxValue);
        }

        /// <summary>
        /// Read <see cref="T:System.Nullable`1" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Nullable`1" /> value.</returns>
        [CLSCompliant(false)]
        public uint? ReadUIntNullable()
        {
            uint num = ReadUInt();
            return num == 0U ? new uint?() : new uint?(num - 1U);
        }

        /// <inheritdoc />
        public int ReadInt()
        {
            int num1 = 0;
            byte thisByte = (byte)ReadByte();
            if ((thisByte & 64) != 0)
                num1 = -1;
            int num3 = (num1 << 7) + (byte)(thisByte & UInt32.MaxValue);
            while ((thisByte & 128) == 0)
            {
                thisByte = (byte)ReadByte();
                num3 = (num3 << 7) + (byte)(thisByte & UInt32.MaxValue);
            }
            return num3;
        }

        /// <summary>
        /// Read <see cref="T:System.Nullable`1" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Nullable`1" /> value.</returns>
        public int? ReadIntNullable()
        {
            int num = ReadInt();
            if (num == 0)
                return new int?();
            return num > 0 ? new int?(num - 1) : new int?(num);
        }

        /// <summary>
        /// Read <see cref="T:System.UInt64" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.UInt64" /> value.</returns>
        [CLSCompliant(false)]
        public ulong ReadULong()
        {
            ulong num1 = 0;
            byte thisByte;
            while (true)
            {
                thisByte = (byte)ReadByte();
                if ((thisByte & 128) == 0)
                    num1 = (num1 << 7) + thisByte;
                else
                    break;
            }
            return (num1 << 7) + (byte)(thisByte & UInt32.MaxValue);
        }

        /// <summary>
        /// Read <see cref="T:System.Nullable`1" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Nullable`1" /> value.</returns>
        [CLSCompliant(false)]
        public ulong? ReadULongNullable()
        {
            ulong num = ReadULong();
            return num == 0UL ? new ulong?() : new ulong?(num - 1UL);
        }

        /// <inheritdoc />
        public long ReadLong()
        {
            long num1 = 0;
            byte thisByte = (byte)ReadByte();
            if ((thisByte & 64) != 0)
                num1 = -1L;
            long num3 = (num1 << 7) + (byte)(thisByte & UInt32.MaxValue);
            while ((thisByte & 128) == 0)
            {
                thisByte = (byte)ReadByte();
                num3 = (num3 << 7) + (byte)(thisByte & UInt32.MaxValue);
            }
            return num3;
        }

        /// <summary>
        /// Read <see cref="T:System.Nullable`1" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Nullable`1" /> value.</returns>
        public long? ReadLongNullable()
        {
            long num = ReadLong();
            return num == 0L ? new long?() : new long?(num - 1L);
        }

        /// <inheritdoc />
        public decimal ReadDecimal()
        {
            int num1 = ReadInt();
            long thisByte = ReadLong();
            if (num1 < -63 || 63 < num1)
                throw new InvalidOperationException();
            return thisByte * (decimal)Math.Pow(10.0, num1);
        }

        /// <summary>
        /// Read <see cref="T:System.Nullable`1" /> value.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Nullable`1" /> value.</returns>
        public decimal? ReadDecimalNullable()
        {
            int num1 = ReadInt();
            if (num1 == 0)
                return new decimal?();
            if (num1 > 0)
                --num1;
            long thisByte = ReadLong();
            return num1 >= -63 && 63 >= num1 ? new decimal?(thisByte * (decimal)Math.Pow(10.0, num1)) : throw new InvalidOperationException();
        }

        /// <inheritdoc />
        public char ReadChar()
        {
            int num = (int)ReadUInt();
            return (uint)num <= (uint)sbyte.MaxValue ? (char)num : throw new NotSupportedException();
        }

        /// <summary>
        /// Read <see cref="T:System.Char" /> value.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// <see cref="T:System.Char" /> value.</returns>
        public char ReadChar(char defaultValue)
        {
            uint num = ReadUInt();
            if (num > (uint)sbyte.MaxValue)
                throw new NotSupportedException();
            return num == 0U ? defaultValue : (char)num;
        }

        /// <summary>
        /// Read <see cref="T:System.String" /> value.
        /// </summary>
        /// <param name="buffer">
        /// <see cref="T:System.String" /> value.</param>
        /// <param name="length">Length of result buffer.</param>
        /// <returns>
        /// <see cref="T:System.String" /> value.</returns>
        public char[] ReadStringDelta(char[] buffer, out int length)
        {
            if (ReadInt() != 0)
                return ReadString(buffer, out length);
            length = 0;
            return buffer;
        }

        /// <summary>
        /// Read <see cref="T:System.String" /> value.
        /// </summary>
        /// <param name="buffer">
        /// <see cref="T:System.String" /> value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="length">Length of result buffer.</param>
        /// <returns>
        /// <see cref="T:System.Char" /> value.</returns>
        public unsafe char[] ReadString(char[] buffer, string defaultValue, out int length)
        {
            fixed (char* buffer1 = &buffer[0])
            {
                length = ReadString(buffer1);
                if (length == 0)
                {
                    ReadString(buffer1, defaultValue);
                    length = defaultValue.Length;
                }
            }
            return buffer;
        }

        /// <summary>
        /// Read <see cref="T:System.String" /> value.
        /// </summary>
        /// <param name="buffer">
        /// <see cref="T:System.String" /> value.</param>
        /// <param name="length">Length of result buffer.</param>
        /// <returns>
        /// <see cref="T:System.Char" /> value.</returns>
        public unsafe char[] ReadString(char[] buffer, out int length)
        {
            fixed (char* buffer1 = &buffer[0])
                length = ReadString(buffer1);
            return buffer;
        }

        /// <summary>
        /// Read <see cref="T:System.String" /> value.
        /// </summary>
        /// <param name="buffer">
        /// <see cref="T:System.String" /> value.</param>
        /// <returns>Length of result buffer.</returns>
        [CLSCompliant(false)]
        public unsafe int ReadString(char* buffer)
        {
            int num1 = ReadByte();
            int index = 0;
            if (num1 == 128)
                return index;
            while ((num1 & 128) == 0)
            {
                buffer[index] = (char)num1;
                num1 = ReadByte();
                ++index;
            }
            int thisByte = num1 & -129;
            buffer[index] = (char)thisByte;
            return index;
        }

        /// <summary>
        /// Read <see cref="T:System.String" /> value as a constant value.
        /// </summary>
        /// <param name="buffer">
        /// <see cref="T:System.String" /> value.</param>
        /// <param name="constValue">The constant value.</param>
        [CLSCompliant(false)]
        public unsafe void ReadString(char* buffer, string constValue)
        {
            int index;
            for (index = 0; index < constValue.Length; ++index)
                buffer[index] = constValue[index];
            buffer[index] = char.MinValue;
        }

        /// <inheritdoc />
        public string ReadString() => throw new NotSupportedException();

        /// <inheritdoc />
        public bool ReadBool() => throw new NotSupportedException();

        /// <inheritdoc />
        public void SkipValue() => throw new NotImplementedException();

        /// <summary>Read bytes array.</summary>
        /// <param name="bytes">Bytes array.</param>
        /// <param name="length">The number of bytes read.</param>
        /// <returns>Result buffer.</returns>
        [CLSCompliant(false)]
        public unsafe byte[] ReadByteVectorNullable(byte[] bytes, out uint length)
        {
            fixed (byte* bytes1 = &bytes[0])
                length = ReadByteVectorNullable(bytes1);
            return bytes;
        }

        /// <summary>Read bytes array.</summary>
        /// <param name="bytes">Bytes array.</param>
        /// <returns>The number of bytes read.</returns>
        [CLSCompliant(false)]
        public unsafe uint ReadByteVectorNullable(byte* bytes)
        {
            uint num1 = ReadUInt();
            if (num1 == 0U)
                return 0;
            uint thisByte = num1 - 1U;
            for (int index = 0; index < thisByte; ++index)
                bytes[index] = (byte)ReadByte();
            return thisByte;
        }

        /// <summary>Read bytes array.</summary>
        /// <param name="bytes">Bytes array.</param>
        /// <param name="length">The number of bytes read.</param>
        /// <returns>Result buffer.</returns>
        [CLSCompliant(false)]
        public unsafe byte[] ReadByteVector(byte[] bytes, out uint length)
        {
            fixed (byte* bytes1 = &bytes[0])
                length = ReadByteVector(bytes1);
            return bytes;
        }

        /// <summary>Read bytes array.</summary>
        /// <param name="bytes">Bytes array.</param>
        /// <returns>The number of bytes read.</returns>
        [CLSCompliant(false)]
        public unsafe uint ReadByteVector(byte* bytes)
        {
            uint num = ReadUInt();
            for (int index = 0; index < num; ++index)
                bytes[index] = (byte)ReadByte();
            return num;
        }
    }
}
