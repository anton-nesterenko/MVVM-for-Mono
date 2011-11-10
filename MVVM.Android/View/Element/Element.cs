using System;
using System.Collections.Generic;
using MVVM.Common.View;
using Mvvm.Android.View.Visitor;

namespace Mvvm.Android.View.Element
{
    public abstract class Element : IElement
    {
        public abstract void Accept(IVisitor visitor);

        public IDictionary<String, String> Properties { get; set; }

        public object Cell { get; set; }
    }
}
