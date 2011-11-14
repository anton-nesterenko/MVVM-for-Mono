using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using MVVM.Common.Binding.BindingCollection;
using MonoMobile.Views;
using Mvvm.Android.View.Element;

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

            var id = element.Attribute("id").Value;
            var properties = element.Attributes()
                                    .Where(a => a.Name.LocalName != "id")
                                    .ToDictionary(a => a.Name.LocalName, a => ParseBindingExpression(a.Value));

            switch (element.Name.NamespaceName)
            {
                case "EditView":
                    elementToAdd = new Node<Element.Element>() { Value = new EditViewElement(id, properties) };
                    break;

                default:
                    elementToAdd = new Node<Element.Element>() { Value = new UnknownElement(id, properties) };
                    break;
            }   
 
            parentResultNode.Collection.Add(elementToAdd);

            return parentResultNode.Collection.Last();
        }

        private static Binding ParseBindingExpression(string value)
        {
            throw new NotImplementedException();
        }

        private static string CreatePropertyBindingInfos(XmlAttributeCollection attributes, out  IDictionary<string, BindingInfo> bindings)
        {
            bindings = new Dictionary<string, BindingInfo>();
            string Id = null;
            if (attributes != null && attributes.Count > 0)
            {
                foreach (var att in attributes.Cast<XmlAttribute>())
                {
					if(Id == null && att.LocalName.Equals("id"))
					{
					 	Id = att.Value;
					}
					else
					{
					    BindingInfo bi = BindingFetcher.Instance.GetBinding(att);
						if(bi!= null)
						{
							bindings[bi.ViewProperty] = bi;
						}
					}
                }
            }
			return Id;
        }
    }
	
	public class BindingFetcher
	{
		public enum DefaultValueType 
		{
			String, 
			Boolean,
		}
			
		private class Binding
		{
		    public string Prefix { get; private set; }
		    public string Name { get; private set; }
		    public DefaultValueType Type { get; private set; }

		    public Binding(string prefix, string name, DefaultValueType type)
		    {
		        Prefix = prefix;
		        Name = name;
		        Type = type;
		    }
		}
		
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
		
        private Regex _kvpRegex = new Regex(@"(?<key>\w*)\s*=\s*(?<Value>\w*)");

       

		private BindingFetcher()
		{
			CreateBinding("Android","text", DefaultValueType.String);

			
			Android.Util.Log.Info(AndroidConstants.ExecptionLogTag,"Finding all converters in assemblies");
            // find all Value Converters
            _converters = AppDomain.CurrentDomain.GetAssemblies()
                                                            .SelectMany(ass => ass.GetTypes())
                                                            .Where(t => t.GetInterfaces().Contains(typeof(IValueConverter)))
                                                            .ToDictionary(key => key, value => (IValueConverter)null); // caret one when needed
            
			Android.Util.Log.Info(string.Format(AndroidConstants.ExecptionLogTag,"Converters found: {0}", _converters.Count));
			
		}
		

        public void  CreateBinding(string prefix, string name, DefaultValueType type)
        {
            _bindings[CreateKey(prefix,name)] = new Binding(prefix,name,type);
        }

	    private string CreateKey(string prefix, string name)
	    {
	        return string.Format("{0}^{1}", prefix, name).ToLower();
	    }


	    public BindingInfo GetBinding(XmlAttribute attribute)
		{
            if (!string.IsNullOrEmpty(attribute.Value) && attribute.Value.StartsWith("{Binding ") && attribute.Value.EndsWith("}"))
            {
				var @value =  attribute.Value.Substring(9, attribute.Value.Length - 10);
				
				IList<String> list = new List<String>();
				StringBuilder builder = new StringBuilder(@value.Length);
				
				foreach (char c in  @value)
				{
					if((char.IsWhiteSpace(c) || c == ',') && builder.Length > 0)
					{
						list.Add(builder.ToString());
						builder.Clear();
					}
					builder.Append(c);
				}
				if(builder.Length > 0)
				{
					list.Add(builder.ToString());
					builder.Clear();
				}
				if(list.Count > 0)
				{
					IValueConverter con = null;
					string path = "."; 
					
	                foreach (var kvp in list)
	                {
	                    var match = _kvpRegex.Match(kvp);
						var k =  match.Groups["key"].Value.Trim();
                        var v = match.Groups["value"].Value.Trim();
						
                        switch (k)
                        {
                            case "Path":
                                path = v;
                                break;
                            case "Converter":
                                con = GetConverter(v);
                                break;
                        }
					}
					return new BindingInfo(path,con,attribute.LocalName);	
                }
			}
			return null;
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
				Android.Util.Log.Warn(string.Format(AndroidConstants.ExecptionLogTag,"Converter for Type '{0}' not found", converterName));
	        }

	    	return null;
	    }
	}
}