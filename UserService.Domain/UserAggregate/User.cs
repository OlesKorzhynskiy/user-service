using System;

namespace UserService.Domain.UserAggregate
{
    public class User : AggregateRoot
    {
        public User(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}