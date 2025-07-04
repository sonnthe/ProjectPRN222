using System;
using System.Collections.Generic;

namespace ProjectPRN222.Models;

public partial class RegistrationStatus
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
