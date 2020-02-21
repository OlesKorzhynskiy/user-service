using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Confluent.Kafka;
using Gateway.Contracts.UserService;
using Gateway.UserService.Adapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Command.Contracts;
using UserService.Query.Contracts;

namespace Gateway.Controllers
{
    /// <summary>
    /// Provides functionality for creating, updating, deleting and getting users.
    /// </summary>
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

            var command = _mapper.Map<CreateUser>(request);
            await _userServiceAdapter.CreateAsync(command);

            return NoContent();
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

            var command = _mapper.Map<UpdateUser>(request);
            command.Id = userId;

            await _userServiceAdapter.UpdateAsync(command);

            return NoContent();
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UserReadModel>>> GetAll()
        {
            _logger.LogInformation($"Getting all users");

            var users = await _userServiceAdapter.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [HttpGet("usersbygrpc")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllByGrpc()
        {
            _logger.LogInformation($"Getting all users");

            var response = await _userServiceAdapter.GetAllByGrpcAsync();
            return Ok(response.Users);
        }
    }
}