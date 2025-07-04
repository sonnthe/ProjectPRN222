using ProjectPRN222.DataAccessLayers.Interface;
using ProjectPRN222.Models;
using ProjectPRN222.Services.Interface;

namespace ProjectPRN222.Services.Impl
{
    public class PackageService(IPackageDA packageDA) : IPackageService
    {
        private readonly IPackageDA _packageDA = packageDA;

        public bool AddPackage(Package package)
        {
            return _packageDA.AddPackage(package);
        }

        public bool DeletePackage(int packageId)
        {
            return _packageDA.DeletePackage(packageId);
        }

        public List<Package> GetAllPackagesBySubjectId(int subjectId)
        {
            return _packageDA.GetAllPackagesBySubjectId(subjectId);
        }

        public Package GetPackageById(int packageId)
        {
           return _packageDA.GetPackageById(packageId);
        }

        public bool UpdatePackage(Package package)
        {
            return _packageDA.UpdatePackage(package);
        }
    }
}
