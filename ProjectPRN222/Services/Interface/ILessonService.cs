using ProjectPRN222.Models;

namespace ProjectPRN222.Services.Interface
{
    public interface ILessonService
    {
        List<Lesson> GetAllLessonsBySubjectId(int subjectId, int lesson_topic_id);

        List<LessonTopic> GetLessonTopicsBySubjectId(int subjectId);
        Lesson GetLessonById(int lessonId);
        LessonTopic GetLessonTopicById(int lessonTopicId);

        bool AddLesson(Lesson lesson);
        bool AddLessonTopic(LessonTopic lessonTopic);
        bool UpdateLesson(Lesson lesson);
        bool UpdateLessonTopic(LessonTopic lessonTopic);
        bool DeleteLesson(int lessonId);
        bool DeleteLessonTopic(int lessonTopicId);
    }
}
