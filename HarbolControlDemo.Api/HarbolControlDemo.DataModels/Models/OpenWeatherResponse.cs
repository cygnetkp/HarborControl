using System;
using System.Collections.Generic;
using System.Text;

namespace HarbolControlDemo.DataModels.Models
{
    public class OpenWeatherResponse
    {
        public Wind wind { get; set; }
    }
    public class Wind
    {
        public decimal speed { get; set; }
    }
}
