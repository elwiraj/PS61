using Microsoft.EntityFrameworkCore;
using PS6.Areas.Data.Models;
using PS6.Areas.YearDb;
using PS6.Pages;
using PS6.Pages.Forms;
using System.Security.Claims;

namespace PS6.Areas.Data
{
    public interface ILeapYearInterface
    {
        Task<List<YearValidationResult>> GetYearResults(int pageIndex);
        Task SaveYearResult(YearResponse yearResponse, HttpContext httpContext);
    }


    public class LeapYearService : ILeapYearInterface
    {
        private readonly YearDbContext _context;

        public LeapYearService(YearDbContext context)
        {
            _context = context;
        }


        public async Task SaveYearResult(YearResponse yearResponse, HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = httpContext.User.Identity?.Name;

            var yearValidationResult = new YearValidationResult
            {
                Year = yearResponse.Year,
                Result = yearResponse.ToString(),
                TimeAdded = DateTime.Now,
                UserId = userId,
                UserLogin = userName
            };

            _context.YearValidationResult?.Add(yearValidationResult);
            await _context.SaveChangesAsync();
        }

        public async Task<List<YearValidationResult>> GetYearResults(int pageIndex)
        {
            return await PaginatedList<YearValidationResult>.CreateAsync(
                _context.YearValidationResult, pageIndex, 20);
        }
    }
}
