using System;

namespace SharpStructure
{
    public class Fring<T>
    {
        private T[] _items;
        private int _size;

        #region Properties

        public bool IsEmpty => Length == -1;
        public int Length { get; private set; }

        #endregion

        #region Pointers

        //Stack

        //Queue
        private int front;

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
            front = 0;
            Length = -1; //this is the back
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
            if (Length + 1 >= _size) Resize();
            _items[++Length] = item;
        }

        /// <summary>
        ///     Removes last item pushed on stack and also from the queue[Not really]
        /// </summary>
        /// <returns>last item</returns>
        public T Pop()
        {
            if (Length == -1) throw new Exception("Stack is empty");
            return _items[Length--];
        }

        /// <summary>
        ///     Removes nothing just shows the last item pushed
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return _items[Length];
        }

        #endregion

        #region Queue

        public void Enqueue(T item)
        {
            if (Length + 1 >= _size) Resize();

            _items[++Length] = item;
        }

        public T Dequeue()
        {
            if (Length == -1) throw new Exception("Queue is empty");
            return _items[Length--];
        }

        public T First()
        {
            return _items[front];
        }

        public T Last()
        {
            return Peek();
        }

        #endregion

        #region List

        public void RemoveAtIndex(int index)
        {
            Chop(index);
        }

        public T GetAtIndex(int index)
        {
            if (index < 0 || index >= _size) throw new Exception("Invalid index");
            return _items[index];
        }

        //TODO can be better
        public void InsertAt(T item, int index)
        {
            Resize(); //increases size by one
            var temp = _items; //Memory inefficient
            for (var i = index; i < _size - 1; i++) _items[i + 1] = temp[i];
            _items[index] = item;
        }

        #endregion


     
    }
}
