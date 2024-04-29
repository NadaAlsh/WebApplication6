using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{

    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        // Admin only actions
    }

    [Authorize]
    [Route("api/bank")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly BankContext _context;

        public BankController(BankContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<PageListResult<BankBranch>> GetAll([FromQuery] BankFilter filter, int pageNumber = 1, int pageSize = 10)
        {
            var banks = _context.BankBranches.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                banks = banks.Where(b => b.LocationName.Contains(filter.SearchTerm));
            }

            var pagedBanks = banks.ToPageList(pageNumber, pageSize);

            return Ok(pagedBanks);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<BankBranch> GetBankBranch(int id)
        {
            var bankBranch = _context.BankBranches.FirstOrDefault(b => b.Id == id);

            if (bankBranch == null)
            {
                return NotFound();
            }

            bankBranch.Employees = _context.Employees.Where(e => e.BankBranchId == id).ToList();

            return bankBranch;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddBankRequest req)
        {
            _context.BankBranches.Add(new BankBranch()
            {
                LocationName = req.Location,
                LocationURL = req.LocationURL,
                BranchManager = ""
            });
            _context.SaveChanges();

            return Created();
        }
        [HttpPost("delete")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete()
        {
            return Ok();
        }


}

    public class BankFilter
    {
        public string? SearchTerm { get; set; }
    }

    public static class PagingExtensions
    {
        public static PageListResult<T> ToPageList<T>(this IQueryable<T> query, int pageNumber = 1, int pageSize = 50)
        {
            int totalRecords = query.Count();
            var pagedData = query.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            return new PageListResult<T>
            {
                Data = pagedData,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                TotalRecords = totalRecords
            };
        }
    }

    public class PageListResult<T>
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}