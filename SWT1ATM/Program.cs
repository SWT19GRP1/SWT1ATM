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

            var atmSeperationCondition = fact.CreateInstanceAtmSeparationCondition(atm, 5000, 500);

            var termOutput = fact.CreateInstanceTerminalOutput(vehicleFormatter, atm, atmSeperationCondition);
            var logOutput = fact.CreateInstanceLogOutput(vehicleFormatter, atmSeperationCondition);
            while (true) {}
        }
    }
}
