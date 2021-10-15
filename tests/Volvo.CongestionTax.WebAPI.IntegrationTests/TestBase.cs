using System.Net.Http;

namespace Volvo.CongestionTax.WebAPI.IntegrationTests
{
    public class TestBase
    {
        public TestBase()
        {
            var server = new TestHost<Startup>();
            Client = server.CreateClient();
        }

        public HttpClient Client { get; }
    }
}