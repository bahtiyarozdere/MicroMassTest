using System;
using System.Threading.Tasks;
using BurhansService;
using MassTransit;
using MassTransit.Logging;
using System.Collections;
using System.Collections.Generic;

namespace BahtiyarzAmazingService
{
    class Program
    {
        private static IBusControl bus;

        //192.168.0.119
        static async Task Main(string[] args)
        {
            Greet();
            bus = await SetupBus();
            await ShowMenu(bus);

            Console.WriteLine("Çıkış yapmak için herhangi bir tuşa basınız");
            Console.ReadKey();

            await bus.StopAsync();
        }

        private static async Task ShowMenu(IBusControl bus)
        {
            do
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.P)
                {
                    var sendEndpoint = await bus.GetSendEndpoint(new Uri("queue:paket"));
                    await sendEndpoint.Send<SiparisPaketle>(new
                    {
                        SiparisNo = "Paket Uzak 1",
                    });
                }
                if (key.Key == ConsoleKey.C)
                {
                    break;
                }
                if (key.Key == ConsoleKey.S)
                {
                    await bus.Publish<SiparisOlustur>(new
                    {
                        SiparisNo = "Paket Uzak 1"
                    });
                }

            } while (true);
        }

        private static void Greet()
        {
            Console.Clear();
            Console.WriteLine("Satış Servisi");
        }

        private static async Task<IBusControl> SetupBus()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(conf =>
            {
                conf.Host("rabbitmq://localhost", r =>
                {
                    r.Username("guest");
                    r.Password("guest");
                });
                conf.ReceiveEndpoint("siparis", ep =>
                {
                    ep.Consumer<SiparisOlusturConsumer>();
                    ep.Consumer<SiparisPaketlendiConsumer>();
                    ep.Consumer<SatisYapildiConsumer>();
                    //ep.Consumer<SiparisPaketleConsumer>();
                });
            });

            await bus.StartAsync();
            return bus;
        }
    }

}




