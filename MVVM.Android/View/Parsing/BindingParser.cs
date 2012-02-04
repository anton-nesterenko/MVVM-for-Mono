using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MonoMobile.Views;

namespace Mvvm.Android.View.Parsing
{
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

        private Dictionary<string, Binding> _bindings = new Dictionary<string, Binding>();

        private Regex _kvpRegex = new Regex(@"\s*(?<Key>\w*)=(?<Value>\w*)\s*");



        private BindingFetcher()
        {

            global::Android.Util.Log.Info(AndroidConstants.ExecptionLogTag, "Finding all converters in assemblies");
            // find all Value Converters
            _converters = AppDomain.CurrentDomain.GetAssemblies()
                                                            .SelectMany(ass => ass.GetTypes())
                                                            .Where(t => t.GetInterfaces().Contains(typeof(IValueConverter)))
                                                            .ToDictionary(key => key, value => (IValueConverter)null); // caret one when needed

            global::Android.Util.Log.Info(AndroidConstants.ExecptionLogTag, string.Format(AndroidConstants.ExecptionLogTag, "Converters found: {0}", _converters.Count));

        }

        public Binding  GetBinding(string sorucePath, string attValue)
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
            string targetpath = ".";

            if (list.Count > 0)
            {
                foreach (var kvp in list)
                {
                    var match = _kvpRegex.Match(kvp);
                    var k = match.Groups["Key"].Value.Trim();
                    var v = match.Groups["Value"].Value.Trim();

                    switch (k)
                    {
                        case BindingConstants.PathString:
                            targetpath = v;
                            break;
                        case BindingConstants.ConverterString:
                            con = GetConverter(v);
                            break;
						default:
							if(list.Count == 1)
							{
                                targetpath = kvp.Trim();
							}
							break;
                    }
                }
            }
            return new Binding(sorucePath, targetpath) { Converter = con };
        }

        private IValueConverter GetConverter(string converterName)
        {
            Type type = null;
            foreach (var kvp in _converters.Where(kvp => kvp.Key.FullName.Contains(converterName)))
            {
                if (kvp.Value != null)
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
                global::Android.Util.Log.Warn(AndroidConstants.ExecptionLogTag, string.Format("Converter for Type '{0}' not found", converterName));
            }

            return null;
        }
    }
}