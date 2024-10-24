using System;
using Unity.VisualScripting;

namespace Prototype.Scripts.Common.Pools
{
    public class ConstSizePool<T> : IPool<T>, IInitializable, IDisposable
        where T : IPoolableObject
    {
        private readonly T[] _pool;
        private readonly bool[] _flags;
        private int _nextFreeId;
        private readonly Func<T> _instantiateMethod;

        public ConstSizePool(int size, Func<T> instantiateMethod)
        {
            _pool = new T[size];
            _flags = new bool[size];
            _instantiateMethod = instantiateMethod;
        }

        public void Initialize()
        {
            for (int i = 0; i < _pool.Length; i++)
            {
                _pool[i] = _instantiateMethod.Invoke();
                _pool[i].PoolObjectId = i;
                _flags[i] = false;
            }
            _nextFreeId = 0;
        }

        public T GetItem()
        {
            var id = GetNextFreeId();
            IncrementNextFreeId();
            if (id == -1 || _flags[id]) return default;

            _flags[id] = true;
            return _pool[id];
        }

        public void Release(T obj)
        {
            _flags[obj.PoolObjectId] = false;
        }

        private int GetNextFreeId()
        {
            int count = 0;
            while (_flags[_nextFreeId])
            {
                count++;
                IncrementNextFreeId();

                if (count > _pool.Length)
                {
                    return -1;
                }
            }

            return _nextFreeId;
        }
        private void IncrementNextFreeId()
        {
            if (++_nextFreeId >= _pool.Length)
            {
                _nextFreeId = 0;
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < _pool.Length; i++)
            {
                var disposable = _pool[i] as IDisposable;
                disposable?.Dispose();

                _pool[i] = default;
                _flags[i] = true;
            }

            _nextFreeId = -1;
        }
    }
}