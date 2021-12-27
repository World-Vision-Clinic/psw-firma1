using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        TenderService tenderService = new TenderService(new TendersRepository());

        [HttpGet("{id?}")]      
        public IActionResult Get(string id)
        {
            Tender tender = tenderService.GetTenderById(id);
            if (tender == null || (tender.EndTime != null && tender.EndTime< DateTime.Now))
            {
                return NotFound();
            } 

            TenderOffer tenderOffer = tenderService.CreateTenderOffer(tender);
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "JankovicOffersChannel", type: ExchangeType.Direct);
                channel.QueueDeclare(queue: "JankovicOffers",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tenderOffer));

                channel.BasicPublish(exchange: "JankovicOffersChannel",
                                     routingKey: "JankovicOffers",
                                     basicProperties: null,
                                     body: body);

            }

            return Ok();
        }
    }
}
