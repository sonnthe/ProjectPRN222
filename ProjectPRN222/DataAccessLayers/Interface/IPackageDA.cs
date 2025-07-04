using ProjectPRN222.Models;

namespace ProjectPRN222.DataAccessLayers.Interface
{
    public interface IPackageDA
    {
        List<Package> GetAllPackagesBySubjectId(int subjectId);
        Package GetPackageById(int packageId);
        bool AddPackage(Package package);
        bool IsPackageExist(Package package);
        bool UpdatePackage(Package package);
        bool DeletePackage(int packageId);

    }
}
