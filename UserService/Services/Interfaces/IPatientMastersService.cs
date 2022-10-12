using UserService.DBContext;
using UserService.Models;

namespace UserService.Services.Interfaces
{
    public interface IPatientMastersService
    {
        List<PatientMaster> GetPatientList();
        bool PutPatientMaster(int id, PatientMasterModel patientMaster);
        bool PatientMasterExists(int id);
        bool UpdatePatientMaster(int id, string status);
        PatientMaster GetPatientMaster(string name);
        List<PatientMaster> GetPatientDetailsByNameOrEmail(string searchValue);
    }
}
