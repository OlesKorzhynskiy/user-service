using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using UserService.Contracts.Commands;
using UserService.Mediator.Dispatcher;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public UsersController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
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

        [HttpPut]
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