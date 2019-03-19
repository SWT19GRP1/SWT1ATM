using System;
using System.Collections.Generic;
using System.Linq;
using SWT1ATM;

namespace SWT1ATM
{
    public class Atm
    {
        public List<IVehicle> AirCrafts { get; set; }
        public IOutput DataOutputType;


        public Atm(ITrackFilter track, IOutput Output)
        {
            AirCrafts = new List<IVehicle>();
            track.AirTrackToMonitorEvent += OnTrackDataRecieved;
            DataOutputType = Output;

        }

        public void OnTrackDataRecieved(object sender, FormattedTransponderDataEventArgs e)
        {
            OutputData(e.vehicles);
        }

        /*public void OnRemoveAirPlainRecievedEvent(object sender, FormattedTransponderDataEventArgs e)
        {
            foreach (var airCraft in AirCrafts)
            {
                if (airCraft.Tag == e.TrackfilterDto.Tag) { 
                    AirCrafts.Remove(airCraft);
                    return;
                }
            }
        } 

        public void AddOrUpdateAirplainRecievedEvent(FormattedTransponderDataEventArgs e)
        {
            var newAircraft = new Aircraft(e.TrackfilterDto.X, e.TrackfilterDto.Y, e.TrackfilterDto.Z,
                e.TrackfilterDto.Time, e.TrackfilterDto.Tag);

            bool found = false;

            foreach (var airCraft in AirCrafts)
            {
                if (airCraft.Tag == e.TrackfilterDto.Tag)
                {
                    airCraft.Update(newAircraft);
                    found = true;
                }
            }

            if (found == false)
                AirCrafts.Add(newAircraft);
        } */

        public void OutputData(List<IVehicle> vehicles)
        {
            DataOutputType.LogVehicleData(this, new SeparationConditionEventArgs(vehicles));
        }

    }
}