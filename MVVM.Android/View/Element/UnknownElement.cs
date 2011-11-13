using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MVVM.Common.Binding.BindingCollection;
using MonoMobile.Views;
using Mvvm.Android.View.Visitor;

namespace Mvvm.Android.View.Element
{
    public class UnknownElement : Element
    {
        public UnknownElement(string id, IDictionary<string, Binding> properties) : base(id, properties)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            //do nothing
        }
    }
}