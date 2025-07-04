using Microsoft.EntityFrameworkCore;
using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;

namespace ProjectPRN222.DataAccessLayers.Impl
{
    public class LessonDAO : ILessonDA
    {
         private readonly QuizOnlineContext _context = new();

        public bool AddLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            return _context.SaveChanges() > 0;
        }

        public bool AddLessonTopic(LessonTopic lessonTopic)
        {
            _context.LessonTopics.Add(lessonTopic);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteLesson(int lessonId)
        {
            _context.Lessons.Remove(_context.Lessons.FirstOrDefault(l => l.LessonId == lessonId)!);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteLessonTopic(int lessonTopicId)
        {
            _context.LessonTopics.Remove(_context.LessonTopics.FirstOrDefault(lt => lt.LessonTopicId == lessonTopicId)!);
            return _context.SaveChanges() > 0;
        }

        public List<Lesson> GetAllLessonsBySubjectId(int subjectId, int lesson_topic_id)
        {
            return [.. _context.Lessons
                .Where(l => l.SubjectId == subjectId)
                .Include(l => l.Subject)
                .Include(l => l.LessonTopic)
                .Where(l => l.LessonTopicId == lesson_topic_id)];
        }

        public Lesson GetLessonById(int lessonId)
        {
            return _context.Lessons
                 .Include(l => l.Subject)
                 .Include(l => l.LessonTopic)
                 .FirstOrDefault(l => l.LessonId == lessonId) ??  new ();
        }

        public LessonTopic GetLessonTopicById(int lessonTopicId)
        {
            return _context.LessonTopics
                .Include(lt => lt.Subject)
                .Include(lt => lt.Lessons)
                .FirstOrDefault(lt => lt.LessonTopicId == lessonTopicId) ??  new ();
        }

        public List<LessonTopic> GetLessonTopicsBySubjectId(int subjectId)
        {
            return [.. _context.LessonTopics
                .Where(lt => lt.SubjectId == subjectId)
                .Include(lt => lt.Lessons)
                .Include(lt => lt.Subject)];
        }

        public bool IsLessonExists(Lesson lesson)
        {
            return _context.Lessons
                .Any(l => l.LessonName == lesson.LessonName && l.SubjectId == lesson.SubjectId && l.LessonTopicId == lesson.LessonTopicId);
        }

        public bool IsLessonTopicExists(LessonTopic lessonTopic)
        {
            return _context.LessonTopics
                .Any(lt => lt.LessonTopicName == lessonTopic.LessonTopicName && lt.SubjectId == lessonTopic.SubjectId);
        }

        public bool UpdateLesson(Lesson lesson)
        {
            var existingLesson = _context.Lessons.FirstOrDefault(l => l.LessonId == lesson.LessonId);
            if (existingLesson == null)
            {
                throw new KeyNotFoundException("Lesson not found");
            }
            existingLesson.LessonName = lesson.LessonName;
            existingLesson.LessonOrder = lesson.LessonOrder;
            existingLesson.Summary = lesson.Summary;
            existingLesson.LessonContent = lesson.LessonContent;
            return _context.SaveChanges() > 0;

        }

        public bool UpdateLessonTopic(LessonTopic lessonTopic)
        {
            var existingLessonTopic = _context.LessonTopics.FirstOrDefault(lt => lt.LessonTopicId == lessonTopic.LessonTopicId);
            if (existingLessonTopic == null)
            {
                throw new KeyNotFoundException("Lesson topic not found");
            }
            existingLessonTopic.LessonTopicName = lessonTopic.LessonTopicName;
            return _context.SaveChanges() > 0;
        }
    }
}
