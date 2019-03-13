﻿using System;
using System.Collections.Generic;
using SWT1ATM;

namespace SWT1ATM
{
    public class ATMRTSeparationCondition : IAtmSeparationCondition
    {
        private double _planeThreshold;
        private int _heightThreshold;

        public event EventHandler<SeparationConditionEventArgs> SeparationConditionEvent;

        public ATMRTSeparationCondition(int plThres = 0, int heightThres = 0)
        {
            _planeThreshold = plThres;
            _heightThreshold = heightThres;
        }

        public void UpdateSeparationDetection(List<IVehicle> vehicles)
        {
            for(int i = 0; i < vehicles.Count - 1 ; i++)
            {
                for (int j = i + 1; j < vehicles.Count; j++)
                {
                    if (SeparationDetection(vehicles[i], vehicles[j]))
                    {
                        EventHandler<SeparationConditionEventArgs> handler = SeparationConditionEvent;
                        List<IVehicle> confVehicles = new List<IVehicle>();

                        confVehicles.Add(vehicles[i]);
                        confVehicles.Add(vehicles[j]);

                        if (handler != null)
                            handler(this, new SeparationConditionEventArgs(confVehicles));
                    }
                }
            }
        }

        public bool SeparationDetection(IVehicle vehicleA, IVehicle vehicleB)
        {
            double deltaX = vehicleA.X - vehicleB.X;
            double deltaY = vehicleA.Y - vehicleB.Y;
            int deltaZ = Math.Abs(vehicleA.Z - vehicleB.Z);

            if ((Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)) <= Math.Pow(_planeThreshold, 2))
            {
                if (deltaZ <= _heightThreshold)
                    return true;
            }
            return false;
        }
    }
}
