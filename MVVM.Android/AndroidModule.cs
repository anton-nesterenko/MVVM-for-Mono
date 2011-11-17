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
using Mvvm.Android.View;
using Mvvm.Android.View.Visitor;
using Ninject.Modules;

namespace Mvvm.Android
{
    public class AndroidModule : NinjectModule
    {
        public override void Load()
        {
            Bind<PageFactory>().ToSelf();
            Bind<ViewTokenizer>().ToSelf();
            Bind<TokenWalker>().ToSelf();
        }
    }
}