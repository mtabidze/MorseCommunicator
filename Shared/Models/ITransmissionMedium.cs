using System.Threading.Tasks;


namespace MorseCommunicator.Shared.Models
{
    public interface ITransmissionMedium
    {
        public Task PropagateSignals(string signals);
    }
}
