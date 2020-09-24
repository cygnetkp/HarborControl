using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HarbolControlDemo.DataModels.Enum
{
    public enum BoatType
    {
        [Description("Cargo Boat")]
        CargoShip,
        [Description("Sail Boat")]
        SailBoat,
        [Description("Speed Boat")]
        SpeedBoat
    }
}
