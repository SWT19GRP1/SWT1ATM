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

            var termOutput = fact.CreateInstanceTerminalOutput(vehicleFormatter, trackfilter);

            var ATM = fact.CreateAtm(trackfilter, termOutput);

            var atmSeperationCondition = fact.CreateInstanceAtmSeparationCondition(5000, 500);

            while (true) {}
        }
    }
}
