namespace ScheduleBox.Tests
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage response;

        public FakeHttpMessageHandler(HttpResponseMessage response)
        {
            this.response = response;
        }

        public HttpRequestMessage Request { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.Request = request;
            return Task.FromResult(this.response);
        }
    }
}