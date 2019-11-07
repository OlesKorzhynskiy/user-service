using System;

namespace UserService.Domain
{
    public class BaseModel
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int Version { get; set; }
    }
}