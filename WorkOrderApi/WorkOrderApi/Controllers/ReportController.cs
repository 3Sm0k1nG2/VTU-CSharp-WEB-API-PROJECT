using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;
using WorkOrders_BAL.Entities.Auth;
using WorkOrders_DAL;

namespace WorkOrderApi.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        protected readonly IConverter _converter;
        protected readonly WorkOrderController _workOrderController;
        protected readonly IMemoryCache _memoryCache;

        public ReportController(WorkOrderController workOrderController, IConverter converter, IMemoryCache memoryCache)
        {
            _converter = converter;
            _workOrderController = workOrderController;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GeneratePDF()
        {
            var now = DateTime.Now;
            //_workOrderController.GetTotalCost().Result;


            //_workOrderController.GetRushJobsCount()).Result.ExecuteResult(new ActionContext());
            var html = _memoryCache.Get<string>(Cache.Keys.Pdf);

            if(html is null) 
            {
                html = $@"
                <!DOCTYPE html>
                <html lang=""en"">
                    <head>
                        <title>Report</title>
                        Summary report for work orders.
                    </head>
                    <body>
                        <center><h1>SUMMARY REPORT</h1></center>
                        <div>Rush jobs: {_workOrderController.GetRushJobsCount()}</div>
                        <div>Total parts costs: {_workOrderController.GetTotalCost().ToString("F")}</div>
                        <div>Average part costs: {_workOrderController.GetAverageCost().ToString("F")}</div>
                        <div>Most preferred method of payment: {_workOrderController.GetMostFound("payment")}</div>
                        <div>Most jobs assigned to lead tech: {_workOrderController.GetMostFound("leadtech")}</div>
                        <div>Most requested service: {_workOrderController.GetMostFound("service")}</div>
                        <div>Most work in district: {_workOrderController.GetMostFound("district")}</div>

                        <footer>
                            <p>Summary report has been completed.</p>
                        </footer>
                    </body>
                </html>
                ";

                _memoryCache.Set(Cache.Keys.Pdf, html, Cache.Options.TimeSpans.Day);
            }

            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A4;
            globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25 };
            globalSettings.DocumentTitle = "report_" + now.ToString("s");

            ObjectSettings objectSettings = new ObjectSettings();
            objectSettings.PagesCount = true;
            objectSettings.HtmlContent = html;

            WebSettings webSettings = new WebSettings();
            webSettings.DefaultEncoding = "utf-8";
            webSettings.PrintMediaType = true;

            HeaderSettings headerSettings = new HeaderSettings();
            headerSettings.FontSize = 15;
            headerSettings.FontName = "Ariel";
            headerSettings.Right = "Page [page] of [toPage]";
            headerSettings.Line = true;
                
            FooterSettings footerSettings = new FooterSettings();
            footerSettings.FontSize = 12;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = $"Generated at:\t {now.ToLongDateString()} {now.ToLongTimeString()}";
            footerSettings.Line = true;

            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;

            HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            var pdfFile = _converter.Convert(htmlToPdfDocument);
            return File(pdfFile, "application/pdf");
        }
    }
}
