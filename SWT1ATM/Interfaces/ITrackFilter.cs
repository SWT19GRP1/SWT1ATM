using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SWT1ATM
{
    public interface ITrackFilter
    {
        event EventHandler<FormattedTransponderDataEventArgs> AirTrackToMonitorEvent;
        event EventHandler<FormattedTransponderDataEventArgs> AirTrackOutSideMonitorEvent;
    }

    public class FormattedTransponderDataEventArgs : EventArgs
    {
        public FormattedTransponderDataEventArgs(List<IVehicle> data)
        {
            vehicles = data;
        }

        public List<IVehicle> vehicles { get; private set; }
    }
}