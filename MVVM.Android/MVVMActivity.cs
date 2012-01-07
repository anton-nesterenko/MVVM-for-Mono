using System;
using System.Collections.Generic;
using Mvvm.Android.View;
using Mvvm.Android.View.Element;
using io = System.IO;
using System.Linq;
using System.Reflection;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using util = Android.Util;
using Android.Views;
using String = System.String;

namespace Mvvm.Android
{
    public class MvvmActivity : Activity
    {
        public override void SetContentView(int layoutResID)
        {	
			string contant;
			var stream = EmbeddedResource.Instance.GetStream(this, layoutResID);

            var _node = ViewTokenizer.Parser(stream);
			
            this.LayoutInflater.Factory = new BindingViewFactory(this.LayoutInflater, _node);
            
			base.SetContentView(layoutResID);
        }
		
	}

    public class BindingViewFactory :Java.Lang.Object, LayoutInflater.IFactory
    {
        private readonly LayoutInflater _layoutInflater;
        private readonly Node<Element> _node;

        public BindingViewFactory(LayoutInflater layoutInflater, Node<View.Element.Element> node)
        {
            _layoutInflater = layoutInflater;
            _node = node;
        }

        public global::Android.Views.View OnCreateView(string name, Context context, IAttributeSet attrs)
        {
			String viewFullName = string.Format("android.widget.{0}",name); // this is bad as it will only do the normal controls....
           	var id = attrs.GetAttributeValue(AndroidConstants.AndroidNamespace, BindingConstants.IdString);
            
			
			var view = _layoutInflater.CreateView(viewFullName, null, attrs);
            
			if(view == null || id == null)
			{
                return view;
			}
			return null;
        }
    }

    public class EmbeddedResource
    {
        private static EmbeddedResource _embeddedResource;

        public static EmbeddedResource Instance
        {
            get
            {
                if (_embeddedResource == null)
                {
                    _embeddedResource = new EmbeddedResource();
                }

                return _embeddedResource;
            }
        }

        private IDictionary<Assembly,IList<string>> assemblyfileNames; 
        

        private EmbeddedResource()
        {
            assemblyfileNames = new Dictionary<Assembly,IList<string>>();
        }

        public io.Stream GetStream(Context context, int layoutResID)
        {
            string axmlFileName = context.Resources.GetText(layoutResID).ToLowerInvariant();
			var viewName = io.Path.GetFileName(axmlFileName);
			
			var callingAss = System.Reflection.Assembly.GetAssembly(context.GetType());
			
			IList<string> fileList = null;
			
			if(!assemblyfileNames.TryGetValue(callingAss, out fileList))
			{
				fileList = assemblyfileNames[callingAss] = callingAss.GetManifestResourceNames();
			}
			
            var matchingEmbeddedName = fileList.Where(enf => enf.ToLowerInvariant().Contains(viewName)).FirstOrDefault();

            return matchingEmbeddedName != null ? callingAss.GetManifestResourceStream(matchingEmbeddedName) : null;
        }
    }
}

