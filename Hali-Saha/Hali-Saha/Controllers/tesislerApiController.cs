using DataAccess.Data;
using HaliSaha_Model.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hali_Saha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tesislerApiController : ControllerBase
    {

        private readonly DbHaliSahaContext context;
        public tesislerApiController(DbHaliSahaContext _context)
        {
            context = _context;
        }
        // GET: api/<tesislerApiController>
        [HttpGet]
        public IEnumerable<SporTesisi> Get()
        {
            return context.Tesisler.ToList();
        }

        // GET api/<tesislerApiController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = context.Tesisler.Where(p => p.TesisId == id).FirstOrDefault();
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(book);
            }
        }

        // POST api/<tesislerApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<tesislerApiController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SporTesisi newBook)
        {
            var bookName = newBook != null ? newBook.TesisAdi: "";
            if (newBook != null)
            {
                context.Tesisler.Add(newBook);
                context.SaveChanges();
            }
            return Ok(bookName);
        }

        // DELETE api/<tesislerApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
