using Applications.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace Infraestructure.Infraestructure
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IModel channel;
        public MessagePublisher(IModel channel)
        {
            this.channel = channel;
        }

        public void PublishMessage(string eventName, string message)
        {
            var routingKey = $"event.{eventName}";
            var body = Encoding.UTF8.GetBytes(message);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "AGP",
                                  routingKey: routingKey,
                                  basicProperties: properties,
                                  body: body);
        }
    }
}
