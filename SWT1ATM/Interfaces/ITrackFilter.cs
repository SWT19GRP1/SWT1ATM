using System;

namespace SWT1ATM.Interfaces
{
    public interface ITrackFilter
    {
        event EventHandler<FormattedTransponderDataEventArgs> AirTrackToMonitorEvent;
        event EventHandler<FormattedTransponderDataEventArgs> AirTrackOutSideMonitorEvent;
    }

    public class FormattedTransponderDataEventArgs : EventArgs
    {
        public FormattedTransponderDataEventArgs(TrackfilterDto data)
        {
            TrackfilterDto = data;
        }

        public TrackfilterDto TrackfilterDto { get;}
    }
}