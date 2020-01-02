using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Command.Contracts;
using UserService.Mediator.Dispatcher;
using UserService.Query.Client;

namespace Gateway.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly IUserServiceWebClient _userServiceWebClient;

        public UsersController(IDispatcher dispatcher, IUserServiceWebClient userServiceWebClient)
        {
            _dispatcher = dispatcher;
            _userServiceWebClient = userServiceWebClient;
        }

        /// <summary>
        /// Create a new appointment
        /// </summary>
        [HttpPost("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(string name)
        {
            try
            {
                var command = new CreateUser()
                {
                    Name = name
                };

                await _dispatcher.Dispatch(command, Handler);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update an existing appointment
        /// </summary>
        [HttpPut("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid id, string name)
        {
            try
            {
                var command = new UpdateUser()
                {
                    Id = id,
                    Name = name
                };

                await _dispatcher.Dispatch(command, Handler);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get all appointments
        /// </summary>
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var users = await _userServiceWebClient.GetAll();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        private void Handler(DeliveryReport<string, string> r)
        {
            if (r.Error.IsError)
            {
                throw new Exception(r.Error.Reason);
            }

            Console.WriteLine($"Delivered message to {r.TopicPartitionOffset}");
        }
    }
}