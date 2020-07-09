//------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) Microsoft Corporation. All Rights Reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SensorEventGenerator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new EventHubConfig();
            var minutesToSend = GetConfigDouble("MinutesToRun");

            // Configuration can be picked up from Env variable of the step or fall back on App.config
            config.ConnectionString = GetConfigString("EventHubConnectionString");
            config.EventHubName = GetConfigString("EventHubName");
            config.EventDelayMs = GetConfigInt("EventDelayMs");
            config.EventsPerMessage = GetConfigInt("EventsPerMessage");
            config.EventType = GetConfigString("EventType");
            Console.WriteLine("Starting With " + JsonConvert.SerializeObject(config));

            if (config.EventType?.Equals("Sensor") ?? true)
            {
                var eventHubevents = Observable.Interval(TimeSpan.FromMilliseconds(config.EventDelayMs)).
                                        Select(i => Sensor.Generate(config.EventsPerMessage));

                // To send data to EventHub as JSON
                using (var eventHubDis = eventHubevents.Subscribe(new EventHubObserver(config)))
                {
                    await Task.Delay(TimeSpan.FromMinutes(minutesToSend));
                }
            }
        }

        public static string GetConfigString(string name) => Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process) ?? ConfigurationManager.AppSettings[name];

        public static double GetConfigDouble(string name) => double.Parse(GetConfigString(name));

        public static int GetConfigInt(string name) => int.Parse(GetConfigString(name));
    }
}
