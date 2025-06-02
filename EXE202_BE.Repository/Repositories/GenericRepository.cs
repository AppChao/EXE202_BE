using EXE202_BE.Data.Models;

namespace EXE202_BE.Repository.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EXE202_BE.Repository.Interface;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        internal readonly DbSet<T> DbSet;

        public GenericRepository (AppDbContext db)
        {
            _db = db;
            DbSet = db.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            using (var transaction = _db.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                // Your insert operation
                await transaction.CommitAsync();
            }
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        
        public AppDbContext GetDbContext()
        {
            return _db;
        }
    }

