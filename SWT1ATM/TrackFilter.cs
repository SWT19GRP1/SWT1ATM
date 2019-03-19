using System;
using System.Collections.Generic;
using SWT1ATM;
using SWT1ATM.Factory;
using TransponderReceiver;

namespace SWT1ATM
{
    public class TrackFilter : ITrackFilter
    {
        public event EventHandler<FormattedTransponderDataEventArgs> AirTrackToMonitorEvent;

        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int ZOffset { get; set; }
        public int XLength { get; set; }
        public int YWidth { get; set; }
        public int ZHeight { get; set; }
        public TrackFilter(ITransponderReceiver reciever, int xOffset = 0, int yOffset = 0,
            int zOffset = 500, int xLength = 80000, int yWidth = 80000, int zHeight = 19500)
        {
            reciever.TransponderDataReady += HandlerOnRaiseTrackInsideMonitoringAreaEvent;

            XOffset = xOffset;
            YOffset = yOffset;
            ZOffset = zOffset;
            XLength = xLength;
            YWidth = yWidth;
            ZHeight = zHeight;
        }

        public void HandlerOnRaiseTrackInsideMonitoringAreaEvent(object sender, RawTransponderDataEventArgs e)
        {
            char[] separators = { ';' };
            List<IVehicle> vehicles = new List<IVehicle>();

            foreach (var data in e.TransponderData)
            {

                var tokens = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string tag = tokens[0];
                var xCoordinate = int.Parse(tokens[1]);
                var yCoordinate = int.Parse(tokens[2]);
                var zCoordinate = int.Parse(tokens[3]);
                var dateTime = GetDate(tokens[4]);

                var aircraft = new Aircraft(xCoordinate, yCoordinate, zCoordinate, dateTime, tag);

                if (xCoordinate <= XOffset + XLength && xCoordinate >= XOffset)
                {
                    if (yCoordinate <= YOffset + YWidth && yCoordinate >= YOffset)
                    {
                        if (zCoordinate <= zCoordinate + ZHeight && zCoordinate >= ZOffset)
                        {
                            vehicles.Add(aircraft);
                        }
                    }
                }
            }
            OnAirTrackToMonitorEvent(vehicles);
        }

        public virtual void OnAirTrackToMonitorEvent(List<IVehicle> vehicles)
        {
            AirTrackToMonitorEvent?.Invoke(this, new FormattedTransponderDataEventArgs(vehicles));
        }

        /*public void OnAirTrackOutSideMonitorEvent(TrackfilterDto dto)
        {
            AirTrackOutSideMonitorEvent?.Invoke(this, new FormattedTransponderDataEventArgs(dto));
        }
        */

        public DateTime GetDate(string date)
        {
            var year = int.Parse(date.Substring(0, 4));
            var month = int.Parse(date.Substring(4, 2));
            var day = int.Parse(date.Substring(6, 2));
            var hour = int.Parse(date.Substring(8, 2));
            var minute = int.Parse(date.Substring(10, 2));
            var second = int.Parse(date.Substring(12, 2));
            var milli = int.Parse(date.Substring(14, 3));
            var returnDateTime = new DateTime(year, month, day, hour, minute, second, milli);

            return returnDateTime;
        }
    }
}
