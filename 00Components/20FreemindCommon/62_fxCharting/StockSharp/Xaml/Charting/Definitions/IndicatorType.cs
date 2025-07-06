//using Ecng.Common;
//using Ecng.ComponentModel;
//using Ecng.Serialization;
//using StockSharp.Algo.Indicators;
//using System;

//namespace StockSharp.Xaml.Charting
//{
//    public class IndicatorType : Equatable< IndicatorType >, IPersistable
//    {
//        private Type _indicatorType;
//        private string _name;
//        private string _description;
//        private Type _painterType;
//        private Type _inputValueType;
//        private Type _outputValueType;

//        public IndicatorType( )
//        {
//        }

//        public IndicatorType( Type indicator, Type painter )
//        {
//            if( indicator == null )
//            {
//                throw new ArgumentNullException( nameof( indicator ) );
//            }

//            Indicator = indicator;
//            Painter   = painter;
//        }

//        public string Name
//        {
//            get
//            {
//                return _name;
//            }
//            private set
//            {
//                _name = value;
//            }
//        }

//        public string Description
//        {
//            get
//            {
//                return _description;
//            }
//            private set
//            {
//                _description = value;
//            }
//        }

//        public Type Indicator
//        {
//            get
//            {
//                return _indicatorType;
//            }

//            set
//            {
//                _indicatorType = value;
//                Name = value != null ? _indicatorType.GetDisplayName( null ) : string.Empty;
//                Description = value != null ? _indicatorType.GetDescription( null ) : string.Empty;

//                InputValue = ( value != null ? IndicatorHelper.GetValueType( _indicatorType, true ) : null );
//                OutputValue = ( value != null ? IndicatorHelper.GetValueType( _indicatorType, false ) : null );
//            }
//        }

//        
//        public Type Painter
//        {
//            get
//            {
//                return _painterType;
//            }

//            set
//            {
//                _painterType = value;
//            }
//        }

//        

//        public Type InputValue
//        {
//            get
//            {
//                return _inputValueType;
//            }

//            set
//            {
//                _inputValueType = value;
//            }
//        }

//       

//        public Type OutputValue
//        {
//            get
//            {
//                return _outputValueType;
//            }

//            set
//            {
//                _outputValueType = value;
//            }
//        }

//        

//        public override IndicatorType Clone( )
//        {
//            IndicatorType instance = GetType( ).CreateInstance< IndicatorType >( );
//            instance.Load( this.Save( ) );
//            return instance;
//        }

//        protected override bool OnEquals( IndicatorType other )
//        {
//            return Indicator == other.Indicator;
//        }

//        public void Load( SettingsStorage storage )
//        {
//            if( storage.ContainsKey( "Indicator" ) )
//            {
//                Indicator = storage.GetValue< string >( "Indicator", null ).To< Type >( );
//            }
//            
//            if( storage.ContainsKey( "Painter" ) )
//            {
//                Painter = storage.GetValue< string >( "Painter", null ).To< Type >( );
//            }
//            
//            if( storage.ContainsKey( "InputValue" ) )
//            {
//                InputValue = storage.GetValue< string >( "InputValue", null ).To< Type >( );
//            }
//            
//            if( !storage.ContainsKey( "OutputValue" ) )
//            {
//                return;
//            }

//            OutputValue = storage.GetValue< string >( "OutputValue", null ).To< Type >( );
//        }

//        public void Save( SettingsStorage storage )
//        {
//            if( Indicator != null )
//            {
//                storage.SetValue( "Indicator", Indicator.GetTypeName( false ) );
//            }
//            if( Painter != null )
//            {
//                storage.SetValue( "Painter", Painter.GetTypeName( false ) );
//            }
//            if( InputValue != null )
//            {
//                storage.SetValue( "InputValue", InputValue.GetTypeName( false ) );
//            }
//            if( !( OutputValue != null ) )
//            {
//                return;
//            }
//            storage.SetValue( "OutputValue", OutputValue.GetTypeName( false ) );
//        }
//    }
//}
