using System;
using System.Collections.Generic;
using Android.App;
using MonoMobile.Views;

namespace Mvvm.Android.Bindings
{
    /// <summary>
    /// Add a single binding to the list of bindings for a page.
    /// 
    /// This class will enable caching of bindings by page.
    /// </summary>
    public class PageBindingFactory
    {
        private IDictionary<Activity, Binding> _perPageBindings = new Dictionary<Activity, Binding>();
        private IList<BindingExpression> _bindingsForCurrentPage = new List<BindingExpression>(); 

        /// <summary>
        /// Add a single binding for any page to the list of cached bindings.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="elementId"></param>
        /// <param name="property"></param>
        /// <param name="bindingInfo"></param>
        public void Add(IViewModel viewModel, String elementId, String property, Binding bindingInfo)
        {
            
        }

        /// <summary>
        /// The page that we want to create instances for.
        /// 
        /// 1) freeze or GC old binding instances for the current and soon to be previous page.
        /// 2) 
        /// </summary>
        /// <param name="page"></param>
        public void CreateInstancesForPage(Activity page)
        {
            
        }
    }
}