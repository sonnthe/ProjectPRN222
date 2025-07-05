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

        public bool DeleteSubject(int id)
        {
           _context.Subjects.Remove(_context.Subjects.FirstOrDefault(s => s.SubjectId == id)!);
            return _context.SaveChanges() > 0;

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

        public int GetAllSubjectsCount()
        {
            return _context.Subjects.Count(s => s.Status == true);
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
                FirstOrDefault(s => s.SubjectId == id) ??  new ();
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

        public bool UpdateSubject(Subject subject)
        {
            var existingSubject = _context.Subjects.FirstOrDefault(s => s.SubjectId == subject.SubjectId);
            if (existingSubject == null)
            {
                return false;
            }
            existingSubject.SubjectName = subject.SubjectName;
            existingSubject.CategoryId = subject.CategoryId;
            existingSubject.Thumbnail = subject.Thumbnail;
            existingSubject.Tagline = subject.Tagline;
            existingSubject.Description = subject.Description;
            existingSubject.AccountId = subject.AccountId;

            return _context.SaveChanges() > 0;
        }
    }
}
