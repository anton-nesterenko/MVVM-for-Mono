using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Android;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Java.IO;
using Java.Lang;
using util = Android.Util;
using Android.Views;
using Android.Widget;
using Mvvm.Android.View;
using String = System.String;

namespace Mvvm.Android
{
    public class MvvmActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here

            this.LayoutInflater.Factory = new BindingViewFactory(this.LayoutInflater);
			
		}

        public override void SetContentView(int layoutResID)
        {
			  
			var text =  this.Resources.GetText(layoutResID);
            var resourceName = this.Resources.GetResourceName(layoutResID);
            var resource = this.Resources.GetResourceTypeName(layoutResID);
			
			var files = System.Reflection.Assembly.GetAssembly(this.GetType()).GetManifestResourceNames();
			
			
			var ass = System.Reflection.Assembly.GetAssembly(this.GetType()).Location;
			
			var dir = Path.GetDirectoryName(ass);
			var hello = Directory.GetDirectories(dir);
			var hello2 = Directory.GetFiles(dir);
            var viewName = Path.GetFileName(text);
			viewName = string.Format("Android.Mvvm.TestApp.Views.Main.axml", viewName);
			
			string contant;
			
			using (var reader = new StreamReader(System.Reflection.Assembly.GetAssembly(this.GetType()).GetManifestResourceStream(viewName)))
            {
                contant = reader.ReadToEnd();
            }
			
            //var stream = ass.GetManifestResourceStream(viewName);
            
			
			
			
            //var view = this.LayoutInflater.Inflate(layoutResID, null);
            
            //TypedValue outValue = new TypedValue();
            
            

           // var  myxml = this.


            //ClassLoader loader = Class.ClassLoader;
           
            ////var resourceAsStream = loader.GetResourceAsStream(text);
            //using (var reader = new StreamReader(stream))
            //{
            //    var constant = reader.ReadToEnd();
            //}




            //this.LayoutInflater.Inflate()
            //string UTF8;
			//string ASCII;
			//string Unicode;
			//string zip;

            //var en = Encoding.UTF32;

            //var fd = Resources.OpenRawResourceFd(layoutResID);

            //var v = Assets.OpenNonAssetFd(text);

            //using (var strm = Resources.OpenRawResource(layoutResID))
            //{
            //    using (var reader = new StreamReader(new DeflateStream(strm, CompressionMode.Decompress), en))
            //    {
            //        ASCII = reader.ReadToEnd();
            //    }
            //}

            //using (var strm = Resources.OpenRawResource(layoutResID))
            //{
            //    using (var reader = new StreamReader(strm))
            //    {
            //        Unicode = reader.ReadToEnd();
            //    }
            //}

			 
            //using (var strm = Resources.OpenRawResource(layoutResID))
            //using (var reader = new StreamReader(strm, Encoding.ASCII))
            //{
            //    ASCII = reader.ReadToEnd();
            //}
			
            //using (var strm = Resources.OpenRawResource(layoutResID))
            //using (var reader = new StreamReader(strm, Encoding.Unicode))
            //{
            //    Unicode = reader.ReadToEnd();
            //}

            //using (var strm = Resources.OpenRawResource(layoutResID))
            //{
            //    //using (var reader = new StreamReader(new GZipStream(strm, CompressionMode.Decompress)))
            //    //{
            //    //    zip = reader.ReadToEnd();
            //    //}
            //}

            //Stream parser = this.Resources.OpenRawResource(layoutResID,outValue);

            ////var xmlDocument = XDocument.Load(parser);



            base.SetContentView(layoutResID);
        }
		
	}

    public class BindingViewFactory :Java.Lang.Object, LayoutInflater.IFactory
    {
        private readonly LayoutInflater _layoutInflater;

        public BindingViewFactory(LayoutInflater layoutInflater)
        {
            _layoutInflater = layoutInflater;
        }

        #region Implementation of IJavaObject

        
        public global::Android.Views.View OnCreateView(string name, Context context, IAttributeSet attrs)
        {
			String viewFullName = string.Format("android.widget.{0}",name); // this is bad as it will only do the normal controls....
           	var id = attrs.GetAttributeValue(AndroidConstants.AndroidBindingNamespace, BindingConstants.IdString);
			var view = _layoutInflater.CreateView(viewFullName,null,attrs);
            
			if(view == null || id == null)
			{
                return view;
			}
			
            
			
			return null;
        }

        #endregion
    }
}

