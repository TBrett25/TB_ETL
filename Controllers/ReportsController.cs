using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAnalyticsAPI.Data;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace SalesAnalyticsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Export Sales Data to CSV
        [HttpGet("export-sales")]
        public async Task<IActionResult> ExportSalesToCsv()
        {
            var sales = await _context.Sales
                .Include(s => s.Product)
                .Include(s => s.Customer)
                .ToListAsync();

            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture); // ✅ Fix: CultureInfo now works

            // ✅ Write CSV Header
            csv.WriteField("Sale ID");
            csv.WriteField("Product Name");
            csv.WriteField("Customer Name");
            csv.WriteField("Sale Date");
            csv.WriteField("Amount");
      
            csv.NextRecord();

            // ✅ Write CSV Rows
            foreach (var sale in sales)
            {
                csv.WriteField(sale.Id);
                csv.WriteField(sale.Product.Name);
                csv.WriteField(sale.Customer.Name);
                csv.WriteField(sale.SaleDate.ToShortDateString());
                csv.WriteField(sale.Amount);
                csv.NextRecord();
            }

            writer.Flush();
            memoryStream.Position = 0;

            return File(memoryStream.ToArray(), "text/csv", "sales_report.csv");
        }
    }
}
