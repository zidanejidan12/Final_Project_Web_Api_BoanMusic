using BoanMusicApp.BO;
using BoanMusicApp.BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace BoanMusicApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackController : ControllerBase
    {
        private readonly TrackBLL _trackBLL;

        public TrackController(TrackBLL trackBLL)
        {
            _trackBLL = trackBLL;
        }

        [HttpGet]
        public IActionResult GetTracks()
        {
            var tracks = _trackBLL.GetTracks();
            return Ok(tracks);
        }

        [HttpGet("Search")]
        [Authorize]
        public IActionResult Search(string searchQuery)
        {
            var tracks = _trackBLL.SearchTracks(searchQuery);
            return Ok(tracks);
        }
    }
}
