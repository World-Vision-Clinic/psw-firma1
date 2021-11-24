using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.Service;
using Pharmacy.Repository;
using Pharmacy.Model;
using PharmacyAPI.Mapper;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace PharmacyAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        NewsService newsService = new NewsService(new NewsRepository());
        [HttpPost("add")]
        public IActionResult Add(NewsDto dto)
        {
            if (dto.Content.Length <= 0 || dto.Title.Length <= 0 || dto.FromDate == null || dto.ToDate ==null)
            {
                return BadRequest();
            }

            News newNews = NewsMapper.NewsDtoToNews(dto, Generator.GenerateNewsId());
            newsService.Save(newNews);


            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "NewsChannel", type: ExchangeType.Fanout);
                channel.QueueDeclare(queue: "Jankovic",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto));

                channel.BasicPublish(exchange: "NewsChannel",
                                     routingKey: "Jankovic",
                                     basicProperties: null,
                                     body: body);

            }
                return Ok();
        }
    }
}
