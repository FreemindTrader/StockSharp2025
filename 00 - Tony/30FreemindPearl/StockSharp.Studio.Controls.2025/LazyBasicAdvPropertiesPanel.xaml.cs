// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LazyBasicAdvPropertiesPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.PropertyGrid;
using Ecng.Collections;
using Ecng.Serialization;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Studio.Controls
{
    public partial class LazyBasicAdvPropertiesPanel : ContentControl, IPersistable
    {
        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register("SelectedObject", typeof(object), typeof(LazyBasicAdvPropertiesPanel), new PropertyMetadata(null, delegate(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            LazyBasicAdvPropertiesPanel lazyBasicAdvPropertiesPanel = s as LazyBasicAdvPropertiesPanel;
            if (lazyBasicAdvPropertiesPanel == null)
            {
                return;
            }
            lazyBasicAdvPropertiesPanel.Underlying.SelectedObject = e.NewValue;
        }));
        private bool _isReadOnly;
        private bool _showGridLines;
        private bool _postImmediately;
        private SettingsStorage _delayedStorage;
        
        public LazyBasicAdvPropertiesPanel()
        {
            this.InitializeComponent();
        }

        public object SelectedObject
        {
            get
            {
                return this.GetValue( LazyBasicAdvPropertiesPanel.SelectedObjectProperty );
            }
            set
            {
                this.SetValue( LazyBasicAdvPropertiesPanel.SelectedObjectProperty, value );
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this._isReadOnly;
            }
            set
            {
                this._isReadOnly = value;
                if ( this.Content == null )
                    return;
                this.Underlying.IsReadOnly = value;
            }
        }

        public bool ShowGridLines
        {
            get
            {
                return this._showGridLines;
            }
            set
            {
                this._showGridLines = value;
                if ( this.Content == null )
                    return;
                this.Underlying.ShowGridLines = value;
            }
        }

        public bool PostImmediately
        {
            get
            {
                return this._postImmediately;
            }
            set
            {
                this._postImmediately = value;
                if ( this.Content == null )
                    return;
                this.Underlying.PostImmediately = value;
            }
        }

        public event CustomExpandEventHandler CustomExpand;

        private void OnUnderlyingCustomExpand( object sender, CustomExpandEventArgs args )
        {
            CustomExpandEventHandler customExpand = this.CustomExpand;
            if ( customExpand == null )
                return;
            customExpand( sender, args );
        }

        private BasicAdvPropertiesPanel Underlying
        {
            get
            {
                if ( this.Content == null )
                {
                    object obj;
                    this.Content =  ( BasicAdvPropertiesPanel ) ( obj =  create() );
                }
                BasicAdvPropertiesPanel content = (BasicAdvPropertiesPanel) this.Content;
                if ( this._delayedStorage != null )
                {
                    PersistableHelper.ForceLoad<BasicAdvPropertiesPanel>(  content, this._delayedStorage );
                    this._delayedStorage = ( SettingsStorage ) null;
                }
                return content;

                BasicAdvPropertiesPanel create()
                {
                    BasicAdvPropertiesPanel advPropertiesPanel = new BasicAdvPropertiesPanel();
                    advPropertiesPanel.ShowGridLines = this.ShowGridLines;
                    advPropertiesPanel.IsReadOnly = this.IsReadOnly;
                    advPropertiesPanel.CustomExpand += new CustomExpandEventHandler( this.OnUnderlyingCustomExpand );
                    return advPropertiesPanel;
                }
            }
        }

        void IPersistable.Load( SettingsStorage storage )
        {
            if ( this.Content == null )
            {
                SettingsStorage settingsStorage = new SettingsStorage();
                using ( IEnumerator<KeyValuePair<string, object>> enumerator = ( ( SynchronizedDictionary<string, object> ) storage ).GetEnumerator() )
                {
                    while ( enumerator.MoveNext() )
                    {
                        KeyValuePair<string, object> current = enumerator.Current;
                        ( ( SynchronizedDictionary<string, object> ) settingsStorage ).Add( current );
                    }
                }
                this._delayedStorage = settingsStorage;
            }
            else
                PersistableHelper.ForceLoad<BasicAdvPropertiesPanel>(  this.Underlying, storage );
        }

        void IPersistable.Save( SettingsStorage storage )
        {
            if ( this._delayedStorage != null )
                CollectionHelper.AddRange<KeyValuePair<string, object>>(  storage,  this._delayedStorage );
            else
                ( ( IPersistable ) this.Underlying ).Save( storage );
        }

        
    }
}
