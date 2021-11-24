using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.Pharmacy.Service;
using Moq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.IntegrationTests
{
    
    public class NewsTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public async void Check_if_news_are_received(News pieceOfNews, Boolean isFirst)
        {
            var stubRepository = new Mock<INewsRepository>();
            var news = new List<News>();
            stubRepository.Setup(m => m.GetAll()).Returns(news);    
            RabbitMQService rabbitMQ = new RabbitMQService(stubRepository.Object, new PharmaciesRepository());
            CancellationToken token = new CancellationToken(false);
            await rabbitMQ.StartAsync(token);

      
            sendMessage(pieceOfNews, isFirst);
            await Task.Delay(3000);


            
            if (isFirst) stubRepository.Verify(rep => rep.GetAll(), Times.Once);
            else stubRepository.Verify(rep => rep.GetAll(), Times.Never);

        }

        public static IEnumerable<object[]> Data()
        {
            var retVal = new List<object[]>();

            retVal.Add(new object[] { new News(1, "Naslov1", "Sadrzaj1", DateTime.Now, DateTime.Now.AddDays(1), "1111", false, "Jankovic"), true });
            retVal.Add(new object[] { new News(2, "Naslov2", "Sadrzaj2", DateTime.Now, DateTime.Now.AddDays(1), "2222", true, "Jankovic"), false });

            return retVal;
        }

        private void sendMessage(News pieceOfNews, Boolean isFirst)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                if (isFirst)
                {
                    channel.ExchangeDeclare(exchange: "NewsChannel", type: ExchangeType.Fanout);
                    channel.QueueDeclare(queue: "Jankovic",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);


                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pieceOfNews));

                    channel.BasicPublish(exchange: "NewsChannel",
                                         routingKey: "Jankovic",
                                         basicProperties: null,
                                         body: body);
                }
                else
                {
                    {
                        channel.ExchangeDeclare(exchange: "Channel1", type: ExchangeType.Fanout);
                        channel.QueueDeclare(queue: "Queue1",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);


                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pieceOfNews));

                        channel.BasicPublish(exchange: "Channel1",
                                             routingKey: "Queue1",
                                             basicProperties: null,
                                             body: body);
                    }
                }
            }
        }

       

    }
}
