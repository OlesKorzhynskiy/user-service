using System;

namespace UserService.Query.Contracts
{
    public class UserReadModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}