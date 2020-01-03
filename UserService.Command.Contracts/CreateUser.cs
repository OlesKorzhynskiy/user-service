using System;

namespace UserService.Command.Contracts
{
    public class CreateUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}