using System.Net.Http;
using System.Threading.Tasks;

namespace TestApp.Core
{
    public interface IHttpGetRequestSender
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}