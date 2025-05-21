using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Cuisine;

namespace EXE202_BE.Service.Interface;

public interface ICuisinesService
{
    Task<PageListResponse<CuisineResponse>> GetCuisinesAsync(string? searchTerm, int page, int pageSize);
}