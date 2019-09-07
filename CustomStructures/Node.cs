using System.Collections.Generic;

namespace BPCode.CustomImpl
{
    internal class Node<Y>
    {
        public Y Value { get; }

        public Node<Y> Next { get; set; }
        public Node<Y> Previous { get; set; }

        public Node(Y value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Node<Y> node && EqualityComparer<Y>.Default.Equals(Value, node.Value);
        }

        public override int GetHashCode()
        {
            var hashCode = 512;
            hashCode = ~hashCode ^ -256 + EqualityComparer<Y>.Default.GetHashCode(Value);
            return hashCode;
        }
    }
}