using MonoMobile.Views;

namespace MVVM.Common.Binding.BindingCollection
{
    public class BindingInfo
    {
        public string Path { get; set; }

        public IValueConverter Converter { get; set; }
    }
}