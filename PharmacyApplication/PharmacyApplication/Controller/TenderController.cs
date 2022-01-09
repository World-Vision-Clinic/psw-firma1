using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using PharmacyAPI.Dto;
using PharmacyAPI.Mapper;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace PharmacyAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        TenderService tenderService = new TenderService(new TendersRepository(), new MedicineRepository());
        HospitalsService hospitalService = new HospitalsService(new HospitalsRepository());
        const string PHARMACY_NAME = "Jankovic";

        [HttpPost]
        public IActionResult SendOffer(TenderDto tenderDto)
        {
            Tender tender = tenderService.GetByTenderHash(tenderDto.Id);
            if (tender == null || (tender.EndTime != null && tender.EndTime < DateTime.Now))
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


                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(TenderOfferMapper.TenderOfferToTenderOfferDto(tenderOffer, tender.TenderHash)));

                channel.BasicPublish(exchange: "JankovicOffersChannel",
                                     routingKey: "JankovicOffers",
                                     basicProperties: null,
                                     body: body);

            }

            return Ok();
        }

        [HttpPost("result")]
        public IActionResult reciveTenderResults(TenderWinnerDto dto)
        {
            if (!Request.Headers.TryGetValue("ApiKey", out var extractedApiKey))
            {
                return BadRequest("Api Key was not provided");
            }

            Hospital hospital = hospitalService.GetHospitalByApiKey(extractedApiKey);
            if (hospital == null)
            {
                return BadRequest("Api Key is not valid!");
            }

            Tender activeTender = tenderService.GetByTenderHash(dto.TenderHash);

            if (activeTender == null)
            {
                return BadRequest();
            }

            foreach (TenderOffer offer in dto.TenderOffers)
            {
                if (offer.PharmacyName == PHARMACY_NAME)
                {
                    TenderOffer tenderOffer = activeTender.TenderOffers.SingleOrDefault(o => o.TenderOfferHash == offer.TenderOfferHash);
                    tenderOffer.Winner = true;
                    tenderService.Update(activeTender);
                }
            }
            activeTender.EndTime = DateTime.Now;
            tenderService.CloseTender(activeTender);


            return Ok();

        }
    }
}
