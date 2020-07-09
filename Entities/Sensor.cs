//------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) Microsoft Corporation. All Rights Reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace SensorEventGenerator
{
    public class Sensor
    {
        static Random random = new Random();
        static string[] sensorNames = new[] { "sensorA", "sensorB", "sensorC", "sensorD", "sensorE" };
        static string fillertext = new string('a', 900);

        public int Pkey { get; set; }
        public string Time { get; set; }
        public string Dspl { get; set; }
        public string Dspl2 { get; set; }
        public int Temp { get; set; }
        public int Hmdt { get; set; }

        public static List<Sensor> Generate(int count = 100)
        {
            return Enumerable.Range(1, count).Select(i => 
                    new Sensor {
                        Pkey = random.Next(1, 1024),
                        Time = DateTime.UtcNow.ToString(),
                        Dspl = sensorNames[random.Next(sensorNames.Length)],
                        Dspl2 = fillertext,
                        Temp = random.Next(70, 150),
                        Hmdt = random.Next(30, 70), }).ToList();
        }
    }
}
