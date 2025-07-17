//// Decompiled with JetBrains decompiler
//// Type: fx.Xaml.Charting.Rendering.HighQualityRasterizer.HqRenderContext
//// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
//// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
//// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using MatterHackers.Agg;
//using MatterHackers.Agg.Image;
//using MatterHackers.Agg.VertexSource;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Windows;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;

//namespace fx.Xaml.Charting//{
//  internal class HqRenderContext : RenderContextBase
//  {
//    private readonly RenderOperationLayers _renderLayers = new RenderOperationLayers();
//    private readonly List<IDisposable> _resourcesToDispose = new List<IDisposable>();
//    private object _syncRoot = new object();
//    private readonly WriteableBitmap _bmp;
//    private readonly uint[] _emptyStrideRow;
//    protected readonly Graphics2D _graphics2D;
//    private readonly System.Windows.Controls.Image _image;
//    private readonly ImageBuffer _imageBuffer;
//    protected readonly Size _viewportSize;

//    private TextureCache TextureCache
//    {
//      get
//      {
//        return (TextureCache) _textureCache;
//      }
//    }

//    public HqRenderContext(System.Windows.Controls.Image image, WriteableBitmap bmp, uint[] emptyStrideRow, ImageBuffer imageBuffer, Graphics2D graphics2D, TextureCacheBase textureCache)
//      : base(textureCache)
//    {
//      if (textureCache == null)
//        throw new ArgumentNullException();
//      _viewportSize = new Size((double) bmp.PixelWidth, (double) bmp.PixelHeight);
//      _image = image;
//      _bmp = bmp;
//      _emptyStrideRow = emptyStrideRow;
//      _imageBuffer = imageBuffer;
//      _graphics2D = graphics2D;
//      _graphics2D.Rasterizer.reset();
//    }

//    public override RenderOperationLayers Layers
//    {
//      get
//      {
//        return _renderLayers;
//      }
//    }

//    public override Size ViewportSize
//    {
//      get
//      {
//        return _viewportSize;
//      }
//    }

//    public override unsafe void Dispose()
//    {
//      if (_viewportSize.Width == 0.0 || _viewportSize.Height == 0.0)
//        return;
//      _bmp.Lock();
//      fixed (byte* numPtr = &_imageBuffer.GetBuffer()[0])
//      {
//        byte* pointer = (byte*) _bmp.BackBuffer.ToPointer();
//        byte* srcPtr = numPtr;
//        int srcOffset = 0;
//        byte* dstPtr = pointer;
//        int dstOffset = 0;
//        Size viewportSize = _viewportSize;
//        double width = viewportSize.Width;
//        viewportSize = _viewportSize;
//        double height = viewportSize.Height;
//        int count = (int) (width * height * 4.0);
//        NativeMethods.CopyUnmanagedMemory(srcPtr, srcOffset, dstPtr, dstOffset, count);
//      }
//      _bmp.AddDirtyRect(new Int32Rect(0, 0, _bmp.PixelWidth, _bmp.PixelHeight));
//      _bmp.Unlock();
//      if (_bmp != _image.Source)
//        _image.Source = (ImageSource) _bmp;
//      foreach (IDisposable disposable in _resourcesToDispose)
//        disposable.Dispose();
//    }

//    public override IBrush2D CreateBrush(Color color, double opacity = 1.0, bool? alphaBlend = null)
//    {
//      return (IBrush2D) new HqBrush(color, true, opacity);
//    }

//    public override IBrush2D CreateBrush(Brush brush, double opacity = 1.0, TextureMappingMode mappingMode = TextureMappingMode.PerScreen)
//    {
//      if (brush == null)
//        return CreateBrush(Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0), 1.0, new bool?());
//      if (brush is SolidColorBrush)
//        return CreateBrush(((SolidColorBrush) brush).Color, opacity, new bool?(true));
//      return (IBrush2D) new TextureBrush(brush, mappingMode, TextureCache);
//    }

//    public override IPen2D CreatePen(Color color, bool antiAliasing, float strokeThickness, double opacity, double[] strokeDashArray = null, PenLineCap strokeEndLineCap = PenLineCap.Round)
//    {
//      return (IPen2D) new HqPen(color, strokeThickness, strokeEndLineCap, antiAliasing, opacity, strokeDashArray);
//    }

//    public override ISprite2D CreateSprite(FrameworkElement fe)
//    {
//      return (ISprite2D) new HqSprite2D(TextureCache.GetWriteableBitmapTexture(fe));
//    }

//    public override void Clear()
//    {
//      int num1 = 0;
//      while (true)
//      {
//        double num2 = (double) num1;
//        Size viewportSize = _viewportSize;
//        double height = viewportSize.Height;
//        if (num2 < height)
//        {
//          uint[] emptyStrideRow = _emptyStrideRow;
//          int srcOffset = 0;
//          byte[] buffer = _imageBuffer.GetBuffer();
//          double num3 = (double) (4 * num1);
//          viewportSize = _viewportSize;
//          double width = viewportSize.Width;
//          int dstOffset = (int) (num3 * width);
//          int count = 4 * _emptyStrideRow.Length;
//          Buffer.BlockCopy((Array) emptyStrideRow, srcOffset, (Array) buffer, dstOffset, count);
//          ++num1;
//        }
//        else
//          break;
//      }
//    }

//    public override void FillRectangle(IBrush2D brush, Point pt1, Point pt2, double gradientRotationAngle = 0.0)
//    {
//      if (pt1.X < 0.0 && pt2.X < 0.0 || pt1.Y < 0.0 && pt2.Y < 0.0)
//        return;
//      double y1 = pt1.Y;
//      Size viewportSize = ViewportSize;
//      double height1 = viewportSize.Height;
//      if (y1 > height1)
//      {
//        double y2 = pt2.Y;
//        viewportSize = ViewportSize;
//        double height2 = viewportSize.Height;
//        if (y2 > height2)
//          return;
//      }
//      double x1 = pt1.X;
//      viewportSize = ViewportSize;
//      double width1 = viewportSize.Width;
//      if (x1 > width1)
//      {
//        double x2 = pt2.X;
//        viewportSize = ViewportSize;
//        double width2 = viewportSize.Width;
//        if (x2 > width2)
//          return;
//      }
//      ClipRectangle(ref pt1, ref pt2);
//      _graphics2D.Rasterizer.reset();
//      Rect rect = new Rect(new Point(pt1.X, pt1.Y), new Point(pt2.X, pt2.Y));
//      _graphics2D.Rasterizer.add_path((IVertexSource) new RoundedRect(rect.Left, rect.Bottom, rect.Right, rect.Top, 0.0));
//      if (brush is TextureBrush)
//      {
//        Rect primitiveRect = new Rect(pt1, pt2);
//        TextureBrush textureBrush = (TextureBrush) brush;
//        byte[] textureArray = textureBrush.GetByteTexture(ViewportSize);
//        new ScanlineRenderer().render_scanlines_aa_solid(_graphics2D.DestImage, (IRasterizer) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, (Func<int, int, RGBA_Bytes>) ((x, y) =>
//        {
//          int consideringMappingMode = textureBrush.GetByteOffsetConsideringMappingMode(x, y, primitiveRect, gradientRotationAngle);
//          return new RGBA_Bytes((int) textureArray[consideringMappingMode + 2], (int) textureArray[consideringMappingMode + 1], (int) textureArray[consideringMappingMode], (int) textureArray[consideringMappingMode + 3]);
//        }));
//      }
//      else
//        new ScanlineRenderer().render_scanlines_aa_solid(_graphics2D.DestImage, (IRasterizer) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ToRgbaBytes(brush.Color));
//    }

//    public override void FillArea(IBrush2D brush, IEnumerable<Tuple<Point, Point>> lines, bool isVerticalChart = false, double gradientRotationAngle = 0.0)
//    {
//      foreach (IEnumerable<Tuple<Point, Point>> splitMultilineByGap in lines.SplitMultilineByGaps())
//      {
//        PathStorage pathStorage = new PathStorage();
//        _graphics2D.Rasterizer.reset();
//        Point[] array = ClipArea(splitMultilineByGap).ToArray<Point>();
//        if (array.Length < 2)
//          break;
//        pathStorage.MoveTo(array[0].X, array[0].Y);
//        for (int index = 1; index < array.Length; ++index)
//          pathStorage.LineTo(array[index].X, array[index].Y);
//        _graphics2D.Rasterizer.add_path((IVertexSource) pathStorage);
//        _graphics2D.Rasterizer.close_polygon();
//        if (brush is TextureBrush)
//        {
//          TextureBrush textureBrush = (TextureBrush) brush;
//          byte[] textureArray = textureBrush.GetByteTexture(ViewportSize);
//          new ScanlineRenderer().render_scanlines_aa_solid(_graphics2D.DestImage, (IRasterizer) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, (Func<int, int, RGBA_Bytes>) ((x, y) =>
//          {
//            int consideringMappingMode = textureBrush.GetByteOffsetNotConsideringMappingMode(x, y, gradientRotationAngle);
//            return new RGBA_Bytes((int) textureArray[consideringMappingMode + 2], (int) textureArray[consideringMappingMode + 1], (int) textureArray[consideringMappingMode], (int) textureArray[consideringMappingMode + 3]);
//          }));
//        }
//        else
//          new ScanlineRenderer().render_scanlines_aa_solid(_graphics2D.DestImage, (IRasterizer) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ToRgbaBytes(brush.Color));
//      }
//    }

//    public void FillTriangle(Point point, Point point1, Point point2, Color color)
//    {
//      PathStorage pathStorage = new PathStorage();
//      pathStorage.MoveTo(point.X, point.Y);
//      pathStorage.LineTo(point1.X, point1.Y);
//      pathStorage.LineTo(point2.X, point2.Y);
//      pathStorage.ClosePolygon();
//      _graphics2D.Rasterizer.add_path((IVertexSource) pathStorage);
//      new ScanlineRenderer().render_scanlines_aa_solid(_graphics2D.DestImage, (IRasterizer) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ToRgbaBytes(color));
//    }

//    public void DrawTriangle(Point point, Point point1, Point point2, float thickness, Color color)
//    {
//      double w = (double) thickness;
//      rasterizer_outline_aa rasterizerOutlineAa = new rasterizer_outline_aa((LineRenderer) new OutlineRenderer(_graphics2D.DestImage, new LineProfileAnitAlias(w, (IGammaFunction) new gamma_none())));
//      rasterizerOutlineAa.line_join(w > 2.0 ? rasterizer_outline_aa.outline_aa_join_e.outline_round_join : rasterizer_outline_aa.outline_aa_join_e.outline_no_join);
//      rasterizerOutlineAa.round_cap(w > 2.0);
//      PathStorage pathStorage = new PathStorage();
//      pathStorage.MoveTo(point.X, point.Y);
//      pathStorage.LineTo(point1.X, point1.Y);
//      pathStorage.LineTo(point2.X, point2.Y);
//      pathStorage.LineTo(point.X, point.Y);
//      RGBA_Bytes[] colors = new RGBA_Bytes[1]{ ToRgbaBytes(color) };
//      int[] path_id = new int[1];
//      rasterizerOutlineAa.RenderAllPaths((IVertexSource) pathStorage, colors, path_id, 1);
//    }

//    public override void DisposeResourceAfterDraw(IDisposable disposable)
//    {
//      if (disposable == null)
//        return;
//      _resourcesToDispose.Add(disposable);
//    }

//    private RGBA_Bytes ToRgbaBytes(Color color)
//    {
//      return new RGBA_Bytes((int) color.R, (int) color.G, (int) color.B, (int) color.A);
//    }

//    private int CoerceValues(ref int coord, ref double measure)
//    {
//      int num = 0;
//      if (coord < 0 && (double) coord + measure > 0.0)
//      {
//        num = -coord;
//        measure -= (double) num;
//        coord += num;
//      }
//      return num;
//    }

//    public override void DrawSprite(ISprite2D sprite, Rect srcRect, Point destPoint)
//    {
//      HqSprite2D hqSprite2D = sprite as HqSprite2D;
//      if (hqSprite2D == null)
//        throw new ArgumentException(string.Format("Input Sprite must be of type {0}", (object) typeof (HqSprite2D)));
//      ImageBuffer linkedImage = ((ImageProxy) _graphics2D.DestImage).LinkedImage as ImageBuffer;
//      BlenderBGRA blenderBgra = new BlenderBGRA();
//      BlenderPreMultBGRA blenderPreMultBgra = new BlenderPreMultBGRA();
//      byte[] buffer1 = hqSprite2D.GetBuffer();
//      byte[] buffer2 = linkedImage.GetBuffer();
//      int y = (int) destPoint.Y;
//      int x = (int) destPoint.X;
//      double width = (double) hqSprite2D.Width;
//      double height = (double) hqSprite2D.Height;
//      int i = CoerceValues(ref x, ref width);
//      int num1 = CoerceValues(ref y, ref height);
//      if (y < 0 || x < 0)
//        return;
//      for (int index1 = 0; (double) index1 < height && index1 + y < linkedImage.Height && x < linkedImage.Width; ++index1)
//      {
//        int bufferOffset1 = hqSprite2D.GetBufferOffsetXY(i, index1 + num1);
//        int num2 = linkedImage.GetBufferOffsetXY(x, index1 + y);
//        for (int index2 = 0; (double) index2 < width && index2 + x < linkedImage.Width; ++index2)
//        {
//          switch (buffer1[bufferOffset1 + 3])
//          {
//            case 0:
//              bufferOffset1 += 4;
//              num2 += 4;
//              break;
//            case byte.MaxValue:
//              byte[] numArray1 = buffer2;
//              int index3 = num2;
//              int num3 = index3 + 1;
//              byte[] numArray2 = buffer1;
//              int index4 = bufferOffset1;
//              int num4 = index4 + 1;
//              int num5 = (int) numArray2[index4];
//              numArray1[index3] = (byte) num5;
//              byte[] numArray3 = buffer2;
//              int index5 = num3;
//              int num6 = index5 + 1;
//              byte[] numArray4 = buffer1;
//              int index6 = num4;
//              int num7 = index6 + 1;
//              int num8 = (int) numArray4[index6];
//              numArray3[index5] = (byte) num8;
//              byte[] numArray5 = buffer2;
//              int index7 = num6;
//              int num9 = index7 + 1;
//              byte[] numArray6 = buffer1;
//              int index8 = num7;
//              int num10 = index8 + 1;
//              int num11 = (int) numArray6[index8];
//              numArray5[index7] = (byte) num11;
//              byte[] numArray7 = buffer2;
//              int index9 = num9;
//              num2 = index9 + 1;
//              byte[] numArray8 = buffer1;
//              int index10 = num10;
//              bufferOffset1 = index10 + 1;
//              int num12 = (int) numArray8[index10];
//              numArray7[index9] = (byte) num12;
//              break;
//            default:
//              RGBA_Bytes colorRgbaBytes = blenderBgra.PixelToColorRGBA_Bytes(buffer1, bufferOffset1);
//              int bufferOffset2;
//              byte[] pixelPointerXy = linkedImage.GetPixelPointerXY(x + index2, index1 + y, out bufferOffset2);
//              blenderPreMultBgra.BlendPixel(pixelPointerXy, bufferOffset2, colorRgbaBytes);
//              bufferOffset1 += 4;
//              num2 += 4;
//              break;
//          }
//        }
//      }
//    }

//    public void DrawSprite(Rect destRect, ISprite2D sprite)
//    {
//      HqSprite2D hqSprite2D = sprite as HqSprite2D;
//      if (hqSprite2D == null)
//        throw new ArgumentException(string.Format("Input Sprite must be of type {0}", (object) typeof (HqSprite2D)));
//      ImageBuffer linkedImage = ((ImageProxy) _graphics2D.DestImage).LinkedImage as ImageBuffer;
//      BlenderBGRA blenderBgra = new BlenderBGRA();
//      BlenderPreMultBGRA blenderPreMultBgra = new BlenderPreMultBGRA();
//      byte[] buffer1 = hqSprite2D.GetBuffer();
//      byte[] buffer2 = linkedImage.GetBuffer();
//      double width1 = (double) hqSprite2D.Width;
//      double height1 = (double) hqSprite2D.Height;
//      int left = (int) destRect.Left;
//      int top = (int) destRect.Top;
//      int width2 = (int) destRect.Width;
//      int height2 = (int) destRect.Height;
//      for (int index1 = 0; index1 < width2; ++index1)
//      {
//        for (int index2 = 0; index2 < height2; ++index2)
//        {
//          if (index1 + left >= 0 && index1 + left < linkedImage.Width && (index2 + top >= 0 && index2 + top < linkedImage.Height))
//          {
//            int i = (int) ((double) index1 / (double) width2 * width1);
//            int j = (int) ((double) index2 / (double) height2 * height1);
//            int bufferOffsetXy1 = hqSprite2D.GetBufferOffsetXY(i, j);
//            switch (buffer1[bufferOffsetXy1 + 3])
//            {
//              case 0:
//                continue;
//              case byte.MaxValue:
//                int bufferOffsetXy2 = linkedImage.GetBufferOffsetXY(index1 + left, index2 + top);
//                byte[] numArray1 = buffer2;
//                int index3 = bufferOffsetXy2;
//                int num1 = index3 + 1;
//                byte[] numArray2 = buffer1;
//                int index4 = bufferOffsetXy1;
//                int num2 = index4 + 1;
//                int num3 = (int) numArray2[index4];
//                numArray1[index3] = (byte) num3;
//                byte[] numArray3 = buffer2;
//                int index5 = num1;
//                int num4 = index5 + 1;
//                byte[] numArray4 = buffer1;
//                int index6 = num2;
//                int num5 = index6 + 1;
//                int num6 = (int) numArray4[index6];
//                numArray3[index5] = (byte) num6;
//                byte[] numArray5 = buffer2;
//                int index7 = num4;
//                int num7 = index7 + 1;
//                byte[] numArray6 = buffer1;
//                int index8 = num5;
//                int num8 = index8 + 1;
//                int num9 = (int) numArray6[index8];
//                numArray5[index7] = (byte) num9;
//                byte[] numArray7 = buffer2;
//                int index9 = num7;
//                int num10 = index9 + 1;
//                byte[] numArray8 = buffer1;
//                int index10 = num8;
//                int num11 = index10 + 1;
//                int num12 = (int) numArray8[index10];
//                numArray7[index9] = (byte) num12;
//                continue;
//              default:
//                RGBA_Bytes colorRgbaBytes = blenderBgra.PixelToColorRGBA_Bytes(buffer1, bufferOffsetXy1);
//                int bufferOffset;
//                byte[] pixelPointerXy = linkedImage.GetPixelPointerXY(index1 + left, index2 + top, out bufferOffset);
//                blenderPreMultBgra.BlendPixel(pixelPointerXy, bufferOffset, colorRgbaBytes);
//                continue;
//            }
//          }
//        }
//      }
//    }

//    public override void DrawSprites(ISprite2D sprite, Rect srcRect, IEnumerable<Point> points)
//    {
//      foreach (Point point in points)
//        DrawSprite(sprite, srcRect, point);
//    }

//    public override void DrawSprites(ISprite2D sprite2D, IEnumerable<Rect> dstRects)
//    {
//      foreach (Rect dstRect in dstRects)
//        DrawSprite(dstRect, sprite2D);
//    }

//    public override void DrawEllipse(IPen2D strokePen, IBrush2D fillBrush, Point center, double width, double height)
//    {
//      if (!IsInBounds(center))
//        return;
//      lock (_syncRoot)
//      {
//        _graphics2D.Rasterizer.reset();
//        Ellipse ellipse = new Ellipse(center.X, center.Y, width / 2.0, height / 2.0, 0, false);
//        _graphics2D.Rasterizer.add_path((IVertexSource) ellipse);
//        new ScanlineRenderer().render_scanlines_aa_solid(_graphics2D.DestImage, (IRasterizer) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ToRgbaBytes(fillBrush.Color));
//        Stroke stroke = new Stroke((IVertexSource) ellipse, 1.0);
//        stroke.line_join(LineJoin.Round);
//        stroke.inner_join(InnerJoin.Round);
//        stroke.line_cap(LineCap.Butt);
//        stroke.width((double) strokePen.StrokeThickness);
//        _graphics2D.Rasterizer.add_path((IVertexSource) stroke);
//        new ScanlineRenderer().render_scanlines_aa_solid(_graphics2D.DestImage, (IRasterizer) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ToRgbaBytes(strokePen.Color));
//        _graphics2D.Rasterizer.reset();
//      }
//    }

//    public override void DrawEllipses(IPen2D strokePen, IBrush2D fillBrush, IEnumerable<Point> centres, double width, double height)
//    {
//      foreach (Point centre in centres)
//        DrawEllipse(strokePen, fillBrush, centre, width, height);
//    }

//    public override void DrawPixelsVertically(int x0, int yStartBottom, int yEndTop, IList<int> pixelColorsArgb, double opacity, bool yAxisIsFlipped)
//    {
//      int num1 = Math.Max(yStartBottom, yEndTop);
//      yEndTop = Math.Min(yStartBottom, yEndTop);
//      yStartBottom = num1;
//      ImageBuffer linkedImage = ((ImageProxy) _graphics2D.DestImage).LinkedImage as ImageBuffer;
//      byte[] buffer = linkedImage.GetBuffer();
//      int height = linkedImage.Height;
//      if (yStartBottom == yEndTop)
//        return;
//      byte num2 = (byte) (opacity * 256.0);
//      for (int y = Math.Min(yStartBottom, height); y >= yEndTop && y >= 0; --y)
//      {
//        if (y >= 0 && y < height)
//        {
//          int index1 = (yStartBottom - y) * pixelColorsArgb.Count / (yStartBottom - yEndTop);
//          if (yAxisIsFlipped)
//            index1 = pixelColorsArgb.Count - 1 - index1;
//          if (index1 >= 0 && index1 < pixelColorsArgb.Count)
//          {
//            int num3 = pixelColorsArgb[index1];
//            int bufferOffsetXy = linkedImage.GetBufferOffsetXY(x0, y);
//            byte num4 = (byte) ((double) (((long) num3 & 4278190080L) >> 24) * opacity);
//            if (num4 < byte.MaxValue)
//            {
//              byte num5 = (byte) ((num3 & 16711680) >> 16);
//              byte num6 = (byte) ((num3 & 65280) >> 8);
//              byte num7 = (byte) (num3 & (int) byte.MaxValue);
//              byte num8 = buffer[bufferOffsetXy + 2];
//              byte num9 = buffer[bufferOffsetXy + 1];
//              byte num10 = buffer[bufferOffsetXy];
//              byte num11 = buffer[bufferOffsetXy + 3];
//              int num12 = (int) num5 * (int) num4 / (int) byte.MaxValue + (int) num8 * (int) num11 * ((int) byte.MaxValue - (int) num4) / 65025;
//              int num13 = (int) num6 * (int) num4 / (int) byte.MaxValue + (int) num9 * (int) num11 * ((int) byte.MaxValue - (int) num4) / 65025;
//              int num14 = (int) num7 * (int) num4 / (int) byte.MaxValue + (int) num10 * (int) num11 * ((int) byte.MaxValue - (int) num4) / 65025;
//              int num15 = (int) num4 + (int) num11 * ((int) byte.MaxValue - (int) num4) / (int) byte.MaxValue;
//              buffer[bufferOffsetXy + 2] = (byte) num12;
//              buffer[bufferOffsetXy + 1] = (byte) num13;
//              buffer[bufferOffsetXy] = (byte) num14;
//              buffer[bufferOffsetXy + 3] = (byte) num15;
//            }
//            else
//            {
//              byte[] numArray1 = buffer;
//              int index2 = bufferOffsetXy;
//              int num5 = index2 + 1;
//              int num6 = (int) (byte) (num3 & (int) byte.MaxValue);
//              numArray1[index2] = (byte) num6;
//              byte[] numArray2 = buffer;
//              int index3 = num5;
//              int num7 = index3 + 1;
//              int num8 = (int) (byte) ((num3 & 65280) >> 8);
//              numArray2[index3] = (byte) num8;
//              byte[] numArray3 = buffer;
//              int index4 = num7;
//              int index5 = index4 + 1;
//              int num9 = (int) (byte) ((num3 & 16711680) >> 16);
//              numArray3[index4] = (byte) num9;
//              buffer[index5] = (byte) (((long) num3 & 4278190080L) >> 24);
//            }
//          }
//        }
//      }
//    }

//    public override sealed IPathDrawingContext BeginLine(IPen2D pen, double startX, double startY)
//    {
//      return (IPathDrawingContext) new HqRenderContext.HqLineDrawingContext((HqPen) pen, this, startX, startY);
//    }

//    public override sealed IPathDrawingContext BeginPolygon(IBrush2D brush, double startX, double startY, double gradientRotationAngle = 0.0)
//    {
//      return (IPathDrawingContext) new HqRenderContext.HqFillDrawingContext(brush, this, startX, startY) { GradientRotationAngle = gradientRotationAngle };
//    }

//    private static void DrawPixel(ImageBuffer dest, byte[] buffer, int w, int h, int x1, int y1, Color color)
//    {
//      if (y1 >= h || y1 < 0 || (x1 >= w || x1 < 0))
//        return;
//      HqRenderContext.BlendPixel(dest, buffer, w, color, y1, x1);
//    }

//    private static void DrawLineBresenham(ImageBuffer dest, byte[] buffer, int w, int h, int x1, int y1, int x2, int y2, Color color)
//    {
//      if (y1 < 0 && y2 < 0 || y1 > h && y2 > h)
//        return;
//      if (x1 == x2 && y1 == y2)
//      {
//        HqRenderContext.DrawPixel(dest, buffer, w, h, x1, y1, color);
//      }
//      else
//      {
//        if (!WriteableBitmapExtensions.CohenSutherlandLineClip(new Rect(0.0, 0.0, (double) w, (double) h), ref x1, ref y1, ref x2, ref y2))
//          return;
//        int num1 = x2 - x1;
//        int num2 = y2 - y1;
//        int num3 = 0;
//        if (num1 < 0)
//        {
//          num1 = -num1;
//          num3 = -1;
//        }
//        else if (num1 > 0)
//          num3 = 1;
//        int num4 = 0;
//        if (num2 < 0)
//        {
//          num2 = -num2;
//          num4 = -1;
//        }
//        else if (num2 > 0)
//          num4 = 1;
//        int num5;
//        int num6;
//        int num7;
//        int num8;
//        int num9;
//        int num10;
//        if (num1 > num2)
//        {
//          num5 = num3;
//          num6 = 0;
//          num7 = num3;
//          num8 = num4;
//          num9 = num2;
//          num10 = num1;
//        }
//        else
//        {
//          num5 = 0;
//          num6 = num4;
//          num7 = num3;
//          num8 = num4;
//          num9 = num1;
//          num10 = num2;
//        }
//        int x = x1;
//        int y = y1;
//        int num11 = num10 >> 1;
//        if (y < h && y >= 0 && (x < w && x >= 0))
//          HqRenderContext.BlendPixel(dest, buffer, w, color, y, x);
//        for (int index = 0; index < num10; ++index)
//        {
//          num11 -= num9;
//          if (num11 < 0)
//          {
//            num11 += num10;
//            x += num7;
//            y += num8;
//          }
//          else
//          {
//            x += num5;
//            y += num6;
//          }
//          if (y < h && y >= 0 && (x < w && x >= 0))
//            HqRenderContext.BlendPixel(dest, buffer, w, color, y, x);
//        }
//      }
//    }

//    private static void BlendPixel(ImageBuffer dest, byte[] buffer, int w, Color color, int y, int x)
//    {
//      int index = (y * w + x) * 4;
//      byte a = color.A;
//      byte r = color.R;
//      byte g = color.G;
//      byte b = color.B;
//      if (color.A == byte.MaxValue)
//      {
//        buffer[index + 3] = a;
//        buffer[index + 2] = r;
//        buffer[index + 1] = g;
//        buffer[index] = b;
//      }
//      else
//      {
//        byte num1 = buffer[index + 3];
//        byte num2 = buffer[index + 2];
//        byte num3 = buffer[index + 1];
//        byte num4 = buffer[index];
//        int num5 = (int) num2 * (int) num1 / (int) byte.MaxValue + (int) r * (int) a * ((int) byte.MaxValue - (int) num1) / 65025;
//        int num6 = (int) num3 * (int) num1 / (int) byte.MaxValue + (int) g * (int) a * ((int) byte.MaxValue - (int) num1) / 65025;
//        int num7 = (int) num4 * (int) num1 / (int) byte.MaxValue + (int) b * (int) a * ((int) byte.MaxValue - (int) num1) / 65025;
//        int num8 = (int) num1 + (int) a * ((int) byte.MaxValue - (int) num1) / (int) byte.MaxValue;
//        buffer[index + 3] = (byte) num8;
//        buffer[index + 2] = (byte) num5;
//        buffer[index + 1] = (byte) num6;
//        buffer[index] = (byte) num7;
//      }
//    }

//    internal sealed class HqLineDrawingContext : IPathDrawingContext, IDisposable
//    {
//      private readonly HqRenderContext _context;
//      private HqPen _pen;
//      private double _lastX;
//      private double _lastY;
//      private Size _viewportSize;
//      private readonly ImageBuffer _destImage;
//      private bool _useAggMethod;
//      private PathStorage _ps;
//      private rasterizer_outline_aa _rasterizer;
//      private Stroke _pg;

//      public HqLineDrawingContext(HqPen pen, HqRenderContext context, double x, double y)
//      {
//        _context = context;
//        _viewportSize = _context.ViewportSize;
//        _destImage = ((ImageProxy) _context._graphics2D.DestImage).LinkedImage as ImageBuffer;
//        Begin((IPathColor) pen, x, y);
//      }

//      public IPathDrawingContext Begin(IPathColor pen, double x, double y)
//      {
//        _pen = (HqPen) pen;
//        _useAggMethod = _pen.Antialiased || (double) _pen.StrokeThickness > 1.0;
//        _lastX = x;
//        _lastY = y;
//        if (_useAggMethod)
//        {
//          _ps = new PathStorage();
//          if (_pen.HasDashes)
//          {
//            LineCap lc = _pen.StrokeEndLineCap == PenLineCap.Square ? LineCap.Square : LineCap.Round;
//            _pg = new Stroke((IVertexSource) _ps, (double) _pen.StrokeThickness);
//            _pg.line_join(LineJoin.Round);
//            _pg.inner_join(InnerJoin.Round);
//            _pg.line_cap(lc);
//            _context._graphics2D.Rasterizer.reset();
//          }
//          else
//          {
//            IGammaFunction gamma_function = !_pen.Antialiased ? (IGammaFunction) new gamma_threshold(0.5) : (IGammaFunction) new gamma_none();
//            double strokeThickness = (double) _pen.StrokeThickness;
//            _rasterizer = new rasterizer_outline_aa((LineRenderer) new OutlineRenderer(_context._graphics2D.DestImage, new LineProfileAnitAlias(strokeThickness, gamma_function)));
//            bool v = strokeThickness >= 2.0;
//            _rasterizer.line_join(v ? rasterizer_outline_aa.outline_aa_join_e.outline_round_join : rasterizer_outline_aa.outline_aa_join_e.outline_no_join);
//            _rasterizer.round_cap(v);
//          }
//          _ps.MoveTo(x, y);
//        }
//        return (IPathDrawingContext) this;
//      }

//      public IPathDrawingContext MoveTo(double x, double y)
//      {
//        if (!_pen.HasDashes)
//        {
//          LineToImplementation(x, y);
//          _lastX = x;
//          _lastY = y;
//          return (IPathDrawingContext) this;
//        }
//        Point pt1 = new Point(_lastX, _lastY);
//        Point pt2 = new Point(x, y);
//        RenderContextBase.ClipLine(ref pt1, ref pt2, _viewportSize);
//        DashSplitter dashSplitter = _context.DashSplitter;
//        dashSplitter.Reset(pt1, pt2, _viewportSize, (IDashSplittingContext) _pen);
//        while (dashSplitter.MoveNext())
//        {
//          LineD current = dashSplitter.Current;
//          End();
//          Begin((IPathColor) _pen, current.X1, current.Y1);
//          LineToImplementation(current.X2, current.Y2);
//        }
//        _lastX = x;
//        _lastY = y;
//        return (IPathDrawingContext) this;
//      }

//      private void LineToImplementation(double x, double y)
//      {
//        if (_useAggMethod)
//          _ps.LineTo(x, y);
//        else
//          HqRenderContext.DrawLineBresenham(_destImage, _destImage.GetBuffer(), (int) _viewportSize.Width, (int) _viewportSize.Height, (int) _lastX.ClipToInt(), (int) _lastY.ClipToInt(), (int) x.ClipToInt(), (int) y.ClipToInt(), _pen.Color);
//        _lastX = x;
//        _lastY = y;
//      }

//      public void End()
//      {
//        if (!_useAggMethod)
//          return;
//        if (_pen.HasDashes)
//        {
//          Graphics2D graphics2D = _context._graphics2D;
//          graphics2D.Rasterizer.add_path((IVertexSource) _pg);
//          if (!_pen.Antialiased)
//            graphics2D.Rasterizer.gamma((IGammaFunction) new gamma_threshold(0.5));
//          new ScanlineRenderer().render_scanlines_aa_solid(graphics2D.DestImage, (IRasterizer) graphics2D.Rasterizer, graphics2D.ScanlineCache, _context.ToRgbaBytes(_pen.Color));
//          if (_pen.Antialiased)
//            return;
//          graphics2D.Rasterizer.gamma((IGammaFunction) new gamma_none());
//        }
//        else
//          _rasterizer.RenderAllPaths((IVertexSource) _ps, new RGBA_Bytes[1]
//          {
//            _context.ToRgbaBytes(_pen.Color)
//          }, new int[1], 1);
//      }

//      void IDisposable.Dispose()
//      {
//        End();
//      }
//    }

//    internal sealed class HqFillDrawingContext : IPathDrawingContext, IDisposable
//    {
//      private IBrush2D _brush;
//      private readonly HqRenderContext _hqRenderContext;
//      private PathStorage _ps;

//      public HqFillDrawingContext(IBrush2D brush, HqRenderContext hqRenderContext, double startX, double startY)
//      {
//        _hqRenderContext = hqRenderContext;
//        Begin((IPathColor) brush, startX, startY);
//      }

//      public double GradientRotationAngle { get; set; }

//      public IPathDrawingContext Begin(IPathColor pen, double x, double y)
//      {
//        _brush = (IBrush2D) pen;
//        _ps = new PathStorage();
//        _hqRenderContext._graphics2D.Rasterizer.reset();
//        _ps.MoveTo(x, y);
//        return (IPathDrawingContext) this;
//      }

//      public IPathDrawingContext MoveTo(double x, double y)
//      {
//        _ps.LineTo(x, y);
//        return (IPathDrawingContext) this;
//      }

//      public void End()
//      {
//        _hqRenderContext._graphics2D.Rasterizer.add_path((IVertexSource) _ps);
//        _hqRenderContext._graphics2D.Rasterizer.close_polygon();
//        TextureBrush textureBrush = _brush as TextureBrush;
//        if (textureBrush != null)
//        {
//          byte[] textureArray = textureBrush.GetByteTexture(_hqRenderContext.ViewportSize);
//          new ScanlineRenderer().render_scanlines_aa_solid(_hqRenderContext._graphics2D.DestImage, (IRasterizer) _hqRenderContext._graphics2D.Rasterizer, _hqRenderContext._graphics2D.ScanlineCache, (Func<int, int, RGBA_Bytes>) ((x, y) =>
//          {
//            int consideringMappingMode = textureBrush.GetByteOffsetNotConsideringMappingMode(x, y, GradientRotationAngle);
//            return new RGBA_Bytes((int) textureArray[consideringMappingMode + 2], (int) textureArray[consideringMappingMode + 1], (int) textureArray[consideringMappingMode], (int) textureArray[consideringMappingMode + 3]);
//          }));
//        }
//        else
//          new ScanlineRenderer().render_scanlines_aa_solid(_hqRenderContext._graphics2D.DestImage, (IRasterizer) _hqRenderContext._graphics2D.Rasterizer, _hqRenderContext._graphics2D.ScanlineCache, _hqRenderContext.ToRgbaBytes(_brush.Color));
//      }

//      void IDisposable.Dispose()
//      {
//        End();
//      }
//    }

//    private class gamma_noaa : IGammaFunction
//    {
//      public double GetGamma(double x)
//      {
//        return 1.0;
//      }
//    }
//  }
//}

// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Rendering.HighQualityRasterizer.HqRenderContext
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MatterHackers.Agg;
using MatterHackers.Agg.Image;
using MatterHackers.Agg.VertexSource;
namespace fx.Xaml.Charting
{
    internal class HqRenderContext : RenderContextBase
    {
        private readonly RenderOperationLayers _renderLayers = new RenderOperationLayers();
        private readonly List<IDisposable> _resourcesToDispose = new List<IDisposable>();
        private object _syncRoot = new object();
        private readonly WriteableBitmap _bmp;
        private readonly uint[] _emptyStrideRow;
        protected readonly Graphics2D _graphics2D;
        private readonly System.Windows.Controls.Image _image;
        private readonly ImageBuffer _imageBuffer;
        protected readonly Size _viewportSize;

        private TextureCache TextureCache
        {
            get
            {
                return ( TextureCache ) _textureCache;
            }
        }

        public HqRenderContext( System.Windows.Controls.Image image, WriteableBitmap bmp, uint[ ] emptyStrideRow, ImageBuffer imageBuffer, Graphics2D graphics2D, TextureCacheBase textureCache )
          : base( textureCache )
        {
            if ( textureCache == null )
                throw new ArgumentNullException();
            _viewportSize = new Size( ( double ) bmp.PixelWidth, ( double ) bmp.PixelHeight );
            _image = image;
            _bmp = bmp;
            _emptyStrideRow = emptyStrideRow;
            _imageBuffer = imageBuffer;
            _graphics2D = graphics2D;
            _graphics2D.Rasterizer.reset();
        }

        public override RenderOperationLayers Layers
        {
            get
            {
                return _renderLayers;
            }
        }

        public override Size ViewportSize
        {
            get
            {
                return _viewportSize;
            }
        }

        public override unsafe void Dispose()
        {
            if ( _viewportSize.Width == 0.0 || _viewportSize.Height == 0.0 )
                return;
            _bmp.Lock();
            fixed ( byte* srcPtr = &_imageBuffer.GetBuffer()[ 0 ] )
            {
                byte* pointer = (byte*) _bmp.BackBuffer.ToPointer();
                int srcOffset = 0;
                byte* dstPtr = pointer;
                int dstOffset = 0;
                Size viewportSize = _viewportSize;
                double width = viewportSize.Width;
                viewportSize = _viewportSize;
                double height = viewportSize.Height;
                int count = (int) (width * height * 4.0);
                NativeMethods.CopyUnmanagedMemory( srcPtr, srcOffset, dstPtr, dstOffset, count );
            }
            _bmp.AddDirtyRect( new Int32Rect( 0, 0, _bmp.PixelWidth, _bmp.PixelHeight ) );
            _bmp.Unlock();
            if ( _bmp != _image.Source )
                _image.Source = ( ImageSource ) _bmp;
            foreach ( IDisposable disposable in _resourcesToDispose )
                disposable.Dispose();
        }

        public override IBrush2D CreateBrush( Color color, double opacity = 1.0, bool? alphaBlend = null )
        {
            return ( IBrush2D ) new HqBrush( color, true, opacity );
        }

        public override IBrush2D CreateBrush( Brush brush, double opacity = 1.0, TextureMappingMode mappingMode = TextureMappingMode.PerScreen )
        {
            if ( brush == null )
                return CreateBrush( Color.FromArgb( ( byte ) 0, ( byte ) 0, ( byte ) 0, ( byte ) 0 ), 1.0, new bool?() );
            if ( brush is SolidColorBrush )
                return CreateBrush( ( ( SolidColorBrush ) brush ).Color, opacity, new bool?( true ) );
            return ( IBrush2D ) new TextureBrush( brush, mappingMode, TextureCache );
        }

        public override IPen2D CreatePen( Color color, bool antiAliasing, float strokeThickness, double opacity, double[ ] strokeDashArray = null, PenLineCap strokeEndLineCap = PenLineCap.Round )
        {
            return ( IPen2D ) new HqPen( color, strokeThickness, strokeEndLineCap, antiAliasing, opacity, strokeDashArray );
        }

        public override ISprite2D CreateSprite( FrameworkElement fe )
        {
            return ( ISprite2D ) new HqSprite2D( TextureCache.GetWriteableBitmapTexture( fe ) );
        }

        public override void Clear()
        {
            int num1 = 0;
            while ( true )
            {
                double num2 = (double) num1;
                Size viewportSize = _viewportSize;
                double height = viewportSize.Height;
                if ( num2 < height )
                {
                    uint[] emptyStrideRow = _emptyStrideRow;
                    int srcOffset = 0;
                    byte[] buffer = _imageBuffer.GetBuffer();
                    double num3 = (double) (4 * num1);
                    viewportSize = _viewportSize;
                    double width = viewportSize.Width;
                    int dstOffset = (int) (num3 * width);
                    int count = 4 * _emptyStrideRow.Length;
                    Buffer.BlockCopy( ( Array ) emptyStrideRow, srcOffset, ( Array ) buffer, dstOffset, count );
                    ++num1;
                }
                else
                    break;
            }
        }

        public override void FillRectangle( IBrush2D brush, Point pt1, Point pt2, double gradientRotationAngle = 0.0 )
        {
            if ( pt1.X < 0.0 && pt2.X < 0.0 || pt1.Y < 0.0 && pt2.Y < 0.0 )
                return;
            double y1 = pt1.Y;
            Size viewportSize = ViewportSize;
            double height1 = viewportSize.Height;
            if ( y1 > height1 )
            {
                double y2 = pt2.Y;
                viewportSize = ViewportSize;
                double height2 = viewportSize.Height;
                if ( y2 > height2 )
                    return;
            }
            double x1 = pt1.X;
            viewportSize = ViewportSize;
            double width1 = viewportSize.Width;
            if ( x1 > width1 )
            {
                double x2 = pt2.X;
                viewportSize = ViewportSize;
                double width2 = viewportSize.Width;
                if ( x2 > width2 )
                    return;
            }
            ClipRectangle( ref pt1, ref pt2 );
            _graphics2D.Rasterizer.reset();
            Rect rect = new Rect(new Point(pt1.X, pt1.Y), new Point(pt2.X, pt2.Y));
            _graphics2D.Rasterizer.add_path( ( IVertexSource ) new RoundedRect( rect.Left, rect.Bottom, rect.Right, rect.Top, 0.0 ) );
            if ( brush is TextureBrush )
            {
                Rect primitiveRect = new Rect(pt1, pt2);
                TextureBrush textureBrush = (TextureBrush) brush;
                byte[] textureArray = textureBrush.GetByteTexture(ViewportSize);
                new ScanlineRenderer().render_scanlines_aa_solid( _graphics2D.DestImage, ( IRasterizer ) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ( Func<int, int, RGBA_Bytes> ) ( ( x, y ) =>
                {
                    int consideringMappingMode = textureBrush.GetByteOffsetConsideringMappingMode(x, y, primitiveRect, gradientRotationAngle);
                    return new RGBA_Bytes( ( int ) textureArray[ consideringMappingMode + 2 ], ( int ) textureArray[ consideringMappingMode + 1 ], ( int ) textureArray[ consideringMappingMode ], ( int ) textureArray[ consideringMappingMode + 3 ] );
                } ) );
            }
            else
                new ScanlineRenderer().render_scanlines_aa_solid( _graphics2D.DestImage, ( IRasterizer ) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ToRgbaBytes( brush.Color ) );
        }

        public override void FillArea( IBrush2D brush, IEnumerable<Tuple<Point, Point>> lines, bool isVerticalChart = false, double gradientRotationAngle = 0.0 )
        {
            foreach ( IEnumerable<Tuple<Point, Point>> splitMultilineByGap in lines.SplitMultilineByGaps() )
            {
                PathStorage pathStorage = new PathStorage();
                _graphics2D.Rasterizer.reset();
                Point[] array = ClipArea(splitMultilineByGap).ToArray<Point>();
                if ( array.Length < 2 )
                    break;
                pathStorage.MoveTo( array[ 0 ].X, array[ 0 ].Y );
                for ( int index = 1 ; index < array.Length ; ++index )
                    pathStorage.LineTo( array[ index ].X, array[ index ].Y );
                _graphics2D.Rasterizer.add_path( ( IVertexSource ) pathStorage );
                _graphics2D.Rasterizer.close_polygon();
                if ( brush is TextureBrush )
                {
                    TextureBrush textureBrush = (TextureBrush) brush;
                    byte[] textureArray = textureBrush.GetByteTexture(ViewportSize);
                    new ScanlineRenderer().render_scanlines_aa_solid( _graphics2D.DestImage, ( IRasterizer ) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ( Func<int, int, RGBA_Bytes> ) ( ( x, y ) =>
                    {
                        int consideringMappingMode = textureBrush.GetByteOffsetNotConsideringMappingMode(x, y, gradientRotationAngle);
                        return new RGBA_Bytes( ( int ) textureArray[ consideringMappingMode + 2 ], ( int ) textureArray[ consideringMappingMode + 1 ], ( int ) textureArray[ consideringMappingMode ], ( int ) textureArray[ consideringMappingMode + 3 ] );
                    } ) );
                }
                else
                    new ScanlineRenderer().render_scanlines_aa_solid( _graphics2D.DestImage, ( IRasterizer ) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ToRgbaBytes( brush.Color ) );
            }
        }

        public void FillTriangle( Point point, Point point1, Point point2, Color color )
        {
            PathStorage pathStorage = new PathStorage();
            pathStorage.MoveTo( point.X, point.Y );
            pathStorage.LineTo( point1.X, point1.Y );
            pathStorage.LineTo( point2.X, point2.Y );
            pathStorage.ClosePolygon();
            _graphics2D.Rasterizer.add_path( ( IVertexSource ) pathStorage );
            new ScanlineRenderer().render_scanlines_aa_solid( _graphics2D.DestImage, ( IRasterizer ) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ToRgbaBytes( color ) );
        }

        public void DrawTriangle( Point point, Point point1, Point point2, float thickness, Color color )
        {
            double w = (double) thickness;
            rasterizer_outline_aa rasterizerOutlineAa = new rasterizer_outline_aa((LineRenderer) new OutlineRenderer(_graphics2D.DestImage, new LineProfileAnitAlias(w, (IGammaFunction) new gamma_none())));
            rasterizerOutlineAa.line_join( w > 2.0 ? rasterizer_outline_aa.outline_aa_join_e.outline_round_join : rasterizer_outline_aa.outline_aa_join_e.outline_no_join );
            rasterizerOutlineAa.round_cap( w > 2.0 );
            PathStorage pathStorage = new PathStorage();
            pathStorage.MoveTo( point.X, point.Y );
            pathStorage.LineTo( point1.X, point1.Y );
            pathStorage.LineTo( point2.X, point2.Y );
            pathStorage.LineTo( point.X, point.Y );
            RGBA_Bytes[] colors = new RGBA_Bytes[1]
      {
        ToRgbaBytes(color)
      };
            int[] path_id = new int[1];
            rasterizerOutlineAa.RenderAllPaths( ( IVertexSource ) pathStorage, colors, path_id, 1 );
        }

        public override void DisposeResourceAfterDraw( IDisposable disposable )
        {
            if ( disposable == null )
                return;
            _resourcesToDispose.Add( disposable );
        }

        private RGBA_Bytes ToRgbaBytes( Color color )
        {
            return new RGBA_Bytes( ( int ) color.R, ( int ) color.G, ( int ) color.B, ( int ) color.A );
        }

        private int CoerceValues( ref int coord, ref double measure )
        {
            int num = 0;
            if ( coord < 0 && ( double ) coord + measure > 0.0 )
            {
                num = -coord;
                measure -= ( double ) num;
                coord += num;
            }
            return num;
        }

        public override void DrawSprite( ISprite2D sprite, Rect srcRect, Point destPoint )
        {
            HqSprite2D hqSprite2D = sprite as HqSprite2D;
            if ( hqSprite2D == null )
                throw new ArgumentException( string.Format( "Input Sprite must be of type {0}", ( object ) typeof( HqSprite2D ) ) );
            ImageBuffer linkedImage = ((ImageProxy) _graphics2D.DestImage).LinkedImage as ImageBuffer;
            BlenderBGRA blenderBgra = new BlenderBGRA();
            BlenderPreMultBGRA blenderPreMultBgra = new BlenderPreMultBGRA();
            byte[] buffer1 = hqSprite2D.GetBuffer();
            byte[] buffer2 = linkedImage.GetBuffer();
            int y = (int) destPoint.Y;
            int x = (int) destPoint.X;
            double width = (double) hqSprite2D.Width;
            double height = (double) hqSprite2D.Height;
            int i = CoerceValues(ref x, ref width);
            int num1 = CoerceValues(ref y, ref height);
            if ( y < 0 || x < 0 )
                return;
            for ( int index1 = 0 ; ( double ) index1 < height && index1 + y < linkedImage.Height && x < linkedImage.Width ; ++index1 )
            {
                int bufferOffset1 = hqSprite2D.GetBufferOffsetXY(i, index1 + num1);
                int num2 = linkedImage.GetBufferOffsetXY(x, index1 + y);
                for ( int index2 = 0 ; ( double ) index2 < width && index2 + x < linkedImage.Width ; ++index2 )
                {
                    switch ( buffer1[ bufferOffset1 + 3 ] )
                    {
                        case 0:
                            bufferOffset1 += 4;
                            num2 += 4;
                            break;
                        case byte.MaxValue:
                            byte[] numArray1 = buffer2;
                            int index3 = num2;
                            int num3 = index3 + 1;
                            byte[] numArray2 = buffer1;
                            int index4 = bufferOffset1;
                            int num4 = index4 + 1;
                            int num5 = (int) numArray2[index4];
                            numArray1[ index3 ] = ( byte ) num5;
                            byte[] numArray3 = buffer2;
                            int index5 = num3;
                            int num6 = index5 + 1;
                            byte[] numArray4 = buffer1;
                            int index6 = num4;
                            int num7 = index6 + 1;
                            int num8 = (int) numArray4[index6];
                            numArray3[ index5 ] = ( byte ) num8;
                            byte[] numArray5 = buffer2;
                            int index7 = num6;
                            int num9 = index7 + 1;
                            byte[] numArray6 = buffer1;
                            int index8 = num7;
                            int num10 = index8 + 1;
                            int num11 = (int) numArray6[index8];
                            numArray5[ index7 ] = ( byte ) num11;
                            byte[] numArray7 = buffer2;
                            int index9 = num9;
                            num2 = index9 + 1;
                            byte[] numArray8 = buffer1;
                            int index10 = num10;
                            bufferOffset1 = index10 + 1;
                            int num12 = (int) numArray8[index10];
                            numArray7[ index9 ] = ( byte ) num12;
                            break;
                        default:
                            RGBA_Bytes colorRgbaBytes = blenderBgra.PixelToColorRGBA_Bytes(buffer1, bufferOffset1);
                            int bufferOffset2;
                            byte[] pixelPointerXy = linkedImage.GetPixelPointerXY(x + index2, index1 + y, out bufferOffset2);
                            blenderPreMultBgra.BlendPixel( pixelPointerXy, bufferOffset2, colorRgbaBytes );
                            bufferOffset1 += 4;
                            num2 += 4;
                            break;
                    }
                }
            }
        }

        public void DrawSprite( Rect destRect, ISprite2D sprite )
        {
            HqSprite2D hqSprite2D = sprite as HqSprite2D;
            if ( hqSprite2D == null )
                throw new ArgumentException( string.Format( "Input Sprite must be of type {0}", ( object ) typeof( HqSprite2D ) ) );
            ImageBuffer linkedImage = ((ImageProxy) _graphics2D.DestImage).LinkedImage as ImageBuffer;
            BlenderBGRA blenderBgra = new BlenderBGRA();
            BlenderPreMultBGRA blenderPreMultBgra = new BlenderPreMultBGRA();
            byte[] buffer1 = hqSprite2D.GetBuffer();
            byte[] buffer2 = linkedImage.GetBuffer();
            double width1 = (double) hqSprite2D.Width;
            double height1 = (double) hqSprite2D.Height;
            int left = (int) destRect.Left;
            int top = (int) destRect.Top;
            int width2 = (int) destRect.Width;
            int height2 = (int) destRect.Height;
            for ( int index1 = 0 ; index1 < width2 ; ++index1 )
            {
                for ( int index2 = 0 ; index2 < height2 ; ++index2 )
                {
                    if ( index1 + left >= 0 && index1 + left < linkedImage.Width && ( index2 + top >= 0 && index2 + top < linkedImage.Height ) )
                    {
                        int i = (int) ((double) index1 / (double) width2 * width1);
                        int j = (int) ((double) index2 / (double) height2 * height1);
                        int bufferOffsetXy1 = hqSprite2D.GetBufferOffsetXY(i, j);
                        switch ( buffer1[ bufferOffsetXy1 + 3 ] )
                        {
                            case 0:
                                continue;
                            case byte.MaxValue:
                                int bufferOffsetXy2 = linkedImage.GetBufferOffsetXY(index1 + left, index2 + top);
                                byte[] numArray1 = buffer2;
                                int index3 = bufferOffsetXy2;
                                int num1 = index3 + 1;
                                byte[] numArray2 = buffer1;
                                int index4 = bufferOffsetXy1;
                                int num2 = index4 + 1;
                                int num3 = (int) numArray2[index4];
                                numArray1[ index3 ] = ( byte ) num3;
                                byte[] numArray3 = buffer2;
                                int index5 = num1;
                                int num4 = index5 + 1;
                                byte[] numArray4 = buffer1;
                                int index6 = num2;
                                int num5 = index6 + 1;
                                int num6 = (int) numArray4[index6];
                                numArray3[ index5 ] = ( byte ) num6;
                                byte[] numArray5 = buffer2;
                                int index7 = num4;
                                int num7 = index7 + 1;
                                byte[] numArray6 = buffer1;
                                int index8 = num5;
                                int num8 = index8 + 1;
                                int num9 = (int) numArray6[index8];
                                numArray5[ index7 ] = ( byte ) num9;
                                byte[] numArray7 = buffer2;
                                int index9 = num7;
                                int num10 = index9 + 1;
                                byte[] numArray8 = buffer1;
                                int index10 = num8;
                                int num11 = index10 + 1;
                                int num12 = (int) numArray8[index10];
                                numArray7[ index9 ] = ( byte ) num12;
                                continue;
                            default:
                                RGBA_Bytes colorRgbaBytes = blenderBgra.PixelToColorRGBA_Bytes(buffer1, bufferOffsetXy1);
                                int bufferOffset;
                                byte[] pixelPointerXy = linkedImage.GetPixelPointerXY(index1 + left, index2 + top, out bufferOffset);
                                blenderPreMultBgra.BlendPixel( pixelPointerXy, bufferOffset, colorRgbaBytes );
                                continue;
                        }
                    }
                }
            }
        }

        public override void DrawSprites( ISprite2D sprite, Rect srcRect, IEnumerable<Point> points )
        {
            foreach ( Point point in points )
                DrawSprite( sprite, srcRect, point );
        }

        public override void DrawSprites( ISprite2D sprite2D, IEnumerable<Rect> dstRects )
        {
            foreach ( Rect dstRect in dstRects )
                DrawSprite( dstRect, sprite2D );
        }

        public override void DrawEllipse( IPen2D strokePen, IBrush2D fillBrush, Point center, double width, double height )
        {
            if ( !IsInBounds( center ) )
                return;
            lock ( _syncRoot )
            {
                _graphics2D.Rasterizer.reset();
                Ellipse ellipse = new Ellipse(center.X, center.Y, width / 2.0, height / 2.0, 0, false);
                _graphics2D.Rasterizer.add_path( ( IVertexSource ) ellipse );
                new ScanlineRenderer().render_scanlines_aa_solid( _graphics2D.DestImage, ( IRasterizer ) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ToRgbaBytes( fillBrush.Color ) );
                Stroke stroke = new Stroke((IVertexSource) ellipse, 1.0);
                stroke.line_join( LineJoin.Round );
                stroke.inner_join( InnerJoin.Round );
                stroke.line_cap( LineCap.Butt );
                stroke.width( ( double ) strokePen.StrokeThickness );
                _graphics2D.Rasterizer.add_path( ( IVertexSource ) stroke );
                new ScanlineRenderer().render_scanlines_aa_solid( _graphics2D.DestImage, ( IRasterizer ) _graphics2D.Rasterizer, _graphics2D.ScanlineCache, ToRgbaBytes( strokePen.Color ) );
                _graphics2D.Rasterizer.reset();
            }
        }

        public override void DrawEllipses( IPen2D strokePen, IBrush2D fillBrush, IEnumerable<Point> centres, double width, double height )
        {
            foreach ( Point centre in centres )
                DrawEllipse( strokePen, fillBrush, centre, width, height );
        }

        public override void DrawPixelsVertically( int x0, int yStartBottom, int yEndTop, IList<int> pixelColorsArgb, double opacity, bool yAxisIsFlipped )
        {
            int num1 = Math.Max(yStartBottom, yEndTop);
            yEndTop = Math.Min( yStartBottom, yEndTop );
            yStartBottom = num1;
            ImageBuffer linkedImage = ((ImageProxy) _graphics2D.DestImage).LinkedImage as ImageBuffer;
            byte[] buffer = linkedImage.GetBuffer();
            int height = linkedImage.Height;
            if ( yStartBottom == yEndTop )
                return;
            for ( int y = Math.Min( yStartBottom, height ) ; y >= yEndTop && y >= 0 ; --y )
            {
                if ( y >= 0 && y < height )
                {
                    int index1 = (yStartBottom - y) * pixelColorsArgb.Count / (yStartBottom - yEndTop);
                    if ( yAxisIsFlipped )
                        index1 = pixelColorsArgb.Count - 1 - index1;
                    if ( index1 >= 0 && index1 < pixelColorsArgb.Count )
                    {
                        int num2 = pixelColorsArgb[index1];
                        int bufferOffsetXy = linkedImage.GetBufferOffsetXY(x0, y);
                        byte num3 = (byte) ((double) (((long) num2 & 4278190080L) >> 24) * opacity);
                        if ( num3 < byte.MaxValue )
                        {
                            int num4 = (int) (byte) ((num2 & 16711680) >> 16);
                            byte num5 = (byte) ((num2 & 65280) >> 8);
                            byte num6 = (byte) (num2 & (int) byte.MaxValue);
                            byte num7 = buffer[bufferOffsetXy + 2];
                            byte num8 = buffer[bufferOffsetXy + 1];
                            byte num9 = buffer[bufferOffsetXy];
                            byte num10 = buffer[bufferOffsetXy + 3];
                            int num11 = (int) num3;
                            int num12 = num4 * num11 / (int) byte.MaxValue + (int) num7 * (int) num10 * ((int) byte.MaxValue - (int) num3) / 65025;
                            int num13 = (int) num5 * (int) num3 / (int) byte.MaxValue + (int) num8 * (int) num10 * ((int) byte.MaxValue - (int) num3) / 65025;
                            int num14 = (int) num6 * (int) num3 / (int) byte.MaxValue + (int) num9 * (int) num10 * ((int) byte.MaxValue - (int) num3) / 65025;
                            int num15 = (int) num3 + (int) num10 * ((int) byte.MaxValue - (int) num3) / (int) byte.MaxValue;
                            buffer[ bufferOffsetXy + 2 ] = ( byte ) num12;
                            buffer[ bufferOffsetXy + 1 ] = ( byte ) num13;
                            buffer[ bufferOffsetXy ] = ( byte ) num14;
                            buffer[ bufferOffsetXy + 3 ] = ( byte ) num15;
                        }
                        else
                        {
                            byte[] numArray1 = buffer;
                            int index2 = bufferOffsetXy;
                            int num4 = index2 + 1;
                            int num5 = (int) (byte) (num2 & (int) byte.MaxValue);
                            numArray1[ index2 ] = ( byte ) num5;
                            byte[] numArray2 = buffer;
                            int index3 = num4;
                            int num6 = index3 + 1;
                            int num7 = (int) (byte) ((num2 & 65280) >> 8);
                            numArray2[ index3 ] = ( byte ) num7;
                            byte[] numArray3 = buffer;
                            int index4 = num6;
                            int index5 = index4 + 1;
                            int num8 = (int) (byte) ((num2 & 16711680) >> 16);
                            numArray3[ index4 ] = ( byte ) num8;
                            buffer[ index5 ] = ( byte ) ( ( ( long ) num2 & 4278190080L ) >> 24 );
                        }
                    }
                }
            }
        }

        public override sealed IPathDrawingContext BeginLine( IPen2D pen, double startX, double startY )
        {
            return ( IPathDrawingContext ) new HqRenderContext.HqLineDrawingContext( ( HqPen ) pen, this, startX, startY );
        }

        public override sealed IPathDrawingContext BeginPolygon( IBrush2D brush, double startX, double startY, double gradientRotationAngle = 0.0 )
        {
            return ( IPathDrawingContext ) new HqRenderContext.HqFillDrawingContext( brush, this, startX, startY )
            {
                GradientRotationAngle = gradientRotationAngle
            };
        }

        private static void DrawPixel( ImageBuffer dest, byte[ ] buffer, int w, int h, int x1, int y1, Color color )
        {
            if ( y1 >= h || y1 < 0 || ( x1 >= w || x1 < 0 ) )
                return;
            HqRenderContext.BlendPixel( dest, buffer, w, color, y1, x1 );
        }

        private static void DrawLineBresenham( ImageBuffer dest, byte[ ] buffer, int w, int h, int x1, int y1, int x2, int y2, Color color )
        {
            if ( y1 < 0 && y2 < 0 || y1 > h && y2 > h )
                return;
            if ( x1 == x2 && y1 == y2 )
            {
                HqRenderContext.DrawPixel( dest, buffer, w, h, x1, y1, color );
            }
            else
            {
                if ( !WriteableBitmapExtensions.CohenSutherlandLineClip( new Rect( 0.0, 0.0, ( double ) w, ( double ) h ), ref x1, ref y1, ref x2, ref y2 ) )
                    return;
                int num1 = x2 - x1;
                int num2 = y2 - y1;
                int num3 = 0;
                if ( num1 < 0 )
                {
                    num1 = -num1;
                    num3 = -1;
                }
                else if ( num1 > 0 )
                    num3 = 1;
                int num4 = 0;
                if ( num2 < 0 )
                {
                    num2 = -num2;
                    num4 = -1;
                }
                else if ( num2 > 0 )
                    num4 = 1;
                int num5;
                int num6;
                int num7;
                int num8;
                int num9;
                int num10;
                if ( num1 > num2 )
                {
                    num5 = num3;
                    num6 = 0;
                    num7 = num3;
                    num8 = num4;
                    num9 = num2;
                    num10 = num1;
                }
                else
                {
                    num5 = 0;
                    num6 = num4;
                    num7 = num3;
                    num8 = num4;
                    num9 = num1;
                    num10 = num2;
                }
                int x = x1;
                int y = y1;
                int num11 = num10 >> 1;
                if ( y < h && y >= 0 && ( x < w && x >= 0 ) )
                    HqRenderContext.BlendPixel( dest, buffer, w, color, y, x );
                for ( int index = 0 ; index < num10 ; ++index )
                {
                    num11 -= num9;
                    if ( num11 < 0 )
                    {
                        num11 += num10;
                        x += num7;
                        y += num8;
                    }
                    else
                    {
                        x += num5;
                        y += num6;
                    }
                    if ( y < h && y >= 0 && ( x < w && x >= 0 ) )
                        HqRenderContext.BlendPixel( dest, buffer, w, color, y, x );
                }
            }
        }

        private static void BlendPixel( ImageBuffer dest, byte[ ] buffer, int w, Color color, int y, int x )
        {
            int index = (y * w + x) * 4;
            byte a = color.A;
            byte r = color.R;
            byte g = color.G;
            byte b = color.B;
            if ( color.A == byte.MaxValue )
            {
                buffer[ index + 3 ] = a;
                buffer[ index + 2 ] = r;
                buffer[ index + 1 ] = g;
                buffer[ index ] = b;
            }
            else
            {
                byte num1 = buffer[index + 3];
                int num2 = (int) buffer[index + 2];
                byte num3 = buffer[index + 1];
                byte num4 = buffer[index];
                int num5 = (int) num1;
                int num6 = num2 * num5 / (int) byte.MaxValue + (int) r * (int) a * ((int) byte.MaxValue - (int) num1) / 65025;
                int num7 = (int) num3 * (int) num1 / (int) byte.MaxValue + (int) g * (int) a * ((int) byte.MaxValue - (int) num1) / 65025;
                int num8 = (int) num4 * (int) num1 / (int) byte.MaxValue + (int) b * (int) a * ((int) byte.MaxValue - (int) num1) / 65025;
                int num9 = (int) num1 + (int) a * ((int) byte.MaxValue - (int) num1) / (int) byte.MaxValue;
                buffer[ index + 3 ] = ( byte ) num9;
                buffer[ index + 2 ] = ( byte ) num6;
                buffer[ index + 1 ] = ( byte ) num7;
                buffer[ index ] = ( byte ) num8;
            }
        }

        internal sealed class HqLineDrawingContext : IPathDrawingContext, IDisposable
        {
            private readonly HqRenderContext _context;
            private HqPen _pen;
            private double _lastX;
            private double _lastY;
            private Size _viewportSize;
            private readonly ImageBuffer _destImage;
            private bool _useAggMethod;
            private PathStorage _ps;
            private rasterizer_outline_aa _rasterizer;
            private Stroke _pg;

            public HqLineDrawingContext( HqPen pen, HqRenderContext context, double x, double y )
            {
                _context = context;
                _viewportSize = _context.ViewportSize;
                _destImage = ( ( ImageProxy ) _context._graphics2D.DestImage ).LinkedImage as ImageBuffer;
                Begin( ( IPathColor ) pen, x, y );
            }

            public IPathDrawingContext Begin( IPathColor pen, double x, double y )
            {
                _pen = ( HqPen ) pen;
                _useAggMethod = _pen.Antialiased || ( double ) _pen.StrokeThickness > 1.0;
                _lastX = x;
                _lastY = y;
                if ( _useAggMethod )
                {
                    _ps = new PathStorage();
                    if ( _pen.HasDashes )
                    {
                        LineCap lc = _pen.StrokeEndLineCap == PenLineCap.Square ? LineCap.Square : LineCap.Round;
                        _pg = new Stroke( ( IVertexSource ) _ps, ( double ) _pen.StrokeThickness );
                        _pg.line_join( LineJoin.Round );
                        _pg.inner_join( InnerJoin.Round );
                        _pg.line_cap( lc );
                        _context._graphics2D.Rasterizer.reset();
                    }
                    else
                    {
                        IGammaFunction gamma_function = !_pen.Antialiased ? (IGammaFunction) new gamma_threshold(0.5) : (IGammaFunction) new gamma_none();
                        double strokeThickness = (double) _pen.StrokeThickness;
                        _rasterizer = new rasterizer_outline_aa( ( LineRenderer ) new OutlineRenderer( _context._graphics2D.DestImage, new LineProfileAnitAlias( strokeThickness, gamma_function ) ) );
                        bool v = strokeThickness >= 2.0;
                        _rasterizer.line_join( v ? rasterizer_outline_aa.outline_aa_join_e.outline_round_join : rasterizer_outline_aa.outline_aa_join_e.outline_no_join );
                        _rasterizer.round_cap( v );
                    }
                    _ps.MoveTo( x, y );
                }
                return ( IPathDrawingContext ) this;
            }

            public IPathDrawingContext MoveTo( double x, double y )
            {
                if ( !_pen.HasDashes )
                {
                    LineToImplementation( x, y );
                    _lastX = x;
                    _lastY = y;
                    return ( IPathDrawingContext ) this;
                }
                Point pt1 = new Point(_lastX, _lastY);
                Point pt2 = new Point(x, y);
                RenderContextBase.ClipLine( ref pt1, ref pt2, _viewportSize );
                DashSplitter dashSplitter = _context.DashSplitter;
                dashSplitter.Reset( pt1, pt2, _viewportSize, ( IDashSplittingContext ) _pen );
                while ( dashSplitter.MoveNext() )
                {
                    LineD current = dashSplitter.Current;
                    End();
                    Begin( ( IPathColor ) _pen, current.X1, current.Y1 );
                    LineToImplementation( current.X2, current.Y2 );
                }
                _lastX = x;
                _lastY = y;
                return ( IPathDrawingContext ) this;
            }

            private void LineToImplementation( double x, double y )
            {
                if ( _useAggMethod )
                    _ps.LineTo( x, y );
                else
                    HqRenderContext.DrawLineBresenham( _destImage, _destImage.GetBuffer(), ( int ) _viewportSize.Width, ( int ) _viewportSize.Height, ( int ) _lastX.ClipToInt(), ( int ) _lastY.ClipToInt(), ( int ) x.ClipToInt(), ( int ) y.ClipToInt(), _pen.Color );
                _lastX = x;
                _lastY = y;
            }

            public void End()
            {
                if ( !_useAggMethod )
                    return;
                if ( _pen.HasDashes )
                {
                    Graphics2D graphics2D = _context._graphics2D;
                    graphics2D.Rasterizer.add_path( ( IVertexSource ) _pg );
                    if ( !_pen.Antialiased )
                        graphics2D.Rasterizer.gamma( ( IGammaFunction ) new gamma_threshold( 0.5 ) );
                    new ScanlineRenderer().render_scanlines_aa_solid( graphics2D.DestImage, ( IRasterizer ) graphics2D.Rasterizer, graphics2D.ScanlineCache, _context.ToRgbaBytes( _pen.Color ) );
                    if ( _pen.Antialiased )
                        return;
                    graphics2D.Rasterizer.gamma( ( IGammaFunction ) new gamma_none() );
                }
                else
                    _rasterizer.RenderAllPaths( ( IVertexSource ) _ps, new RGBA_Bytes[ 1 ]
                    {
            _context.ToRgbaBytes(_pen.Color)
                    }, new int[ 1 ], 1 );
            }

            void IDisposable.Dispose()
            {
                End();
            }
        }

        internal sealed class HqFillDrawingContext : IPathDrawingContext, IDisposable
        {
            private IBrush2D _brush;
            private readonly HqRenderContext _hqRenderContext;
            private PathStorage _ps;

            public HqFillDrawingContext( IBrush2D brush, HqRenderContext hqRenderContext, double startX, double startY )
            {
                _hqRenderContext = hqRenderContext;
                Begin( ( IPathColor ) brush, startX, startY );
            }

            public double GradientRotationAngle
            {
                get; set;
            }

            public IPathDrawingContext Begin( IPathColor pen, double x, double y )
            {
                _brush = ( IBrush2D ) pen;
                _ps = new PathStorage();
                _hqRenderContext._graphics2D.Rasterizer.reset();
                _ps.MoveTo( x, y );
                return ( IPathDrawingContext ) this;
            }

            public IPathDrawingContext MoveTo( double x, double y )
            {
                _ps.LineTo( x, y );
                return ( IPathDrawingContext ) this;
            }

            public void End()
            {
                _hqRenderContext._graphics2D.Rasterizer.add_path( ( IVertexSource ) _ps );
                _hqRenderContext._graphics2D.Rasterizer.close_polygon();
                TextureBrush textureBrush = _brush as TextureBrush;
                if ( textureBrush != null )
                {
                    byte[] textureArray = textureBrush.GetByteTexture(_hqRenderContext.ViewportSize);
                    new ScanlineRenderer().render_scanlines_aa_solid( _hqRenderContext._graphics2D.DestImage, ( IRasterizer ) _hqRenderContext._graphics2D.Rasterizer, _hqRenderContext._graphics2D.ScanlineCache, ( Func<int, int, RGBA_Bytes> ) ( ( x, y ) =>
                    {
                        int consideringMappingMode = textureBrush.GetByteOffsetNotConsideringMappingMode(x, y, GradientRotationAngle);
                        return new RGBA_Bytes( ( int ) textureArray[ consideringMappingMode + 2 ], ( int ) textureArray[ consideringMappingMode + 1 ], ( int ) textureArray[ consideringMappingMode ], ( int ) textureArray[ consideringMappingMode + 3 ] );
                    } ) );
                }
                else
                    new ScanlineRenderer().render_scanlines_aa_solid( _hqRenderContext._graphics2D.DestImage, ( IRasterizer ) _hqRenderContext._graphics2D.Rasterizer, _hqRenderContext._graphics2D.ScanlineCache, _hqRenderContext.ToRgbaBytes( _brush.Color ) );
            }

            void IDisposable.Dispose()
            {
                End();
            }
        }

        private class gamma_noaa : IGammaFunction
        {
            public double GetGamma( double x )
            {
                return 1.0;
            }
        }
    }
}

