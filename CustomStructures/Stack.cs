using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using BPCode.Helpers;

namespace BPCode.CustomImpl
{
    public class Stack<T> : IEnumerable<T>, IEnumerable
    {
        private Node<T> lastNode;

        public int Length { get; private set; }

        public void Push(T value)
        {
            var node = new Node<T>(value);

            if (lastNode == null)
            {
                lastNode = node;
            }
            else
            {
                node.Next = lastNode;
                lastNode = node;
            }

            Length++;
        }

        public T Pop()
        {
            if (Length == 0)
                RiseStackIsEmpty();

            var node = lastNode;
            lastNode = node.Next;
            node.Next = null;
            Length--;

            return node.Value;
        }

        public bool TryPop(out T value)
        {
            value = default;
            
            if (Length == 0)
                return false;

            value = lastNode.Value;

            return true;
        }

        public T Peek()
        {
            if (Length == 0)
                RiseStackIsEmpty();

            return lastNode.Value;
        }

        #region IEnumerable implementation

        public IEnumerator<T> GetEnumerator()
        {
            var node = lastNode;

            while (node != null)
            {
                yield return node.Value;

                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        public T[] ToArray()
        {
            var array = new T[Length];

            var node = lastNode;
            for (var i = 0; i < Length; i++)
            {
                array[i] = node.Value;
                node = node.Next;
            }

            return array;
        }

        public override bool Equals(object obj)
        {
            return obj is Stack<T> stack && Length == stack.Length &&
                   (Length == 0 ? true : (lastNode?.Equals(stack.lastNode) ?? false) &&
                                         this.Skip(1).SequenceEqual(stack.Skip(1)));
        }

        public override int GetHashCode()
        {
            var hashCode = -512;
            hashCode = hashCode ^ -256 + this.Fold((x, acc) => x.GetHashCode() + acc, 0);
            hashCode = hashCode & -256 + Length;
            return hashCode;
        }

        private static void RiseStackIsEmpty()
        {
            throw new InvalidOperationException("Cannot extract item from empty stack");
        }
    }
}