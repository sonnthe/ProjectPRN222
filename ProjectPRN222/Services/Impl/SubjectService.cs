using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;

namespace ProjectPRN222.Services.Impl
{
    public class SubjectService(ISubjectDA subjectDA) : ISubjectService
    {
        private readonly ISubjectDA _subjectDA = subjectDA;

        public bool AddSubject(Subject subject)
        {
            return _subjectDA.AddSubject(subject);
        }

        public bool DeleteSubject(int id)
        {
            return _subjectDA.DeleteSubject(id);
        }

        public List<Subject> GetAllSubjects()
        {
            return _subjectDA.GetAllSubjects();
        }

        public List<Subject> GetAllSubjectsForAdmin()
        {
            return _subjectDA.GetAllSubjectsForAdmin();
        }

        public int GetLatestSubjectId()
        {
            return _subjectDA.GetLatestSubjectId();
        }

        public Subject GetSubjectById(int id)
        {
            return _subjectDA.GetSubjectById(id);
        }

        public bool UpdateSubject(Subject subject)
        {
            return _subjectDA.UpdateSubject(subject);
        }
    }
}
