using Microsoft.EntityFrameworkCore;
using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;

namespace ProjectPRN222.DataAccessLayers.Impl
{
    public class SubjectDAO : ISubjectDA
    {
        private QuizOnlineContext _context =new QuizOnlineContext();

        public bool AddSubject(Subject subject)
        {
                _context.Subjects.Add(subject);
            return _context.SaveChanges() > 0;
        }

        public void DeleteSubject(int id)
        {
            throw new NotImplementedException();
        }

        public List<Subject> GetAllSubjects()
        {
            return [.. _context.Subjects.
                Include(s => s.Category).
                Include(s => s.Lessons).
                Include(s => s.Packages).
                Include(s=>s.Account).
                Include(s=>s.Registrations).
                ThenInclude(r=>r.Account).
                Include(s=>s.Registrations).
                ThenInclude(r=>r.Status)
                                .AsNoTracking()
                                .Where(s => s.Status == true)
                ];  
        }

        public List<Subject> GetAllSubjectsForAdmin()
        {
            return [.. _context.Subjects.
                Include(s => s.Category).
                Include(s => s.LessonTopics).
                Include(s => s.Lessons).
                Include(s => s.Packages).
                Include(s=>s.Account).
                Include(s=>s.Registrations).
                ThenInclude(r=>r.Account).
                Include(s=>s.Registrations).
                ThenInclude(r=>r.Status)
                                .AsNoTracking()
                ];
        }

        public int GetLatestSubjectId()
        {
            return _context.Subjects
                .OrderByDescending(s => s.SubjectId)
                .Select(s => s.SubjectId)
                .FirstOrDefault();
        }

        public Subject GetSubjectById(int id)
        {
            return _context.Subjects.
                Include(s => s.Category).
                Include(s => s.Account).
                Include(s => s.LessonTopics).
                Include(s => s.Lessons).
                FirstOrDefault(s => s.SubjectId == id) ?? throw new KeyNotFoundException($"Subject with ID {id} not found.");
        }

        public List<Subject> GetSubjectsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public List<Subject> GetSubjectsByStudentId(int studentId)
        {
            throw new NotImplementedException();
        }

        public bool IsSubjectNameExists(Subject subject)
        {
            return _context.Subjects
                .Any(s => s.SubjectName == subject.SubjectName && s.SubjectId != subject.SubjectId);
        }

        public List<Subject> SearchSubjects(string keyword)
        {
            throw new NotImplementedException();
        }

        public void UpdateSubject(Subject subject)
        {
            throw new NotImplementedException();
        }
    }
}
