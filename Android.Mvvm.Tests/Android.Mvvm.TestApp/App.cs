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

namespace Android.Mvvm.TestApp
{
	[Application(Label = "Android.Mvvm.TestApp", Debuggable = true, Icon = "@drawable/icon", Enabled = true)]
	public class TestApplication : Application
	{
        private readonly IntPtr _input;

        public TestApplication(IntPtr input)
		{
		    _input = input;
		}

        public override void OnCreate()
        {
            base.OnCreate();
			//rhys
            //Binder.Init(this); 
        }
		
		public override Context BaseContext {
			get {
				return base.BaseContext;
			}
		}
	}
}

