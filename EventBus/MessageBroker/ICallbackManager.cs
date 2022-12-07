using Models.Library.Events;
using System.Net;
using System.Threading.Tasks;

namespace MessageBroker
{
    public interface ICallbackManager
    {
        Task<HttpStatusCode?> CallAsync<ContentT>(ContentT content) where ContentT : BaseOrderEvent;
    }
}
