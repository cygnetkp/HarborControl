using HarbolControlDemo.DataModels.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarbolControlDemo.DataModels.Models
{
    public class BoatInformation
    {
        public int Id { get; set; }
        public BoatType BoatType { get; set; }
        public string BoatSpeed { get; set; }
        public BoatStatusType BoatStatus { get; set; }
        public int BoatReachTimeDuration { get; set; }
        public int BoatActualTimeDuration { get; set; }

    }
}
