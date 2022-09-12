using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Wintellect.PowerCollections;

namespace StockSharp.Xaml
{
    public class OrderConditionalGrid : OrderGrid
    {
        private static readonly string[] _defaultVisibleColumns = { "StopPrice", "Type" };
        private readonly SynchronizedSet<Type> _conditionTypes = new SynchronizedSet<Type>();
        private readonly IList<GridColumn> _serializableColumns;

        public OrderConditionalGrid( )
        {
            _serializableColumns = Columns.ToList();
        }

        protected override void OnOrderAdded( Order order )
        {
            if ( order.Type != OrderTypes.Conditional )
                return;

            Type conditionType;

            var condition = order.Condition;
            if ( condition == null )
            {
                return;
            }

            lock ( _conditionTypes.SyncRoot )
            {
                conditionType = condition.GetType();

                if ( _conditionTypes.Contains( conditionType ) )
                {
                    return;
                }

                _conditionTypes.Add( conditionType );
            }
            GuiDispatcher.GlobalDispatcher.AddAction( ( ) => AddColumns( conditionType ) );
        }        

        private void AddColumns( Type conditionType )
        {
            var properties = conditionType.GetProperties();

            foreach ( PropertyInfo property in properties )
            {                
                if ( !property.Name.CompareIgnoreCase( "Parameters" ) )
                {
                    var name = "Order.Condition." + property.Name;
                    if ( !_serializableColumns.Any<GridColumn>( col => col.FieldName.CompareIgnoreCase( name ) ) )
                    {
                        var displayNameAttr = property.GetCustomAttributes(typeof (DisplayNameAttribute), false).OfType<DisplayNameAttribute>().FirstOrDefault();
                        GridColumn column = new GridColumn();
                        column.FieldName =  name;
                        column.Header = ( displayNameAttr != null ? displayNameAttr.DisplayName : property.Name );

                        GridColumn gridColumn2 = column;
                        if ( ! _defaultVisibleColumns.Contains<string>( property.Name ) )
                        {
                            column.Visible = false;
                        } 
                        
                        _serializableColumns.Add( column );
                    }
                }
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<string[ ]>( "ConditionTypes",  _conditionTypes.Select( x => x.GetTypeName( false ) ).ToArray() );
        }

        public override void Load( SettingsStorage storage )
        {
            _conditionTypes.Clear();
            _conditionTypes.AddRange( storage.GetValue<IEnumerable<string>>( "ConditionTypes"  ).Select( s => s.To<Type>() ) );
            _conditionTypes.ForEach( new Action<Type>( AddColumns ) );

            base.Load( storage );
        }        
    }
}
