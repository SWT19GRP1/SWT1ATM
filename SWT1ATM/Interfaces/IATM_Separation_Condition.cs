using System;
using System.Collections.Generic;

namespace SWT1ATM.Interfaces
{
    interface IAtmSeparationCondition
    {
        event EventHandler<SeparationConditionEventArgs> SeparationConditionEvent;
        void UpdateSeparationDetection(List<IVehicle> vehicles);
        bool SeparationDetection(IVehicle vehicleA, IVehicle vehicleB);
    }

    public class SeparationConditionEventArgs : EventArgs
    {
        public IVehicle[] Vehicles { get; private set; }
        public SeparationConditionEventArgs(IVehicle[] vehicles)
        {
            this.Vehicles = vehicles;
        }
    }
}
