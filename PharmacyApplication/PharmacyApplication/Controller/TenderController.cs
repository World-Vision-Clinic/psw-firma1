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

            //Devopsi
            OfferItem offerItem = new OfferItem();
            OfferItem offerItem1 = new OfferItem();
            offerItem.MedicineName = "Brufen";
            offerItem.Dosage = 200;
            offerItem.Quantity = 1;
            offerItem.Price = 600;
            offerItem1.MedicineName = "Aspirin";
            offerItem1.Dosage = 200;
            offerItem1.Quantity = 1;
            offerItem1.Price = 300;
            TenderOffer tenderOffer = new TenderOffer();
            tenderOffer.TenderOfferHash = "1";
            tenderOffer.OfferItems = new List<OfferItem>();
            tenderOffer.TotalPrice = 0;
            tenderOffer.OfferItems.Add(offerItem);
            tenderOffer.OfferItems.Add(offerItem1);
            foreach(OfferItem item in tenderOffer.OfferItems){
                tenderOffer.TotalPrice += item.Price;
            }






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
