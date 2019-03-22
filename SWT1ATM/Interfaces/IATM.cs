using System;
using System.Collections.Generic;

namespace SWT1ATM
{
    public interface IATM
    {
        event EventHandler<FormattedTransponderDataEventArgs> ATMMonitorEvent;
        List<IVehicle> AirCrafts { get; set; }
        void OnTrackDataRecieved(object sender, FormattedTransponderDataEventArgs e);
        void OnRemoveAirPlainRecievedEvent(object sender, FormattedTransponderDataEventArgs e);
    }
}