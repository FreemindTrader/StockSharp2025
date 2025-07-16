// Decompiled with JetBrains decompiler
// Type: #=zEa5ACpOap4rFIaHj5p9yfKOZw$v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
public static class \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY
{
  public static unsafe void \u0023\u003Dz8YbqsoQ7EyhA(
    byte* _param0,
    int _param1,
    byte* _param2,
    int _param3,
    int _param4)
  {
    _param0 += _param1;
    _param2 += _param3;
    \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.dje_zGUAM84XM8WU9682_ejd(_param2, _param0, _param4);
  }

  public static unsafe void \u0023\u003Dz8YbqsoQ7EyhA(int* _param0, int* _param1, int _param2)
  {
    \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.dje_zGUAM84XM8WU9682_ejd((byte*) _param1, (byte*) _param0, _param2 * 4);
  }

  public static void \u0023\u003DzvOds\u0024YwY7CNP(IntPtr _param0, int _param1, int _param2)
  {
    \u0023\u003DzEa5ACpOap4rFIaHj5p9yfKOZw\u0024v7EEle8KTpMJrJSHdUYMMMqiLDrUZC4IkY.dje_zFX4HDMPMMWSHFCA_ejd(_param0, _param1, _param2);
  }

  [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = (CallingConvention) 2)]
  private static extern unsafe byte* dje_zGUAM84XM8WU9682_ejd(
    byte* dje_z94UNALHN_ejd,
    byte* dje_zUBJANP3U_ejd,
    int dje_zQXKRAKZG_ejd);

  [DllImport("msvcrt.dll", EntryPoint = "memset", CallingConvention = (CallingConvention) 2)]
  private static extern void dje_zFX4HDMPMMWSHFCA_ejd(
    IntPtr dje_z94UNALHN_ejd,
    int dje_z74NFVDWP292AF7Z_ejd,
    int dje_zQXKRAKZG_ejd);
}
