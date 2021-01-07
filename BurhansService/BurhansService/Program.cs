using MassTransit;
using System;
using System.Threading.Tasks;

namespace BurhansService
{
    //192.168.0.119
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(conf =>
            {

                conf.Host("192.168.8.110", hc =>
                {
                    hc.Username("rabus");
                    hc.Password("rabus");
                });
                conf.ReceiveEndpoint("paket", ep =>
                 {
                     ep.Consumer<SiparisPaketleConsumer>();
                 });

            });
            await bus.StartAsync();

            do
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.B)
                {
                    await bus.Publish<SiparisPaketle>(new
                    {
                       
                        SiparisNo = "Paket Uzak 1"
                    });

                };
            } while (true);

            //var komut = new SiparisPaketle();
            //komut.SiparisPaketNo = "Paket 1";
            //komut.SiparisNo = "Siparis no:5";
            //await bus.Publish(komut);
            Console.WriteLine("Çıkış yapmak için herhangi bir tuşa basınız");
            Console.ReadKey();

            await bus.StopAsync();
        }
    }
    public interface SiparisPaketle
    {
        public string SiparisNo { get; set; }
        

       
    }
    public interface SiparisPaketlendi
    {
        public string SiparisPaketNo { get; set; }
        public string SiparisNo { get; set; }
        public DateTime Tarih { get; set; }
    }

    public class SiparisPaketleConsumer : IConsumer<SiparisPaketle>
    {
        public async Task Consume(ConsumeContext<SiparisPaketle> context)
        {
            var paketNo = Guid.NewGuid().ToString();
            //Paket oluşturuyorum
            Console.WriteLine($"Paket oluşturuldu{context.Message.SiparisNo} - {paketNo} ");
            await Task.Delay(5000);
            await context.Publish<SiparisPaketlendi>(new
            { 
                SiparisPaketNo =paketNo,
                SiparisNo = context.Message.SiparisNo,
                Tarih = DateTime.Now
            });
        }
    }

}
