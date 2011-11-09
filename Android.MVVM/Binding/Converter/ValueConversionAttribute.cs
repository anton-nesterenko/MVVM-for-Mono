using System;

namespace Ordo.Android.Mvvm.Iteration1.Binding.Converter
{
    public class ValueConversionAttribute : Attribute
    {
        public ValueConversionAttribute(Type sourceType, Type destinationType)
        {
            SourceType = sourceType;
            TargetType = destinationType;
        }

        public Type ParameterType { get; set; }
        public Type SourceType { get; set; }
        public Type TargetType { get; set; }

        public override object TypeId
        {
            get { return GetType(); }
        }
    }
}