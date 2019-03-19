using SWT1ATM.Factory;
using SWT1ATM.Output;
using TransponderReceiver;

namespace SWT1ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            ATM_Factory fact = new ATM_Factory();

            var reciever = TransponderReceiverFactory.CreateTransponderDataReceiver();

            var trackfilter = fact.CreateInstanceTrackFilter(reciever);

            var vehicleFormatter = fact.CreateInstanceAirplaneFormatter();

            var atm = fact.CreateAtm(trackfilter);

            var termOutput = fact.CreateInstanceTerminalOutput(vehicleFormatter, atm);

            var atmSeperationCondition = fact.CreateInstanceAtmSeparationCondition(5000, 500);

            while (true) {}
        }
    }
}
