using System;
using System.Collections.Generic;

namespace Satis.Abstractions
{
    public class SatisYapildi
    {
        public List<UrunSatiri> Urunler { get; set; }
        public DateTime Tarih { get; set; }
    }

    public class UrunSatiri
    {
        public string UrunKodu { get; set; }
        public int Adet { get; set; }
    }
}
