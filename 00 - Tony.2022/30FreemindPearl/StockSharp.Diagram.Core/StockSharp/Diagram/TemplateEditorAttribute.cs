
using System;

namespace StockSharp.Diagram
{
    /// <summary>
    /// </summary>
    public class TemplateEditorAttribute : Attribute
    {

        private string _templateKey;

        /// <summary>Template key.</summary>
        public string TemplateKey
        {
            get
            {
                return this._templateKey;
            }
            set
            {
                this._templateKey = value;
            }
        }
    }
}
