using SWT1ATM.Output;
using TransponderReceiver;

namespace SWT1ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            AtmFactory fact = new AtmFactory();

            var reciever = TransponderReceiverFactory.CreateTransponderDataReceiver();

            var trackfilter = fact.CreateInstanceTrackFilter(reciever, fact);

            var vehicleFormatter = fact.CreateInstanceAirplaneFormatter();

            var atm = fact.CreateAtm(trackfilter);

            var termOutput = fact.CreateInstanceTerminalOutput(vehicleFormatter, atm);

            var atmSeperationCondition = fact.CreateInstanceAtmSeparationCondition(atm, 5000, 500);

            while (true) {}
        }
    }
}
