using EVCharging.WebApp.TrongLH.Models;
using EVCharging.WebApp.TrongLH.Services;
using Microsoft.AspNetCore.Mvc;

namespace EVCharging.WebApp.TrongLH.Controllers;

public class EnergySupplyController : Controller
{
    private readonly EnergySupplyGrpcService _energySupplyService;
    private readonly StationGrpcService _stationService;
    private readonly ILogger<EnergySupplyController> _logger;

    public EnergySupplyController(
        EnergySupplyGrpcService energySupplyService,
        StationGrpcService stationService,
        ILogger<EnergySupplyController> logger)
    {
        _energySupplyService = energySupplyService;
        _stationService = stationService;
        _logger = logger;
    }

    // GET: EnergySupply
    public async Task<IActionResult> Index()
    {
        try
        {
            var energySupplies = await _energySupplyService.GetAllAsync();

            // Get station names for display
            var stations = await _stationService.GetAllStationsAsync();
            var stationDict = stations.ToDictionary(s => s.Id, s => s.Name);

            foreach (var energySupply in energySupplies)
            {
                if (stationDict.ContainsKey(energySupply.StationTrongLhid))
                {
                    energySupply.StationName = stationDict[energySupply.StationTrongLhid];
                }
            }

            return View(energySupplies);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading energy supplies");
            TempData["Error"] = "An error occurred while loading energy supplies.";
            return View(new List<EnergySupplyViewModel>());
        }
    }

    // GET: EnergySupply/Details/5
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var energySupply = await _energySupplyService.GetByIdAsync(id);
            if (energySupply == null)
            {
                return NotFound();
            }

            // Get station name
            var station = await _stationService.GetStationByIdAsync(energySupply.StationTrongLhid);
            energySupply.StationName = station?.Name ?? "Unknown";

            return View(energySupply);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading energy supply details");
            TempData["Error"] = "An error occurred while loading energy supply details.";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: EnergySupply/Create
    public async Task<IActionResult> Create()
    {
        try
        {
            var stations = await _stationService.GetAllStationsAsync();
            var model = new EnergySupplyViewModel
            {
                AvailableStations = stations,
                StartDate = DateTime.Now.ToString("yyyy-MM-dd")
            };
            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading create form");
            TempData["Error"] = "An error occurred while loading the create form.";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: EnergySupply/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EnergySupplyViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var result = await _energySupplyService.CreateAsync(model);
                if (result > 0)
                {
                    TempData["Success"] = "Energy supply created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Failed to create energy supply.";
                }
            }

            // Reload stations for dropdown
            model.AvailableStations = await _stationService.GetAllStationsAsync();
            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating energy supply");
            TempData["Error"] = "An error occurred while creating the energy supply.";
            model.AvailableStations = await _stationService.GetAllStationsAsync();
            return View(model);
        }
    }

    // GET: EnergySupply/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var energySupply = await _energySupplyService.GetByIdAsync(id);
            if (energySupply == null)
            {
                return NotFound();
            }

            // Get stations for dropdown
            energySupply.AvailableStations = await _stationService.GetAllStationsAsync();

            return View(energySupply);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading edit form");
            TempData["Error"] = "An error occurred while loading the edit form.";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: EnergySupply/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EnergySupplyViewModel model)
    {
        try
        {
            if (id != model.EnergySupplyTrongLhid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _energySupplyService.UpdateAsync(model);
                if (result > 0)
                {
                    TempData["Success"] = "Energy supply updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Failed to update energy supply.";
                }
            }

            // Reload stations for dropdown
            model.AvailableStations = await _stationService.GetAllStationsAsync();
            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating energy supply");
            TempData["Error"] = "An error occurred while updating the energy supply.";
            model.AvailableStations = await _stationService.GetAllStationsAsync();
            return View(model);
        }
    }

    // GET: EnergySupply/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var energySupply = await _energySupplyService.GetByIdAsync(id);
            if (energySupply == null)
            {
                return NotFound();
            }

            // Get station name
            var station = await _stationService.GetStationByIdAsync(energySupply.StationTrongLhid);
            energySupply.StationName = station?.Name ?? "Unknown";

            return View(energySupply);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading delete confirmation");
            TempData["Error"] = "An error occurred while loading the delete confirmation.";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: EnergySupply/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var result = await _energySupplyService.DeleteAsync(id);
            if (result > 0)
            {
                TempData["Success"] = "Energy supply deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to delete energy supply.";
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting energy supply");
            TempData["Error"] = "An error occurred while deleting the energy supply.";
            return RedirectToAction(nameof(Index));
        }
    }
}
