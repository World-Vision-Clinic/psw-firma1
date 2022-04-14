using Integration.Partnership.Model;
using Integration.Partnership.Repository;
using Integration.Partnership.Repository.RepositoryInterfaces;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Integration_API.MessageQueue
{
    public class TenderMQConnection : BackgroundService
    {
        IConnection connection;
        IModel channel;
        IPharmaciesRepository pharmaciesRepository;
        ITenderRepository tenderRepository;

        public TenderMQConnection(IPharmaciesRepository pharmaciesRepository, ITenderRepository tenderRepository)
        {
            this.pharmaciesRepository = pharmaciesRepository;
            this.tenderRepository = tenderRepository;
        }

        public TenderMQConnection()
        {
            pharmaciesRepository = new PharmaciesRepository();
            tenderRepository = new TenderRepository();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            TenderChannelExchange();
            return base.StartAsync(cancellationToken);
        }

        private void TenderChannelExchange()
        {
            foreach (PharmacyProfile pharmacyProfile in pharmaciesRepository.GetAll())
            {
                OpenConnectionTender(pharmacyProfile);
                RecieveTenderOffers(pharmacyProfile);
            }
        }

        private void RecieveTenderOffers(PharmacyProfile pharmacyProfile)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var jsonMessage = Encoding.UTF8.GetString(body);
                TenderOffer offer = JsonConvert.DeserializeObject<TenderOffer>(jsonMessage);
                SaveTenderOffer(offer, pharmacyProfile);
            };

            channel.BasicConsume(queue: pharmacyProfile.Name + "Offers",
                                    autoAck: true,
                                    consumer: consumer);
        }

        private void OpenConnectionTender(PharmacyProfile pharmacyProfile)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: pharmacyProfile.Name + "OffersChannel", type: ExchangeType.Direct);
            var queueName = channel.QueueDeclare(queue: pharmacyProfile.Name + "Offers",
                                   durable: false,
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null).QueueName;
            channel.QueueBind(queue: queueName,
                                exchange: pharmacyProfile.Name + "OffersChannel",
                                routingKey: pharmacyProfile.Name + "Offers");
        }

        private void SaveTenderOffer(TenderOffer offer, PharmacyProfile pharmacyProfile)
        {
            offer.PharmacyName = pharmacyProfile.Name;
            tenderRepository.AddOffer(offer);
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
