using System;
using System.Collections.Generic;

namespace ProjectPRN222.Models;

public partial class Registration
{
    public int RegistrationId { get; set; }

    public DateTime RegistrationTime { get; set; }

    public int SubjectId { get; set; }

    public int PackageId { get; set; }

    public int Cost { get; set; }

    public DateOnly ValidFrom { get; set; }

    public DateOnly ValidTo { get; set; }

    public int AccountId { get; set; }

    public int StatusId { get; set; }
    public float SalePrice { get; set; }

    public string Note { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual Package Package { get; set; } = null!;

    public virtual RegistrationStatus Status { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
