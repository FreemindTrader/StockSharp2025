
using Ecng.Collections;
using Ecng.Serialization;
using System;
using System.Security;

namespace StockSharp.Diagram
{
    /// <summary>The storage of composite elements.</summary>
    public interface ICompositionRegistry
    {
        /// <summary>List of elements.</summary>
        INotifyList<DiagramElement> DiagramElements { get; }

        /// <summary>To serialize the composite element.</summary>
        /// <param name="element">
        ///   <see cref="T:StockSharp.Diagram.CompositionDiagramElement" />
        /// </param>
        /// <param name="schemeType">Scheme type.</param>
        /// <param name="password">Password.</param>
        /// <returns>Settings storage.</returns>
        SettingsStorage Serialize( CompositionDiagramElement element, SchemeTypes? schemeType = null, SecureString password = null );

        /// <summary>To deserialize the composite element.</summary>
        /// <param name="element">
        ///   <see cref="T:StockSharp.Diagram.CompositionDiagramElement" />
        /// </param>
        /// <param name="storage">Settings storage.</param>
        /// <param name="getPassword">Get password handler.</param>
        void Deserialize( CompositionDiagramElement element, SettingsStorage storage, Func<SecureString> getPassword = null );

        /// <summary>To deserialize the composite element.</summary>
        /// <param name="storage">Settings storage.</param>
        /// <param name="getPassword">Get password handler.</param>
        /// <returns>
        ///   <see cref="T:StockSharp.Diagram.CompositionDiagramElement" />
        /// </returns>
        CompositionDiagramElement Deserialize( SettingsStorage storage, Func<SecureString> getPassword = null );

        /// <summary>To serialize the composite element.</summary>
        /// <param name="element">
        ///   <see cref="T:StockSharp.Diagram.CompositionDiagramElement" />
        /// </param>
        /// <param name="schemeType">
        ///   <see cref="T:StockSharp.Diagram.SchemeTypes" />
        /// </param>
        /// <param name="password">Password.</param>
        /// <returns>Byte array.</returns>
        byte[ ] SerializeToBytes( CompositionDiagramElement element, SchemeTypes? schemeType = null, SecureString password = null );

        /// <summary>To deserialize the composite element.</summary>
        /// <param name="data">Byte array.</param>
        /// <param name="getPassword">Get password handler.</param>
        /// <returns>
        ///   <see cref="T:StockSharp.Diagram.CompositionDiagramElement" />
        /// </returns>
        CompositionDiagramElement Deserialize( byte[ ] data, Func<SecureString> getPassword );

        /// <summary>
        /// Create error stub <see cref="T:StockSharp.Diagram.CompositionDiagramElement" /> instance.
        /// </summary>
        /// <param name="fileName">File name from where loading was failed.</param>
        /// <param name="error">Error.</param>
        /// <returns>
        ///   <see cref="T:StockSharp.Diagram.CompositionDiagramElement" />
        /// </returns>
        CompositionDiagramElement CreateErrorStubComposition( string fileName, Exception error );

        /// <summary>
        /// Create <see cref="T:StockSharp.Diagram.CompositionDiagramElement" /> instance.
        /// </summary>
        /// <returns>
        ///   <see cref="T:StockSharp.Diagram.CompositionDiagramElement" />
        /// </returns>
        CompositionDiagramElement CreateComposition();
    }
}
