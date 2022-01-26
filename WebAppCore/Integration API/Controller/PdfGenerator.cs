using Integration_API.Dto;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements.Charting;
using ceTe.DynamicPDF.PageElements.Charting.Axes;
using ceTe.DynamicPDF.PageElements.Charting.Series;
using System;
using System.IO;
using System.Collections.Generic;
using Integration.Partnership.Model;
using System.Linq;
using Integration.Partnership.Service;
using Integration.Partnership.Repository;

namespace Integration_API.Controller
{
    public class PdfGenerator
    {
        TenderService tenderService = new TenderService(new TenderRepository());
        public void GeneratePrescriptionPdf(string fileName, PrescriptionDto prescriptionDto)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                iTextSharp.text.Document doc = new iTextSharp.text.Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                string prescriptionData = "Prescription for patient " + prescriptionDto.PatientName + "\nMedicine " + prescriptionDto.MedicineName +
                "\nDurationInDays " + prescriptionDto.DurationInDays + "\nTimesPerDay " + prescriptionDto.TimesPerDay;
                doc.Open();
                doc.Add(new Paragraph("Prescription for patient " + prescriptionDto.PatientName));
                doc.Add(new Paragraph("Medicine " + prescriptionDto.MedicineName));
                doc.Add(new Paragraph("DurationInDays " + prescriptionDto.DurationInDays));
                doc.Add(new Paragraph("TimesPerDay " + prescriptionDto.TimesPerDay));

                PdfContentByte cb = writer.DirectContent;
                BarcodeQRCode Qr = new BarcodeQRCode(prescriptionData, 100, 100, null);
                iTextSharp.text.Image img = Qr.GetImage();
                doc.Add(img);
                doc.Close();
                writer.Close();
            }
        }

        public void GenerateReport(DateTime start, DateTime end)
        {
            ceTe.DynamicPDF.Document document = new ceTe.DynamicPDF.Document();
            Page page = new Page();
            document.Pages.Add(page);
            string title = "Report for " + start.Month + "/" + start.Day + "/" + start.Year + " - " + end.Month + "/" + end.Day + "/" + end.Year;
            TextArea textArea = new TextArea(title, 100, 0, 400, 30, ceTe.DynamicPDF.Font.HelveticaBoldOblique, 18);
            page.Elements.Add(textArea);

            Chart chartNumberOfOffers = getGraphForNumberOfOffers(start, end);
            Chart chartMaxPrices = getGraphForMaxPrices(start, end);
            Chart chartMinPrices = getGraphForMinPrices(start, end);

            page.Elements.Add(chartNumberOfOffers);
            page.Elements.Add(chartMaxPrices);
            page.Elements.Add(chartMinPrices);

            string localPath = "Reports/Tender" + start.Month + "-" + start.Day + "-" + start.Year + "to" + end.Month + "-" + end.Day + "-" + end.Year + ".pdf";
            document.Draw(localPath);
        }

        private Chart getGraphForNumberOfOffers(DateTime start, DateTime end)
        {
            Chart chart = new Chart(80, 40, 400, 230);
            PlotArea plotArea = chart.PrimaryPlotArea;

            Dictionary<string, List<float>> data = tenderService.GetNumberOfOffersForAllTenders(start, end);

            Title title1 = new Title("Number of Offers");
            chart.HeaderTitles.Add(title1);

            List<IndexedColumnSeries> columnSeries = new List<IndexedColumnSeries>();

            foreach (string k in data.Keys)
            {
                IndexedColumnSeries newColumnSeries = new IndexedColumnSeries(k);
                newColumnSeries.Values.Add(data[k].ToArray());
                columnSeries.Add(newColumnSeries);
                plotArea.Series.Add(newColumnSeries);
            }

            Title Title = new Title("Number of offers");
            columnSeries[0].YAxis.Titles.Add(Title);
            List<Tender> allTenders = tenderService.GetTendersWithOffers();
            for (int t = 0; t < allTenders.Count; t++)
            {
                if (allTenders[t].EndTime >= start && allTenders[t].EndTime <= end)
                    columnSeries[0].XAxis.Labels.Add(new IndexedXAxisLabel(allTenders.ElementAt(t).Title, t));
            }

            return chart;
        }

        private Chart getGraphForMaxPrices(DateTime start, DateTime end)
        {
            Chart chart = new Chart(80, 270, 400, 230);
            PlotArea plotArea = chart.PrimaryPlotArea;

            Dictionary<string, List<float>> data = tenderService.GetMaxPricesForAllTenders(start, end);

            Title title1 = new Title("Max prices in dollars");
            chart.HeaderTitles.Add(title1);

            List<IndexedColumnSeries> columnSeries = new List<IndexedColumnSeries>();

            foreach (string k in data.Keys)
            {
                IndexedColumnSeries newColumnSeries = new IndexedColumnSeries(k);
                newColumnSeries.Values.Add(data[k].ToArray());
                columnSeries.Add(newColumnSeries);
                plotArea.Series.Add(newColumnSeries);
            }

            Title Title = new Title("Price in dollars");
            columnSeries[0].YAxis.Titles.Add(Title);
            List<Tender> allTenders = tenderService.GetTendersWithOffers();
            for (int t = 0; t < allTenders.Count; t++)
            {
                if (allTenders[t].EndTime >= start && allTenders[t].EndTime <= end)
                    columnSeries[0].XAxis.Labels.Add(new IndexedXAxisLabel(allTenders.ElementAt(t).Title, t));
            }

            return chart;
        }

        private Chart getGraphForMinPrices(DateTime start, DateTime end)
        {
            Chart chart = new Chart(80, 490, 400, 230);
            PlotArea plotArea = chart.PrimaryPlotArea;

            Dictionary<string, List<float>> data = tenderService.GetMinPricesForAllTenders(start, end);

            Title title1 = new Title("Min prices in dollars");
            chart.HeaderTitles.Add(title1);

            List<IndexedColumnSeries> columnSeries = new List<IndexedColumnSeries>();

            foreach (string k in data.Keys)
            {
                IndexedColumnSeries newColumnSeries = new IndexedColumnSeries(k);
                newColumnSeries.Values.Add(data[k].ToArray());
                columnSeries.Add(newColumnSeries);
                plotArea.Series.Add(newColumnSeries);
            }

            Title Title = new Title("Price in dollars");
            columnSeries[0].YAxis.Titles.Add(Title);
            List<Tender> allTenders = tenderService.GetTendersWithOffers();
            for (int t = 0; t < allTenders.Count; t++)
            {
                if (allTenders[t].EndTime >= start && allTenders[t].EndTime <= end)
                    columnSeries[0].XAxis.Labels.Add(new IndexedXAxisLabel(allTenders.ElementAt(t).Title, t));
            }

            return chart;
        }
    }
}
