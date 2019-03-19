using System;
using System.Collections.Generic;

namespace SWT1ATM
{
    public interface IAtmSeparationCondition
    {
        void subscribeToATM(Atm atm);
        event EventHandler<FormattedTransponderDataEventArgs> SeparationConditionEvent;
        void UpdateSeparationDetection(List<IVehicle> vehicles);
        bool SeparationDetection(IVehicle vehicleA, IVehicle vehicleB);
    }
}
