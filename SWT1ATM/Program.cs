using SWT1ATM.Output;
using TransponderReceiver;

namespace SWT1ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            var reciever = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var trackfilter = new TrackFilter(reciever);
            var ATM = new  Atm(trackfilter, new TerminalOutput(new AirplaneFormatter()));
            while (true)
            {

            }
        }
    }
}
