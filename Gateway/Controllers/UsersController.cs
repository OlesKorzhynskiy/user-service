using System;
using System.Threading.Tasks;
using AutoMapper;
using Confluent.Kafka;
using Gateway.Contracts.UserService;
using Gateway.UserService.Adapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Command.Contracts;

namespace Gateway.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        private readonly IUserServiceAdapter _userServiceAdapter;

        public UsersController(IMapper mapper, ILogger<UsersController> logger, IUserServiceAdapter userServiceAdapter)
        {
            _mapper = mapper;
            _logger = logger;
            _userServiceAdapter = userServiceAdapter;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        [HttpPost("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(CreateUserRequest request)
        {
            _logger.LogInformation($"Creating user");

            try
            {
                var command = _mapper.Map<CreateUser>(request);
                await _userServiceAdapter.Create(command);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        [HttpPut("users/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid userId, UpdateUserRequest request)
        {
            _logger.LogInformation($"Updating user with id {userId}");

            try
            {
                var command = _mapper.Map<UpdateUser>(request);
                await _userServiceAdapter.Update(command);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAll()
        {
            _logger.LogInformation($"Getting all users");

            try
            {
                var users = await _userServiceAdapter.GetAll();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}