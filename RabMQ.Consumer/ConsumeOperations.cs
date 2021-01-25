using GreenPipes;
using MassTransit;
using RabMQ.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabMQ.Consumer
{
    public class ConsumeOperations
    {
        public static void ConsumeMail(string queueName)
        {
            try
            {
                Bus.Factory.CreateUsingRabbitMq(sbc =>
                {
                    sbc.Host(new Uri("rabbitmq://localhost/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    sbc.ReceiveEndpoint(queueName, e =>
                    {
                        e.UseRetry(r => r.Incremental(10, TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10)));
                        e.Consumer<Consumer>();

                    });
                }).Start();

            }
            catch (Exception ex)
            {
            }
        }

        public class Consumer : IConsumer<RequestModel>
        {
            public async Task Consume(ConsumeContext<RequestModel> context)
            {
                RequestModel mailCommand = context.Message;
                Console.WriteLine(mailCommand.DocUuid);
            }
        }
    }
}
