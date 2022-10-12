using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserService.DBContext;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Services
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
    public class PatientMastersService : IPatientMastersService
    {
        private DBDataLoaderPortalContext _DBDataLoaderPortalContext;

        //private IMapper _mapper;

        public PatientMastersService(DBDataLoaderPortalContext dBDataLoaderPortalContext)
        {
            _DBDataLoaderPortalContext = dBDataLoaderPortalContext;
            //_mapper = mapper;
        }

        public List<PatientMaster> GetPatientList()
        {
            return _DBDataLoaderPortalContext.PatientMasters.ToList();
        }

        public bool PutPatientMaster(int id, PatientMasterModel patientMaster)
        {
            bool retVal = false;

            //var patient = new PatientMaster()
            //{
            //    Id = id,
            //    Address1 = patientMaster.Address1,
            //    Address2 = patientMaster.Address2,
            //    Address3 = patientMaster.Address3,
            //    District = patientMaster.District,
            //    State = patientMaster.State,
            //    Country = patientMaster.Country,
            //    Dob = patientMaster.Dob,
            //    EmailId = patientMaster.EmailId,
            //    PhoneNumber = patientMaster.PhoneNumber
            //};

            //using (var db = new DBDataLoaderPortalContext())
            //{
            //    db.PatientMasters.Attach(patient);
            //    db.Entry(patient).Property(x => x.Address1).IsModified = true;
            //    db.Entry(patient).Property(x => x.Address2).IsModified = true;
            //    db.Entry(patient).Property(x => x.Address3).IsModified = true;
            //    db.Entry(patient).Property(x => x.District).IsModified = true;
            //    db.Entry(patient).Property(x => x.State).IsModified = true;
            //    db.Entry(patient).Property(x => x.Country).IsModified = true;
            //    db.Entry(patient).Property(x => x.Dob).IsModified = true;
            //    db.Entry(patient).Property(x => x.EmailId).IsModified = true;
            //    db.Entry(patient).Property(x => x.PhoneNumber).IsModified = true;
            //    db.SaveChanges();

            //    retVal = true;
            //}

            var dbPatient = _DBDataLoaderPortalContext.PatientMasters
       .FirstOrDefault(p => p.Id.Equals(id));

            if(dbPatient == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            dbPatient.Address1 = patientMaster.Address1;
            dbPatient.Address2 = patientMaster.Address2;
            dbPatient.Address3 = patientMaster.Address3;
            dbPatient.District = patientMaster.District;
            dbPatient.State = patientMaster.State;
            dbPatient.Country = patientMaster.Country;
            dbPatient.Dob = patientMaster.Dob;
            dbPatient.EmailId = patientMaster.EmailId;
            dbPatient.PhoneNumber = patientMaster.PhoneNumber;


            var isAddress1Modified = _DBDataLoaderPortalContext.Entry(dbPatient).Property("Address1").IsModified;
            var isAddress2Modified = _DBDataLoaderPortalContext.Entry(dbPatient).Property("Address2").IsModified;
            var isAddress3Modified = _DBDataLoaderPortalContext.Entry(dbPatient).Property("Address3").IsModified;
            var isDistrictModified = _DBDataLoaderPortalContext.Entry(dbPatient).Property("District").IsModified;
            var isStateModified = _DBDataLoaderPortalContext.Entry(dbPatient).Property("State").IsModified;
            var isCountryModified = _DBDataLoaderPortalContext.Entry(dbPatient).Property("Country").IsModified;
            var isDobModified = _DBDataLoaderPortalContext.Entry(dbPatient).Property("Dob").IsModified;
            var isEmailIdModified = _DBDataLoaderPortalContext.Entry(dbPatient).Property("EmailId").IsModified;
            var isPhoneNumberModified = _DBDataLoaderPortalContext.Entry(dbPatient).Property("PhoneNumber").IsModified;
            _DBDataLoaderPortalContext.SaveChanges();
            retVal = true;

            return retVal;
        }

        public bool UpdatePatientMaster(int id, string status)
        {
            bool retVal = false;
            //var patient = new PatientMaster()
            //{
            //    Id = id,
            //    Status = status
            //};


            //using (var db = new DBDataLoaderPortalContext())
            //{
            //    db.PatientMasters.Attach(patient);
            //    db.Entry(patient).Property(x => x.Status).IsModified = true;
            //    db.SaveChanges();

            //    retVal = true;
            //}


            var dbPatient = _DBDataLoaderPortalContext.PatientMasters
       .FirstOrDefault(p => p.Id.Equals(id));

            if (dbPatient == null)
            {
                throw new DbUpdateConcurrencyException();
            }
            dbPatient.Status = status;

            var isStatusModified = _DBDataLoaderPortalContext.Entry(dbPatient).Property("Status").IsModified;
            _DBDataLoaderPortalContext.SaveChanges();
            retVal = true;

            return retVal;
        }
        public PatientMaster GetPatientMaster(string name)
        {
            return _DBDataLoaderPortalContext.PatientMasters.Where(x => x.PatientName == name).FirstOrDefault();
        }
        public List<PatientMaster> GetPatientDetailsByNameOrEmail(string searchValue)
        {
            return _DBDataLoaderPortalContext.PatientMasters.Where(x => x.PatientName == searchValue || x.EmailId == searchValue).ToList();
        }

        bool IPatientMastersService.PatientMasterExists(int id)
        {
            return _DBDataLoaderPortalContext.PatientMasters.Any(e => e.Id == id);
        }
    }
}
