using System;
using System.Collections.Generic;

namespace ProjectPRN222.Models;

public partial class LessonTopic
{
    public int LessonTopicId { get; set; }

    public string LessonTopicName { get; set; } = null!;

    public int SubjectId { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Subject Subject { get; set; } = null!;
}
