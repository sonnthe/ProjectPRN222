using ProjectPRN222.Models;

namespace ProjectPRN222.Services.Interface
{
    public interface IPackageService
    {
        List<Package> GetAllPackagesBySubjectId(int subjectId);
        Package GetPackageById(int packageId);
        bool AddPackage(Package package);
        bool UpdatePackage(Package package);    
        bool DeletePackage(int packageId);
    }
}
