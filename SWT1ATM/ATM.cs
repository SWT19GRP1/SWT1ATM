using System;
using System.Collections.Generic;
using SWT1ATM;

namespace SWT1ATM
{
    public class Atm
    {
        public List<IVehicle> AirCrafts { get; }
        public IOutput DataOutputType;


        public Atm(ITrackFilter track, IOutput Output)
        {
            AirCrafts = new List<IVehicle>();
            track.AirTrackToMonitorEvent += OnTrackDataRecieved;
           // track.AirTrackOutSideMonitorEvent += OnRemoveAirPlainRecievedEvent;
            DataOutputType = Output;
            
        }

        public void OnTrackDataRecieved(object sender, FormattedTransponderDataEventArgs e)
        {
            AddOrUpdateAirplainRecievedEvent(e);
            //OutputData();
        }
        public void OnRemoveAirPlainRecievedEvent(object sender, FormattedTransponderDataEventArgs e)
        {
            foreach (var airCraft in AirCrafts)
            {
                if (airCraft.Tag == e.TrackfilterDto.Tag)
                    AirCrafts.Remove(airCraft);
            }
        }

        public void AddOrUpdateAirplainRecievedEvent(FormattedTransponderDataEventArgs e)
        {
            var newAircraft = new Aircraft(e.TrackfilterDto.X, e.TrackfilterDto.Y, e.TrackfilterDto.Z,
                e.TrackfilterDto.Time, e.TrackfilterDto.Tag);

            foreach (var airCraft in AirCrafts)
            {
                if (airCraft.Tag == e.TrackfilterDto.Tag)
                {
                    airCraft.Update(newAircraft);
                    return;
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