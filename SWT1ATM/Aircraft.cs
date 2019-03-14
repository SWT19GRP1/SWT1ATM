using System;
using SWT1ATM.Interfaces;

namespace SWT1ATM
{
    public class Aircraft : IVehicle
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Tag { get; private set; }
        public double Speed { get; private set; }
        public int Direction { get; private set; }

        public Aircraft(int x, int y, int z, DateTime timestamp, string tag)
        {
            X = x;
            Y = y; 
            Z = z;
            Timestamp = timestamp;
            Tag = tag;
            Speed = 0;
            Direction = 0;

        }

        public void Update(IVehicle referenceVehicle)
        {


            if (Tag != referenceVehicle.Tag) return;

                
        
                var deltaX = X - referenceVehicle.X;
                var deltaY = Y - referenceVehicle.Y;
                var deltaZ = Z - referenceVehicle.Z;
        
                var distance = (float) Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
                var timeDiff = referenceVehicle.Timestamp - Timestamp;
                var timeDiffInSec = timeDiff.TotalSeconds;
                var roundedSec =  Math.Round(timeDiffInSec);

                Speed = distance / roundedSec;
       

                var bearing = Math.Atan(deltaY/deltaX)*180/Math.PI + 90.00;
                Direction = (int) Math.Round(bearing);

                Timestamp = referenceVehicle.Timestamp;

                X = referenceVehicle.X;
                Y = referenceVehicle.Y;
                Z = referenceVehicle.Z;
        }
    }
}
