//------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) Microsoft Corporation. All Rights Reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace SensorEventGenerator
{
    class EventHubConfig
    {
        public string ConnectionString;
        public string EventHubName;
        public string EventType;
        public int EventDelayMs { get; internal set; }
        public int EventsPerMessage { get; internal set; }
    }
}
