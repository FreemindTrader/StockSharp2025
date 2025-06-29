// Decompiled with JetBrains decompiler
// Type: #=zkNYFXojT3SDc_K8aE$Ajg_dUi3E9GqJy4InVE$$Mw_K$X1_XD9dYMcaiKS$3
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

#nullable disable
internal sealed class \u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg_dUi3E9GqJy4InVE\u0024\u0024Mw_K\u0024X1_XD9dYMcaiKS\u00243 : 
  \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<\u0023\u003DzMv9TAT1PEEnC0UeBhCNwDENc6zAkdrvFqAGR1NCAy_gz9BuO7gAeqY9kG_DT>
{
  public override void \u0023\u003DzObQSsmE\u003D(
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDENc6zAkdrvFqAGR1NCAy_gz9BuO7gAeqY9kG_DT _param1)
  {
    if (this.\u0023\u003DzG2qqjnQ\u003D() > 1 && !this.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzG2qqjnQ\u003D() - 2].\u0023\u003DzjYzFpMk\u003D(this.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzG2qqjnQ\u003D() - 1]))
      this.\u0023\u003Dz8ClqfHs\u003D();
    base.\u0023\u003DzObQSsmE\u003D(_param1);
  }

  public void \u0023\u003DzTI_2C2gAGMW03CHW9w\u003D\u003D(
    \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDENc6zAkdrvFqAGR1NCAy_gz9BuO7gAeqY9kG_DT _param1)
  {
    this.\u0023\u003Dz8ClqfHs\u003D();
    this.\u0023\u003DzObQSsmE\u003D(_param1);
  }

  public void \u0023\u003DzEzdbhJc\u003D(bool _param1)
  {
    while (this.\u0023\u003DzG2qqjnQ\u003D() > 1 && !this.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzG2qqjnQ\u003D() - 2].\u0023\u003DzjYzFpMk\u003D(this.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzG2qqjnQ\u003D() - 1]))
    {
      \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDENc6zAkdrvFqAGR1NCAy_gz9BuO7gAeqY9kG_DT gz9BuO7gAeqY9kGDt = this[this.\u0023\u003DzG2qqjnQ\u003D() - 1];
      this.\u0023\u003Dz8ClqfHs\u003D();
      this.\u0023\u003DzTI_2C2gAGMW03CHW9w\u003D\u003D(gz9BuO7gAeqY9kGDt);
    }
    if (!_param1)
      return;
    while (this.\u0023\u003DzG2qqjnQ\u003D() > 1 && !this.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzG2qqjnQ\u003D() - 1].\u0023\u003DzjYzFpMk\u003D(this.\u0023\u003DzvsnCYl4\u003D()[0]))
      this.\u0023\u003Dz8ClqfHs\u003D();
  }

  internal \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDENc6zAkdrvFqAGR1NCAy_gz9BuO7gAeqY9kG_DT \u0023\u003Dz9JbQ4jY\u003D(
    int _param1)
  {
    return this[(_param1 + this.\u0023\u003DzGupBQuw\u003D - 1) % this.\u0023\u003DzGupBQuw\u003D];
  }

  internal \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDENc6zAkdrvFqAGR1NCAy_gz9BuO7gAeqY9kG_DT \u0023\u003DzLCJuIho\u003D(
    int _param1)
  {
    return this[_param1];
  }

  internal \u0023\u003DzMv9TAT1PEEnC0UeBhCNwDENc6zAkdrvFqAGR1NCAy_gz9BuO7gAeqY9kG_DT \u0023\u003Dz2zQMp9I\u003D(
    int _param1)
  {
    return this[(_param1 + 1) % this.\u0023\u003DzGupBQuw\u003D];
  }
}
