using Microsoft.AspNetCore.Mvc;

namespace Team3.ThePollProject.Controllers
{
    public class PollController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
