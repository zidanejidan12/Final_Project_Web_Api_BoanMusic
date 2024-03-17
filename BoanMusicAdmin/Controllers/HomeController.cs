using BoanMusicAdmin.Models;
using BoanMusicApp.BLL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace BoanMusicAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TrackBLL _trackBLL;

        public HomeController(ILogger<HomeController> logger, TrackBLL trackBLL)
        {
            _logger = logger;
            _trackBLL = trackBLL;
        }

        public IActionResult Index(string searchQuery, int? page)
        {
            // Get all tracks or search based on the query
            var tracks = string.IsNullOrEmpty(searchQuery)
                ? _trackBLL.GetTracks()
                : _trackBLL.SearchTracks(searchQuery);

            // Convert the list of tracks to a paged list
            int pageSize = 10; // You can set your desired page size here
            int pageNumber = page ?? 1; // If no page number is specified, default to the first page
            var pagedTracks = tracks.ToPagedList(pageNumber, pageSize);

            return View(pagedTracks);
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
