using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RabMQ.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabMQ.Common
{
    public class Publisher
    {
        public void Send(string queueName, object message)
        {
            try
            {
                string messageJson = getMessageModel(message);
                
                var connection = RabbitMqService.GetRabbitMQSingleConnection();
                var channel = RabbitMqService.GetRabbitMQSingleChannel(connection);

                IBasicProperties props = channel.CreateBasicProperties();
                props.DeliveryMode = 2;

                channel.BasicPublish("", queueName, true, props, Encoding.UTF8.GetBytes(messageJson));

                channel.ConfirmSelect();
            }
            catch (BrokerUnreachableException ex)
            {
                RabbitMqService.ConnectionRefresh();
                throw;
            }
            catch (Exception ex)
            {
                RabbitMqService.ConnectionRefresh();
                throw;
            }
        }

        private string getMessageModel(object message)
        {
            using (RabbitMqMessageModel model = new RabbitMqMessageModel())
            {
                model.ConversationId = Guid.NewGuid().ToString();
                model.MessageId = Guid.NewGuid().ToString();
                model.Message = (RequestModel)message;
                model.MessageType = new[]
                        {
                        "urn:message:RabMQ.Common.Model:RequestModel",
                    };
                model.DestinationAddress = "rabbitmq://localhost/RabbitMq:RequestModel";
                model.SentTime = DateTime.Now;

                return JsonConvert.SerializeObject(model);
            }
        }
    }
}
