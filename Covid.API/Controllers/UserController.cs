using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid.Models.DTO;
using Covid.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Covid.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<AdminController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        #region Subscription

        [HttpGet]
        [Route("subscribe")]
        public IActionResult Subscribe()
        {
            var result = _userService.Subscribe();

            if (result.Exception == null)
            {
                return new JsonResult(new UserDTO { Key = result.Value.Key });
            }
            else
            {
                return new JsonResult(null);
            }
        }

        [HttpDelete]
        [Route("unsubscribe")]
        public IActionResult Unsubscribe([FromBody] UserDTO user)
        {
            var result = _userService.Unsubcribe(user.Key);

            if (result.Exception == null)
            {
                return new JsonResult(result.Value);
            }
            else
            {
                return new JsonResult(null);
            }
        }

        #endregion

        #region Matches

        [HttpGet]
        [Route("matches")]
        public IActionResult PullMatches([FromBody] UserDTO user)
        {
            var result = _userService.PullMatches(user.Key);

            if (result.Exception == null)
            {
                return new JsonResult(result.Value.Select(x => new MatchDTO { UserKey = x.UserX.Key, MatchedKey = x.UserY.Key, When = x.When }));
            }
            else
            {
                return new JsonResult(null);
            }
        }

        [HttpPost]
        [Route("match")]
        public IActionResult PushMatch([FromBody] MatchDTO match)
        {
            var result = _userService.PushMatch(match.UserKey, match.MatchedKey, match.When);

            if (result.Exception == null)
            {
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(null);
            }
        }

        #endregion

        #region - Alerts -

        [HttpGet]
        [Route("alerts")]
        public IActionResult PullAlerts([FromBody] UserDTO user)
        {
            var result = _userService.PullAlerts(user.Key);

            if (result.Exception == null)
            {
                return new JsonResult(result.Value.Select(x => new AlertDTO { UserKey = x.User.Key, When = x.When }));
            }
            else
            {
                return new JsonResult(null);
            }
        }

        [HttpPost]
        [Route("alert")]
        public IActionResult PushAlert([FromBody] AlertDTO alert)
        {
            var result = _userService.PushAlert(alert.UserKey, alert.When);

            if (result.Exception == null)
            {
                return new JsonResult(result.Value);
            }
            else
            {
                return new JsonResult(null);
            }
        }

        [HttpDelete]
        [Route("alert")]
        public IActionResult RemoveAlert([FromBody] UserDTO user)
        {
            var result = _userService.RemoveAlert(user.Key);

            if (result.Exception == null)
            {
                return new JsonResult(result.Value);
            }
            else
            {
                return new JsonResult(null);
            }
        }

        #endregion

        #region - Infections -

        [HttpGet]
        [Route("infections")]
        public IActionResult PullInfections([FromBody] InfectionDTO infection)
        {
            var result = _userService.PullInfections(infection.UserKey, infection.IncubationDays);

            if (result.Exception == null)
            {
                return new JsonResult(result.Value.ToArray());
            }
            else
            {
                return new JsonResult(null);
            }
        }
        
        #endregion
    }
}
