using Integration_API.Dto;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    public class PdfGenerator
    {
        public void GeneratePrescriptionPdf(string fileName, PrescriptionDto prescriptionDto)
        {
            using (FileStream fs = new FileStream(fileName + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();
                doc.Add(new Paragraph("Prescription for patient " + prescriptionDto.PatientName));
                doc.Add(new Paragraph("Medicine " + prescriptionDto.MedicineName));
                doc.Add(new Paragraph("DurationInDays " + prescriptionDto.DurationInDays));
                doc.Add(new Paragraph("TimesPerDay " + prescriptionDto.TimesPerDay));
                doc.Close();
                writer.Close();
            }
        }
    }
}
