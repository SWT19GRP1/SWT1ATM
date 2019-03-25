using System;
using System.Collections.Generic;
using System.IO;
using SWT1ATM;

namespace SWT1ATM.Output
{
    public class LogOutput:IOutput
    {
        public LogOutput(IVehicleFormatter formatter, IATM atm)
        {
            
            Formatter = formatter;
            atm.ATMMonitorEvent += LogVehicleData;
        }
        public void LogVehicleData(object sender, FormattedTransponderDataEventArgs args)
        {
            var vehicles = args.vehicles;

            string path = @"C:\Temp\SeparationCondition.txt";
            var myFile = new System.IO.StreamWriter(path, append:true);
            foreach (var plane in vehicles)
            {
                myFile.Write(Formatter.VehicleToString(plane));
            }
            myFile.Close();
        }

        public IVehicleFormatter Formatter { get; set; }
    }
}
