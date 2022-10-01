using Ecng.Common;
using System;
using System.Windows;

namespace Ecng.Xaml
{
    public class MessageBoxBuilder
    {
        private static IMessageBoxHandler _defaultHandler = ( IMessageBoxHandler )new MessageBoxBuilder.WpfMessageBoxHandler();
        private IMessageBoxHandler _handler = MessageBoxBuilder.DefaultHandler;
        private Window _owner;
        private string _text;
        private string _caption;
        private MessageBoxButton _button;
        private MessageBoxImage _icon;
        private MessageBoxResult _defaultResult;
        private MessageBoxOptions _options;

        public static IMessageBoxHandler DefaultHandler
        {
            get
            {
                return MessageBoxBuilder._defaultHandler;
            }
            set
            {
                if ( value == null )
                    throw new ArgumentNullException( nameof( value ) );
                MessageBoxBuilder._defaultHandler = value;
            }
        }

        public MessageBoxBuilder Handler( IMessageBoxHandler handler )
        {
            if ( handler == null )
                throw new ArgumentNullException( nameof( handler ) );
            this._handler = handler;
            return this;
        }

        public MessageBoxBuilder Owner( DependencyObject owner )
        {
            this._owner = owner.GetWindow();
            return this;
        }

        public MessageBoxBuilder Owner( Window owner )
        {
            this._owner = owner;
            return this;
        }

        public MessageBoxBuilder Text( string text )
        {
            this._text = text;
            return this;
        }

        public MessageBoxBuilder Caption( string caption )
        {
            this._caption = caption;
            return this;
        }

        public MessageBoxBuilder Error( Exception error )
        {
            if ( error == null )
                throw new ArgumentNullException( nameof( error ) );
            return this.Text( error.ToString() ).Icon( MessageBoxImage.Hand );
        }

        public MessageBoxBuilder Error()
        {
            return this.Icon( MessageBoxImage.Hand );
        }

        public MessageBoxBuilder Warning()
        {
            return this.Icon( MessageBoxImage.Exclamation );
        }

        public MessageBoxBuilder Info()
        {
            return this.Icon( MessageBoxImage.Asterisk );
        }

        public MessageBoxBuilder Question()
        {
            return this.Icon( MessageBoxImage.Question );
        }

        public MessageBoxBuilder Icon( MessageBoxImage icon )
        {
            this._icon = icon;
            return this;
        }

        public MessageBoxBuilder YesNo()
        {
            return this.Button( MessageBoxButton.YesNo );
        }

        public MessageBoxBuilder OkCancel()
        {
            return this.Button( MessageBoxButton.OKCancel );
        }

        public MessageBoxBuilder Button( MessageBoxButton button )
        {
            this._button = button;
            return this;
        }

        public MessageBoxBuilder Options( MessageBoxOptions options )
        {
            this._options = options;
            return this;
        }

        public MessageBoxBuilder DefaultResult( MessageBoxResult defaultResult )
        {
            this._defaultResult = defaultResult;
            return this;
        }

        public MessageBoxResult Show()
        {
            string caption = this._caption ?? TypeHelper.ApplicationName;
            if ( this._owner != null )
                return this._handler.Show( this._owner, this._text, caption, this._button, this._icon, this._defaultResult, this._options );
            return this._handler.Show( this._text, caption, this._button, this._icon, this._defaultResult, this._options );
        }

        public class WpfMessageBoxHandler : IMessageBoxHandler
        {
            MessageBoxResult IMessageBoxHandler.Show( string text, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options )
            {
                return MessageBox.Show( text, caption, button, icon, defaultResult, options );
            }

            MessageBoxResult IMessageBoxHandler.Show( Window owner, string text, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options )
            {
                return MessageBox.Show( owner, text, caption, button, icon, defaultResult, options );
            }
        }
    }
}
