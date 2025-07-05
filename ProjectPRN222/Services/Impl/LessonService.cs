using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;

namespace ProjectPRN222.Services.Impl
{
    public class LessonService(ILessonDA lessonDA) : ILessonService
    {
        private readonly ILessonDA _lessonDA = lessonDA;

        public bool AddLesson(Lesson lesson)
        {
            return _lessonDA.AddLesson(lesson);
        }

        public bool AddLessonTopic(LessonTopic lessonTopic)
        {
            return _lessonDA.AddLessonTopic(lessonTopic);
        }

        public bool DeleteLesson(int lessonId)
        {
            return _lessonDA.DeleteLesson(lessonId);
        }

        public bool DeleteLessonTopic(int lessonTopicId)
        {
            return _lessonDA.DeleteLessonTopic(lessonTopicId);
        }

        public int GetAllLessonCount()
        {
            return _lessonDA.GetAllLessonCount();
        }

        public List<Lesson> GetAllLessonsBySubjectId(int subjectId, int lesson_topic_id)
        {
            return _lessonDA.GetAllLessonsBySubjectId(subjectId, lesson_topic_id);
        }

        public Lesson GetLessonById(int lessonId)
        {
            return _lessonDA.GetLessonById(lessonId);
        }

        public LessonTopic GetLessonTopicById(int lessonTopicId)
        {
            return _lessonDA.GetLessonTopicById(lessonTopicId);
        }

        public List<LessonTopic> GetLessonTopicsBySubjectId(int subjectId)
        {
            return _lessonDA.GetLessonTopicsBySubjectId(subjectId);
        }

        public bool UpdateLesson(Lesson lesson)
        {
            return _lessonDA.UpdateLesson(lesson);
        }

        public bool UpdateLessonTopic(LessonTopic lessonTopic)
        {
            return _lessonDA.UpdateLessonTopic(lessonTopic);
        }
    }
}
