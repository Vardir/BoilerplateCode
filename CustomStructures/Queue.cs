using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using BPCode.Helpers;

namespace BPCode.CustomImpl
{
    public class Queue<T> : IEnumerable<T>, IEnumerable
    {
        private Node<T> firstNode;
        private Node<T> lastNode;

        public int Length { get; private set; }

        public void Enqueue(T value)
        {
            var node = new Node<T>(value);

            if (firstNode == null)
            {
                firstNode = node;
                lastNode = node;
            }
            else
            {
                lastNode.Previous = node;
                lastNode = node;
            }

            Length++;
        }

        public T Dequeue()
        {
            if (Length == 0)
                RiseQueueIsEmpty();

            var node = firstNode;

            firstNode = node.Previous;
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

            return firstNode.Value;
        }

        #region IEnumerable implementation

        public IEnumerator<T> GetEnumerator()
        {
            var node = firstNode;

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

            var node = firstNode;
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
                   (Length == 0 ? true : (firstNode?.Equals(queue.firstNode) ?? false) &&
                                         (lastNode?.Equals(queue.lastNode) ?? false) &&
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