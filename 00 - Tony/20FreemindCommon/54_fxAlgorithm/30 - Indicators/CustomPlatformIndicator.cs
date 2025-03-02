using StockSharp.BusinessEntities;
using System;
using System.Reflection;

namespace fx.Algorithm
{
    /// <summary>
    /// Custom platform indicators are not automatically imported, they are conventional indicator classes. So we need
    /// to gather their properties for the automated properties display system to work.
    /// </summary>
    [Serializable]
    public abstract class CustomPlatformIndicator : PlatformIndicator
    {        
        protected TimeSpan _currentPeriod = TimeSpan.MinValue;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomPlatformIndicator( string name, bool isIndicatorVisible, bool? isTradeable, bool? isShowInMasterPane, string[ ] resultSetNames ) : base( name, isIndicatorVisible, isTradeable, isShowInMasterPane, resultSetNames )
        {
            // Collect properties and supply them to the indicator parameters class.
            Type type = GetType( );
            foreach( PropertyInfo info in type.GetProperties( ) )
            {
                if( info.CanRead &&
                     info.CanWrite )
                {
                    _parameters.SetCore( info.Name, info.GetValue( this, null ) );
                }
            }

            _parameters.IndicatorParameterUpdatedEvent += new IndicatorParameters.ParameterUpdatedValueDelegate( _parameters_ParameterUpdatedValueEvent );
        }

        public override void OnDeserialization( object sender )
        {
            base.OnDeserialization( sender );
            _parameters.IndicatorParameterUpdatedEvent += new IndicatorParameters.ParameterUpdatedValueDelegate( _parameters_ParameterUpdatedValueEvent );
        }

        /// <summary>
        /// This is needed to capture the value changes coming from the user and apply them to the properties we
        /// extracted them from.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private void _parameters_ParameterUpdatedValueEvent( string name, object value )
        {
            foreach( PropertyInfo info in GetType( ).GetProperties( ) )
            {
                if( info.Name == name )
                {
                    info.SetValue( this, value, null );
                    break;
                }
            }
        }
    }
}