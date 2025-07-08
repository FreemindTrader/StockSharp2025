// Decompiled with JetBrains decompiler
// Type: -.dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace \u002D;

internal class dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd : 
  ChartModifierBase
{
  
  public static readonly DependencyProperty \u0023\u003DziAqnE8_\u0024SBDB = DependencyProperty.Register(nameof (YAxisId), typeof (string), typeof (dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd), new PropertyMetadata((object) "DefaultAxisId"));
  
  public static readonly DependencyProperty \u0023\u003DzSEAakZbtZKgY = DependencyProperty.Register(nameof (XAxisId), typeof (string), typeof (dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd), new PropertyMetadata((object) "DefaultAxisId"));
  
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
      this.\u0023\u003DzSsJ0VVqBhS6c = !(value != (Type) null) || typeof (IAnnotation).IsAssignableFrom(value) ? value : throw new ArgumentOutOfRangeException("value", $"Type {value} does not implement IAnnotation interface.");
    }
  }

  public Style AnnotationStyle
  {
    get => this.\u0023\u003DzL3qbeQTHvLZ5;
    set => this.\u0023\u003DzL3qbeQTHvLZ5 = value;
  }

  public IAnnotation Annotation
  {
    get
    {
      return (IAnnotation) this.\u0023\u003DzJu3oQ4_zae0S;
    }
  }

  protected override void \u0023\u003DzCM2UQyuakisf()
  {
    base.\u0023\u003DzCM2UQyuakisf();
    this.\u0023\u003DzJu3oQ4_zae0S = (AnnotationBase) null;
    if (!this.IsEnabled || this.ParentSurface == null)
      return;
    this.ParentSurface.get_Annotations().\u0023\u003Dz30RSSSygABj_<IAnnotation>(dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.SomeClass34343383.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D ?? (dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.SomeClass34343383.\u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D = new Action<IAnnotation>(dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.SomeClass34343383.SomeMethond0343.\u0023\u003DzLdGxEgyAELbBj1m4vj3jYKw\u003D)));
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
    this.ParentSurface.get_Annotations().Add((IAnnotation) instance);
    return instance;
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.SomeClass34343383 SomeMethond0343 = new dje_zZTHFGMBWG8W4RABP3CZXW8NL5AQ7G4LTKKFALWWSGS7J5ARQQP83C_ejd.SomeClass34343383();
    public static Action<IAnnotation> \u0023\u003DzmsiBBf5ih\u0024wjnD8X9g\u003D\u003D;

    internal void \u0023\u003DzLdGxEgyAELbBj1m4vj3jYKw\u003D(
      IAnnotation _param1)
    {
      _param1.set_IsSelected(false);
      _param1.set_IsEditable(false);
    }
  }
}
