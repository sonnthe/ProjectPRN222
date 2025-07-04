using System;
using System.Collections.Generic;

namespace ProjectPRN222.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool Gender { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    public int RoleId { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
