// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.VisualTreeExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Ecng.Xaml.Charting.Common.Extensions
{
    internal static class VisualTreeExtensions
    {
        internal static IEnumerable<DependencyObject> GetVisualAncestors( this DependencyObject element )
        {
            if ( element == null )
                throw new ArgumentNullException( nameof( element ) );
            return VisualTreeExtensions.GetVisualAncestorsAndSelfIterator( element ).Skip<DependencyObject>( 1 );
        }

        internal static IEnumerable<DependencyObject> GetVisualAncestorsAndSelf( this DependencyObject element )
        {
            if ( element == null )
                throw new ArgumentNullException( nameof( element ) );
            return VisualTreeExtensions.GetVisualAncestorsAndSelfIterator( element );
        }

        private static IEnumerable<DependencyObject> GetVisualAncestorsAndSelfIterator( DependencyObject element )
        {
            DependencyObject obj;
            for ( obj = element ; obj != null ; obj = VisualTreeHelper.GetParent( obj ) )
                yield return obj;
            obj = ( DependencyObject ) null;
        }

        internal static IEnumerable<DependencyObject> GetVisualChildren( this DependencyObject element )
        {
            if ( element == null )
                throw new ArgumentNullException( nameof( element ) );
            return element.GetVisualChildrenAndSelfIterator().Skip<DependencyObject>( 1 );
        }

        internal static IEnumerable<DependencyObject> GetVisualChildrenAndSelf( this DependencyObject element )
        {
            if ( element == null )
                throw new ArgumentNullException( nameof( element ) );
            return element.GetVisualChildrenAndSelfIterator();
        }

        private static IEnumerable<DependencyObject> GetVisualChildrenAndSelfIterator( this DependencyObject element )
        {
            yield return element;
            int count = VisualTreeHelper.GetChildrenCount(element);
            for ( int i = 0 ; i < count ; ++i )
                yield return VisualTreeHelper.GetChild( element, i );
        }

        internal static IEnumerable<DependencyObject> GetVisualDescendants( this DependencyObject element )
        {
            if ( element == null )
                throw new ArgumentNullException( nameof( element ) );
            return VisualTreeExtensions.GetVisualDescendantsAndSelfIterator( element ).Skip<DependencyObject>( 1 );
        }

        internal static IEnumerable<DependencyObject> GetVisualDescendantsAndSelf( this DependencyObject element )
        {
            if ( element == null )
                throw new ArgumentNullException( nameof( element ) );
            return VisualTreeExtensions.GetVisualDescendantsAndSelfIterator( element );
        }

        private static IEnumerable<DependencyObject> GetVisualDescendantsAndSelfIterator( DependencyObject element )
        {
            Queue<DependencyObject> remaining = new Queue<DependencyObject>();
            remaining.Enqueue( element );
            while ( remaining.Count > 0 )
            {
                DependencyObject obj = remaining.Dequeue();
                yield return obj;
                foreach ( DependencyObject visualChild in obj.GetVisualChildren() )
                    remaining.Enqueue( visualChild );
                obj = ( DependencyObject ) null;
            }
        }

        internal static IEnumerable<DependencyObject> GetVisualSiblings( this DependencyObject element )
        {
            return element.GetVisualSiblingsAndSelf().Where<DependencyObject>( ( Func<DependencyObject, bool> ) ( p => p != element ) );
        }

        internal static IEnumerable<DependencyObject> GetVisualSiblingsAndSelf( this DependencyObject element )
        {
            if ( element == null )
                throw new ArgumentNullException( nameof( element ) );
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            if ( parent != null )
                return parent.GetVisualChildren();
            return Enumerable.Empty<DependencyObject>();
        }

        internal static void InvokeOnLayoutUpdated( this FrameworkElement element, Action action )
        {
            if ( element == null )
                throw new ArgumentNullException( nameof( element ) );
            if ( action == null )
                throw new ArgumentNullException( nameof( action ) );
            EventHandler handler = (EventHandler) null;
            handler = ( EventHandler ) ( ( s, e ) =>
            {
                element.LayoutUpdated -= handler;
                action();
            } );
            element.LayoutUpdated += handler;
        }

        internal static IEnumerable<FrameworkElement> GetLogicalChildren( this FrameworkElement parent )
        {
            Popup popup = parent as Popup;
            if ( popup != null )
            {
                FrameworkElement child = popup.Child as FrameworkElement;
                if ( child != null )
                    yield return child;
            }
            ItemsControl itemsControl = parent as ItemsControl;
            if ( itemsControl != null )
            {
                foreach ( FrameworkElement frameworkElement in Enumerable.Range( 0, itemsControl.Items.Count ).Select<int, DependencyObject>( ( Func<int, DependencyObject> ) ( index => itemsControl.ItemContainerGenerator.ContainerFromIndex( index ) ) ).OfType<FrameworkElement>() )
                    yield return frameworkElement;
            }
            string name = parent.Name;
            Queue<FrameworkElement> queue = new Queue<FrameworkElement>(parent.GetVisualChildren().OfType<FrameworkElement>());
            while ( queue.Count > 0 )
            {
                FrameworkElement element = queue.Dequeue();
                if ( element.Parent == parent || element is UserControl )
                {
                    yield return element;
                }
                else
                {
                    foreach ( FrameworkElement frameworkElement in element.GetVisualChildren().OfType<FrameworkElement>() )
                        queue.Enqueue( frameworkElement );
                }
            }
        }

        internal static Color WithAlpha( this Color color, byte alpha )
        {
            return Color.FromArgb( alpha, color.R, color.G, color.B );
        }
    }
}
