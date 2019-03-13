using System.Collections.Generic;

namespace SWT1ATM
{
    public interface IOutput
    {
        IVehicleFormatter Formatter { get; set; }
        void LogVehicleData(object sender, SeparationConditionEventArgs args);
    }
}
