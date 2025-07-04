using System;
using System.Collections.Generic;

namespace ProjectPRN222.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public int CategoryId { get; set; }

    public bool Status { get; set; }

    public string Thumbnail { get; set; } = null!;

    public string Tagline { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int AccountId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<LessonTopic> LessonTopics { get; set; } = new List<LessonTopic>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
