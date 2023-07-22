using System;
using InfluxDB.Client;
using Microsoft.Extensions.Configuration;

namespace NTU.IoT.Utility
{
	public class InfluxDBService
	{
        private readonly string _token;
        private readonly IConfiguration _configuration;

        public InfluxDBService(IConfiguration configuration)
        {
            _token = configuration["InfluxDB:Token"];
            _configuration = configuration;
        }

        public void Write(Action<WriteApi> action)
        {
            using var client = InfluxDBClientFactory.Create(_configuration["InfluxDB:Url"], _token);
            using var write = client.GetWriteApi();
            action(write);
        }

        public async Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action)
        {
            using var client = InfluxDBClientFactory.Create(_configuration["InfluxDB:Url"], _token);
            var query = client.GetQueryApi();
            return await action(query);
        }
    }
}

