using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Android.Mvvm.Tests
{
	public class rhys : View
	{
		public rhys (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		public rhys (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize ();
		}

		private void Initialize ()
		{
		}
	}
}

