using System;
using System.Collections.Generic;
using SWT1ATM;

namespace SWT1ATM.Output
{
    public class LogOutput:IOutput
    {
        public LogOutput(IVehicleFormatter formatter, ITrackFilter track)
        {
            track.AirTrackToMonitorEvent += LogVehicleData;
            Formatter = formatter;
        }
        public void LogVehicleData(object sender, FormattedTransponderDataEventArgs args)
        {
            var vehicles = args.vehicles;

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            foreach (var plane in vehicles)
            {
                System.IO.File.AppendAllText(path,Formatter.VehicleToString(plane));
            }
        }

        public IVehicleFormatter Formatter { get; set; }
    }
}
