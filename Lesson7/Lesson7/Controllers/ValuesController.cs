using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lesson7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lesson7.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private  readonly PersonDBContext _context;

        public ValuesController(PersonDBContext context) {
            _context = context;
        }
        // GET api/values
     /*   [HttpGet]
        public ActionResult<IEnumerable<Employees>> Get()
        {
            return _context.Employees.ToList();
        }*/

        [HttpGet]
        public IEnumerable<Employees> GetEmployees()
        {
            return _context.Employees;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employees = await _context.Employees.SingleOrDefaultAsync(m => m.PersonelId == id);

            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpGet("{firstName}/{lastName}")]
        public async Task<IActionResult> Get([FromRoute] string firstName, string lastName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employees = await _context.Employees.SingleOrDefaultAsync(m => m.FirstName == firstName && m.LastName==lastName);

            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]Employees employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Add(employee);
            _context.SaveChanges();
            return Ok();

        }

        // PUT api/values/5
        [HttpPut]
        public ActionResult Put( [FromBody]Employees employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (employee == null)
            {
                return NotFound();
            }

            _context.Update(employee);
            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee =  _context.Employees.FirstOrDefault(m => m.PersonelId == id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChangesAsync();

            return Ok();
        }

    }
}
