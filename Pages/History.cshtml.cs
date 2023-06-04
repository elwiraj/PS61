using Microsoft.AspNetCore.Mvc.RazorPages;
using PS6.Areas.Data;
using PS6.Areas.Data.Models;

namespace PS6.Pages.History;

public class IndexModel : PageModel
{
    private readonly ILeapYearInterface _leapYearService;

    public IndexModel(ILeapYearInterface leapYearService)
    {
        _leapYearService = leapYearService;
    }

    public IList<YearValidationResult> YearValidationResult { get;set; } = default!;

    public async Task OnGetAsync(int pageIndex = 1)
    {
        YearValidationResult = await _leapYearService.GetYearResults(pageIndex);
    }
}
