using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabMQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsumeOperations.ConsumeMail("TestMessage");

            Console.WriteLine("Waiting consume message.");
            Console.ReadLine();
        }
    }
}
