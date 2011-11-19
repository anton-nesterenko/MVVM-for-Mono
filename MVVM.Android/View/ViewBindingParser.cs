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

    public class ViewBindingParser
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
                                    .ToDictionary(a => a.Name.LocalName, a => ParseBindingExpression(a.Value));

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

        private static Binding ParseBindingExpression(string value)
        {
            return BindingFetcher.Instance.GetBinding(value.Substring(8, value.Length - 9));
        }
    }
	
	public class BindingFetcher
	{
		
		private static BindingFetcher _instance;
		public static BindingFetcher Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new BindingFetcher();
				}
				return _instance;
			}
		}



        private IDictionary<Type, IValueConverter> _converters = new Dictionary<Type, IValueConverter>();
		
        private Dictionary<string,Binding> _bindings = new Dictionary<string,Binding>();
		
        private Regex _kvpRegex = new Regex(@"(?<key>\w*)=(?<Value>\w*)");

       

		private BindingFetcher()
		{
			
			andUtil.Log.Info(AndroidConstants.ExecptionLogTag,"Finding all converters in assemblies");
            // find all Value Converters
            _converters = AppDomain.CurrentDomain.GetAssemblies()
                                                            .SelectMany(ass => ass.GetTypes())
                                                            .Where(t => t.GetInterfaces().Contains(typeof(IValueConverter)))
                                                            .ToDictionary(key => key, value => (IValueConverter)null); // caret one when needed
            
			andUtil.Log.Info(AndroidConstants.ExecptionLogTag,string.Format(AndroidConstants.ExecptionLogTag,"Converters found: {0}", _converters.Count));
			
		}
		
        public Binding GetBinding(string attValue)
        {
            IList<String> list = new List<String>();
            StringBuilder builder = new StringBuilder(attValue.Length);
			
            foreach (char c in attValue)
            {
                if ((char.IsWhiteSpace(c) || c == ',') && builder.Length > 0)
                {
                    list.Add(builder.ToString());
                    builder.Clear();
                }
                builder.Append(c);
            }
            if (builder.Length > 0)
            {
                list.Add(builder.ToString());
                builder.Clear();
            }
			
			IValueConverter con = null;
            string path = ".";
			
            if (list.Count > 0)
            {
                foreach (var kvp in list)
                {
                    var match = _kvpRegex.Match(kvp);
                    var k = match.Groups["key"].Value.Trim();
                    var v = match.Groups["value"].Value.Trim();

                    switch (k)
                    {
                        case BindingConstants.PathString:
                            path = v;
                            break;
                        case BindingConstants.ConverterString:
                            con = GetConverter(v);
                            break;
						default:
							path = kvp.Trim();
						break;
                    }
                }
            }
			
            return new Binding(path) {Converter = con};
        }

	    private IValueConverter GetConverter(string converterName)
	    {
	        Type type = null;
            foreach (var kvp in _converters.Where(kvp => kvp.Key.FullName.Contains(converterName)))
	        {
	            if(kvp.Value != null)
	            {
	                return kvp.Value;
	            }

	            type = kvp.Key;
	        }

	        if (type != null)
	        {
	            var valueConverter = (IValueConverter)Activator.CreateInstance(type);
	            _converters[type] = valueConverter;
	            return valueConverter;
	        }
            else
	        {
				andUtil.Log.Warn(AndroidConstants.ExecptionLogTag,string.Format("Converter for Type '{0}' not found", converterName));
	        }

	    	return null;
	    }
	}
}