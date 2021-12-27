using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Repository.RepositoryInterfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pharmacy.Service
{
    public class RabbitMQService :BackgroundService
    {
        IConnection connection;
        IModel channel;    
        IHospitalsRepository hospitalsRepository;
        ITendersRepository tendersRepository;

      
        public RabbitMQService()
        {
            hospitalsRepository = new HospitalsRepository();
            tendersRepository = new TendersRepository();
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (Hospital hospital in hospitalsRepository.GetAll())
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();

                channel.ExchangeDeclare(exchange: "TenderChannel", type: ExchangeType.Fanout);
                var queueName = channel.QueueDeclare(queue: hospital.Name,
                                       durable: false,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null).QueueName;
                channel.QueueBind(queue: queueName,
                                    exchange: "TenderChannel",
                                    routingKey: hospital.Name);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    Tender tender;

                    tender = JsonConvert.DeserializeObject<Tender>(jsonMessage);

                    tender.HospitalName = hospital.Name;

                    tendersRepository.Save(tender);

                };


                channel.BasicConsume(queue: hospital.Name,
                                        autoAck: true,
                                        consumer: consumer);
            }
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            channel.Close();
            connection.Close();
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
