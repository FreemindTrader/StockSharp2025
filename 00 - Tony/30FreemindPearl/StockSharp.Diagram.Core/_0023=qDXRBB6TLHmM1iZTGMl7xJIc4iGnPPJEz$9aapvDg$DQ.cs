// Decompiled with JetBrains decompiler
// Type: #=qDXRBB6TLHmM1iZTGMl7xJIc4iGnPPJEz$9aapvDg$DQ=
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

internal static class SomeHolder
{
  private static ConcurrentDictionary<int, string> \u0023\u003DzmGmcXflrTCmUF5eABdHsWeZB\u0024Erj;
  private static int \u0023\u003DzLNaTOymPPDjWg613TfXsjoCdJu0I;
  private static SomeHolder.\u0023\u003Dz\u0024f53\u0024vGCciUdlDPVMN3w6sDlI0S\u0024 \u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0;
  private static int \u0023\u003DzuD1BM5U1cQNd084PAJ3OciKaBhey;
  private static int \u0023\u003DzHJTTZusbrbwvHIdtC4mDBYulAWWH;
  private static byte[] \u0023\u003DzSH4i4aHHdEHFOUW3qAqIHIQiaZXC;
  private static byte[] \u0023\u003Dz8WqnBEZ\u0024aZZfrB24QoA5hZBfseb3;
  private static SomeHolder.\u0023\u003DzPYz8VIgpZ4GFzotf9qZ3n_eps_Pj \u0023\u003DzxsVis2YxH3XhXnZru\u00241EkG2bH6Ld;
  private static short \u0023\u003Dzb_cGIc1Gaz9NCBr7Vid8IS0uexlk;

  [MethodImpl(MethodImplOptions.NoInlining)]
  static SomeHolder()
  {
    int num1 = 1666005864;
    int num2 = num1 - 848647005;
    SomeHolder.\u0023\u003DzmGmcXflrTCmUF5eABdHsWeZB\u0024Erj = new ConcurrentDictionary<int, string>();
    SomeHolder.\u0023\u003DzuD1BM5U1cQNd084PAJ3OciKaBhey += -~-~~-~--~~(1605315195 - num1 + num2);
    SomeHolder.\u0023\u003DzxsVis2YxH3XhXnZru\u00241EkG2bH6Ld = (SomeHolder.\u0023\u003DzPYz8VIgpZ4GFzotf9qZ3n_eps_Pj) 16 | SomeHolder.\u0023\u003DzxsVis2YxH3XhXnZru\u00241EkG2bH6Ld;
  }

  [MethodImpl(MethodImplOptions.NoInlining)]
  internal static string \u0023\u003DzKCEbSA0\u003D(int _param0)
  {
    string str;
    if (SomeHolder.\u0023\u003DzmGmcXflrTCmUF5eABdHsWeZB\u0024Erj.TryGetValue(_param0, out str))
      return str;
    return SomeHolder.\u0023\u003Dz10qw95b74UrE0kZWYRw_JQj9dFek(_param0, true);
  }

  [MethodImpl(MethodImplOptions.NoInlining)]
  private static string \u0023\u003Dz10qw95b74UrE0kZWYRw_JQj9dFek(int _param0, bool _param1)
  {
    int num1 = 1545461300;
    int num2 = num1 - 1768214361;
    string str1 = (string) null;
    byte[] numArray1;
    int num3;
    int num4;
    int wg613TfXsjoCdJu0I;
    int num5;
    int num6;
    byte[] numArray2;
    byte[] ehfouW3qAqIhiQiaZxc;
    do
    {
      lock (SomeHolder.\u0023\u003DzmGmcXflrTCmUF5eABdHsWeZB\u0024Erj)
      {
        int num7;
        if (SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0 == null)
        {
          Assembly executingAssembly = Assembly.GetExecutingAssembly();
          SomeHolder.\u0023\u003DzLNaTOymPPDjWg613TfXsjoCdJu0I |= 1769824731 - num1 + num2;
          Assembly assembly = executingAssembly;
          StringBuilder stringBuilder = new StringBuilder();
          int num8 = num1 - 1231269717 - num2;
          stringBuilder.Append((char) (num8 >> 16)).Append((char) num8);
          int num9 = (1901448977 ^ num1) + num2;
          stringBuilder.Append((char) num9).Append((char) (num9 >> 16));
          int num10 = 785911059 - num1 ^ num2;
          stringBuilder.Append((char) num10).Append((char) (num10 >> 16));
          int num11 = num1 - 785108234 + num2;
          stringBuilder.Append((char) num11).Append((char) (num11 >> 16));
          int num12 = 1860046101 - num1 - num2;
          stringBuilder.Append((char) num12).Append((char) (num12 >> 16));
          int num13 = num1 - 1768206178 ^ num2;
          stringBuilder.Append((char) num13);
          string name = stringBuilder.ToString();
          SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0 = new SomeHolder.\u0023\u003Dz\u0024f53\u0024vGCciUdlDPVMN3w6sDlI0S\u0024(assembly.GetManifestResourceStream(name));
          short num14 = (short) ((int) SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0.\u0023\u003DzZow2cpyJf0iA9PWOxc7zJzmWW\u00243V() ^ (int) (short) ~-~--~~--~~((num1 ^ -1364942176) - num2));
          if (num14 == (short) 0)
            SomeHolder.\u0023\u003Dzb_cGIc1Gaz9NCBr7Vid8IS0uexlk = (short) ((int) SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0.\u0023\u003DzZow2cpyJf0iA9PWOxc7zJzmWW\u00243V() ^ (int) (short) ~--~~-~-~(num1 - 1322733426 ^ num2));
          else
            SomeHolder.\u0023\u003Dz8WqnBEZ\u0024aZZfrB24QoA5hZBfseb3 = SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0.\u0023\u003Dz7otzSwnsw4RDi2ZlITp992L\u0024fiUz((int) num14);
          SomeHolder.\u0023\u003DzSH4i4aHHdEHFOUW3qAqIHIQiaZXC = SomeHolder.\u0023\u003Dzvbn_2vDiqBiXMT9LNeetMV\u0024FMzeX(SomeHolder.\u0023\u003DzFNIITv3O8\u00246FvducSlyAgUcU6CLp(executingAssembly));
          int num15 = SomeHolder.\u0023\u003DzuD1BM5U1cQNd084PAJ3OciKaBhey ^ -531326074 - num1 + num2;
          SomeHolder.\u0023\u003DzuD1BM5U1cQNd084PAJ3OciKaBhey = 0;
          int num16 = num15 ^ num1 + 98978191 - num2 ^ (num1 ^ 901481954) + num2;
          int num17 = num16;
          int num18 = 0;
          int num19 = num17;
          int num20 = num19;
          \u0023\u003Dqk0gPbWPyrh_riPLJQUJLiVrI1HNVKgJ95RlwZDL0T8A\u003D<int> enumerator = ((\u0023\u003Dq6P34Z0doe4ChSWpukic1SYpy47Zoix20fK8OYvG3cXM\u003D<int>) new \u0023\u003DqyqwLjBme8RIKm_lMznUuBv_0OjrFhxezez7oJ_bzM\u0024s\u003D.\u0023\u003Dzo_kWXN0\u003D(num1 - 1322708239 ^ num2) { _diagramElement = num20 }).GetEnumerator();
          try
          {
            while (enumerator.\u0023\u003Dzwy2rN9Xi70ZxqBLH3IrSQoU5Lf8zImrce3wacQpXc3Zz82C7jc447cMC2X9DWjy\u0024bXkdQy6KyVHQMY0oKg\u003D\u003D())
            {
              int num21 = enumerator.\u0023\u003DzYiJDU9Ld3NCYHVrB\u0024MOPvuGTCYxFWPYc8ShzeEi1FBur9uRGr74phsFm4AkjoQt1Hw\u003D\u003D();
              num19 ^= num21 + num18;
              num18 += num19 >> 3;
            }
          }
          finally
          {
            enumerator?.\u0023\u003DzOvLPpgU4r7e7fhOXQgNkkHMd9syYifKNHybbF7cO5KBNdJn5\u00246lVBy3IqcKcJN91Tgb1\u0024h8\u003D();
          }
          int num22 = num19;
          int num23 = -202501310 - num1 + num2 + num22 ^ (482655645 ^ num1 ^ num2);
          int num24 = num16 ^ -~-~-~~-~(-1856977121 - num1 - num2);
          num7 = (num1 - 1322702946 + num2) * num23 % (num1 ^ -1358717694 ^ num2) + num24;
          SomeHolder.\u0023\u003DzLNaTOymPPDjWg613TfXsjoCdJu0I = SomeHolder.\u0023\u003DzLNaTOymPPDjWg613TfXsjoCdJu0I & num1 - 1054272925 + num2 ^ (-1364920469 ^ num1) - num2;
          SomeHolder.\u0023\u003DzHJTTZusbrbwvHIdtC4mDBYulAWWH = num7;
          if ((SomeHolder.\u0023\u003DzxsVis2YxH3XhXnZru\u00241EkG2bH6Ld & (SomeHolder.\u0023\u003DzPYz8VIgpZ4GFzotf9qZ3n_eps_Pj) ~--~~-~-~(num1 - 1322708222 ^ num2)) == (SomeHolder.\u0023\u003DzPYz8VIgpZ4GFzotf9qZ3n_eps_Pj) 0)
            SomeHolder.\u0023\u003DzLNaTOymPPDjWg613TfXsjoCdJu0I = 1322752201 - num1 - num2;
        }
        else
          num7 = SomeHolder.\u0023\u003DzHJTTZusbrbwvHIdtC4mDBYulAWWH;
        if (SomeHolder.\u0023\u003DzLNaTOymPPDjWg613TfXsjoCdJu0I == (1364869867 ^ num1) + num2)
        {
          str1 = (string) new string(new char[3]
          {
            (char) (num1 - 1768214273 - num2),
            '0',
            (char) (1768214449 - num1 + num2)
          });
          return str1;
        }
        int num25 = _param0 ^ (num1 - 1464031970 ^ num2) ^ num7 ^ -1219358956 - num1 - num2;
        SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0.\u0023\u003DzWa3CJ7DWTs3WAg3yxjUuoDKKjhxA().Position = (long) num25;
        if (SomeHolder.\u0023\u003Dz8WqnBEZ\u0024aZZfrB24QoA5hZBfseb3 != null)
        {
          numArray1 = SomeHolder.\u0023\u003Dz8WqnBEZ\u0024aZZfrB24QoA5hZBfseb3;
        }
        else
        {
          short num8 = SomeHolder.\u0023\u003Dzb_cGIc1Gaz9NCBr7Vid8IS0uexlk != (short) -1 ? SomeHolder.\u0023\u003Dzb_cGIc1Gaz9NCBr7Vid8IS0uexlk : (short) ((int) SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0.\u0023\u003DzZow2cpyJf0iA9PWOxc7zJzmWW\u00243V() ^ (1364891845 ^ num1) + num2 ^ num25);
          if (num8 == (short) 0)
          {
            numArray1 = (byte[]) null;
          }
          else
          {
            numArray1 = SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0.\u0023\u003Dz7otzSwnsw4RDi2ZlITp992L\u0024fiUz((int) num8);
            for (int index = 0; index != numArray1.Length; ++index)
              numArray1[index] ^= (byte) (SomeHolder.\u0023\u003DzHJTTZusbrbwvHIdtC4mDBYulAWWH >> ((3 & index) << 3));
          }
        }
        num3 = SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0.\u0023\u003DzNv8KaF2K5FOLeTYczqacCLhz4gGy() ^ num25 ^ -~~-~-~-~(530482054 - num1 - num2) ^ num7;
        if (num3 == (num1 ^ 1364930321 ^ num2))
        {
          byte[] numArray3 = SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0.\u0023\u003Dz7otzSwnsw4RDi2ZlITp992L\u0024fiUz(4);
          _param0 = -1734526753 - num1 + num2 ^ num7;
          _param0 = ((int) numArray3[2] | (int) numArray3[3] << 16 | (int) numArray3[0] << 8 | (int) numArray3[1] << 24) ^ -_param0;
        }
        else
        {
          num4 = num1 - 1766606547 - num2;
          wg613TfXsjoCdJu0I = SomeHolder.\u0023\u003DzLNaTOymPPDjWg613TfXsjoCdJu0I;
          num5 = num3;
          num6 = wg613TfXsjoCdJu0I - 12;
          num3 &= num1 ^ -1587859696 ^ num2;
          numArray2 = SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0.\u0023\u003Dz7otzSwnsw4RDi2ZlITp992L\u0024fiUz(num3);
          ehfouW3qAqIhiQiaZxc = SomeHolder.\u0023\u003DzSH4i4aHHdEHFOUW3qAqIHIQiaZXC;
          goto label_33;
        }
      }
    }
    while (!SomeHolder.\u0023\u003DzmGmcXflrTCmUF5eABdHsWeZB\u0024Erj.TryGetValue(_param0, out str1));
    return str1;
label_33:
    bool flag1 = (uint) (num5 & -1898517233 - num1 - num2) > 0U;
    bool flag2 = (uint) (num5 & 824775409 + num1 + num2) > 0U;
    bool flag3 = (uint) (num5 & (num1 ^ -1901801233 ^ num2)) > 0U;
    byte[] numArray4 = numArray1;
    byte[] numArray5 = numArray2;
    byte[] numArray6 = numArray4;
    byte num26 = numArray6[1];
    int length1 = numArray5.Length;
    byte num27 = (byte) (length1 + 11 ^ 7 + (int) num26);
    uint num28 = (uint) (((int) numArray6[0] | (int) numArray6[2] << 8) + ((int) num27 << 3));
    int index1 = 0;
    ushort num29 = 0;
    while (index1 < length1)
    {
      if ((1 & index1) == 0)
      {
        num28 = (uint) ((int) num28 * (num1 - 1322494226 + num2) + (1325239250 - num1 - num2));
        num29 = (ushort) (num28 >> 16);
      }
      byte num7 = (byte) num29;
      num29 >>= 8;
      byte num8 = numArray5[index1];
      numArray5[index1] = (byte) ((uint) ((int) num26 ^ (int) num8 ^ 3 + (int) num27) ^ (uint) num7);
      ++index1;
      num27 = num8;
    }
    byte[] numArray7 = numArray5;
    if (ehfouW3qAqIhiQiaZxc != null != (wg613TfXsjoCdJu0I != num4))
    {
      for (int index2 = 0; index2 < num3; index2 = 1 + index2)
      {
        byte num7 = ehfouW3qAqIhiQiaZxc[7 & index2];
        byte num8 = (byte) ((int) num7 << 3 | (int) num7 >> 5);
        numArray7[index2] = (byte) ((uint) numArray7[index2] ^ (uint) num8);
      }
    }
    int length2;
    byte[] bytes;
    if (!flag2)
    {
      length2 = num3;
      bytes = numArray7;
    }
    else
    {
      length2 = (int) numArray7[2] | (int) numArray7[0] << 16 | (int) numArray7[3] << 8 | (int) numArray7[1] << 24;
      bytes = new byte[length2];
      SomeHolder.\u0023\u003DzyMYipwrzdyVCXp64W_uGZwyLpOfJ(numArray7, 4, bytes);
    }
    string str2;
    if (flag1 && num6 == num4 - 12)
    {
      char[] chArray = new char[length2];
      for (int index2 = 0; index2 < length2; index2 = 1 + index2)
        chArray[index2] = (char) bytes[index2];
      str2 = (string) new string(chArray);
    }
    else
      str2 = Encoding.Unicode.GetString(bytes, 0, bytes.Length);
    int num30 = num6 + ((-1364930416 ^ num1 ^ num2) + (3 & num6) << 5);
    if (num30 != num4 - 12 + (num1 - 1768214234 - num2 + (num4 - 12 & 3) << 5))
    {
      int num7 = _param0 + num3 ^ (num1 ^ 1363731881) + num2 ^ num30 & num1 - 1768213068 - num2;
      ref int local = ref num7;
      StringBuilder stringBuilder = new StringBuilder();
      int num8 = (num1 ^ 1364930377) + num2;
      stringBuilder.Append((char) (byte) num8);
      string format = stringBuilder.ToString();
      str2 = local.ToString(format);
    }
    if (!flag3 & _param1)
    {
      str2 = string.Intern(str2);
      SomeHolder.\u0023\u003DzmGmcXflrTCmUF5eABdHsWeZB\u0024Erj[_param0] = str2;
      if (SomeHolder.\u0023\u003DzmGmcXflrTCmUF5eABdHsWeZB\u0024Erj.Count == (1322708454 - num1 ^ num2))
      {
        bool lockTaken = false;
        ConcurrentDictionary<int, string> uf5eAbdHsWeZbErj = SomeHolder.\u0023\u003DzmGmcXflrTCmUF5eABdHsWeZB\u0024Erj;
        try
        {
          Monitor.Enter((object) uf5eAbdHsWeZbErj, ref lockTaken);
          SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0.\u0023\u003DzT\u0024rGNbOtGD8Hte\u0024VpX69qjz2K_vz();
          SomeHolder.\u0023\u003Dz_889O7bw5\u0024h1\u00246i594v9qjNg71i0 = (SomeHolder.\u0023\u003Dz\u0024f53\u0024vGCciUdlDPVMN3w6sDlI0S\u0024) null;
          SomeHolder.\u0023\u003Dz8WqnBEZ\u0024aZZfrB24QoA5hZBfseb3 = (byte[]) null;
          SomeHolder.\u0023\u003DzSH4i4aHHdEHFOUW3qAqIHIQiaZXC = (byte[]) null;
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit((object) uf5eAbdHsWeZbErj);
        }
      }
    }
    return str2;
  }

  private static AssemblyName \u0023\u003DzFNIITv3O8\u00246FvducSlyAgUcU6CLp(
    Assembly _param0)
  {
    try
    {
      return _param0.GetName();
    }
    catch (object ex)
    {
      return new AssemblyName(_param0.FullName);
    }
  }

  private static byte[] \u0023\u003Dzvbn_2vDiqBiXMT9LNeetMV\u0024FMzeX(AssemblyName _param0)
  {
    byte[] numArray = _param0.GetPublicKeyToken();
    if (numArray != null && numArray.Length == 0)
      numArray = (byte[]) null;
    return numArray;
  }

  private static void \u0023\u003DzyMYipwrzdyVCXp64W_uGZwyLpOfJ(
    byte[] _param0,
    int _param1,
    byte[] _param2)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 128;
    int length = _param2.Length;
label_10:
    while (num1 < length)
    {
      if ((num3 <<= 1) == 256)
      {
        num3 = 1;
        num2 = (int) _param0[_param1++];
      }
      if ((num2 & num3) != 0)
      {
        int num4 = ((int) _param0[_param1] >> 2) + 3;
        int num5 = ((int) _param0[_param1] << 8 | (int) _param0[_param1 + 1]) & 1023;
        _param1 += 2;
        int num6 = num1 - num5;
        if (num6 < 0)
          break;
        while (true)
        {
          if (--num4 >= 0 && num1 < length)
            _param2[num1++] = _param2[num6++];
          else
            goto label_10;
        }
      }
      else
        _param2[num1++] = _param0[_param1++];
    }
  }

  private sealed class \u0023\u003Dz\u0024f53\u0024vGCciUdlDPVMN3w6sDlI0S\u0024
  {
    private Stream \u0023\u003DzMWk2BCcd\u0024oqATJsekvHCObsop2pk;
    private byte[] \u0023\u003DzfxiGMIoFdCrthPjbv_JBQIdKglrd;

    public \u0023\u003Dz\u0024f53\u0024vGCciUdlDPVMN3w6sDlI0S\u0024(Stream _param1)
    {
      this.\u0023\u003DzMWk2BCcd\u0024oqATJsekvHCObsop2pk = _param1;
      this.\u0023\u003DzfxiGMIoFdCrthPjbv_JBQIdKglrd = new byte[4];
    }

    public Stream \u0023\u003DzWa3CJ7DWTs3WAg3yxjUuoDKKjhxA()
    {
      return this.\u0023\u003DzMWk2BCcd\u0024oqATJsekvHCObsop2pk;
    }

    public short \u0023\u003DzZow2cpyJf0iA9PWOxc7zJzmWW\u00243V()
    {
      this.\u0023\u003DzQ8Ig4pRT5tUtlK55jdHqhvc\u003D(2);
      return (short) ((int) this.\u0023\u003DzfxiGMIoFdCrthPjbv_JBQIdKglrd[0] | (int) this.\u0023\u003DzfxiGMIoFdCrthPjbv_JBQIdKglrd[1] << 8);
    }

    public int \u0023\u003DzNv8KaF2K5FOLeTYczqacCLhz4gGy()
    {
      this.\u0023\u003DzQ8Ig4pRT5tUtlK55jdHqhvc\u003D(4);
      return (int) this.\u0023\u003DzfxiGMIoFdCrthPjbv_JBQIdKglrd[0] | (int) this.\u0023\u003DzfxiGMIoFdCrthPjbv_JBQIdKglrd[1] << 8 | (int) this.\u0023\u003DzfxiGMIoFdCrthPjbv_JBQIdKglrd[2] << 16 | (int) this.\u0023\u003DzfxiGMIoFdCrthPjbv_JBQIdKglrd[3] << 24;
    }

    private static void \u0023\u003DzSfIKLSbmHlaboyl2ov_sQKSnQrDa()
    {
      throw new EndOfStreamException();
    }

    private void \u0023\u003DzQ8Ig4pRT5tUtlK55jdHqhvc\u003D(int _param1)
    {
      int offset = 0;
      if (_param1 == 1)
      {
        int num = this.\u0023\u003DzMWk2BCcd\u0024oqATJsekvHCObsop2pk.ReadByte();
        if (num == -1)
          SomeHolder.\u0023\u003Dz\u0024f53\u0024vGCciUdlDPVMN3w6sDlI0S\u0024.\u0023\u003DzSfIKLSbmHlaboyl2ov_sQKSnQrDa();
        this.\u0023\u003DzfxiGMIoFdCrthPjbv_JBQIdKglrd[0] = (byte) num;
      }
      else
      {
        do
        {
          int num = this.\u0023\u003DzMWk2BCcd\u0024oqATJsekvHCObsop2pk.Read(this.\u0023\u003DzfxiGMIoFdCrthPjbv_JBQIdKglrd, offset, _param1 - offset);
          if (num == 0)
            SomeHolder.\u0023\u003Dz\u0024f53\u0024vGCciUdlDPVMN3w6sDlI0S\u0024.\u0023\u003DzSfIKLSbmHlaboyl2ov_sQKSnQrDa();
          offset += num;
        }
        while (offset < _param1);
      }
    }

    public void \u0023\u003DzT\u0024rGNbOtGD8Hte\u0024VpX69qjz2K_vz()
    {
      Stream atJsekvHcObsop2pk = this.\u0023\u003DzMWk2BCcd\u0024oqATJsekvHCObsop2pk;
      this.\u0023\u003DzMWk2BCcd\u0024oqATJsekvHCObsop2pk = (Stream) null;
      atJsekvHcObsop2pk?.Close();
      this.\u0023\u003DzfxiGMIoFdCrthPjbv_JBQIdKglrd = (byte[]) null;
    }

    public byte[] \u0023\u003Dz7otzSwnsw4RDi2ZlITp992L\u0024fiUz(int _param1)
    {
      if (_param1 < 0)
        throw new ArgumentOutOfRangeException();
      byte[] buffer = new byte[_param1];
      int length = 0;
      do
      {
        int num = this.\u0023\u003DzMWk2BCcd\u0024oqATJsekvHCObsop2pk.Read(buffer, length, _param1);
        if (num != 0)
        {
          length += num;
          _param1 -= num;
        }
        else
          break;
      }
      while (_param1 > 0);
      if (length != buffer.Length)
      {
        byte[] numArray = new byte[length];
        Buffer.BlockCopy((Array) buffer, 0, (Array) numArray, 0, length);
        buffer = numArray;
      }
      return buffer;
    }
  }

  private enum \u0023\u003DzPYz8VIgpZ4GFzotf9qZ3n_eps_Pj
  {
  }
}
