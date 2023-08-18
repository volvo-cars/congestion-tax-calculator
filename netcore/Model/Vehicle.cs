using Microsoft.VisualBasic;
using System;

namespace congestion.Model
{
    public class Vehicle
    {
        public Vehicle()
        {
        }

        public string Type { get; private set; }
    }

    public class VehicleChekinLog
    {
        public VehicleChekinLog()
        {
        }

        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public string Plate { get; private set; }
        public DateTime LogDate { get; private set; }
    }
}