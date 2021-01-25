using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabMQ.Common
{
    public class RabbitMqService
    {
        private static IConnection _connection { get; set; }
        private static IModel _channel { get; set; }

        private static object LockObject = new object();

        public static IConnection GetRabbitMQSingleConnection()
        {
            try
            {
                if (_connection == null || _connection.CloseReason != null)
                {
                    lock (LockObject)
                    {
                        if (_connection == null || _connection.CloseReason != null)
                        {
                            string _uri = ConfigurationManager.AppSettings["AMQP_Uri"];
                            _connection = new ConnectionFactory
                            {
                                Uri = new Uri(_uri),
                                AutomaticRecoveryEnabled = true,
                                NetworkRecoveryInterval = TimeSpan.FromSeconds(5),

                            }.CreateConnection();
                        }
                    }
                }

                return _connection;
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                throw;
            }
        }

        public static IModel GetRabbitMQSingleChannel(IConnection connection)
        {
            try
            {
                if (_channel == null || _channel.CloseReason != null)
                {
                    lock (LockObject)
                    {
                        if (_channel == null || _channel.CloseReason != null)
                        {
                            _channel = connection.CreateModel();
                        }
                    }
                }

                return _channel;
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                throw;
            }
        }

        public static void CloseConnection()
        {
            _connection.Close();
            _channel.Close();
        }

        public static void ConnectionRefresh()
        {
            _connection = null;
            _channel = null;
        }

        public static IConnection GetRabbitMQConnection()
        {
            try
            {

                string _uri = ConfigurationManager.AppSettings["AMQP_Uri"];
                _connection = new ConnectionFactory
                {
                    Uri = new Uri(_uri),
                    AutomaticRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds(5),

                }.CreateConnection();

                return _connection;
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                throw;
            }
        }
    }
}
