using TransponderReceiver;

namespace SWT1ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            var reciever = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var trackfilter = new TrackFilter(reciever);
            while (true)
            {

            }
        }
    }
}
