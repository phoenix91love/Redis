using System;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisCore redis = new RedisCore();
            var config = redis.InitConfig();
            redis.TestConfig(config);
            Console.WriteLine("Hello World!");
        }

        
    }
}
