using System;

namespace UserService.Domain.UserAggregate
{
    public class User : BaseModel
    {
        public User(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}