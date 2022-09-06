// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Controls.AutoRefreshCollectionViewSource
// Assembly: Hydra, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BCB1262-CD24-4283-B1AE-24E756F47247
// Assembly location: T:\00-StockSharp\Data\Hydra.dll

using Ecng.Common;
using StockSharp.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace StockSharp.Hydra.Controls
{
    public class AutoRefreshCollectionViewSource : CollectionViewSource
    {
        protected override void OnSourceChanged( object oldSource, object newSource )
        {
            if ( oldSource != null )
                UnsubscribeSourceEvents( oldSource );
            if ( newSource != null )
                SubscribeSourceEvents( newSource );
            base.OnSourceChanged( oldSource, newSource );
        }

        private void UnsubscribeSourceEvents( object source )
        {
            INotifyCollectionChanged collectionChanged = source as INotifyCollectionChanged;
            if ( collectionChanged != null )
                collectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler( OnSourceCollectionChanged );
            IEnumerable items = source as IEnumerable;
            if ( items == null )
                return;
            UnsubscribeItemsEvents( items );
        }

        private void SubscribeSourceEvents( object source )
        {
            INotifyCollectionChanged collectionChanged = source as INotifyCollectionChanged;
            if ( collectionChanged != null )
                collectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler( OnSourceCollectionChanged );
            IEnumerable items = source as IEnumerable;
            if ( items == null )
                return;
            SubscribeItemsEvents( items );
        }

        private void UnsubscribeItemEvents( object item )
        {
            INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
            if ( notifyPropertyChanged == null )
                return;
            notifyPropertyChanged.PropertyChanged -= new PropertyChangedEventHandler( OnItemPropertyChanged );
        }

        private void SubscribeItemEvents( object item )
        {
            INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
            if ( notifyPropertyChanged == null )
                return;
            notifyPropertyChanged.PropertyChanged += new PropertyChangedEventHandler( OnItemPropertyChanged );
        }

        private void UnsubscribeItemsEvents( IEnumerable items )
        {
            foreach ( object obj in items )
                UnsubscribeItemEvents( obj );
        }

        private void SubscribeItemsEvents( IEnumerable items )
        {
            foreach ( object obj in items )
                SubscribeItemEvents( obj );
        }

        private void OnSourceCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.Action == NotifyCollectionChangedAction.Reset )
                throw new InvalidOperationException( "The action {0} is not supported by {1}".Translate( "en", null ).Put( e.Action, GetType() ) );
            if ( e.NewItems != null )
                SubscribeItemsEvents( e.NewItems );
            if ( e.OldItems == null )
                return;
            UnsubscribeItemsEvents( e.OldItems );
        }

        private void OnItemPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( !IsViewRefreshNeeded( e.PropertyName ) )
                return;
            ICollectionView view = View;
            if ( view == null )
                return;
            object currentItem = view.CurrentItem;
            IEditableCollectionView ieditableCollectionView = view as IEditableCollectionView;
            if ( ieditableCollectionView != null )
            {
                ieditableCollectionView.EditItem( sender );
                ieditableCollectionView.CommitEdit();
            }
            else
                view.Refresh();
            view.MoveCurrentTo( currentItem );
        }

        private bool IsViewRefreshNeeded( string propertyName )
        {
            if ( !SortDescriptions.Any( sort => string.Equals( sort.PropertyName, propertyName ) ) )
                return GroupDescriptions.OfType<PropertyGroupDescription>().Any( g => string.Equals( g.PropertyName, propertyName ) );
            return true;
        }
    }
}
