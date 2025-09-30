using Company.Pro.PL.Models;
using Company.Pro.PL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Company.Pro.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScooped scooped;
        private readonly IScooped scooped01;
        private readonly ITransiant trainsiant;
        private readonly ITransiant trainsiant01;
        private readonly ISingelton singelton;
        private readonly ISingelton singelton01;

        public HomeController(ILogger<HomeController> logger,
                              IScooped  scooped,
                              IScooped scooped01,
                              ITransiant trainsiant,
                              ITransiant trainsiant01,
                              ISingelton singelton,
                              ISingelton singelton01)
        {
            _logger = logger;
            this.scooped = scooped;
            this.scooped01 = scooped01;
            this.trainsiant = trainsiant;
            this.trainsiant01 = trainsiant01;
            this.singelton = singelton;
            this.singelton01 = singelton01;
        }

        public string TestLifTime()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"scooped :: {scooped.GetGuid()}");
            sb.AppendLine($"scooped01 :: {scooped01.GetGuid()}");
            sb.AppendLine($"trainsiant :: {trainsiant.GetGuid()}");
            sb.AppendLine($"trainsiant01 :: {trainsiant01.GetGuid()}");
            sb.AppendLine($"singelton :: {singelton.GetGuid()}");
            sb.AppendLine($"singelton01 :: {singelton01.GetGuid()}");
            return sb.ToString();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
