using Integration.Partnership.Model;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration.SharedModel;
using Integration_API.MessageQueue;
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

        [SkippableTheory]
        [MemberData(nameof(Data))]
        public async void Check_if_news_are_received(News pieceOfNews, string channelName, string queueName, int expected)
        {
            Skip.IfNot(development);
            var testContext = new IntegrationDbContext(dbContextOptions);
            NewsRepository newsRepository = new NewsRepository(testContext);
            NewsMQConnection newsMQ = new NewsMQConnection(new PharmaciesRepository(), newsRepository);
            Sender sender = new Sender();   
            sender.sendMessage(pieceOfNews, channelName, queueName);

            CancellationToken token = new CancellationToken(false);
            await newsMQ.StartAsync(token);
            await Task.Delay(3000);

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
