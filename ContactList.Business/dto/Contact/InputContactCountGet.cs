﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Business
{
    public class InputContactCountGet
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
