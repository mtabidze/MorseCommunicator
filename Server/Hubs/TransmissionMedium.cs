using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

using MorseCommunicator.Shared.Models;


namespace MorseCommunicator.Server.Hubs
{

    public class TransmissionMedium: Hub<IStation>, ITransmissionMedium
    {
        private static Dictionary<string, string> ConnectedStationsCallSigns { get; set; } = new();
        private readonly object syncObjectConnectedStationsCallSigns = new();

        public async Task PropagateSignals(string signals)
        {
            string connectionId = Context.ConnectionId;
            string callingStationCallSign = ConnectedStationsCallSigns.GetValueOrDefault(connectionId);

            Transmission transmission = new()
            {
                CallingStationCallSign = callingStationCallSign,
                Telegram = signals,
                CreationDate = DateTime.UtcNow
            };
            await Clients.All.RecieveTransmission(transmission);
        }

        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            string connectedStationCallSign = new Random().Next(1000, 10000).ToString();

            await Clients.All.StationConnected(connectedStationCallSign);
            lock (syncObjectConnectedStationsCallSigns)
            {
                ConnectedStationsCallSigns.Add(connectionId, connectedStationCallSign);
            }
            await Clients.Caller.SetCallSign(connectedStationCallSign);
            await Clients.Caller.SetStationsCallSigns(ConnectedStationsCallSigns.Values.ToList());
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            string disconnectedStationCallSign = ConnectedStationsCallSigns.GetValueOrDefault(connectionId);

            if(disconnectedStationCallSign != null)
            {
                lock (syncObjectConnectedStationsCallSigns)
                {
                    ConnectedStationsCallSigns.Remove(disconnectedStationCallSign);
                }
                await Clients.All.StationDisconnected(disconnectedStationCallSign);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
