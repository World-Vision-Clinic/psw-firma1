using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Integration_API.Dto;
using Integration_API.Mapper;
using Integration.Partnership.Service;
using Integration.Partnership.Repository;
using Integration.Partnership.Model;
using Integration.Pharmacy.Service;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
       
        TenderService service = new TenderService(new TenderRepository());
        [HttpPost]
        public IActionResult CreateTender(TenderCreationDto dto)
        {
            Tender tender = TenderMapper.TenderCreationDtoToTender(dto,Generator.GenerateTenderId());
            service.SaveTender(tender);
            TenderDto tenderDto = TenderMapper.TenderToTenderDto(tender);
            SendTender(tenderDto);
            return Ok();
        }

        public void SendTender(TenderDto tenderDto)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "TenderChannel", type: ExchangeType.Fanout);
                channel.QueueDeclare(queue: "World Vision Clinic",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tenderDto));

                channel.BasicPublish(exchange: "TenderChannel",
                                     routingKey: "World Vision Clinic",
                                     basicProperties: null,
                                     body: body);

            }
        }

        [HttpGet("getWholeStatistic")]
        public IActionResult GetWholeStatistic(string pharmacyName)
        {
            List<int> statistic = service.GetPharmacyWinningStatistic(pharmacyName);
            return Ok(statistic);
        }

        [HttpGet("getTendersPharmacyParticipated")]
        public IActionResult GetTendersPharmacyParticipated(string pharmacyName)
        {
            List<Tender> tenders = service.GetTendersPharmacyParticipated(pharmacyName);
            return Ok(tenders);
        }

        [HttpGet("getOffersForTender")]
        public IActionResult GetOffersForTender(string tenderHash, string pharmacyName)
        {
            List<TenderOffer> tenderOffers = service.GetOffersForTender(tenderHash, pharmacyName);
            return Ok(tenderOffers);
        }

        [HttpGet("getWinningOffersForPharmacy")]
        public IActionResult GetWinningOffersForPharmacy(string pharmacyName)
        {
            List<TenderOffer> winningOffers = service.GetWinningOffersForPharmacy(pharmacyName);
            List<WinningOfferDto> winningOffersDto = new List<WinningOfferDto>();
            foreach(TenderOffer offer in winningOffers)
            {
                winningOffersDto.Add(OfferMapper.OfferToWinningOfferDto(offer, service.GetTenderName(offer.TenderOfferHash)));
            }
            return Ok(winningOffersDto);
        }

    }
}