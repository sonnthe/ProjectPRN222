using Microsoft.EntityFrameworkCore;
using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;
using System.Diagnostics;

namespace ProjectPRN222.DataAccessLayers.Impl
{
    public class RegistrationDAO : IRegistrationDA
    {
        private QuizOnlineContext _context = new QuizOnlineContext();
        public List<Subject> GetBoughtSubjects(int accountId)
        {
            return [.. _context.Registrations
                .Include(r => r.Subject)
                    .ThenInclude(s => s.Packages)
                .Include(r => r.Subject)
                    .ThenInclude(s => s.Account)
                .Where(r => r.AccountId == accountId && r.Status.StatusName == "Paid")
                .Select(r => r.Subject)
                .Distinct()];
        }

        public bool IsBought(int accountId, int subjectId)
        {
            return _context.Registrations
                .Include(r => r.Status)
                .Any(r => r.AccountId == accountId && r.SubjectId == subjectId && r.Status.StatusName!.Equals("Paid"));
        }

        public bool IsRegistered(int accountId, int subjectId)
        {
            return _context.Registrations
                .Include(r => r.Status)
                 .Any(r => r.AccountId == accountId && r.SubjectId == subjectId && r.Status.StatusId==2);
        }

        public bool PurchaseSubject(string orderInfo)
        {
            Registration registration = HandleOrderInfo(orderInfo);
            if (registration.SubjectId != 0)
            {
                _context.Registrations.Add(registration);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ConfirmPurchaseRegisteredSubject(string orderInfo)
        {
            string[] parts = orderInfo.Split("|");
            int subjectId = int.Parse(parts[0]);
            int accountId = int.Parse(parts[1]);
            Registration registration = _context.Registrations.Where(r => r.SubjectId == subjectId && r.AccountId == accountId && r.StatusId == 2).FirstOrDefault()??new();
            if (registration.RegistrationId != 0)
            {
                registration.StatusId = 3; 
                _context.Registrations.Update(registration);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        private static Registration HandleOrderInfo(string orderInfo)
        {
            string[] parts = orderInfo.Split("|");
            int subjectId;
            int accountId;
            float salePrice;
            int packageId;
  
                subjectId = int.Parse(parts[0]);
                accountId = int.Parse(parts[1]);
                salePrice = float.Parse(parts[2]);
                packageId = int.Parse(parts[3]);
            Registration registration = new()
            {
                RegistrationTime = DateTime.Now,
                SubjectId = subjectId,
                PackageId = packageId,
                Cost = 0,
                ValidFrom = DateOnly.FromDateTime(DateTime.Now),
                ValidTo = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                AccountId = accountId,
                StatusId = 3,
                SalePrice = salePrice,
                Note = "Payment successful via VNPAY "
            };
            return registration;

        }

        public List<Subject> GetRegisteredSubjects(int accountId)
        {
            return [.. _context.Registrations
                .Include(r => r.Subject)
                    .ThenInclude(s => s.Packages)
                .Include(r => r.Subject)
                    .ThenInclude(s => s.Account)
                .Where(r => r.AccountId == accountId && r.Status.StatusName == "Pending")
                .Select(r => r.Subject)
                .Distinct()];
        }

        public bool RegisterSubject(int accountId, int subjectId, int packageId)
        {
            Registration registration = new Registration
            {
                AccountId = accountId,
                SubjectId = subjectId,
                Cost= 0,
                PackageId = packageId,
                RegistrationTime = DateTime.Now,
                StatusId = 2, 
                ValidFrom = DateOnly.FromDateTime(DateTime.Now),
                ValidTo = DateOnly.FromDateTime(DateTime.Now.AddDays(30)), 
                Note = "Registered subject"
            };

                _context.Registrations.Add(registration);
                _context.SaveChanges();
                return true;
        }

        public bool RemoveRegistration(int accountId, int subjectId)
        {
            Registration? registration = _context.Registrations
                .FirstOrDefault(r => r.AccountId == accountId && r.SubjectId == subjectId && r.StatusId==2);
            if (registration != null)
            {
                registration.StatusId = 1;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }

    }
