using System;
using System.ComponentModel;
using Ordo.Android.Mvvm.Iteration1.Binding.Converter;

namespace Ordo.Android.Mvvm.Iteration1.Binding.BindingCollection
{
    public class Binding
    {
        public IValueConverter Converter { get; set; }

        public String ElementName { get; set; }
    
        public void Resume()
        {
            _pause = false;
        }

        public void Pause()
        {
            _pause = true;
        }

        private INotifyPropertyChanged _source;

        /// <summary>
        /// This will generally be the UI of the binding.
        /// </summary>
        public INotifyPropertyChanged Source
        {
            get { return _source; }
            set
            {
                _source = value;

                if(_source != null)
                {
                    _source.PropertyChanged += (o, s) => UpdateFromSource();
                }
            }
        }

        private INotifyPropertyChanged _target;
        private bool _pause;

        /// <summary>
        /// This will generally be the view model class instance containing the property of the binding
        /// </summary>
        public INotifyPropertyChanged Target
        {
            get { return _target; }
            set
            {
                _target = value;

                if (_target != null)
                {
                    _target.PropertyChanged += (o, s) => UpdateFromTarget();
                }
            }
        }

        public Action TargetProperty { get; set; }

        private void UpdateFromSource()
        {
            if (_pause) return;

            //TODO: work out how to get this working for 2 way bindings
            TargetProperty(GetSourceData());
        }

        private void UpdateFromTarget()
        {
            if (_pause) return;

            SourceProperty(GetTargetData());
        }
    }
}
