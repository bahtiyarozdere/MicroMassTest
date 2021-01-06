using BurhansService;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace BildirimServis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            var bus = Bus.Factory.CreateUsingRabbitMq(conf =>
            {
                conf.Host("rabbitmq://localhost", r =>
                {
                    r.Username("guest");
                    r.Password("guest");
                });
                conf.ReceiveEndpoint("bildirim", ep =>
                {
                    ep.Consumer<SiparisPaketlendiConsumer>();
                });
            });
            Console.WriteLine("Bildirim Servis Başlatılıyor");
            bus.Start();

            Console.ReadKey();

            bus.Stop();
        }

        internal class SiparisPaketlendiConsumer : IConsumer<SiparisPaketlendi>
        {
            public async Task Consume(ConsumeContext<SiparisPaketlendi> context)
            {
                Console.WriteLine(" === YENİ SMS ===");
                Console.WriteLine($"Sipariş No : {context.Message.SiparisNo}");
                Console.WriteLine($"Sipariş Paket No : {context.Message.SiparisPaketNo}");
                Console.WriteLine($"Tarih : {context.Message.Tarih}");
            }
        }

    }
}
