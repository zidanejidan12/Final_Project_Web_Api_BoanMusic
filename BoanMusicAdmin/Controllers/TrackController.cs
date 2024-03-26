using BoanMusicApp.BO;
using BoanMusicApp.BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace BoanMusicApp.Controllers
{
    [Route("[controller]")]
    public class TrackController : Controller
    {
        private readonly TrackBLL _trackBLL;
        private readonly ArtistBLL _artistBLL;

        public TrackController(TrackBLL trackBLL, ArtistBLL artistBLL)
        {
            _trackBLL = trackBLL;
            _artistBLL = artistBLL;
        }

        [HttpGet]
        public IActionResult GetTracks(int? page)
        {
            const int pageSize = 10;
            int pageNumber = page ?? 1;

            List<Track> allTracks = _trackBLL.GetTracks();
            IPagedList<Track> tracks = allTracks.ToPagedList(pageNumber, pageSize);

            return View("Index", tracks); // Ensure that the view name matches your Index.cshtml file
        }

        [HttpGet("Search")]
        public IActionResult Search(string searchQuery, int? page)
        {
            var tracks = _trackBLL.SearchTracks(searchQuery);

            // Convert the list of tracks to a paged list
            int pageSize = 10; // You can set your desired page size here
            int pageNumber = page ?? 1; // If no page number is specified, default to the first page
            var pagedTracks = tracks.ToPagedList(pageNumber, pageSize);

            return View(pagedTracks);
        }

        [HttpGet("AddNew")]
        public IActionResult AddNewTrack()
        {
            var viewModel = new ArtistDTO
            {
                Artists = _artistBLL.GetAllArtists()
            };
            return View(viewModel);
        }

        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            var track = _trackBLL.GetTrackById(id);

            if (track == null)
            {
                return NotFound(); // Return 404 if the track is not found
            }

            return View(track); // Pass the track object to the Details.cshtml view
        }

        [HttpPost("AddNew")]
        public IActionResult AddNewTrackPost(Track track)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _trackBLL.AddNewTrack(track);
                    return RedirectToAction("Index", "Home"); // Redirect to the home page after adding the track
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while adding the track. Please try again.");
                    // Log the exception or handle it appropriately
                }
            }
            // If ModelState is not valid, return to the same view with validation errors
            return View("AddNew", track);
        }
    }
}
