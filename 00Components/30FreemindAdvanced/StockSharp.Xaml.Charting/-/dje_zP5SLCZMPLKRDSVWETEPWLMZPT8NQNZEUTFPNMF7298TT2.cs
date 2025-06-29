// Decompiled with JetBrains decompiler
// Type: -.dje_zP5SLCZMPLKRDSVWETEPWLMZPT8NQNZEUTFPNMF7298TT2H3CX8DZZ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zP5SLCZMPLKRDSVWETEPWLMZPT8NQNZEUTFPNMF7298TT2H3CX8DZZ_ejd : ContentControl
{
  
  public static readonly DependencyProperty \u0023\u003DzXMV_skc\u003D = DependencyProperty.Register("", typeof (Orientation), typeof (dje_zP5SLCZMPLKRDSVWETEPWLMZPT8NQNZEUTFPNMF7298TT2H3CX8DZZ_ejd), new PropertyMetadata((object) Orientation.Horizontal));

  public dje_zP5SLCZMPLKRDSVWETEPWLMZPT8NQNZEUTFPNMF7298TT2H3CX8DZZ_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zP5SLCZMPLKRDSVWETEPWLMZPT8NQNZEUTFPNMF7298TT2H3CX8DZZ_ejd);
  }

  public Orientation Orientation
  {
    get
    {
      return (Orientation) this.GetValue(dje_zP5SLCZMPLKRDSVWETEPWLMZPT8NQNZEUTFPNMF7298TT2H3CX8DZZ_ejd.\u0023\u003DzXMV_skc\u003D);
    }
    set
    {
      this.SetValue(dje_zP5SLCZMPLKRDSVWETEPWLMZPT8NQNZEUTFPNMF7298TT2H3CX8DZZ_ejd.\u0023\u003DzXMV_skc\u003D, (object) value);
    }
  }
}
