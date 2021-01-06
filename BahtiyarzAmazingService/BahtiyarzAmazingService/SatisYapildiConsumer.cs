using System;
using System.Threading.Tasks;
using BurhansService;
using MassTransit;
using Satis.Abstractions;

namespace BahtiyarzAmazingService
{
    public class SatisYapildiConsumer : IConsumer<SatisYapildi>
    {
        public async Task Consume(ConsumeContext<SatisYapildi> context)
        {
            SatisAlindiGoster(context);
            UrunleriListele(context);
            await PaketlemeIstegiGonder(context);

            Console.WriteLine("---------");
            await Task.CompletedTask;
        }

        private  async Task PaketlemeIstegiGonder(ConsumeContext<SatisYapildi> context)
        {
            Console.WriteLine();
            Console.WriteLine("Paketleme isteği gönderiliyor.");
            Console.WriteLine("...");
            await Task.Delay(3000);
            var sendEndpoint = await context.GetSendEndpoint(new Uri("queue:paket"));
            await sendEndpoint.Send<SiparisPaketle>(new
            {
                SiparisNo = "Paket Uzak 1",
            });
            Console.WriteLine("Paketleme isteği gönderildi.");
        }

        private void UrunleriListele(ConsumeContext<SatisYapildi> context)
        {
            foreach (var item in context.Message.Urunler)
            {
                Console.WriteLine($"- {item.Adet} :  {item.UrunKodu}");
            }
        }

        private void SatisAlindiGoster(ConsumeContext<SatisYapildi> context)
        {
            Console.Clear();
            Console.WriteLine("---------");
            Console.WriteLine("Satış bilgisi alındı " + context.Message.Urunler.Count);
        }
    }

}




