using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace hotelEase.Services
{
    public class NotificationWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBus _bus;
        public NotificationWorker(IServiceProvider serviceProvider, IBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _bus.PubSub.SubscribeAsync<Model.NotificationMessage>("notification_handler", async msg =>
                {
                    using var scope = _serviceProvider.CreateScope();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();


                    if (msg.Type == "email")
                    {
                        await emailService.SendEmailAsync(msg.To, msg.Subject, msg.Body);
                    }

                    //if(msg.Type == "push")
                    //{
                    //    var pushService = scope.ServiceProvider.GetRequiredService<IPushService>();

                    //}
                });
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine("RabbitMQ ERROR: " + ex.Message);
                }

            }
        }
    }
}
