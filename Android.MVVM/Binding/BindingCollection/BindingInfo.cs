using Ordo.Android.Mvvm.Iteration1.Binding.Converter;

namespace Ordo.Android.Mvvm.Iteration1.Binding.BindingCollection
{
    public class BindingInfo
    {
        public string Path { get; set; }

        public IValueConverter Converter { get; set; }
    }
}