using SWT1ATM.Interfaces;

namespace SWT1ATM.Output
{
  public class AirplaneFormatter: IVehicleFormatter
    {
        public string VehicleToString(IVehicle plane)
        {
            string general = "Tag: " + plane.Tag + " Coordinates: X: " + plane.X + ", Y: " + plane.Y + ", Z: " + plane.Z + "," + " Direction: " + plane.Direction + " degrees.";
            string date = " Date: " + plane.Timestamp;
            string airplaneString = general + date +"\n\r";
            return airplaneString;
        }
    }
}
