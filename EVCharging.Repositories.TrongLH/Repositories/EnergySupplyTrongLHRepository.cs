using EVCharging.Repositories.TrongLH.Basic;
using EVCharging.Repositories.TrongLH.Context;
using EVCharging.Repositories.TrongLH.ModelExtensions;
using EVCharging.Repositories.TrongLH.Models;
using Microsoft.EntityFrameworkCore;

namespace EVCharging.Repositories.TrongLH.Repositories;

public class EnergySupplyTrongLHRepository : GenericRepository<EnergySupplyTrongLh>
{
    public EnergySupplyTrongLHRepository()
    {
    }

    public EnergySupplyTrongLHRepository(FA25_PRN232_SE1717_G2_EVChargingContext context) : base(context)
    {
    }

    public async Task<List<EnergySupplyTrongLh>> GetAllAsync()
    {
        var items = await _context.EnergySupplyTrongLhs.Include(e => e.StationTrongLh).ToListAsync();
        return items;
    }

    public async Task<PaginationResult<List<EnergySupplyTrongLh>>> GetAllAsync(
        EnergySupplyTrongLhSearchRequest request)
    {
        var pageSize = request.PageSize > 0 ? request.PageSize.Value : 10;
        var currentPage = request.CurrentPage > 0 ? request.CurrentPage.Value : 1;

        var query = _context.EnergySupplyTrongLhs
            .Include(e => e.StationTrongLh)
            .AsQueryable();

        // Apply filters only if search criteria are provided
        if (!string.IsNullOrEmpty(request.SupplyType))
        {
            query = query.Where(ev => ev.SupplyType != null && ev.SupplyType.Contains(request.SupplyType));
        }

        if (request.CapacityKw.HasValue && request.CapacityKw > 0)
        {
            query = query.Where(ev => ev.CapacityKw >= request.CapacityKw);
        }

        if (!string.IsNullOrEmpty(request.StationName))
        {
            query = query.Where(ev => ev.StationTrongLh != null &&
                                    ev.StationTrongLh.Name != null &&
                                    ev.StationTrongLh.Name.Contains(request.StationName));
        }

        // If no search criteria provided, return all records
        // This ensures we get all records when no filters are applied

        query = query.OrderByDescending(ev => ev.CreatedAt);

        var items = await query
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalItems = await query.CountAsync();
        var result = new PaginationResult<List<EnergySupplyTrongLh>>
        {
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
            CurrentPage = currentPage,
            PageSize = pageSize,
            Items = items
        };

        return result;
    }

    public async Task<EnergySupplyTrongLh?> GetByIdAsync(int id)
    {
        var item = await _context.EnergySupplyTrongLhs
            .Include(e => e.StationTrongLh)
            .FirstOrDefaultAsync(e => e.EnergySupplyTrongLhid == id);
        return item;
    }
}