using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _context;


        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            _context = nZWalksDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await _context.AddAsync(walk);
            await _context.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await _context.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

             _context.Walks.Remove(existingWalk);

            await _context.SaveChangesAsync();

            return existingWalk;
            
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await 
                _context.Walks
                .Include(x=> x.Region)
                .Include(x=> x.WalkDifficulty)
                .ToListAsync();

        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
           var existingWalk = await _context.Walks.FindAsync(id);

            if (existingWalk != null)
            {
                existingWalk.Length = walk.Length;
                existingWalk.Name = walk.Name;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId = walk.RegionId;
                await _context.SaveChangesAsync();
                return (existingWalk);
            }

            return null;
        }
    }
}
