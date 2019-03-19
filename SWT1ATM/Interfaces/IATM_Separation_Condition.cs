using System;
using System.Collections.Generic;

namespace SWT1ATM
{
    public interface IAtmSeparationCondition
    {
        void subscribeToATM(Atm atm);
        event EventHandler<SeparationConditionEventArgs> SeparationConditionEvent;
        void UpdateSeparationDetection(List<IVehicle> vehicles);
        bool SeparationDetection(IVehicle vehicleA, IVehicle vehicleB);
    }

    public class SeparationConditionEventArgs : EventArgs
    {
        public List<IVehicle> vehicles { get; private set; }
        public SeparationConditionEventArgs(List<IVehicle> Vehicles)
        {
            vehicles = Vehicles;
        }
    }
}
