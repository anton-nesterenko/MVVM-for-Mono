using Android.App;
using Mvvm.Android.Bindings;
using Mvvm.Android.View;
using and = Android.Views;

namespace Mvvm.Android
{
    /// <summary>
    /// A class for triggering our UI parsing when changing pages
    /// </summary>
    public class PageFactory
    {
        private readonly ViewBindingParser _viewBindingParser;
        private and.View _currentPage;

        public PageFactory(ViewBindingParser viewBindingParser)
        {
            _viewBindingParser = viewBindingParser;
        }

        /// <summary>
        /// If the bindings for the page being loaded are not already cached, then
        /// 
        /// 1) store the page we are on.
        /// 2) convert the xml UI into an element based object model representing the hierarchy and binding information of the UI.
        /// 3) parse the page being loaded by calling the parser and telling it to start. 
        /// 
        /// The parser will create the binding by calling the PageBindingFactory
        /// </summary>
        private void Load(and.View page)
        {
            _currentPage = page;
            

            // should be a singliton ?
            //new PageBindingFactory().CreateInstancesForPage(page); 

            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Put the bindings into an inactive state so that they are inactive while sitting in cache.
        /// 
        /// 1) If there is no page currently then we can't unload the page. (No exception thrown)
        /// </summary>
        private void Unload()
        {
            if (_currentPage == null) return;

            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Move from one page to the next by unloading the bindings on the current page, then loading the new bindings.
        /// </summary>
        public void TransitionToPage(Activity page)
        {
            Unload();

            //Load(page);
        }
    }
}
