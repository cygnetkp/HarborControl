using HarbolControlDemo.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HarbolControlDemo.Utility
{
    public static class Common
    {
        private static Random random = null;
        
      /// <summary>
      /// Description: Use for pick random boats
      /// Created by: Kishan Prajapati
      /// Created On : 23/09/2020
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="values"></param>
      /// <param name="numValues"></param>
      /// <returns>Random boat information list based on paramater pass</returns>
        public static List<T> PickRandom<T>(List<T> values, int numValues)
        {
            if (random == null)
                random = new Random();
            if (numValues >= values.Count)
                numValues = values.Count - 1;
            int[] indexes = Enumerable.Range(0, values.Count).ToArray();
            List<T> results = new List<T>();
            for (int i = 0; i < numValues; i++)
            {
                int j = random.Next(i, values.Count);
                int temp = indexes[i];
                indexes[i] = indexes[j];
                indexes[j] = temp;
                results.Add(values[indexes[i]]);
            }
            return results;
        }
        /// <summary>
        /// Description: Calculate boat speeed in minutes based on perimeter defined.
        /// Created by : Kishan Prajapati
        /// Created On : 23/09/2020
        /// </summary>
        /// <param name="boatSpeed"></param>
        /// <returns>Boat time duration in minute </returns>
        public static int CalculateSpeed(int boatSpeed)
        {
 
            int _totalMinutes = (AppConstant.PERIMETER_DISTANCE * AppConstant.MINUTES) / boatSpeed;
            return _totalMinutes;
        }
        /// <summary>
        /// Description : Use for to build Open Weather API URL
        /// Created by :Kishan Prajapati
        /// Created On: 23/09/2020
        /// </summary>
        /// <param name="location"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static string BuildOpenWeatherUrl(string location, string apiKey)
        {
            return "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + apiKey;
        }
    }
}
