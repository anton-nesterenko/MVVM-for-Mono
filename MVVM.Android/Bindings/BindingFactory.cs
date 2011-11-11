using System;
using MVVM.Common.Binding.BindingCollection;
using MonoMobile.Views;

namespace Mvvm.Android.Bindings
{
    /// <summary>
    /// Create/Remove a single binding expression from a Binding class whcih has the information
    /// </summary>
    public class BindingFactory
    {
        public BindingExpression Create(IViewModel viewModel, String elementId, String property, Binding binding)
        {
            var bindingExpression = new BindingExpression(binding, targetProperty, target);

            //TODO: return new BindingExpression();
            throw new System.NotImplementedException();
        }

        public void Remove()
        {
            throw new System.NotImplementedException();
        }
    }
}
