using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid.Models.DTO;
using Covid.Models.Entities;
using Covid.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Covid.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly UserService _userService;

        public AdminController(ILogger<AdminController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Route("users")]
        public IActionResult PullUsers()
        {
            var result = _userService.AdminPullUsers();

            if (result.Exception == null)
            {
                return new JsonResult(result.Value.Select(x => new UserDTO { Key = x.Key }));
            }
            else
            {
                return new JsonResult(null);
            }
        }

        [HttpGet]
        [Route("matches")]
        public IActionResult PullMatches()
        {
            var result = _userService.AdminPullMatches();

            if (result.Exception == null)
            {
                return new JsonResult(result.Value.Select(x => new MatchDTO { UserKey = x.UserX.Key, MatchedKey = x.UserY.Key, When = x.When }));
            }
            else
            {
                return new JsonResult(null);
            }
        }

        [HttpGet]
        [Route("alerts")]
        public IActionResult PullAlerts()
        {
            var result = _userService.AdminPullAlerts();

            if (result.Exception == null)
            {
                return new JsonResult(result.Value.Select(x => new AlertDTO { UserKey = x.User.Key, When = x.When }));
            }
            else
            {
                return new JsonResult(null);
            }
        }


    }
}
