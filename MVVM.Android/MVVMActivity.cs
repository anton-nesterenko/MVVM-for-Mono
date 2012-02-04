using System;
using System.Collections.Generic;
using Android.Widget;
using MVVM.Common.ViewModel;
using MonoMobile.Views;
using Mvvm.Android.Bindings;
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
using views = Android.Views;
using Android.Views;
using String = System.String;

namespace Mvvm.Android
{
    public class MvvmActivity : Activity
    {
        public void SetContentView(int layoutResId, IViewModel viewModel)
        {
            var stream = EmbeddedResource.Instance.GetStream(this, layoutResId);

            var node = ViewTokenizer.Parser(stream);

            var pageBinding = new PageBindingFactory(this.LayoutInflater, node, viewModel);

            this.LayoutInflater.Factory = pageBinding;

            base.SetContentView(layoutResId);

            pageBinding.Bind();
		}
	}


    public class EmbeddedResource
    {
        private static EmbeddedResource _embeddedResource;

        public static EmbeddedResource Instance
        {
            get { return _embeddedResource ?? (_embeddedResource = new EmbeddedResource()); }
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

