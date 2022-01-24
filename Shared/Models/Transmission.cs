using System;


namespace MorseCommunicator.Shared.Models
{
    public class Transmission
    {
        public string CallingStationCallSign { get; set; }
        public string Telegram { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
