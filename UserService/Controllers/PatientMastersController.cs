using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.DBContext;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMastersController : ControllerBase
    {
        private readonly DBDataLoaderPortalContext _context;

        public PatientMastersController(DBDataLoaderPortalContext context)
        {
            _context = context;
        }

        // GET: api/PatientMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientMaster>>> GetPatientMasters()
        {
            return await _context.PatientMasters.OrderByDescending(x => x.Id).ToListAsync();
        }

        // GET: api/PatientMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientMaster>> GetPatientMaster(int id)
        {
            var patientMaster = await _context.PatientMasters.FindAsync(id);

            if (patientMaster == null)
            {
                return NotFound();
            }

            return patientMaster;
        }

        // PUT: api/PatientMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPatientMaster(int id, PatientMaster patientMaster)
        //{
        //    if (id != patientMaster.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(patientMaster).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PatientMasterExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/PatientMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PatientMaster>> PostPatientMaster(PatientMaster patientMaster)
        {
            _context.PatientMasters.Add(patientMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatientMaster", new { id = patientMaster.Id }, patientMaster);
        }

        // DELETE: api/PatientMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientMaster(int id)
        {
            var patientMaster = await _context.PatientMasters.FindAsync(id);
            if (patientMaster == null)
            {
                return NotFound();
            }

            _context.PatientMasters.Remove(patientMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientMaster(int id, PatientMasterModel patientMaster)
        {

            if (id != patientMaster.Id)
            {
                return BadRequest();
            }

            bool retVal = false;
            try
            {
                var patient = new PatientMaster()
                {
                    Id = id,
                    Address1 = patientMaster.Address1,
                    Address2 = patientMaster.Address2,
                    Address3 = patientMaster.Address3,
                    District = patientMaster.District,
                    State = patientMaster.State,
                    Country = patientMaster.Country,
                    Dob = patientMaster.Dob,
                    EmailId = patientMaster.EmailId,
                    PhoneNumber = patientMaster.PhoneNumber
                };


                using (var db = new DBDataLoaderPortalContext())
                {
                    db.PatientMasters.Attach(patient);
                    db.Entry(patient).Property(x => x.Address1).IsModified = true;
                    db.Entry(patient).Property(x => x.Address2).IsModified = true;
                    db.Entry(patient).Property(x => x.Address3).IsModified = true;
                    db.Entry(patient).Property(x => x.District).IsModified = true;
                    db.Entry(patient).Property(x => x.State).IsModified = true;
                    db.Entry(patient).Property(x => x.Country).IsModified = true;
                    db.Entry(patient).Property(x => x.Dob).IsModified = true;
                    db.Entry(patient).Property(x => x.EmailId).IsModified = true;
                    db.Entry(patient).Property(x => x.PhoneNumber).IsModified = true;
                    db.SaveChanges();

                    return Ok();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientMasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }


        [HttpPut()]
        [Route("UpdateStatus")]
        public async Task<IActionResult> UpdateStatusPatientMaster(int id,string status)
        {

            if (id < 1)
            {
                return BadRequest();
            }

            bool retVal = false;
            try
            {
                var patient = new PatientMaster()
                {
                    Id = id,
                    Status = status
                };


                using (var db = new DBDataLoaderPortalContext())
                {
                    db.PatientMasters.Attach(patient);
                    db.Entry(patient).Property(x => x.Status).IsModified = true;
                    db.SaveChanges();

                    return Ok();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientMasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        [HttpGet()]
        [Route("GetPatientDetailsByName")]
        public  PatientMaster GetPatientMaster(string name)
        {
            var patientMaster =  _context.PatientMasters.Where(x => x.PatientName == name || x.EmailId == name).FirstOrDefault();

            if (patientMaster == null)
            {
                return null;
            }

            return patientMaster;
        }

        [HttpGet()]
        [Route("GetPatientDetailsByNameOrEmail")]
        public List<PatientMaster> GetPatientDetailsByNameOrEmail(string searchValue)
        {
            var patientMaster = _context.PatientMasters.Where(x => x.PatientName == searchValue || x.EmailId == searchValue).ToList();

            if (patientMaster == null)
            {
                return null;
            }

            return patientMaster;
        }

        private bool PatientMasterExists(int id)
        {
            return _context.PatientMasters.Any(e => e.Id == id);
        }
    }
}
