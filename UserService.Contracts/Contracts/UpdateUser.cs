﻿using System;

namespace UserService.Contracts.Contracts
{
    public class UpdateUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}