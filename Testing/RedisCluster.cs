using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Testing
{
   public class RedisCluster
    {
        public ConnectionMultiplexer InitConfig()
        {
            var currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
            // var caCert = new X509Certificate2(Path.Combine(currentPath, "ssl", "ca-cert.pem"));
            var clientCert = new X509Certificate2(Path.Combine(currentPath, "ssl", "redis-client.pfx"), "123456");

            var config = new ConfigurationOptions
            {
                EndPoints = { "192.168.1.90:7000", "192.168.1.90:7001", "192.168.1.90:7002", "192.168.1.90:7003", "192.168.1.90:7004", "192.168.1.90:7005" },
                Ssl = true,
                Password = "passServer",
                AbortOnConnectFail = false,
                AllowAdmin = true,
                ConnectTimeout = 30000,
                SyncTimeout = 30000,
                SslProtocols = SslProtocols.Tls12,
            };

            config.CertificateSelection += delegate
            {
                return clientCert;
            };

            config.CertificateValidation += (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };


            return ConnectionMultiplexer.Connect(config);
        }

        public void TestConfig(ConnectionMultiplexer connection)
        {
            try
            {
                var server = connection.GetDatabase();
                var result = server.StringSet("Demo", "Demo");
                var data = server.StringGet("Demo");
            }
            catch (System.Exception ex)
            {


            }

        }
    }
}
