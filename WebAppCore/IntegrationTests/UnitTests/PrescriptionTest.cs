using Integration_API.Controller;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.UnitTests
{
    public class PrescriptionTest
    {
        [Fact]
        public void Create_consumed_file()
        {
            PdfGenerator generator = new PdfGenerator();
            PrescriptionDto dto = new PrescriptionDto();
            dto.PatientName = "TestPacijent";
            dto.MedicineName = "TestMedicine";
            dto.Quantity = "10";
            dto.TimesPerDay = 1;
            dto.DosageInMg = "10";
            dto.DurationInDays = 1;
            generator.GeneratePrescriptionPdf("testgenerisanje.pdf", dto);

            Assert.NotNull(System.IO.File.OpenRead("testgenerisanje.pdf"));
        }
    }
}
