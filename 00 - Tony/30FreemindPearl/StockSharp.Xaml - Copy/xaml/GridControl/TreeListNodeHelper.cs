
using DevExpress.Data.TreeList;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace StockSharp.Xaml.GridControl
{
    public class TreeListNodeHelper
    {
        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.RegisterAttached("IsExpanded", typeof (bool), typeof (TreeListNodeHelper), (PropertyMetadata) new UIPropertyMetadata((object) false, new PropertyChangedCallback(OnPropertyChanged), new CoerceValueCallback(OnCoerceValueChanged)));

        private static void OnPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( TreeListNodeBase ) ( ( TreeListRowData ) ( ( FrameworkElement ) d ).DataContext ).Node ).IsExpanded = ( ( bool ) e.NewValue ); 
        }

        private static object OnCoerceValueChanged( DependencyObject d, object object_0 )
        {
            ( ( TreeListNodeBase ) ( ( TreeListRowData ) ( ( FrameworkElement ) d ).DataContext ).Node ).PropertyChanged += TreeListNodeHelper_PropertyChanged;
            return object_0;
        }

        private static void TreeListNodeHelper_PropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName != "IsExpanded" )
            {
                return;
            }

            var treeListNode = (TreeListNode) sender;
            var content = treeListNode.Content;

            if ( CallSiteHelper.GlobalCallSite == null )
            {
                CallSiteHelper.GlobalCallSite = CallSite<Func<CallSite, object, bool, object>>.Create( Binder.SetMember( CSharpBinderFlags.None, "IsExpanded", typeof( TreeListNodeHelper ), 
                    new CSharpArgumentInfo[ 2 ]
                    {
                        CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.None, null),
                        CSharpArgumentInfo.Create( CSharpArgumentInfoFlags.UseCompileTimeType, null)
                } ) );
            }

            object obj = CallSiteHelper.GlobalCallSite.Target( CallSiteHelper.GlobalCallSite, content, treeListNode.IsExpanded );
        }

        

        public static bool GetIsExpanded( DependencyObject obj )
        {
            return ( bool ) obj.GetValue( IsExpandedProperty );
        }

        public static void SetIsExpanded( DependencyObject obj, bool value )
        {
            obj.SetValue( IsExpandedProperty, ( object ) value );
        }

        private static class CallSiteHelper
        {
            public static CallSite<Func<CallSite, object, bool, object>> GlobalCallSite;
        }
    }
}
