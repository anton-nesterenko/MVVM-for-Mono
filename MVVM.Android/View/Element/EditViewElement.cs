using System;
using System.Collections.Generic;
using MVVM.Common.Binding.BindingCollection;
using MonoMobile.Views;
using Mvvm.Android.View.Visitor;

namespace Mvvm.Android.View.Element
{
    public class EditViewElement : Element
    {
        public EditViewElement(string id, IDictionary<string, Binding> properties) : base(id, properties)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
