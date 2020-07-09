
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace SensorEventGenerator
{
    class EventHubObserver : IObserver<List<Sensor>>
    {
        private EventHubConfig _config;
        private EventHubClient _eventHubClient;
        private Logger _logger;
        private bool initialized = false;

        public EventHubObserver(EventHubConfig config)
        {
            _config = config;
            if (!string.IsNullOrWhiteSpace(_config.ConnectionString))
            {
                _eventHubClient = EventHubClient.CreateFromConnectionString(_config.ConnectionString, _config.EventHubName);
            }
            else
            {
                throw new ArgumentException("EventHub ConnectionString is null or empty");
            }

            this._logger = new Logger(ConfigurationManager.AppSettings["logger_path"]);
        }

        public void OnNext(List<Sensor> sensorData)
        {
            try
            {
                var serializedString = JsonConvert.SerializeObject(sensorData);
                EventData data = new EventData(Encoding.UTF8.GetBytes(serializedString)) { PartitionKey = sensorData[0].Pkey.ToString() };
                _eventHubClient.Send(data);

                if (!initialized)
                {
                    _logger.Write("Started sending" + serializedString + " at: " + sensorData[0].Time);
                    initialized = true;
                }
            }
            catch (MessagingException mse)
            {
                _logger.Write(mse.Message);
                if (!mse.IsTransient)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.Write(ex);
                throw;
            }
        }

        public void OnCompleted() => throw new NotImplementedException();

        public void OnError(Exception error)
        {
            _logger.Write(error);
            throw error;
        }
    }
}
