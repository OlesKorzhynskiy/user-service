using System;

namespace UserService.Contracts.Commands
{
    public class UpdateUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}