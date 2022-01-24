using System.Collections.Generic;
using System.Threading.Tasks;


namespace MorseCommunicator.Shared.Models
{
    public interface IStation
    {
        public Task RecieveTransmission(Transmission transmission);
        public Task SetCallSign(string callSign);
        public Task SetStationsCallSigns(List<string> stationsCallSigns);
        public Task StationConnected(string stationCallSign);
        public Task StationDisconnected(string stationCallSign);
    }
}
