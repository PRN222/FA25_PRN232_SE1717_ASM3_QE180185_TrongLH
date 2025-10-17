namespace EVCharging.Repositories.TrongLH.ModelExtensions;

public class SearchRequest
{
    public SearchRequest(int? currentPage, int? pageSize)
    {
        if (currentPage.HasValue && currentPage > 0) CurrentPage = currentPage;
        if (pageSize.HasValue && pageSize > 0) PageSize = pageSize;
    }

    public int? CurrentPage { get; set; } = 1;
    public int? PageSize { get; set; } = 50;
}

public sealed class EnergySupplyTrongLhSearchRequest : SearchRequest
{
    public EnergySupplyTrongLhSearchRequest(string? supplyType, decimal? capacityKw, string? stationName,
        int? currentPage, int? pageSize) : base(currentPage, pageSize)
    {
        SupplyType = supplyType;
        CapacityKw = capacityKw;
        StationName = stationName;
    }

    public string? SupplyType { get; set; } // Bảng chính EnergySupplyTrongLh
    public decimal? CapacityKw { get; set; } // Bảng chính EnergySupplyTrongLh
    public string? StationName { get; set; } // Bảng phụ StationTrongLh
}