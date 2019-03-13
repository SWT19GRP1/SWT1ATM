using System;

namespace SWT1ATM.Interfaces
{
    public interface ITrackFilter
    {
        event EventHandler<FormattedTransponderDataEventArgs> FormattedDataEvent;
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