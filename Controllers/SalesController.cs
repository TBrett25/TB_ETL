using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAnalyticsAPI.Data;
using SalesAnalyticsAPI.Models;

namespace SalesAnalyticsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  // ✅ Require Authentication for all routes
    public class SalesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            return await _context.Sales
                .Include(s => s.Product)
                .Include(s => s.Customer)
                .ToListAsync();
        }
    }
}
