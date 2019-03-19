using System;
using System.Collections.Generic;
using SWT1ATM;

namespace SWT1ATM
{
    public class Atm
    {
        public List<IVehicle> AirCrafts { get; }

        public Atm(ITrackFilter track)
        {
            AirCrafts = new List<IVehicle>();
            track.AirTrackToMonitorEvent += RecievedEventOffInsideVehicle;
           // track.AirTrackOutSideMonitorEvent += OnRemoveAirPlainRecievedEvent;
        }

        public void RecievedEventOffInsideVehicle(object sender, FormattedTransponderDataEventArgs e)
        {
            var newAircraft = new Aircraft(e.TrackfilterDto.X, e.TrackfilterDto.Y, e.TrackfilterDto.Z,
                e.TrackfilterDto.Time, e.TrackfilterDto.Tag);

            foreach (var airCraft in AirCrafts)
            {
                if (airCraft.Tag == e.TrackfilterDto.Tag)
                {

                }
            }
            AirCrafts.Add(newAircraft);
        }

        /*
         Skal ske vha. events
        public void OutputData()
        {
            DataOutputType.LogVehicleData(AirCrafts);
        }
        */
    }
}