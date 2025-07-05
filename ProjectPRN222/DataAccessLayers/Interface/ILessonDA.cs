using ProjectPRN222.Models;

namespace ProjectPRN222.DataAccessLayers.Interface
{
    public interface ILessonDA
    {
        List<Lesson> GetAllLessonsBySubjectId(int subjectId, int lesson_topic_id);
        int GetAllLessonCount();
        List<LessonTopic> GetLessonTopicsBySubjectId(int subjectId);
        Lesson GetLessonById(int lessonId);
        LessonTopic GetLessonTopicById(int lessonTopicId);
        bool AddLesson(Lesson lesson);
        bool UpdateLesson(Lesson lesson);
        bool DeleteLesson(int lessonId);
        bool IsLessonExists(Lesson lesson);
        bool AddLessonTopic(LessonTopic lessonTopic);
        bool UpdateLessonTopic(LessonTopic lessonTopic);
        bool DeleteLessonTopic(int lessonTopicId);
        bool IsLessonTopicExists(LessonTopic lessonTopic);
    }
}
