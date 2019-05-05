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

        public async Task WaitForCompletion()
        {
            while (bus.IsConnected)
                await Task.Delay(1000);
        }

        private async Task<FibbonaciResponse> Responder(FibbonaciRequest arg)
        {
            return new FibbonaciResponse(await CalculateFibbonaci(arg.N));
        }

        private async Task<int> CalculateFibbonaci(int n)
        {
            if (n < 0)
                return -1;
            var a = GetOrRequestFibbonaci(n - 1);
            var b = GetOrRequestFibbonaci(n - 2);
            return (await a) + (await b);
        }

        private async Task<int> GetOrRequestFibbonaci(int n)
        {
            if (TryGetCachedFibbonaci(n, out int result))
                return result;

            else
                return (await bus.RequestAsync<FibbonaciRequest, FibbonaciResponse>(new FibbonaciRequest(n - 1))).Value;
        }

        private bool TryGetCachedFibbonaci(int n, out int result)
        {
            if (n == 0)
            {
                result = 0;
                return true;
            }
            if (n == 1)
            {
                result = 1;
                return true;
            }
            if (n == 2)
            {
                result = 1;
                return true;
            }
            result = -1;
            return false;
        }

        public void Dispose()
        {
            bus?.Dispose();
        }
    }
}
