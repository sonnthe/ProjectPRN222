using ProjectPRN222.Models;

namespace ProjectPRN222.Services.Interface
{
    public interface ISubjectService
    {
        List<Subject> GetAllSubjects();
        List<Subject> GetAllSubjectsForAdmin();
        Subject GetSubjectById(int id);
        bool AddSubject(Subject subject);
        bool UpdateSubject(Subject subject);
        bool DeleteSubject(int id);
        int GetAllSubjectCount();
        int GetLatestSubjectId();
    }
}
