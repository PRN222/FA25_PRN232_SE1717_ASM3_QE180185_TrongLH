using EVCharing.gRPCService.TrongLH;
using EVCharging.Repositories.TrongLH.Context;
using Grpc.Core;

namespace EVCharing.gRPCService.TrongLH.Services;

public class StationTrongLhGRPCService : StationTrongLhGRPC.StationTrongLhGRPCBase
{
    private readonly FA25_PRN232_SE1717_G2_EVChargingContext _context = new();
    private readonly ILogger<StationTrongLhGRPCService> _logger;

    public StationTrongLhGRPCService(ILogger<StationTrongLhGRPCService> logger)
    {
        _logger = logger;
    }

    public override Task<StationList> GetAllAsync(StationEmptyRequest request, ServerCallContext context)
    {
        var list = new StationList();
        foreach (var s in _context.StationTrongLhs)
        {
            list.Stations.Add(new Station
            {
                StationTrongLhid = s.StationTrongLhid,
                Name = s.Name ?? string.Empty,
                Code = s.Code ?? string.Empty,
                Location = s.Location ?? string.Empty
            });
        }
        return Task.FromResult(list);
    }

    public override Task<Station> GetByIdAsync(StationIdRequest request, ServerCallContext context)
    {
        var s = _context.StationTrongLhs.FirstOrDefault(x => x.StationTrongLhid == request.Id);
        if (s == null) return Task.FromResult(new Station());
        return Task.FromResult(new Station
        {
            StationTrongLhid = s.StationTrongLhid,
            Name = s.Name ?? string.Empty,
            Code = s.Code ?? string.Empty,
            Location = s.Location ?? string.Empty
        });
    }
}


