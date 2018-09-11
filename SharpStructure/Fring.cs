using System;

namespace SharpStructure
{
    public class Fring<T>
    {
        private T[] _items;
        private int _size;

        #region Pointers

        //Stack
        private int topOfStack;

        //Queue
        private int front;

        #endregion

        #region Flags
        public bool Stack { get;private set; }
        public bool List { get;private set; }
        public bool Queue { get;private set; }

        #endregion

        #region Constructor

        public Fring()
        {
            _size = 100;
            Initalize();
        }

        public Fring(int size)
        {
            this._size = size;
            Initalize();
        }

        public Fring(Fring<T> item)
        {
            _size = item._size;
            Initalize();
            _items = item._items;
        }

        #endregion

        private void Initalize()
        {
           
            front = 0;
            topOfStack = -1;//this is the back
            List = true;
            _items = new T[_size];
        }

        private void Resize()
        {
            Array.Resize(ref _items,_size+1);
        }


        #region Stack

        /// <summary>
        /// Pushes one item in the array. Topofstack+1 and back +1 as well
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            if (topOfStack + 1 >= _size)
            {
                Resize();
            }
            _items[++topOfStack] = item;
        }

        /// <summary>
        /// Removes last item pushed on stack and also from the queue[Not really]
        /// </summary>
        /// <returns>last item</returns>
        public T Pop()
        {
            if (topOfStack == -1) throw new Exception("Stack is empty");
            return _items[topOfStack--];
           
        }

        /// <summary>
        /// Removes nothing just shows the last item pushed
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return _items[topOfStack];
        }

        #endregion

        #region Queue

        public void Enqueue(T item)
        {
            if (topOfStack + 1 >= _size)
            {
                Resize();
            }

            _items[++topOfStack] = item;
       

        }

        public T Dequeue()
        {
            if (topOfStack == -1) throw new Exception("Queue is empty");
            return _items[topOfStack--];
         
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

    }
}
