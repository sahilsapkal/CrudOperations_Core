using CrudOperations_EF.Data;
using CrudOperations_EF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperations_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudOperationController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public CrudOperationController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var emp = await _dbContext.SahilTable.ToListAsync();
            return Ok(emp);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid(int id)
        {
            var emp = _dbContext.SahilTable.FindAsync(id);
            if(emp == null) { return NotFound(); }
            return Ok(emp);

        }

        [HttpPost]
        public async Task<IActionResult> Create(SahilTable sahil)
        {
            await _dbContext.SahilTable.AddAsync(sahil);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByid), new {id = sahil.rowval}, sahil);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,SahilTable sahil)
        {
            if(id != sahil.rowval) {  return BadRequest(); }
            _dbContext.Entry(sahil).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sahil = await _dbContext.SahilTable.FindAsync(id);
            if (sahil == null) { return NotFound(); }
            _dbContext.SahilTable.Remove(sahil);
            await _dbContext.SaveChangesAsync();
            return NoContent();


        }
    }
}
