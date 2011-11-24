using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using MonoMobile.Views;
using Mvvm.Android.View.Element;
using andUtil = Android.Util;

namespace Mvvm.Android.View
{
    public class ParentNode
    {
        public Node<Element.Element> Parent { get; set; }
        public XmlNode Node { get; set; }
    }

    public class ViewTokenizer
    {
        public static Node<Element.Element> Parser(Stream inputStream)
        {
            inputStream.Position = 0;
            var doc = XDocument.Load(inputStream);

            Node<Element.Element> result = null;

            CrawlNodes(doc.Root, ref result);

            return result;
        }

        private static void CrawlNodes(XElement element, ref Node<Element.Element> iterator)
        {
            iterator = CreateElement(element, iterator);


            foreach (var node in element.Elements())
            {
                CrawlNodes(node, ref iterator);
            }
        }



        private static Node<Element.Element> CreateElement(XElement element, Node<Element.Element> parentResultNode)
        {
            Node<Element.Element> elementToAdd;



            var id = element.Attribute(XName.Get(Mvvm.Android.BindingConstants.IdString, AndroidConstants.AndroidBindingNamespace)).Value;
            var properties = element.Attributes()
                                    .Where(a => !a.Name.LocalName.Equals(Mvvm.Android.BindingConstants.IdString) && HasBindingExpression(a.Value))
                                    .ToDictionary(a => a.Name.LocalName, a => ParseBindingExpression(a.Name.LocalName, a.Value));

            switch (element.Name.LocalName)
            {
			case AndroidConstants.Views.EditTextString:
                    elementToAdd = new Node<Element.Element>() { Value = new EditViewElement(id, properties) };
                    break;

                default:
                    elementToAdd = new Node<Element.Element>() { Value = new UnknownElement(id, properties) };
                    break;
            }   
 			if(parentResultNode != null)
			{
				parentResultNode.Collection.Add(elementToAdd);
			}
			
            

            return elementToAdd;
        }

        private static bool HasBindingExpression(string value)
        {
            return value.StartsWith("{Binding") && value.EndsWith("}"); // we only want to pass the attributes that have our binding syntax in it.
        }

        private static Binding ParseBindingExpression(string targetPath, string bindingValue)
        {
            return BindingFetcher.Instance.GetBinding(targetPath, bindingValue.Substring(8, bindingValue.Length - 9));
        }
    }
}