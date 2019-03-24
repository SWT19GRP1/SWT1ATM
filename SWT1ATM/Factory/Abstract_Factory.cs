using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT1ATM.Output;
using TransponderReceiver;

namespace SWT1ATM.Factory
{
    public interface Abstract_Factory
    {
        Atm CreateAtm(ITrackFilter trackFilter);
        IVehicle CreateInstanceAirCraft(int x, int y, int z, DateTime time, string tag);
        ITrackFilter CreateInstanceTrackFilter(ITransponderReceiver tranRec, Abstract_Factory airplaneFactory, int x_off = 5000, int y_off = 5000, int z_off = 500, int x_len = 75000, int y_len = 75000, int z_len = 19500);
        IVehicleFormatter CreateInstanceAirplaneFormatter();
        IOutput CreateInstanceTerminalOutput(IVehicleFormatter formatter, Atm atm);
        IOutput CreateInstanceLogOutput(IVehicleFormatter formatter, ITrackFilter track);
        IAtmSeparationCondition CreateInstanceAtmSeparationCondition(int plThreshold, int heightThreshold);
    }
}
