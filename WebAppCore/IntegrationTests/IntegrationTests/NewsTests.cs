using Integration.Partnership.Model;
using Integration.Partnership.Repository;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration.SharedModel;
using IntegrationTests.IntegrationTests.RabbitMQSender;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.IntegrationTests
{

    public class NewsTests
    {
        bool development = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";


        public readonly DbContextOptions<IntegrationDbContext> dbContextOptions;

        public NewsTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<IntegrationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }


        /* Test testira da li je sistem primio poruku preko RabbitMQ-a, tako sto proveri da li se primljena poruka sacuva u bazi
           Prvo se testira kada nas sistem nije pretplacen na neki kanal a poruka stigne na taj kanal 
                - trebalo bi da ignorise tj. da se poruka ne sacuva u bazu
           A zatim se testira kada stigne poruka na kanal na koji smo pretplaceni - da li se ta poruka sacuvala u bazi
        */
        [SkippableTheory]
        [MemberData(nameof(Data))]
        public async void Check_if_news_are_received(News pieceOfNews, string channelName, string queueName, int expected)
        {
            Skip.IfNot(development);
            // Arrange
            var testContext = new IntegrationDbContext(dbContextOptions);
            NewsRepository newsRepository = new NewsRepository(testContext);
            RabbitMQService rabbitMQ = new RabbitMQService(newsRepository, new PharmaciesRepository(), new TenderRepository());
            /* Napravljena je klasa Sender koja unutar ima sendMessage metodu koja posalje poruku na kanal i poziv te metode je u Arrange sekciji jer ne testiram tu metodu*/
            Sender sender = new Sender();   
            sender.sendMessage(pieceOfNews, channelName, queueName);

            // Act
            CancellationToken token = new CancellationToken(false);
            /* Unutar ove metode poziva metoda za primanje poruka StartAsync() -> NewsChannelExchange() -> RecieveNews()
               Mozda bi i ovo moglo da se refaktorise tako da se testira RecieveNews metoda,
               ali ovako sam htela da obuhvatim testom sto veci deo koda - manja verovatnoca od regresije */
            await rabbitMQ.StartAsync(token);   
            await Task.Delay(3000);

            // Assert
            newsRepository.GetAll().Count.ShouldBe(expected);
        }

        public static IEnumerable<object[]> Data()
        {
            var retVal = new List<object[]>();

            retVal.Add(new object[] { new News(2, "Naslov2", "Sadrzaj2", new DateRange(DateTime.Now, DateTime.Now.AddDays(1)), "2222", true, "Jankovic"), "Channel1", "Queue1", 0 });
            retVal.Add(new object[] { new News(1, "Naslov1", "Sadrzaj1", new DateRange(DateTime.Now, DateTime.Now.AddDays(1)), "1111", false, "Jankovic"), "JankovicNewsChannel", "Jankovic", 1 });

            return retVal;
        }


    }
}
