using System;
using System.Collections.Generic;
using Android.App;
using MonoMobile.Views;
using and = Android.Views;
	

namespace Mvvm.Android.Bindings
{
    /// <summary>
    /// Add a single binding to the list of bindings for a page.
    /// 
    /// This class will enable caching of bindings by page.
    /// </summary>
    public class PageBindingFactory
    {

        private BindingFactory _bindingFactory = new BindingFactory();
        private IDictionary<and.View, IList<Binding>> _perPageBindings = new Dictionary<and.View, IList<Binding>>();
        private IList<BindingExpression> _bindingsForCurrentPage = new List<BindingExpression>(); 

        /// <summary>
        /// Add a single binding for any page to the list of cached bindings.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="elementId"></param>
        /// <param name="property"></param>
        /// <param name="bindingInfo"></param>
        public void Add(and.View view , Binding binding)
        {
            IList<Binding> bindings;
            if(!_perPageBindings.TryGetValue(view, out bindings))
            {
                bindings = new List<Binding>();
                _perPageBindings[view] = bindings;
            }

            bindings.Add(binding);
        }

        /// <summary>
        /// The page that we want to create instances for.
        /// 
        /// 1) freeze or GC old binding instances for the current and soon to be previous page.
        /// 2) generate and cache all of our new bindings
        /// </summary>
        /// <param name="page"></param>
        public void CreateInstancesForPage(and.View page)
        {
            _bindingsForCurrentPage.Clear();

            foreach (var binding in _perPageBindings[page])
            {
                Add(page, binding);
            }
        }
    }
}