using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using MVVM.Common.View;
using MVVM.Common.ViewModel;
using MonoMobile.Views;
using Mvvm.Android.View;
using Mvvm.Android.View.Element;
using and = Android.Views;
	

namespace Mvvm.Android.Bindings
{
    /// <summary>
    /// Add a single binding to the list of bindings for a page.
    /// 
    /// This class will enable caching of bindings by page.
    /// </summary>
    public class PageBindingFactory : Java.Lang.Object, LayoutInflater.IFactory
    {
        private readonly LayoutInflater _layoutInflater;
        private readonly Node<Element> _rootnode;
        private readonly IViewModel _viewModel;

        public PageBindingFactory(LayoutInflater layoutInflater, Node<View.Element.Element> rootNode, IViewModel viewModel)
        {
            _layoutInflater = layoutInflater;
            _rootnode = rootNode;
            _viewModel = viewModel;
        }

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
        public void Add(and.View view , Binding binding, IElement element )
        {
            IList<Binding> bindings;
            if(!_perPageBindings.TryGetValue(view, out bindings))
            {
                bindings = new List<Binding>();
                _perPageBindings[view] = bindings;
            }

            bindings.Add(binding);

            binding.Target = _viewModel;
            binding.Source = view;

            var expression = _bindingFactory.Create((ViewModel) binding.Target,
                                                    binding.SourcePath,
                                                    binding.TargetPath,
                                                    binding);
            expression.Element = element;

            _bindingsForCurrentPage.Add(expression);

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
            ClearCurrentBindings();

            foreach (var binding in _perPageBindings[page])
            {
                //Add(page, binding);
            }
        }

        private void ClearCurrentBindings()
        {
            _bindingsForCurrentPage.Clear();
        }

        #region Implementation of IFactory

        public global::Android.Views.View OnCreateView(string name, Context context, IAttributeSet attrs)
        {
            String viewFullName = string.Format("android.widget.{0}", name); // this is bad as it will only do the normal controls....
            var id = attrs.GetAttributeValue(BindingConstants.BindingNamespace, BindingConstants.IdString);

            var view = _layoutInflater.CreateView(viewFullName, null, attrs);

            if (view == null || id == null)
            {
                return view;
            }

            var node = FindNodeWithId(_rootnode, id);

            if (node != null)
            {
                BindingFactory bf = new BindingFactory();
                foreach (var property in node.Value.Properties)
                {
                    Add(view, property.Value, node.Value);
                }

            }
            return view;
        }


        private Node<Element> FindNodeWithId(Node<Element> node, string id)
        {
            if (node.Value.ElementId.Equals(id))
            {
                return node;
            }

            foreach (var childNode in node.Collection)
            {
                var returnNode = FindNodeWithId(childNode, id);
                if (returnNode != null)
                {
                    return returnNode;
                }
            }
            return null;
        }

        #endregion

        public void Bind()
        {
            _viewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_viewModel_PropertyChanged);


            foreach (var bindingExpression in _bindingsForCurrentPage)
            {
                bindingExpression.UpdateSource();
                if(bindingExpression.Binding.ViewSource is EditText && bindingExpression.SourceProperty.Name.Equals("Text"))
                {
                    EditText et = bindingExpression.Binding.ViewSource as EditText;
                    et.AddTextChangedListener(new TextWatcher(bindingExpression));
                }
            }

        }

        void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            foreach (var bindingExpression in _bindingsForCurrentPage.Where(be =>be.TargetProperty.Equals(e.PropertyName)))
            {
                bindingExpression.UpdateSource();
            }
        }
    }
}