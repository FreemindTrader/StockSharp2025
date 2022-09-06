using Ecng.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StockSharp.Fix.Native
{
    /// <summary>
    /// The reader of data recorded in the text FIX protocol format.
    /// </summary>
    public class TextFixReader : BaseFixReader, IFixReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Fix.Native.TextFixReader" />.
        /// </summary>
        /// <param name="stream">The stream from which data will be read.</param>
        /// <param name="encoding">Text encoding.</param>
        public TextFixReader(Stream stream, Encoding encoding)
          : base(stream, encoding)
        {
        }

        private static bool IsOne(int _param0) => _param0 == 1 || _param0 == -1;

        /// <inheritdoc />
        public FixTags ReadTag()
        {
            IsValueRead = false;
            int byteVal = ReadByte();

            if (byteVal == -1)
            {
                LastTag = (FixTags)(-1);
                return (FixTags)(-1);
            }

            int? myNumber = this.GetIntNumeric(ref byteVal);

            if (!myNumber.HasValue)
            {
                throw new InvalidOperationException(nameof(myNumber));
            }

            if (byteVal != 61)
            {
                throw new InvalidOperationException();
            }

            LastTag = (FixTags)myNumber.Value;
            return LastTag;
        }

        /// <inheritdoc />
        public DateTime ReadDateTime(FastDateTimeParser parser) => parser.Parse(ReadString());

        /// <inheritdoc />
        public TimeSpan ReadTimeSpan(FastTimeSpanParser parser)
        {
            string str = ReadString();
            return str.CompareIgnoreCase(nameof(FastTimeSpanParser)) ? TimeSpan.Zero : parser.Parse(str);
        }

        /// <inheritdoc />
        public int ReadInt()
        {
            int byteVal = ReadByte();
            int? nullable = this.GetSignedIntNumeric(ref byteVal);
            if (!nullable.HasValue || !IsOne(byteVal))
                throw new InvalidOperationException();
            IsValueRead = true;
            return nullable.Value;
        }

        /// <inheritdoc />
        public long ReadLong()
        {
            int byteVal = ReadByte();
            long? nullable = this.GetSignedLongNumeric(ref byteVal);
            if (!nullable.HasValue || !IsOne(byteVal))
                throw new InvalidOperationException();
            IsValueRead = true;
            return nullable.Value;
        }

        /// <inheritdoc />
        public decimal ReadDecimal()
        {
            int byteVal = ReadByte();
            decimal? nullable = this.GetSignedDecimalNumeric(ref byteVal);
            if (!nullable.HasValue || !IsOne(byteVal))
                throw new InvalidOperationException();
            IsValueRead = true;
            return nullable.Value;
        }

        /// <inheritdoc />
        public char ReadChar()
        {
            int byteVal = ReadByte();
            if (IsOne(byteVal))
                throw new InvalidOperationException();
            if (!IsOne(ReadByte()))
                throw new InvalidOperationException();
            IsValueRead = true;
            return (char)(byte)byteVal;
        }

        /// <inheritdoc />
        public string ReadString()
        {
            try
            {
                List<byte> source = new List<byte>();
                while (true)
                {
                    int byteVal = ReadByte();
                    if (!IsOne(byteVal))
                        source.Add((byte)byteVal);
                    else
                        break;
                }
                return !source.Any<byte>() ? (string)null : Encoding.GetString(source.ToArray());
            }
            finally
            {
                IsValueRead = true;
            }
        }

        /// <inheritdoc />
        public bool ReadBool()
        {
            switch (ReadChar())
            {
                case 'N':
                    return false;
                case 'Y':
                    return true;
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <inheritdoc />
        public void SkipValue()
        {
            do
            {

            }
            while (ReadByte() != 1);

            IsValueRead = true;
        }

        private bool? HasSign(ref int _param1)
        {
            bool? nullable = new bool?();
            if (_param1 == 45 || _param1 == 43)
            {
                nullable = new bool?(_param1 == 45);
                _param1 = ReadByte();
            }
            return nullable;
        }

        private int? GetSignedIntNumeric(ref int _param1)
        {
            if (_param1 == -1)
                return new int?();
            bool? hasSign = HasSign(ref _param1);
            int? numericValue = this.GetIntNumeric(ref _param1);
            if (hasSign.HasValue)
            {
                if (!numericValue.HasValue)
                    throw new InvalidOperationException();
                if (hasSign.Value)
                    numericValue = new int?(-numericValue.Value);
            }
            return numericValue;
        }

        private long? GetSignedLongNumeric(ref int _param1)
        {
            if (_param1 == -1)
                return new long?();
            bool? hasSign = this.HasSign(ref _param1);
            long? numericValue = this.GetLongNumeric(ref _param1);
            if (hasSign.HasValue)
            {
                if (!numericValue.HasValue)
                    throw new InvalidOperationException();
                if (hasSign.Value)
                    numericValue = new long?(-numericValue.Value);
            }
            return numericValue;
        }

        private decimal? GetSignedDecimalNumeric(ref int _param1)
        {
            if (_param1 == -1)
                return new decimal?();
            bool? hasSign = this.HasSign(ref _param1);
            int num1;
            decimal? numericValue = this.GetDecimalNumeric(ref _param1, out num1);
            if (_param1 == 46)
            {
                _param1 = this.ReadByte();
                decimal? nullable3 = this.GetDecimalNumeric(ref _param1, out num1);
                if (!nullable3.HasValue)
                    throw new InvalidOperationException("no numeric");
                if (!numericValue.HasValue)
                    numericValue = new decimal?(0M);
                decimal? nullable4 = numericValue;
                decimal num2 = nullable3.Value / 10M.Pow((decimal)num1);
                numericValue = nullable4.HasValue ? new decimal?(nullable4.GetValueOrDefault() + num2) : new decimal?();
            }
            if (hasSign.HasValue)
            {
                if (!numericValue.HasValue)
                    throw new InvalidOperationException();
                if (hasSign.Value)
                    numericValue = new decimal?(-numericValue.Value);
            }
            return numericValue;
        }

        private int? GetIntNumeric(ref int _param1)
        {
            int num1 = _param1 - 48;
            if (num1 < 0 || 9 < num1)
                return new int?();
            int num2;
            while (true)
            {
                num2 = this.ReadByte();
                int num3 = num2 - 48;
                if (num3 >= 0 && 9 >= num3)
                    num1 = checked(num1 * 10 + num3);
                else
                    break;
            }
            _param1 = num2;
            return new int?(num1);
        }

        private long? GetLongNumeric(ref int _param1)
        {
            long num1 = (long)(_param1 - 48);
            if (num1 < 0L || 9L < num1)
                return new long?();
            int num2;
            while (true)
            {
                num2 = this.ReadByte();
                int num3 = num2 - 48;
                if (num3 >= 0 && 9 >= num3)
                    num1 = checked(num1 * 10L + (long)num3);
                else
                    break;
            }
            _param1 = num2;
            return new long?(num1);
        }

        private decimal? GetDecimalNumeric(ref int _param1, out int _param2)
        {
            _param2 = 0;
            decimal num1 = (decimal)(_param1 - 48);
            if (num1 < 0M || 9M < num1)
                return new decimal?();
            ++_param2;
            int num2;
            while (true)
            {
                num2 = this.ReadByte();
                int num3 = num2 - 48;
                if (num3 >= 0 && 9 >= num3)
                {
                    num1 = num1 * 10M + (decimal)num3;
                    checked { ++_param2; }
                }
                else
                    break;
            }
            _param1 = num2;
            return new decimal?(num1);
        }
    }
}
