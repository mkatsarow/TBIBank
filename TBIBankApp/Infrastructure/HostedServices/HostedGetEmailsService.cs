using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TBIApp.MailClient.Client.Contracts;
using TBIBankApp.Hubs;

namespace TBIBankApp.Infrastructure.HostedServices
{
    public class HostedGetEmailsService : IHostedService
    {

        private readonly IHubContext<NotificationHub> hubContext;
        private readonly IServiceProvider serviceProvider;
        private Timer timer;

        public HostedGetEmailsService(IHubContext<NotificationHub> hubContext, IServiceProvider serviceProvider)
        {
            this.hubContext = hubContext;
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(GetEmails, null, TimeSpan.FromSeconds(0),
               TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private async void GetEmails(object state)
        {
            using (var scope = this.serviceProvider.CreateScope())
            {
                var gmailApiService = scope.ServiceProvider.GetRequiredService<IGmailAPIService>();

                var count = await gmailApiService.SyncEmails();

                if (count != 0)
                {
                    await this.hubContext.Clients.All.SendAsync("RecieveNewEmails",count);

                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
