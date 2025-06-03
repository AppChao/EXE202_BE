using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_BE.Repository.Repositories;

public class HealthConditionsRepository : GenericRepository<HealthConditions>, IHealthConditionsRepository
{
    public HealthConditionsRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<PageListResponse<HealthConditions>> GetHealthConditionsAsync(string? type, string? searchTerm, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        IQueryable<HealthConditions> query = DbSet;

        if (!string.IsNullOrEmpty(type))
        {
            query = query.Where(hc => hc.HealthConditionType != null && hc.HealthConditionType.ToLower().Contains(type.ToLower()));
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            query = query.Where(hc =>
                (hc.HealthConditionName != null && hc.HealthConditionName.ToLower().Contains(searchTerm)) ||
                (hc.BriefDescription != null && hc.BriefDescription.ToLower().Contains(searchTerm)));
        }

        var totalCount = await query.CountAsync();

        var paginatedItems = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PageListResponse<HealthConditions>
        {
            Items = paginatedItems,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            HasNextPage = (page * pageSize) < totalCount,
            HasPreviousPage = page > 1
        };
    }

    public async Task<PageListResponse<string>> GetHealthConditionTypesAsync(string? searchTerm, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        IQueryable<string> query = DbSet
            .Select(hc => hc.HealthConditionType!)
            .Distinct();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            query = query.Where(t => t.ToLower().Contains(searchTerm));
        }

        var totalCount = await query.CountAsync();

        var paginatedItems = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PageListResponse<string>
        {
            Items = paginatedItems,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            HasNextPage = (page * pageSize) < totalCount,
            HasPreviousPage = page > 1
        };
    }
}