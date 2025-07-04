using System;
using System.Collections.Generic;

namespace ProjectPRN222.Models;

public partial class Package
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public int Duration { get; set; }

    public float SalePrice { get; set; }

    public bool Status { get; set; }

    public int SubjectId { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public virtual Subject Subject { get; set; } = null!;
}
