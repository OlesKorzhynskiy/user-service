using System;

namespace UserService.Command.Contracts
{
    public class UpdateUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}