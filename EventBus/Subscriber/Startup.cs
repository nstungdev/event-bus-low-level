using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models.Library;
using Models.Library.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Subscriber
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            Subscribe();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void Subscribe()
        {
            try
            {
                string messageBrokerUrl = "http://localhost:5000/";
                string subscriberUrl = "http://localhost:5001/";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(messageBrokerUrl);

                var subscribePayload = new SubscribePayload();
                subscribePayload.Type = typeof(AddressChangeEvent).Name;
                subscribePayload.Callback = new Callback() { CallbackURL = subscriberUrl + "Receive", HttpCallbackMethod = Models.Library.HttpMethod.POST };

                var postContent = new StringContent(JsonConvert.SerializeObject(subscribePayload));
                postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var result = client.PostAsync("Subscribe", postContent).GetAwaiter().GetResult();
                if (result.IsSuccessStatusCode)
                    Console.WriteLine("Service Subscribed");
                else
                    Console.WriteLine("Failed to Subscribe");
            }
            catch
            {
                Console.WriteLine("Failed to Subscribe");
            }
        }
    }
}
