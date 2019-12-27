using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserService.Query.Contracts;
using UserService.Query.Services.Interfaces;

namespace UserService.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<UserReadModel>> GetAll()
        {
            return await _userService.GetAsync();
        }
    }
}