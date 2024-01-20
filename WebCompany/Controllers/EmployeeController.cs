using Microsoft.AspNetCore.Mvc;

namespace WebCompany.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
