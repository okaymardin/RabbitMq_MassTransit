using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabMQ.Common.Model
{
    public class RabbitMqMessageModel : IDisposable
    {
        public virtual string MessageId { get; set; }
        public virtual string ConversationId { get; set; }
        public virtual string InitiatorId { get; set; }
        public virtual string RequestId { get; set; }
        public virtual string SourceAddress { get; set; }
        public virtual string DestinationAddress { get; set; }
        public virtual string ResponseAddress { get; set; }
        public virtual string FaultAddress { get; set; }
        public virtual DateTime? ExpirationTime { get; set; }
        public virtual DateTime? SentTime { get; set; }
        public virtual IDictionary<string, object> Headers { get; set; }
        public virtual RequestModel Message { get; set; }
        public virtual HostInfo Host { get; set; }
        public virtual string[] MessageType { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
