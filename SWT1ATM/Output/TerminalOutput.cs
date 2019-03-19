using System;
using System.Collections.Generic;
using System.Threading;
using SWT1ATM;

namespace SWT1ATM.Output
{
    public class TerminalOutput:IOutput
    {
        public TerminalOutput(IVehicleFormatter formatter)
        {
            Formatter = formatter;
        }
        public IVehicleFormatter Formatter { get; set; }

        public void LogVehicleData(object sender, SeparationConditionEventArgs args)
        {
            var vehicles = args.vehicles;

            Thread.Sleep(5);
            Console.Clear();
            Console.WriteLine("Current Airplanes in Airspace:");
            foreach (var plane in vehicles)
            {
                Console.Write(Formatter.VehicleToString(plane));
            }
            
        }
    }
}
