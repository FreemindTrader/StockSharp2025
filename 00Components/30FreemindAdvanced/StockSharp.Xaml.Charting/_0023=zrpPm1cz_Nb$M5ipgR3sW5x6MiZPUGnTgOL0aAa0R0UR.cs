// Decompiled with JetBrains decompiler
// Type: #=zrpPm1cz_Nb$M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using Ecng.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable disable
internal sealed class \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH : 
  dje_zW9CS5E2KYALJRMCDFUV9GBWAD6FTS94JN3AD7LFC8K7BTMRPRXTEX_ejd
{
  
  private int \u0023\u003DzbMH2duO6275S\u0024V7L\u0024g\u003D\u003D;

  public \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH()
  {
    this.SourceMode = \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.AllSeries;
    this.ReceiveHandledEvents = true;
    this.IsEnabled = true;
    this.SetCurrentValue(dje_zW9CS5E2KYALJRMCDFUV9GBWAD6FTS94JN3AD7LFC8K7BTMRPRXTEX_ejd.\u0023\u003DzE7h5hUE7Vu4g, (object) new \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D());
  }

  protected override bool \u0023\u003DzaBvGZQmHUOsn(
    IRenderableSeries _param1)
  {
    return true;
  }

  protected override bool \u0023\u003DzD5SquRN7M_9c(
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param1)
  {
    return true;
  }

  protected override void \u0023\u003Dz_wtru8oSZoY9(Point _param1)
  {
    this.SeriesData.\u0023\u003DzGPrsWyT8SibF(this.\u0023\u003DzzhlDItrRFv\u0024\u0024(_param1));
    CollectionHelper.ForEach<IGrouping<UIBaseVM, \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>>(this.SeriesData.SeriesInfo.GroupBy<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, UIBaseVM>(\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D ?? (\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, UIBaseVM>(\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzP2KQy6cvHJetC3NEOoABaIg\u003D))).Where<IGrouping<UIBaseVM, \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>>(\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzPgm0u7xHO4Weuga3SQ\u003D\u003D ?? (\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzPgm0u7xHO4Weuga3SQ\u003D\u003D = new Func<IGrouping<UIBaseVM, \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>, bool>(\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzCyJGy_nYUkMGqDtkSnLYZgM\u003D))), \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzr3tKWd1XimhmhizQXg\u003D\u003D ?? (\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzr3tKWd1XimhmhizQXg\u003D\u003D = new Action<IGrouping<UIBaseVM, \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>>(\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzxL3Llt6OGrVxJQubHdBdcdo\u003D)));
  }

  protected override void \u0023\u003Dz1z_ZexRGbAiN91rPDA\u003D\u003D(Point _param1)
  {
  }

  protected override void \u0023\u003DzleRWWIS9Sb_X()
  {
  }

  public override void \u0023\u003DzY1JcdEJm3Ryc(
    \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt _param1)
  {
    base.\u0023\u003DzY1JcdEJm3Ryc(_param1);
    int count = this.ParentSurface.get_RenderableSeries().Count;
    if (this.\u0023\u003DzbMH2duO6275S\u0024V7L\u0024g\u003D\u003D == count)
      return;
    this.\u0023\u003DzbMH2duO6275S\u0024V7L\u0024g\u003D\u003D = count;
    this.\u0023\u003DzebZge1miA2O0(new \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY(new Point(this.ModifierSurface.\u0023\u003Dzu2ObQ3hMALTN() / 2.0, this.ModifierSurface.\u0023\u003Dz2kO1mtG\u0024bEUM() / 2.0), (\u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzu9w\u0024DbQvIH5WWg9Ebw\u003D) 0, \u0023\u003Dz11\u00242ZZHXfa65mwO6Nijb7bojGMWzLUPPhd\u0024cYfw\u003D.None, true, (\u0023\u003DzTNhhT9A_S5PTAzjbiBFcpOgj\u0024HEwAG4ZlfwSGT7i2APW) null));
  }

  private sealed class \u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D
  {
    public \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D \u0023\u003Dz\u00246gChyc\u003D;

    internal void \u0023\u003Dzrmvrnfo5nmhUy3uWog0cspw\u003D(
      \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy _param1)
    {
      _param1.\u0023\u003DzGPrsWyT8SibF(this.\u0023\u003Dz\u00246gChyc\u003D);
    }
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, UIBaseVM> \u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D;
    public static Func<IGrouping<UIBaseVM, \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>, bool> \u0023\u003DzPgm0u7xHO4Weuga3SQ\u003D\u003D;
    public static Action<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> \u0023\u003Dz08eHob2PYGlidFcWEw\u003D\u003D;
    public static Action<IGrouping<UIBaseVM, \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>> \u0023\u003Dzr3tKWd1XimhmhizQXg\u003D\u003D;

    internal UIBaseVM \u0023\u003DzP2KQy6cvHJetC3NEOoABaIg\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return !(((FrameworkElement) _param1.RenderableSeries).Tag is Tuple<UIBaseVM, \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[]> tag) ? (UIBaseVM) null : tag.Item1;
    }

    internal bool \u0023\u003DzCyJGy_nYUkMGqDtkSnLYZgM\u003D(
      IGrouping<UIBaseVM, \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> _param1)
    {
      return _param1.Key != null;
    }

    internal void \u0023\u003DzxL3Llt6OGrVxJQubHdBdcdo\u003D(
      IGrouping<UIBaseVM, \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D> _param1)
    {
      CollectionHelper.ForEach<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>((IEnumerable<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>) _param1, \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz08eHob2PYGlidFcWEw\u003D\u003D ?? (\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz08eHob2PYGlidFcWEw\u003D\u003D = new Action<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D>(\u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzSo1idhEGGfqUtId8KFuThME\u003D)));
    }

    internal void \u0023\u003DzSo1idhEGGfqUtId8KFuThME\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D vbxLeArTkallkIdHg = new \u0023\u003DzrpPm1cz_Nb\u0024M5ipgR3sW5x6MiZPUGnTgOL0aAa0R0UReQBJAyQUw5cjprelH.\u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D();
      vbxLeArTkallkIdHg.\u0023\u003Dz\u00246gChyc\u003D = _param1;
      if (!(((FrameworkElement) vbxLeArTkallkIdHg.\u0023\u003Dz\u00246gChyc\u003D.RenderableSeries).Tag is Tuple<UIBaseVM, \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy[]> tag))
        return;
      CollectionHelper.ForEach<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy>((IEnumerable<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy>) tag.Item2, new Action<\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy>(vbxLeArTkallkIdHg.\u0023\u003Dzrmvrnfo5nmhUy3uWog0cspw\u003D));
    }
  }
}
