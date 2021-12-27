using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements.Charting;
using ceTe.DynamicPDF.PageElements.Charting.Axes;
using ceTe.DynamicPDF.PageElements.Charting.Series;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Ionic.Zip;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = Integration.Pharmacy.Model.File;

namespace Integration_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        FilesRepository repository = new FilesRepository();

        [HttpGet]
        public IActionResult GetAllFiles()
        {
            return Ok(repository.GetAll().Where(n => n.Extension == "pdf").Select(n => new Integration.Pharmacy.Model.File
            {
                Id = n.Id,
                Name = n.Name,
                Extension = n.Extension,
            }).ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = repository.GetAll().Where(n => n.Id == id).FirstOrDefault();

            var memory = new MemoryStream();
            using (var stream = new FileStream(file.Path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            var fileName = Path.GetFileName(file.Path);

            return File(memory, contentType, fileName);
        }

        [HttpGet("generate")]
        public IActionResult Get()
        {
            Document document = new Document();
            Page page = new Page();
            document.Pages.Add(page);

            Chart chart = new Chart(0, 0, 400, 230);
            PlotArea plotArea = chart.PrimaryPlotArea;

            Title title1 = new Title("Number of Offers");
            Title title2 = new Title("09/22/2021 - 12/22/2021");
            chart.HeaderTitles.Add(title1);
            chart.HeaderTitles.Add(title2);

            IndexedColumnSeries columnSeries1 = new IndexedColumnSeries("Jankovic");
            columnSeries1.Values.Add(new float[] { 5, 7, 9, 6 });
            IndexedColumnSeries columnSeries2 = new IndexedColumnSeries("Benu");
            columnSeries2.Values.Add(new float[] { 4, 2, 5, 8 });
            IndexedColumnSeries columnSeries3 = new IndexedColumnSeries("Lilly");
            columnSeries3.Values.Add(new float[] { 2, 4, 6, 9 });

            AutoGradient autogradient1 = new AutoGradient(180f, CmykColor.Red, CmykColor.IndianRed);
            columnSeries1.Color = autogradient1;
            AutoGradient autogradient2 = new AutoGradient(180f, CmykColor.Green, CmykColor.YellowGreen);
            columnSeries2.Color = autogradient2;
            AutoGradient autogradient3 = new AutoGradient(180f, CmykColor.Blue, CmykColor.LightBlue);
            columnSeries3.Color = autogradient3;

            plotArea.Series.Add(columnSeries1);
            plotArea.Series.Add(columnSeries2);
            plotArea.Series.Add(columnSeries3);

            Title lTitle = new Title("Number of offers");
            columnSeries1.YAxis.Titles.Add(lTitle);

            columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("T1", 0));
            columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("T2", 1));
            columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("T3", 2));
            columnSeries1.XAxis.Labels.Add(new IndexedXAxisLabel("T4", 3));

            page.Elements.Add(chart);


            // CHART 1

            Chart chart1 = new Chart(0, 240, 400, 230);
            PlotArea plotArea1 = chart1.PrimaryPlotArea;

            Title title3 = new Title("Pharmacy profit");
            Title title4 = new Title("09/22/2021 - 12/22/2021");
            chart1.HeaderTitles.Add(title3);
            chart1.HeaderTitles.Add(title4);

            IndexedColumnSeries columnSeries11 = new IndexedColumnSeries("Jankovic");
            columnSeries11.Values.Add(new float[] { 4, 2, 5, 8 });

            AutoGradient autogradient11 = new AutoGradient(180f, CmykColor.Red, CmykColor.IndianRed);
            columnSeries11.Color = autogradient11;


            plotArea1.Series.Add(columnSeries11);

            Title lTitle1 = new Title("Profit (in millions)");
            columnSeries11.YAxis.Titles.Add(lTitle1);

            columnSeries11.XAxis.Labels.Add(new IndexedXAxisLabel("T1", 0));
            columnSeries11.XAxis.Labels.Add(new IndexedXAxisLabel("T2", 1));
            columnSeries11.XAxis.Labels.Add(new IndexedXAxisLabel("T3", 2));
            columnSeries11.XAxis.Labels.Add(new IndexedXAxisLabel("T4", 3));

            page.Elements.Add(chart1);

            // CHART 2

            Chart chart2 = new Chart(0, 480, 400, 230);
            PlotArea plotArea2 = chart2.PrimaryPlotArea;

            Title title5 = new Title("Pharmacy offers");
            Title title6 = new Title("09/22/2021 - 12/22/2021");
            chart2.HeaderTitles.Add(title5);
            chart2.HeaderTitles.Add(title6);

            IndexedColumnSeries columnSeries22 = new IndexedColumnSeries("Jankovic");
            columnSeries22.Values.Add(new float[] { 1, 3, 4, 2 });

            AutoGradient autogradient22 = new AutoGradient(180f, CmykColor.Red, CmykColor.IndianRed);
            columnSeries22.Color = autogradient22;

            plotArea2.Series.Add(columnSeries22);

            Title lTitle2 = new Title("Number of offers");
            columnSeries22.YAxis.Titles.Add(lTitle2);

            columnSeries22.XAxis.Labels.Add(new IndexedXAxisLabel("T1", 0));
            columnSeries22.XAxis.Labels.Add(new IndexedXAxisLabel("T2", 1));
            columnSeries22.XAxis.Labels.Add(new IndexedXAxisLabel("T3", 2));
            columnSeries22.XAxis.Labels.Add(new IndexedXAxisLabel("T4", 3));

            page.Elements.Add(chart2);
            document.Draw(@"Output.pdf");

            return Ok();
        }
    }
}
