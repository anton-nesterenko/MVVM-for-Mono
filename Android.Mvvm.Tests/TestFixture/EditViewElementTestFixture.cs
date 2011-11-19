using System.IO;
using System.Text;
using Android.Widget;
using Mvvm.Android;
using Mvvm.Android.View;
using Mvvm.Android.View.Element;
using NUnit.Framework;
using NUnitLite;

namespace Android.Mvvm.Tests.TestFixture
{
	[TestFixture]
    public class When_parsing_a_edit_text_axml_with_binding_syntax_in_text_attribute_with_no_set_Path : SpecificationContext
	{
		private static Node<Element> _node;
		private static string axml;


	    private Establish context = () => axml = "<EditText xmlns:android=\"http://schemas.android.com/apk/res/android\" android:id = \"@+id/BindingText\" android:layout_width = \"fill_parent\" android:layout_height = \"wrap_content\" android:text = \"{Binding}\"/>";


        private Beacuse of = () => _node = ViewTokenizer.Parser(new MemoryStream(ASCIIEncoding.Default.GetBytes(axml)));

	    [Test]
        public void It_should_not_Be_Null()
        { 
			Assert.NotNull(_node);
		}
		
		[Test]
		public void It_should_have_no_children_nodes()
		{
			Assert.True(_node.Collection.Count == 0);
		}
		
		[Test]
        public void It_should_have_Element_type_of_EditViewElement()
		{
            Assert.True(_node.Value is EditViewElement);
		}
		
		[Test]
		public void It_should_have_Path_as_dot()
		{
			EditViewElement element = _node.Value as EditViewElement;
			
			Assert.NotNull(element.Properties);
			
			Assert.True(element.Properties.Count > 0);

            Assert.True(element.Properties.ContainsKey(BindingConstants.Attributes.Text));

            Assert.True(element.Properties[BindingConstants.Attributes.Text].TargetPath.Equals("."));
		}
		
		[Test]
		public void It_should_have_no_converter()
		{
			EditViewElement element = _node.Value as EditViewElement;
			
			Assert.NotNull(element.Properties);
			
			Assert.True(element.Properties.Count > 0);

            Assert.True(element.Properties.ContainsKey(BindingConstants.Attributes.Text));

            Assert.Null(element.Properties[BindingConstants.Attributes.Text].Converter);
		}
    }

    [TestFixture]
    public class When_parsing_a_edit_text_axml_with_binding_syntax_in_text_attribute_with_set_Path_without_specifing_Path  : SpecificationContext
    {
        private static Node<Element> _node;
		private static string axml;
		

        private Establish context = () => axml = "<EditText xmlns:android=\"http://schemas.android.com/apk/res/android\" android:id = \"@+id/BindingText\" android:layout_width = \"fill_parent\" android:layout_height = \"wrap_content\" android:text = \"{Binding TestPath}\"/>";

        private Beacuse of = () => {
			_node = ViewTokenizer.Parser(new MemoryStream(ASCIIEncoding.Default.GetBytes(axml)));
		};

        [Test]
        public void It_should_not_Be_Null()
        {
            Assert.NotNull(_node);
        }

        [Test]
        public void It_should_have_no_children_nodes()
        {
            Assert.True(_node.Collection.Count == 0);
        }

        [Test]
        public void It_should_have_Element_type_of_EditViewElement()
        {
            Assert.True(_node.Value is EditViewElement);
        }

        [Test]
        public void It_should_have_Path_as_TestPath()
        {
            EditViewElement element = _node.Value as EditViewElement;

            Assert.NotNull(element.Properties);

            Assert.True(element.Properties.Count > 0);

            Assert.True(element.Properties.ContainsKey(BindingConstants.Attributes.Text));

            Assert.True(element.Properties[BindingConstants.Attributes.Text].TargetPath.Equals("TestPath"));
            
        }

        [Test]
        public void It_should_have_no_converter()
        {
            EditViewElement element = _node.Value as EditViewElement;

            Assert.NotNull(element.Properties);

            Assert.True(element.Properties.Count > 0);

            Assert.True(element.Properties.ContainsKey(BindingConstants.Attributes.Text));

            Assert.Null(element.Properties[BindingConstants.Attributes.Text].Converter);
        }
    }

    [TestFixture]
    public class When_parsing_a_edit_text_axml_with_binding_syntax_in_text_attribute_with_set_Path_with_specifing_Path : SpecificationContext
    {
        private static Node<Element> _node;
        private static string axml;


        private Establish context = () => axml = "<EditText xmlns:android=\"http://schemas.android.com/apk/res/android\" android:id = \"@+id/BindingText\" android:layout_width = \"fill_parent\" android:layout_height = \"wrap_content\" android:text = \"{Binding Path=TestPath}\"/>";

        private Beacuse of = () => 
		{
			_node = ViewTokenizer.Parser(new MemoryStream(ASCIIEncoding.Default.GetBytes(axml)));
		};

        [Test]
        public void It_should_not_Be_Null()
        {
            Assert.NotNull(_node);
        }

        [Test]
        public void It_should_have_no_children_nodes()
        {
            Assert.True(_node.Collection.Count == 0);
        }

        [Test]
        public void It_should_have_Element_type_of_EditViewElement()
        {
            Assert.True(_node.Value is EditViewElement);
        }

        [Test]
        public void It_should_have_Path_as_TestPath()
        {
            EditViewElement element = _node.Value as EditViewElement;

            Assert.NotNull(element.Properties);

            Assert.True(element.Properties.Count > 0);

            Assert.True(element.Properties.ContainsKey(BindingConstants.Attributes.Text));

            Assert.True(element.Properties[BindingConstants.Attributes.Text].TargetPath.Equals("TestPath"));

        }

        [Test]
        public void It_should_have_no_converter()
        {
            EditViewElement element = _node.Value as EditViewElement;

            Assert.NotNull(element.Properties);

            Assert.True(element.Properties.Count > 0);

            Assert.True(element.Properties.ContainsKey(BindingConstants.Attributes.Text));

            Assert.Null(element.Properties[BindingConstants.Attributes.Text].Converter);
        }
    }

    [TestFixture]
    public class When_parsing_a_edit_text_axml_with_binding_syntax_in_text_attribute_with_set_Path_with_specifing_Path_with_space : SpecificationContext
    {
        private static Node<Element> _node;
        private static string axml;


        private Establish context = () => axml = "<EditText xmlns:android=\"http://schemas.android.com/apk/res/android\" android:id = \"@+id/BindingText\" android:layout_width = \"fill_parent\" android:layout_height = \"wrap_content\" android:text = \"{Binding Path = TestPath}\"/>";

        private Beacuse of = () => {
			_node = ViewTokenizer.Parser(new MemoryStream(ASCIIEncoding.Default.GetBytes(axml)));
		};

        [Test]
        public void It_should_not_Be_Null()
        {
            Assert.NotNull(_node);
        }

        [Test]
        public void It_should_have_no_children_nodes()
        {
            Assert.True(_node.Collection.Count == 0);
        }

        [Test]
        public void It_should_have_Element_type_of_EditViewElement()
        {
            Assert.True(_node.Value is EditViewElement);
        }

        [Test]
        public void It_should_not_have_Path_as_TestPath()
        {
            EditViewElement element = _node.Value as EditViewElement;

            Assert.NotNull(element.Properties);

            Assert.True(element.Properties.Count > 0);

            Assert.True(element.Properties.ContainsKey(BindingConstants.Attributes.Text));

            Assert.False(element.Properties[BindingConstants.Attributes.Text].TargetPath.Equals("TestPath"));

        }

        [Test]
        public void It_should_have_no_converter()
        {
            EditViewElement element = _node.Value as EditViewElement;

            Assert.NotNull(element.Properties);

            Assert.True(element.Properties.Count > 0);

            Assert.True(element.Properties.ContainsKey(BindingConstants.Attributes.Text));

            Assert.Null(element.Properties[BindingConstants.Attributes.Text].Converter);
        }
    }
}