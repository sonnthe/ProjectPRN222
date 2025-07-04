using ProjectPRN222.Models;

namespace ProjectPRN222.DataAccessLayers.Interface
{
    public interface IRegistrationDA
    {
        bool IsRegistered(int accountId, int subjectId);
        bool IsBought(int accountId, int subjectId);
        List<Subject> GetBoughtSubjects(int accountId);
        List<Subject> GetRegisteredSubjects(int accountId);
        bool ConfirmPurchaseRegisteredSubject(string orderInfo);
        bool PurchaseSubject(string orderInfo);
        bool RegisterSubject(int accountId, int subjectId, int packageId);
        bool RemoveRegistration(int accountId, int subjectId);
    }
}
