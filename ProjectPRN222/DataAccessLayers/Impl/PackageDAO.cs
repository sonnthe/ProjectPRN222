using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;

namespace ProjectPRN222.DataAccessLayers.Impl
{
    public class PackageDAO : IPackageDA
    {
        private readonly QuizOnlineContext _context =new();

        public bool AddPackage(Package package)
        {
            _context.Packages.Add(package);
            return _context.SaveChanges() > 0;
        }

        public bool DeletePackage(int packageId)
        {
            _context.Packages.Remove(_context.Packages.FirstOrDefault(p => p.PackageId == packageId)!);
            return _context.SaveChanges() > 0;
        }

        public List<Package> GetAllPackagesBySubjectId(int subjectId)
        {
            return [.. _context.Packages.Where(p => p.SubjectId == subjectId)];
        }

        public Package GetPackageById(int packageId)
        {
            return _context.Packages.FirstOrDefault(p => p.PackageId == packageId) ?? new();
        }

        public bool IsPackageExist(Package package)
        {
            return _context.Packages.Any(p => p.PackageName == package.PackageName && p.SubjectId == package.SubjectId);
        }

        public bool UpdatePackage(Package package)
        {
            var existingPackage = _context.Packages.FirstOrDefault(p => p.PackageId == package.PackageId);
            existingPackage!.PackageName = package.PackageName;
            existingPackage.Duration = package.Duration;
            existingPackage.SalePrice = package.SalePrice;
            return _context.SaveChanges() > 0;
        }
    }
}
