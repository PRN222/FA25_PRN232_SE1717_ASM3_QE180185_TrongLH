using System.ComponentModel.DataAnnotations;

namespace EVCharging.Repositories.TrongLH.Models;

public class StationTrongLh
{
    [Key] public int StationTrongLhid { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? Location { get; set; }

    public string? City { get; set; }

    public string? Province { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? OwnerCompany { get; set; }

    public string? ContactNumber { get; set; }

    public int? TotalChargers { get; set; }

    public string? Status { get; set; }

    public bool HasWifi { get; set; }

    public bool HasRestroom { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<EnergySupplyTrongLh> EnergySupplyTrongLhs { get; set; } =
        new List<EnergySupplyTrongLh>();
}