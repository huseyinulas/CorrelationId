using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CorrelationId
{
    public class UserAgentMessageHandler : DelegatingHandler
    {
        private readonly string _headerName = "User-Agent";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains(_headerName))
            {
                request.Headers.Add(_headerName, $"{AppInfo.Name}/{AppInfo.Version.ToString()}");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
