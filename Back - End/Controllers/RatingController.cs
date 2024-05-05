using Microsoft.AspNetCore.Mvc;

namespace Team3.ThePollProject.Controllers
{
    public class RatingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
