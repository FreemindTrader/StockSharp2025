// Decompiled with JetBrains decompiler
// Type: StockSharp.Fix.Native.TextFixWriter
// Assembly: StockSharp.Fix.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9148E39-A5BB-4657-14B1-EA8DED27B1C2
// Assembly location: A:\StockSharpBin\Terminal\StockSharp.Fix.Core.dll

using Ecng.Common;
using System;
using System.IO;
using System.Text;

namespace StockSharp.Fix.Native
{
    /// <summary>
    /// The data recorder which records in the text FIX protocol format.
    /// </summary>
    public class TextFixWriter : BaseFixWriter, IFixWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Fix.Native.TextFixWriter" />.
        /// </summary>
        /// <param name="stream">Writing stream.</param>
        /// <param name="encoding">Text encoding.</param>
        public TextFixWriter(Stream stream, Encoding encoding)
          : base(stream, encoding)
        {
        }

        private void WriteLong(long _param1)
        {
            if (_param1 < 0L)
            {
                _param1 = -_param1;
                this.WriteByte((byte)45);
            }
            if (_param1 < 10L)
                this.WriteByte((byte)((uint)(int)_param1 + 48U));
            else if (_param1 == 10L)
            {
                this.WriteByte((byte)49);
                this.WriteByte((byte)48);
            }
            else
            {
                long num1 = 1;
                long num2 = _param1;
                while (num2 >= 10L)
                {
                    num2 /= 10L;
                    num1 *= 10L;
                }
                for (; num1 != 0L; num1 /= 10L)
                {
                    int num3 = (int)(_param1 / num1);
                    if (num3 < 0 || num3 > 9)
                        throw new InvalidOperationException();
                    this.WriteByte((byte)(num3 + 48));
                    _param1 -= (long)num3 * num1;
                }
            }
        }

        /// <inheritdoc />
        public void Write(FixTags tag)
        {
            this.WriteLong((long)tag);
            this.WriteByte((byte)61);
            this.LastTag = tag;
        }

        /// <inheritdoc />
        public void Write(bool value)
        {
            this.WriteByte(value ? (byte)89 : (byte)78);
            this.WriteSoh();
        }

        /// <inheritdoc />
        public void Write(int value)
        {
            this.WriteLong((long)value);
            this.WriteSoh();
        }

        /// <inheritdoc />
        public void Write(long value)
        {
            this.WriteLong(value);
            this.WriteSoh();
        }

        /// <inheritdoc />
        public void Write(decimal value)
        {
            if (value == 0M)
            {
                this.WriteByte((byte)48);
                this.WriteSoh();
            }
            else
            {
                if (value < 0M)
                {
                    value = -value;
                    this.WriteByte((byte)45);
                }
                int num1 = (int)Math.Floor(Math.Log10((double)value));
                decimal num2 = (decimal)Math.Pow(10.0, (double)num1);
                bool flag = false;
                if (num1 < 0)
                {
                    this.WriteByte((byte)48);
                    this.WriteByte((byte)46);
                    flag = true;
                    int num3 = num1 + 1;
                    while (num3++ < 0)
                        this.WriteByte((byte)48);
                }
                while (value != 0M || 1M <= num2)
                {
                    int num3 = (int)(value / num2);
                    if (num3 < 0 || 9 < num3)
                        throw new InvalidOperationException();
                    if (!flag && num2 < 1M)
                    {
                        this.WriteByte((byte)46);
                        flag = true;
                    }
                    value -= (decimal)num3 * num2;
                    this.WriteByte((byte)(num3 + 48));
                    num2 /= 10M;
                }
                this.WriteSoh();
            }
        }

        /// <inheritdoc />
        public void Write(DateTime value, FastDateTimeParser parser) => this.Write(parser.ToString(value));

        /// <inheritdoc />
        public void Write(TimeSpan value, FastTimeSpanParser parser) => this.Write(parser.ToString(value));

        /// <inheritdoc />
        public void Write(string value)
        {
            byte[] bytes = this.Encoding.GetBytes(value);
            this.WriteBytes(bytes, 0, bytes.Length);
            this.WriteSoh();
        }

        /// <inheritdoc />
        public void Write(char value)
        {
            this.WriteByte((byte)value);
            this.WriteSoh();
        }
    }
}
