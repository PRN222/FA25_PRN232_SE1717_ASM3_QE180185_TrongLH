using System.ComponentModel.DataAnnotations;

namespace EVCharging.WebApp.TrongLH.Models;

public class EnergySupplyViewModel
{
    public int? EnergySupplyTrongLhid { get; set; }

    [Required(ErrorMessage = "Station is required")]
    [Display(Name = "Station")]
    public int StationTrongLhid { get; set; }

    [Required(ErrorMessage = "Supply Type is required")]
    [StringLength(50, ErrorMessage = "Supply Type cannot exceed 50 characters")]
    [Display(Name = "Supply Type")]
    public string SupplyType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Capacity is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
    [Display(Name = "Capacity (kW)")]
    public double CapacityKw { get; set; }

    [Required(ErrorMessage = "Available Capacity is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Available Capacity must be 0 or greater")]
    [Display(Name = "Available Capacity (kW)")]
    public double AvailableKw { get; set; }

    [Required(ErrorMessage = "Source Name is required")]
    [StringLength(100, ErrorMessage = "Source Name cannot exceed 100 characters")]
    [Display(Name = "Source Name")]
    public string SourceName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Start Date is required")]
    [Display(Name = "Start Date")]
    public string StartDate { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "Contract Number cannot exceed 50 characters")]
    [Display(Name = "Contract Number")]
    public string? ContractNumber { get; set; }

    [Display(Name = "Is Renewable")]
    public bool IsRenewable { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Peak Capacity must be 0 or greater")]
    [Display(Name = "Peak Capacity (kW)")]
    public double? PeakCapacity { get; set; }

    [Range(0, 100, ErrorMessage = "Efficiency Rate must be between 0 and 100")]
    [Display(Name = "Efficiency Rate (%)")]
    public double? EfficiencyRate { get; set; }

    [Display(Name = "Created At")]
    public string? CreatedAt { get; set; }

    // For display purposes
    [Display(Name = "Station Name")]
    public string? StationName { get; set; }

    // For dropdown
    public List<StationOption>? AvailableStations { get; set; }
}

public class StationOption
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}
