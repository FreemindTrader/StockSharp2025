// Decompiled with JetBrains decompiler
// Type: #=zolvWmzKCnovSLB$fEd65UxTe9ueugBFPPbNRHut89GEww9Us2BC2kQvpfKw4
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.IO;

#nullable disable
internal static class \u0023\u003DzolvWmzKCnovSLB\u0024fEd65UxTe9ueugBFPPbNRHut89GEww9Us2BC2kQvpfKw4
{
  private static bool \u0023\u003DzI9OrIVJe2vBk;

  public static void \u0023\u003Dz90p4sVE\u003D(string _param0)
  {
    FileStream fileStream;
    if (\u0023\u003DzolvWmzKCnovSLB\u0024fEd65UxTe9ueugBFPPbNRHut89GEww9Us2BC2kQvpfKw4.\u0023\u003DzI9OrIVJe2vBk)
    {
      fileStream = new FileStream("test.txt", FileMode.Append, FileAccess.Write);
    }
    else
    {
      fileStream = new FileStream("test.txt", FileMode.Create, FileAccess.Write);
      \u0023\u003DzolvWmzKCnovSLB\u0024fEd65UxTe9ueugBFPPbNRHut89GEww9Us2BC2kQvpfKw4.\u0023\u003DzI9OrIVJe2vBk = true;
    }
    StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
    streamWriter.Write(_param0);
    streamWriter.Close();
    fileStream.Close();
  }
}
