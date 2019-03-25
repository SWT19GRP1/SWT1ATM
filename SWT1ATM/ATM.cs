using System;
using System.Collections.Generic;
using System.Linq;
using SWT1ATM;
using SWT1ATM.Interfaces;

namespace SWT1ATM
{
    public class Atm:IATM
    {
        public event EventHandler<FormattedTransponderDataEventArgs> ATMMonitorEvent;

        public List<IVehicle> AirCrafts { get; set; }

        public Atm(ITrackFilter track)
        {
            AirCrafts = new List<IVehicle>();
            track.AirTrackToMonitorEvent += OnTrackDataRecieved;
            track.AirTrackOutSideMonitorEvent += OnRemoveAirPlainRecievedEvent;
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

        public void OnRemoveAirPlainRecievedEvent(object sender, FormattedTransponderDataEventArgs e)
        {
            foreach (var vehicleAfter in e.vehicles)
            {
                foreach (var vehicleBefore in AirCrafts)
                {
                    if (vehicleBefore.Tag == vehicleAfter.Tag)
                    {
                        AirCrafts.Remove(vehicleBefore);
                        break;
                    }
                }
            }
        } 
    }
}