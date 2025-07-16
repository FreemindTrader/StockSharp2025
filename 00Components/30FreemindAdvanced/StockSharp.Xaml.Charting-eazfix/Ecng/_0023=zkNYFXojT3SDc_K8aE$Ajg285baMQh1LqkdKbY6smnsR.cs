// Decompiled with JetBrains decompiler
// Type: #=zkNYFXojT3SDc_K8aE$Ajg285baMQh1LqkdKbY6smnsRH$nQalw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;

#nullable disable
public sealed class \u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D : 
  \u0023\u003DzeHqydGt1MYwtwPKPfmmWnLJ77ByVH81gaeQU2i00qjI8QhelTQ\u003D\u003D
{
  
  private bool \u0023\u003DzvEWdpLc\u0024gZvh;
  
  private string \u0023\u003Dzd865j7U\u003D = string.Empty;
  
  private string \u0023\u003Dz3dV4yOVvBOEZr20ovA\u003D\u003D = string.Empty;

  public override bool HasExponent
  {
    get => this.\u0023\u003DzvEWdpLc\u0024gZvh;
    set
    {
      if (this.\u0023\u003DzvEWdpLc\u0024gZvh == value)
        return;
      this.\u0023\u003DzvEWdpLc\u0024gZvh = value;
      this.OnPropertyChanged(nameof (HasExponent));
    }
  }

  public override string Separator
  {
    get => this.\u0023\u003Dzd865j7U\u003D;
    set
    {
      if (!(this.\u0023\u003Dzd865j7U\u003D != value))
        return;
      this.\u0023\u003Dzd865j7U\u003D = value;
      this.OnPropertyChanged(nameof (Separator));
    }
  }

  public override string Exponent
  {
    get => this.\u0023\u003Dz3dV4yOVvBOEZr20ovA\u003D\u003D;
    set
    {
      if (!(this.\u0023\u003Dz3dV4yOVvBOEZr20ovA\u003D\u003D != value))
        return;
      this.\u0023\u003Dz3dV4yOVvBOEZr20ovA\u003D\u003D = value;
      this.OnPropertyChanged(nameof (Exponent));
    }
  }
}
