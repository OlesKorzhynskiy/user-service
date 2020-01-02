using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Query.Contracts;
using UserService.Query.Services.Interfaces;

namespace UserService.Query.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAsync();
            
            return Ok(result);
        }
    }
}