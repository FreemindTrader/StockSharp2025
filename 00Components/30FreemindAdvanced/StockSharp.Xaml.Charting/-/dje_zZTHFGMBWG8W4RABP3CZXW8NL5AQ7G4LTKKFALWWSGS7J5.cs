// Decompiled with JetBrains decompiler
// Type: -.dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal class dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd : 
  ChartModifierBase
{
  
  public static readonly DependencyProperty \u0023\u003DziAqnE8_\u0024SBDB = DependencyProperty.Register("", typeof (string), typeof (dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd), new PropertyMetadata((object) ""));
  
  public static readonly DependencyProperty \u0023\u003DzSEAakZbtZKgY = DependencyProperty.Register("", typeof (string), typeof (dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd), new PropertyMetadata((object) ""));
  
  private Point \u0023\u003DzxMlGl5jnCfhrWW4I2Vp56HE\u003D;
  
  private AnnotationBase \u0023\u003DzJu3oQ4_zae0S;
  
  private Type \u0023\u003DzSsJ0VVqBhS6c;
  
  private Style \u0023\u003DzL3qbeQTHvLZ5;

  public event EventHandler dje_zHHBMH6HSAJ3TGDQ_ejd;

  public void \u0023\u003DzyBjJfv2nzVhf(EventHandler _param1);

  public void \u0023\u003DzzZ2ZUVx2hG0P(EventHandler _param1);

  public string YAxisId
  {
    get
    {
      return (string) this.GetValue(dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.\u0023\u003DziAqnE8_\u0024SBDB);
    }
    set
    {
      this.SetValue(dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.\u0023\u003DziAqnE8_\u0024SBDB, (object) value);
    }
  }

  public string XAxisId
  {
    get
    {
      return (string) this.GetValue(dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.\u0023\u003DzSEAakZbtZKgY);
    }
    set
    {
      this.SetValue(dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.\u0023\u003DzSEAakZbtZKgY, (object) value);
    }
  }

  public Type AnnotationType
  {
    get => this.\u0023\u003DzSsJ0VVqBhS6c;
    set
    {
      this.\u0023\u003DzSsJ0VVqBhS6c = !(value != (Type) null) || typeof (\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D).IsAssignableFrom(value) ? value : throw new ArgumentOutOfRangeException("", string.Format("", (object) value));
    }
  }

  public Style AnnotationStyle
  {
    get => this.\u0023\u003DzL3qbeQTHvLZ5;
    set => this.\u0023\u003DzL3qbeQTHvLZ5 = value;
  }

  public \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D Annotation
  {
    get
    {
      return (\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) this.\u0023\u003DzJu3oQ4_zae0S;
    }
  }

  protected override void \u0023\u003DzCM2UQyuakisf()
  {
    base.\u0023\u003DzCM2UQyuakisf();
    this.\u0023\u003DzJu3oQ4_zae0S = (AnnotationBase) null;
    if (!this.IsEnabled || this.ParentSurface == null)
      return;
    this.ParentSurface.get_Annotations().\u0023\u003Dz30RSSSygABj_<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D ?? (dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D = new Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D>(dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzLdGxEgyAELbBj1m4vj3jYKw\u003D)));
  }

  protected void \u0023\u003Dz2OXXIFQJYQ3d()
  {
    EventHandler z6KsbJRt22Hb = this.\u0023\u003Dz6KSbJ_RT22HB;
    if (z6KsbJRt22Hb == null)
      return;
    z6KsbJRt22Hb((object) this, EventArgs.Empty);
  }

  public override void \u0023\u003Dz11bcnbUrALaA(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (this.\u0023\u003DzSsJ0VVqBhS6c == (Type) null || this.\u0023\u003DzJu3oQ4_zae0S == null || !this.\u0023\u003DzJu3oQ4_zae0S.IsAttached || this.\u0023\u003DzJu3oQ4_zae0S.IsSelected)
      return;
    this.\u0023\u003DzJu3oQ4_zae0S.UpdatePosition(this.\u0023\u003DzxMlGl5jnCfhrWW4I2Vp56HE\u003D, this.\u0023\u003DzOaYrn8YGTeR7(_param1.\u0023\u003DztkyOk5amPcz3(), (IHitTestable) this.ModifierSurface));
  }

  private bool \u0023\u003DzYcyvKa51rB8n(Type _param1)
  {
    return typeof (IAnchorPointAnnotation).IsAssignableFrom(_param1) || _param1.IsSubclassOf(typeof (LineAnnotationWithLabelsBase));
  }

  public override void \u0023\u003DzsXEfcKpqchyX(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    base.\u0023\u003DzsXEfcKpqchyX(_param1);
    if (this.\u0023\u003DzSsJ0VVqBhS6c == (Type) null || !this.\u0023\u003DzK46Xo3q3PoYX(_param1.\u0023\u003DzwuSh61ofE2mr(), this.ExecuteOn) || !_param1.\u0023\u003DzCJb5Ya_8UZCR() || this.\u0023\u003DzJu3oQ4_zae0S != null && !this.\u0023\u003DzJu3oQ4_zae0S.IsSelected)
      return;
    _param1.\u0023\u003DzBHH5KNloEXNR(true);
    if (this.\u0023\u003DzJu3oQ4_zae0S != null && this.\u0023\u003DzJu3oQ4_zae0S.IsAttached)
      this.\u0023\u003DzJu3oQ4_zae0S.IsSelected = false;
    this.\u0023\u003DzxMlGl5jnCfhrWW4I2Vp56HE\u003D = this.\u0023\u003DzOaYrn8YGTeR7(_param1.\u0023\u003DztkyOk5amPcz3(), (IHitTestable) this.ModifierSurface);
    if (this.\u0023\u003DzYcyvKa51rB8n(this.\u0023\u003DzSsJ0VVqBhS6c))
      return;
    this.\u0023\u003DzJu3oQ4_zae0S = this.\u0023\u003DzWj46Xvc\u003D(this.\u0023\u003DzSsJ0VVqBhS6c, this.\u0023\u003DzL3qbeQTHvLZ5);
    this.\u0023\u003DzJu3oQ4_zae0S.UpdatePosition(this.\u0023\u003DzxMlGl5jnCfhrWW4I2Vp56HE\u003D, this.\u0023\u003DzxMlGl5jnCfhrWW4I2Vp56HE\u003D);
  }

  public override void \u0023\u003DzU3pYs4rYVmOS(
    \u0023\u003Dz4lH8q7tXMt_gtLJO2itFk2pVig_avtdU95\u0024saf5kXBsY _param1)
  {
    if (this.\u0023\u003DzSsJ0VVqBhS6c == (Type) null || !this.\u0023\u003DzK46Xo3q3PoYX(_param1.\u0023\u003DzwuSh61ofE2mr(), this.ExecuteOn) || !_param1.\u0023\u003DzCJb5Ya_8UZCR())
      return;
    if (this.\u0023\u003DzYcyvKa51rB8n(this.\u0023\u003DzSsJ0VVqBhS6c) && this.\u0023\u003DzJu3oQ4_zae0S == null)
    {
      this.\u0023\u003DzJu3oQ4_zae0S = this.\u0023\u003DzWj46Xvc\u003D(this.\u0023\u003DzSsJ0VVqBhS6c, this.\u0023\u003DzL3qbeQTHvLZ5);
      Point point = this.\u0023\u003DzOaYrn8YGTeR7(_param1.\u0023\u003DztkyOk5amPcz3(), (IHitTestable) this.ModifierSurface);
      this.\u0023\u003DzJu3oQ4_zae0S.UpdatePosition(point, point);
    }
    if (this.\u0023\u003DzJu3oQ4_zae0S == null)
      return;
    AnnotationBase zJu3oQ4Zae0S = this.\u0023\u003DzJu3oQ4_zae0S;
    this.\u0023\u003DzJu3oQ4_zae0S.IsSelected = true;
    this.\u0023\u003Dz2OXXIFQJYQ3d();
    zJu3oQ4Zae0S.UpdateAdorners();
  }

  protected virtual AnnotationBase \u0023\u003DzWj46Xvc\u003D(Type _param1, Style _param2)
  {
    AnnotationBase instance = (AnnotationBase) Activator.CreateInstance(_param1);
    instance.YAxisId = this.YAxisId;
    instance.XAxisId = this.XAxisId;
    if (_param2 != null && _param2.TargetType == _param1)
    {
      Style style = new Style(_param1) { BasedOn = _param2 };
      instance.Style = style;
    }
    this.ParentSurface.get_Annotations().Add((\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D) instance);
    return instance;
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Action<\u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D> \u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D;

    internal void \u0023\u003DzLdGxEgyAELbBj1m4vj3jYKw\u003D(
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5HHh9_3Q0DQKV5SV90k\u003D _param1)
    {
      _param1.set_IsSelected(false);
      _param1.set_IsEditable(false);
    }
  }
}
