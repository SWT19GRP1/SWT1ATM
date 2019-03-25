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
        private string AirplanesInAirSpace;
        private string SeperationConditions= "Seperation Conditions: ";
        public void LogVehicleData(object sender, FormattedTransponderDataEventArgs args)
        {
            var vehicles = args.vehicles;
            Console.Clear();
            AirplanesInAirSpace = "Current Airplanes in Airspace: \n";
            foreach (var plane in vehicles)
            {
                AirplanesInAirSpace+=Formatter.VehicleToString(plane);
            }
            Console.Write(AirplanesInAirSpace+SeperationConditions);
            SeperationConditions = ""; //Clear string for sep. cond. every tick
        }

        public void LogSeperationCondition(object sender, FormattedTransponderDataEventArgs args)
        {
            var vehicles = args.vehicles;
            SeperationConditions += "\nSeperation Condition between: " + vehicles[0].Tag + " and " + vehicles[1].Tag;
        }
    }
}
