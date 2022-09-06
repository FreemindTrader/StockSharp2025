
using Ecng.Collections;
using Ecng.Serialization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Diagram
{
    /// <summary>
    /// Extension class for <see cref="T:StockSharp.Diagram.IDiagramElementParam" />.
    /// </summary>
    public static class DiagramElementParamHelper
    {
        /// <summary>
        /// To set the <see cref="T:System.ComponentModel.ExpandableObjectConverter" /> attribute for the diagram element parameter.
        /// </summary>
        /// <typeparam name="TParam">The diagram element parameter type.</typeparam>
        /// <param name="param">The diagram element parameter.</param>
        /// <returns>The diagram element parameter.</returns>
        public static TParam SetExpandable<TParam>( this TParam param ) where TParam : IDiagramElementParam
        {
            if ( ( object )param == null )
                throw new ArgumentNullException( "param == null" );
            param.Attributes.Add( ( Attribute )new TypeConverterAttribute( typeof( ExpandableObjectConverter ) ) );
            return param;
        }

        /// <summary>
        /// To add the attribute <see cref="T:System.Attribute" /> for the diagram element parameter.
        /// </summary>
        /// <typeparam name="TParam">The diagram element parameter type.</typeparam>
        /// <param name="param">The diagram element parameter.</param>
        /// <param name="editor">Attribute.</param>
        /// <returns>The diagram element parameter.</returns>
        public static TParam SetEditor<TParam>( this TParam param, Attribute editor ) where TParam : IDiagramElementParam
        {
            if ( ( object )param == null )
                throw new ArgumentNullException( "param == null" );
            if ( editor == null )
                throw new ArgumentNullException( "editor == null" );
            param.Attributes.Add( editor );
            return param;
        }

        /// <summary>
        /// To set the <see cref="T:System.ComponentModel.DataAnnotations.DisplayAttribute" /> attribute for the diagram element parameter.
        /// </summary>
        /// <typeparam name="TParam">The diagram element parameter type.</typeparam>
        /// <param name="param">The diagram element parameter.</param>
        /// <param name="groupName">The category of the diagram element parameter.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="description">The description of the diagram element parameter.</param>
        /// <param name="order">The property order.</param>
        /// <returns>The diagram element parameter.</returns>
        public static TParam SetDisplay<TParam>(
          this TParam param,
          string groupName,
          string displayName,
          string description,
          int order )
          where TParam : IDiagramElementParam
        {
            if ( ( object )param == null )
                throw new ArgumentNullException( "param == null" );
            param.Category = groupName;
            param.DisplayName = displayName;
            param.Description = description;
            param.Attributes.Add( ( Attribute )new DisplayAttribute()
            {
                Name = displayName,
                Description = description,
                GroupName = groupName,
                Order = order
            } );
            return param;
        }

        /// <summary>
        /// To set the <see cref="T:System.ComponentModel.ReadOnlyAttribute" /> attribute for the diagram element parameter.
        /// </summary>
        /// <typeparam name="TParam">The diagram element parameter type.</typeparam>
        /// <param name="param">The diagram element parameter.</param>
        /// <param name="readOnly">Read-only.</param>
        /// <returns>The diagram element parameter.</returns>
        public static TParam SetReadOnly<TParam>( this TParam param, bool readOnly = true ) where TParam : IDiagramElementParam
        {
            if ( ( object )param == null )
                throw new ArgumentNullException( "param == null" );
            if ( !readOnly )
                param.RemoveAttribute<TParam, ReadOnlyAttribute>();
            else
                param.Attributes.Add( ( Attribute )new ReadOnlyAttribute( true ) );
            return param;
        }

        /// <summary>
        /// To set the <see cref="T:System.ComponentModel.BrowsableAttribute" /> attribute for the diagram element parameter.
        /// </summary>
        /// <typeparam name="TParam">The diagram element parameter type.</typeparam>
        /// <param name="param">The diagram element parameter.</param>
        /// <param name="browsable">Visible parameter.</param>
        /// <returns>The diagram element parameter.</returns>
        public static TParam SetBrowsable<TParam>( this TParam param, bool browsable = true ) where TParam : IDiagramElementParam
        {
            if ( ( object )param == null )
                throw new ArgumentNullException( "param == null" );
            if ( browsable )
                param.RemoveAttribute<TParam, BrowsableAttribute>();
            else
                param.Attributes.Add( ( Attribute )new BrowsableAttribute( false ) );
            return param;
        }

        /// <summary>
        /// To set the handler at the start of the value change for the diagram element parameter.
        /// </summary>
        /// <typeparam name="TValue">The diagram element parameter type.</typeparam>
        /// <param name="param">The diagram element parameter.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>The diagram element parameter.</returns>
        public static DiagramElementParam<TValue> SetOnValueChangingHandler<TValue>(
          this DiagramElementParam<TValue> param,
          Action<TValue, TValue> handler )
        {
            if ( param == null )
                throw new ArgumentNullException( "param == null" );
            if ( handler == null )
                throw new ArgumentNullException( "handler == null" );
            param.ValueChanging += handler;
            return param;
        }

        /// <summary>
        /// To set the handler to the value change for the diagram element parameter.
        /// </summary>
        /// <typeparam name="TValue">The diagram element parameter type.</typeparam>
        /// <param name="param">The diagram element parameter.</param>
        /// <param name="handler">The handler of the diagram element value change.</param>
        /// <returns>The diagram element parameter.</returns>
        public static DiagramElementParam<TValue> SetOnValueChangedHandler<TValue>(
          this DiagramElementParam<TValue> param,
          Action<TValue> handler )
        {
            if ( param == null )
                throw new ArgumentNullException( "param == null" );
            if ( handler == null )
                throw new ArgumentNullException( "handler == null" );
            param.ValueChanged += handler;
            return param;
        }

        /// <summary>
        /// To set the handler of saving/loading for the diagram element parameter.
        /// </summary>
        /// <typeparam name="TValue">The diagram element parameter type.</typeparam>
        /// <param name="param">The diagram element parameter.</param>
        /// <param name="save">The handler for the parameter saving.</param>
        /// <param name="load">The handler for the parameter loading.</param>
        /// <returns>The diagram element parameter.</returns>
        public static DiagramElementParam<TValue> SetSaveLoadHandlers<TValue>(
          this DiagramElementParam<TValue> param,
          Func<TValue, SettingsStorage> save,
          Func<SettingsStorage, TValue> load )
        {
            if ( param == null )
                throw new ArgumentNullException( "param == null" );


            if ( save == null )
                throw new ArgumentNullException( "save == null" );
            param.SaveHandler = save;


            if ( load == null )
                throw new ArgumentNullException( "load == null" );
            param.LoadHandler = load;
            return param;
        }

        /// <summary>
        /// To modify <see cref="P:StockSharp.Diagram.IDiagramElementParam.IsParam" />.
        /// </summary>
        /// <typeparam name="TParam">The diagram element parameter type.</typeparam>
        /// <param name="param">The diagram element parameter.</param>
        /// <returns>The diagram element parameter.</returns>
        public static TParam SetIsParam<TParam>( this TParam param ) where TParam : IDiagramElementParam
        {
            param.IsParam = true;
            return param;
        }

        /// <summary>
        /// To remove the attribute for the diagram element parameter.
        /// </summary>
        /// <typeparam name="TParam">The diagram element parameter type.</typeparam>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <param name="param">The diagram element parameter.</param>
        public static void RemoveAttribute<TParam, TAttribute>( this TParam param ) where TParam : IDiagramElementParam
        {
            var myFunc = new Func<Attribute, bool>( x => x is TAttribute );
            //param.Attributes.RemoveWhere( Holder323<TParam, TAttribute>.Function032 ?? ( Holder323<TParam, TAttribute>.Function032 = new Func<Attribute, bool>( Holder323<TParam, TAttribute>._lamdaShit.Method341 ) ) );

            param.Attributes.RemoveWhere( myFunc );
        }

        /// <summary>
        /// To modify <see cref="P:StockSharp.Diagram.IDiagramElementParam.NotifyOnChanged" />.
        /// </summary>
        /// <typeparam name="TParam">The diagram element parameter type.</typeparam>
        /// <param name="param">The diagram element parameter.</param>
        /// <param name="value">Value.</param>
        /// <returns>The diagram element parameter.</returns>
        public static TParam SetNotifyOnChange<TParam>( this TParam param, bool value ) where TParam : IDiagramElementParam
        {
            param.NotifyOnChanged = value;
            return param;
        }

        //[Serializable]
        //private sealed class Holder323<X, Y> where X : IDiagramElementParam
        //{
        //    public static readonly Holder323<X, Y> _lamdaShit = new Holder323<X, Y>();
        //    public static Func<Attribute, bool> Function032;

        //    internal bool Method341( Attribute x )
        //    {
        //        return x is Y;
        //    }
        //}
    }
}
