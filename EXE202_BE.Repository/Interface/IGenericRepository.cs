using System.Linq.Expressions;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Repository.Interface;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
    Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    AppDbContext GetDbContext();
}