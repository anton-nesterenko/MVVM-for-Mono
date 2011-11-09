using System;
using System.Collections.Generic;

namespace Ordo.Android.Mvvm.Iteration1.View.Element
{
    public abstract class Element
    {
        public abstract void Accept(IVisitor visitor);

        public IDictionary<String, String> Properties { get; set; }
    }
}
