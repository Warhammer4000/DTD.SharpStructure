using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpStructure
{
    public class Fring<T>:IList<T>,IEqualityComparer<T>
    {
        private T[] _items;
        private int _size;

        #region Properties

        public bool IsEmpty => TopOfStack == -1;
        public int TopOfStack { get; private set; }

        #endregion

        #region Pointers

        //Stack

        //Queue
        private int _front;

        #endregion


        #region Constructor

        public Fring()
        {
            _size = 100;
            Initalize();
        }

        public Fring(int size)
        {
            _size = size;
            Initalize();
        }

        public Fring(Fring<T> item)
        {
            _size = item._size;
            Initalize();
            _items = item._items;
        }

        #endregion

        #region Configuration

        public bool Resizable { get; set; }
        public int MaxSize { get; set; }
        public bool TrimEnd { get; set; }

        #endregion

        private void Initalize()
        {
            _front = 0;
            TopOfStack = -1; //this is the back
            Resizable = true;
            MaxSize = -1;
            _items = new T[_size];
            
        }

        private void Resize()
        {
            if (!Resizable) throw new Exception("Size limit!");
            if (MaxSize != -1)
                if (MaxSize == _size)
                {
                    if (!TrimEnd)
                        throw new Exception("Size limit!");
                    Chop(); //trims it so resize doesn't matter
                }

            Array.Resize(ref _items, ++_size);
        }

        private void Chop(int index = 0)
        {
            //Expensive
            for (var i = index; i < _size - 1; i++) _items[i] = _items[i + 1];

            _size--;
        }


        #region Stack

        /// <summary>
        ///     Pushes one item in the array. Topofstack+1 and back +1 as well
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            if (TopOfStack + 1 >= _size) Resize();
            _items[++TopOfStack] = item;
        }

        /// <summary>
        ///     Removes last item pushed on stack and also from the queue[Not really]
        /// </summary>
        /// <returns>last item</returns>
        public T Pop()
        {
            if (TopOfStack == -1) throw new Exception("Stack is empty");
            return _items[TopOfStack--];
        }

        /// <summary>
        ///     Removes nothing just shows the last item pushed
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return _items[TopOfStack];
        }

        #endregion

        #region Queue

        public void Enqueue(T item)
        {
            if (TopOfStack + 1 >= _size) Resize();

            _items[++TopOfStack] = item;
        }

        public T Dequeue()
        {
            if (TopOfStack == -1) throw new Exception("Queue is empty");
            return _items[TopOfStack--];
        }

        public T First()
        {
            return _items[_front];
        }

        public T Last()
        {
            return Peek();
        }

        #endregion

        #region List

        private void RemoveAtIndex(int index)
        {
            Chop(index);
        }

        public T GetAtIndex(int index)
        {
            if (index < 0 || index >= _size) throw new Exception("Invalid index");
            return _items[index];
        }

        //TODO can be better
        private void InsertAt(T item, int index)
        {
            Resize(); //increases size by one
            var temp = _items; //Memory inefficient
            for (var i = index; i < _size - 1; i++) _items[i + 1] = temp[i];
            _items[index] = item;
        }

        #endregion


        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _items)
            {
                if (item == null) break;

                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void Add(T item)
        {
            Push(item);
        }

        public void Clear()
        {
           Initalize();
        }

        public bool Contains(T item)
        {
            foreach (var data in _items)
            {
                if (data.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < arrayIndex+_size; i++)
            {
                array[i] = _items[i - arrayIndex];
            }
            
        }

        public bool Remove(T item)
        {
            for (var index = 0; index < _items.Length; index++)
            {
                var data = _items[index];
                if (!data.Equals(item)) continue;
                RemoveAtIndex(index);
                return true;
            }

            return false;
        }

        public int Count { get=>_size; }
        public bool IsReadOnly { get; }
        public int IndexOf(T item)
        {
            for (var index = 0; index < _items.Length; index++)
            {
                var dataItem = _items[index];
                if (dataItem.Equals(item))
                {
                    return index;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            InsertAt(item,index);
        }

        public void RemoveAt(int index)
        {
            RemoveAtIndex(index);
        }

        public T this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        public bool Equals(T x, T y)
        {
            
            return !EqualityComparer<T>.Default.Equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return IndexOf(obj);

        }
    }
}
