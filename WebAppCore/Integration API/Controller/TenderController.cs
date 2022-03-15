using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Model;
using System.IO;
using RestSharp;
using System.Net;
using Microsoft.AspNetCore.SignalR;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
       
        TenderService service = new TenderService(new TenderRepository());
        PharmaciesService pharmaciesService = new PharmaciesService(new PharmaciesRepository());
        CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());
        IPharmacyHttpConnection pharmacyConnection = new PharmacyHTTPConnection();
        EmailSender emailSender = new EmailSender();
        PdfGenerator pdfGenerator = new PdfGenerator();
        MedicinesController medicinesController;

        private IHubContext<SignalServer> _hubContext;

        public TenderController(IHubContext<SignalServer> hubcontext)
        {
            _hubContext = hubcontext;
            medicinesController = new MedicinesController(new PharmacyHTTPConnection(), new PharmacyGRPConnection(),_hubContext);
        }

        [HttpGet]
        public IActionResult GetTenders()
        {
            List<Tender> tenders = service.GetTenders();
            List<TenderDto> tendersDto = new List<TenderDto>();
            foreach (Tender t in tenders)
            {
                TenderDto dto = TenderMapper.TenderToTenderDto(t);
                tendersDto.Add(dto);
            }
            return Ok(tendersDto);
        }

        [HttpPost("addTenderOffer")]
        public IActionResult GetNewTenderOffer()
        {
            _hubContext.Clients.All.SendAsync("askServerResponse", "You recieved new tender offer.");
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateTender(TenderCreationDto dto)
        {
            Tender tender = TenderMapper.TenderCreationDtoToTender(dto,Generator.GenerateTenderId());
            service.SaveTender(tender);
            TenderDto tenderDto = TenderMapper.TenderToTenderDto(tender);
            SendTender(tenderDto);
            return Ok();
        }

        [HttpPost("closeTender")]
        public IActionResult CloseTender(TenderDto dto)
        {
            Tender tender = TenderMapper.TenderDtoToTender(dto);
            tender.EndTime = DateTime.Now;
            service.EditTenderEndTimeByHash(tender);
            TenderDto tenderDto = TenderMapper.TenderToTenderCloseDto(tender);
            SendTender(tenderDto);
            return Ok();
        }

        [HttpPost("chooseTenderWinner")]
        public IActionResult ChooseTenderWinner(TenderOfferDto dto)
        {
            Tender tender = GetTenderByWinnerOffer(dto);

            foreach (PharmacyProfile pharmacy in pharmaciesService.GetAll())
            {
                if (!pharmacy.Name.Equals(tender.TenderOffers.ElementAt(0).PharmacyName)) 
                {
                    emailSender.SendMailToLosingPharmacy(pharmacy.Email,tender);
                    continue; 
                }

                emailSender.SendMailToWinningPharmacy(pharmacy.Email,tender);
                if (pharmacy.ConnectionInfo.Protocol.Equals(ProtocolType.HTTP))
                    if (DeclareTenderWinner(TenderMapper.TenderToTenderDto(tender), pharmacy.ConnectionInfo.Domain))
                        if (!ExecuteProcurement(tender, pharmacy))
                            return BadRequest("Procurement wasn't executed!");
            }

            return Ok();
        }

        private Tender GetTenderByWinnerOffer(TenderOfferDto dto)
        {
            TenderOffer offer = TenderMapper.TenderOfferDtoToTenderOffer(dto);
            offer.Winner = true;
            service.EditTenderOfferById(offer);
            Tender tender = service.GetByTenderHash(dto.TenderHash);
            tender.EndTime = DateTime.Now;
            service.EditTenderEndTimeByHash(tender);
            tender.AddTenderOffer(offer.TenderOfferHash, offer.TenderHash, offer.PharmacyName, offer.TotalPrice, offer.OfferItems, offer.Winner);
            return tender;
        }

        private bool ExecuteProcurement(Tender tender, PharmacyProfile pharmacy) {
            TenderOffer offerForOrdering = service.GetTenderOfferWithOfferItems(tender.TenderOffers.ElementAt(0).PharmacyName, tender.TenderOffers.ElementAt(0).TenderOfferHash);
            foreach (OfferItem oi in offerForOrdering.OfferItems.ToArray())
            {
                if (!pharmacyConnection.SendMedicineOrderingRequestHTTP(new OrderingMedicineDTO(pharmacy.ConnectionInfo.Domain, oi.MedicineName, oi.Dosage.ToString(), oi.Quantity.ToString())))
                    return false;
            }
            return true;
        }

        private bool DeclareTenderWinner(TenderDto tenderDto, string domain)
        {
            var client = new RestClient(domain);
            var request = new RestRequest("/tender/result");
            Credential credential = credentialsService.GetByPharmacyLocalhost(domain);
            if (credential == null) return false;
            request.AddHeader("ApiKey", credential.ApiKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(tenderDto);
            IRestResponse response = client.Post(request);
            return response.StatusCode.Equals(HttpStatusCode.OK);
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

        [HttpGet("report")]
        public async Task<IActionResult> GetReport(DateTime start, DateTime end)
        {
            pdfGenerator.GenerateReport(start, end);
            string localPath = "Reports/Tender" + start.Month + "-" + start.Day + "-" + start.Year + "to" + end.Month + "-" + end.Day + "-" + end.Year + ".pdf";
            var memory = new MemoryStream();
            using (var stream = new FileStream(localPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            var fileName = Path.GetFileName(localPath);

            return File(memory, contentType, fileName);
        }

        [HttpGet("offers/number")]
        public IActionResult GetNumberOfOffers(DateTime start, DateTime end)
        {
            Dictionary<string, List<float>> data = service.GetNumberOfOffersForAllTenders(start, end);
            List<string> tenders = new List<string>();
            foreach (Tender tender in service.GetTendersWithOffers())
                if (tender.EndTime >= start && tender.EndTime <= end)
                    tenders.Add(tender.Title);

            var statisticData = new
            {
                data = data,
                tenders = tenders
            };
            return Ok(statisticData);
        }

        [HttpGet("prices/max")]
        public IActionResult GetMaxPrices(DateTime start, DateTime end)
        {
            Dictionary<string, List<float>> data = service.GetMaxPricesForAllTenders(start, end);
            List<string> tenders = new List<string>();
            foreach (Tender tender in service.GetTendersWithOffers())
                if (tender.EndTime >= start && tender.EndTime <= end)
                    tenders.Add(tender.Title);

                var statisticData = new
            {
                data = data,
                tenders = tenders
            };
            return Ok(statisticData);
        }

        [HttpGet("prices/min")]
        public IActionResult GetMinPrices(DateTime start, DateTime end)
        {
            Dictionary<string, List<float>> data = service.GetMinPricesForAllTenders(start, end);
            List<string> tenders = new List<string>();
            foreach (Tender tender in service.GetTendersWithOffers())
                if (tender.EndTime >= start && tender.EndTime <= end)
                    tenders.Add(tender.Title);

            var statisticData = new
            {
                data = data,
                tenders = tenders
            };
            return Ok(statisticData);
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