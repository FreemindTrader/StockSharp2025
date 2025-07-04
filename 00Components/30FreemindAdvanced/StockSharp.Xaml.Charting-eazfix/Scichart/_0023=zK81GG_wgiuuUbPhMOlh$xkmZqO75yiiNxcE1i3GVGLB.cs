// Decompiled with JetBrains decompiler
// Type: #=zK81GG_wgiuuUbPhMOlh$xkmZqO75yiiNxcE1i3GVGLBk7pXAGd$2IV5d6o6Os2AavA==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
internal sealed class \u0023\u003DzK81GG_wgiuuUbPhMOlh\u0024xkmZqO75yiiNxcE1i3GVGLBk7pXAGd\u00242IV5d6o6Os2AavA\u003D\u003D
{
  private \u0023\u003DzGf68ilGq59TJ0aVKr0K_9eoU7rVJSKf2\u0024pLc\u0024Q_CRRI0B0xDoi_RvpM0a\u0024y\u0024HqY7Xw\u003D\u003D<byte> \u0023\u003Dznvk9a7y2KXcJ = new \u0023\u003DzGf68ilGq59TJ0aVKr0K_9eoU7rVJSKf2\u0024pLc\u0024Q_CRRI0B0xDoi_RvpM0a\u0024y\u0024HqY7Xw\u003D\u003D<byte>();
  private byte[] \u0023\u003DzY\u0024iy3H6MDQlk = new byte[256 /*0x0100*/];
  private int \u0023\u003DzV_VWhM\u00240YBe2u0P9ylDRaVg\u003D;
  private double \u0023\u003DzEvw\u0024o4zKEtz2;
  private double \u0023\u003Dzqedki754HHbcGut7PfXsq\u0024I\u003D;

  public \u0023\u003DzK81GG_wgiuuUbPhMOlh\u0024xkmZqO75yiiNxcE1i3GVGLBk7pXAGd\u00242IV5d6o6Os2AavA\u003D\u003D()
  {
    this.\u0023\u003DzV_VWhM\u00240YBe2u0P9ylDRaVg\u003D = 0;
    this.\u0023\u003DzEvw\u0024o4zKEtz2 = 1.0;
    this.\u0023\u003Dzqedki754HHbcGut7PfXsq\u0024I\u003D = 1.0;
    for (int index = 0; index < 256 /*0x0100*/; ++index)
      this.\u0023\u003DzY\u0024iy3H6MDQlk[index] = (byte) index;
  }

  public \u0023\u003DzK81GG_wgiuuUbPhMOlh\u0024xkmZqO75yiiNxcE1i3GVGLBk7pXAGd\u00242IV5d6o6Os2AavA\u003D\u003D(
    double _param1,
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOx1RkmuVmhXUogDkNvXBX5gA\u003D\u003D _param2)
  {
    this.\u0023\u003DzV_VWhM\u00240YBe2u0P9ylDRaVg\u003D = 0;
    this.\u0023\u003DzEvw\u0024o4zKEtz2 = 1.0;
    this.\u0023\u003Dzqedki754HHbcGut7PfXsq\u0024I\u003D = 1.0;
    this.\u0023\u003DzruxDfy35wG0J(_param2);
    this.\u0023\u003DzCIN619c\u003D(_param1);
  }

  public void \u0023\u003DzS7aVj_ZQG138(double _param1)
  {
    this.\u0023\u003DzEvw\u0024o4zKEtz2 = _param1;
  }

  public void \u0023\u003Dzgt0Ljrpc\u0024SBET6lC4Q\u003D\u003D(double _param1)
  {
    this.\u0023\u003Dzqedki754HHbcGut7PfXsq\u0024I\u003D = _param1;
  }

  public void \u0023\u003DzruxDfy35wG0J(
    \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOx1RkmuVmhXUogDkNvXBX5gA\u003D\u003D _param1)
  {
    for (int index = 0; index < 256 /*0x0100*/; ++index)
      this.\u0023\u003DzY\u0024iy3H6MDQlk[index] = (byte) \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzROReRE0C5MV7(_param1.\u0023\u003DzoxmYZFvB84ZN((double) index / (double) byte.MaxValue) * (double) byte.MaxValue);
  }

  public void \u0023\u003DzCIN619c\u003D(double _param1)
  {
    if (_param1 < 0.0)
      _param1 = 0.0;
    if (_param1 < this.\u0023\u003Dzqedki754HHbcGut7PfXsq\u0024I\u003D)
      _param1 += _param1;
    else
      _param1 += this.\u0023\u003Dzqedki754HHbcGut7PfXsq\u0024I\u003D;
    _param1 *= 0.5;
    _param1 -= this.\u0023\u003Dzqedki754HHbcGut7PfXsq\u0024I\u003D;
    double zqedki754HhbcGut7PfXsqI = this.\u0023\u003Dzqedki754HHbcGut7PfXsq\u0024I\u003D;
    if (_param1 < 0.0)
    {
      zqedki754HhbcGut7PfXsqI += _param1;
      _param1 = 0.0;
    }
    this.\u0023\u003DzUtSIjNc\u003D(_param1, zqedki754HhbcGut7PfXsqI);
  }

  public int \u0023\u003DzZnzRbK4OTE0q()
  {
    return this.\u0023\u003Dznvk9a7y2KXcJ.\u0023\u003DzdTxNrgQ\u003D();
  }

  public int \u0023\u003DzqKKXccv\u0024wvZxrEpbtQ\u003D\u003D()
  {
    return this.\u0023\u003DzV_VWhM\u00240YBe2u0P9ylDRaVg\u003D;
  }

  public double \u0023\u003DzS7aVj_ZQG138() => this.\u0023\u003DzEvw\u0024o4zKEtz2;

  public double \u0023\u003Dzgt0Ljrpc\u0024SBET6lC4Q\u003D\u003D()
  {
    return this.\u0023\u003Dzqedki754HHbcGut7PfXsq\u0024I\u003D;
  }

  public byte \u0023\u003DzxGz2_8k\u003D(int _param1)
  {
    return this.\u0023\u003Dznvk9a7y2KXcJ.\u0023\u003DzvsnCYl4\u003D()[_param1 + 512 /*0x0200*/];
  }

  private byte[] \u0023\u003Dze_G\u0024P7U\u003D(double _param1)
  {
    this.\u0023\u003DzV_VWhM\u00240YBe2u0P9ylDRaVg\u003D = \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzROReRE0C5MV7(_param1 * 256.0);
    int num = this.\u0023\u003DzV_VWhM\u00240YBe2u0P9ylDRaVg\u003D + 1536 /*0x0600*/;
    if (num > this.\u0023\u003Dznvk9a7y2KXcJ.\u0023\u003DzdTxNrgQ\u003D())
      this.\u0023\u003Dznvk9a7y2KXcJ.\u0023\u003Dz7FKHKl8\u003D(num);
    return this.\u0023\u003Dznvk9a7y2KXcJ.\u0023\u003DzvsnCYl4\u003D();
  }

  private void \u0023\u003DzUtSIjNc\u003D(double _param1, double _param2)
  {
    double num1 = 1.0;
    if (_param1 == 0.0)
      _param1 = 1.0 / 256.0;
    if (_param2 == 0.0)
      _param2 = 1.0 / 256.0;
    double num2 = _param1 + _param2;
    if (num2 < this.\u0023\u003DzEvw\u0024o4zKEtz2)
    {
      double num3 = num2 / this.\u0023\u003DzEvw\u0024o4zKEtz2;
      num1 *= num3;
      _param1 /= num3;
      _param2 /= num3;
    }
    byte[] numArray = this.\u0023\u003Dze_G\u0024P7U\u003D(_param1 + _param2);
    int num4 = (int) (_param1 * 256.0);
    int num5 = (int) (_param2 * 256.0);
    int num6 = 512 /*0x0200*/;
    int num7 = num6 + num4;
    int num8 = (int) this.\u0023\u003DzY\u0024iy3H6MDQlk[(int) (num1 * (double) byte.MaxValue)];
    int num9 = num6;
    for (int index = 0; index < num4; ++index)
      numArray[num9++] = (byte) num8;
    for (int index = 0; index < num5; ++index)
      numArray[num7++] = this.\u0023\u003DzY\u0024iy3H6MDQlk[(int) ((num1 - num1 * ((double) index / (double) num5)) * (double) byte.MaxValue)];
    int num10 = numArray.Length - num5 - num4 - 512 /*0x0200*/;
    int num11 = (int) this.\u0023\u003DzY\u0024iy3H6MDQlk[0];
    for (int index = 0; index < num10; ++index)
      numArray[num7++] = (byte) num11;
    int num12 = num6;
    for (int index = 0; index < 512 /*0x0200*/; ++index)
      numArray[--num12] = numArray[num6++];
    for (int index = 0; index < numArray.Length; ++index)
      this.\u0023\u003Dznvk9a7y2KXcJ.\u0023\u003DzvsnCYl4\u003D()[index] = numArray[index];
  }
}
