using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    public sealed class PropertyChangeNotifier : DependencyObject, IDisposable
    {
        /// <summary>
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register( nameof( Value ), typeof( object ), typeof( PropertyChangeNotifier ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )null, new PropertyChangedCallback( OnValuePropertyChanged ) ) );

        private readonly WeakReference _weakRef;

        /// <summary>
        /// </summary>
        public PropertyChangeNotifier( DependencyObject propertySource, string path )
          : this( propertySource, new PropertyPath( path, Array.Empty<object>() ) )
        {
        }

        /// <summary>
        /// </summary>
        public PropertyChangeNotifier( DependencyObject propertySource, DependencyProperty property )
          : this( propertySource, new PropertyPath( ( object )property ) )
        {
        }

        /// <summary>
        /// </summary>
        public PropertyChangeNotifier( DependencyObject propertySource, PropertyPath property )
        {
            if ( propertySource == null )
                throw new ArgumentNullException( nameof( propertySource ) );
            if ( property == null )
                throw new ArgumentNullException( nameof( property ) );
            this._weakRef = new WeakReference( ( object )propertySource );
            Binding binding = new Binding()
            {
                Path = property,
                Mode = BindingMode.OneWay,
                Source = ( object )propertySource
            };
            BindingOperations.SetBinding( ( DependencyObject )this, PropertyChangeNotifier.ValueProperty, ( BindingBase )binding );
        }

        private static void OnValuePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            Action changed = ( ( PropertyChangeNotifier )d ).ValueChanged;
            if ( changed == null )
                return;
            changed();
        }

        /// <summary>
        /// </summary>
        [Bindable( true )]
        public object Value
        {
            get
            {
                return this.GetValue( PropertyChangeNotifier.ValueProperty );
            }
            set
            {
                this.SetValue( PropertyChangeNotifier.ValueProperty, value );
            }
        }

        /// <summary>
        /// </summary>
        public DependencyObject PropertySource
        {
            get
            {
                try
                {
                    return this._weakRef.IsAlive ? this._weakRef.Target as DependencyObject : ( DependencyObject )null;
                }
                catch
                {
                    return ( DependencyObject )null;
                }
            }
        }

        /// <summary>
        /// </summary>
        public event Action ValueChanged;

        /// <inheritdoc />
        public void Dispose()
        {
            BindingOperations.ClearBinding( ( DependencyObject )this, PropertyChangeNotifier.ValueProperty );
        }
    }
}
