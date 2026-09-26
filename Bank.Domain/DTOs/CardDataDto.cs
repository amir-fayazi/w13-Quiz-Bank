using System;
using System.Collections.Generic;
using System.Text;

namespace w13_Quiz.DTOs
{
    public class CardDataDto
    {
        public string CardNumber { get; set; } = null!;
        public string HolderName { get; set; } = null!;
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; } = null!;
        public int FailedPasswordAttempts { get; set; }
    }
}
