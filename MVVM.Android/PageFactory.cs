using Mvvm.Android.View;

namespace Mvvm.Android
{
    /// <summary>
    /// A class for triggering our UI parsing when changing pages
    /// </summary>
    public class PageFactory
    {
        private readonly ViewBindingParser _viewBindingParser;

        public PageFactory(ViewBindingParser viewBindingParser)
        {
            _viewBindingParser = viewBindingParser;
        }

        /// <summary>
        /// If the bindings for the page being loaded are not already cached, then
        /// 
        /// 1) convert the xml UI into an element based object model representing the heirachy and binding information of the UI.
        /// 2) parse the page being loaded by calling the parser and telling it to start. 
        /// 
        /// The parser will create the binding by calling the PageBindingFactory
        /// </summary>
        private void Load()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Put the bindings into an inactive state so that they are inactive while sitting in cache.
        /// </summary>
        private void Unload()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Move from one page to the next by unloading the bindings on the current page, then loading the new bindings.
        /// </summary>
        public void TransitionToPage()
        {
            Unload();

            Load();
        }
    }
}
