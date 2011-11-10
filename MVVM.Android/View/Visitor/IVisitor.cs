using MVVM.Common.View;

namespace Mvvm.Android.View.Visitor
{
    /// <summary>
    /// A Visitor is a contract which allows for the use of the vistor gang of four design pattern.
    /// 
    /// This pattern can be used to inspect items in the UI DOM. 
    /// The advantage that a visitor provides is that all visitors can visit every UI element and UI elements dictate what visitors they support.
    /// 
    /// Example:
    /// 
    /// We have a text binding visitor class. Text bindings can be relevant to textbox and labels but not for listboxes.
    /// The problem that we have solved here is that the visitor doesn't know what it applys to, it just knows what it is, and how to do it.
    /// The UI elements know what behaviors apply to them, meaning we don't break anything by adding new binding types.
    /// </summary>
    /// <seealso cref="IElement"/>
    public interface IVisitor
    {
        void Visit(IElement element);
    }
}
