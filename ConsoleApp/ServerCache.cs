using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ServerCache<T>
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private int _expireTime = 5;
        private MemoryCacheEntryOptions _slidingOptions;

        public ServerCache(int expireTime)
        {
            _expireTime = expireTime;
            _slidingOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(_expireTime));
        }

        public void Set(string key, T value)
        {
            _cache.Set(key, value, _slidingOptions);
        }

        public T? Get(string key)
        {
            return _cache.Get<T>(key);
        }


        public static void Test()
        {
            {
                ServerCache<int> serverCache = new ServerCache<int>(5);
                // 3. 演示滑动过期 (例如: 5秒内无访问则过期)
                string slideKey = "slideData";
                serverCache.Set(slideKey, 444);

                // 每 2 秒访问一次，保持缓存活跃
                for (int i = 0; i < 3; i++)
                {
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine($"[第{i + 1}次访问/2秒后] 滑动缓存值: {serverCache.Get(slideKey)}");
                }

                System.Threading.Thread.Sleep(6000);
                var slideValue = serverCache.Get(slideKey);
                Console.WriteLine($"[最终] 滑动缓存是否过期: {(slideValue == 0 ? "是" : "否")}");
            }
            {
                ServerCache<string> serverCache = new ServerCache<string>(5);
                // 3. 演示滑动过期 (例如: 5秒内无访问则过期)
                string slideKey = "slideData";
                serverCache.Set(slideKey, "abc");

                // 每 2 秒访问一次，保持缓存活跃
                for (int i = 0; i < 3; i++)
                {
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine($"[第{i + 1}次访问/2秒后] 滑动缓存值: {serverCache.Get(slideKey)}");
                }

                System.Threading.Thread.Sleep(6000);
                var slideValue = serverCache.Get(slideKey);
                Console.WriteLine($"[最终] 滑动缓存是否过期: {(slideValue == null ? "是" : "否")}");
            }
        }

        public static void Add()
        {
            // 1. 创建 MemoryCache 实例
            var cache = new MemoryCache(new MemoryCacheOptions());

            string key = "myData";

            // 2. 设置带绝对过期的缓存 (例如: 10秒后过期)
            var absoluteOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

            cache.Set(key, "Hello, Cache!", absoluteOptions);
            Console.WriteLine($"[初始] 获取缓存: {cache.Get<string>(key)}");

            // 等待 5 秒
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine($"[5秒后] 获取缓存: {cache.Get<string>(key)}");

            // 再等待 6 秒 (总共11秒，已过期)
            System.Threading.Thread.Sleep(6000);
            var value = cache.Get<string>(key);
            if (value == null)
            {
                Console.WriteLine("[11秒后] 缓存已过期，值为 null");
            }
            else
            {
                Console.WriteLine($"[11秒后] 获取缓存: {value}");
            }

            // 3. 演示滑动过期 (例如: 5秒内无访问则过期)
            string slideKey = "slideData";
            var slidingOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(5));

            cache.Set(slideKey, "Sliding Data", slidingOptions);
            Console.WriteLine($"\n[滑动过期测试] 初始值: {cache.Get<string>(slideKey)}");

            // 每 2 秒访问一次，保持缓存活跃
            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine($"[第{i + 1}次访问/2秒后] 滑动缓存值: {cache.Get<string>(slideKey)}");
            }

            // 等待 6 秒不访问，使其过期
            Console.WriteLine("等待 6 秒不访问...");
            System.Threading.Thread.Sleep(6000);
            var slideValue = cache.Get<string>(slideKey);
            Console.WriteLine($"[最终] 滑动缓存是否过期: {(slideValue == null ? "是" : "否")}");

            cache.Dispose();
        }
    }
}
