using System;
using System.Collections.Generic;

namespace ProjectPRN222.Models;

public partial class TokenForgetPassword
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiryTime { get; set; }

    public bool IsUsed { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}
