using System;
using System.Threading.Tasks;
using MassTransit;

namespace BahtiyarzAmazingService
{
    public class SiparisOlusturConsumer : IConsumer<SiparisOlustur>
    {

        public async Task Consume(ConsumeContext<SiparisOlustur> context)
        {
            // Siparişi oluşturuyorum
            Console.WriteLine($"Sipariş oluşturuldu {context.Message.SiparisNo}");
        }
    }

}




