using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.DBContext;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMastersController : ControllerBase
    {
        //private readonly DBDataLoaderPortalContext _context;
        private IPatientMastersService _PatientMastersService;

        public PatientMastersController(IPatientMastersService patientMasterService)
        {
            //_context = context;
            _PatientMastersService = patientMasterService;
        }

        // GET: api/PatientMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientMaster>>> GetPatientMasters()
        {
            return _PatientMastersService.GetPatientList();
        }

        //// GET: api/PatientMasters/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<PatientMaster>> GetPatientMaster(int id)
        //{
        //    var patientMaster = await _context.PatientMasters.FindAsync(id);

        //    if (patientMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    return patientMaster;
        //}

        //// PUT: api/PatientMasters/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        ////[HttpPut("{id}")]
        ////public async Task<IActionResult> PutPatientMaster(int id, PatientMaster patientMaster)
        ////{
        ////    if (id != patientMaster.Id)
        ////    {
        ////        return BadRequest();
        ////    }

        ////    _context.Entry(patientMaster).State = EntityState.Modified;

        ////    try
        ////    {
        ////        await _context.SaveChangesAsync();
        ////    }
        ////    catch (DbUpdateConcurrencyException)
        ////    {
        ////        if (!PatientMasterExists(id))
        ////        {
        ////            return NotFound();
        ////        }
        ////        else
        ////        {
        ////            throw;
        ////        }
        ////    }

        ////    return NoContent();
        ////}

        //// POST: api/PatientMasters
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<PatientMaster>> PostPatientMaster(PatientMaster patientMaster)
        //{
        //    _context.PatientMasters.Add(patientMaster);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPatientMaster", new { id = patientMaster.Id }, patientMaster);
        //}

        //// DELETE: api/PatientMasters/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePatientMaster(int id)
        //{
        //    var patientMaster = await _context.PatientMasters.FindAsync(id);
        //    if (patientMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.PatientMasters.Remove(patientMaster);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

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
                retVal = _PatientMastersService.PutPatientMaster(id, patientMaster);
                if (retVal)
                    return Ok();
                else
                    return BadRequest("Something went wrong.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_PatientMastersService.PatientMasterExists(id))
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
                retVal = _PatientMastersService.UpdatePatientMaster(id, status);
                if (retVal)
                    return Ok();
                else
                    return BadRequest("Something went wrong.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_PatientMastersService.PatientMasterExists(id))
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
        public async Task<PatientMaster> GetPatientMaster(string name)
        {
            var patientMaster = _PatientMastersService.GetPatientMaster(name);

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
            var patientMaster = _PatientMastersService.GetPatientDetailsByNameOrEmail(searchValue);

            if (patientMaster == null)
            {
                return null;
            }

            return patientMaster;
        }

        //private bool PatientMasterExists(int id)
        //{
        //    return _context.PatientMasters.Any(e => e.Id == id);
        //}
    }
}
