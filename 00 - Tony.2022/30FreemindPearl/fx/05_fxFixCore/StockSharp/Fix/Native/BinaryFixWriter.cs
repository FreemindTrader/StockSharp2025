// Decompiled with JetBrains decompiler
// Type: StockSharp.Fix.Native.BinaryFixWriter
// Assembly: StockSharp.Fix.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9148E39-A5BB-4657-14B1-EA8DED27B1C2
// Assembly location: A:\StockSharpBin\Terminal\StockSharp.Fix.Core.dll

using Ecng.Common;
using StockSharp.Localization;
using System;
using System.Collections;
using System.IO;
using System.Text;

namespace StockSharp.Fix.Native
{
    /// <summary>
    /// The data recorder which records in the binary FIX protocol format (FAST).
    /// </summary>
    public class BinaryFixWriter : BaseFixWriter, IFixWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Fix.Native.BinaryFixWriter" />.
        /// </summary>
        /// <param name="stream">Writing stream.</param>
        /// <param name="encoding">Text encoding.</param>
        public BinaryFixWriter(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }

        /// <inheritdoc />
        public void Write(FixTags tag)
        {
        }

        /// <inheritdoc />
        public void Write(bool value) => throw new NotSupportedException();

        /// <inheritdoc />
        public void Write(int value)
        {
            WriteInt(value);
            WriteSoh();
        }

        /// <inheritdoc />
        public void Write(long value)
        {
            WriteLong(value);
            WriteSoh();
        }

        /// <inheritdoc />
        public void Write(decimal value)
        {
            int num1;
            long num2;

            WriteDecimal(value, out num1, out num2);
            if (num1 < -63 || 63 < num1)
                throw new InvalidOperationException();
            Write(num1);
            Write(num2);
        }

        /// <inheritdoc />
        public void Write(DateTime value, FastDateTimeParser parser) => Write(value.Ticks);

        /// <inheritdoc />
        public void Write(TimeSpan value, FastTimeSpanParser parser) => Write(value.Ticks);

        /// <inheritdoc />
        public void Write(string myString)
        {
            /*  ------------------------------------------------------------------------------------------------------------------------------------------
             *  
             *    Tony 01: The high order bit of each byte of the message is reserved to indicate the end of the field.
             *    So each of the fields described below will only use 7 bit bytes and the 8th bit will be set on the last byte of the field.
             *    
             *  ------------------------------------------------------------------------------------------------------------------------------------------
            */
            if (myString.IsEmpty() || myString == "|")   //<SOH>
            {
                WriteByte(128);
            }
            else
            {
                for (int index = 0; index < myString.Length; ++index)
                {
                    int myChar = myString[index];

                    if (myChar > sbyte.MaxValue)
                        throw new InvalidOperationException();

                    if (index == myString.Length - 1)
                        myChar |= 128;

                    WriteByte((byte)myChar);
                }
                WriteSoh();
            }
        }

        /// <inheritdoc />
        public void Write(char value)
        {
            if (value > '\x007F')
                throw new ArgumentOutOfRangeException("Invalid Character", value, LocalizedStrings.Str1219);
            Write((int)value);
        }

        /// <summary>Write Presence Map.</summary>
        /// <param name="map">Presence Map.</param>
        public void Write(BitArray map)
        {
            if (map == null)
                throw new ArgumentNullException("map is null");
            if (map.Length == 0)
                throw new ArgumentOutOfRangeException("map length is zero");
            int num1 = 0;
            for (int index = map.Length - 1; index >= 0 && !map[index]; --index)
                ++num1;
            map.Length -= num1;
            byte num2 = 0;
            byte num3 = 1;
            bool flag = false;
            for (int index = 0; index < map.Length; ++index)
            {
                if (map[index])
                    num2 |= num3;
                num3 <<= 1;
                if (num3 == 128)
                {
                    if (index == map.Length - 1)
                    {
                        num2 |= 128;
                        WriteByte(num2);
                        flag = true;
                    }
                    else
                    {
                        WriteByte(num2);
                        num3 = 1;
                        num2 = 0;
                    }
                }
            }
            if (flag)
                return;
            WriteByte((byte)(num2 | 128U));
        }

        /// <summary>To record an array of bytes.</summary>
        /// <param name="bytes">Bytes array.</param>
        public void WriteVector(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException("byte array is null");
            if (bytes.Length == 0)
                throw new ArgumentOutOfRangeException("byte array is empty");
            Write(bytes.Length);
            WriteBytes(bytes, 0, bytes.Length);
            WriteSoh();
        }

        private void WriteInt(int _param1)
        {
            uint num1 = Int32.MaxValue;
            uint num2 = (uint)_param1;
            bool flag = (num2 & num1) > 0U;
            int num3 = 0;
            for (uint index = num1 >> 1; index != 0U && (num2 & index) > 0U == flag; index >>= 1)
                ++num3;
            int num4 = 32 - num3;
            uint num5 = (uint)(1 << num4 - 1);
            int num6 = num4 % 7;
            int num7 = num4 / 7;
            if (0 < num6)
            {
                byte num8 = 0;
                if (flag)
                {
                    for (int index = 6; num6 <= index; --index)
                        num8 |= (byte)(1 << index);
                }
                uint num9 = (uint)(1 << num6 - 1);
                while (num9 != 0U)
                {
                    if (((int)num2 & (int)num5) != 0)
                        num8 |= (byte)num9;
                    num9 >>= 1;
                    num5 >>= 1;
                }
                if (num7 == 0)
                    num8 |= 128;
                WriteByte(num8);
            }
            for (int index = 0; index < num7; ++index)
            {
                uint num8 = 64;
                byte num9 = 0;
                while (num8 != 0U)
                {
                    if (((int)num2 & (int)num5) != 0)
                        num9 |= (byte)num8;
                    num8 >>= 1;
                    num5 >>= 1;
                }
                if (index == num7 - 1)
                    num9 |= 128;
                WriteByte(num9);
            }
        }

        private void WriteLong(long _param1)
        {
            ulong num1 = 9223372036854775808;
            ulong num2 = (ulong)_param1;
            bool flag = (num2 & num1) > 0UL;
            int num3 = 0;
            for (ulong index = num1 >> 1; index != 0UL && (num2 & index) > 0UL == flag; index >>= 1)
                ++num3;
            int num4 = 64 - num3;
            ulong num5 = 1UL << num4 - 1;
            int num6 = num4 % 7;
            int num7 = num4 / 7;
            if (0 < num6)
            {
                byte num8 = 0;
                if (flag)
                {
                    for (int index = 6; num6 <= index; --index)
                        num8 |= (byte)(1 << index);
                }
                uint num9 = (uint)(1 << num6 - 1);
                while (num9 != 0U)
                {
                    if (((long)num2 & (long)num5) != 0L)
                        num8 |= (byte)num9;
                    num9 >>= 1;
                    num5 >>= 1;
                }
                if (num7 == 0)
                    num8 |= 128;
                WriteByte(num8);
            }
            for (int index = 0; index < num7; ++index)
            {
                uint num8 = 64;
                byte num9 = 0;
                while (num8 != 0U)
                {
                    if (((long)num2 & (long)num5) != 0L)
                        num9 |= (byte)num8;
                    num8 >>= 1;
                    num5 >>= 1;
                }
                if (index == num7 - 1)
                    num9 |= 128;
                WriteByte(num9);
            }
        }

        private static void WriteDecimal(decimal decimalValue, out int _param1, out long _param2)
        {
            if (decimalValue == 0M)
            {
                _param1 = 0;
                _param2 = 0L;
            }
            else
            {
                bool flag = false;
                if (decimalValue < 0M)
                {
                    decimalValue = -decimalValue;
                    flag = true;
                }
                int num1 = (int)Math.Floor(Math.Log10((double)decimalValue));
                decimal num2 = decimalValue / (decimal)Math.Pow(10.0, num1);
                for (int index = 0; index < 17 && !(decimal.Remainder(num2, 1M) == 0M); ++index)
                {
                    --num1;
                    num2 *= 10M;
                }
                decimal num3 = decimal.Round(num2);
                if (flag)
                    num3 = -num3;
                _param1 = num1;
                _param2 = (long)num3;
            }
        }
    }
}
