using StackExchange.Redis;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Testing
{
    public class RedisCore
    {
        public ConnectionMultiplexer InitConfig()
        {
            var currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
           // var caCert = new X509Certificate2(Path.Combine(currentPath, "ssl", "ca-cert.pem"));
            var clientCert = new X509Certificate2(Path.Combine(currentPath, "ssl", "redis-client.pfx"), "123456");

            var config = new ConfigurationOptions
            {
                EndPoints = { "127.0.0.1:6379" },
                Ssl = true,
                AbortOnConnectFail = false,
                //  AllowAdmin = true,
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
