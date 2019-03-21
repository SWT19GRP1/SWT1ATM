using SWT1ATM;

namespace SWT1ATM.Output
{
  public class AirplaneFormatter: IVehicleFormatter
    {
        public string VehicleToString(IVehicle plane)
        {
            string general = "Tag: " + plane.Tag + "Coordinates: " + plane.X + "," + plane.Y + "," + plane.Z + "Direction:" + plane.Direction;
            string date = " Date: " + plane.Timestamp;
            string airplaneString = general + date +"\n\r";
            return airplaneString;
        }
    }
}
