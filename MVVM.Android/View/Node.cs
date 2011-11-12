using System.Collections.Generic;

namespace Mvvm.Android.View
{
    public class Node<T> 
    {
       
    }

    public class NodeCollection<T> : Node<T>
    {
        public NodeCollection()
        {
            Collection = new List<Node<T>>();
        }

        public ICollection<Node<T>> Collection { get; private set; } 
    }
    public class ElementNode<T> : Node<T>
    {
        public  T Value { get; set; }
    }

}