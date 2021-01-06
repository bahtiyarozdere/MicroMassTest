using MassTransit;
using Satis.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Satis
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Clear();
            var bus = Bus.Factory.CreateUsingRabbitMq(conf =>
            {
                conf.Host("rabbitmq://localhost", r =>
                {
                    r.Username("guest");
                    r.Password("guest");
                });
                conf.ReceiveEndpoint("satis", ep =>
                {

                });
            });

            List<UrunSatiri> urunListesi = new List<UrunSatiri>();
            do
            {
                if(urunListesi.Count == 0)
                {
                    Console.WriteLine("Hoşgeldiniz, siparişlerinizi alalım");
                }

                Console.WriteLine("Sıradaki ürün :");
                var urun = new UrunSatiri();
                Console.WriteLine("Lütfen almak istediğini ürün kodunu giriniz");
                urun.UrunKodu = Console.ReadLine();

                do
                {
                    var hatali = true;
                    Console.WriteLine("Lütfen ürün adedini giriniz");
                    hatali = !int.TryParse(Console.ReadLine(), out var adet);
                    if (hatali) continue;
                    
                    urun.Adet = adet;
                    break;

                } while (true);

                urunListesi.Add(urun);

                Console.WriteLine("Yeni ürün için Y / Siparişi Tamamlamak İçin S / İptal için P");

                var key = Console.ReadKey(true).Key;
                if(key == ConsoleKey.Y)
                {
                    
                }
                else if (key == ConsoleKey.S)
                {
                    await bus.Publish<SatisYapildi>(new
                    {
                        Urunler = urunListesi,
                        Tarih = DateTime.Now
                    });
                    urunListesi = new List<UrunSatiri>();
                }
                else if (key == ConsoleKey.P)
                {
                    urunListesi = new List<UrunSatiri>();
                    Console.WriteLine("Sipariş iptal edildi");
                    continue;
                }


            } while (true);


        }
    }

    
}
