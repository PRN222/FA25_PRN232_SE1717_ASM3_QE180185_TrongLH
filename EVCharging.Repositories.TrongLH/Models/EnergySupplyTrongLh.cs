using System.ComponentModel.DataAnnotations;

namespace EVCharging.Repositories.TrongLH.Models;

public class EnergySupplyTrongLh
{
    [Key] public int? EnergySupplyTrongLhid { get; set; }

    public int StationTrongLhid { get; set; }

    public string? SupplyType { get; set; }

    public decimal? CapacityKw { get; set; }

    public decimal? AvailableKw { get; set; }

    public string? SourceName { get; set; }

    public DateTime? StartDate { get; set; }

    public string? ContractNumber { get; set; }

    public bool IsRenewable { get; set; }

    public decimal? PeakCapacity { get; set; }

    public decimal? EfficiencyRate { get; set; }

    public DateTime CreatedAt { get; set; }
    public virtual StationTrongLh? StationTrongLh { get; set; }
}