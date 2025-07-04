using System;
using System.Collections.Generic;

namespace ProjectPRN222.Models;

public partial class Lesson
{
    public int LessonId { get; set; }

    public string LessonName { get; set; } = null!;

    public int LessonOrder { get; set; }

    public string Summary { get; set; } = null!;

    public bool Status { get; set; }

    public int SubjectId { get; set; }

    public int LessonTopicId { get; set; }

    public string LessonContent { get; set; } = null!;

    public virtual LessonTopic LessonTopic { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
