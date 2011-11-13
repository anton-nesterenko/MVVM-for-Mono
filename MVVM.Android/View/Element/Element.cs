using System;
using System.Collections.Generic;
using MVVM.Common.View;
using MonoMobile.Views;
using Mvvm.Android.View.Visitor;

namespace Mvvm.Android.View.Element
{
    /// <summary>
    /// This is the other half of the IVisitor Pattern. Visitors vist Elements. The elements inform the visitors if they apply to the element.
    /// 
    /// elements are UI elements in the case of this project. We use visitors to identify bindings across elements.
    /// </summary>
    /// <seealso cref="IVisitor"/>
    public abstract class Element : IElement
    {
        protected Element(string id, IDictionary<String, Binding> properties)
        {
            ElementId = id;
            Properties = properties;
        }

        public abstract void Accept(IVisitor visitor);

        public IDictionary<String, Binding> Properties { get; private set; }

        public object Cell { get; set; }

        public string ControlName
        {
            get; set;
        }

        public string ElementId { get; private set; }
    }
}
