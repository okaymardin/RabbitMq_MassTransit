using RabMQ.Common;
using RabMQ.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabMQ.PublisherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PublishOperations publishOperations = new PublishOperations();
            publishOperations.SendToQueue("TestMessage", new RequestModel
                                                         {
                                                             DocType = "DocType",
                                                             DocUuid = Guid.NewGuid().ToString()
                                                         });

            Console.WriteLine("Message sent successfully");

            Console.ReadLine();
        }
    }
}
