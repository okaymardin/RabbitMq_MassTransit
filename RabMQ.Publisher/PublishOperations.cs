using RabMQ.Common;
using RabMQ.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabMQ.PublisherConsole
{
    public class PublishOperations
    {
        Publisher publisher;

        public PublishOperations()
        {
            publisher = new Publisher();
        }

        public async void SendToQueue(string queueName, RequestModel model)
        {
            publisher.Send(queueName, model);
        }
    }
}
