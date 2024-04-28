using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    [Route("api/bank")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly BankContext _context;

        public BankController(BankContext context)
        {
            _context = context;
        }


        [HttpGet]
        public List<BankBranch> GetAll()
        {
            return _context.BankBranches.ToList();
        }


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
    }
}
