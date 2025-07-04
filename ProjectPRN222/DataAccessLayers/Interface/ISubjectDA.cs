using ProjectPRN222.Models;

namespace ProjectPRN222.DataAccessLayers.Interface
{
    public interface ISubjectDA
    {
        List<Subject> GetAllSubjects();
        List<Subject> GetAllSubjectsForAdmin();
        Subject GetSubjectById(int id);
        bool AddSubject(Subject subject);
        bool IsSubjectNameExists(Subject subject);
        bool UpdateSubject(Subject subject);
        bool DeleteSubject(int id);
        List<Subject> SearchSubjects(string keyword);
        List<Subject> GetSubjectsByCategoryId(int categoryId);
        List<Subject> GetSubjectsByStudentId(int studentId);
        int GetLatestSubjectId();
    }
}
