using System.Collections.Generic;
using SWT1ATM.Interfaces;

namespace SWT1ATM
{
    public interface IOutput
    {
        IVehicleFormatter Formatter { get; set; }
        void LogVehicleData(object sender, FormattedTransponderDataEventArgs args);
    }
}
