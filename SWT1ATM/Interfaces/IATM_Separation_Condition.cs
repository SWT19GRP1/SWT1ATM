using System;
using System.Collections.Generic;

namespace SWT1ATM
{
    public interface IAtmSeparationCondition
    {
        event EventHandler<FormattedTransponderDataEventArgs> SeparationConditionEvent;
        void UpdateSeparationDetection(object sender, FormattedTransponderDataEventArgs e);
        bool SeparationDetection(IVehicle vehicleA, IVehicle vehicleB);
    }
}
