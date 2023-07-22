using System;
using NTU.IoT.Models;
using InfluxDB.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

namespace NTU.IoT.Utility.ChainOfResponsibility
{
	public class NoSqlMeasurementCreation: AbstractHandler
	{
        private readonly InfluxDBService _service;

        public NoSqlMeasurementCreation(InfluxDBService service) {
            _service = service;

        }

        public override object Handle(object obj)
        {
            System.Diagnostics.Debug.WriteLine("NoSqlMeasurementCreation: " + ((DeviceType)obj).name);

            _service.Write(write =>
            {
                var point = PointData.Measurement(((DeviceType)obj).table_name)
                    .Field("field1", 0.0)
                    .Field("field2", 0.0);
                write.WritePoint(point,"IOT", "NTU" );
            });



            return base.Handle(obj);
        }
    }
}

