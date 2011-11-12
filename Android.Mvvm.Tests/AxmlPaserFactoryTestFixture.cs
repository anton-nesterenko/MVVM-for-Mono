using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mvvm.Android.View;
using Mvvm.Android.View.Element;
using NUnit.Framework;

namespace Android.Mvvm.Tests
{
	[TestFixture]
    public class AxmlPaserFactoryTestFixture
    {
		[Test]
        public void  EditTextElementTest()
        {
            var node = AxmalParser.Parser(new MemoryStream(ASCIIEncoding.Default.GetBytes("<EditText xmlns:android=\"http://schemas.android.com/apk/res/android\" android:id = \"@+id/BindingText\" android:layout_width = \"fill_parent\" android:layout_height = \"wrap_content\" android:text = \"{Binding Path=hello Converter=TestValueConverter}\"/>")));

            Assert.NotNull(node);
            Assert.True(node is NodeCollection<Element>);

            var nodeCollection = node as NodeCollection<Element>;

            Assert.True(nodeCollection.Collection.Count == 1);
            Assert.True(nodeCollection.Collection.First() is ElementNode<Element>);
			
			ElementNode<Element> elementNode = nodeCollection.Collection.First() as ElementNode<Element>;
			
			Assert.True(elementNode.Value is EditViewElement);
			
			EditViewElement viewElement = elementNode.Value as EditViewElement;
			
			Assert.True(!string.IsNullOrEmpty(viewElement.ElementId));
			
			Assert.True(viewElement.Properties.Count > 0);
			
			Assert.True(viewElement.Properties.ContainsKey("text"));
			
			Assert.NotNull(viewElement.Properties["text"]);
			
			
			
        }
    }
}