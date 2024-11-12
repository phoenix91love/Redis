using System;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisCluster redis = new RedisCluster();
            var config = redis.InitConfig();
            redis.TestConfig(config);
            Console.WriteLine("Hello World!");
        }

        
    }
}
