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

namespace Mvvm.Android.View.Visitor
{
    public class TokenWalker
    {
        private readonly Node<Element.Element> _elements = new Node<Element.Element>();

        public void Walk(Node<Element.Element> element, ICollection<IVisitor> visitors)
        {
            foreach (var visitor in visitors)
            {
                Walk(element, visitor);
            }
        }

        private void Walk(Node<Element.Element> element, IVisitor visitor)
        {
            if(element.Collection == null || element.Collection.Count() < 1)
                return;

            foreach (Node<Element.Element> childElement in element.Collection)
            {
                childElement.Value.Accept(visitor);
                Walk(childElement, visitor);
            }

        }
    }
}