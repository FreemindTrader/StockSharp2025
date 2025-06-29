// Decompiled with JetBrains decompiler
// Type: #=zog2RRdwFHTXCAYv8G696lSlkzE2A9FZw6Nreq9_G$I2L
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003Dzog2RRdwFHTXCAYv8G696lSlkzE2A9FZw6Nreq9_G\u0024I2L : 
  IEquatable<\u0023\u003Dzog2RRdwFHTXCAYv8G696lSlkzE2A9FZw6Nreq9_G\u0024I2L>
{
  
  private Color \u0023\u003Dz3r6jbzmLus0miFTvtA\u003D\u003D;
  
  private char \u0023\u003DzBKq7DHDJX9M_\u00241uWlg\u003D\u003D;
  
  private string \u0023\u003Dz\u0024FtDSFPymyy8nwXpCkEGtF4\u003D;
  
  private FontWeight \u0023\u003Dz1O5theycM5GnI\u0024n9Uf0pl6c\u003D;
  
  private float \u0023\u003DzBRCWtDn5nuSwxqXxOA\u003D\u003D;

  public Color \u0023\u003DzWR\u0024VcnfG1_Vk() => this.\u0023\u003Dz3r6jbzmLus0miFTvtA\u003D\u003D;

  public void \u0023\u003Dzt6hPoGyP_GFA(Color _param1)
  {
    this.\u0023\u003Dz3r6jbzmLus0miFTvtA\u003D\u003D = _param1;
  }

  public char \u0023\u003Dz6OYvaJq_Jxe1() => this.\u0023\u003DzBKq7DHDJX9M_\u00241uWlg\u003D\u003D;

  public void \u0023\u003Dz8Odk1LFUZS2b(char _param1)
  {
    this.\u0023\u003DzBKq7DHDJX9M_\u00241uWlg\u003D\u003D = _param1;
  }

  public string \u0023\u003DzfFpWmUYdz7xm() => this.\u0023\u003Dz\u0024FtDSFPymyy8nwXpCkEGtF4\u003D;

  public void \u0023\u003DzWkJ\u0024_LzO16MF(string _param1)
  {
    this.\u0023\u003Dz\u0024FtDSFPymyy8nwXpCkEGtF4\u003D = _param1;
  }

  public FontWeight \u0023\u003DzmTA5w5GPXfNk()
  {
    return this.\u0023\u003Dz1O5theycM5GnI\u0024n9Uf0pl6c\u003D;
  }

  public void \u0023\u003DzG1z2GkU3awEN(FontWeight _param1)
  {
    this.\u0023\u003Dz1O5theycM5GnI\u0024n9Uf0pl6c\u003D = _param1;
  }

  public float FontSize
  {
    get => this.\u0023\u003DzBRCWtDn5nuSwxqXxOA\u003D\u003D;
    set => this.\u0023\u003DzBRCWtDn5nuSwxqXxOA\u003D\u003D = value;
  }

  public override int GetHashCode()
  {
    return this.\u0023\u003DzWR\u0024VcnfG1_Vk().GetHashCode() ^ this.\u0023\u003Dz6OYvaJq_Jxe1().GetHashCode() ^ this.\u0023\u003DzfFpWmUYdz7xm().GetHashCode() ^ this.\u0023\u003DzmTA5w5GPXfNk().GetHashCode() ^ this.FontSize.GetHashCode();
  }

  public bool Equals(
    \u0023\u003Dzog2RRdwFHTXCAYv8G696lSlkzE2A9FZw6Nreq9_G\u0024I2L _param1)
  {
    return _param1 != null && (int) _param1.\u0023\u003Dz6OYvaJq_Jxe1() == (int) this.\u0023\u003Dz6OYvaJq_Jxe1() && _param1.\u0023\u003DzWR\u0024VcnfG1_Vk() == this.\u0023\u003DzWR\u0024VcnfG1_Vk() && _param1.\u0023\u003DzfFpWmUYdz7xm() == this.\u0023\u003DzfFpWmUYdz7xm() && _param1.\u0023\u003DzmTA5w5GPXfNk().Equals(this.\u0023\u003DzmTA5w5GPXfNk()) && (double) _param1.FontSize == (double) this.FontSize;
  }

  public override bool Equals(object _param1)
  {
    return this.Equals(_param1 as \u0023\u003Dzog2RRdwFHTXCAYv8G696lSlkzE2A9FZw6Nreq9_G\u0024I2L);
  }
}
