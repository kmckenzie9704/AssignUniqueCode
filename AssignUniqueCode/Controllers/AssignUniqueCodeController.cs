using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignUniqueCode.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace AssignUniqueCode.Controllers
{
    [Route("api/AssignUniqueCode")]
    public class AssignUniqueCodeController : Controller
    {
        private DatabaseContext _context;

        public AssignUniqueCodeController(DatabaseContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            string strUniqueCode = "Assign Unique Code to applicant.";
            return strUniqueCode;
        }

        // POST api/values
        [HttpPost]
        public bool Post([FromBody]Applicant applicant)
        {
            bool blnUniqueCodeIsValid = AssignCode(applicant.appUniqueCode, applicant.appEmail);

            return blnUniqueCodeIsValid;
        }

        private bool AssignCode(string strUniqueCode, string strEmail)
        {
            bool blnCodeUpdated = false;
            using (_context)
            {
                UniqueCode newCode = new UniqueCode();
                DateTime datToday = DateTime.Today;
                newCode.uniCode = strUniqueCode;
                newCode.uniEmail = strEmail;
                newCode.uniAssignDate = datToday;
                _context.UniqueCodes.Add(newCode);
                _context.SaveChanges();
                blnCodeUpdated = true;
            }
            return blnCodeUpdated;
        }
    }

}
