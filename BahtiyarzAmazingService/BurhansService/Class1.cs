using System;

namespace BurhansService
{
   
    public interface SiparisPaketle
    {
        string SiparisNo { get; set; }
    }

    public interface SiparisPaketlendi
    {
        string SiparisPaketNo { get; set; }
        string SiparisNo { get; set; }
        DateTime Tarih { get; set; }
    }
}
