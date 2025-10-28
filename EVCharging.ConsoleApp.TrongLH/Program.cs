using EVCharing.ConsoleApp.TrongLH;
using Grpc.Net.Client;

// DELAY 3s to wait for the gRPC server to be ready
Console.WriteLine("Waiting for gRPC server to be ready...");
await Task.Delay(3000);

var channel = GrpcChannel.ForAddress("http://localhost:5089");
var grpcClient = new EnergySupplyTrongLhGRPC.EnergySupplyTrongLhGRPCClient(channel);

Console.WriteLine("=== Energy Supply Management System ===");
Console.WriteLine("gRPC Service Connected Successfully!\n");

while (true)
{
    ShowMainMenu();
    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                await GetAllEnergySupplies(grpcClient);
                break;
            case "2":
                await GetEnergySupplyById(grpcClient);
                break;
            case "3":
                await CreateEnergySupply(grpcClient);
                break;
            case "4":
                await UpdateEnergySupply(grpcClient);
                break;
            case "5":
                await DeleteEnergySupply(grpcClient);
                break;
            case "0":
                Console.WriteLine("Goodbye!");
                return;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
    Console.Clear();
}

static void ShowMainMenu()
{
    Console.WriteLine("=== MAIN MENU ===");
    Console.WriteLine("1. Get All Energy Supplies");
    Console.WriteLine("2. Get Energy Supply by ID");
    Console.WriteLine("3. Create New Energy Supply");
    Console.WriteLine("4. Update Energy Supply");
    Console.WriteLine("5. Delete Energy Supply");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");
}

static async Task GetAllEnergySupplies(EnergySupplyTrongLhGRPC.EnergySupplyTrongLhGRPCClient client)
{
    Console.WriteLine("\n=== GET ALL ENERGY SUPPLIES ===");

    var energySupplyList = client.GetAllAsync(new EmptyRequest());
    if (energySupplyList != null && energySupplyList.EnergySupplies.Count > 0)
    {
        Console.WriteLine($"Found {energySupplyList.EnergySupplies.Count} energy supplies:\n");
        foreach (var item in energySupplyList.EnergySupplies)
        {
            Console.WriteLine($"ID: {item.EnergySupplyTrongLhid}, SupplyType: {item.SupplyType}, SourceName: {item.SourceName}");
            Console.WriteLine($"  StationId: {item.StationTrongLhid}, Capacity: {item.CapacityKw}kW, Available: {item.AvailableKw}kW");
            Console.WriteLine($"  Renewable: {item.IsRenewable}, Peak: {item.PeakCapacity}kW, Efficiency: {item.EfficiencyRate}%");
            Console.WriteLine($"  Contract: {item.ContractNumber}, StartDate: {item.StartDate}, CreatedAt: {item.CreatedAt}");
            Console.WriteLine("---");
        }
    }
    else
    {
        Console.WriteLine("No energy supplies found.");
    }
}

static async Task GetEnergySupplyById(EnergySupplyTrongLhGRPC.EnergySupplyTrongLhGRPCClient client)
{
    Console.WriteLine("\n=== GET ENERGY SUPPLY BY ID ===");
    Console.Write("Enter Energy Supply ID: ");

    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var energySupply = client.GetByIdAsync(new IdRequest { Id = id });
        if (energySupply != null && energySupply.EnergySupplyTrongLhid > 0)
        {
            Console.WriteLine("\nEnergy Supply Details:");
            Console.WriteLine($"ID: {energySupply.EnergySupplyTrongLhid}");
            Console.WriteLine($"Station ID: {energySupply.StationTrongLhid}");
            Console.WriteLine($"Supply Type: {energySupply.SupplyType}");
            Console.WriteLine($"Source Name: {energySupply.SourceName}");
            Console.WriteLine($"Capacity: {energySupply.CapacityKw}kW");
            Console.WriteLine($"Available: {energySupply.AvailableKw}kW");
            Console.WriteLine($"Peak Capacity: {energySupply.PeakCapacity}kW");
            Console.WriteLine($"Efficiency Rate: {energySupply.EfficiencyRate}%");
            Console.WriteLine($"Is Renewable: {energySupply.IsRenewable}");
            Console.WriteLine($"Contract Number: {energySupply.ContractNumber}");
            Console.WriteLine($"Start Date: {energySupply.StartDate}");
            Console.WriteLine($"Created At: {energySupply.CreatedAt}");
        }
        else
        {
            Console.WriteLine("Energy supply not found.");
        }
    }
    else
    {
        Console.WriteLine("Invalid ID format.");
    }
}

static async Task CreateEnergySupply(EnergySupplyTrongLhGRPC.EnergySupplyTrongLhGRPCClient client)
{
    Console.WriteLine("\n=== CREATE NEW ENERGY SUPPLY ===");

    var newEnergySupply = new EnergySupplyTrongLh
    {
        EnergySupplyTrongLhid = 0, // Will be ignored by server (identity column)
        StationTrongLhid = GetIntInput("Station ID"),
        SupplyType = GetStringInput("Supply Type (e.g., Solar, Grid, Wind)"),
        CapacityKw = GetDoubleInput("Capacity (kW)"),
        AvailableKw = GetDoubleInput("Available Capacity (kW)"),
        SourceName = GetStringInput("Source Name"),
        StartDate = GetStringInput("Start Date (yyyy-MM-dd)"),
        ContractNumber = GetStringInput("Contract Number"),
        IsRenewable = GetBoolInput("Is Renewable (true/false)"),
        PeakCapacity = GetDoubleInput("Peak Capacity (kW)"),
        EfficiencyRate = GetDoubleInput("Efficiency Rate (%)"),
        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    };

    var result = client.CreateAsync(newEnergySupply);
    Console.WriteLine($"\nCreate result: {result.AffectedRows} rows affected");
}

static async Task UpdateEnergySupply(EnergySupplyTrongLhGRPC.EnergySupplyTrongLhGRPCClient client)
{
    Console.WriteLine("\n=== UPDATE ENERGY SUPPLY ===");
    Console.Write("Enter Energy Supply ID to update: ");

    if (int.TryParse(Console.ReadLine(), out int id))
    {
        // First get the existing record
        var existing = client.GetByIdAsync(new IdRequest { Id = id });
        if (existing == null || existing.EnergySupplyTrongLhid == 0)
        {
            Console.WriteLine("Energy supply not found.");
            return;
        }

        Console.WriteLine("\nCurrent values (press Enter to keep current value):");

        var updatedEnergySupply = new EnergySupplyTrongLh
        {
            EnergySupplyTrongLhid = id,
            StationTrongLhid = GetIntInput($"Station ID (current: {existing.StationTrongLhid})", existing.StationTrongLhid),
            SupplyType = GetStringInput($"Supply Type (current: {existing.SupplyType})", existing.SupplyType),
            CapacityKw = GetDoubleInput($"Capacity (current: {existing.CapacityKw})", existing.CapacityKw),
            AvailableKw = GetDoubleInput($"Available Capacity (current: {existing.AvailableKw})", existing.AvailableKw),
            SourceName = GetStringInput($"Source Name (current: {existing.SourceName})", existing.SourceName),
            StartDate = GetStringInput($"Start Date (current: {existing.StartDate})", existing.StartDate),
            ContractNumber = GetStringInput($"Contract Number (current: {existing.ContractNumber})", existing.ContractNumber),
            IsRenewable = GetBoolInput($"Is Renewable (current: {existing.IsRenewable})", existing.IsRenewable),
            PeakCapacity = GetDoubleInput($"Peak Capacity (current: {existing.PeakCapacity})", existing.PeakCapacity),
            EfficiencyRate = GetDoubleInput($"Efficiency Rate (current: {existing.EfficiencyRate})", existing.EfficiencyRate),
            CreatedAt = existing.CreatedAt
        };

        var result = client.UpdateAsync(updatedEnergySupply);
        Console.WriteLine($"\nUpdate result: {result.AffectedRows} rows affected");
    }
    else
    {
        Console.WriteLine("Invalid ID format.");
    }
}

static async Task DeleteEnergySupply(EnergySupplyTrongLhGRPC.EnergySupplyTrongLhGRPCClient client)
{
    Console.WriteLine("\n=== DELETE ENERGY SUPPLY ===");
    Console.Write("Enter Energy Supply ID to delete: ");

    if (int.TryParse(Console.ReadLine(), out int id))
    {
        Console.Write($"Are you sure you want to delete Energy Supply ID {id}? (y/N): ");
        var confirm = Console.ReadLine();

        if (confirm?.ToLower() == "y" || confirm?.ToLower() == "yes")
        {
            var result = client.DeleteAsync(new IdRequest { Id = id });
            Console.WriteLine($"\nDelete result: {result.AffectedRows} rows affected");
        }
        else
        {
            Console.WriteLine("Delete cancelled.");
        }
    }
    else
    {
        Console.WriteLine("Invalid ID format.");
    }
}

// Helper methods for input
static int GetIntInput(string prompt, int? defaultValue = null)
{
    while (true)
    {
        Console.Write($"{prompt}: ");
        var input = Console.ReadLine();

        if (string.IsNullOrEmpty(input) && defaultValue.HasValue)
            return defaultValue.Value;

        if (int.TryParse(input, out int result))
            return result;

        Console.WriteLine("Invalid input. Please enter a valid integer.");
    }
}

static double GetDoubleInput(string prompt, double? defaultValue = null)
{
    while (true)
    {
        Console.Write($"{prompt}: ");
        var input = Console.ReadLine();

        if (string.IsNullOrEmpty(input) && defaultValue.HasValue)
            return defaultValue.Value;

        if (double.TryParse(input, out double result))
            return result;

        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
}

static string GetStringInput(string prompt, string? defaultValue = null)
{
    Console.Write($"{prompt}: ");
    var input = Console.ReadLine();

    if (string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(defaultValue))
        return defaultValue;

    return input ?? "";
}

static bool GetBoolInput(string prompt, bool? defaultValue = null)
{
    while (true)
    {
        Console.Write($"{prompt}: ");
        var input = Console.ReadLine();

        if (string.IsNullOrEmpty(input) && defaultValue.HasValue)
            return defaultValue.Value;

        if (bool.TryParse(input, out bool result))
            return result;

        if (input?.ToLower() == "true" || input?.ToLower() == "false")
            return input.ToLower() == "true";

        Console.WriteLine("Invalid input. Please enter 'true' or 'false'.");
    }
}