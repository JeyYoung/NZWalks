using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _context;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            _context = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _context.AddAsync(region);
            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(region == null)
            { 
                return null; 
            }

            //Delete the region
            _context.Regions.Remove(region);

            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();

        }

        public async Task<Region> GetAsync(Guid id)
        {
           return await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
           
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await _context.SaveChangesAsync();


            return existingRegion;
        }
    }
}
