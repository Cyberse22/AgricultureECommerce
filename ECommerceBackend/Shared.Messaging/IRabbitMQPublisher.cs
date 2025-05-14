using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging
{
    public interface IRabbitMQPublisher
    {
        Task PublishAsync<T>(string queue, T message);
    }
}
