using BurhansService;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace BahtiyarzAmazingService
{
    internal class SiparisPaketlendiConsumer : IConsumer<SiparisPaketlendi>
    {
        public async Task Consume(ConsumeContext<SiparisPaketlendi> context)
        {

            Console.WriteLine($"Sipariş No : {context.Message.SiparisNo}");
            Console.WriteLine($"Sipariş Paket No : {context.Message.SiparisPaketNo}");
            Console.WriteLine($"Tarih : {context.Message.Tarih}");
        }
    }
}