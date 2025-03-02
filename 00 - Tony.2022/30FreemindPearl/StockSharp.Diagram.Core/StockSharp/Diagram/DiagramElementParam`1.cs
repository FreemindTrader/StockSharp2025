using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace StockSharp.Diagram
{
    /// <summary>The diagram element parameter.</summary>
    /// <typeparam name="T">Value type.</typeparam>
    public class DiagramElementParam<T> : NotifiableObject, IDiagramElementParam, IPersistable, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private bool _canChangeValue = true;

        private readonly List<Attribute> _attributes = new List<Attribute>();

        private bool _bNotifyOnChanged = true;

        private string _name;

        private string _displayName;

        private string _description;

        private string _category;

        private T _value;

        private bool _isDefault;

        private bool _isParam;

        private bool _bIgnoreOnSave;

        private Func<T, SettingsStorage> _saveHandlerFunc;

        private Func<SettingsStorage, T> _loadHandlerFunc;

        /// <summary>The parameter value change start event.</summary>
        public event Action<T, T> ValueChanging;

        /// <summary>The parameter value change event.</summary>
        public event Action<T> ValueChanged;

        /// <inheritdoc />
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
                this.NotifyChanged( nameof( Name ) );
            }
        }

        /// <inheritdoc />
        public string DisplayName
        {
            get
            {
                return this._displayName;
            }
            set
            {
                this._displayName = value;
                this.NotifyChanged( nameof( DisplayName ) );
            }
        }

        /// <inheritdoc />
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
                this.NotifyChanged( nameof( Description ) );
            }
        }

        /// <inheritdoc />
        public string Category
        {
            get
            {
                return this._category;
            }
            set
            {
                this._category = value;
                this.NotifyChanged( nameof( Category ) );
            }
        }

        /// <inheritdoc />
        public Type Type
        {
            get
            {
                return typeof( T );
            }
        }

        /// <summary>Can change value.</summary>
        public bool CanChangeValue
        {
            get
            {
                return this._canChangeValue;
            }
            set
            {
                this._canChangeValue = value;
            }
        }

        /// <inheritdoc />
        public IList<Attribute> Attributes
        {
            get
            {
                return ( IList<Attribute> )this._attributes;
            }
        }

        /// <summary>The parameter value.</summary>
        public T Value
        {
            get
            {
                return this._value;
            }
            set
            {
                if ( !this.CanChangeValue || EqualityComparer<T>.Default.Equals( this._value, value ) )
                    return;
                if ( this.NotifyOnChanged )
                    this.NotifyChanging( nameof( Value ) );
                Action<T, T> z6QfjXzs = this.ValueChanging;
                if ( z6QfjXzs != null )
                    z6QfjXzs( this._value, value );
                this.IgnoreOnSave = false;
                this._value = value;
                this._isDefault = true;

                Action<T> zFadtpL8 = this.ValueChanged;
                if ( zFadtpL8 != null )
                    zFadtpL8( this._value );
                if ( !this.NotifyOnChanged )
                    return;
                this.NotifyChanged( nameof( Value ) );
            }
        }

        /// <inheritdoc />
        public bool IsDefault
        {
            get
            {
                return !this._isDefault;
            }
        }

        /// <inheritdoc />
        public bool IsParam
        {
            get
            {
                return this._isParam;
            }
            set
            {
                this._isParam = value;
            }
        }

        /// <inheritdoc />
        public bool IgnoreOnSave
        {
            get
            {
                return this._bIgnoreOnSave;
            }
            set
            {
                this._bIgnoreOnSave = value;
            }
        }

        /// <inheritdoc />
        public void SetValueWithIgnoreOnSave( object value )
        {
            this.IgnoreOnSave = true;
            this.Value = ( T )value;
            this.IgnoreOnSave = true;
        }

        /// <inheritdoc />
        public bool NotifyOnChanged
        {
            get
            {
                return this._bNotifyOnChanged;
            }
            set
            {
                this._bNotifyOnChanged = value;
            }
        }

        object IDiagramElementParam.Value
        {
            get
            {
                return ( object )this.Value;
            }

            set
            {
                this.Value = ( T )value;
            }

        }



        /// <summary>The parameter value saving handler.</summary>
        public Func<T, SettingsStorage> SaveHandler
        {
            get
            {
                return this._saveHandlerFunc;
            }
            set
            {
                this._saveHandlerFunc = value;
            }
        }

        /// <summary>The parameter value loading handler.</summary>
        public Func<SettingsStorage, T> LoadHandler
        {
            get
            {
                return this._loadHandlerFunc;
            }
            set
            {
                this._loadHandlerFunc = value;
            }
        }

        /// <summary>Load settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Load( SettingsStorage storage )
        {
            if ( this.LoadHandler != null )
            {
                storage.CheckParameters<SettingsStorage>( nameof( Value), new Action<SettingsStorage>( this.SomeShitMethod03 ), true );
            }
            else if ( typeof( T ).IsPersistable() )
            {
                storage.CheckParameters<SettingsStorage>( nameof( Value), new Action<SettingsStorage>( this.SomeShitMethod04 ), true );
            }                
            else
            {
                this.Value = storage.GetValue<T>( nameof( Value), default( T ) );
            }
                
        }

        /// <summary>Save settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public void Save( SettingsStorage storage )
        {
            if ( this.SaveHandler != null )
            {
                storage.SetValue<SettingsStorage>( nameof( Value), this.SaveHandler( this.Value ) );
            }
            else
            {
                IPersistable persistable = ( object )this.Value as IPersistable;
                if ( persistable != null )
                {
                    if ( this.Value.IsNull<T>() )
                        return;
                    storage.SetValue<SettingsStorage>( nameof( Value), persistable.SaveEntire( false ) );
                }
                else
                    storage.SetValue<T>( nameof( Value), this.Value );
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format( nameof( -1260199384 ), ( object )this.Name, ( object )this.Value );
        }

        private void SomeShitMethod03( SettingsStorage _param1 )
        {
            this.Value = _param1 == null ? default( T ) : this.LoadHandler( _param1 );
        }

        private void SomeShitMethod04( SettingsStorage _param1 )
        {
            this.Value = _param1 == null ? default( T ) : ( T )_param1.LoadEntire<IPersistable>();
        }
    }
}
