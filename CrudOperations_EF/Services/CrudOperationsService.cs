
using CrudOperations_EF.Data;
using CrudOperations_EF.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CrudOperations_EF.Services
{
    public class CrudOperationsService
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;

        public CrudOperationsService(AppDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        private const string CacheKey = "SahilTable";
        List<SahilTable> _sahilTable = new List<SahilTable>();
        //public List<SahilTable> GetAll() => _sahilTable;
        public async Task<SahilTable?> GetById(int id)
        {
            try
            {

            }
            catch(Exception E) { }
            string key = $"user-{id}";

            if (!_cache.TryGetValue(key, out SahilTable? user))
            {
                user = await _context.SahilTable.FirstOrDefaultAsync(x => x.rowval == id);

                if (user != null)
                {
                    _cache.Set(key, user, TimeSpan.FromMinutes(30));
                }
            }

            return user;
        }

        public async Task<List<SahilTable>> GetAll()
        {
            try
            {
                if (!_cache.TryGetValue(CacheKey, out List<SahilTable> users))
                {
                    users = await _context.SahilTable.ToListAsync();
                    var CacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10))
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                    _cache.Set(CacheKey, users, CacheOptions);
                }

                return users;

            }
            catch (Exception E) { return new List<SahilTable>(); }
            

        }
        public async Task<List<SahilTable>> AddtoTableAsync(List<SahilTable> table)
        {
            try
            {
                await _context.SahilTable.AddRangeAsync(table);
                await _context.SaveChangesAsync();

                _cache.Remove(CacheKey);
                return table;



            }
            catch (Exception E) { return new List<SahilTable>(); }
       
        }

        public async Task<bool> Update(int id , SahilTable sahilTable)
        {
            try
            {
                var Exists = GetById(id);
                if (Exists != null) { return false; }
                //Exists.Name = sahilTable.Name;
                //Exists.Sirname = sahilTable.Sirname;
                _context.Entry(sahilTable).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Clear caches
                _cache.Remove(CacheKey);
                _cache.Remove($"user-{id}");

                return true;

            }
            catch (Exception E)
            {
                return false;
            }
       

        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var sahiltable = await GetById(id);
                if (sahiltable == null) { return false; }

                _context.SahilTable.Remove(sahiltable);
                await _context.SaveChangesAsync();

                _cache.Remove(CacheKey);
                _cache.Remove($"user-{id}");
                return true;

            }
            catch (Exception E) {
                return false;

            }
           
        }

       
    }
}
