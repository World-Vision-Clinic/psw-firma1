using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.Pharmacy.Service;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.UnitTests
{
    public class NewsTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Change_view_status(News pieceOfNews, bool isPublished)
        {
          
            NewsService service = new NewsService(CreateStubRepository());
            service.Update(pieceOfNews);

            pieceOfNews.Posted.ShouldBe(isPublished);
        }

        public static IEnumerable<object[]> Data()
        {
            var retVal = new List<object[]>();

            retVal.Add(new object[] { new News(1, "Naslov1", "Sadrzaj1", DateTime.Now, DateTime.Now.AddDays(1), "1111", false, "Jankovic"), true });
            retVal.Add(new object[] { new News(2, "Naslov2", "Sadrzaj2", DateTime.Now, DateTime.Now.AddDays(1), "2222", true, "Jankovic"), false });

            return retVal;
        }

        private static INewsRepository CreateStubRepository()
        {
            var stubRepository = new Mock<INewsRepository>();
            var news = new List<News>();
            
            news.Add(new News(1,"Naslov1","Sadrzaj1",DateTime.Now,DateTime.Now.AddDays(1),"1111",false,"Jankovic"));
            news.Add(new News(2, "Naslov2", "Sadrzaj2", DateTime.Now, DateTime.Now.AddDays(1), "2222", true, "Jankovic"));
            stubRepository.Setup(m => m.GetAll()).Returns(news);

            return stubRepository.Object;
        }
    }
}
