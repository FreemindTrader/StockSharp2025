// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.MouseManager
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
namespace fx.Xaml.Charting
{
    public class MouseManager : IMouseManager
    {
        public static readonly DependencyProperty MouseEventGroupProperty = DependencyProperty.RegisterAttached("MouseEventGroup", typeof (string), typeof (MouseManager), new PropertyMetadata((object) null, new PropertyChangedCallback(MouseManager.OnMouseEventGroupPropertyChanged)));
        private static readonly IDictionary<string, IList<IReceiveMouseEvents>> _modifiersByGroup = (IDictionary<string, IList<IReceiveMouseEvents>>) new Dictionary<string, IList<IReceiveMouseEvents>>();
        private readonly IDictionary<IReceiveMouseEvents, MouseDelegates> _delegatesByElement = (IDictionary<IReceiveMouseEvents, MouseDelegates>) new Dictionary<IReceiveMouseEvents, MouseDelegates>();
        private readonly IDictionary<IReceiveMouseEvents, IPublishMouseEvents> _subscribersBySource = (IDictionary<IReceiveMouseEvents, IPublishMouseEvents>) new Dictionary<IReceiveMouseEvents, IPublishMouseEvents>();
        private readonly IDictionary<object, Point> _previousMousePositions = (IDictionary<object, Point>) new Dictionary<object, Point>();
        private DateTime _lastClickTime;
        private Point _lastClickPosition;

        [DllImport( "user32.dll" )]
        private static extern uint GetDoubleClickTime();

        public MouseManager()
        {
            this.MousePositionProvider = ( IMousePositionProvider ) new fx.Xaml.Charting.MousePositionProvider();
            this.TouchPositionProvider = ( ITouchPositionProvider ) new fx.Xaml.Charting.TouchPositionProvider();
        }

        public static void SetMouseEventGroup( DependencyObject element, string modifierGroup )
        {
            element.SetValue( MouseManager.MouseEventGroupProperty, ( object ) modifierGroup );
        }

        public static string GetMouseEventGroup( DependencyObject element )
        {
            return ( string ) element.GetValue( MouseManager.MouseEventGroupProperty );
        }

        public void Subscribe( IPublishMouseEvents source, IReceiveMouseEvents target )
        {
            Guard.NotNull( ( object ) source, "element" );
            Guard.NotNull( ( object ) target, "receiveMouseEvents" );
            this.Unsubscribe( target );
            MouseDelegates mouseDelegates = new MouseDelegates();
            mouseDelegates.Target = target;
            this.ResetGroup( target );
            Action<object, TouchManipulationEventArgs, Action<ModifierTouchManipulationArgs, IReceiveMouseEvents, bool>> touchHandler = (Action<object, TouchManipulationEventArgs, Action<ModifierTouchManipulationArgs, IReceiveMouseEvents, bool>>) ((s, e, raiseEvent) =>
      {
          IEnumerable<IReceiveMouseEvents> targets = this.GetTargets(mouseDelegates.Target);
          ModifierTouchManipulationArgs args = new ModifierTouchManipulationArgs(e.TouchPoints, true, mouseDelegates.Target);
          targets.ForEachDo<IReceiveMouseEvents>((Action<IReceiveMouseEvents>) (t => raiseEvent(args, t, t.Equals((object) mouseDelegates.Target))));
          e.Handled = args.Handled;
      });
            mouseDelegates.TouchDownDelegate = ( EventHandler<TouchManipulationEventArgs> ) ( ( s, e ) => touchHandler( s, e, new Action<ModifierTouchManipulationArgs, IReceiveMouseEvents, bool>( MouseManager.RaiseTouchDown ) ) );
            mouseDelegates.TouchMoveDelegate = ( EventHandler<TouchManipulationEventArgs> ) ( ( s, e ) => touchHandler( s, e, new Action<ModifierTouchManipulationArgs, IReceiveMouseEvents, bool>( MouseManager.RaiseTouchMove ) ) );
            mouseDelegates.TouchUpDelegate = ( EventHandler<TouchManipulationEventArgs> ) ( ( s, e ) => touchHandler( s, e, new Action<ModifierTouchManipulationArgs, IReceiveMouseEvents, bool>( MouseManager.RaiseTouchUp ) ) );
            mouseDelegates.MouseLeftDownDelegate = ( MouseButtonEventHandler ) ( ( s, e ) =>
            {
                Point position = this.GetPosition(source, (MouseEventArgs) e);
                IEnumerable<IReceiveMouseEvents> targets = this.GetTargets(mouseDelegates.Target);
                ModifierMouseArgs args = new ModifierMouseArgs(position, MouseButtons.Left, MouseExtensions.GetCurrentModifier(), true, mouseDelegates.Target);
                TimeSpan timeSpan = DateTime.UtcNow - this._lastClickTime;
                if ( timeSpan < TimeSpan.FromMilliseconds( ( double ) MouseManager.GetDoubleClickTime() ) && timeSpan > TimeSpan.FromMilliseconds( 1.0 ) && PointUtil.Distance( position, this._lastClickPosition ) < 5.0 )
                {
                    targets.ForEachDo<IReceiveMouseEvents>( ( Action<IReceiveMouseEvents> ) ( t => this.RaiseMouseDoubleClick( args, t, t.Equals( ( object ) mouseDelegates.Target ) ) ) );
                }
                else
                {
                    targets.ForEachDo<IReceiveMouseEvents>( ( Action<IReceiveMouseEvents> ) ( t => MouseManager.RaiseMouseDown( args, t, t.Equals( ( object ) mouseDelegates.Target ) ) ) );
                    e.Handled = args.Handled;
                    this._lastClickTime = DateTime.UtcNow;
                    this._lastClickPosition = position;
                }
            } );
            mouseDelegates.MouseLeftUpDelegate = ( MouseButtonEventHandler ) ( ( s, e ) =>
            {
                Point position = this.GetPosition(source, (MouseEventArgs) e);
                IEnumerable<IReceiveMouseEvents> targets = this.GetTargets(mouseDelegates.Target);
                ModifierMouseArgs args = new ModifierMouseArgs(position, MouseButtons.Left, MouseExtensions.GetCurrentModifier(), true, mouseDelegates.Target);
                targets.ForEachDo<IReceiveMouseEvents>( ( Action<IReceiveMouseEvents> ) ( t => this.RaiseMouseUp( args, t, t.Equals( ( object ) mouseDelegates.Target ) ) ) );
                e.Handled = args.Handled;
            } );
            mouseDelegates.MouseMoveDelegate = ( MouseEventHandler ) ( ( s, e ) =>
            {
                Point position = this.GetPosition(source, e);
                IEnumerable<IReceiveMouseEvents> targets = this.GetTargets(mouseDelegates.Target);
                ModifierMouseArgs args = new ModifierMouseArgs(position, MouseButtons.None, MouseExtensions.GetCurrentModifier(), true, mouseDelegates.Target);
                targets.ForEachDo<IReceiveMouseEvents>( ( Action<IReceiveMouseEvents> ) ( t => this.RaiseMouseMove( args, t, t.Equals( ( object ) mouseDelegates.Target ) ) ) );
                e.Handled = args.Handled;
            } );
            mouseDelegates.MouseRightDownDelegate = ( MouseButtonEventHandler ) ( ( s, e ) =>
            {
                Point position = this.GetPosition(source, (MouseEventArgs) e);
                IEnumerable<IReceiveMouseEvents> targets = this.GetTargets(mouseDelegates.Target);
                ModifierMouseArgs args = new ModifierMouseArgs(position, MouseButtons.Right, MouseExtensions.GetCurrentModifier(), true, mouseDelegates.Target);
                targets.ForEachDo<IReceiveMouseEvents>( ( Action<IReceiveMouseEvents> ) ( t => MouseManager.RaiseMouseDown( args, t, t.Equals( ( object ) mouseDelegates.Target ) ) ) );
                e.Handled = args.Handled;
            } );
            mouseDelegates.MouseRightUpDelegate = ( MouseButtonEventHandler ) ( ( s, e ) =>
            {
                Point position = this.GetPosition(source, (MouseEventArgs) e);
                IEnumerable<IReceiveMouseEvents> targets = this.GetTargets(mouseDelegates.Target);
                ModifierMouseArgs args = new ModifierMouseArgs(position, MouseButtons.Right, MouseExtensions.GetCurrentModifier(), true, mouseDelegates.Target);
                targets.ForEachDo<IReceiveMouseEvents>( ( Action<IReceiveMouseEvents> ) ( t => this.RaiseMouseUp( args, t, t.Equals( ( object ) mouseDelegates.Target ) ) ) );
                e.Handled = args.Handled;
            } );
            mouseDelegates.MouseWheelDelegate = ( MouseWheelEventHandler ) ( ( s, e ) =>
            {
                Point position = this.GetPosition(source, (MouseEventArgs) e);
                IEnumerable<IReceiveMouseEvents> targets = this.GetTargets(mouseDelegates.Target);
                ModifierMouseArgs args = new ModifierMouseArgs(position, MouseButtons.None, MouseExtensions.GetCurrentModifier(), e.Delta, true, mouseDelegates.Target);
                targets.ForEachDo<IReceiveMouseEvents>( ( Action<IReceiveMouseEvents> ) ( t => this.RaiseMouseWheel( args, t, t.Equals( ( object ) mouseDelegates.Target ) ) ) );
                e.Handled = args.Handled;
            } );
            mouseDelegates.MouseLeaveDelegate = ( MouseEventHandler ) ( ( s, e ) =>
            {
                Point position = this.GetPosition(source, e);
                IEnumerable<IReceiveMouseEvents> targets = this.GetTargets(mouseDelegates.Target);
                ModifierMouseArgs args = new ModifierMouseArgs(position, MouseButtons.None, MouseExtensions.GetCurrentModifier(), true, mouseDelegates.Target);
                targets.ForEachDo<IReceiveMouseEvents>( ( Action<IReceiveMouseEvents> ) ( t => this.RaiseMouseLeave( args, t, t.Equals( ( object ) mouseDelegates.Target ) ) ) );
                e.Handled = args.Handled;
            } );
            mouseDelegates.MouseMiddleDownDelegate = ( MouseButtonEventHandler ) ( ( s, e ) =>
            {
                Point position = this.GetPosition(source, (MouseEventArgs) e);
                IEnumerable<IReceiveMouseEvents> targets = this.GetTargets(mouseDelegates.Target);
                ModifierMouseArgs args = new ModifierMouseArgs(position, MouseButtons.Middle, MouseExtensions.GetCurrentModifier(), true, mouseDelegates.Target);
                targets.ForEachDo<IReceiveMouseEvents>( ( Action<IReceiveMouseEvents> ) ( t => MouseManager.RaiseMouseDown( args, t, t.Equals( ( object ) mouseDelegates.Target ) ) ) );
                e.Handled = args.Handled;
            } );
            mouseDelegates.MouseMiddleUpDelegate = ( MouseButtonEventHandler ) ( ( s, e ) =>
            {
                Point position = this.GetPosition(source, (MouseEventArgs) e);
                IEnumerable<IReceiveMouseEvents> targets = this.GetTargets(mouseDelegates.Target);
                ModifierMouseArgs args = new ModifierMouseArgs(position, MouseButtons.Middle, MouseExtensions.GetCurrentModifier(), true, mouseDelegates.Target);
                targets.ForEachDo<IReceiveMouseEvents>( ( Action<IReceiveMouseEvents> ) ( t => this.RaiseMouseUp( args, t, t.Equals( ( object ) mouseDelegates.Target ) ) ) );
                e.Handled = args.Handled;
            } );
            source.TouchDown += mouseDelegates.TouchDownDelegate;
            source.TouchMove += mouseDelegates.TouchMoveDelegate;
            source.TouchUp += mouseDelegates.TouchUpDelegate;
            source.MouseLeftButtonDown += mouseDelegates.MouseLeftDownDelegate;
            source.MouseLeftButtonUp += mouseDelegates.MouseLeftUpDelegate;
            mouseDelegates.SynchronizedMouseMove = new RenderSynchronizedMouseMove( source );
            mouseDelegates.SynchronizedMouseMove.SynchronizedMouseMove += mouseDelegates.MouseMoveDelegate;
            source.MouseRightButtonDown += mouseDelegates.MouseRightDownDelegate;
            source.MouseRightButtonUp += mouseDelegates.MouseRightUpDelegate;
            source.MouseWheel += mouseDelegates.MouseWheelDelegate;
            source.MouseLeave += mouseDelegates.MouseLeaveDelegate;
            source.MouseMiddleButtonDown += mouseDelegates.MouseMiddleDownDelegate;
            source.MouseMiddleButtonUp += mouseDelegates.MouseMiddleUpDelegate;
            this.DelegatesByElement.Add( target, mouseDelegates );
            this._subscribersBySource.Add( target, source );
        }

        private Point GetPosition( IPublishMouseEvents source, MouseEventArgs e )
        {
            return this.MousePositionProvider.GetPosition( source, e );
        }

        private TouchPointCollection GetPosition( IPublishMouseEvents source, TouchFrameEventArgs e )
        {
            return this.TouchPositionProvider.GetPosition( source, e );
        }

        private void ResetGroup( IReceiveMouseEvents target )
        {
            DependencyObject dependencyObject = target as DependencyObject;
            if ( dependencyObject == null )
                return;
            string str = (string) dependencyObject.GetValue(MouseManager.MouseEventGroupProperty);
            dependencyObject.SetCurrentValue( MouseManager.MouseEventGroupProperty, ( object ) string.Empty );
            dependencyObject.SetCurrentValue( MouseManager.MouseEventGroupProperty, ( object ) str );
        }

        public void Unsubscribe( IPublishMouseEvents source )
        {
            if ( source == null )
                return;
            List<IReceiveMouseEvents> receiveMouseEventsList = new List<IReceiveMouseEvents>();
            foreach ( KeyValuePair<IReceiveMouseEvents, MouseDelegates> keyValuePair in ( IEnumerable<KeyValuePair<IReceiveMouseEvents, MouseDelegates>> ) this.DelegatesByElement )
            {
                if ( this._subscribersBySource[ keyValuePair.Key ] == source )
                {
                    MouseDelegates mouseDelegates = keyValuePair.Value;
                    source.MouseLeftButtonDown -= mouseDelegates.MouseLeftDownDelegate;
                    source.MouseLeftButtonUp -= mouseDelegates.MouseLeftUpDelegate;
                    source.MouseMove -= mouseDelegates.MouseMoveDelegate;
                    source.MouseRightButtonDown -= mouseDelegates.MouseRightDownDelegate;
                    source.MouseRightButtonUp -= mouseDelegates.MouseRightUpDelegate;
                    source.MouseWheel -= mouseDelegates.MouseWheelDelegate;
                    source.MouseLeave -= mouseDelegates.MouseLeaveDelegate;
                    source.MouseMiddleButtonDown -= mouseDelegates.MouseMiddleDownDelegate;
                    source.MouseMiddleButtonUp -= mouseDelegates.MouseMiddleUpDelegate;
                    foreach ( string key in ( IEnumerable<string> ) MouseManager._modifiersByGroup.Keys )
                    {
                        if ( MouseManager._modifiersByGroup[ key ].Contains( mouseDelegates.Target ) )
                            MouseManager._modifiersByGroup[ key ].Remove( mouseDelegates.Target );
                    }
                    mouseDelegates.Target = ( IReceiveMouseEvents ) null;
                    mouseDelegates.SynchronizedMouseMove.Dispose();
                    mouseDelegates.SynchronizedMouseMove = ( RenderSynchronizedMouseMove ) null;
                    receiveMouseEventsList.Add( keyValuePair.Key );
                }
            }
            foreach ( IReceiveMouseEvents key in receiveMouseEventsList )
            {
                this.DelegatesByElement.Remove( key );
                this._subscribersBySource.Remove( key );
            }
        }

        public void Unsubscribe( IReceiveMouseEvents element )
        {
            if ( element == null || !this._subscribersBySource.ContainsKey( element ) )
                return;
            MouseDelegates mouseDelegates = this.DelegatesByElement[element];
            IPublishMouseEvents publishMouseEvents = this._subscribersBySource[element];
            publishMouseEvents.MouseLeftButtonDown -= mouseDelegates.MouseLeftDownDelegate;
            publishMouseEvents.MouseLeftButtonUp -= mouseDelegates.MouseLeftUpDelegate;
            publishMouseEvents.MouseMove -= mouseDelegates.MouseMoveDelegate;
            publishMouseEvents.MouseRightButtonDown -= mouseDelegates.MouseRightDownDelegate;
            publishMouseEvents.MouseRightButtonUp -= mouseDelegates.MouseRightUpDelegate;
            publishMouseEvents.MouseWheel -= mouseDelegates.MouseWheelDelegate;
            publishMouseEvents.MouseLeave -= mouseDelegates.MouseLeaveDelegate;
            publishMouseEvents.MouseMiddleButtonDown -= mouseDelegates.MouseMiddleDownDelegate;
            publishMouseEvents.MouseMiddleButtonUp -= mouseDelegates.MouseMiddleUpDelegate;
            foreach ( string key in ( IEnumerable<string> ) MouseManager._modifiersByGroup.Keys )
            {
                if ( MouseManager._modifiersByGroup[ key ].Contains( mouseDelegates.Target ) )
                    MouseManager._modifiersByGroup[ key ].Remove( mouseDelegates.Target );
            }
            mouseDelegates.Target = ( IReceiveMouseEvents ) null;
            mouseDelegates.SynchronizedMouseMove.Dispose();
            mouseDelegates.SynchronizedMouseMove = ( RenderSynchronizedMouseMove ) null;
            this.DelegatesByElement.Remove( element );
            this._subscribersBySource.Remove( element );
        }

        private static void OnMouseEventGroupPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            IReceiveMouseEvents receiveMouseEvents = (IReceiveMouseEvents) d;
            string oldValue = e.OldValue as string;
            if ( oldValue != null && MouseManager._modifiersByGroup.ContainsKey( oldValue ) )
                MouseManager._modifiersByGroup[ oldValue ].Remove( receiveMouseEvents );
            string newValue = e.NewValue as string;
            if ( string.IsNullOrEmpty( newValue ) )
            {
                receiveMouseEvents.MouseEventGroup = ( string ) null;
            }
            else
            {
                if ( !MouseManager._modifiersByGroup.ContainsKey( newValue ) )
                    MouseManager._modifiersByGroup[ newValue ] = ( IList<IReceiveMouseEvents> ) new List<IReceiveMouseEvents>();
                MouseManager._modifiersByGroup[ newValue ].Add( receiveMouseEvents );
                receiveMouseEvents.MouseEventGroup = newValue;
            }
        }

        private static void RaiseTouchDown( ModifierTouchManipulationArgs args, IReceiveMouseEvents target, bool isMaster )
        {
            UltrachartDebugLogger.Instance.WriteLine( "Raising {0}.OnModifierManipulationStarted", ( object ) target.GetType().Name );
            args.IsMaster = isMaster;
            target.OnModifierTouchDown( args );
        }

        private static void RaiseTouchMove( ModifierTouchManipulationArgs args, IReceiveMouseEvents target, bool isMaster )
        {
            UltrachartDebugLogger.Instance.WriteLine( "Raising {0}.OnModifierManipulationDelta", ( object ) target.GetType().Name );
            args.IsMaster = isMaster;
            target.OnModifierTouchMove( args );
        }

        private static void RaiseTouchUp( ModifierTouchManipulationArgs args, IReceiveMouseEvents target, bool isMaster )
        {
            UltrachartDebugLogger.Instance.WriteLine( "Raising {0}.OnModifierManipulationCompleted", ( object ) target.GetType().Name );
            args.IsMaster = isMaster;
            target.OnModifierTouchUp( args );
        }

        private static void RaiseMouseDown( ModifierMouseArgs args, IReceiveMouseEvents target, bool isMaster )
        {
            UltrachartDebugLogger.Instance.WriteLine( "Raising {0}.OnModifierMouseDown", ( object ) target.GetType().Name );
            args.IsMaster = isMaster;
            target.OnModifierMouseDown( args );
        }

        private void RaiseMouseUp( ModifierMouseArgs args, IReceiveMouseEvents target, bool isMaster )
        {
            UltrachartDebugLogger.Instance.WriteLine( "Raising {0}.OnModifierMouseUp", ( object ) target.GetType().Name );
            args.IsMaster = isMaster;
            target.OnModifierMouseUp( args );
        }

        private void RaiseMouseDoubleClick( ModifierMouseArgs args, IReceiveMouseEvents target, bool isMaster )
        {
            UltrachartDebugLogger.Instance.WriteLine( "Raising {0}.OnModifierDoubleClick", ( object ) target.GetType().Name );
            args.IsMaster = isMaster;
            target.OnModifierDoubleClick( args );
            this._lastClickTime = DateTime.MinValue;
            this._lastClickPosition = new Point( -1.0, -1.0 );
        }

        private void RaiseMouseMove( ModifierMouseArgs args, IReceiveMouseEvents target, bool isMaster )
        {
            args.IsMaster = isMaster;
            target.OnModifierMouseMove( args );
        }

        private void RaiseMouseLeave( ModifierMouseArgs args, IReceiveMouseEvents target, bool isMaster )
        {
            UltrachartDebugLogger.Instance.WriteLine( "Raising {0}.OnModifierMouseLeave", ( object ) target.GetType().Name );
            args.IsMaster = isMaster;
            target.OnMasterMouseLeave( args );
        }

        private void RaiseMouseWheel( ModifierMouseArgs args, IReceiveMouseEvents target, bool isMaster )
        {
            UltrachartDebugLogger.Instance.WriteLine( "Raising {0}.OnMasterMouseLeave", ( object ) target.GetType().Name );
            args.IsMaster = isMaster;
            target.OnModifierMouseWheel( args );
        }

        internal IEnumerable<IReceiveMouseEvents> GetTargets( IReceiveMouseEvents target )
        {
            IEnumerable<IReceiveMouseEvents> receiveMouseEventses = Enumerable.Empty<IReceiveMouseEvents>();
            if ( target != null )
            {
                if ( target.MouseEventGroup == null )
                {
                    if ( target.CanReceiveMouseEvents() )
                        receiveMouseEventses = ( IEnumerable<IReceiveMouseEvents> ) new IReceiveMouseEvents[ 1 ]
                        {
              target
                        };
                }
                else
                    receiveMouseEventses = MouseManager._modifiersByGroup[ target.MouseEventGroup ].Where<IReceiveMouseEvents>( ( Func<IReceiveMouseEvents, bool> ) ( md => md.CanReceiveMouseEvents() ) );
            }
            return receiveMouseEventses;
        }

        internal IMousePositionProvider MousePositionProvider
        {
            get; set;
        }

        internal ITouchPositionProvider TouchPositionProvider
        {
            get; set;
        }

        internal IDictionary<IReceiveMouseEvents, MouseDelegates> DelegatesByElement
        {
            get
            {
                return this._delegatesByElement;
            }
        }

        internal IDictionary<object, Point> PreviousMousePositions
        {
            get
            {
                return this._previousMousePositions;
            }
        }

        internal IDictionary<string, IList<IReceiveMouseEvents>> ModifiersByGroup
        {
            get
            {
                return MouseManager._modifiersByGroup;
            }
        }

        internal IDictionary<IReceiveMouseEvents, IPublishMouseEvents> SubscribersBySource
        {
            get
            {
                return this._subscribersBySource;
            }
        }
    }
}
