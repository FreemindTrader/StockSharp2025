// Decompiled with JetBrains decompiler
// Type: #=zAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dr0Mor7TsgpRyiVdmhwT3f$leflCXc=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal sealed class \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dr0Mor7TsgpRyiVdmhwT3f\u0024leflCXc\u003D
{
  private double \u0023\u003DzY\u0024iy3H6MDQlk;
  private byte[] \u0023\u003DzWVUZoyYrdEAhL0POug\u003D\u003D;
  private byte[] \u0023\u003Dz_Mi9vVvOQ0BJhRJz_A\u003D\u003D;

  public \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dr0Mor7TsgpRyiVdmhwT3f\u0024leflCXc\u003D()
  {
    this.\u0023\u003DzY\u0024iy3H6MDQlk = 1.0;
    this.\u0023\u003DzWVUZoyYrdEAhL0POug\u003D\u003D = new byte[256 /*0x0100*/];
    this.\u0023\u003Dz_Mi9vVvOQ0BJhRJz_A\u003D\u003D = new byte[256 /*0x0100*/];
  }

  public \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dr0Mor7TsgpRyiVdmhwT3f\u0024leflCXc\u003D(
    double _param1)
  {
    this.\u0023\u003DzY\u0024iy3H6MDQlk = _param1;
    this.\u0023\u003DzWVUZoyYrdEAhL0POug\u003D\u003D = new byte[256 /*0x0100*/];
    this.\u0023\u003Dz_Mi9vVvOQ0BJhRJz_A\u003D\u003D = new byte[256 /*0x0100*/];
    this.\u0023\u003DzsNYjh59Cc7Mf(this.\u0023\u003DzY\u0024iy3H6MDQlk);
  }

  public void \u0023\u003DzsNYjh59Cc7Mf(double _param1)
  {
    this.\u0023\u003DzY\u0024iy3H6MDQlk = _param1;
    for (uint index = 0; index < 256U /*0x0100*/; ++index)
      this.\u0023\u003DzWVUZoyYrdEAhL0POug\u003D\u003D[(int) index] = (byte) \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzROReRE0C5MV7(Math.Pow((double) index / (double) byte.MaxValue, this.\u0023\u003DzY\u0024iy3H6MDQlk) * (double) byte.MaxValue);
    double y = 1.0 / _param1;
    for (uint index = 0; index < 256U /*0x0100*/; ++index)
      this.\u0023\u003Dz_Mi9vVvOQ0BJhRJz_A\u003D\u003D[(int) index] = (byte) \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzROReRE0C5MV7(Math.Pow((double) index / (double) byte.MaxValue, y) * (double) byte.MaxValue);
  }

  public double \u0023\u003DzoxmYZFvB84ZN() => this.\u0023\u003DzY\u0024iy3H6MDQlk;

  public byte \u0023\u003DzKiES5cU\u003D(int _param1)
  {
    return this.\u0023\u003DzWVUZoyYrdEAhL0POug\u003D\u003D[_param1];
  }

  public byte \u0023\u003Dz9YtkX8U\u003D(int _param1)
  {
    return this.\u0023\u003Dz_Mi9vVvOQ0BJhRJz_A\u003D\u003D[_param1];
  }

  private enum \u0023\u003DzdMqfsDYCtUkvsBPFNg\u003D\u003D
  {
  }
}
