using System;

namespace SWT1ATM.Interfaces
{
    public interface IAtmSeparationCondition
    {
        event EventHandler<FormattedTransponderDataEventArgs> SeparationConditionEvent;
        void UpdateSeparationDetection(object sender, FormattedTransponderDataEventArgs e);
        bool SeparationDetection(IVehicle vehicleA, IVehicle vehicleB);
    }
}
