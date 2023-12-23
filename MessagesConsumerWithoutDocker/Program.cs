using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessagesConsumerWithoutDocker
{
    class Program
    {
        static void Main()
        {
            //read message
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "financeapp_createcountry_queue",
                               durable: true,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                    channel.ExchangeDeclare(exchange: "financeapp_exchange",
                                            type: ExchangeType.Direct,
                                            durable: true,
                                            autoDelete: false,
                                            arguments: null);

                    channel.QueueBind(queue: "financeapp_createcountry_queue",
                                      exchange: "financeapp_exchange",
                                      routingKey: "financeapp_createcountry_key",
                                      arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    Console.WriteLine("Waiting for messages . . .");

                    consumer.Received += (sender, args) =>
                    {
                        var body = args.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Message received is: {0}", message);
                    };

                    channel.BasicConsume(queue: "financeapp_createcountry_queue",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.ReadLine();
                }
            }
        }
    }
}