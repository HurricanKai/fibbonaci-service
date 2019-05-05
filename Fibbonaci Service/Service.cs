using EasyNetQ;
using Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fibbonaci_Service
{
    public class Service : IDisposable
    {
        private IBus bus;

        public Service(string connectionString)
        {
            bus = RabbitHutch.CreateBus(connectionString);
        }

        public void Init()
        {
            bus.RespondAsync<FibbonaciRequest, FibbonaciResponse>(Responder);
        }

        public void WaitForCompletion()
        {
            while (bus.IsConnected)
                Task.Delay(1000);
        }

        private async Task<FibbonaciResponse> Responder(FibbonaciRequest arg)
        {
            return new FibbonaciResponse(await CalculateFibbonaci(arg.N));
        }

        private async Task<int> CalculateFibbonaci(int n)
        {
            var a = bus.RequestAsync<FibbonaciRequest, FibbonaciResponse>(new FibbonaciRequest(n - 1));
            var b = bus.RequestAsync<FibbonaciRequest, FibbonaciResponse>(new FibbonaciRequest(n - 2));
            return (await a).Value + (await b).Value;
        }

        public void Dispose()
        {
            bus?.Dispose();
        }
    }
}
