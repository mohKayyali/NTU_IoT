using System;
using Newtonsoft.Json;
using NTU.IoT.Models;
using RestSharp;

namespace NTU.IoT.Utility.ChainOfResponsibility
{
	public class FlowHandlerCreation : AbstractHandler
    {
        public override object Handle(object obj)
        {
            System.Diagnostics.Debug.WriteLine("FlowHandler: "+ ((DeviceType)obj).name);


            var deviceType = (DeviceType)obj;
            string kafkaBrokerId = "";
            string mqttBrokerId = "";
            string noSqlDBId = "";

            var client = new RestClient("http://localhost:1880");

            var request = new RestRequest("flows", Method.Get);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var flows = JsonConvert.DeserializeObject<List<dynamic>>(response.Content);

                // Access the flow ID(s) from the response

                var kafkaBroker = flows.Where(x => x.type == "Kafka Broker").FirstOrDefault();


                kafkaBrokerId = kafkaBroker.id;

                var mqttBroker = flows.Where(x => x.type == "mqtt-broker").FirstOrDefault();
                mqttBrokerId = mqttBroker.id;

                var influxDB = flows.Where(x => x.type == "influxdb").FirstOrDefault();
                noSqlDBId = influxDB.id;

            }
            else
            {
                Console.WriteLine($"Error retrieving flows: {response.ErrorMessage}");
            }



            string topicName = deviceType.topic_name;
            string flowId = deviceType.name + "_flow";
            string flowName = deviceType.name;
            string tableName = deviceType.table_name;

            request = new RestRequest("/flow", Method.Post);
            request.AddJsonBody(new
            {

                label = flowName,
                nodes = new object[]
                {
        new {
            id = "mqttin" + flowId,
            type = "mqtt in",
            name = "mqtt in",
            topic = topicName,
            qos = "0",
            datatype = "auto-detect",
            broker = mqttBrokerId,
            nl = false,
            rap = true,
            rh = 0,
            inputs = 0,
            x = 110,
            y = 180,
            wires = new[] { "json1" + flowId }
            },
            new {
            id = "kafkaProducer" + flowId,
            type = "Kafka Producer",
            name = "Kafka Producer",
            broker = kafkaBrokerId,
            topic = topicName,
            topicSlash2dot = false,
            requireAcks = 1,
            ackTimeoutMs = 100,
            partitionerType = 0,
            key = "",
            partition = 0,
            x = 440,
            y = 180
            },
            new {
         id="KafkaConsumer"+flowId,
         type="Kafka Consumer",
         name="Kafka Consumer",
         broker=kafkaBrokerId,
         regex=false,
         topics=new[]
            {
             new{
               topic=topicName,
               offset=0,
               partition=0
             }
            },
         groupId="kafka-node-group",
         autoCommit="true",
         autoCommitIntervalMs=5000,
         fetchMaxWaitMs=100,
         fetchMinBytes=1,
         fetchMaxBytes=1048576,
         fromOffset=0,
         encoding="utf8",
         keyEncoding="utf8",
         connectionType="Consumer",
         convertToJson=false,
         x=160,
         y=500,
         wires = new[] { "json2" + flowId }
            },
        new {
         id="NoSqlDB" + flowId,
         type="influxdb out",
         influxdb=noSqlDBId, //to do
         name=topicName,
         measurement=tableName,
         precision="",
         retentionPolicy="",
         database="IOT",
         precisionV18FluxV20="ms",
         retentionPolicyV18Flux="autogen",
         org="NTU",
         bucket="IOT",
         x=770,
         y=500
        },
        new {
         id="json2"+flowId,
         type="json",
         name="",
         property="payload",
         action="",
         pretty=false,
         x=350,
         y=500,
         wires = new[] { "function" + flowId }

        },
        new {
         id="json1"+flowId,
         type="json",
         name="",
         property="payload",
         action="",
         pretty=false,
         x=290,
         y=180,
         wires = new[] { "kafkaProducer" + flowId }

        },
        new{
        id="function"+flowId,
         type="function",

         name="Adding timestamp",
         func="\nvar timestamp = new Date().toISOString();\nmsg.payload.timestamp = timestamp;\nreturn msg;",
         outputs=1,
         noerr=0,
         initialize="",
         finalize="",

            x=550,
            y=500,
            wires = new[] { "NoSqlDB" + flowId }
            }

            }

            });



            response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var flow = JsonConvert.DeserializeObject<dynamic>(response.Content);
                Console.WriteLine("Inject node added successfully" + flow.id);
            }
            else
            {
                Console.WriteLine("Failed to add the Inject node");
            }

            return base.Handle(obj);
        }
    }
}

