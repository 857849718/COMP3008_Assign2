using Intermed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        [HttpGet("ShowAdminDashboard")]
        public IActionResult ShowAdminDashboard()
        {
            return PartialView("AdminDashboard");
        }
    }
}
