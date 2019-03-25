using System;
using TransponderReceiver;

namespace SWT1ATM.Interfaces
{
    public interface IFactory
    {
        Atm CreateAtm(ITrackFilter trackFilter);
        IVehicle CreateInstanceAirCraft(int x, int y, int z, DateTime time, string tag);
        ITrackFilter CreateInstanceTrackFilter(ITransponderReceiver tranRec, IFactory airplaneFactory, int x_off = 5000, int y_off = 5000, int z_off = 500, int x_len = 75000, int y_len = 75000, int z_len = 19500);
        IVehicleFormatter CreateInstanceAirplaneFormatter();
        IOutput CreateInstanceTerminalOutput(IVehicleFormatter formatter, Atm atm, IAtmSeparationCondition separation);
        IOutput CreateInstanceLogOutput(IVehicleFormatter formatter, IAtmSeparationCondition separation);
        IAtmSeparationCondition CreateInstanceAtmSeparationCondition(Atm atm, int plThreshold, int heightThreshold);
    }
}
