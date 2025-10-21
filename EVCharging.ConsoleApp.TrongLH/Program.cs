using EVCharing.ConsoleApp.TrongLH;
using Grpc.Net.Client;

// DELAY 3s to wait for the gRPC server to be ready
Console.WriteLine("Waiting for gRPC server to be ready...");
await Task.Delay(3000);

var channel = GrpcChannel.ForAddress("http://localhost:5089");

var grpcClient = new EnergySupplyTrongLhGRPC.EnergySupplyTrongLhGRPCClient(channel);

Console.WriteLine("Get all items from gRPC service...");
var energySupplyList = grpcClient.GetAllAsync(new EmptyRequest());
if (energySupplyList != null && energySupplyList.EnergySupplies.Count > 0)
    foreach (var item in energySupplyList.EnergySupplies)
    {
        Console.WriteLine(
            $"ID: {item.EnergySupplyTrongLhid}, SupplyType: {item.SupplyType}, SourceName: {item.SourceName}");
        Console.WriteLine(
            $"  StationId: {item.StationTrongLhid}, Capacity: {item.CapacityKw}kW, Available: {item.AvailableKw}kW");
        Console.WriteLine(
            $"  Renewable: {item.IsRenewable}, Peak: {item.PeakCapacity}kW, Efficiency: {item.EfficiencyRate}%");
        Console.WriteLine(
            $"  Contract: {item.ContractNumber}, StartDate: {item.StartDate}, CreatedAt: {item.CreatedAt}");
        Console.WriteLine("---");
    }
else
    Console.WriteLine("No items found.");

Console.WriteLine("\nPress any key to exit...");
Console.Read();