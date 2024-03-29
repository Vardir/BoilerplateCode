using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Vardirsoft.Shared.Helpers;

namespace Vardirsoft.Shared.CustomImpl.Collections
{
    public class Stack<T> : IEnumerable<T>, IEnumerable
    {
        private Node<T> _lastNode;

        public int Length { get; private set; }

        public void Push(T value)
        {
            var node = new Node<T>(value);

            if (_lastNode == null)
            {
                _lastNode = node;
            }
            else
            {
                node.Next = _lastNode;
                _lastNode = node;
            }

            Length++;
        }

        public T Pop()
        {
            if (Length == 0)
                RiseStackIsEmpty();

            var node = _lastNode;
            _lastNode = node.Next;
            node.Next = null;
            Length--;

            return node.Value;
        }

        public bool TryPop(out T value)
        {
            value = default;
            
            if (Length == 0)
                return false;

            value = _lastNode.Value;

            return true;
        }

        public T Peek()
        {
            if (Length == 0)
                RiseStackIsEmpty();

            return _lastNode.Value;
        }

        public void Clear()
        {
            Length = 0;
            _lastNode.Next = null;
        }

        #region IEnumerable implementation

        public IEnumerator<T> GetEnumerator()
        {
            var node = _lastNode;

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

            var node = _lastNode;
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
                   (Length == 0 ? true : (_lastNode?.Equals(stack._lastNode) ?? false) &&
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