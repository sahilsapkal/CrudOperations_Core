using CrudOperations_EF.Data;
using CrudOperations_EF.Models;
using CrudOperations_EF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperations_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudOperationController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly CrudOperationsService _crudOperationsService;
        public CrudOperationController(AppDbContext dbContext, CrudOperationsService crudOperationsService)
        {
            _dbContext = dbContext;
            _crudOperationsService = crudOperationsService;
        }

        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            //var emp = await _dbContext.SahilTable.ToListAsync();
            var emp = await _crudOperationsService.GetAll();
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
        public async Task<IActionResult> Create(List<SahilTable> sahil)
        {
        //    await _dbContext.SahilTable.AddRangeAsync(sahil);
        //    await _dbContext.SaveChangesAsync();
            var result = await _crudOperationsService.AddtoTableAsync(sahil);
            return Ok($"{sahil.Count} records inserted successfully!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,SahilTable sahil)
        {
            //if(id != sahil.rowval) {  return BadRequest(); }
            //_dbContext.Entry(sahil).State = EntityState.Modified;
            //await _dbContext.SaveChangesAsync();
            //return NoContent();

            return await _crudOperationsService.Update(id,sahil) ? Ok(sahil) : NotFound();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var sahil = await _dbContext.SahilTable.FindAsync(id);
            //if (sahil == null) { return NotFound(); }
            //_dbContext.SahilTable.Remove(sahil);
            //await _dbContext.SaveChangesAsync();
            //return NoContent();

            return await _crudOperationsService.Delete(id) ? Ok("Entry Deleted") : BadRequest();


        }
    }
}
