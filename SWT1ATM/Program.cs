using SWT1ATM.Output;
using TransponderReceiver;

namespace SWT1ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            var reciever = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var trackfilter = new TrackFilter(reciever,0,0,0,15000000,15100000,151500000);
            var ATM = new  Atm(trackfilter, new TerminalOutput(new AirplaneFormatter()));
            while (true)
            {

            }
        }
    }
}
