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
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                String prescriptionData = "Prescription for patient " + prescriptionDto.PatientName + "\nMedicine " + prescriptionDto.MedicineName +
                "\nDurationInDays " + prescriptionDto.DurationInDays + "\nTimesPerDay " + prescriptionDto.TimesPerDay;
                doc.Open();
                doc.Add(new Paragraph("Prescription for patient " + prescriptionDto.PatientName));
                doc.Add(new Paragraph("Medicine " + prescriptionDto.MedicineName));
                doc.Add(new Paragraph("DurationInDays " + prescriptionDto.DurationInDays));
                doc.Add(new Paragraph("TimesPerDay " + prescriptionDto.TimesPerDay));

                iTextSharp.text.pdf.PdfContentByte cb = writer.DirectContent;
                iTextSharp.text.pdf.BarcodeQRCode Qr = new BarcodeQRCode(prescriptionData,100,100,null);
                iTextSharp.text.Image img = Qr.GetImage();
                doc.Add(img);
                doc.Close();
                writer.Close();
            }
        }
    }
}
