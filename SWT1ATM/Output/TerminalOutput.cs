using System;
using System.Threading;
using SWT1ATM.Interfaces;

namespace SWT1ATM.Output
{
    public class TerminalOutput:IOutput
    {
        public TerminalOutput(IVehicleFormatter formatter, IATM atm, IAtmSeparationCondition sep)
        {
            atm.ATMMonitorEvent += LogVehicleData;
            Formatter = formatter;
            sep.SeparationConditionEvent += LogSeperationCondition;
        }
        public IVehicleFormatter Formatter { get; set; }

        public void LogVehicleData(object sender, FormattedTransponderDataEventArgs args)
        {
            var vehicles = args.vehicles;
            Console.Clear();
            Console.WriteLine("Current Airplanes in Airspace:");
            foreach (var plane in vehicles)
            {
                Console.WriteLine(Formatter.VehicleToString(plane));
            }            
        }

        public void LogSeperationCondition(object sender, FormattedTransponderDataEventArgs args)
        {
            var vehicles = args.vehicles;
            Console.WriteLine("Seperation Conditions: ");
            foreach (var plane in vehicles)
            {
                
                Console.WriteLine(plane.Tag);
            }
            Console.WriteLine("-----------------------------------------");
        }
    }
}
