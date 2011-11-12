using MonoMobile.Views;

namespace MVVM.Common.Binding.BindingCollection
{
    public class BindingInfo
    {
		public BindingInfo(string path, IValueConverter converter, string viewProperty)
		{
			Path = path;
			Converter = converter;
			ViewProperty = viewProperty;
		}
		
        public string Path { get; private set; }

        public IValueConverter Converter { get; set; }
		
		public string ViewProperty {get; private set;}
    }
}