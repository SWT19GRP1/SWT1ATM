using SWT1ATM.Interfaces;

namespace SWT1ATM.Output
{
  public class AirplaneFormatter: IVehicleFormatter
    {
        public string VehicleToString(IVehicle plane)
        {
            string general = "Tag: " + plane.Tag + 
                             " Coordinates X: " + plane.X.ToString("D5") + 
                             ", Y: " + plane.Y.ToString("D5") + 
                             ", Z: " + plane.Z.ToString("D5") + 
                             ", Direction: " + plane.Direction.ToString("D3") + " degrees" +
                             ", Speed: "+plane.Speed.ToString("000.00") + " m/s";
            string date = ", Date: " + plane.Timestamp;
            string airplaneString = general + date+"\n";
            return airplaneString;
        }
    }
}
