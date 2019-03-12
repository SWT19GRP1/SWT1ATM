using System.Collections.Generic;

namespace SWT1ATM.Interfaces
{
    interface IOutput
    {
        IVehicleFormatter Formatter { get; set; }
        void LogVehicleData(List<IVehicle> v);
    }
}
