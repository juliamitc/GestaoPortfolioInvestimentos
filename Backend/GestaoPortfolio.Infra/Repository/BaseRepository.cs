using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task<TEntity> GetById(object id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> Insert(TEntity obj)
        {
            await _entities.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<TEntity> Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(int id)
        {
            _context.Set<TEntity>().Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }
    }
}
