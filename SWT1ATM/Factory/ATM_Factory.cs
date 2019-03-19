using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT1ATM.Output;
using TransponderReceiver;

namespace SWT1ATM.Factory
{
    class ATM_Factory : Abstract_Factory
    {
        public Atm CreateAtm(ITrackFilter trackFilter)
        {
            return new Atm(trackFilter);
        }

        public IVehicle CreateInstanceAirCraft(int x, int y, int z, DateTime time, string tag)
        {
            return new Aircraft(x, y, z, time, tag);
        }

        public ITrackFilter CreateInstanceTrackFilter(ITransponderReceiver tranRec, int x_off = 5000, int y_off = 5000, int z_off = 500, int x_len = 75000, int y_len = 75000, int z_len = 19500)
        {
            return new TrackFilter(tranRec, x_off, y_off, z_off, x_len, y_len, z_len);
        }

        public IVehicleFormatter CreateInstanceAirplaneFormatter()
        {
            return new AirplaneFormatter();
        }

        public IOutput CreateInstanceTerminalOutput(IVehicleFormatter formatter)
        {
            return new TerminalOutput(formatter);
        }

        public IOutput CreateInstanceLogOutput(IVehicleFormatter formatter)
        {
            return new LogOutput(formatter);
        }

        public IAtmSeparationCondition CreateInstanceAtmSeparationCondition(int plThreshold, int heightThreshold)
        {
            return new ATMRTSeparationCondition(plThreshold, heightThreshold);
        }
    }
}
