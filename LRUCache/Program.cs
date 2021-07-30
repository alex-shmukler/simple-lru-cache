using System;

namespace LRUCache
{
    class Program
    {
        static void Main(string[] args)
        {
            LruCache<string, object> lruCache = new LruCache<string, object>(2);

            lruCache.Add("item1", new object());

            lruCache.Add("item2", new object());

            lruCache.Add("item3", new object());

            if (lruCache.TryGetValue("item1", out object item1))
            {
                // do something with item1
            }
            else
            {
                lruCache.Add("item1", new object());
            }
        }
    }
}
