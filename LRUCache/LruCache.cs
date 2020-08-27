using System.Collections.Generic;

namespace LRUCache
{
    public class LruCache<TKey, TValue>
    {
        private readonly int Capacity;

        private readonly Dictionary<TKey, LinkedListNode<LruCacheItem>> CacheMap = new Dictionary<TKey, LinkedListNode<LruCacheItem>>();

        private readonly LinkedList<LruCacheItem> LruList = new LinkedList<LruCacheItem>();

        private static readonly object LockObject = new object();

        public LruCache(int capacity)
        {
            Capacity = capacity;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (LockObject)
            {
                if (CacheMap.TryGetValue(key, out LinkedListNode<LruCacheItem> node))
                {
                    value = node.Value.Value;

                    LruList.Remove(node);

                    LruList.AddLast(node);

                    return true;
                }

                value = default;

                return false;
            }
        }

        public void Add(TKey key, TValue value)
        {
            lock (LockObject)
            {
                if (CacheMap.Count >= Capacity)
                {
                    CacheMap.Remove(LruList.First.Value.Key);

                    LruList.RemoveFirst();
                }

                LinkedListNode<LruCacheItem> node = new LinkedListNode<LruCacheItem>(new LruCacheItem(key, value));

                LruList.AddLast(node);

                CacheMap.Add(key, node);
            }
        }

        private class LruCacheItem
        {
            public TKey Key { get; }

            public TValue Value { get; }

            public LruCacheItem(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}
