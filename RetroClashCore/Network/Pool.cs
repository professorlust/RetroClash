using System.Collections.Concurrent;

namespace RetroClashCore.Network
{
    public class Pool<T>
    {
        private readonly ConcurrentQueue<T> _stack;

        public Pool()
        {
            _stack = new ConcurrentQueue<T>();
        }

        public T Pop
        {
            get
            {
                var ret = default(T);

                if (_stack.Count > 0)
                    _stack.TryDequeue(out ret);

                return ret;
            }
        }

        public int Count => _stack.Count;

        public void Push(T item)
        {
            if (_stack.Count < Configuration.MaxClients)
                _stack.Enqueue(item);
        }
    }
}