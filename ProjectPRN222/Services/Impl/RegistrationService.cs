using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;

namespace ProjectPRN222.Services.Impl
{
    public class RegistrationService(IRegistrationDA registrationDA) : IRegistrationService
    {
        private readonly IRegistrationDA _registrationDA = registrationDA;

        public List<Subject> GetBoughtSubjects(int accountId)
        {
           return _registrationDA.GetBoughtSubjects(accountId);
        }

        public bool IsBought(int accountId, int subjectId)
        {
            return _registrationDA.IsBought(accountId, subjectId);
        }

        public bool IsRegistered(int accountId, int subjectId)
        {
            return _registrationDA.IsRegistered(accountId, subjectId);
        }

        public bool PurchaseSubject(string orderInfo)
        {
            return _registrationDA.PurchaseSubject(orderInfo);
        }

        public bool ConfirmPurchaseRegisteredSubject(string orderInfo)
        {
            return _registrationDA.ConfirmPurchaseRegisteredSubject(orderInfo);
        }

        public List<Subject> GetRegisteredSubjects(int accountId)
        {
            return _registrationDA.GetRegisteredSubjects(accountId);
        }

        public bool RegisterSubject(int accountId, int subjectId, int packageId)
        {
            return _registrationDA.RegisterSubject(accountId, subjectId, packageId);
        }

        public bool RemoveRegistration(int accountId, int subjectId)
        {
            return _registrationDA.RemoveRegistration(accountId, subjectId);
        }
    }
}
