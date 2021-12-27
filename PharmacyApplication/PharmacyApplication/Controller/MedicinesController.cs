using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using PharmacyAPI.Dto;
using Renci.SshNet;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        MedicineService service = new MedicineService(new MedicineRepository());
        HospitalsService hospitalService = new HospitalsService(new HospitalsRepository());
        CredentialsService credentialsService = new CredentialsService(new CredentialsRepository());

        [HttpGet("check")]
        public IActionResult CheckMedicineAvailability(string name = "", string dosage = "", string quantity = "")
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

            if (name.Length <= 0 || dosage.Length <= 0 || quantity.Length <= 0)
            {
                return BadRequest();
            }

            double dosageInMg = 0;
            int quantityInBoxes = 0;
            try
            {
                dosageInMg = Double.Parse(dosage);
                quantityInBoxes = Int32.Parse(quantity);
            }
            catch
            {
                return BadRequest();
            }


            if (!service.CheckQuantity(name, dosageInMg, quantityInBoxes))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("OrderMedicine")]
        public IActionResult OrderMedicine(OrderingMedicineDto dto)
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

            if (!dto.Test)
            {
                Medicine medicine = new Medicine(dto.MedicineName, Double.Parse(dto.MedicineGrams), int.Parse(dto.NumOfBoxes));
                Medicine med = service.FoundOrderedMedicine(medicine);
                service.OrderMedicine(medicine);

                var client = new RestSharp.RestClient(hospital.Localhost);
                var request = new RestRequest("/medicines/ordered");
                request.AddHeader("Content-Type", "application/json");
                List<string> replacements = service.FoundReplacements(medicine);
                request.AddJsonBody(
                new
                {
                    MedicineName = med.MedicineName,
                    Manufacturer = med.Manufacturer,
                    SideEffects = med.SideEffects,
                    Usage = med.Usage,
                    Weigth = med.Weigth,
                    MainPrecautions = med.MainPrecautions,
                    PotentialDangers = med.PotentialDangers,
                    Quantity = dto.NumOfBoxes,
                    Replacements = replacements,
                    Price = med.Price
                });
                IRestResponse response = client.Post(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok();
                }
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet("medicineConsumation")]
        public IActionResult GetMedicineCousumation()
        {
            LoadFile();
            string consumationReport = ReadConsumationReport();
            return Ok();
        }

        public void LoadFile()
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.0.16", "user", "password")))
            {
                client.Connect();
                string serverFile = @"\public\ConsumedMedicineReport.pdf";
                string localFile = "Reports/ConsumedMedicineReport.pdf";
                using (Stream stream = System.IO.File.OpenWrite(localFile))
                {
                    client.DownloadFile(serverFile, stream, x => { Console.WriteLine(x); });
                }
                client.Disconnect();
            }
        }

        public string ReadConsumationReport()
        {
            StreamReader reader = new StreamReader("Reports/ConsumedMedicineReport.pdf");
            string consumationReport = reader.ReadToEnd();
            reader.Close();
            return consumationReport;
        }

        [HttpGet("spec")]
        public IActionResult GetMedicineSpecification(string name = "")
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

            if (name.Length <= 0)
            {
                return BadRequest();
            }

            name = name.Substring(0, 1).ToUpper() + name.Substring(1, name.Length - 1);
            List<Medicine> medicines = service.GetByName(name);
            if (medicines.Count <= 0)
            {
                return BadRequest("Medicine don't exist");
            }

            foreach (Medicine medicine in medicines)
            {
                createPDFFile(service.GetSpecification(medicine), medicine.MedicineName);
                break;
            }

            uploadSpecification(name + ".pdf");

            return Ok();
        }

        private void createPDFFile(string specification, string fileName)
        {
            PdfWriter writer = new PdfWriter(fileName + ".pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            FontProgram fontProgram = FontProgramFactory.CreateFont();
            PdfFont font = PdfFontFactory.CreateFont(fontProgram, "Cp1250");
            document.SetFont(font);
            Paragraph header = new Paragraph("Specification" + "\n").SetTextAlignment(TextAlignment.CENTER).SetFontSize(26);
            document.Add(header);
            string[] paragraphs = specification.Split("\n");
            foreach (string p in paragraphs)
            {
                Paragraph paragraph = new Paragraph().SetTextAlignment(TextAlignment.LEFT).SetFontSize(16);
                paragraph.Add(p);
                document.Add(paragraph);
            }
            document.Close();
        }

        private void uploadSpecification(string filePath)
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.0.116", "user", "password")))
            {
                client.Connect();

                string sourceFile = @filePath;
                using (Stream stream = System.IO.File.OpenRead(sourceFile))
                {
                    client.UploadFile(stream, @"\public\" + System.IO.Path.GetFileName(sourceFile));
                }

                client.Disconnect();
            }

        }
        // Za testiranje dockera
        [HttpGet("test")]
        public IActionResult Get(long id = 0)
        {
            Medicine medicine = service.GetById(id);
            if (medicine == null)
                return BadRequest("No medicine with that id");
            MedicineDto dto = new MedicineDto(medicine.MedicineName, medicine.Weigth, medicine.Quantity);
            return Ok(dto);
        }
    }
}
