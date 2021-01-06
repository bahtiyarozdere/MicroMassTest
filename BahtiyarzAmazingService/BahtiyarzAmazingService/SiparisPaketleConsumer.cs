using System;
using System.Threading.Tasks;
using BurhansService;
using MassTransit;

namespace BahtiyarzAmazingService
{
    public class SiparisPaketleConsumer : IConsumer<SiparisPaketle>
    {
        public async Task Consume(ConsumeContext<SiparisPaketle> context)
        {
            Console.WriteLine("Paketleniyor");
        }
    }

}




