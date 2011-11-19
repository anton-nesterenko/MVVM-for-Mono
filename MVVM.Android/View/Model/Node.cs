using System.Collections.Generic;

namespace Mvvm.Android.View
{
    public class Node<T>
    {
        public Node()
        {
            Collection = new List<Node<T>>();
        }

        public ICollection<Node<T>> Collection { get; private set; }

        public T Value { get; set; }
    }
}