using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Vardirsoft.Shared.Helpers;

namespace Vardirsoft.Shared.CustomImpl.Collections
{
    public class Queue<T> : IEnumerable<T>, IEnumerable
    {
        private Node<T> _firstNode;
        private Node<T> _lastNode;

        public int Length { get; private set; }

        public void Enqueue(T value)
        {
            var node = new Node<T>(value);

            if (_firstNode == null)
            {
                _firstNode = node;
                _lastNode = node;
            }
            else
            {
                _lastNode.Previous = node;
                _lastNode = node;
            }

            Length++;
        }

        public T Dequeue()
        {
            if (Length == 0)
                RiseQueueIsEmpty();

            var node = _firstNode;

            _firstNode = node.Previous;
            node.Previous = null;
            Length--;

            return node.Value;
        }

        public bool TryDequeue(out T value)
        {
            value = default;

            if (Length == 0)
                return false;

            value = Dequeue();
            
            return true;
        }

        public T Peek()
        {
            if (Length == 0)
                RiseQueueIsEmpty();

            return _firstNode.Value;
        }

        public void Clear()
        {
            Length = 0;
            _firstNode.Next = null;
            _lastNode.Previous = null;
            _firstNode = null;
            _lastNode = null;
        }

        #region IEnumerable implementation

        public IEnumerator<T> GetEnumerator()
        {
            var node = _firstNode;

            while (node != null)
            {
                yield return node.Value;

                node = node.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        public T[] ToArray()
        {
            var array = new T[Length];

            var node = _firstNode;
            for (var i = 0; i < Length; i++)
            {
                array[i] = node.Value;
                node = node.Previous;
            }

            return array;
        }

        public override bool Equals(object obj)
        {
            return obj is Queue<T> queue && Length == queue.Length &&
                   (Length == 0 ? true : (_firstNode?.Equals(queue._firstNode) ?? false) &&
                                         (_lastNode?.Equals(queue._lastNode) ?? false) &&
                                         this.Skip(1).SequenceEqual(queue.Skip(1)));
        }

        public override int GetHashCode()
        {
            var hashCode = -512;
            hashCode = hashCode ^ -256 + this.Fold((x, acc) => x.GetHashCode() + acc, 0);
            hashCode = hashCode & -256 + Length;
            return hashCode;
        }

        private static void RiseQueueIsEmpty()
        {
            throw new InvalidOperationException("Cannot extract item from empty queue");
        }
    }
}