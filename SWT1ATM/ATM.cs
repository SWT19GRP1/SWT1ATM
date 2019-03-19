using System;
using System.Collections.Generic;
using System.Linq;
using SWT1ATM;

namespace SWT1ATM
{
    public class Atm
    {
        public event EventHandler<FormattedTransponderDataEventArgs> ATMMonitorEvent;

        public List<IVehicle> AirCrafts { get; set; }

        public Atm(ITrackFilter track)
        {
            AirCrafts = new List<IVehicle>();
            track.AirTrackToMonitorEvent += OnTrackDataRecieved;
        }

        public void OnTrackDataRecieved(object sender, FormattedTransponderDataEventArgs e)
        {
            foreach (var vehicleAfter in e.vehicles)
            {
                bool inList = false;
                foreach (var vehicleBefore in AirCrafts)
                {
                    if (vehicleBefore.Tag == vehicleAfter.Tag)
                        inList = true;

                    vehicleBefore.Update(vehicleAfter);
                }
                if(!inList)
                    AirCrafts.Add(vehicleAfter);
            }

            ATMMonitorEvent?.Invoke(this, new FormattedTransponderDataEventArgs(AirCrafts));
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
        }

        public void OutputData(List<IVehicle> vehicles)
        {
            DataOutputType.LogVehicleData(this, new FormattedTransponderDataEventArgs(vehicles));
        }
        */

    }
}