using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MVVM.Common.ViewModel;
using MonoMobile.Views;

namespace Mvvm.Android.Bindings
{
    /// <summary>
    /// Create/Remove a single binding expression from a Binding class which has the information
    /// </summary>
    public class BindingFactory
    {
		
		// viewmodelType, targetProperty, MemberInfo 
        IDictionary<Type, IDictionary<string, MemberInfo>> _typeBindingDic = new Dictionary<Type, IDictionary<string, MemberInfo>>();
		
		
		
        public BindingExpression Create(IViewModel viewModel, String elementId, String targetProperty, Binding binding)
        {
			var type = viewModel.GetType();
			
			// find property dic
            IDictionary<string, MemberInfo> propertyDic;
			if(!_typeBindingDic.TryGetValue(type, out propertyDic))
			{
                propertyDic = new Dictionary<string, MemberInfo>();
				_typeBindingDic[type] = propertyDic;
			}

            MemberInfo propertyInfo = null;
			
			if(!propertyDic.TryGetValue(targetProperty, out propertyInfo))
			{
				// find the member...
				propertyInfo = type.GetMember(targetProperty).FirstOrDefault(); 
				
                if(propertyInfo == null)
                {
                    return null;
                }
				propertyDic[targetProperty] = propertyInfo;
			}
            var bindingExpression = new BindingExpression(binding, propertyInfo, viewModel);
			
			return bindingExpression;
        }
    }
}
