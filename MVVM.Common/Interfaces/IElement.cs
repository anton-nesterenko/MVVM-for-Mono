namespace MVVM.Common.View
{
    /// <summary>
    /// This is the other half of the IVisitor Pattern. Visitors vist Elements. The elements inform the visitors if they apply to the element.
    /// 
    /// elements are UI elements in the case of this project. We use visitors to identify bindings across elements.
    /// </summary>
    public interface IElement
    {
        object Cell { get; set; }

        string ControlName { get; set; }
    }
}