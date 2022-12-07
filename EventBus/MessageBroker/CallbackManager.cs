using Models.Library;
using Models.Library.Events;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker
{
    public class CallbackManager : ICallbackManager
    {
        private readonly Callback _callback;
        private readonly HttpClient _httpClient;

        public CallbackManager(Callback callback)
        {
            _callback = callback;
            _httpClient = new HttpClient();
        }

        public async Task<HttpStatusCode?> CallAsync<ContentT>(ContentT content) where ContentT : BaseOrderEvent
        {
            switch (_callback.HttpCallbackMethod)
            {
                case Models.Library.HttpMethod.GET:
                    return await GetCallbackAsync<ContentT>(content);
                case Models.Library.HttpMethod.POST:
                    return await PostCallbackAsync<ContentT>(content);
            }
            return null;
        }

        private async Task<HttpStatusCode?> GetCallbackAsync<ContentT>(ContentT content) where ContentT : BaseOrderEvent
        {
            var evntString = JsonConvert.SerializeObject(content);
            var result = await _httpClient.GetAsync(_callback.CallbackURL + $"?payload={Uri.EscapeDataString(evntString)}");
            return result.StatusCode;
        }

        private async Task<HttpStatusCode?> PostCallbackAsync<ContentT>(ContentT content) where ContentT : BaseOrderEvent
        {
            var evntString = JsonConvert.SerializeObject(content);
            var result = await _httpClient.PostAsJsonAsync(_callback.CallbackURL, evntString);
            return result.StatusCode;
        }
    }
}
