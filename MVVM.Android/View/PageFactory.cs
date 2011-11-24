using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.App;
using MonoMobile.Views;
using Mvvm.Android.Bindings;
using Mvvm.Android.View;
using Mvvm.Android.View.Visitor;
using and = Android.Views;

namespace Mvvm.Android
{
    /// <summary>
    /// A class for triggering our UI parsing when changing pages
    /// </summary>
    public class PageFactory
    {
        private readonly ViewTokenizer _viewBindingParser;
        private readonly TokenWalker _tokenWalker;
        private and.View _currentPage;
        private readonly ICollection<IVisitor> _visitors;

        public PageFactory(ViewTokenizer viewBindingParser, TokenWalker tokenWalker)
        {
            _viewBindingParser = viewBindingParser;
            _tokenWalker = tokenWalker;
            _visitors = typeof(IVisitor).GetSubclassesOf(true).Select(GetInstance).ToList();
        }

        public Func<Type,IVisitor> TypeResolver { get; set; }

        private IVisitor GetInstance(Type type)
        {
            return TypeResolver.Invoke(type);
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
        private void Load(MemoryStream page)
        {
            //1) get the node tree from the UI markup.
            var nodes = ViewTokenizer.Parser(page);

            //2) get the visitors to visit the node tree.
            _tokenWalker.Walk(nodes, _visitors);
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
        public void TransitionToPage(MemoryStream page)
        {
            Unload();

            Load(page);
        }
    }
}
