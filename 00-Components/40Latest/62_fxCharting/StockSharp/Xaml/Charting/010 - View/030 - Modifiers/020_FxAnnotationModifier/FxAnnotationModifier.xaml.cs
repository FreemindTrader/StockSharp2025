//// Decompiled with JetBrains decompiler
//// Type: -.AnnotationModifier
//// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
//// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

//using DevExpress.Xpf.Bars;
//using DevExpress.Xpf.Editors.Helpers;
//using Ecng.Collections;
//using Ecng.Common;
//using Ecng.Xaml;
//using SciChart.Charting.Visuals.Annotations;
//using SciChart.Core.Utility.Mouse;
//using StockSharp.BusinessEntities;
//using StockSharp.Charting;
//using StockSharp.Localization;
//using StockSharp.Messages;
//using StockSharp.Xaml;
//using StockSharp.Xaml.Charting;
//using StockSharp.Xaml.Charting.CustomAnnotations;
//using StockSharp.Xaml.Charting.Ultrachart;
//using StockSharp.Xaml.Charting.Visuals.Annotations;
//using System;
//using System.CodeDom.Compiler;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Markup;
//using System.Windows.Media;

//#nullable enable
//namespace StockSharp.Xaml.Charting;

//public partial class AnnotationModifier :
//  fxAnnotationCreationModifier,
//  IComponentConnector
//{

//    private sealed class SomeClass398
//    {
//        public AnnotationModifier _variableSome3535;
//        public AnnotationBase _AnnotationBase11;
//        public DelegateCommand<AnnotationBase> _DelegateCommand_0001;

//        public void Method001(
//          AnnotationBase _param1 )
//        {
//            this._variableSome3535._annotationCollection.Remove( ( IAnnotation ) _param1 );
//            ChartAnnotation chartAnnotation;
//            if ( !( ( KeyedCollection<AnnotationBase, ChartAnnotation> ) this._variableSome3535._baseToAnnotationPair ).TryGetValue( _param1, out chartAnnotation ) )
//                return;
//            ( ( KeyedCollection<AnnotationBase, ChartAnnotation> ) this._variableSome3535._baseToAnnotationPair ).Remove( _param1 );
//            ( ( ICollection<IChartElement> ) this._variableSome3535._chartArea.Elements ).Remove( ( IChartElement ) chartAnnotation );
//            this._variableSome3535.ChartArea?.InvokeAnnotationDeletedEvent( chartAnnotation );
//        }

//        public void Method002( AnnotationBase _param1 )
//        {
//            AnnotationModifier.AnotherSealedClass003 wd3zPhu0dS2ZqhuzuE = new AnnotationModifier.AnotherSealedClass003();
//            wd3zPhu0dS2ZqhuzuE._public_ActiveOrderAnnotation_083 = _param1;
//            UltrachartAnnotationEditor m53T5BwgtpbkV9ZEjd = this._variableSome3535.GetAnnotationEditor();
//            m53T5BwgtpbkV9ZEjd.IsOpen = false;
//            CollectionHelper.ForEach<IAnnotation>( this._variableSome3535.ParentSurface.get_Annotations().Where<IAnnotation>( new Func<IAnnotation, bool>( wd3zPhu0dS2ZqhuzuE.Method009) ), AnnotationModifier.SomeClass34343383._Action_IAnnotation_003 ?? ( AnnotationModifier.SomeClass34343383._Action_IAnnotation_003 = new Action<IAnnotation>( AnnotationModifier.SomeClass34343383.SomeMethond0343.Setset_IsSelected_false_ ) ));
//            wd3zPhu0dS2ZqhuzuE._public_ActiveOrderAnnotation_083.IsSelected = true;
//            m53T5BwgtpbkV9ZEjd.PlacementTarget = ( UIElement ) wd3zPhu0dS2ZqhuzuE._public_ActiveOrderAnnotation_083;
//            m53T5BwgtpbkV9ZEjd.IsOpen = true;
//        }

//        public void Method003(
//          object _param1,
//          KeyEventArgs _param2 )
//        {
//            if ( !this._AnnotationBase11.IsSelected || Keyboard.Modifiers != null )
//                return;
//            if ( _param2.Key == 32 /*0x20*/)
//            {
//                this._DelegateCommand_0001.TryExecute( ( object ) this._AnnotationBase11 );
//            }
//            else
//            {
//                if ( _param2.Key != 13 || !( this._AnnotationBase11 is TextAnnotation zLxiKoA ) )
//                    return;
//                zLxiKoA.RemoveFocusFromInputTextArea();
//            }
//        }

//        public void Method004(
//          object _param1,
//          EventArgs _param2 )
//        {
//            Keyboard.Focus( ( IInputElement ) this._AnnotationBase11 );
//        }

//        public void Method005(
//          object _param1,
//          MouseButtonEventArgs _param2 )
//        {
//            Keyboard.Focus( ( IInputElement ) this._AnnotationBase11 );
//        }

//        public void Method006(
//          object _param1,
//          EventArgs _param2 )
//        {
//            this._variableSome3535.GetAnnotationEditor().IsOpen = false;
//        }

//        public void Method007(
//          object _param1,
//          EventArgs _param2 )
//        {
//            this._variableSome3535._isUpdating = true;
//        }

//        public void Method008(
//          object _param1,
//          EventArgs _param2 )
//        {
//            AnnotationBase annotationBase = (AnnotationBase) _param1;
//            try
//            {
//                UltrachartAnnotationEditor m53T5BwgtpbkV9ZEjd = this._variableSome3535.GetAnnotationEditor();
//                if ( !m53T5BwgtpbkV9ZEjd.IsOpen )
//                    return;
//                m53T5BwgtpbkV9ZEjd.IsOpen = false;
//                m53T5BwgtpbkV9ZEjd.IsOpen = true;
//            }
//            finally
//            {
//                this._variableSome3535._isUpdating = false;
//                ChartAnnotation chartAnnotation;
//                if ( ( ( KeyedCollection<AnnotationBase, ChartAnnotation> ) this._variableSome3535._baseToAnnotationPair ).TryGetValue( annotationBase, ref chartAnnotation ) )
//                    this._variableSome3535.ChartArea.InvokeAnnotationModifiedEvent( chartAnnotation, this._variableSome3535.GetAnnotationData( annotationBase ) );
//            }
//        }
//    }

//    private readonly ChartArea _chartArea;

//    private readonly AnnotationCollection _annotationCollection;

//    private readonly PairSet<AnnotationBase, ChartAnnotation> _baseToAnnotationPair = new PairSet<AnnotationBase, ChartAnnotation>();

//    private readonly HashSet<AnnotationBase> _annotationBaseHashset = new HashSet<AnnotationBase>();

//    private RulerAnnotation _rulerAnnotation;

//    private bool _isUpdating;

//    private UltrachartAnnotationEditor _annotationEditor;

//    public static readonly DependencyProperty UserAnnotationTypeProperty = DependencyProperty.Register(nameof (UserAnnotationType), typeof (ChartAnnotationTypes), typeof (AnnotationModifier), new PropertyMetadata((object) ChartAnnotationTypes.None, new PropertyChangedCallback(AnnotationModifier.OnUserAnnotationTypePropertyChanged)));

//    private bool _someInternalBoolean;

//    public AnnotationModifier( ChartArea area, AnnotationCollection annotation )
//    {
//        this.InitializeComponent();
//        this._chartArea = area ?? throw new ArgumentNullException( "area" );
//        this._annotationCollection = annotation ?? throw new ArgumentNullException( "annotations" );
//    }

//    private Chart ChartArea => this._chartArea.Chart as Chart;

//    private UltrachartAnnotationEditor GetAnnotationEditor()
//    {
//        return this._annotationEditor ?? ( this._annotationEditor = new UltrachartAnnotationEditor() );
//    }

//    private static void OnUserAnnotationTypePropertyChanged(
//      DependencyObject d,
//      DependencyPropertyChangedEventArgs _param1 )
//    {
//        ( ( AnnotationModifier ) d ).SetAnnotationStyleAndType( ( ChartAnnotationTypes ) _param1.NewValue );
//    }

//    public ChartAnnotationTypes UserAnnotationType
//    {
//        get
//        {
//            return ( ChartAnnotationTypes ) this.GetValue( AnnotationModifier.UserAnnotationTypeProperty );
//        }
//        set
//        {
//            this.SetValue( AnnotationModifier.UserAnnotationTypeProperty, ( object ) value );
//        }
//    }

//    private void RemoveRulerAnnotation()
//    {
//        if ( this._rulerAnnotation == null )
//            return;
//        this.ParentSurface.Annotations.Remove( ( IAnnotation ) this._rulerAnnotation );
//        this._rulerAnnotation = ( RulerAnnotation ) null;
//    }

//    protected override IAnnotation CreateAnnotation( Type annotationType, Style annotationStyle )
//    {
//        if ( annotationType != typeof( RulerAnnotation ) )
//            return base.CreateAnnotation( annotationType, annotationStyle );

//        this.RemoveRulerAnnotation();
//        var candle =  this._chartArea.Elements.OfType<IChartCandleElement>().FirstOrDefault<IChartCandleElement>();

//        double num = (double) ((Decimal?) ( (candle == null ? null :  this._chartArea.Chart.TryGetSubscription( candle ) )?.MarketData).PriceStep ?? 0.01M);

//        var ruler = new RulerAnnotation();
//        ruler.YAxisId = this.YAxisId;
//        ruler.XAxisId = this.XAxisId;
//        ruler.PriceStep = num;
//        ruler.RemoveOnClick = true;


//        this._rulerAnnotation = ruler;

//        this.ParentSurface.Annotations.Add( ruler );
//        return ( AnnotationBase ) ruler;
//    }

//    public override void OnModifierMouseDown( ModifierMouseArgs e )
//    {
//        if ( MathHelper.IsNaN( ( double ) this.YAxis.GetDataValue( e.MousePoint.Y ) ) )
//            this.UserAnnotationType = ChartAnnotationTypes.None;
//        else
//            base.OnModifierMouseDown( e );
//    }

//    public void SetAnnotationStyleAndType( ChartAnnotationTypes annotationTypes )
//    {
//        if ( annotationTypes == ChartAnnotationTypes.None )
//        {
//            CollectionHelper.ForEach<IAnnotation>( this._annotationCollection, i => i.IsEditable = true );
//            this.AnnotationType = ( Type ) null;
//            this.IsEnabled = false;
//        }
//        else
//        {
//            Type type = annotationTypes.GetType();
//            string key = type.Name + "Style";
//            if ( this.Resources.Contains( ( object ) key ) )
//                this.AnnotationStyle = ( Style ) this.Resources[ ( object ) key ];
//            this.AnnotationType = type;
//            this.IsEnabled = true;
//        }
//    }

//    private void fxAnnotationCreationModifier_AnnotationCreated( object _param1, EventArgs _param2 )
//    {
//        AnnotationBase annotation = (AnnotationBase) this.Annotation;
//        ChartAnnotationTypes userAnnotationType = this.UserAnnotationType;
//        this.UserAnnotationType = ChartAnnotationTypes.None;
//        this.AnnotationType = ( Type ) null;
//        this.IsEnabled = false;
//        if ( annotation == null )
//            return;
//        bool flag = !(annotation is RulerAnnotation);
//        this.AddMenuItems( annotation, flag );
//        this.AddDependencyProperties( annotation );
//        ChartAnnotation chartAnnotation = new ChartAnnotation()
//        {
//            Type = userAnnotationType
//        };
//        ( ( KeyedCollection<AnnotationBase, ChartAnnotation> ) this._baseToAnnotationPair )[ annotation ] = chartAnnotation;
//        ( ( ICollection<IChartElement> ) this._chartArea.Elements ).Add( ( IChartElement ) chartAnnotation );
//        Chart chart = this.ChartArea;
//        if ( chart != null )
//        {
//            chart.InvokeAnnotationCreatedEvent( chartAnnotation );
//            chart.InvokeAnnotationModifiedEvent( chartAnnotation, this.GetAnnotationData( annotation ) );
//        }
//        if ( !annotation.IsSelected )
//            return;
//        Keyboard.Focus( ( IInputElement ) annotation );
//        this.ChartArea?.InvokeAnnotationSelectedEvent( chartAnnotation, this.GetAnnotationData( annotation ) );
//    }

//    private bool HasAnnotation( AnnotationBase _param1 )
//    {
//        return this._annotationBaseHashset.Contains( _param1 );
//    }

//    private void AddMenuItems( AnnotationBase _param1, bool _param2 )
//    {
//        AnnotationModifier.SomeClass398 jq9Llz3ahZ2LrQl4 = new AnnotationModifier.SomeClass398();
//        jq9Llz3ahZ2LrQl4._variableSome3535 = this;
//        jq9Llz3ahZ2LrQl4._AnnotationBase11 = _param1;
//        if ( _param2 == this.HasAnnotation( jq9Llz3ahZ2LrQl4._AnnotationBase11 ) )
//            return;
//        if ( _param2 )
//            this._annotationBaseHashset.Add( jq9Llz3ahZ2LrQl4._AnnotationBase11 );
//        else
//            this._annotationBaseHashset.Remove( jq9Llz3ahZ2LrQl4._AnnotationBase11 );
//        jq9Llz3ahZ2LrQl4._AnnotationBase11.IsEditable = _param2;
//        jq9Llz3ahZ2LrQl4._AnnotationBase11.CanEditText = _param2;
//        jq9Llz3ahZ2LrQl4._AnnotationBase11.FocusVisualStyle = ( Style ) null;
//        jq9Llz3ahZ2LrQl4._DelegateCommand_0001 = new DelegateCommand<AnnotationBase>( new Action<AnnotationBase>( jq9Llz3ahZ2LrQl4.Method001 ) );
//        PopupMenu popupMenu1 = new PopupMenu();
//        CommonBarItemCollection items1 = popupMenu1.Items;
//        BarButtonItem barButtonItem1 = new BarButtonItem();
//        barButtonItem1.Glyph = ThemedIconsExtension.GetImage( "Settings" );
//        barButtonItem1.Content = ( object ) ( LocalizedStrings.Properties + "…" );
//        barButtonItem1.Command = ( ICommand ) new DelegateCommand<AnnotationBase>( new Action<AnnotationBase>( jq9Llz3ahZ2LrQl4.Method002 ) );
//        barButtonItem1.CommandParameter = ( object ) jq9Llz3ahZ2LrQl4._AnnotationBase11;
//        items1.Add( ( IBarItem ) barButtonItem1 );
//        CommonBarItemCollection items2 = popupMenu1.Items;
//        BarButtonItem barButtonItem2 = new BarButtonItem();
//        barButtonItem2.Glyph = ThemedIconsExtension.GetImage( "Remove2" );
//        barButtonItem2.Content = ( object ) LocalizedStrings.Delete;
//        barButtonItem2.Command = ( ICommand ) jq9Llz3ahZ2LrQl4._DelegateCommand_0001;
//        barButtonItem2.CommandParameter = ( object ) jq9Llz3ahZ2LrQl4._AnnotationBase11;
//        items2.Add( ( IBarItem ) barButtonItem2 );
//        PopupMenu popupMenu2 = popupMenu1;
//        if ( _param2 )
//            BarManager.SetDXContextMenu( ( UIElement ) jq9Llz3ahZ2LrQl4._AnnotationBase11, ( IPopupControl ) popupMenu2 );
//        if ( _param2 )
//        {
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.KeyDown += new KeyEventHandler( jq9Llz3ahZ2LrQl4.Method003 );
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.Selected += new EventHandler( jq9Llz3ahZ2LrQl4.Method004 );
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.PreviewMouseLeftButtonDown += new MouseButtonEventHandler( jq9Llz3ahZ2LrQl4.Method005 );
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.Unselected += new EventHandler( jq9Llz3ahZ2LrQl4.Method006 );
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.DragStarted += new EventHandler<EventArgs>( jq9Llz3ahZ2LrQl4.Method007 );
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.DragEnded += new EventHandler<EventArgs>( jq9Llz3ahZ2LrQl4.Method008 );
//        }
//        else
//        {
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.KeyDown -= new KeyEventHandler( jq9Llz3ahZ2LrQl4.Method003 );
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.Selected -= new EventHandler( jq9Llz3ahZ2LrQl4.Method004 );
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler( jq9Llz3ahZ2LrQl4.Method005 );
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.Unselected -= new EventHandler( jq9Llz3ahZ2LrQl4.Method006 );
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.DragStarted -= new EventHandler<EventArgs>( jq9Llz3ahZ2LrQl4.Method007 );
//            jq9Llz3ahZ2LrQl4._AnnotationBase11.DragEnded -= new EventHandler<EventArgs>( jq9Llz3ahZ2LrQl4.Method008 );
//        }
//    }

//    private void AddDependencyProperties( AnnotationBase _param1 )
//    {
//        AnnotationModifier.SomeClass7237 doDcwiev7trI4Ny0 = new AnnotationModifier.SomeClass7237();
//        doDcwiev7trI4Ny0._variableSome3535 = this;
//        doDcwiev7trI4Ny0._public_ActiveOrderAnnotation_083 = _param1;
//        if ( this.ChartArea == null )
//            return;
//        doDcwiev7trI4Ny0._public_ActiveOrderAnnotation_083.Selected += new EventHandler( doDcwiev7trI4Ny0.Method0845);
//        doDcwiev7trI4Ny0._public_ActiveOrderAnnotation_083.Unselected += new EventHandler( doDcwiev7trI4Ny0.Method0833);
//        List<DependencyProperty> dependencyPropertyList = new List<DependencyProperty>()
//    {
//      AnnotationBase.IsHiddenProperty,
//      AnnotationBase.IsEditableProperty,
//      AnnotationBase.X1Property,
//      AnnotationBase.X2Property,
//      AnnotationBase.Y1Property,
//      AnnotationBase.Y2Property,
//      AnnotationBase.CoordinateModeProperty
//    };
//        if ( doDcwiev7trI4Ny0._public_ActiveOrderAnnotation_083 is LineAnnotationBase )
//        {
//            dependencyPropertyList.Add( LineAnnotationBase.StrokeThicknessProperty );
//            dependencyPropertyList.Add( LineAnnotationBase.StrokeProperty );
//        }
//        if ( !( doDcwiev7trI4Ny0._public_ActiveOrderAnnotation_083 is HorizontalLineAnnotation ) )
//        {
//            if ( !( doDcwiev7trI4Ny0._public_ActiveOrderAnnotation_083 is VerticalLineAnnotation ) )
//            {
//                if ( !( doDcwiev7trI4Ny0._public_ActiveOrderAnnotation_083 is BoxAnnotation ) )
//                {
//                    if ( !( doDcwiev7trI4Ny0._public_ActiveOrderAnnotation_083 is TextAnnotation ) )
//                    {
//                        if ( doDcwiev7trI4Ny0._public_ActiveOrderAnnotation_083 is RulerAnnotation )
//                            dependencyPropertyList.Add( Control.BackgroundProperty );
//                    }
//                    else
//                    {
//                        dependencyPropertyList.Add( TextAnnotation.TextProperty );
//                        dependencyPropertyList.Add( Control.BackgroundProperty );
//                        dependencyPropertyList.Add( Control.BorderBrushProperty );
//                        dependencyPropertyList.Add( Control.BorderThicknessProperty );
//                    }
//                }
//                else
//                {
//                    dependencyPropertyList.Add( Control.BackgroundProperty );
//                    dependencyPropertyList.Add( Control.BorderBrushProperty );
//                    dependencyPropertyList.Add( Control.BorderThicknessProperty );
//                }
//            }
//            else
//                dependencyPropertyList.Add( FrameworkElement.VerticalAlignmentProperty );
//        }
//        else
//            dependencyPropertyList.Add( FrameworkElement.HorizontalAlignmentProperty );
//        dependencyPropertyList.ForEach( new Action<DependencyProperty>( doDcwiev7trI4Ny0.DoSomePropertyListStuff034) );
//    }

//    private ChartDrawData.AnnotationData GetAnnotationData( AnnotationBase _param1 )
//    {
//        AnnotationModifier.Struct1 vm6DexIkyzzokCaW;
//        vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083 = _param1;
//        ChartDrawData.AnnotationData annotationData = new ChartDrawData.AnnotationData();
//        vm6DexIkyzzokCaW.sCalc = vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083.XAxis?.GetCurrentCoordinateCalculator();
//        annotationData.IsVisible = new bool?( !vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083.IsHidden );
//        annotationData.IsEditable = new bool?( this.HasAnnotation( vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083 ) );
//        annotationData.CoordinateMode = new AnnotationCoordinateMode?( vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083.CoordinateMode );
//        annotationData.X1 = AnnotationModifier.IndexToData( vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083.X1, ref vm6DexIkyzzokCaW );
//        annotationData.X2 = AnnotationModifier.IndexToData( vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083.X2, ref vm6DexIkyzzokCaW );
//        annotationData.Y1 = AnnotationModifier.DataToIndex( vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083.Y1, ref vm6DexIkyzzokCaW );
//        annotationData.Y2 = AnnotationModifier.DataToIndex( vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083.Y2, ref vm6DexIkyzzokCaW );
//        if ( vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083 is LineAnnotationBase z2vouRgM1 )
//        {
//            annotationData.Stroke = z2vouRgM1.Stroke;
//            annotationData.Thickness = new Thickness?( new Thickness( z2vouRgM1.StrokeThickness ) );
//        }
//        if ( !( vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083 is HorizontalLineAnnotation z2vouRgM5 ) )
//        {
//            if ( !( vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083 is VerticalLineAnnotation z2vouRgM4 ) )
//            {
//                if ( !( vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083 is BoxAnnotation z2vouRgM3 ) )
//                {
//                    if ( vm6DexIkyzzokCaW._public_ActiveOrderAnnotation_083 is TextAnnotation z2vouRgM2 )
//                    {
//                        annotationData.Foreground = z2vouRgM2.Foreground;
//                        annotationData.Text = z2vouRgM2.Text;
//                        annotationData.Fill = z2vouRgM2.Background;
//                        annotationData.Stroke = z2vouRgM2.BorderBrush;
//                        annotationData.Thickness = new Thickness?( z2vouRgM2.BorderThickness );
//                    }
//                }
//                else
//                {
//                    annotationData.Fill = z2vouRgM3.Background;
//                    annotationData.Stroke = z2vouRgM3.BorderBrush;
//                    annotationData.Thickness = new Thickness?( z2vouRgM3.BorderThickness );
//                }
//            }
//            else
//            {
//                annotationData.VerticalAlignment = new VerticalAlignment?( z2vouRgM4.VerticalAlignment );
//                annotationData.LabelPlacement = new LabelPlacement?( z2vouRgM4.LabelPlacement );
//                annotationData.ShowLabel = new bool?( z2vouRgM4.ShowLabel );
//            }
//        }
//        else
//        {
//            annotationData.HorizontalAlignment = new HorizontalAlignment?( z2vouRgM5.HorizontalAlignment );
//            annotationData.LabelPlacement = new LabelPlacement?( z2vouRgM5.LabelPlacement );
//            annotationData.ShowLabel = new bool?( z2vouRgM5.ShowLabel );
//        }
//        return annotationData;
//    }

//    public void GuiUpdateAndClear( ChartAnnotation _param1 )
//    {
//        AnnotationBase annotationBase;
//        if ( !this._baseToAnnotationPair.TryGetKey( _param1, ref annotationBase ) )
//            return;
//        this._annotationCollection.Remove( ( IAnnotation ) annotationBase );
//        ( ( KeyedCollection<AnnotationBase, ChartAnnotation> ) this._baseToAnnotationPair ).Remove( annotationBase );
//        ( ( ICollection<IChartElement> ) this._chartArea.Elements ).Remove( ( IChartElement ) _param1 );
//        this.ChartArea?.InvokeAnnotationDeletedEvent( _param1 );
//    }

//    public void Draw(
//      ChartAnnotation _param1,
//      ChartDrawData.AnnotationData _param2 )
//    {
//        AnnotationModifier.Struct0 vqd1Qhu2nAw1nzwT0;
//        bool? nullable;
//        if ( !this._baseToAnnotationPair.TryGetKey( _param1, ref vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 ) )
//        {
//            Type type = _param1.Type.GetType();
//            vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 = ( AnnotationBase ) Activator.CreateInstance( type );
//            vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083.XAxisId = _param1.XAxisId;
//            vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083.YAxisId = _param1.YAxisId;
//            vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083.IsHidden = false;
//            AnnotationBase z2vouRgM = vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083;
//            nullable = _param2.IsEditable;
//            int num = !(!nullable.GetValueOrDefault() & nullable.HasValue) ? 1 : 0;
//            this.AddMenuItems( z2vouRgM, num != 0 );
//            this.AddDependencyProperties( vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 );
//            ( ( KeyedCollection<AnnotationBase, ChartAnnotation> ) this._baseToAnnotationPair )[ vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 ] = _param1;
//            this._annotationCollection.Add( ( IAnnotation ) vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 );
//            this.ChartArea?.InvokeAnnotationCreatedEvent( _param1 );
//        }
//        vqd1Qhu2nAw1nzwT0.sCalc = vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083.XAxis?.GetCurrentCoordinateCalculator();
//        try
//        {
//            this._isUpdating = true;
//            nullable = _param2.IsVisible;
//            if ( nullable.HasValue )
//            {
//                AnnotationBase z2vouRgM = vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083;
//                nullable = _param2.IsVisible;
//                int num = !nullable.Value ? 1 : 0;
//                z2vouRgM.IsHidden = num != 0;
//            }
//            nullable = _param2.IsEditable;
//            if ( nullable.HasValue )
//            {
//                AnnotationBase z2vouRgM = vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083;
//                nullable = _param2.IsEditable;
//                int num = nullable.Value ? 1 : 0;
//                this.AddMenuItems( z2vouRgM, num != 0 );
//            }
//            if ( _param2.CoordinateMode.HasValue )
//                vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083.CoordinateMode = _param2.CoordinateMode.Value;
//            IComparable comparable1 = AnnotationModifier.DataToIndex(_param2.X1, ref vqd1Qhu2nAw1nzwT0);
//            IComparable comparable2 = AnnotationModifier.DataToIndex(_param2.X2, ref vqd1Qhu2nAw1nzwT0);
//            IComparable comparable3 = AnnotationModifier.\u0023\u003Dq8f\u0024Kf3mr1qpotJDNtCA37\u0024_mt5h9RLbGp_SzHkzBWCc\u003D( _param2.Y1 );
//            IComparable comparable4 = AnnotationModifier.\u0023\u003Dq8f\u0024Kf3mr1qpotJDNtCA37\u0024_mt5h9RLbGp_SzHkzBWCc\u003D( _param2.Y2 );
//            if ( comparable1 != null )
//                vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083.X1 = comparable1;
//            if ( comparable2 != null )
//                vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083.X2 = comparable2;
//            if ( comparable3 != null )
//                vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083.Y1 = comparable3;
//            if ( comparable4 != null )
//                vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083.Y2 = comparable4;
//            if ( vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 is LineAnnotationBase z2vouRgM1 )
//            {
//                if ( _param2.Stroke != null )
//                    z2vouRgM1.Stroke = _param2.Stroke;
//                if ( _param2.Thickness.HasValue )
//                    z2vouRgM1.StrokeThickness = _param2.Thickness.Value.Left;
//            }
//            if ( !( vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 is HorizontalLineAnnotation z2vouRgM6 ) )
//            {
//                if ( !( vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 is VerticalLineAnnotation z2vouRgM5 ) )
//                {
//                    if ( !( vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 is BoxAnnotation z2vouRgM4 ) )
//                    {
//                        if ( !( vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 is TextAnnotation z2vouRgM3 ) )
//                        {
//                            if ( !( vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 is RulerAnnotation z2vouRgM2 ) || _param2.Fill == null )
//                                return;
//                            Brush brush = _param2.Fill is SolidColorBrush fill ? (Brush) new SolidColorBrush(fill.Color.ToTransparent((byte) 50)) : _param2.Fill;
//                            z2vouRgM2.Background = brush;
//                        }
//                        else
//                        {
//                            if ( _param2.Foreground != null )
//                                z2vouRgM3.Foreground = _param2.Foreground;
//                            if ( _param2.Text != null )
//                                z2vouRgM3.Text = _param2.Text;
//                            if ( _param2.Fill != null )
//                                z2vouRgM3.Background = _param2.Fill;
//                            if ( _param2.Stroke != null )
//                                z2vouRgM3.BorderBrush = _param2.Stroke;
//                            if ( !_param2.Thickness.HasValue )
//                                return;
//                            z2vouRgM3.BorderThickness = _param2.Thickness.Value;
//                        }
//                    }
//                    else
//                    {
//                        if ( _param2.Fill != null )
//                            z2vouRgM4.Background = _param2.Fill;
//                        if ( _param2.Stroke != null )
//                            z2vouRgM4.BorderBrush = _param2.Stroke;
//                        if ( !_param2.Thickness.HasValue )
//                            return;
//                        z2vouRgM4.BorderThickness = _param2.Thickness.Value;
//                    }
//                }
//                else
//                {
//                    if ( _param2.VerticalAlignment.HasValue )
//                        z2vouRgM5.VerticalAlignment = _param2.VerticalAlignment.Value;
//                    if ( _param2.LabelPlacement.HasValue )
//                        z2vouRgM5.LabelPlacement = _param2.LabelPlacement.Value;
//                    nullable = _param2.ShowLabel;
//                    if ( !nullable.HasValue )
//                        return;
//                    VerticalLineAnnotation verticalLineAnnotation = z2vouRgM5;
//                    nullable = _param2.ShowLabel;
//                    int num = nullable.Value ? 1 : 0;
//                    verticalLineAnnotation.ShowLabel = num != 0;
//                }
//            }
//            else
//            {
//                if ( _param2.HorizontalAlignment.HasValue )
//                    z2vouRgM6.HorizontalAlignment = _param2.HorizontalAlignment.Value;
//                if ( _param2.LabelPlacement.HasValue )
//                    z2vouRgM6.LabelPlacement = _param2.LabelPlacement.Value;
//                nullable = _param2.ShowLabel;
//                if ( !nullable.HasValue )
//                    return;
//                HorizontalLineAnnotation horizontalLineAnnotation = z2vouRgM6;
//                nullable = _param2.ShowLabel;
//                int num = nullable.Value ? 1 : 0;
//                horizontalLineAnnotation.ShowLabel = num != 0;
//            }
//        }
//        finally
//        {
//            this._isUpdating = false;
//            this.ChartArea?.InvokeAnnotationModifiedEvent( _param1, this.GetAnnotationData( vqd1Qhu2nAw1nzwT0._public_ActiveOrderAnnotation_083 ) );
//        }
//    }

//    [DebuggerNonUserCode]
//    [GeneratedCode( "PresentationBuildTasks", "9.0.0.0" )]
//    public void InitializeComponent()
//    {
//        if ( this._someInternalBoolean )
//            return;
//        this._someInternalBoolean = true;
//        Application.LoadComponent( ( object ) this, new Uri( "/StockSharp.Xaml.Charting;V5.0.0;component/ultrachart/ultrachartannotationmodifier.xaml", UriKind.Relative ) );
//    }

//    [DebuggerNonUserCode]
//    [GeneratedCode( "PresentationBuildTasks", "9.0.0.0" )]
//    public Delegate \u0023\u003DzciIj4U627yBM( Type _param1, string _param2 )
//    {
//        return Delegate.CreateDelegate( _param1, ( object ) this, _param2 );
//    }

//    [DebuggerNonUserCode]
//    [GeneratedCode( "PresentationBuildTasks", "9.0.0.0" )]
//    [EditorBrowsable( EditorBrowsableState.Never )]
//    void IComponentConnector.\u0023\u003DzuNHLeGEnMjz9FDFZ6wymuXfyw_Iz(int _param1, object _param2)
//    {
//        this._someInternalBoolean = true;
//    }

//    public static IComparable IndexToData(
//      IComparable _param0,
//      ref AnnotationModifier.Struct1 _param1)
//    {
//        switch ( _param0 )
//        {
//            case null:
//                return ( IComparable ) null;
//            case int num1:
//                if ( !( _param1.sCalc is ICategoryCoordinateCalculator zFlkZpfJp6G9R))
//          throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.UnexpectedCoordTypeParams, new object[ 1 ]
//          {
//            (object) "int"
//          } ) );
//                return ( IComparable ) new DateTimeOffset( zFlkZpfJp6G9R.\u0023\u003DzWZQlXHuDrnKc( num1 ), TimeSpan.Zero );
//            case DateTime dateTime:
//                return ( IComparable ) new DateTimeOffset( dateTime, TimeSpan.Zero );
//            case double num2:
//                return ( IComparable ) num2;
//            default:
//                throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.UnexpectedCoordTypeParams, new object[ 1 ]
//                {
//          (object) _param0.GetType().Name
//                } ) );
//        }
//    }

//    public static IComparable DataToIndex(
//      IComparable _param0,
//      ref AnnotationModifier.Struct1 _param1)
//    {
//        if ( _param0 == null )
//            return ( IComparable ) null;
//        if ( _param0 is double num )
//            return _param1._public_ActiveOrderAnnotation_083.CoordinateMode == AnnotationCoordinateMode.Relative || _param1._public_ActiveOrderAnnotation_083.CoordinateMode == AnnotationCoordinateMode.RelativeY ? ( IComparable ) num : ( IComparable ) Converter.To<Decimal>( ( object ) num );
//        throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.UnexpectedCoordTypeParams, new object[ 1 ]
//        {
//      (object) _param0.GetType().Name
//        } ) );
//    }

//    public static IComparable DataToIndex(
//      IComparable _param0,
//      ref AnnotationModifier.Struct0 _param1)
//  {
//    if (_param0 == null)
//      return (IComparable) null;
//    if (_param0 is DateTimeOffset dateTimeOffset)
//      return (IComparable) dateTimeOffset.UtcDateTime;
//    if (_param0 is DateTime dateTime)
//      return (IComparable) dateTime;
//    if (_param1.sCalc is ICategoryCoordinateCalculator && (_param1._public_ActiveOrderAnnotation_083.CoordinateMode == AnnotationCoordinateMode.Absolute || _param1._public_ActiveOrderAnnotation_083.CoordinateMode == AnnotationCoordinateMode.RelativeY))
//      throw new InvalidOperationException( StringHelper.Put(LocalizedStrings.UnexpectedCoordTypeParams, new object[ 1 ]
//      {
//        (object) _param0.GetType().Name
//}));
//    switch (_param0)
//    {
//      case Decimal num1:
//        return (IComparable) (double) num1;
//      case double num2:
//        return (IComparable) num2;
//      default:
//        throw new InvalidOperationException( StringHelper.Put(LocalizedStrings.UnexpectedCoordTypeParams, new object[ 1 ]
//        {
//          (object) _param0.GetType().Name
//        }));
//    }
//  }

//  public static IComparable \u0023\u003Dq8f\u0024Kf3mr1qpotJDNtCA37\u0024_mt5h9RLbGp_SzHkzBWCc\u003D(
//    IComparable _param0)
//  {
//    switch (_param0)
//    {
//      case null:
//        return ( IComparable ) null;
//      case Decimal num1:
//    return ( IComparable ) ( double ) num1;
//case double num2:
//    return ( IComparable ) num2;
//default:
//    throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.UnexpectedCoordTypeParams, new object[ 1 ]
//    {
//          (object) _param0.GetType().Name
//    } ) );
//}
//  }

//  [Serializable]
//private new sealed class SomeClass34343383
//{
//    public static readonly AnnotationModifier.SomeClass34343383 SomeMethond0343 = new AnnotationModifier.SomeClass34343383();
//    public static Action<IAnnotation> _Action_IAnnotation_002;
//    public static Action<IAnnotation> _Action_IAnnotation_003;

//    public void SomeImportantMethod3234(
//      IAnnotation _param1 )
//    {
//        _param1.set_IsEditable( true );
//    }

//    public void Setset_IsSelected_false_(
//      IAnnotation _param1 )
//    {
//        _param1.set_IsSelected( false );
//    }
//}

//[StructLayout( LayoutKind.Auto )]
//private struct Struct0
//{

//    public ICoordinateCalculator<<double> sCalc;
    
//    public AnnotationBase _public_ActiveOrderAnnotation_083;
//}

//[StructLayout( LayoutKind.Auto )]
//private struct Struct1
//  {


//    public ICoordinateCalculator<<double> sCalc;
    
//    public AnnotationBase _public_ActiveOrderAnnotation_083;
//  }

  

//  private sealed class AnotherSealedClass003
//  {
//    public AnnotationBase _public_ActiveOrderAnnotation_083;

//    public bool Method009(
//      IAnnotation _param1)
//    {
//      return _param1 != this._public_ActiveOrderAnnotation_083;
//    }
//  }

//  private sealed class SomeClass7237
//  {
//    public AnnotationModifier _variableSome3535;
//    public AnnotationBase _public_ActiveOrderAnnotation_083;
//    public Action<DependencyPropertyChangedEventArgs> \u0023\u003DzDg_APFfs\u0024qGS;

//    public void Method0845(
//    #nullable enable
//    object? _param1, EventArgs _param2)
//    {
//      AnnotationBase annotationBase = (AnnotationBase) _param1;
//      ChartAnnotation chartAnnotation = CollectionHelper.TryGetValue<AnnotationBase, ChartAnnotation>((IDictionary<AnnotationBase, ChartAnnotation>) this._variableSome3535._baseToAnnotationPair, annotationBase);
//      this._variableSome3535.ChartArea?.InvokeAnnotationSelectedEvent(chartAnnotation, Equatable<ChartAnnotation>.op_Equality((Equatable<ChartAnnotation>) chartAnnotation, (ChartAnnotation) null) ? (ChartDrawData.AnnotationData) null : this._variableSome3535.GetAnnotationData(annotationBase));
//    }

//    public void Method0833(object? _param1, EventArgs _param2)
//    {
//      this._variableSome3535.ChartArea?.InvokeAnnotationSelectedEvent((ChartAnnotation) null, (ChartDrawData.AnnotationData) null);
//    }

//    public void DoSomePropertyListStuff034(
//    #nullable disable
//    DependencyProperty _param1)
//    {
//      this._public_ActiveOrderAnnotation_083.AddPropertyListener(_param1, this.\u0023\u003DzDg_APFfs\u0024qGS ?? (this.\u0023\u003DzDg_APFfs\u0024qGS = new Action<DependencyPropertyChangedEventArgs>(this.Method0844)));
//    }

//    public void Method0844(
//      DependencyPropertyChangedEventArgs _param1)
//    {
//      ChartAnnotation chartAnnotation;
//      if (!((KeyedCollection<AnnotationBase, ChartAnnotation>) this._variableSome3535._baseToAnnotationPair).TryGetValue(this._public_ActiveOrderAnnotation_083, ref chartAnnotation) || this._variableSome3535._isUpdating)
//        return;
//      this._variableSome3535.ChartArea.InvokeAnnotationModifiedEvent(chartAnnotation, this._variableSome3535.GetAnnotationData(this._public_ActiveOrderAnnotation_083));
//    }
//  }
//}
