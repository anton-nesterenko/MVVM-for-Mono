using System;
using Mvvm.Android.View.Visitor;
using Ninject;

namespace Mvvm.Android
{
    public class Core
    {
        private readonly IKernel _kernel = new StandardKernel(new AndroidModule());

        public Core()
        {
            PageFactory = _kernel.Get<PageFactory>();

            PageFactory.TypeResolver = type => (IVisitor)_kernel.Get(type);
        }

        public PageFactory PageFactory { get; set; } 
    }
}