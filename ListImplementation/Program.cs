using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ListImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            List list = new List(6);
        }
    }
    public class List
    {
        private const int _defaultCapacity = 4;
        private int[] _items;
        private int _size;
        private int _version;

        static readonly int[] _emptyArray = new int[0];
        public List()
        {
            _items = _emptyArray;
        }
        public List(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            if (capacity == 0)
                _items = _emptyArray;
            else
                _items = new int[capacity];
        }
        public int Capacity
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return _items.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        int[] _newItems = new int[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, _newItems, 0, _size);
                        }
                        _items = _newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }
                }

            }
        }
        public override string ToString()
        {
            return $"Count={_size}";
        }
        public int Count
        {
            get
            {
                return _size;
            }
        }

        public int this[int index]
        {
            get
            {
                if ((uint)index > (uint)_size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _items[index];
            }
            set
            {
                if ((uint)index > (uint)_size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _items[index] = value;
                _version++;
            }
        }

        public void Add(int item)
        {
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            _items[_size++] = item;
            _version++;
        }
        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length;
                if (_items.Length == 0)
                {
                    newCapacity = _defaultCapacity;
                }
                else
                {
                    newCapacity = _items.Length * 2;
                }
                if ((uint)newCapacity > 0X7FEFFFFF)
                    newCapacity = 0X7FEFFFFF;
                if (newCapacity < min)
                    newCapacity = min;
                Capacity = newCapacity;
            }
        }

        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
                _size = 0;
            }
            _version++;
        }
    }
}

