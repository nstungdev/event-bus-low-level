using Models.Library.Events;
using Models.Library;
using System;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;

namespace Publisher
{
    class Program
    {
        public const string messageBrokerBaseUrl = "http://localhost:5000";
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(messageBrokerBaseUrl);

            for (int i = 0; i < 10; i++)
            {
                var evnt = new AddressChangeEvent();
                evnt.Id = 100 + i;
                evnt.OrderId = 1000 + i;
                evnt.NewAddress = "new test address";
                evnt.OldAddress = "old test address";
                evnt.Product = "test product";
                evnt.OrderDate = DateTime.Now;
                var evntString = JsonConvert.SerializeObject(evnt);

                var message = new PublishPaload();
                message.Type = typeof(AddressChangeEvent).Name;
                message.Event = evntString;

                var postContent = new StringContent(JsonConvert.SerializeObject(message));
                postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var result = client.PostAsync("Publish", postContent).GetAwaiter().GetResult();
                Console.WriteLine($"Event {evnt.Id} - {result.StatusCode}");
                Thread.Sleep(2000);
            }
        }
    }
}
