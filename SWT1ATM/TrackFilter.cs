using System;
using System.Collections.Generic;
using SWT1ATM;
using SWT1ATM.Interfaces;
using TransponderReceiver;

namespace SWT1ATM
{
    public class TrackFilter : ITrackFilter
    {
        public event EventHandler<FormattedTransponderDataEventArgs> AirTrackToMonitorEvent;
        public event EventHandler<FormattedTransponderDataEventArgs> AirTrackOutSideMonitorEvent;

        public List<IVehicle> vehiclesIn = new List<IVehicle>();

        public List<IVehicle> vehiclesOut = new List<IVehicle>();

        public IFactory AirplaneFactory;
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int ZOffset { get; set; }
        public int XLength { get; set; }
        public int YWidth { get; set; }
        public int ZHeight { get; set; }
        public TrackFilter(ITransponderReceiver reciever, IFactory airplaneFactory, int xOffset = 10000, int yOffset = 10000,
            int zOffset = 500, int xLength = 70000, int yWidth = 70000, int zHeight = 19500)
        {
            reciever.TransponderDataReady += HandlerOnRaiseTrackInsideMonitoringAreaEvent;

            AirplaneFactory = airplaneFactory;
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

            //Clearing lists each time. Lists have to be public to test them. 
            vehiclesIn.Clear();
            vehiclesOut.Clear();

            foreach (var data in e.TransponderData)
            {

                var tokens = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string tag = tokens[0];
                var xCoordinate = int.Parse(tokens[1]);
                var yCoordinate = int.Parse(tokens[2]);
                var zCoordinate = int.Parse(tokens[3]);
                var dateTime = GetDate(tokens[4]);

                var aircraft = AirplaneFactory.CreateInstanceAirCraft(xCoordinate, yCoordinate, zCoordinate, dateTime, tag);

                bool inbounds = false;
                if (xCoordinate <= XOffset + XLength && xCoordinate >= XOffset)
                {
                    if (yCoordinate <= YOffset + YWidth && yCoordinate >= YOffset)
                    {
                        if (zCoordinate <= ZOffset + ZHeight && zCoordinate >= ZOffset)
                        {
                            inbounds = true;
                            vehiclesIn.Add(aircraft);
                        }
                    }
                }
                if(!inbounds)
                    vehiclesOut.Add(aircraft);
            }
            OnAirTrackToMonitorEvent(vehiclesIn);
            OnAirTrackOutSideMonitorEvent(vehiclesOut);
        }

        public virtual void OnAirTrackToMonitorEvent(List<IVehicle> vehicles)
        {
            AirTrackToMonitorEvent?.Invoke(this, new FormattedTransponderDataEventArgs(vehicles));
        }

        public void OnAirTrackOutSideMonitorEvent(List<IVehicle> vehicles)
        {
            AirTrackOutSideMonitorEvent?.Invoke(this, new FormattedTransponderDataEventArgs(vehicles));
        }

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
