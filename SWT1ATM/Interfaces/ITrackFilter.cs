using System;

namespace SWT1ATM.Interfaces
{
    public interface ITrackFilter
    {
        event EventHandler<FormattedTransponderDataEventArgs> FormattedDataEvent;
    }

    public class FormattedTransponderDataEventArgs : EventArgs
    {
        public FormattedTransponderDataEventArgs(TrackfilterDTO data)
        {
            TrackfilterDto = data;
        }

        public TrackfilterDTO TrackfilterDto { get;}
    }
}