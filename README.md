# SensorEventGenerator #

This solution simulates SensorTag events and pushes them to an Azure Event Hub instance. 

Events are approximately 1 KB in size.

For more information about scenarios and usage, see [here](http://gallery.azureml.net/Tutorial/6f95aeaca0fc43b3aec37ad3a0526a21)
- Provide these details in `App.config` file. 

```xml
<add key="EventHubConnectionString" value="***YOUR EVENT HUB CONNECTION STRING***" />
<add key="EventHubName" value="***YOUR EVENT HUB NAME***" />
```

- Build the solution using VS2019 and then trigger `InvokeSenders.ps1` that ensures the senders keep running. You can increase the number of Senders if you wish , e.g. D4V3 VM can easily run 20 senders in parallel.

- You may use this event simulator for custom event generation for your testing purposes.
`Sensor.cs`  defines the format of the event.

- Use a Azure Windows VM in the same region as your stream processing job to run the senders.
