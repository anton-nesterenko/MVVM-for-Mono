using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mvvm.Android;

namespace Android.Mvvm.TestApp
{
	[Activity (Label = "Android.Mvvm.TestApp", MainLauncher = true)]
    public class Activity1 : MvvmActivity
	{
		

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			var editText = FindViewById<EditText> (Resource.Id.editText);
		}
	}
}


